using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Bovelo
{
    class OrderBike
    {
        public string BikeType;
        public string BikeSize;
        public string BikeColor;
        public int Quantity;
        public string ShippingTime;
        public List<string> CartLine ;
        public List<List<string>> Cart;
        public User _currentUser = new User(" "," ");

        public OrderBike(User incomingUser)
        {
            _currentUser = incomingUser;

        }
        public void addOrderBike()
        {
            Cart c = new Cart(ref _currentUser);
            int i = 0;
            foreach (List<string> element in Cart)
            {
                this.BikeType = element[0];
                this.BikeSize = element[1];
                this.BikeColor = element[2];
                this.Quantity = int.Parse(element[3]);
                this.ShippingTime = element[4];
                Console.WriteLine(element);

                string connStr = "server=193.191.240.67;user=testuser;database=Bovelo;port=63304;password=user_password";
                MySqlConnection conn = new MySqlConnection(connStr);
                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "INSERT INTO Order_Bikes (Bike_type,Bike_Size,Bike_Color,Quantity,Shipping_Time) VALUES('" + this.BikeType + "','" + this.BikeSize + "', '" + this.BikeColor + "'," + this.Quantity + ",  '" + this.ShippingTime + "'); ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    //while (rdr.Read())
                    //{
                    //}
                    rdr.Close();
                }
                catch (Exception ex)
                {

                }
                conn.Close();
            }

        }
    }
}
