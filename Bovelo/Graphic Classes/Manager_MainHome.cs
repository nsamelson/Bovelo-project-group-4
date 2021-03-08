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
        public Manager_MainHome()
        {
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
            Manager_Make_Planning mmp = new Manager_Make_Planning();// create new window
            mmp.FormClosed += (s, args) => this.Close();
            mmp.Show();// Showing the manager make planning window
        }
    }
}
