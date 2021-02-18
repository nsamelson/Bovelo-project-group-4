using System;
using Mysql.

public class OrderBike
{
	public DateTime TimeBeforeShipping;
	public bool IsReadyToShip;
	public int OrderId;

	public addOrderBike()
	{
        //string connStr = "server=localhost;user=root;database=bovelo;port=3306;password=root"; 
        string connStr = "server=193.191.240.67;user=testuser;database=Bovelo;port=63304;password=user_password";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");

            conn.Open();

            string sql = "INSERT INTO Bikes  VALUES (2,'Adventure',1200,'2 jours');";

            MySqlCommand cmd = new MySqlCommand(sql, conn);

            MySqlDataReader rdr = cmd.ExecuteReader();

            //while (rdr.Read())                   
            //{                
            //    listBox1.Items.Add(rdr[0] + " -- " + rdr[1]);                    
            //}               
            rdr.Close();
        }
        catch (Exception ex)
        {
                 = ex.ToString();
        }
        conn.Close();
        label1.Text = "Done";
    }
	public displayOrders()
    {

    }
}
