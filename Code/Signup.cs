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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();// create new window
            login.Show();// Showing the Login window
            this.Hide();// Hiding the MainHome Window
        }

        private void Signup_Load(object sender, EventArgs e)
        {

        }
    }
}
