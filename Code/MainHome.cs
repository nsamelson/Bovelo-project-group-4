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
        private App app = new App();
        public int userIndex;
        public MainHome(App app, int idx)
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
            this.Hide();
            var login = new Login(app);// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart();// create new window
            cart.Show();// Showing the Cart window
            this.Hide();// Hiding the MainHome Window
        }


        private void button_Click(object sender, EventArgs e)
        {

            Button Bt = (Button)sender;

            if (Convert.ToString(Bt.Tag) == "City")//ou autre
            {
                Citybike citybike = new Citybike();
                //Explorerbike explorerbike = new Explorerbike();
                //explorerbike.Show();

                citybike.Show();
                // ou bordure rouge ou ce que tu veux
            }

            this.Hide();// Hiding the MainHome Window
        }

        private void button6_Click(object sender, EventArgs e)
        {

            this.Hide();// Hiding the MainHome Window
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the MainHome Window
        }

        private void button4_Click(object sender, EventArgs e)
        {

            this.Hide();// Hiding the MainHome Window
        }
    }
}
