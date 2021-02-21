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
        private User _currentUser = new User(" "," "); 
        internal MainHome(User incomingUser)
        {
            InitializeComponent();
            _currentUser = incomingUser;
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
            Cart cart = new Cart(_currentUser);// create new window
            cart.Show();// Showing the Cart window
            this.Hide();// Hiding the MainHome Window
        }


        private void button_Click(object sender, EventArgs e)
        {

            Button Bt = (Button)sender;

            switch (Convert.ToString(Bt.Tag))
            {
                case "City" :
                
                    ShowBike citybike = new ShowBike("City",_currentUser);
                    citybike.Show();
                    break;
                case "Adventure" :
                    ShowBike adventure = new ShowBike("Adventure", _currentUser);
                    adventure.Show();
                    break;
                case "Explorer" :
                    ShowBike explorerbike = new ShowBike("Explorer", _currentUser);
                    explorerbike.Show();
                    break; 
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
