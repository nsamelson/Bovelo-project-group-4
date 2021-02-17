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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // to complete
        }

        private void signin_Click(object sender, EventArgs e) // signin button
        {
           if(username.Text=="bovelo" && password.Text=="bovelo") // check the password 
            {
                MainHome mh = new MainHome();// create new window
                mh.Show();// Showing the Login window
                this.Hide();// Hiding the MainHome Window
            }
           else
            {
                MessageBox.Show("The User name or password you entered is incorrect,try again !");
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
