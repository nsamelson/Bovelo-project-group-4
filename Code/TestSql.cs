

namespace mysqlDistant 

{ 

    public partial class Form1 : Form 

    { 

        public Form1() 

        { 

            InitializeComponent(); 

        } 

  

        private void button1_Click(object sender, EventArgs e) 

        { 

            //string connStr = "server=localhost;user=root;database=bovelo;port=3306;password=root"; 

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

            label1.Text = "Done"; 

        } 

    } 

} 

 

 