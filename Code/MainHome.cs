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
    public partial class MainHome : Form
    {
        public MainHome()
        {
            InitializeComponent();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // to complete
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // to complete
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // to complete
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();// create new window
            login.Show();// Showing the Login window
            this.Hide();// Hiding the MainHome Window
        }
    }
}
