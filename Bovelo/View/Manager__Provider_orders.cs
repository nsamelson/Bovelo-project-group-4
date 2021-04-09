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
        private App newApp = new App();//because i need part name 
        internal Manager__Provider_orders(User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();
            newApp.SetBikePartList();
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
            Manager__Provider_catalog mpc = new Manager__Provider_catalog(ref user);// maybe send the userType with it
            mpc.FormClosed += (s, args) => this.Close(); // close the login Form
            mpc.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.Value.ToString() == "Remove")
            {
                string query = "DELETE FROM Order_Detailed_Part WHERE Id_Order=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value + " AND idOrder_Detailed_Part=" + dataGridView1.Rows[e.RowIndex].Cells[1].Value + " AND Id_Bike_Parts =" + dataGridView1.Rows[e.RowIndex].Cells[2].Value + " AND Quantity=" + dataGridView1.Rows[e.RowIndex].Cells[4].Value + ";";
                Console.WriteLine(query);
                DataBase.SendToDB(query);
                orderLoad();
                MessageBox.Show(@"Deleted :" +
                    "\nname = " + dataGridView1.Rows[e.RowIndex].Cells[3].Value+
                    "\nId_Order= " + dataGridView1.Rows[e.RowIndex].Cells[0].Value +
                    "\nId_Order_Detailed_Part = " + dataGridView1.Rows[e.RowIndex].Cells[1].Value +
                    "\nId_Bike_Parts = " + dataGridView1.Rows[e.RowIndex].Cells[2].Value +
                    "\nQuantity = " + dataGridView1.Rows[e.RowIndex].Cells[4].Value);

            }
            if (dataGridView1.CurrentCell.Value.ToString() == "Received")
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString() == "Not Received")
                {
                    string query = "UPDATE Order_Detailed_Part SET State='Received' WHERE Id_Order=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value + " AND idOrder_Detailed_Part=" + dataGridView1.Rows[e.RowIndex].Cells[1].Value + " AND Id_Bike_Parts =" + dataGridView1.Rows[e.RowIndex].Cells[2].Value + " AND Quantity=" + dataGridView1.Rows[e.RowIndex].Cells[4].Value + " ;";
                    //Console.WriteLine(query);
                    DataBase.SendToDB(query);
                    BikePart.addReceivedBikePart(Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()), Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()));
                    MessageBox.Show(@"Changed State :" +
                        "\nname = " + dataGridView1.Rows[e.RowIndex].Cells[3].Value +
                        "\nId_Order= " + dataGridView1.Rows[e.RowIndex].Cells[0].Value +
                        "\nId_Order_Detailed_Part = " + dataGridView1.Rows[e.RowIndex].Cells[1].Value +
                        "\nId_Bike_Parts = " + dataGridView1.Rows[e.RowIndex].Cells[2].Value +
                        "\nQuantity = " + dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                }
                orderLoad();
            }

        }

        private void orderLoad()
        {
            int i = 0;
            List<List<string>> result = DataBase.GetFromDB("Order_Detailed_Part");
            List<List<string>> result2 = DataBase.GetFromDB("Order_Part");
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
                    result2 = DataBase.GetFromDBWhere("Order_Part", all, "id_Order_Part=" + currentOrder);
                    previousOrder = currentOrder;
                }
                dataGridView1.Rows[i].Cells[0].Value = result[i][1];
                dataGridView1.Rows[i].Cells[1].Value = result[i][0];
                dataGridView1.Rows[i].Cells[2].Value = result[i][2];
                
                foreach(var value in newApp.bikePartList)
                {
                    if (value.part_Id == Int32.Parse(result[i][2]))
                    {
                        dataGridView1.Rows[i].Cells[3].Value = value.name.ToString();
                    }
                }
                dataGridView1.Rows[i].Cells[4].Value = result[i][3];
                dataGridView1.Rows[i].Cells[5].Value = result[i][4];
                dataGridView1.Rows[i].Cells[6].Value = result2[0][3];
                dataGridView1.Rows[i].Cells[7].Value = result[i][5];
                //dataGridView1.Rows[i].Cells[4].Value = newApp.getQuantity(elem.part.part_Id);
                i++;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Manager_Manager_Stock mms = new Manager_Manager_Stock(user);// maybe send the userType with it
            mms.FormClosed += (s, args) => this.Close(); // close the login Form
            mms.Show();
        }
    }
}
