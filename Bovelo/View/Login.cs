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
            app.SetUserList();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = app.GetDifferentUserTypes();
        }

        private void signin_Click(object sender, EventArgs e) // login button
        {
            string userType = comboBox1.Text;
            string userName = textBox1.Text;
            bool isExisting = app.userList.Any(login => login.login == userName);
            if (!isExisting) { MessageBox.Show("The Username or password is incorrect, please try again !"); }
            else
            {
                var user = app.userList.FirstOrDefault(x => x.login == userName);
                var type = user.userType.FirstOrDefault(x => x.Value == true).Key;
                if (type == userType)
                {
                    this.Hide(); //hides the current form
                    switch (type)
                    {
                        case "Representative":
                            MainHome mh = new MainHome(user);// maybe send the userType with it
                            mh.FormClosed += (s, args) => this.Close(); // close the login Form
                            mh.Show();
                            break;
                        case "Assembler":
                            Assembler_MainHome amh = new Assembler_MainHome(user);// maybe send the userType with it
                            amh.FormClosed += (s, args) => this.Close(); // close the login Form
                            amh.Show();
                            break;
                        case "Production Manager":
                            Manager_MainHome mmh = new Manager_MainHome(user);// maybe send the userType with it
                            mmh.FormClosed += (s, args) => this.Close(); // close the login Form
                            mmh.Show();
                            break;
                    }
                    
                }
                else { MessageBox.Show("You didn't chose the correct userType !"); }

            }
        }
    }
}
