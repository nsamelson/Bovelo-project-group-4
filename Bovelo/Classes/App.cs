using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Bovelo
{
    public class App 
    {
        internal List<User> userList;
        internal List<Bike> bikeModel;
        internal List<OrderBike> orderBikeList; //takes all the orders from the DB
        

        public App()//MOVE ALL CONNECTIONS TO DB INTO THIS CLASS 
        {
            this.userList = getUserList();
            this.bikeModel = getBikeModelList();
            this.orderBikeList = getOrderBikeList();
        }
        
        internal List<List<string>> getFromDB(string DBTable) //is used to get anything from a database
        {
            var listFromDB = new List<List<string>>();

            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = "SELECT * FROM "+DBTable+";";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var col = new List<string>();
                for (int j = 0; j<rdr.FieldCount; j++)
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
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine("Element added to DB");
            cmd.Dispose();
            conn.Close();
        }

        internal void setNewOrderBike(List<List<string>> newOrder)
        {
            //copy method from OrderBike class
            //Will send 2 things to Database : Order details and Order client
            int clientId = 0;

            foreach (var elem in newOrder)
            {
                //need to change the table
                string query = "INSERT INTO Order_Bikes (Bike_type,Bike_Size,Bike_Color,Quantity,Shipping_Time,Order_Price,id_User) VALUES('" + elem[0] + "','" + elem[2] + "', '" + elem[1] + "'," + elem[3] + ",  '" + elem[0] + "', '" + elem[4] + "', '" + clientId + "'); ";
                sendToDB(query);
            }

        }
        internal void setNewUser(User user) 
        {
            string query = "INSERT INTO Users (Login, Password, Role) VALUES ('" + user.login + "','NULL','" + user.userType.FirstOrDefault(x => x.Value == true).Key + "')";
            sendToDB(query);
            userList = getUserList(); //if latency problems, comment this line and uncomment "userList.Add(user)"
            //userList.Add(user);
        }
        public void SetNewBikeModel(string type, int price, string time)
        {

            string query = "INSERT INTO Bikes (Bike_Type,Price,Bike_total_time) VALUES ('" + type + "', " + price + ", '" + time + "')";
            sendToDB(query);
        }

        internal List<OrderBike> getOrderBikeList()
        {
            List<OrderBike> orderBikeList = new List<OrderBike>();
            List<List<string>> orderList = getFromDB("Order_Bikes");
            foreach (var row in orderList)
            {
                orderBikeList.Add(new OrderBike(row[1]));//row[1] is the column where the name of the client is put
            }
            return orderBikeList;
        }
        internal List<User> getUserList() //GET USERS REGISTERED INSIDE DATABASE 
        {
            var userFromDB = new List<User>();
            List<List<string>> orderList = getFromDB("Users");
            foreach (var row in orderList)
            {
                string login = row[1];
                string userType = row[3];
                switch (userType)
                {
                    case "Representative":
                        userFromDB.Add(new User(login,true,false,false));
                        break;
                    case "ProductionManager":
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
        internal List<Bike> getBikeModelList()//MAYBE MOVE IT INSIDE BIKE CLASS
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

    }
}