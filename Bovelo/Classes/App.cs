using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;

namespace Bovelo
{
    public class App //Super class, it takes everything from the database and will send anything to it
    {
        internal List<User> userList; //All users from DB( Representative, Assembler, ProductionManager)
        internal List<BikePart> bikePartList;
        internal List<BikeModel> bikeModels; //All bike types (Adventure, city and explorer)
        internal List<OrderBike> orderBikeList; //takes all the orders from the DB 
        internal List<Planning> planningList; //takes all the plannings from the DB

        public App(bool getAll = false)
        {
            if (getAll)
            {

                this.bikePartList = getBikePartList();
                this.bikeModels = getBikeModelList();
                this.userList = getUserList();
                this.orderBikeList = getOrderBikeList();
                this.planningList = getPlanningList();
            }
        }

        //methods connecting to the DB
        internal List<List<string>> getFromDB(string DBTable) //is used to get anything from a database
        {
            var listFromDB = new List<List<string>>();

            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            //Console.WriteLine("Connecting to MySQL at table " + DBTable + "...");
            conn.Open();
            string sql = "SELECT * FROM " + DBTable + ";";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var col = new List<string>();
                for (int j = 0; j < rdr.FieldCount; j++)
                {
                    col.Add(rdr[j].ToString());
                }
                listFromDB.Add(col);
            }
            rdr.Close();
            conn.Close();
            return listFromDB;
        }
        public List<List<string>> getFromDBSelect(string DBTable, List<string> argumentList)
        {
            var listFromDB = new List<List<string>>();

            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            //Console.WriteLine("Connecting to MySQL at table " + DBTable + "...");
            conn.Open();
            string sql = "SELECT ";
            for (int i = 0; i < argumentList.Count; i++)
            {
                if (i != argumentList.Count - 1)
                {
                    sql += argumentList[i] + ",";
                }
                else
                {
                    sql += argumentList[i];
                }
            }

            sql += " FROM " + DBTable + ";";
            //Console.WriteLine(sql);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var col = new List<string>();
                for (int j = 0; j < rdr.FieldCount; j++)
                {
                    col.Add(rdr[j].ToString());
                }
                listFromDB.Add(col);
            }
            rdr.Close();
            conn.Close();
            return listFromDB;
        }
        public List<List<string>> getFromDBWhere(string DBTable, List<string> argumentList, string whereClause)
        {
            var listFromDB = new List<List<string>>();
            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            //Console.WriteLine("Connecting to MySQL at table " + DBTable + "...");
            conn.Open();
            string sql = "SELECT ";
            for (int i = 0; i < argumentList.Count; i++)
            {
                if (i != argumentList.Count - 1)
                {
                    sql += argumentList[i] + ",";
                }
                else
                {
                    sql += argumentList[i];
                }
            }

            sql += " FROM " + DBTable + " WHERE " + whereClause + ";";
            //Console.WriteLine(sql);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var col = new List<string>();
                for (int j = 0; j < rdr.FieldCount; j++)
                {
                    col.Add(rdr[j].ToString());
                }
                listFromDB.Add(col);
            }
            rdr.Close();
            conn.Close();
            return listFromDB;
        }
        internal List<List<string>> getFromDbInnerJoin(string week)
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
        }
        internal List<List<string>> getPanifiedOrderDetails(string sql)
        {
            var OrderDetails = new List<List<string>>();
            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var col = new List<string>();
                for (int j = 0; j < rdr.FieldCount; j++)
                {
                    col.Add(rdr[j].ToString());
                }
                OrderDetails.Add(col);
            }
            rdr.Close();
            conn.Close();
            return OrderDetails;
        }
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

        internal void setNewOrderBike(List<List<string>> newOrder, string clientName, int totPrice) //is used to pass a new order
        {
            orderBikeList = getOrderBikeList();//updates the list
            //first request
            int orderId;
            string queryOB = "INSERT INTO Order_Bikes(Customer_Name,Total_Price,Order_Date,Shipping_Time) VALUES('" + clientName + "', '" + totPrice + "' ,'" + DateTime.Now.ToString() + "','" + DateTime.Today.AddDays(7).ToString() + "');";
            sendToDB(queryOB);

            //second request
            if (orderBikeList.Count == 0)
            {
                orderId = 1;
            }
            else
            {
                orderId = orderBikeList.Last().orderId +1;
            }
            foreach (var element in newOrder)
            {
                //Console.WriteLine("type in APP : " + element[0] + " size in APP: " + element[1] + " color: in APP " + element[2] + " quantity in APP : " + element[3] + " price in APP : " + element[4]);
                string type = element[0];
                int size = Int32.Parse(element[1]);
                string color = element[2];
                int quantity = Int32.Parse(element[3]);
                int price = Int32.Parse(element[4]) / quantity;
                for (int q = 0; q < quantity; q++)
                {
                    string queryOD = "INSERT INTO Order_Details (Bike_Type,Bike_Size,Bike_Color,Price,Bike_Status,Id_Order) VALUES('" + type + "', '" + size + "','" + color + "' , '" + price + "', 'New' , '" + orderId + "'); ";
                    sendToDB(queryOD);
                    //Console.WriteLine("New order detail has been added to DB");
                }
            }
            orderBikeList = getOrderBikeList();//updates the list 
        }
        internal void setNewUser(User user) //is used to add a new user (for ex: a new Assembler joins the team)
        {
            string query = "INSERT INTO Users (Login, Password, Role) VALUES ('" + user.login + "','NULL','" + user.userType.FirstOrDefault(x => x.Value == true).Key + "')";
            sendToDB(query);
            userList = getUserList(); //At the end of set, put a get to update App class
            //userList.Add(user); //if latency problems, uncomment this line and comment "userList = getUserList();"
        }
        /*internal void setNewBikeModel(string type, int price, string time)//is used to add a new model (for ex: Electric)
        {

            string query = "INSERT INTO Bikes (Bike_Type,Price,Bike_total_time) VALUES ('" + type + "', " + price + ", '" + time + "')";
            sendToDB(query);
            //bikeModel = getBikeModelList();//At the end of set, put a get to update App class
        }*/
        internal void setNewBikeModel(string type, int size, string color)//is used to add a new model (for ex: Electric)
        {
            string query = "INSERT INTO Bike_Model (Bike_Type,Color,Size) VALUES ('" + type + "', " + color + ", '" + size + "')";
            sendToDB(query);
            bikeModels = getBikeModelList();//At the end of set, put a get to update App class
        }

        internal void setNewPlanning(List<List<string>> planningCartList, string week)//NEED TO SET THE TABLES
        {
            //Planning newPlanning = new Planning(planningCartList, week);
            /*string queryP = "INSERT INTO Schedule(Week_Name) VALUES('" + week + "');";
            sendToDB(queryP);*/

            int planningId;
            int orderDetailsId = 0; //NEED TO LINK ORDER DETAILS WITH THE CORRECT ID
            if (getPlanningList().Count == 0)
            {
                planningId = 1;
            }
            else
            {
                planningId = getPlanningList().Last().planningId ;
            }

            foreach (var bikesToBuild in planningCartList)
            {
                Console.WriteLine("bikes in planning");
                Console.WriteLine(bikesToBuild[0]+ bikesToBuild[1]+bikesToBuild[2]+ bikesToBuild[3]);
                orderDetailsId = Int32.Parse(bikesToBuild[0]);
                string type = bikesToBuild[1];
                string size = bikesToBuild[2];
                //string color = bikesToBuild[3];
                string queryPD = "INSERT INTO Detailed_Schedules (Week_Name,Id_Order_Details) VALUES('" + week + "' , '" + orderDetailsId + "'); ";
                sendToDB(queryPD);
            }

            planningList = getPlanningList();//At the end of set, put a get to update App class
        }
        internal void setNewBikePart(string name, int size = 0,string color="null")//is used to create a new bikePart : takes name, size(0 for default,26,28) and color (null for default,black,red,blue)
        {
            var rand = new Random();
            var bikePartLocation = new List<string>();
            string location = RandomString(3);
            string provider = RandomString(8);
            int timeToBuild = rand.Next(1, 15);
            int price = rand.Next(1, 101);
            string bikePartName = name;
            string query;

            string RandomString(int length)//create a random string of letters and numbers
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[rand.Next(s.Length)]).ToArray());
            }
            foreach (var partlocation in getBikePartList())//takes every used location
            {
                bikePartLocation.Add(partlocation.location);
            }
            while (bikePartLocation.FirstOrDefault(x => x.Contains(location)) != null)//verifies if the location is already in use
            {
                location = RandomString(3);
            }

            if(color == "null" && size != 0){bikePartName += "_" + size.ToString();}
            else if (color != "null" && size == 0) { bikePartName += "_" + color; }
            else if (color != "null" && size != 0) { bikePartName += "_" + color + "_" + size.ToString(); }

            query = "INSERT INTO Bike_Parts (Bike_Parts_Name,Quantity,Location,Price,Provider,Time_To_Build) VALUES('" + bikePartName + "' , '" + 0 + "' , '" + location + "' , '" + price + "' , '" + provider + "' , '" + timeToBuild + "'); ";
            sendToDB(query);            
        }

        //GET from the DB methods

        internal List<BikePart> getBikePartList()
        {
            List<BikePart> bikeParts = new List<BikePart>();
            var BikePartsFromDB = getFromDB("Bike_Parts");

            foreach (var row in BikePartsFromDB)
            {
                bikeParts.Add(new BikePart(Int32.Parse(row[0]),row[1],row[3], Int32.Parse(row[4]),row[5], Int32.Parse(row[6])));
            }

            return bikeParts;
        }
        internal void updateSatus(int id, string status, string user)
        {
            
            string query = "UPDATE Order_Details SET Bike_Status = '" + status + "', Modified_by = '" + user + "'  WHERE Id_Order_Details = '" + id + "' ;";
            sendToDB(query);
        }
        internal List<Planning> getPlanningList() //gets all plannings
        {
            List<Planning> plannings = new List<Planning>();
            /*List<List<string>> planningDB = getFromDB("Schedule");
            var planningDetailsDB = getFromDB("Detailed_Schedules");

            foreach (var row in planningDB)
            {
                List<List<string>> details = new List<List<string>>(planningDetailsDB.FindAll(x => x[5] == row[0]));//takes each lists with the same Id
                foreach (var elem in details)
                {
                    string test = "";
                    for (int i = 0; i < elem.Count; i++)
                    {
                        test += elem[i] + " ";
                    }
                    //Console.WriteLine(test);
                }
                
                Planning newPlanning = new Planning(details, row[1], Convert.ToInt16(row[0]));
                plannings.Add(newPlanning);//row[1] is the column where the name of the client is put

            }*/

            return plannings;

        }
        internal List<OrderBike> getOrderBikeList() //is used to get all Bike Orders NEED TO TRY
        {
            List<OrderBike> orderBikeList = new List<OrderBike>();
            List<List<string>> orderList = getFromDB("Order_Bikes");
            var orderDetailList = getFromDB("Order_Details");

            foreach (var row in orderList)
            {
                List<List<string>> details = new List<List<string>>(orderDetailList.FindAll(x => x[6] == row[0]));//takes each lists with the same order_Id
                OrderBike newOrder = new OrderBike(row[1], details, Int32.Parse(row[0]));
                newOrder.orderDate = Convert.ToDateTime(row[3]);
                newOrder.shippingDate = Convert.ToDateTime(row[4]);
                orderBikeList.Add(newOrder);//row[1] is the column where the name of the client is put

                //OrderBike order = new OrderBike(row[1], details,Int32.Parse(row[0])); 
                //Console.WriteLine(" Order : " + order.getBikeList());
                
            }

            return orderBikeList;
        }
        internal List<List<string>> getOrderDetails()
        {
            var orderDetailList = getFromDB("Order_Details");
            return orderDetailList;
        }
        internal List<List<string>> getPlanifiedBikes()
        {
            string sql = "SELECT * FROM Order_Details inner join  Bovelo.Detailed_Schedules on Detailed_Schedules.Id_Order_Details = Order_Details.Id_Order_Details where Order_Details.Id_Order_Details In (select Id_Order_Details from Detailed_Schedules);";
            var Planified = getPanifiedOrderDetails(sql);
            return Planified;
        }
        internal List<List<string>> getNonPlanifiedBikes()
        {
            string sql = "Select * from Order_Details where Id_Order_Details Not In (select Id_Order_Details from Detailed_Schedules);";
            var nonPlanified = getPanifiedOrderDetails(sql);
            return nonPlanified;
        }
        internal void deletePlanifiedBike(int idOrderDetail, string week)
        {
            string deleteQuery = "delete from Detailed_Schedules where Week_Name = '" + week + "' and Id_Order_Details='" + idOrderDetail + "';";
            sendToDB(deleteQuery);
        }


        internal List<User> getUserList() //is used to get all users 
        {
            var userFromDB = new List<User>();
            //createBikeModel();
            List<List<string>> orderList = getFromDB("Users");
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
            List<List<string>> modelList = getFromDB("Bike_Model");//bikemodels from db
            var allPartsId = getFromDB("Parts");//all parts from db
            
            foreach (var row in modelList)
            {
                List<int> bikePartsIds = new List<int>();
                int id = Int32.Parse(row[0]);
                string color = row[1];
                int size = Int32.Parse(row[2]);
                string type = row[3];
                var newBikeModel = new BikeModel(type, color, size) { idBikeModel = id };

                foreach (var part in allPartsId)
                {
                    if (Int32.Parse(part[1]) == id)
                    {
                        bikePartsIds.Add(Int32.Parse(part[0]));
                    }
                }
                //newBikeModel.bikeParts = bikePartsIds.Select(x=> bikePartList.First(part=>part.part_Id ==x)).ToList(); //FOR DUPLICATES (not working yet)
                newBikeModel.bikeParts = bikePartList.FindAll(part => bikePartsIds.Contains(part.part_Id));
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
        internal List<BikePart> getBikePart(List<string> TypeSizeColor)
        {
            List<string> query = new List<string>();
            query.Add("*");
            List<List<string>> bikePart = new List<List<string>>();
            bikePart = getFromDBWhere("Bike_Parts", query, "Id_Bike_Parts IN ( SELECT Id_Bike_Parts FROM Parts WHERE Bikes_Id IN(SELECT idBike_Model FROM Bike_Model WHERE Color = '"+TypeSizeColor[2]+"' AND Type_Model = '"+ TypeSizeColor[0]+ "' AND Size = '"+TypeSizeColor[1]+"'))");
            List<BikePart> bikePartList = new List<BikePart>();
            foreach (var line in bikePart)
            {
                bikePartList.Add(new BikePart(Int32.Parse(line[0]), line[1], line[3], Int32.Parse(line[4]),line[5],Int32.Parse(line[6])));
            }
            return bikePartList;
        }// end getbikepart
    } // end App Class
} // end namespace Bovelo