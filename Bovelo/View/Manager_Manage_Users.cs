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
            LoadUser();
        }

        internal void LoadUser()
        {
            comboBox1.DataSource= newApp.GetDifferentUserTypes();//set different user types in combobox
            var loginRole = Manager.GetUser();
            int i = 0;
            dataGridView1.Rows.Clear();           
            foreach (var elem in loginRole)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = elem[1];
                dataGridView1.Rows[i].Cells[1].Value = elem[0];
                i++;
            }
        }

        private void signup_Click(object sender, EventArgs e)
        {
            //DONT NEED SIGNUP WINDOWS FORM, ONLY ENTER A NEW TYPE or maybe just a popup
            string userRole = comboBox1.Text;
            string userLogin = textBox1.Text;
            newApp.SetUserList();
            bool isExisting = newApp.userList.Any(login => login.login == userLogin);            
            List<string> loginRole = new List<string> { userRole,userLogin};
            if (!isExisting && userLogin != "")
            {
                if (comboBox1.Text == "Representative")
                {
                    newApp.SetNewUser(loginRole);
                    MessageBox.Show("A new Representative was created!");
                }
                else if (comboBox1.Text == "ProductionManager")
                {
                    newApp.SetNewUser(loginRole);
                    MessageBox.Show("A new Production Manager was created!");
                }
                else if (comboBox1.Text == "Assembler")
                {
                    newApp.SetNewUser(loginRole);
                    MessageBox.Show("A new Assembler was created!");
                }
                else { MessageBox.Show("Please select a valid user Type"); }
            }
            else if (userLogin == "")
            { MessageBox.Show("Please select a valid username "); }
            else { MessageBox.Show("The Username is already in use!"); }
            LoadUser();
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
