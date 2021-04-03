﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Globalization;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Bovelo
{
    public class App //Super class, it takes everything from the database and will send anything to it
    {
        internal List<User> userList; //All users from DB( Representative, Assembler, ProductionManager)
        internal List<BikePart> bikePartList = new List<BikePart>();
        internal List<BikeModel> bikeModels; //All bike types (Adventure, city and explorer)
        internal List<OrderBike> orderBikeList; //takes all the orders from the DB 
        internal List<Planning> planningList; //takes all the plannings from the DB
        private List<List<string>> _linkingPartList = new List<List<string>>();
        public App(bool getAll = false, bool getUpdatable = false)
        {
            if (getAll)
            {
                this.bikePartList = getBikePartList();
                this.bikeModels = getBikeModelList();
                this.userList = getUserList();
                updateOrderBikeList();
                updatePlanningList();
                updateLinkingPartList();
            }
            if (getUpdatable)
            {
                updateOrderBikeList();
                updatePlanningList();
            }
        }

        //methods connecting to the DB

        

        /*internal List<List<string>> getFromDbInnerJoin(string week)
        {
            var listInnerJoin = new List<List<string>>();
            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);

            conn.Open();
            string sql = "SELECT * FROM Order_Details inner join  Bovelo.Detailed_Schedules on Detailed_Schedules.Id_Order_Details = Order_Details.Id_Order_Details where Detailed_Schedules.Week_Name = '" + week + "';";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var col = new List<string>();
                for (int j = 0; j < rdr.FieldCount; j++)
                {
                    col.Add(rdr[j].ToString());
                }
                listInnerJoin.Add(col);
            }
            rdr.Close();
            conn.Close();
            return listInnerJoin;
        }*/


        internal void sendToDB(string query) //is used to send anything to the database
        {
            string connStr = "server=193.191.240.67;user=USER3;database=Bovelo;port=63304;password=USER3";
            MySqlConnection conn = new MySqlConnection(connStr);
            //Console.WriteLine("Connecting to MySQL to send new element...");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            cmd.Dispose();
            conn.Close();
        }

        //SET To the DB methods

        internal void setNewOrderBike(List<List<string>> newOrder, string clientName, int totPrice,int shippingWeek) //is used to pass a new order
        {

            int orderId;
            var daysToAdd = shippingWeek * 7;
            string values = "";
            int i = 0;

            //first request
            string queryOB = "INSERT INTO Order_Bikes(Customer_Name,Total_Price,Order_Date,Shipping_Time) VALUES('" + clientName + "', '" + totPrice + "' ,'" + DateTime.Now.ToString() + "','" + DateTime.Today.AddDays(daysToAdd).ToString() + "');";
            DataBase.SendToDB(queryOB);
            string id = DataBase.GetFromDBLastIdFromColumn("Order_Bikes", "Id_Order");

            //second request
            if (id ==string.Empty || id =="0")//if orderList is empty
            {
                orderId = 1;
            }
            else
            {
                orderId = Int32.Parse(id);
            }
            foreach (var element in newOrder)
            {
                string type = element[0];
                int size = Int32.Parse(element[1]);
                string color = element[2];
                int quantity = Int32.Parse(element[3]);
                int price = Int32.Parse(element[4]) / quantity;
                
                for (int q = 0; q < quantity; q++)
                {
                    /*string queryOD = "INSERT INTO Order_Details (Bike_Type,Bike_Size,Bike_Color,Price,Bike_Status,Id_Order) VALUES('" + type + "', '" + size + "','" + color + "' , '" + price + "', 'New' , '" + orderId + "'); ";
                    DataBase.SendToDB(queryOD);*/
                    values += "('" + type + "', '" + size + "','" + color + "' , '" + price + "', 'New' , '" + orderId + "')";
                    if (i ==newOrder.Count-1 &&q ==quantity-1)
                    {
                        values += ";";
                    }
                    else
                    {
                        values += ",";
                    }
                    
                }
                i++;
            }
            string queryOD = "INSERT INTO Order_Details (Bike_Type,Bike_Size,Bike_Color,Price,Bike_Status,Id_Order) VALUES" + values;
            DataBase.SendToDB(queryOD);
            //orderBikeList = getOrderBikeList();//updates the list 
        }
        internal void setNewUser(User user) //is used to add a new user (for ex: a new Assembler joins the team)
        {
            string query = "INSERT INTO Users (Login, Password, Role) VALUES ('" + user.login + "','NULL','" + user.userType.FirstOrDefault(x => x.Value == true).Key + "')";
            DataBase.SendToDB(query);
            updateUserList(); //At the end of set, put a get to update App class
            //userList.Add(user); //if latency problems, uncomment this line and comment "userList = getUserList();"
        }
        /*internal void setNewBikeModel(string type, int price, string time)//is used to add a new model (for ex: Electric)
        {

            string query = "INSERT INTO Bikes (Bike_Type,Price,Bike_total_time) VALUES ('" + type + "', " + price + ", '" + time + "')";
            DataBase.SendToDB(query);
            //bikeModel = getBikeModelList();//At the end of set, put a get to update App class
        }*/
        internal void setNewBikeModel(string type, int size, string color)//is used to add a new model (for ex: Electric)
        {
            string query = "INSERT INTO Bike_Model (Color,Size,Type_Model) VALUES ('" + color + "','" + size + "', '" + type + "')";
            DataBase.SendToDB(query);
            updateBikeModelList();//At the end of set, put a get to update App class
        }
        internal void setLinkBikePartsToBikeModel(int idBikeModel, List<int> idBikeParts)//id of the bikeModel and List of id's of BikeParts
        {
            foreach (var idPart in idBikeParts)
            {
                string query = "INSERT INTO Parts (Id_Bike_Parts,Bikes_Id) VALUES ('" + idPart + "',' " + idBikeModel + "')";
                DataBase.SendToDB(query);
            }
            updateBikeModelList();//At the end of set, put a get to update App class
        }
        internal void setNewPlanning(List<List<string>> planningCartList, string week)//NEED TO SET THE TABLES
        {
            updatePlanningList();

            int planningId = 1;
            int orderDetailsId; //NEED TO LINK ORDER DETAILS WITH THE CORRECT ID
            if (planningList.Count != 0)
            {
                planningId = planningList.Last().planningId;
            }

            foreach (var bikesToBuild in planningCartList)
            {
                Console.WriteLine("ID_ORDER_DETAILS : " + bikesToBuild[0] + "|" +  bikesToBuild[1] + "|" + bikesToBuild[2]);
                orderDetailsId = Int32.Parse(bikesToBuild[0]);
                /*string type = bikesToBuild[1];
                string size = bikesToBuild[2];
                string color = bikesToBuild[3];*/
                string queryPD = "INSERT INTO Detailed_Schedules (Week_Name,Id_Order_Details) VALUES('" + week + "' , '" + orderDetailsId + "'); ";
                DataBase.SendToDB(queryPD);

            }

            updatePlanningList();//At the end of set, put a get to update App class
        }
        internal void setNewBikePart(string name, int price = 0, int size = 0, string color = "null")//is used to create a new bikePart : takes name, size(0 for default,26,28) and color (null for default,black,red,blue)PROBLEM : Price/time differs from color and sizes
        {
            updateBikePartList();
            var rand = new Random();
            var bikePartLocation = new List<string>();
            string location = RandomString(3);
            string provider = RandomString(8);
            int timeToBuild = rand.Next(1, 15);
            string bikePartName = name;
            string query;
            int partPrice;
            if (price == 0)
            {
                partPrice = rand.Next(1, 101);
            }
            else
            {
                partPrice = price;
            }
            string RandomString(int length)//create a random string of letters and numbers
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[rand.Next(s.Length)]).ToArray());
            }
            foreach (var part in bikePartList)//takes every used location
            {
                bikePartLocation.Add(part.location);
            }
            while (bikePartLocation.FirstOrDefault(x => x.Contains(location)) != null)//verifies if the location is already in use
            {
                location = RandomString(3);
            }

            if (color == "null" && size != 0) { bikePartName += "_" + size.ToString(); }
            else if (color != "null" && size == 0) { bikePartName += "_" + color; }
            else if (color != "null" && size != 0) { bikePartName += "_" + color + "_" + size.ToString(); }

            query = "INSERT INTO Bike_Parts (Bike_Parts_Name,Quantity,Location,Price,Provider,Time_To_Build) VALUES('" + bikePartName + "' , '" + 0 + "' , '" + location + "' , '" + partPrice + "' , '" + provider + "' , '" + timeToBuild + "'); ";
            DataBase.SendToDB(query);
        }
        internal void deletePlanifiedBike(int idOrderDetail, string week)
        {
            string deleteQuery = "delete from Detailed_Schedules where Week_Name = '" + week + "' and Id_Order_Details='" + idOrderDetail + "';";
            DataBase.SendToDB(deleteQuery);
        }
        internal void updateSchedule(int id, string newWeek, string currentWeek)
        {
            string modifyQuery = "UPDATE Detailed_Schedules SET Week_name = '" + newWeek + "' WHERE Id_Order_Details = '" + id + "' and Week_Name = '" + currentWeek + "';";
            DataBase.SendToDB(modifyQuery);
        }

        //UPDATE lists from DB
        internal void updateUserList()
        {
            userList = getUserList();
        }
        internal void updateBikeModelList()
        {
            bikeModels = getBikeModelList();
        }
        internal void updateBikePartList()
        {
            bikePartList = getBikePartList();
        }
        internal void updateOrderBikeList()
        {
            orderBikeList = getOrderBikeList();
        }
        internal void updatePlanningList()
        {
            planningList = getPlanningList();
        }
        internal void updateLinkingPartList()
        {
            _linkingPartList = getLinkingPartList();
        }

        //GET from the DB methods
        internal List<List<string>> getLinkingPartList()
        {
            return DataBase.GetFromDB("Parts");
        }
        internal List<BikePart> getBikePartList()
        {
            List<BikePart> bikeParts = new List<BikePart>();
            var BikePartsFromDB = DataBase.GetFromDB("Bike_Parts");

            foreach (var row in BikePartsFromDB)
            {
                bikeParts.Add(new BikePart(Int32.Parse(row[0]), row[1], row[3], Int32.Parse(row[4]), row[5], Int32.Parse(row[6])) { quantity = Int32.Parse(row[2])});
            }

            return bikeParts;
        }
        internal void updateSatus(int id, string status, string user, string started, string finished)
        {

            string query = "UPDATE Order_Details SET Bike_Status = '" + status + "'  WHERE Id_Order_Details = '" + id + "' ;" +
                           "UPDATE Detailed_Schedules SET Assembled_by = '" + user + "', Started = '" + started + "', Finished = '" + finished + "'  WHERE Id_Order_Details = '" + id + "' ;";

            DataBase.SendToDB(query);
        }
        internal List<Planning> getPlanningList() //gets all plannings
        {
            updateBikeModelList();
            List<Planning> plannings = new List<Planning>();
            var detailedSchedules = DataBase.GetFromDB("Detailed_Schedules");
            var allorders = getOrderDetails();
            /*Dictionary<string, List<string>> weeks = new Dictionary<string, List<string>>();//dictionnary of WeekName as key and List of id_OrderDetails as value
            Dictionary<string, string> assemblerPerBikeId = new Dictionary<string, string>();*/
            Dictionary<string, Dictionary<string,string>> schedules = new Dictionary<string, Dictionary<string, string>>();//<weekName,<idOrderDetails,AssemblerName>>
            int id = 0;
            Dictionary<string, List<List<string>>> test = new Dictionary<string, List<List<string>>>(); //<weekName,<weekName,id_Order_Details,Assebled_By,Started,Finnished>
            foreach (var row in detailedSchedules)//each bike in Detailed_Schedules <weekName,id_Order_Details,Assebled_By,Started,Finnished>
            {
                if (!test.ContainsKey(row[0]))
                {
                    test.Add(row[0], new List<List<string>>() { row }); //adds the detailed schedule to the corresponding week 
                }
                else
                {
                    var values = test.FirstOrDefault(x => x.Key == row[0]).Value;
                    values.Add(row);
                    test[row[0]] = values;
                }

                /*if (!schedules.ContainsKey(row[0]))
                {
                    schedules.Add(row[0], new Dictionary<string, string>() { {row[1], row[2]} });
                }
                else
                {
                    var values = schedules.FirstOrDefault(x => x.Key == row[0]).Value;
                    values.Add(row[1], row[2]);
                    schedules[row[0]] = values;
                }*/
                /*if (!weeks.ContainsKey(row[0]))//if weekName does not exist yet
                {
                    weeks.Add(row[0], new List<string>() { row[1] });
                }
                else //if it exists
                {
                    var values = weeks.FirstOrDefault(k => k.Key == row[0]).Value;
                    values.Add(row[1]);
                    weeks[row[0]] = values;
                }
                assemblerPerBikeId.Add(row[1], row[2]);//adds every orderBike to a dictionnary with an assembler name*/
            }
            /*foreach(var row in schedules)
            {
                var bikes = allorders.FindAll(x => row.Value.Keys.Contains(x[0]));//finds all the bikesid added in a schedule
                plannings.Add(new Planning(bikes, row.Key, id, row.Value, bikeModels));
                id++;
            }*/
            foreach (var week in test)
            {
                List<List<string>> bikes = new List<List<string>>();
                foreach(var row in week.Value)
                {
                    bikes.Add(allorders.FirstOrDefault(x=>x[0] ==row[1]));
                    //Console.WriteLine(string.Join("\t", bikes.Last()) + week.Key);
                }
                plannings.Add(new Planning(bikes, week.Key, id, week.Value, bikeModels));
                id++;
            }
            /*foreach (var row in weeks)//foreach planning created
            {
                var bikes = allorders.FindAll(x => row.Value.Contains(x[0]));//matching the id_OrderDetails from all the orders and the ones from row.Value
                plannings.Add(new Planning(bikes, row.Key, id, assemblerPerBikeId, bikeModels));
                Console.WriteLine(bikes.Count);

                id++;
            }*/
            return plannings;

        }
        internal List<OrderBike> getOrderBikeList() //is used to get all Bike Orders NEED TO TRY
        {
            List<OrderBike> orderBikeList = new List<OrderBike>();
            var orderList = DataBase.GetFromDB("Order_Bikes");
            var orderDetailList = DataBase.GetFromDB("Order_Details");
            updateBikeModelList();

            foreach (var row in orderList)
            {
                List<List<string>> details = new List<List<string>>(orderDetailList.FindAll(x => x[6] == row[0]));//takes each lists with the same order_Id
                OrderBike newOrder = new OrderBike(row[1], details, Int32.Parse(row[0]), Convert.ToDateTime(row[3]), Convert.ToDateTime(row[4]), Int32.Parse(row[2]),bikeModels);

                //maybe put those 3 into a SET method inside orderBike
                /*newOrder.orderDate = Convert.ToDateTime(row[3]);
                newOrder.shippingDate = Convert.ToDateTime(row[4]);
                newOrder.totalPrice = Int32.Parse(row[2]);*/

                orderBikeList.Add(newOrder);//row[1] is the column where the name of the client is put

            }

            return orderBikeList;
        }
        internal List<List<string>> getOrderDetails()
        {
            var orderDetailList = DataBase.GetFromDB("Order_Details");
            return orderDetailList;
        }
        internal List<List<string>> getPlanifiedBikes()
        {
            string sql = "SELECT * FROM Order_Details inner join  Bovelo.Detailed_Schedules on Detailed_Schedules.Id_Order_Details = Order_Details.Id_Order_Details where Order_Details.Id_Order_Details In (select Id_Order_Details from Detailed_Schedules);";
            var Planified = DataBase.GetPlanifiedOrderDetails(sql);
            return Planified;
        }
        internal List<List<string>> getPlanifiedBikesByWeekName(string week)
        {
            string sql = "SELECT * FROM Order_Details inner join  Bovelo.Detailed_Schedules on Detailed_Schedules.Id_Order_Details = Order_Details.Id_Order_Details where Order_Details.Id_Order_Details In (select Id_Order_Details from Detailed_Schedules) and Week_Name = '" +week+ "';";
            var planifiedByWeek = DataBase.GetPlanifiedOrderDetails(sql);
            return planifiedByWeek;
        }
        internal List<List<string>> getAssemblerWork(string assemblerName)
        {
            string sql = "SELECT * FROM Order_Details inner join  Bovelo.Detailed_Schedules on Detailed_Schedules.Id_Order_Details = Order_Details.Id_Order_Details where Order_Details.Id_Order_Details In (select Id_Order_Details from Detailed_Schedules) and Assembled_by = '" + assemblerName + "';";
            var assemblerWork = DataBase.GetPlanifiedOrderDetails(sql);
            return assemblerWork;
        }

        internal List<List<string>> getAssembler()
        {
            string sql = "SELECT Login FROM Bovelo.Users where Role = 'Assembler';";
            var Assemblers = DataBase.GetPlanifiedOrderDetails(sql);
            return Assemblers;
        }
        internal List<List<string>> getNonPlanifiedBikes()
        {
            string sql = "Select * from Order_Details where Id_Order_Details Not In (select Id_Order_Details from Detailed_Schedules);";
            var nonPlanified = DataBase.GetPlanifiedOrderDetails(sql);
            return nonPlanified;
        }
        internal List<User> getUserList() //is used to get all users 
        {
            var userFromDB = new List<User>();
            //createBikeModel();
            List<List<string>> orderList = DataBase.GetFromDB("Users");
            foreach (var row in orderList)
            {
                string login = row[1];
                string userType = row[2];
                switch (userType)
                {
                    case "Representative":
                        userFromDB.Add(new User(login, true, false, false));
                        break;
                    case "Production Manager":
                        userFromDB.Add(new User(login, false, true, false));
                        break;
                    case "Assembler":
                        userFromDB.Add(new User(login, false, false, true));
                        break;
                    default:
                        //Console.WriteLine("user : " + login + ", is not registered correctly in the DataBase");
                        break;
                }
            }
            return userFromDB;
        }
        internal List<BikeModel> getBikeModelList() //is used to get all bike models
        {
            List<BikeModel> bikeList = new List<BikeModel>();//list to return
            List<List<string>> modelList = DataBase.GetFromDB("Bike_Model");//bikemodels from db
            if(_linkingPartList.Count == 0)
            {
                updateLinkingPartList();
            }
            if(bikePartList.Count == 0)
            {
                updateBikePartList();
            }
            foreach (var row in modelList)
            {
                List<int> bikePartsIds = new List<int>();
                int id = Int32.Parse(row[0]);
                string color = row[1];
                int size = Int32.Parse(row[2]);
                string type = row[3];
                var newBikeModel = new BikeModel(type, color, size) { idBikeModel = id };

                foreach (var part in _linkingPartList)
                {
                    if (Int32.Parse(part[1]) == id)
                    {
                        bikePartsIds.Add(Int32.Parse(part[0]));
                    }
                }
                bikePartsIds.Sort();
                newBikeModel.bikeParts = bikePartsIds.Select(x => bikePartList.First(part => part.part_Id == x)).ToList(); //FOR DUPLICATES 
                //newBikeModel.bikeParts = bikePartList.FindAll(part => bikePartsIds.Contains(part.part_Id));
                newBikeModel.setPriceAndTime();
                bikeList.Add(newBikeModel);
            }
            return bikeList;
        }
        /*internal List<BikeModel> getBikeModelList() //is used to get all bike models
        {
            List<BikeModel> bikeList = new List<BikeModel>();
            List<List<string>> modelList = getFromDB("Bikes");
            foreach (var row in modelList)
            {
                string Type = row[1];
                var newBikeModel = new BikeModel(Int32.Parse(row[0]), Type);
                newBikeModel.setBikeParts(getBikePartList());
                bikeList.Add(newBikeModel);
            }
            return bikeList;
        }*/
        /*internal List<string> getBikePartInvoice(List<OrderBike> orderBikeList)
        {
            string path = @"../../Classes/list_part.txt";
            IEnumerable<string> line = File.ReadLines(path);
            var BikeType =new Dictionary<int, List<string>>(); //  to stock data
            List<string> identity = new List<string>();        //  to add data to dict
            int e = 0;                                         //  key index 
            List<string> bikePart = new List<string>();        //  return value
            foreach (var elem in line)
            {
                int i = 0;
                int word = 0;
                string currentWord = "";                
                foreach (var character in elem)                             // reading word
                {
                    currentWord = "";
                    while (character != ';')                   // reading char
                    {
                        currentWord += character;                // concatenate char to make word
                        i++;
                    }
                    identity.Add(currentWord);                 // add word to list string
                    word++;                                    // next word
                    i++;                                       // pass ";" char
                    }
                BikeType.Add(e, identity);                     // add to dict list of word
                identity = new List<string>();                 // reset list of word
                e++;
            }

            foreach (var order in orderBikeList)
            {
                foreach (var bike in order.bikeList)
                {
                    
                    foreach (var elem in BikeType.Values)
                    {
                        if (bike.Type == elem[0])              // finding parts with goods size,type,color
                            if (bike.Size.ToString() == elem[1])                         
                                if (bike.Color == elem[2])
                                {
                                    for(int i = 2; i < 15; i++)
                                    { 
                                        bikePart.Add(elem[i]);
                                    }
                                }                                                               
                    }
                }
            }

            return bikePart;
        }*/
        internal List<BikePart> getSpecificBikePart(List<string> TypeSizeColor)
        {
            List<string> query = new List<string>();
            query.Add("*");
            List<List<string>> bikePart = new List<List<string>>();
            bikePart = DataBase.GetFromDBWhere("Bike_Parts", query, "Id_Bike_Parts IN ( SELECT Id_Bike_Parts FROM Parts WHERE Bikes_Id IN(SELECT idBike_Model FROM Bike_Model WHERE Color = '" + TypeSizeColor[2] + "' AND Type_Model = '" + TypeSizeColor[0] + "' AND Size = '" + TypeSizeColor[1] + "'))");
            List<BikePart> bikePartList = new List<BikePart>();
            foreach (var line in bikePart)
            {
                bikePartList.Add(new BikePart(Int32.Parse(line[0]), line[1], line[3], Int32.Parse(line[4]), line[5], Int32.Parse(line[6])));
            }
            return bikePartList;
        }
        internal Dictionary<int, int> getWeekPieces(string weekName) //really with weekName ?!
        {

            List<Bike> BikesToGetPieces = new List<Bike>();

            foreach (var planning in this.getPlanningList())
            {
                if (planning.weekName == weekName)
                {
                    BikesToGetPieces = planning.getBikesToBuild();
                }
            }
            List<int> differentPartsId = new List<int>();
            List<int> allPartsId = new List<int>();
            foreach (var bike in BikesToGetPieces)
            {
                foreach (var bikepart in bike.bikeParts)
                {
                    if (!differentPartsId.Contains(bikepart.part_Id))
                    {
                        differentPartsId.Add(bikepart.part_Id);
                    }
                    allPartsId.Add(bikepart.part_Id);
                }
            }
            Dictionary<int, int> PartIdQuantity = new Dictionary<int, int>();
            foreach (var partId in differentPartsId)
            {
                List<int> elem_to_count = allPartsId.FindAll(partID => partID == partId);
                PartIdQuantity.Add(partId, elem_to_count.Count());
                elem_to_count.Clear();
            }
            return PartIdQuantity;
        }
        public void addQuantity(int value, int part_Id)
        {
            int quantity = getQuantity(part_Id);
            quantity += value;
            DataBase.SendToDB("UPDATE Bike_Parts SET Quantity =" + quantity + " WHERE Id_Bike_Parts = " + part_Id + ";");
        }
        public int getQuantity(int part_Id)
        {
            List<string> argumentList = new List<string>();
            argumentList.Add("Quantity");
            string whereclause = "Id_Bike_Parts =" + part_Id;
            List<List<string>> result = DataBase.GetFromDBWhere("Bike_Parts", argumentList, whereclause);
            int quantity = Int32.Parse(result[0][0]);
            return quantity;
        }
        internal Dictionary<int, int> computeMissingPieces(ref Dictionary<int, int> PartIdQuantity)
        {
            updateBikePartList();
            int stockQuantity = 0;
            int quantityNeeded = 0;
            Dictionary<int, int> partOrderQuantity = new Dictionary<int, int>();
            foreach (var elem in PartIdQuantity)
            {
                //stockQuantity = getQuantity(elem.Key);
                stockQuantity = bikePartList.FirstOrDefault(x => x.part_Id == elem.Key).quantity;
                quantityNeeded = elem.Value;            // just to be clear
                int orderQuantity = 0;
                orderQuantity = quantityNeeded - stockQuantity + 10; //ex : I have 5, need 20 => order 25
                if (orderQuantity > 0) //means there is enough stock
                {
                    partOrderQuantity.Add(elem.Key, orderQuantity);
                }
                //NEED TO ADD THIS ORDER WITH THE ID TO A LIST         
            }
            return partOrderQuantity;
        }

        internal int getEstimatedTimeBeforeShipping(List<ItemBike> bikesToOrder)//WORKING ON IT
        {
            float days = 0;
            int weeks = 0;
            int minutes = 0;
            float hours = 0;
            //updateOrderBikeList();
            updatePlanningList();
            updateBikeModelList();
            var nonPlannified = getNonPlanifiedBikes();
            var nonPlannifiedBikes = new List<Bike>();
            //var order = orderBikeList.FirstOrDefault(x => x.orderId == orderId);
            foreach(var bike in nonPlannified)
            {
                BikeModel model = bikeModels.FirstOrDefault(x => x.Color == bike[3] && x.Size == Int32.Parse(bike[2]) && x.Type == bike[1]);//gets the specific model
                nonPlannifiedBikes.Add(new Bike(Int32.Parse(bike[0]), model));//adds a corresponding Bike
            }
            foreach(var bike in nonPlannifiedBikes)
            {
                minutes += bike.TotalTime;
            }
            foreach(var elem in bikesToOrder)
            {
                minutes += elem.getTotalTime();
            }
            hours = minutes/ 60;
            days = hours / 24; //because 3 builders working 8 hours a day

            if (planningList.Count != 0)
            {
                string lastWeek = planningList.Last().weekName;
                string b = string.Empty;
                int lastWeekNumber=0;

                CultureInfo myCI = new CultureInfo("en-US");
                Calendar myCal = myCI.Calendar;
                CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
                DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
                var currentWeek = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);//gets the current week
                for (int i = 0; i < lastWeek.Length; i++)
                {
                    if (Char.IsDigit(lastWeek[i]))
                        b += lastWeek[i];
                }
                if (b.Length > 0)
                {
                    lastWeekNumber = int.Parse(b);
                }
                weeks = lastWeekNumber - currentWeek ;
                Console.WriteLine("CURRENT WEEK : " + currentWeek);
                Console.WriteLine("LAST PLANNED WEEK : " + lastWeekNumber + "LAST WEEK NAME : "+lastWeek);
                Console.WriteLine("DIFFERENCE : " + weeks);


            }
            weeks += (int)(Math.Ceiling(days) / 5) +1;
            Console.WriteLine("MINUTES " + minutes+ " HOURS " + hours + " DAYS " + days);
            Console.WriteLine("DIFFERENCE + NEW BIKES TO BUILD: " + weeks);

            return weeks;
        }

        internal List<Bike> getStockBikesID()
        {
            var stockBikeID = DataBase.GetFromDBWhere("Order_Bikes", new List<string>() { "Id_Order" }, "Customer_Name='Stock'");
            List<string> bikes = stockBikeID.SelectMany(x => x).ToList();
            Console.WriteLine(string.Join("\t", bikes));

            var stockBike = DataBase.GetFromDBWhere("Order_Details", new List<string>() { "Bike_Type", "Bike_Size", "Bike_Color","Id_Order"}, "Bike_Status = 'Closed'");

            //List<string> stock = new List<string>();
            List<Bike> test = new List<Bike>();
            foreach(var row in stockBike)
            {
                if (bikes.Any(x => x == row[3]))
                {
                    test.Add(new Bike(0, new BikeModel(row[0],row[2],Int32.Parse(row[1]))));
                    //stock.Add(string.Join("\t", row));                   
                }
            }
            //Console.WriteLine(string.Join("\n", test[0].Size));
            return test;
        }

        internal void createInvoice(string client,List<string> column,List<List<String>> Data)
        {
            string date = DateTime.Now.ToString();
            date = date.Replace('/', '_');
            date = date.Replace(' ', '_');
            date = date.Replace(':', '_');
            string path = Environment.CurrentDirectory;
            PdfWriter writer = new PdfWriter(@"../../facture/" + client + "_" + date + ".pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Header
            Paragraph header = new Paragraph("Bovelo")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);

            // New line
            Paragraph newline = new Paragraph(new Text("\n"));
            document.Add(newline);
            document.Add(header);
            // Add sub-header
            Paragraph subheader = new Paragraph(client)
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(15);
            document.Add(subheader);
            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);
            // Add paragraph1
            Paragraph paragraph1 = new Paragraph("Récapitulatif de commande " + date.Substring(0,8));
            paragraph1.SetTextAlignment(TextAlignment.CENTER);
            document.Add(paragraph1);
            document.Add(ls);
            document.Add(newline);

            // Table
            Table table = new Table(column.Count(), false);

            foreach (var elem in column)
            {
                Cell cell = new Cell(1, 1)
                    .SetBackgroundColor(ColorConstants.GRAY)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetWidth(100)
                    .Add(new Paragraph(elem));
                table.AddCell(cell);
            }
            int price = 0;
            foreach (var item in Data)
            {
                foreach (var value in item)
                {
                    Cell cell = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .Add(new Paragraph(value));
                    table.AddCell(cell);
                    if (value==item[item.Count()-1])
                    {
                        price += Int32.Parse(value);
                    }
                }
            }
            document.Add(table);
            //Total
            document.Add(newline);
            document.Add(ls);
            document.Add(newline);
            Paragraph paragraph2 = new Paragraph("Total : " + price);
            paragraph2.SetTextAlignment(TextAlignment.RIGHT);
            document.Add(paragraph2);
            // Page numbers
            int n = pdf.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format(i + "/" + n)),
                   559, 806, i, TextAlignment.RIGHT,
                   VerticalAlignment.TOP, 0);
            }

            // Close document
            document.Close();
        }       
    } // end App Class
} // end namespace Bovelo