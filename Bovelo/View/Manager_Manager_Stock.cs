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
            var result = DataBase.GetFromDB("Bike_Parts");
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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("TRY");
            string name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            
            Int32.TryParse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), out int quantity);
            string location = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            Int32.TryParse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString(), out int price);
            string provider = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            Int32.TryParse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString(), out int time);

            if (dataGridView1.CurrentCell.Value.ToString() == "Set")
            {
                MessageBox.Show(@"New State :" +
                "\nName    = " + name +
                "\nQuantity= " + quantity +
                "\nLocation= " + location +
                "\nPrice   = " + price +
                "\nProvider= " + provider +
                "\nTime_To_Build = " + time +
                "\nId_Bike_Parts = " + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                Console.WriteLine("CATCH");
                if (quantity < 0)
                {
                    MessageBox.Show("you can not add a negative Value ");
                    dataGridView1.Rows[e.RowIndex].Cells[2].Value = 0;
                }
                else
                {
                    string query = "UPDATE Bike_Parts SET Bike_Parts_Name='" + name + "',Quantity=" + quantity + ",Location='" + location + "',Price=" + price + ",Provider='" + provider + "',Time_To_Build=" + time + " WHERE Id_Bike_Parts = " + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    DataBase.SendToDB(query);
                }
                
                
            }
            
            
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
