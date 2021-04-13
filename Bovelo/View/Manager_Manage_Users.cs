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
    public partial class Manager_Manage_Users : Form
    {
        private App newApp = new App();
        private User user = new User("Manager", false, false, false);
        internal Manager_Manage_Users(User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();
        }

        private void signup_Click(object sender, EventArgs e)
        {
            //DONT NEED SIGNUP WINDOWS FORM, ONLY ENTER A NEW TYPE or maybe just a popup
            /*
            string userName = textBox1.Text;
            bool isExisting = app.userList.Any(login => login.login == userName);
            if (!isExisting && userName != "")
            {
                if (comboBox1.Text == "Representative")
                {
                    app.SetNewUser(new User(userName, true, false, false));
                    MessageBox.Show("A new Representative was created!");
                }
                else if (comboBox1.Text == "ProductionManager")
                {
                    app.SetNewUser(new User(userName,false,true,false));
                    MessageBox.Show("A new Production Manager was created!");
                }
                else if (comboBox1.Text == "Assembler")
                {
                    app.SetNewUser(new User(userName, false, false, true));
                    MessageBox.Show("A new Assembler was created!");
                }
                else { MessageBox.Show("Please select a valid user Type"); }
            }
            else if (userName == "")
            { MessageBox.Show("Please select a valid username "); }
            else { MessageBox.Show("The Username is already in use!"); }
            */
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Manager_MainHome mmh = new Manager_MainHome(user);// create new window
            mmh.FormClosed += (s, args) => this.Close();
            mmh.Show();// Showing the Login window
        }
    }
}
