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
            // to complete
        }

        private void signin_Click(object sender, EventArgs e) // login button
        {
            //NOT OPTIMAL
            //if Assembler : select which one (id, name)?
            //elif representative
            //elif Product manager
            string userType = comboBox1.Text;
            string userName = textBox1.Text;
            Console.WriteLine("user List prob : " + app.userList.Count); ;
            bool isExisting = app.userList.Any(login => login.login == userName);
            if (!isExisting) { MessageBox.Show("The Username or password is incorrect, please try again or create a new user!"); }
            else
            {
                if (userType == "Representative")
                {
                    int index = app.userList.FindIndex(a => a.login == userName);
                    this.Hide(); //hides the current form
                    MainHome mh = new MainHome(app.userList[index]);// maybe send the userType with it
                    mh.FormClosed += (s, args) => this.Close(); // close the login Form
                    mh.Show();
                }
                if (userType == "Assembler")
                {
                    int index = app.userList.FindIndex(a => a.login == userName);
                    this.Hide(); //hides the current form
                    Assembler_MainHome amh = new Assembler_MainHome(app.userList[index]);// maybe send the userType with it
                    amh.FormClosed += (s, args) => this.Close(); // close the login Form
                    amh.Show();
                }
                if (userType == "Production Manager")
                {
                    int index = app.userList.FindIndex(a => a.login == userName);
                    this.Hide(); //hides the current form
                    Manager_MainHome mmh = new Manager_MainHome(app.userList[index]);// maybe send the userType with it
                    mmh.FormClosed += (s, args) => this.Close(); // close the login Form
                    mmh.Show();
                }                    
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
