using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Bovelo
{
    class Bike
    {
         public DateTime TotalTime = DateTime.Now;
         public int Price = 0;
         public bool  isBuilt = false;
         //private List<_BikeType> BikeTypesList = new List<_BikeType>();

         private string _BikeType = " ";
         private string _BikeColor = " ";
         private int _BikeSize = 0;  
         //private BikePart[] Parts = new BikePart[]{};
         
        public string Type{ get => _BikeType; set => _BikeType = value;}
        public string Color{ get => _BikeColor; set => _BikeColor = value;}
        public int Size{ get => _BikeSize; set => _BikeSize = value;}

        public Bike(string Type,string Color,int Size)
        {
            _BikeType = Type;
            _BikeColor = Color;
            _BikeSize = Size;
        }

        public Bike(string Type, int Price)
        {
            this.Type = Type;
            this.Price = Price;
        }

        /*        public void addBikeType()
                {
                    BikeTypesList.Add(new _BikeType() City);
                    BikeTypesList.Add(new _BikeType() Explorer);
                    BikeTypesList.Add(new _BikeType() Adventrue);
                }
                public InsertBikeDB()
                {
                    //string connStr = "server=localhost;user=root;database=bovelo;port=3306;password=root"; 
                    string connStr = "server=193.191.240.67;user=USER1;database=bovelo;port=63304;password=USER1";
                    MySqlConnection conn = new MySqlConnection(connStr);
                    try
                    {
                        Console.WriteLine("Connecting to MySQL...");
                        conn.Open();
                        string sql = "SELECT * FROM Bike;";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            listBox1.Items.Add(rdr[0] + " -- " + rdr[1]);
                        }
                        rdr.Close();
                    }
                    catch (Exception ex)
                    {
                        label1.Text = ex.ToString();
                    }
                    conn.Close();
                    label1.Text = "Done";
                }*
        /*        public void addBikeType()
                {
                    BikeTypesList.Add(new _BikeType() City);
                    BikeTypesList.Add(new _BikeType() Explorer);
                    BikeTypesList.Add(new _BikeType() Adventrue);
                }
        */
        public void SeeBikeDB()
        {
            //string connStr = "server=localhost;user=root;database=bovelo;port=3306;password=root"; 
            string connStr = "server=193.191.240.67;user=USER1;database=Bovelo;port=63304;password=USER1";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = "SELECT * FROM Bikes;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            int bikes = 1;
            while (rdr.Read())
            {
                switch (bikes)
                {
                    case 1: //City bike
                        string Output = rdr.GetValue(1) + "-" + rdr.GetValue(2);
                        Console.WriteLine(Output);
                        Console.WriteLine("TYPE: " + rdr[1]);
                        Console.WriteLine("PRICE: " + rdr[2] + " €");
                        Console.WriteLine("TIME TO BUILD: " + rdr[3]);
                        break;

                    case 2: //Explorer bike

                        break;
                    case 3: //Adventure bike
                        Console.WriteLine(rdr[3]);
                        break;
                    default:
                        Console.WriteLine("Cuoldn't find bike");
                        break;
                }
            }

            rdr.Close();

            conn.Close();
        }

        public void InsertBiketoDB(string type, int price, string time)
        {
            string connStr = "server=193.191.240.67;user=USER1;database=Bovelo;port=63304;password=USER1";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string query = "INSERT INTO Bikes (Bike_Type,Price,Bike_total_time) VALUES ('" + type + "', " + price + ", '" + time + "')";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine("Bike added into DB");
            cmd.Dispose();
            conn.Close();
        }

    }

}




