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
    public partial class Order : Form
    {
        private App newApp = new App();
        private User _currentUser;
        //private List<List<string>> orderList;
        private OrderBike order ;
        internal Order(ref User incomingUser)
        {
            _currentUser = incomingUser;
            InitializeComponent();
            //this.order = new OrderBike(incomingUser);
            //this.orderList = _currentUser.orderList;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void Order_Load(object sender, EventArgs e)
        {
            int i = 0;
            foreach(var orders in newApp.getOrderBikeList())//Errors in the cells
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = orders.orderId.ToString();
                dataGridView1.Rows[i].Cells[1].Value = orders.clientName;
                dataGridView1.Rows[i].Cells[2].Value = orders.totalPrice;
                dataGridView1.Rows[i].Cells[3].Value = orders.orderDate.ToString();
                dataGridView1.Rows[i].Cells[4].Value = orders.shippingDate.ToString();


                i++;
            }
         
        }
        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            var MainHome = new MainHome(_currentUser);// create new window
            MainHome.FormClosed += (s, args) => this.Close();
            MainHome.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Cart cart = new Cart(ref _currentUser);// create new window
            cart.FormClosed += (s, args) => this.Close();
            cart.Show();// Showing the Cart window
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
