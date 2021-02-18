using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Bovelo
{
    class Bike
    {
        /* public DateTime TotalTime = DateTime.Now;
         public int TotalPrice = 0;
         public bool  isBuilt = false;
         private List<_BikeType> BikeTypesList = new List<_BikeType>();

         private string _BikeType = " ";
         private string _BikeColor = " ";
         private int _BikeSize = 0;  


         //private BikePart[] Parts = new BikePart[]{};
         */
        /*public string Type{ get => _Type; set => _Type=value;}
        public string Color{ get => _Color; set => _Color=value;}
        public int Size{ get => _Size; set => _Size =value;}

        public Bike(string Type,string Color,int Size)
        {   
            _Type=Type;
            _Color=Color;
            _Size=Size;
        }
*/
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
                }*/






        public Bike(string Type, string Color, int Size)
        {
            _Type = Type;
            _Color = Color;
            _Size = Size;
        }

        /*        public void addBikeType()
                {
                    BikeTypesList.Add(new _BikeType() City);
                    BikeTypesList.Add(new _BikeType() Explorer);
                    BikeTypesList.Add(new _BikeType() Adventrue);

                }
        */
        public void InsertBikeDB()
        {
            //string connStr = "server=localhost;user=root;database=bovelo;port=3306;password=root"; 
            string connStr = "server=193.191.240.67;user=USER1;database=Bovelo;port=63304;password=USER1";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = "SELECT * FROM Bikes;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0]);

            }

            rdr.Close();

            conn.Close();

        }

    }



}
