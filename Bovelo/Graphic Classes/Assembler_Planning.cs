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
    
    public partial class Assembler_Planning : Form
    {
        private App newApp = new App();
        private User user = new User(" ", false, false, false);
        internal Assembler_Planning(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void assembler_Planning_Load(object sender, EventArgs e)
        {
            
            foreach (var planning in newApp.getPlanningList())
            {
                int i = 0;
                foreach(var bike in planning.bikesToBuild)
                {
                    dataGridView1.Rows[i].Cells[0].Value = 1;
                    dataGridView1.Rows[i].Cells[1].Value = bike.Size;
                    dataGridView1.Rows[i].Cells[2].Value = bike.Type;
                    dataGridView1.Rows[i].Cells[3].Value = bike.Type;
                    //dataGridView1.Rows[i].Cells[4].Value = bike.getBikeParts();
                    dataGridView1.Rows[i].Cells[5].Value = bike.state;
                }
                i++;
            }
        }

        private void Assembler_Planning_Load_1(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Assembler_MainHome amh = new Assembler_MainHome(user);// create new window
            amh.FormClosed += (s, args) => this.Close();
            amh.Show();// Showing the Login window
        }
    }
}
