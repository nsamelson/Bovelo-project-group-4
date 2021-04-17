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
        private User user;
        internal Assembler_Planning(User user)
        {
            this.user = user;
            //this.planningWeek = planning; Was in constructor
            InitializeComponent();
            //newApp.planningList = newApp.getPlanningList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string builder, status, start, finish;
            int id;
            if (dataGridView1.CurrentCell.Value.ToString() == "set on active")
            {
                dataGridView1.Rows[e.RowIndex].Cells[5].Value = "Active";
                DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                cell.Value = DateTime.Now.Day.ToString()+"/"+DateTime.Now.Month.ToString()+"/"+DateTime.Now.Year.ToString() + " "+ DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
                dataGridView1.Rows[e.RowIndex].Cells[8] = cell;
                cell.ReadOnly = true;
                dataGridView1.Refresh();
            }

            else if (dataGridView1.CurrentCell.Value.ToString() == "set on close" &&
                dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString() == "Active")
            {
                dataGridView1.Rows[e.RowIndex].Cells[5].Value = "Closed";
                DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                cell.Value = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
                dataGridView1.Rows[e.RowIndex].Cells[9] = cell;
                cell.ReadOnly = true;
                DataGridViewTextBoxCell newCell = new DataGridViewTextBoxCell();
                newCell.Value = string.Empty;
                dataGridView1.Rows[e.RowIndex].Cells[10] = newCell;
                newCell.ReadOnly = true;
                BikePart.substractClosedBike(dataGridView1.Rows[e.RowIndex].Cells[4].ToolTipText.ToString());
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    id = Int32.Parse(row.Cells[0].Value.ToString());
                    builder = user.login.ToString();
                    status = row.Cells[5].Value.ToString();
                    start = row.Cells[8].Value.ToString();
                    finish = row.Cells[9].Value.ToString();
                    row.Cells[7].Value = builder;
                    Assembler.UpdateSatus(id, status, builder, start, finish);
                }
            }
            else if (dataGridView1.CurrentCell.Value.ToString() == "Reset on new" &&
                dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString() == "Active")
            {
                dataGridView1.Rows[e.RowIndex].Cells[5].Value = "New";
                DataGridViewButtonCell activeCell = new DataGridViewButtonCell();
                activeCell.UseColumnTextForButtonValue = true;
                activeCell.ToolTipText = "set on active";
                dataGridView1.Rows[e.RowIndex].Cells[8] = activeCell;
                
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    id = Int32.Parse(row.Cells[0].Value.ToString());
                    builder = string.Empty;
                    status = row.Cells[5].Value.ToString();
                    start = string.Empty;
                    finish = string.Empty;
                    row.Cells[7].Value = builder;
                    Assembler.UpdateSatus(id, status, builder, start, finish);
                }   
            }
            else if(dataGridView1.CurrentCell.Value.ToString() == "Click to see parts")
            {
                string partsToShow = dataGridView1.Rows[e.RowIndex].Cells[4].ToolTipText.ToString();
                MessageBox.Show(partsToShow);
            }
            newApp.SetPlanningList();
        }

        private void Assembler_Planning_Load_1(object sender, EventArgs e)//loading the page
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            newApp.SetPlanningList();
            var plans = newApp.planningList.Select(x => x.weekName).ToList();
            comboBox1.DataSource = plans; //shows the existing schedules
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Assembler_MainHome amh = new Assembler_MainHome(user);// create new window
            amh.FormClosed += (s, args) => this.Close();
            amh.Show();// Showing the Login window
        }

        private void button3_Click(object sender, EventArgs e)//get planning button
        {
            //newApp.planningList = newApp.getPlanningList();
            dataGridView1.Rows.Clear();
            string week = comboBox1.Text;
            int i = 0;
            string comp;
            var planningWeek = newApp.planningList.FirstOrDefault(x => x.weekName == week);
            foreach (var bike in planningWeek.GetBikesToBuild())
            {
                string parts = "";
                foreach (var part in bike.bikeParts)
                {
                    parts += part.part_Id + " | " + part.name + " | " + part.location + " \n";
                }
                dataGridView1.Rows.Add();
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.Rows[i].Cells[0].Value = bike.bikeId;
                dataGridView1.Rows[i].Cells[1].Value = bike.Type;
                dataGridView1.Rows[i].Cells[2].Value = bike.Size;
                dataGridView1.Rows[i].Cells[3].Value = bike.Color; // must be order date and date add to planning
                dataGridView1.Rows[i].Cells[4].Value = "Click to see parts";
                dataGridView1.Rows[i].Cells[4].ToolTipText = parts;
                dataGridView1.Rows[i].Cells[5].Value = bike.getCurrentState();
                dataGridView1.Rows[i].Cells[6].Value = week;
                dataGridView1.Rows[i].Cells[7].Value = bike.assembler;
                comp = dataGridView1.Rows[i].Cells[5].Value.ToString();
                if (comp == "Closed")
                {
                    Console.WriteLine("id : " + dataGridView1.Rows[i].Cells[0].Value);
                    DataGridViewTextBoxCell startCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell finishCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell newCell = new DataGridViewTextBoxCell();

                    newCell.Value = string.Empty;
                    /*startCell.Value = newApp.getPlanifiedBikesByWeekName(week)[i][10];
                    finishCell.Value = newApp.getPlanifiedBikesByWeekName(week)[i][11];*/
                    startCell.Value = bike.startBuildTime;
                    finishCell.Value = bike.endBuildTime;

                    //Console.WriteLine("finsihed at : " + newApp.getPlanifiedBikesByWeekName(week)[i][11] + "started at  : " + newApp.getPlanifiedBikesByWeekName(week)[i][10]);

                    dataGridView1.Rows[i].Cells[8] = startCell;
                    dataGridView1.Rows[i].Cells[9] = finishCell;
                    dataGridView1.Rows[i].Cells[10] = newCell;
                }
                //dataGridView1.Rows[i].Cells[7].Value = newApp.getPlanifiedBikesByWeekName(week)[i][9];
                i++;
            }
        }
    }
}
