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
        internal List<Bike> bikeModel; //All bike types (Adventure, city and explorer)
        internal List<OrderBike> orderBikeList; //takes all the orders from the DB 
        internal List<Planning> planningList; //takes all the plannings from the DB




        public App()
        {
            this.userList = getUserList();
            this.bikeModel = getBikeModelList();
            this.orderBikeList = getOrderBikeList();
            this.planningList = getPlanningList();
        }

        //methods connecting to the DB
        internal List<List<string>> getFromDB(string DBTable) //is used to get anything from a database
        {
            var listFromDB = new List<List<string>>();

            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL at table " + DBTable + "...");
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
        public List<List<string>> getFromDBWhere(string DBTable, List<string> argumentList)
        {
            var listFromDB = new List<List<string>>();

            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL at table " + DBTable + "...");
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
        internal void sendToDB(string query) //is used to send anything to the database
        {
            string connStr = "server=193.191.240.67;user=USER3;database=Bovelo;port=63304;password=USER3";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL to send new element...");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            cmd.Dispose();
            conn.Close();
        }

        //SET To the DB methods
        internal void setNewOrderBike(List<List<string>> newOrder, string clientName, int totPrice) //is used to pass a new order  HAVE TO CHANGE
        {
            //It has to send 2 things to Database : Order details and Order client

            // TEST 
            Bike bike_test = new Bike("City", "Red", 26, 800);
            List<string> line = getBikePart(bike_test);
            getBikePartsList(line);

            
            int orderId;
            if (orderBikeList.Count == 0)
            {
                orderId = 1;
            }
            else
            {
                orderId = orderBikeList.Last().orderId + 1;
            }

            List<List<string>> Order = new List<List<string>>();// need to change this later
            OrderBike newOrderBike = new OrderBike(clientName, Order, orderId);
            /*Console.WriteLine(Order.Count);
            foreach(var x in Order)
            {
                Console.WriteLine(x);
                foreach ( var y in x)
                {
                    Console.WriteLine("voil� ton  : " + y);
                }
            }*/
            Console.WriteLine("TOTAL PRICE OF ORDER IS :" + newOrderBike.getTotalPrice());
            string queryOB = "INSERT INTO Order_Bikes(Customer_Name,Total_Price,Order_Date,Shipping_Time) VALUES('" + newOrderBike.clientName + "', '" + totPrice + "' ,'" + DateTime.Now.ToString() + "','" + DateTime.Today.AddDays(7).ToString() + "');";
            sendToDB(queryOB);
            Console.WriteLine("New Order has been added to DB");

            foreach (var element in newOrder)
            {

                Console.WriteLine("type in APP : " + element[0] + " size in APP: " + element[1] + " color: in APP " + element[2] + " quantity in APP : " + element[3] + " price in APP : " + element[4]);
                string type = element[0];
                int size = Int32.Parse(element[1]);
                string color = element[2];
                int quantity = Int32.Parse(element[3]);
                int price = Int32.Parse(element[4]) / quantity;
                for (int q = 0; q < quantity; q++)
                {
                    string queryOD = "INSERT INTO Order_Details (Bike_Type,Bike_Size,Bike_Color,Price,Bike_Status,Customer_Name,Id_Order) VALUES('" + type + "', '" + size + "','" + color + "' , '" + price + "', 'New' , '" + newOrderBike.clientName + "','" + orderId + "'); ";
                    sendToDB(queryOD);
                    Console.WriteLine("New order d�tail has been added to DB");
                }

            }
            orderBikeList = getOrderBikeList(); //At the end of set, put a get to update App class
        }
        internal void setNewUser(User user) //is used to add a new user (for ex: a new Assembler joins the team)
        {
            string query = "INSERT INTO Users (Login, Password, Role) VALUES ('" + user.login + "','NULL','" + user.userType.FirstOrDefault(x => x.Value == true).Key + "')";
            sendToDB(query);
            userList = getUserList(); //At the end of set, put a get to update App class
            //userList.Add(user); //if latency problems, uncomment this line and comment "userList = getUserList();"
        }
        internal void setNewBikeModel(string type, int price, string time)//is used to add a new model (for ex: Electric)
        {

            string query = "INSERT INTO Bikes (Bike_Type,Price,Bike_total_time) VALUES ('" + type + "', " + price + ", '" + time + "')";
            sendToDB(query);
            bikeModel = getBikeModelList();//At the end of set, put a get to update App class
        }
        internal void setNewPlanning(List<List<string>> planningCartList, string week)//NEED TO SET THE TABLES
        {
            Planning newPlanning = new Planning(planningCartList, week);
            string queryP = "INSERT INTO Planning(Week_Name,Working_Hours) VALUES('" + newPlanning.weekId + "','" + newPlanning.workingHours + "');";
            //sendToDB(queryP);
            foreach (var bikesToBuild in newPlanning.planningDetails)
            {
                int Planning_Id = 0;
                string type = bikesToBuild[1];
                int size = Int32.Parse(bikesToBuild[2]);
                string color = bikesToBuild[3];
                string queryPD = "INSERT INTO Order_Details (Bike_Type,Bike_Size,Bike_Color,Id_Planning) VALUES('" + type + "', '" + size + "','" + color + "',  '" + Planning_Id + "'); ";
                //sendToDB(queryPD);
            }

            planningList = getPlanningList();//At the end of set, put a get to update App class
        }
        //GET from the DB methods
        internal List<Planning> getPlanningList()//COPY  getOrderBikeList() METHOD
        {
            List<Planning> plannings = new List<Planning>();
            /* List<List<string>> planningDB = getFromDB("");
             var planningDetails = getFromDB("");*/

            return plannings;

        }
        internal List<OrderBike> getOrderBikeList() //is used to get all Bike Orders NEED TO TRY
        {
            List<OrderBike> orderBikeList = new List<OrderBike>();
            List<List<string>> orderList = getFromDB("Order_Bikes");
            var orderDetailList = getFromDB("Order_Details");

            foreach (var row in orderList)
            {
                List<List<string>> details = new List<List<string>>(orderDetailList.FindAll(x => x[7] == row[0]));//takes each lists with the same order_Id
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

       
        internal List<User> getUserList() //is used to get all users 
        {
            var userFromDB = new List<User>();
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
                        Console.WriteLine("user : " + login + ", is not registered correctly in the DataBase");
                        break;
                }

            }
            return userFromDB;
        }
        internal List<Bike> getBikeModelList() //is used to get all bike models
        {
            List<Bike> bikeList = new List<Bike>();
            List<List<string>> orderList = getFromDB("Bikes");
            foreach (var row in orderList)
            {
                string Types = row[1];
                int Prices = Convert.ToInt32(row[2]);
                bikeList.Add(new Bike(Types, "black", 26, Prices)); //will change because the bikeParts will influence the price and also maybe turn back to 2 constructors
            }
            return bikeList;
        }
        internal List<string> getBikePartInvoice(List<OrderBike> orderBikeList)
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
/*          foreach (var elem in BikeType.Values)             // what's in the dict
            {
                string toprint = " ";
                foreach (var info in elem)
                {
                    toprint += info + " | ";
                }
                Console.WriteLine(toprint);
            }

            foreach(var elem in bikepart)                    // what I am returning
            {
                Console.WriteLine(elem);
            }*/
            return bikePart;
        }

        internal List<string> getBikePart(Bike bike)
        {
            string path = @"../../Classes/list_part.txt";
            IEnumerable<string> line = File.ReadLines(path);
            var BikeType = new Dictionary<int, List<string>>(); //  to stock data
            List<string> identity = new List<string>();        //  to add data to dict
            int e = 0;                                         //  key index 
            List<string> bikePart = new List<string>();        //  return value
            foreach (var elem in line)
            {
                int word = 0;
                string currentWord = "";
                foreach (var character in elem)                             // reading word
                {
                    
                    if (character == ';')                   // reading char
                    {
                        identity.Add(currentWord);                 // add word to list string
                        currentWord = "";                          // pass ";" char
                        word++;                                    // next word                                       
                        continue;
                    }
                    currentWord += character;                // concatenate char to make word
                    
                    
                }
                BikeType.Add(e, identity);                     // add to dict list of word
                identity = new List<string>();                 // reset list of word
                e++;
            }
            foreach (var elem in BikeType.Values)
            {
                if (bike.Type == elem[0])              // finding parts with goods size,type,color
                    if (bike.Size.ToString() == elem[1])
                        if (bike.Color == elem[2])
                        {
                            for (int i = 3; i<elem.Count();i++)
                            {
                                bikePart.Add(elem[i]);
                            }
                        }
                }
/*            foreach (var elem in BikeType.Values)             // what's in the dict
            {
                string toprint = " ";
                foreach (var info in elem)
                {
                    toprint += info + " | ";
                }
                Console.WriteLine(toprint);
            }
*/            
            foreach(var elem in bikePart)                    // what I am returning
            {
                Console.WriteLine(elem);
            }
            return bikePart;
        }// end getbikepart

        internal void getBikePartsList(List<string> bikePart)
        {
            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL at table Bike_Parts...");
            conn.Open();
            string sql_string = "SELECT * FROM Bike_Parts WHERE Bike_Parts_Name=";
            string sql = " ";
            List<BikePart> bikePartList = new List<BikePart>();
            int i = 0;
            foreach (var part in bikePart)
            {
                sql = sql_string + "'" + part + "'" + ";";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string name = rdr[1].ToString();
                    int quantity = Convert.ToInt32(rdr[2]);
                    string location = rdr[3].ToString();
                    int price = Convert.ToInt32(rdr[4]);
                    string provider = rdr[5].ToString();
                    int time = Convert.ToInt32(rdr[6]);
                    bikePartList.Add(new BikePart(name, time, price, location));
                }
                sql = sql_string;
                rdr.Close();
                i++;
            }
            conn.Close();
            foreach (var elem in bikePartList)
            {
                Console.WriteLine("LISTE DE PIECES: " + elem.name);
            }
        }
    } // end App Class
} // end namespace Bovelo