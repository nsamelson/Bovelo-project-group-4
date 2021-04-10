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
    public partial class Showbike_other : Form
    {
        private User _currentUser;
        internal Showbike_other(User incomingUser)
        {
            InitializeComponent();
            _currentUser = incomingUser;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainHome mh = new MainHome(_currentUser);// create new window
            mh.FormClosed += (s, args) => this.Close();
            mh.Show();// Showing the Main home window
        }

        private void Showbike_other_Load(object sender, EventArgs e)
        {

        }
    }
}
