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

            //Form login = Application.OpenForms["login"];

            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window

        }

        private void Signup_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //NEED TO MOVE IT IN ANOTHER CLASS
            /*bool isExisting = app.userList.Any(login => login.login == username.Text);
            if(!isExisting && username.Text!="" && password.Text !="")
            {
                if (comboBox1.Text == "Representative")
                {
                    app.addNewUser(new User(username.Text),true,false,false);
                }
                else if (comboBox1.Text == "ProductionManager")
                {
                    app.addNewUser(new User(username.Text),false,true,false);
                }
                else if (comboBox1.Text == "Assembler")
                {
                    app.addNewUser(new User(username.Text), false, false, true);
                }
                MessageBox.Show("The user was created!");
                //app.sendUserToDB(app.userList[app.userList.Count - 1]);
            }
            else if (username.Text == "" || password.Text == "") 
            { MessageBox.Show("Please select a valid username and password!"); }
            else { MessageBox.Show("The Username is already in use!"); }*/
            
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

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
