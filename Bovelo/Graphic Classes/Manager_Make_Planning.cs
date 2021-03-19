using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Bovelo
{
    public partial class Manager_Make_Planning : Form
    {
        private App newApp = new App();
        private User user = new User("Manager", false, false, false);
        internal Manager_Make_Planning(User currentUser)
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
            int adapt = newApp.getPlanifiedBikes().Count;
            int i = 0;
            foreach (var nonPlanifiedBikes in newApp.getNonPlanifiedBikes())
            {
                if (e.RowIndex - adapt == i)
                {
                    Console.WriteLine(nonPlanifiedBikes[0] + "|" + nonPlanifiedBikes[1] + "|" + nonPlanifiedBikes[2] + "|" + nonPlanifiedBikes[3] + "|" + nonPlanifiedBikes[4] + "|" + nonPlanifiedBikes[5] + "|" + nonPlanifiedBikes[6]);
                    Console.WriteLine(nonPlanifiedBikes[1]);
                    Bike bike = new Bike(Int32.Parse(nonPlanifiedBikes[0]), nonPlanifiedBikes[1], nonPlanifiedBikes[3], Int32.Parse(nonPlanifiedBikes[2]));//Needs to be verified (id)
                    user.addToPlanningCart(bike, Int32.Parse(nonPlanifiedBikes[0]));
                }
                i++;    
            }
            if (dataGridView1.CurrentCell.Value.ToString() == "Modify")
            {
                
            }
            if (dataGridView1.CurrentCell.Value.ToString() == "Delete")
            {
                Console.WriteLine("e.RowIndex : " + e.RowIndex);
                int id = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                string week = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                Console.WriteLine("id : " + id + "week : " + week);
                newApp.deletePlanifiedBike(id,week);
                MessageBox.Show("Bike has been delted from schedule");
                Manager_Make_Planning_Load(sender, e);
                
            }

        }

        private void Manager_Make_Planning_Load(object sender, EventArgs e)
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
                DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                cell.Value = String.Empty;
                dataGridView1.Rows[i].Cells[7] = cell;
                cell.ReadOnly = true;
                i++;
            }           
            Console.WriteLine("index i : " + i);
            foreach (var nonPlanifiedOrderDetails in newApp.getNonPlanifiedBikes())
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = nonPlanifiedOrderDetails[0];//id order details
                dataGridView1.Rows[i].Cells[1].Value = nonPlanifiedOrderDetails[1];//type
                dataGridView1.Rows[i].Cells[2].Value = nonPlanifiedOrderDetails[2];//size
                dataGridView1.Rows[i].Cells[3].Value = nonPlanifiedOrderDetails[3];//color
                dataGridView1.Rows[i].Cells[4].Value = nonPlanifiedOrderDetails[5];//status
                dataGridView1.Rows[i].Cells[5].Value = nonPlanifiedOrderDetails[7];//Id Order
                dataGridView1.Rows[i].Cells[6].Value = "Not Plannified Yet"  ;
                DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                cell.Value = String.Empty;
                dataGridView1.Rows[i].Cells[8] = cell;
                cell.ReadOnly = true;
                
                DataGridViewTextBoxCell deleteCell = new DataGridViewTextBoxCell();
                deleteCell.Value = String.Empty;
                dataGridView1.Rows[i].Cells[9] = deleteCell;
                deleteCell.ReadOnly = true;
                i++;
            }

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            monthCalendar1.MaxSelectionCount = 1;
            DateTime Day = monthCalendar1.SelectionStart;
            var a = CultureInfo.CurrentCulture;
            var datetimeformat = a.DateTimeFormat;
            var calendarWeek = a.Calendar.GetWeekOfYear(monthCalendar1.SelectionStart, datetimeformat.CalendarWeekRule, datetimeformat.FirstDayOfWeek);
            textBox1.Text = "Week : " + calendarWeek.ToString() + ",  Year: " +  a.Calendar.GetYear(monthCalendar1.SelectionStart) ;
            Console.WriteLine("Week number : " +  calendarWeek);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string weekName = textBox1.Text;
            //Console.WriteLine("COUNT : " + user.planningCart.Count);
            newApp.setNewPlanning(user.planningCart, weekName);
            Manager_Make_Planning_Load(sender, e);
        } 

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Manager_MainHome mmh = new Manager_MainHome(user);// create new window
            mmh.FormClosed += (s, args) => this.Close();
            mmh.Show();// Showing the Login window
        }
    }
}
