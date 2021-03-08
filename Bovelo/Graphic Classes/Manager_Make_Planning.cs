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
    public partial class Manager_Make_Planning : Form
    {
        private App newApp = new App();
        public Manager_Make_Planning()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Manager_Make_Planning_Load(object sender, EventArgs e)
        {
            int i = 0;
            foreach (var orderDetails in newApp.getOrderDetails())
            {

                //Console.WriteLine("détails in manager plan : " + orderDetails[0]);
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = orderDetails[0];
                dataGridView1.Rows[i].Cells[1].Value = orderDetails[1];
                dataGridView1.Rows[i].Cells[2].Value = orderDetails[2];
                dataGridView1.Rows[i].Cells[3].Value = orderDetails[3];
                dataGridView1.Rows[i].Cells[4].Value = orderDetails[5];

                i++;



            }
        }
    }
}
