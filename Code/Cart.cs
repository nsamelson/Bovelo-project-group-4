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
            int i;
            OrderBike o = new OrderBike(_currentUser);
            o.CartLine = new List<string>();
            o.Cart = new List<List<string>>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    o.CartLine.Add(cell.Value.ToString());

                }
                o.Cart.Add(o.CartLine);
                label3.Text = o.Cart.Count.ToString();
            }

            o.addOrderBike();
            dataGridView1.Rows.Clear();

            //for (int i = 0; i <= dataGridView1.Rows.Count; i++)
            //{
            //label2.Text += dataGridView1.Rows[i].Cells[0].Value.ToString();

            //}
            //o.BikeType = dataGridView1.Rows[0].Cells[0].Value.ToString();
            //o.BikeSize = dataGridView1.Rows[0].Cells[1].Value.ToString();
            //o.BikeColor = dataGridView1.Rows[0].Cells[2].Value.ToString();
            //o.Quantity = int.Parse(dataGridView1.Rows[0].Cells[3].ToString());
            //o.DateTime = dataGridView1.Rows[0].Cells[4].Value.ToString();



           


        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
