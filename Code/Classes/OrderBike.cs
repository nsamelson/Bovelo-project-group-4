using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;

namespace Bovelo
{
    class OrderBike
    {
        //public string BikeType;
        //public string BikeSize;
        //public string BikeColor;
        //public string ShippingTime;

        public User _currentUser;
        

        public OrderBike(User incomingUser)
        {
            _currentUser = incomingUser;

        }
        public void addOrderBike()
        {
            Cart c = new Cart(ref _currentUser);
            string connStr = "server=193.191.240.67;user=testuser;database=Bovelo;port=63304;password=user_password";          
            MySqlConnection conn = new MySqlConnection(connStr);           
            try            
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                foreach(Item elem in _currentUser.cart)
                {
                    string sql = "INSERT INTO Order_Bikes (Bike_type,Bike_Size,Bike_Color,Quantity,Shipping_Time,Order_Price,id_User) VALUES('" + elem.bike.Type + "','" + elem.bike.Size + "', '" + elem.bike.Color + "'," + elem.quantity + ",  '" + elem.bike.TotalTime + "', '"+ elem.bike.Price + "', '" + _currentUser.idUser+ "'); ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Close();
                }          
                //while (rdr.Read())               
                //{               
                //}                               
            }           
            catch (Exception ex)           
            {
                
            }         
            conn.Close();
        }
        /*public List<List<string>> getOrderList()
        {
            List<List<string>> orderList = new List<List<string>>();
            List<string> bikeInfo = new List<string>();

            var userFromDB = new List<User>();
            //userFromDB.Add(new User("user1", "user1"));

            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = "SELECT * FROM Order_Bikes WHERE id_User ="+ _currentUser.idUser+"; ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string idOrder = Convert.ToString(rdr[0]);
                string bikeType = Convert.ToString(rdr[1]);
                string bikeSize = Convert.ToString(rdr[2]);
                string bikeColor = Convert.ToString(rdr[3]);
                string quantity = Convert.ToString(rdr[4]);
                string shipping_time = Convert.ToString(rdr[5]);
                string price = Convert.ToString(rdr[6]);
                Console.WriteLine(rdr[0].ToString());
                bikeInfo.Add(bikeType+ bikeSize+ bikeColor+ quantity+ shipping_time+ price);
                orderList.Add(bikeInfo);
            }
            rdr.Close();
            conn.Close();


            return orderList;
        }*/
    }
}
