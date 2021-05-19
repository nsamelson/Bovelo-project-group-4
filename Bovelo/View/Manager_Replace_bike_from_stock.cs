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

        public void LoadBikeOrder()
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
                    dataGridView2.Rows[i].Cells[3].Value = Manager.GetQuantity(row[0], Int32.Parse(row[2]), row[1], textBox1.Text);
                    dataGridView2.Rows[i].Cells[4].Value = Manager.GetQuantityClosed(row[0], Int32.Parse(row[2]), row[1],textBox1.Text);
                    i++;
                }
            }
        }
        public void LoadBikeStock()
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string bikeTypeStock = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
            string bikeColorStock = dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString();
            string bikeSizeStock = dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString();
            int maxValue = 0;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (bikeTypeStock == dataGridView2.Rows[i].Cells[0].Value.ToString() && bikeColorStock == dataGridView2.Rows[i].Cells[1].Value.ToString() && bikeSizeStock == dataGridView2.Rows[i].Cells[2].Value.ToString())
                {
                    maxValue = Math.Min(Int32.Parse(dataGridView3.Rows[e.RowIndex].Cells[3].Value.ToString()),(Int32.Parse(dataGridView2.Rows[i].Cells[3].Value.ToString())-Int32.Parse(dataGridView2.Rows[i].Cells[4].Value.ToString())));
                }
            }
            if (textBox1.Text == string.Empty)
            {
                string message = "Choose an order ID please !";
                var result = MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Form f = Application.OpenForms["Manager_Stock_Popup"];
                if (f != null)
                {
                    string message = "You didn't Validate your Changes, validate them or close the window !";
                    var result = MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    f.Activate();

                }
                else
                {
                    Manager_Stock_Popup msp = new Manager_Stock_Popup(this, new List<string> { dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString(), dataGridView3.Rows[e.RowIndex].Cells[1].Value.ToString(), dataGridView3.Rows[e.RowIndex].Cells[2].Value.ToString() }, Int32.Parse(textBox1.Text), maxValue);
                    msp.Show();// Showing the Login window
                }

            }
            
            
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
