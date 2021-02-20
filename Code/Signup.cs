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
        private App app = new App();
        public Signup(App app)
        {
            this.app = app;
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

            //Form login = Application.OpenForms["login"];

            this.Hide();
            var login = new Login(app);// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window

        }

        private void Signup_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool isExisting = app.userList.Any(login => login.login == username.Text);
            if(!isExisting)
            {
                if (comboBox1.Text == "Client")
                {
                    app.addNewUser(new User(username.Text, password.Text));
                }
                else if (comboBox1.Text == "Admin")
                {
                    app.addNewAdmin(new User(username.Text, password.Text));
                }
                MessageBox.Show("The user was created!");
            }
            else { MessageBox.Show("The Username is already in use!"); }
            
        }

        private void password_TextChanged(object sender, EventArgs e)
        {

        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
