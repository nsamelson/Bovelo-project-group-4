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
        private User _currentUser = new User(" "," ");
      
        internal Cart(ref User incomingUser)
        {
            _currentUser = incomingUser;
            InitializeComponent();
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            int i = 0;
            while (i< _currentUser.cart.Count)
            {
                dataGridView1.Rows.Add();
                i++;
            }
            i = 0;
            foreach (Item elem in _currentUser.cart)
            {
                Console.WriteLine(elem.bike.Type + " " + elem.quantity);
                dataGridView1.Rows[i].Cells[0].Value = elem.bike.Type;
                dataGridView1.Rows[i].Cells[1].Value = elem.bike.Size;
                dataGridView1.Rows[i].Cells[2].Value = elem.bike.Color;
                dataGridView1.Rows[i].Cells[3].Value = elem.quantity;
                dataGridView1.Rows[i].Cells[4].Value = elem.bike.TotalTime.ToString();
                i++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //MainHome mh = new MainHome();// create new window
            /*mh.Show();// Showing the Main Home window*/
            this.Hide();// Hiding the Explorerbike Window
            var MainHome = new MainHome(_currentUser);// create new window
            MainHome.FormClosed += (s, args) => this.Close();
            MainHome.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Cart cart = new Cart(_currentUser);// create new window
            //_currentUser.cart.Show();// Showing the Cart window
            this.Hide();// Hiding the MainHome Window
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OrderBike o = new OrderBike(_currentUser);
            o.addOrderBike();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
