using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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
                    string sql = "INSERT INTO Order_Bikes (Bike_type,Bike_Size,Bike_Color,Quantity,Shipping_Time,Order_Price,id_User) VALUES('" + elem.bike.Type + "','" + elem.bike.Size + "', '" + elem.bike.Color + "'," + elem.quantity + ",  '" + elem.bike.TotalTime + "', '"+ 1000 + "', '" + _currentUser.idUser+ "'); ";
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
    }
}
