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
        public List<string> maListe = new List<string>();
        public User _currentUser = new User(" "," ");

        public OrderBike(User incomingUser)
        {
            _currentUser = incomingUser;

        }
        public void addOrderBike()
        {
            //int i;
            Cart c = new Cart(ref _currentUser);
            this.BikeType = this.maListe[0];
            this.BikeSize = this.maListe[1];
            this.BikeColor = this.maListe[2];
            this.Quantity = int.Parse(this.maListe[3]);
            this.ShippingTime = this.maListe[4];

            //BikeType = c.maListe[0];

            string connStr = "server=193.191.240.67;user=testuser;database=Bovelo;port=63304;password=user_password";
            MySqlConnection conn = new MySqlConnection(connStr);
            //conn.ConnectionString = "server=193.191.240.67;user=testuser;database=Bovelo;port=63304;password=user_password";
/*            try
            {
                Console.WriteLine("Connecting to MySQL...");

                conn.Open();
                string sql = "INSERT INTO Order_Bikes (Bike_type,Bike_Size,Bike_Color,Quantity,Shipping_Time) VALUES('"+this.BikeType+ "','" + this.BikeSize + "', '" + this.BikeColor + "'," + this.Quantity + ",  '" + this.ShippingTime + "'); ";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //cmd.Connection = conn;
                //cmd.Parameters.Add(new MySqlParameter("@BikeType", BikeType));
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
            */
        }
    }
}
