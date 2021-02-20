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
            //Login login = new Login(appTest);// create new window
            //login.Show();// Showing the Login window
            this.Hide();// Hiding the MainHome Window
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Explorerbike explorerbike = new Explorerbike();// create new window
            explorerbike.Show();// Showing the Explorer bike window
            this.Hide();// Hiding the MainHome Window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart();// create new window
            cart.Show();// Showing the Cart window
            this.Hide();// Hiding the MainHome Window
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Citybike citybike = new Citybike();// create new window
            citybike.Show();// Showing the Citybike window
            this.Hide();// Hiding the MainHome Window
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Adventurebike adventurebike = new Adventurebike();// create new window
            adventurebike.Show();// Showing the Adventurebike window
            this.Hide();// Hiding the MainHome Window
        }
    }
}
