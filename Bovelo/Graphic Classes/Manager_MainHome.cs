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
    public partial class Manager_MainHome : Form
    {
        private User user = new User("Manager", false, false, false);
        internal Manager_MainHome(User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the MainHome Window
            Manager_Make_Planning mmp = new Manager_Make_Planning(user);// create new window
            mmp.FormClosed += (s, args) => this.Close();
            mmp.Show();// Showing the manager make planning window
        }

        private void Manager_MainHome_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            MainHome mh = new MainHome(user);// maybe send the userType with it
            mh.FormClosed += (s, args) => this.Close(); // close the login Form
            mh.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager__Provider_catalog mpc = new Manager__Provider_catalog(user);// maybe send the userType with it
            mpc.FormClosed += (s, args) => this.Close(); // close the login Form
            mpc.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager__Provider_orders mpo = new Manager__Provider_orders(user);// maybe send the userType with it
            mpo.FormClosed += (s, args) => this.Close(); // close the login Form
            mpo.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager_Create_Model mpc = new Manager_Create_Model(user);// maybe send the userType with it
            mpc.FormClosed += (s, args) => this.Close(); // close the login Form
            mpc.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Manager_Manager_Stock mms = new Manager_Manager_Stock(user);// maybe send the userType with it
            mms.FormClosed += (s, args) => this.Close(); // close the login Form
            mms.Show();
        }
    }
}
