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
            this.userList = getUserListFromDB();
            this.bikeModel = getBikeModelFromDB();
            this.orderBikeList = getOrderBikeFromDB();
        }
        internal void sendOrderBikeToDB() 
        {
            //copy method from OrderBike class
            //Will send 2 things to Database : Order details and Order client
        }
        internal List<OrderBike> getOrderBikeFromDB()
        {
            var orderList = new List<OrderBike>();
            //copy method from OrderBike class


            return orderList;
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

            }
            rdr.Close();
            conn.Close();
            return listFromDB;
        }
        internal void sendToDB(string DBTable) //is used to send anything to the database
        {

        }
        internal void addNewUser(User user,bool isRepresentative,bool isProductionManager,bool isAssmebler) 
        {
            user.userType["Representative"] = isRepresentative;
            user.userType["ProductionManager"] = isProductionManager;
            user.userType["Assmebler"] = isAssmebler;

            //userList.Add(user);
            sendUserToDB(user);
            userList = getUserListFromDB(); //if latency problems, comment this line and uncomment "userList.Add(user)"
        }
        internal List<User> getUserListFromDB() //GET USERS REGISTERED INSIDE DATABASE 
        {
            var userFromDB = new List<User>();

            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = "SELECT * FROM Users;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int idUser = Convert.ToInt32(rdr[0]);
                string login = Convert.ToString(rdr[1]);
                string userType = Convert.ToString(rdr[2]);
                if (userType == "Representative")
                {
                    userFromDB.Add(new User(login));
                    //addNewUser(new User(login), true, false, false); //do not uncomment this
                }
                else if (userType == "ProductionManager")
                {
                    User newUser = new User(login);
                    newUser.userType["Representative"] = false;
                    newUser.userType["ProductionManager"] = true;
                    userFromDB.Add(newUser);
                }
                else if (userType == "Assembler")
                {
                    User newUser = new User(login);
                    newUser.userType["Representative"] = false;
                    newUser.userType["Assembler"] = true;
                    userFromDB.Add(newUser);
                }
            }
            rdr.Close();
            conn.Close();
            return userFromDB;
        }
        internal void sendUserToDB(User user) //SEND NEW USER INSIDE DATABASE
        {
            string connStr = "server=193.191.240.67;user=USER3;database=Bovelo;port=63304;password=USER3";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string query = "";
            //query = "INSERT INTO Users (Login, Password, Role) VALUES ('" + user.login + "','"user.userType"')"; //To change

            if (user.userType["Representative"])
            {
                query = "INSERT INTO Users (Login, Password, Role) VALUES ('" + user.login + "', 'Representative')";
            }
            else if (user.userType["ProductionManager"])
            {
                query = "INSERT INTO Users (Login, Password, Role) VALUES ('" + user.login + "', 'ProductionManager')";
            }
            else if (user.userType["Assembler"])
            {
                query = "INSERT INTO Users (Login, Password, Role) VALUES ('" + user.login + "', 'Assembler')";
            }

            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine("User added to DB");
            cmd.Dispose();
            conn.Close();
        }
        internal List<Bike> getBikeModelFromDB()//MAYBE MOVE IT INSIDE BIKE CLASS
        {
            List<Bike> bikeList = new List<Bike>();
            int i = 0;
            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = "SELECT * FROM Bikes;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string Types = Convert.ToString(rdr[1]);
                int Prices = Convert.ToInt32(rdr[2]);

                Bike myBike = new Bike(Types,"black",26 ,Prices);
                bikeList.Add(myBike);

                Console.WriteLine(bikeList[i].Type + " " + bikeList[i].Price);
                i++;

            }
            rdr.Close();
            conn.Close();
            return bikeList;
        }

    }
}