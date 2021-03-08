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
    }
}
