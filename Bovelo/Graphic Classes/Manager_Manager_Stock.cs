using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bovelo
{
    public partial class Manager_Manager_Stock : Form
    {
        private App newApp = new App();
        private User user = new User("Manager", false, false, false);
        internal Manager_Manager_Stock(User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();
            stockLoad();
        }

        private void stockLoad()
        {
            int i = 0;
            var result = newApp.getFromDB("Bike_Parts");
            dataGridView1.Rows.Clear();
            foreach (var elem in result)
            {
                dataGridView1.Rows.Add();
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.Rows[i].Cells[0].Value = elem[0];
                dataGridView1.Rows[i].Cells[1].Value = elem[1];
                dataGridView1.Rows[i].Cells[2].Value = elem[2];
                dataGridView1.Rows[i].Cells[3].Value = elem[3];
                dataGridView1.Rows[i].Cells[4].Value = elem[4];
                dataGridView1.Rows[i].Cells[5].Value = elem[5];
                dataGridView1.Rows[i].Cells[6].Value = elem[6];
                i++;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("TRY");
            string name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            int quantity = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            string location = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            int price = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
            string provider = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            int time = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
            if (dataGridView1.CurrentCell.Value == "Set")
            {
                Console.WriteLine("CATCH");
                string query = "UPDATE Bike_Parts SET Bike_Parts_Name='" + name + "',Quantity=" + quantity + ",Location='" + location + "',Price=" + price + ",Provider='" + provider + "',Time_To_Build=" + time + " WHERE Id_Bike_Parts = " + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                newApp.sendToDB(query);
            }
            MessageBox.Show(@"New State :"+ 
                "\nName    = " + name +
                "\nQuantity= " + quantity +
                "\nLocation= " + location +
                "\nPrice   = " + price +
                "\nProvider= " + provider +
                "\nTime_To_Build = " + time +
                "\nId_Bike_Parts = " + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager__Provider_catalog mpc = new Manager__Provider_catalog(ref user);// maybe send the userType with it
            mpc.FormClosed += (s, args) => this.Close(); // close the login Form
            mpc.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager__Provider_orders mpo = new Manager__Provider_orders(user);// maybe send the userType with it
            mpo.FormClosed += (s, args) => this.Close(); // close the login Form
            mpo.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager_MainHome mmh = new Manager_MainHome(user);// maybe send the userType with it
            mmh.FormClosed += (s, args) => this.Close(); // close the login Form
            mmh.Show();
        }
    }
}
