using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace Bovelo
{
    public partial class Manager_Replace_bike_from_stock : Form
    {
        private App newApp = new App();
        private User user = new User("Manager", false, false, false);
        internal Manager_Replace_bike_from_stock(User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();
            LoadClientName();
            LoadBikeStock();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Manager_MainHome mmh = new Manager_MainHome(user);// create new window
            mmh.FormClosed += (s, args) => this.Close();
            mmh.Show();// Showing the Login window
        }

        void LoadBikeOrder()
        {
            dataGridView2.Rows.Clear();
            var Order = Manager.GetOrder(textBox1.Text);
            App newApp = new App();
            newApp.SetBikeModelList();
            int i = 0;
            List<List<string>> rows =new List<List<string>>();
            foreach (var elem in newApp.bikeModels)//for each item in cart
            {
                var bike = Order.FindAll(x => x[1] == elem.Type && x[3] == elem.Color && x[2]==elem.Size.ToString());
                rows.Add(new List<string> {elem.Type, elem.Color, elem.Size.ToString(), bike.Count().ToString() });
                i++;
            }
            i = 0;
            foreach (var row in rows)
            {
                if (row[3] != "0")
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = row[0];
                    dataGridView2.Rows[i].Cells[1].Value = row[1];
                    dataGridView2.Rows[i].Cells[2].Value = row[2];
                    dataGridView2.Rows[i].Cells[3].Value = row[3];
                    i++;
                }
            }
        }
        void LoadBikeStock()
        {
            dataGridView3.Rows.Clear();
            var stockBike = Representative.GetBikesInStock();
            App newApp= new App();
            newApp.SetBikeModelList();
            int i = 0;
            foreach (var elem in newApp.bikeModels)//for each item in cart
            {
                var stock = stockBike.FindAll(x => x.Type == elem.Type && x.Color == elem.Color && x.Size == elem.Size);
                int quantity = stock.Count();
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = elem.Type;
                dataGridView3.Rows[i].Cells[1].Value = elem.Size;
                dataGridView3.Rows[i].Cells[2].Value = elem.Color;
                dataGridView3.Rows[i].Cells[3].Value = quantity;
                i++;
            }
        }

        void ReplaceBikeFromTheStock()
        {
            
        }
        void LoadClientName()
        {
            dataGridView1.Rows.Clear();
            string sql = "SELECT Id_Order,Customer_Name FROM Order_Bikes;";
            var IdClient=DataBase.ConnectToDB(sql);
            int i = 0;
            foreach(var row in IdClient)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = row[0];
                dataGridView1.Rows[i].Cells[1].Value = row[1];
                i++;
            }
        }
        private void Manager_Replace_bike_from_stock_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Manager_Stock_Popup msp = new Manager_Stock_Popup();
            msp.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadBikeOrder();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
