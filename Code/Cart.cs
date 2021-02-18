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
        public Cart()
        {
            InitializeComponent();
        }

        private void Cart_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            MainHome mh = new MainHome();// create new window
            mh.Show();// Showing the Main Home window
            this.Hide();// Hiding the Explorerbike Window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart();// create new window
            cart.Show();// Showing the Cart window
            this.Hide();// Hiding the MainHome Window
        }
    }
}
