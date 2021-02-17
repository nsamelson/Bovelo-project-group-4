using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; 


namespace Bovelo
{
    class Program
    {
        [STAThread]
       static void Main()
        {

            Bike first_bike=new Bike("city","RED",26);

            string connStr = "server=193.191.240.67;user=USER1;database=bovelo;port=63304;password=USER1"; 

            MySqlConnection conn = new MySqlConnection(connStr); 

            try 

            { 

                Console.WriteLine("Connecting to MySQL..."); 

                conn.Open(); 

  

                string sql = "SELECT * FROM BikePart;"; 

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



            //Console.WriteLine("first_bike :");
            //Console.WriteLine(first_bike.Type);
            //Console.WriteLine(first_bike.Color);
            //Console.WriteLine(first_bike.Size);
            first_bike.Color="GREEN";
            first_bike.Type="explorer";
            first_bike.Size=28;
            //Console.WriteLine("first_bike changed :");
            //Console.WriteLine(first_bike.Type);
            //Console.WriteLine(first_bike.Color);
            //Console.WriteLine(first_bike.Size);

        }
    }
}
