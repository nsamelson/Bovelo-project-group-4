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
    public partial class Manager__Provider_catalog : Form
    {
        private User user = new User("Manager", false, false, false);
        private App newApp = new App();
        internal Manager__Provider_catalog(User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager_MainHome mmh = new Manager_MainHome(user);// maybe send the userType with it
            mmh.FormClosed += (s, args) => this.Close(); // close the login Form
            mmh.Show();
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
            //dataGridView1.Rows.Clear();
            int i = 0;
            Dictionary<int, int> resultWeek = newApp.getWeekPieces("Week : 12");
            Dictionary<int, int> resultCompute = newApp.computeMissingPieces(resultWeek);
            foreach (var part in newApp.bikePartList)
            {
                dataGridView1.Rows.Add();
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.Rows[i].Cells[0].Value = part.part_Id;
                dataGridView1.Rows[i].Cells[1].Value = part.name;
                dataGridView1.Rows[i].Cells[2].Value = part.price;
                dataGridView1.Rows[i].Cells[3].Value = part.provider;
                dataGridView1.Rows[i].Cells[4].Value = newApp.getQuantity(part.part_Id);
                foreach (var elem in resultCompute)
                {
                    if (part.part_Id == elem.Key)
                    {
                        dataGridView1.Rows[i].Cells[5].Value = elem.Value;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[5].Value = "-";
                    }
                }
                i++;
            }
        }
        private void part_load(object sender, EventArgs e)
        {

        }//end of button3_click

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }// end of Manager__Provider_catalog : Form
}//end of namespace Bovelo
