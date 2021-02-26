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
        private App app = new App();
        
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

            bool isExisting = app.userList.Any(login => login.login == username.Text);
            if (!isExisting) { MessageBox.Show("The Username or password is incorrect, please try again!"); }
            else
            {
              int index = app.userList.FindIndex(a => a.login == username.Text);
                this.Hide(); //hides the current form
                MainHome mh = new MainHome(app.userList[index]);// create new window Form with the user passed in it
                mh.FormClosed += (s, args) => this.Close(); // close the login Form
                mh.Show();// Showing the Sign-up window
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

        private void signup_Click(object sender, EventArgs e)
        {
            this.Hide();
            var signup = new Signup();// create new window
            signup.FormClosed += (s, args) => this.Close();
            signup.Show();// Showing the Sign-up window

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
