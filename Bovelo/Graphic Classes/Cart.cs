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
    public partial class Cart : Form
    {
        
        public string[] row;
        private User _currentUser = new User(" ",false,false,false);

        private App app = new App();
        internal Cart(ref User incomingUser)
        {
            _currentUser = incomingUser;
            InitializeComponent();
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            int totalPrice = 0;
            int i = 0;
            while (i< _currentUser.cart.Count)
            {
                dataGridView1.Rows.Add();
                i++;
            }
            i = 0;
            foreach (Item elem in _currentUser.cart)
            {
                int price = 0;
                foreach (var bike in app.bikeModel)
                {
                    if (bike.Type == elem.bike.Type)
                    {
                        price = bike.Price;
                    }
                }
                totalPrice += price * elem.quantity;
                Console.WriteLine(elem.bike.Type + " " + elem.quantity);
                dataGridView1.Rows[i].Cells[0].Value = elem.bike.Type;
                dataGridView1.Rows[i].Cells[1].Value = elem.bike.Size;
                dataGridView1.Rows[i].Cells[2].Value = elem.bike.Color;
                dataGridView1.Rows[i].Cells[3].Value = elem.quantity;
                dataGridView1.Rows[i].Cells[4].Value = elem.bike.TotalTime.ToString();
                dataGridView1.Rows[i].Cells[5].Value = (price * elem.quantity).ToString();
                Console.WriteLine(price);
                i++;
            }
            Decimal B = totalPrice;
            this.labelPrice.Text = B.ToString() + "€";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the Explorerbike Window
            var MainHome = new MainHome(_currentUser);// create new window
            MainHome.FormClosed += (s, args) => this.Close();
            MainHome.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*OrderBike o = new OrderBike(_currentUser);
            o.addOrderBike();*/
            dataGridView1.Rows.Clear();
            _currentUser.emptyCart();
            this.labelPrice.Text = "0€";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Order order = new Order(ref _currentUser);// create new window
            order.FormClosed += (s, args) => this.Close();
            order.Show();// Showing the Order window
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            _currentUser.emptyCart();
            this.labelPrice.Text = "0€";
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
