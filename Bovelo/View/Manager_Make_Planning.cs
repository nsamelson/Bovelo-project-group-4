﻿using System;
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
            this.user = currentUser;
            InitializeComponent();
            newApp.updateBikePartList();
            newApp.updateBikeModelList();
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
            this.Hide(); //hides the current form
            Manager__Provider_catalog mpc = new Manager__Provider_catalog(ref user);// maybe send the userType with it
            mpc.FormClosed += (s, args) => this.Close(); // close the login Form
            mpc.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dataGridView1.CurrentCell.Value.ToString() == "Modify")
            {
                MessageBox.Show("Choose a week from the calendar ");
                
                //dataGridView1.Rows[e.RowIndex].Cells[6].Value = "Week  " + calendarWeek.ToString();
                weekToModify.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                idBike.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();   
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
            int t = 0;
            Console.WriteLine("détails in manager plan : " + newApp.getPlanifiedBikes().Count);
            foreach (var planifiedOrderDetails in newApp.getPlanifiedBikes())
            {
                //Console.WriteLine("détails in manager plan : " + orderDetails.Count);
                Console.WriteLine(planifiedOrderDetails[0] + "|" + planifiedOrderDetails[1] + "|" + planifiedOrderDetails[2] + "|" + planifiedOrderDetails[3] + "|" + planifiedOrderDetails[4] + "|" + planifiedOrderDetails[5] + "|" + planifiedOrderDetails[6] + "|" + planifiedOrderDetails[7] + "|" + planifiedOrderDetails[8]);
                BikeModel model = newApp.bikeModels.FirstOrDefault(x => x.Color == planifiedOrderDetails[3] && x.Size == Int32.Parse(planifiedOrderDetails[2]) && x.Type == planifiedOrderDetails[1]);//gets the specific model
                Bike newBike = new Bike(Int32.Parse(planifiedOrderDetails[0]), model);
                t += newBike.TotalTime;

                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = planifiedOrderDetails[0];//id order details
                dataGridView1.Rows[i].Cells[1].Value = planifiedOrderDetails[1];//type
                dataGridView1.Rows[i].Cells[2].Value = planifiedOrderDetails[2];//size
                dataGridView1.Rows[i].Cells[3].Value = planifiedOrderDetails[3];//color
                dataGridView1.Rows[i].Cells[4].Value = planifiedOrderDetails[5];//status
                dataGridView1.Rows[i].Cells[5].Value = planifiedOrderDetails[7];//plannified week
                dataGridView1.Rows[i].Cells[6].Value = planifiedOrderDetails[8];//Id Order
                dataGridView1.Rows[i].Cells[7].Value = newBike.TotalTime.ToString();
                i++;

                
            }           
           /* Console.WriteLine("index i : " + i);
            Console.WriteLine("Count  : " + newApp.getNonPlanifiedBikes().Count);*/
            i = 0;
            foreach (var nonPlanifiedOrderDetails in newApp.getNonPlanifiedBikes())
            {
                BikeModel model = newApp.bikeModels.FirstOrDefault(x => x.Color == nonPlanifiedOrderDetails[3] && x.Size == Int32.Parse(nonPlanifiedOrderDetails[2]) && x.Type == nonPlanifiedOrderDetails[1]);//gets the specific model
                Bike newBike = new Bike(Int32.Parse(nonPlanifiedOrderDetails[0]), model);
                t += newBike.TotalTime;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = nonPlanifiedOrderDetails[0];//id order details
                dataGridView2.Rows[i].Cells[1].Value = nonPlanifiedOrderDetails[1];//type
                dataGridView2.Rows[i].Cells[2].Value = nonPlanifiedOrderDetails[2];//size
                dataGridView2.Rows[i].Cells[3].Value = nonPlanifiedOrderDetails[3];//color
                dataGridView2.Rows[i].Cells[4].Value = nonPlanifiedOrderDetails[5];//status
                dataGridView2.Rows[i].Cells[5].Value = nonPlanifiedOrderDetails[6];//Id Order
                dataGridView2.Rows[i].Cells[6].Value = newBike.TotalTime.ToString() ;
                
                i++;
            }
            labelTime.Text = t.ToString() + " / " + (120 * 60).ToString();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            monthCalendar1.MaxSelectionCount = 1;
            DateTime Day = monthCalendar1.SelectionStart;
            var a = CultureInfo.CurrentCulture;
            var datetimeformat = a.DateTimeFormat;
            var calendarWeek = a.Calendar.GetWeekOfYear(monthCalendar1.SelectionStart, datetimeformat.CalendarWeekRule, datetimeformat.FirstDayOfWeek);
            newWeekToAssign.Text = "Week : " + calendarWeek.ToString();
            textBox1.Text = "Week : " + calendarWeek.ToString();
            Console.WriteLine("Week number : " +  calendarWeek);
            newWeekToAssign.Text = "Week : " + calendarWeek.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Are you trying to planify a bike without choosing a week, Idiot Manager, You are fired !");
            }
            else
            {
                string weekName = textBox1.Text;
                newApp.setNewPlanning(user.planningCart, weekName);
            }
            
        } 

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Manager_MainHome mmh = new Manager_MainHome(user);// create new window
            mmh.FormClosed += (s, args) => this.Close();
            mmh.Show();// Showing the Login window
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int id = Int32.Parse(idBike.Text.ToString());
            string newWeek = newWeekToAssign.Text.ToString();
            string currentWeek = weekToModify.Text.ToString();
            newApp.updateSchedule(id, newWeek, currentWeek);
            Manager_Make_Planning_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager__Provider_orders mpo = new Manager__Provider_orders(user);// maybe send the userType with it
            mpo.FormClosed += (s, args) => this.Close(); // close the login Form
            mpo.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            BikeModel model = newApp.bikeModels.FirstOrDefault(x => x.Color == dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString() && x.Size == Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString()) && x.Type == dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString());//gets the specific model
            Bike bike = new Bike(Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString()), model);//Needs to be verified (id)
            user.addToPlanningCart(bike, Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString()));
            // NEED TO MODIFY THE DATAGRIDVIEW AND THE NON PLANNIFIEDBIKES

            string weekName = textBox1.Text;
            dataGridView1.Rows.Add();
            List<string> reset = new List<string>();
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = dataGridView2.Rows[e.RowIndex].Cells[0].Value; //id order details
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = dataGridView2.Rows[e.RowIndex].Cells[1].Value; //type
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].Value = dataGridView2.Rows[e.RowIndex].Cells[2].Value;//size
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Value = dataGridView2.Rows[e.RowIndex].Cells[3].Value;//color
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[4].Value = dataGridView2.Rows[e.RowIndex].Cells[4].Value;//status
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[5].Value = weekName;//plannified week
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[6].Value = dataGridView2.Rows[e.RowIndex].Cells[5].Value;
            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[7].Value = dataGridView2.Rows[e.RowIndex].Cells[6].Value;
            dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void labelPrice_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
