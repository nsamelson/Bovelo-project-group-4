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
    public partial class Manager_AssemblerWork : Form
    {
        private App newApp = new App();
        private User user = new User("Manager", false, false, false);
        internal Manager_AssemblerWork(User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();
        }

        private void Manager_AssemblerWork_Load(object sender, EventArgs e)
        {
            int i = 0;
            foreach (var planifiedOrderDetails in newApp.getPlanifiedBikes())
            {
                //Console.WriteLine("détails in manager plan : " + orderDetails.Count);
                //Console.WriteLine(planifiedOrderDetails[0] + "|" + planifiedOrderDetails[1] + "|" + planifiedOrderDetails[2] + "|" + planifiedOrderDetails[3] + "|" + planifiedOrderDetails[4] + "|" + planifiedOrderDetails[5] + "|" + planifiedOrderDetails[6] + "|" + planifiedOrderDetails[7] + "|" + planifiedOrderDetails[8]);
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = planifiedOrderDetails[0];//id order details
                dataGridView1.Rows[i].Cells[1].Value = planifiedOrderDetails[1];//type
                dataGridView1.Rows[i].Cells[2].Value = planifiedOrderDetails[2];//size
                dataGridView1.Rows[i].Cells[3].Value = planifiedOrderDetails[3];//color
                dataGridView1.Rows[i].Cells[4].Value = planifiedOrderDetails[5];//status
                dataGridView1.Rows[i].Cells[5].Value = planifiedOrderDetails[7];//Id Order
                dataGridView1.Rows[i].Cells[6].Value = planifiedOrderDetails[8];//planified week
                i++;
            }
            var users = newApp.getAssembler().Select(x=> x[0]).ToList();
            comboBox1.DataSource = users;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        List<List<string>> Assign = new List<List<string>>();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
            foreach (var bikestoAssign in newApp.getPlanifiedBikes())
            {
                if (e.RowIndex == i)
                {
                    Console.WriteLine(bikestoAssign[0] + "|" + bikestoAssign[1] + "|" + bikestoAssign[2] + "|" + bikestoAssign[3] + "|" + bikestoAssign[4] + "|" + bikestoAssign[5] + "|" + bikestoAssign[6]);
                    Console.WriteLine(bikestoAssign[1]);
                    //Bike bike = new Bike(Int32.Parse(bikestoAssign[0]), bikestoAssign[1], bikestoAssign[3], Int32.Parse(bikestoAssign[2]));//Needs to be verified (id)

                    
                }
                i++;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
