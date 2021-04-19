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
                var bike = Order.FindAll(x => x[1] == elem.type && x[3] == elem.color && x[2]==elem.size.ToString());
                rows.Add(new List<string> {elem.type, elem.color, elem.size.ToString(), bike.Count().ToString() });
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
                    dataGridView2.Rows[i].Cells[4].Value = Manager.GetQuantityNotClosed(row[0], Int32.Parse(row[2]), row[1],textBox1.Text);
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
                var stock = stockBike.FindAll(x => x.type == elem.type && x.color == elem.color && x.size == elem.size);
                int quantity = stock.Count();
                dataGridView3.Rows.Add();
                dataGridView3.Rows[i].Cells[0].Value = elem.type;
                dataGridView3.Rows[i].Cells[1].Value = elem.color;
                dataGridView3.Rows[i].Cells[2].Value = elem.size;
                dataGridView3.Rows[i].Cells[3].Value = quantity;
                i++;
            }
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
            Console.WriteLine(dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString() + "|" + dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString() + "|" + dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString() + "|" + Int32.Parse(textBox1.Text));
            Manager_Stock_Popup msp = new Manager_Stock_Popup(new List<string> {dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString(), dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString(), dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString()},Int32.Parse(textBox1.Text));
            msp.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadBikeOrder();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
