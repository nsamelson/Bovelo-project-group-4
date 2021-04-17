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
    public partial class Manager_Replace_bike_from_stock : Form
    {
        private App newApp = new App();
        private User user = new User("Manager", false, false, false);
        internal Manager_Replace_bike_from_stock(User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Manager_MainHome mmh = new Manager_MainHome(user);// create new window
            mmh.FormClosed += (s, args) => this.Close();
            mmh.Show();// Showing the Login window
        }
    }
}
