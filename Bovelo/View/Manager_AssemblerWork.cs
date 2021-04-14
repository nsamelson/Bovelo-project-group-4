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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string builder = comboBox1.SelectedItem.ToString();
            int i = 0;
            TimeSpan result= new TimeSpan{};
            dataGridView1.Rows.Clear();
            foreach (var assemblerWork in Manager.GetAssemblerWork(builder))
            {
                //Console.WriteLine("détails in manager plan : " + orderDetails.Count);
                //Console.WriteLine(planifiedOrderDetails[0] + "|" + planifiedOrderDetails[1] + "|" + planifiedOrderDetails[2] + "|" + planifiedOrderDetails[3] + "|" + planifiedOrderDetails[4] + "|" + planifiedOrderDetails[5] + "|" + planifiedOrderDetails[6] + "|" + planifiedOrderDetails[7] + "|" + planifiedOrderDetails[8]);
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
                foreach (var elem in splitedDate) { Console.WriteLine(elem); };
                DateTime begin = new DateTime(Int32.Parse(splitedDate[2]), Int32.Parse(splitedDate[1]), Int32.Parse(splitedDate[0]), Int32.Parse(splitedDate[3]), Int32.Parse(splitedDate[4]),0);
                splitedDate = assemblerWork[11].Split('/',' ', ':');
                DateTime end   = new DateTime(Int32.Parse(splitedDate[2]), Int32.Parse(splitedDate[1]), Int32.Parse(splitedDate[0]), Int32.Parse(splitedDate[3]), Int32.Parse(splitedDate[4]),0); ;
                result += end - begin;
                i++;
            }
            label9.Text = result.ToString();
            if (dataGridView1.Rows.Count != 0)
            {
                LoadWeek();
            }
        }

        void LoadWeek()
        {
            string builder = comboBox1.SelectedItem.ToString();
            int min = Int32.Parse(dataGridView1.Rows[0].Cells[6].Value.ToString());
            int max = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Int32.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) < min)
                {
                    min = Int32.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                }
                if (Int32.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString()) > max)
                { 
                    max = Int32.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                }
            }

            List<int> values = new List<int> { };
            List<int> values2 = new List<int> { };
            //for (int i = min; i <= max; i++)
            //{
            //    values.Add(i);
            //    values2.Add(i);
            //}
            foreach(var week in Manager.getWeekName(builder))
            {
                values.Add(Int32.Parse(week[0]));
                values2.Add(Int32.Parse(week[0]));
            }
            comboBox2.DataSource = values;
            comboBox3.DataSource = values2;
        }

        void WorkLoad()
        {
            string builder = comboBox1.SelectedItem.ToString();
            int i = 0;
            TimeSpan result = new TimeSpan { };
            dataGridView1.Rows.Clear();
            foreach (var assemblerWork in Manager.GetAssemblerWork(builder))
            {
                //Console.WriteLine("détails in manager plan : " + orderDetails.Count);
                //Console.WriteLine(planifiedOrderDetails[0] + "|" + planifiedOrderDetails[1] + "|" + planifiedOrderDetails[2] + "|" + planifiedOrderDetails[3] + "|" + planifiedOrderDetails[4] + "|" + planifiedOrderDetails[5] + "|" + planifiedOrderDetails[6] + "|" + planifiedOrderDetails[7] + "|" + planifiedOrderDetails[8]);
                if (Int32.Parse(assemblerWork[7]) >= Int32.Parse(comboBox2.Text.ToString()) && Int32.Parse(assemblerWork[7])<= Int32.Parse(comboBox3.Text.ToString())){
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
                    foreach (var elem in splitedDate) { Console.WriteLine(elem); };
                    DateTime begin = new DateTime(Int32.Parse(splitedDate[2]), Int32.Parse(splitedDate[1]), Int32.Parse(splitedDate[0]), Int32.Parse(splitedDate[3]), Int32.Parse(splitedDate[4]), 0);
                    splitedDate = assemblerWork[11].Split('/', ' ', ':');
                    DateTime end = new DateTime(Int32.Parse(splitedDate[2]), Int32.Parse(splitedDate[1]), Int32.Parse(splitedDate[0]), Int32.Parse(splitedDate[3]), Int32.Parse(splitedDate[4]), 0); ;
                    result += end - begin;
                    i++;
                }
            }
            label9.Text = result.ToString();
        }


        
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Manager_MainHome mmh = new Manager_MainHome(user);// create new window
            mmh.FormClosed += (s, args) => this.Close();
            mmh.Show();// Showing the Login window
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WorkLoad();
        }
    }
}
