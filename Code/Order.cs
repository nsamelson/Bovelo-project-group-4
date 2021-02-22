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
        private User _currentUser = new User(" ", " ", 0);
        internal Order(ref User incomingUser)
        {
            _currentUser = incomingUser;
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Show();

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
            Cart cart = new Cart(ref _currentUser);// create new window
            cart.Show();// Showing the Cart window
            this.Close();// Hiding the Explorerbike Window
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
