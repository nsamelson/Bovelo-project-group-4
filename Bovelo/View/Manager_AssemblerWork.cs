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
            var users = Manager.GetAssemblers().Select(x=> x[0]).ToList();
            comboBox1.DataSource = users;
            LoadWeek();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = 0;
            foreach (var bikestoAssign in Manager.GetPlanifiedBikes())
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

        void LoadWeek()
        {
            newApp.SetPlanningList();
            var plans1 = newApp.planningList.Select(x => x.weekName).ToList();
            var plans2 = newApp.planningList.Select(x => x.weekName).ToList();
            comboBox2.DataSource = plans1; //shows the existing schedules
            comboBox3.DataSource = plans2; //shows the existing schedules
        }

        void WorkLoad()
        {
            string builder = comboBox1.SelectedItem.ToString();
            int i = 0;
            TimeSpan result = new TimeSpan { };
            dataGridView1.Rows.Clear();
            foreach (var assemblerWork in Manager.GetAssemblerWork(builder))
            {
                if (Int32.Parse(assemblerWork[7]) >= Int32.Parse(comboBox2.Text.ToString()) && Int32.Parse(assemblerWork[7]) <= Int32.Parse(comboBox3.Text.ToString()))
                {
                    //planified week
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = assemblerWork[0];//id order details
                    dataGridView1.Rows[i].Cells[1].Value = assemblerWork[1];//type
                    dataGridView1.Rows[i].Cells[2].Value = assemblerWork[2];//size
                    dataGridView1.Rows[i].Cells[3].Value = assemblerWork[3];//color
                    dataGridView1.Rows[i].Cells[4].Value = assemblerWork[5];//status
                    dataGridView1.Rows[i].Cells[5].Value = assemblerWork[6];//Id Order
                    dataGridView1.Rows[i].Cells[6].Value = assemblerWork[7];//planified week
                    dataGridView1.Rows[i].Cells[7].Value = assemblerWork[10];//started at
                    dataGridView1.Rows[i].Cells[8].Value = assemblerWork[11];//finished at
                    var splitedDate = assemblerWork[10].Split('/', ' ', ':');
                    DateTime begin = new DateTime(Int32.Parse(splitedDate[2]), Int32.Parse(splitedDate[1]), Int32.Parse(splitedDate[0]), Int32.Parse(splitedDate[3]), Int32.Parse(splitedDate[4]), 0);
                    splitedDate = assemblerWork[11].Split('/', ' ', ':');
                    DateTime end = new DateTime(Int32.Parse(splitedDate[2]), Int32.Parse(splitedDate[1]), Int32.Parse(splitedDate[0]), Int32.Parse(splitedDate[3]), Int32.Parse(splitedDate[4]), 0); ;
                    result += end - begin;
                    Console.WriteLine("BEGIN : " + begin + " END : " + end + "RESULT : " + result);
                    i++;
                }
            }
            var minutes = result.TotalMinutes % 60;
            var hours = (int)((double)(result.TotalHours));
            label9.Text = hours.ToString() + " hours and " + minutes.ToString() + " minutes";
        }
       
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Manager_MainHome mmh = new Manager_MainHome(user);// create new window
            mmh.FormClosed += (s, args) => this.Close();
            mmh.Show();// Showing the Login window
        }
        private void button1_Click(object sender, EventArgs e)
        {
            WorkLoad();
        }
    }
}
