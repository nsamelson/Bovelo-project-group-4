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
        private User _currentUser;

        private App app = new App();
        internal Cart(ref User incomingUser)
        {
            _currentUser = incomingUser;
            InitializeComponent();
        }

        private void Cart_Load(object sender, EventArgs e)
        {

            int i = 0;
            foreach (ItemBike elem in _currentUser.cart)
            {
                dataGridView1.Rows.Add();
                int price = elem.getPrice();                
                dataGridView1.Rows[i].Cells[0].Value = elem.bike.Type;
                dataGridView1.Rows[i].Cells[1].Value = elem.bike.Size;
                dataGridView1.Rows[i].Cells[2].Value = elem.bike.Color;
                dataGridView1.Rows[i].Cells[3].Value = elem.quantity;
                dataGridView1.Rows[i].Cells[4].Value = "0 in stock";//elem.bike.TotalTime.ToString();
                dataGridView1.Rows[i].Cells[5].Value = price.ToString();
                i++;
            }
            Decimal B = _currentUser.getCartPrice();
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
            string client = textBox1.Text;        
            int RowCount = _currentUser.getCartList().Count();

            if (client != ""&&RowCount!=0)
            {
                app.setNewOrderBike(_currentUser.getCartList(), client, _currentUser.getCartPrice());
                dataGridView1.Rows.Clear();
                _currentUser.emptyCart();
                this.labelPrice.Text = "0€";
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter a valid name or add items to the cart!");
            }
            
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
