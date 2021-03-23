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
    public partial class Manager__Provider_orders : Form
    {
        private User user = new User("Manager", false, false, false);
        private App newApp = new App();
        internal Manager__Provider_orders(User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();
            orderLoad();
        }

        private void Manager__Provider_orders_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager_MainHome mmh = new Manager_MainHome(user);// maybe send the userType with it
            mmh.FormClosed += (s, args) => this.Close(); // close the login Form
            mmh.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager__Provider_catalog mpc = new Manager__Provider_catalog(user);// maybe send the userType with it
            mpc.FormClosed += (s, args) => this.Close(); // close the login Form
            mpc.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.Value == "Remove")
            {
                string query = "DELETE FROM Order_Detailed_Part WHERE Id_Order=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value + " AND idOrder_Detailed_Part=" + dataGridView1.Rows[e.RowIndex].Cells[1].Value + " AND Id_Bike_Parts =" + dataGridView1.Rows[e.RowIndex].Cells[2].Value + " AND Quantity=" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + ";";
                Console.WriteLine(query);
                newApp.sendToDB(query);
                orderLoad();
            }
            if (dataGridView1.CurrentCell.Value == "Received")
            {
                string query = "UPDATE Order_Detailed_Part SET State='Received' WHERE Id_Order=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value + " AND idOrder_Detailed_Part=" + dataGridView1.Rows[e.RowIndex].Cells[1].Value + " AND Id_Bike_Parts =" + dataGridView1.Rows[e.RowIndex].Cells[2].Value + " AND Quantity=" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + " ;";
                Console.WriteLine(query);
                newApp.sendToDB(query);
                orderLoad();
            }

        }

        private void orderLoad()
        {
            int i = 0;
            List<List<string>> result = newApp.getFromDB("Order_Detailed_Part");
            List<List<string>> result2 = newApp.getFromDB("Order_Part");
            int currentOrder = Int32.Parse(result[i][1]);
            int previousOrder = 0;
            dataGridView1.Rows.Clear();
            foreach (var elem in result)
            {
                dataGridView1.Rows.Add();
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                List<string> all = new List<string>();
                all.Add("*");
                currentOrder = Int32.Parse(result[i][1]);
                if (currentOrder != previousOrder)
                {
                    result2 = newApp.getFromDBWhere("Order_Part", all, "id_Order_Part=" + currentOrder);
                    previousOrder = currentOrder;
                }
                dataGridView1.Rows[i].Cells[0].Value = result[i][1];
                dataGridView1.Rows[i].Cells[1].Value = result[i][0];
                dataGridView1.Rows[i].Cells[2].Value = result[i][2];
                dataGridView1.Rows[i].Cells[3].Value = result[i][3];
                dataGridView1.Rows[i].Cells[4].Value = result[i][4];
                dataGridView1.Rows[i].Cells[5].Value = result2[0][3];
                dataGridView1.Rows[i].Cells[6].Value = result[i][5];
                //dataGridView1.Rows[i].Cells[4].Value = newApp.getQuantity(elem.part.part_Id);
                i++;
            }
        }
    }
}
