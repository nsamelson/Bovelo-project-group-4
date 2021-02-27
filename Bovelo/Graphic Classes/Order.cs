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
        private User _currentUser = new User(" ", false, false, false);
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
            int clientId = 0;
            int i = 0;
            /*foreach (var elem in _currentUser.getOrderList(clientId))
            {

                dataGridView1.Rows.Add();
                Console.WriteLine(elem[0] + " " + elem[1]);
                dataGridView1.Rows[i].Cells[0].Value = elem[0].ToString();
                dataGridView1.Rows[i].Cells[1].Value = elem[1].ToString();
                dataGridView1.Rows[i].Cells[2].Value = elem[2].ToString();
                dataGridView1.Rows[i].Cells[3].Value = elem[3].ToString();
                dataGridView1.Rows[i].Cells[4].Value = elem[4].ToString();
                dataGridView1.Rows[i].Cells[5].Value = (Int32.Parse(elem[5])* Int32.Parse(elem[3])).ToString();

                i++;
            }*/
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

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
