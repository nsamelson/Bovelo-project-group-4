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
    public partial class Assembler_MainHome : Form
    {
        private User user = new User(" ", false, false, false);
        internal Assembler_MainHome(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void Builder_MainHome_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the MainHome Window
            Assembler_Planning plng = new Assembler_Planning(user);// create new window
            plng.FormClosed += (s, args) => this.Close();
            plng.Show();// Showing the Assembler planning window
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }
    }
}
