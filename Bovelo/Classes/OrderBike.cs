using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;

namespace Bovelo
{
    class OrderBike
    {
        public List<List<string>> newOrderList; //to remove
        public int clientId;
        public int orderId;
        public string clientName;
        public int orderPrice;
        public DateTime orderDate;
        public DateTime shippingDate;
        public List<Item> orderDetail; // or a list of list of strings


        public OrderBike(string clientName)
        {
            this.clientName = clientName;
            //this.clientId = clientId;
        }
        public void addOrderBike(List<List<string>> newOrder)
        {
            newOrderList = newOrder;
            string connStr = "server=193.191.240.67;user=testuser;database=Bovelo;port=63304;password=user_password";          
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            foreach (var elem in newOrderList)
            {
                //need to change the table
                string sql = "INSERT INTO Order_Bikes (Bike_type,Bike_Size,Bike_Color,Quantity,Shipping_Time,Order_Price,id_User) VALUES('" + elem[0] + "','" + elem[2] + "', '" + elem[1] + "'," + elem[3] + ",  '" + elem[0] + "', '" + elem[4] + "', '" + clientId + "'); ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                rdr.Close();
            }                   
            conn.Close();
        }
        public List<List<string>> getOrderBike()
        {
            List<List<string>> orderList = new List<List<string>>();


            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = "SELECT * FROM Order_Bikes WHERE id_User =" + clientId + "; ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                List<string> bikeInfo = new List<string>();
                string idOrder = Convert.ToString(rdr[0]);
                string bikeType = Convert.ToString(rdr[1]);
                string bikeSize = Convert.ToString(rdr[2]);
                string bikeColor = Convert.ToString(rdr[3]);
                string quantity = Convert.ToString(rdr[4]);
                string shipping_time = Convert.ToString(rdr[5]);
                string price = Convert.ToString(rdr[6]);
                Console.WriteLine(rdr[0].ToString());
                bikeInfo.Add(bikeType);
                bikeInfo.Add(bikeSize);
                bikeInfo.Add(bikeColor);
                bikeInfo.Add(quantity);
                bikeInfo.Add(shipping_time);
                bikeInfo.Add(price);
                orderList.Add(bikeInfo);
            }
            rdr.Close();
            conn.Close();


            return orderList;
        }
    }
}
