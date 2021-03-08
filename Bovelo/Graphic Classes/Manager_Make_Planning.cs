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
            foreach (var orderBikeList in newApp.getOrderBikeList())
            {
                foreach (var orderDetails in orderBikeList.orderDetail)
                {

                    //Console.WriteLine("détails in manager plan : " + orderDetails[0]);
                    //Console.WriteLine(orderDetails[0] + "|" + orderDetails[1] + "|" + orderDetails[2] + "|" + orderDetails[3] + "|" + orderDetails[4] + "|" + orderDetails[5] + "|" + orderDetails[6] + "|" + orderDetails[7] + "|" + orderDetails[8] + "|");
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = orderDetails[6];//[id, Client_Name, Bike_Type, Bike_Color, Bike_Size, Quantity, Price, Order_Time]
                    dataGridView1.Rows[i].Cells[1].Value = orderDetails[0];
                    dataGridView1.Rows[i].Cells[2].Value = orderDetails[7];
                    dataGridView1.Rows[i].Cells[3].Value = orderDetails[1];
                    dataGridView1.Rows[i].Cells[4].Value = orderDetails[2];
                    dataGridView1.Rows[i].Cells[5].Value = orderDetails[3];
                    //dataGridView1.Rows[i].Cells[6].Value = orderDetails[4];
                    dataGridView1.Rows[i].Cells[6].Value = orderBikeList.orderDate;

                    i++;
                }
                
            }
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            monthCalendar1.MaxSelectionCount = 1;
            textBox1.Text = (monthCalendar1.SelectionRange.Start).ToString();
            DateTime Day = monthCalendar1.SelectionStart;
            var a = CultureInfo.CurrentCulture;
            var dtf = a.DateTimeFormat;
            var calendarWeek = a.Calendar.GetWeekOfYear(monthCalendar1.SelectionStart, dtf.CalendarWeekRule, dtf.FirstDayOfWeek);

            Console.WriteLine("Week number : " +  calendarWeek);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
