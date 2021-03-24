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
            newApp.planningList = newApp.getPlanningList();
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

            
            string Builder, status;
            int id;
            if (dataGridView1.CurrentCell.Value.ToString() == "set on active")
            {
                dataGridView1.Rows[e.RowIndex].Cells[5].Value = "Active";
                DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                cell.Value = DateTime.Now.DayOfWeek + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
                dataGridView1.Rows[e.RowIndex].Cells[8] = cell;
                cell.ReadOnly = true;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {

                    id = Int32.Parse(row.Cells[0].Value.ToString());
                    Builder = user.login.ToString();
                    status = row.Cells[5].Value.ToString();

                    row.Cells[7].Value = Builder;
                    newApp.updateSatus(id, status, Builder);
                }
                dataGridView1.Refresh();
                newApp.planningList = newApp.getPlanningList();
            }
            else if (dataGridView1.CurrentCell.Value.ToString() == "set on closed")
            {
                dataGridView1.Rows[e.RowIndex].Cells[5].Value = "Closed";
                DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                cell.Value = DateTime.Now.DayOfWeek + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
                dataGridView1.Rows[e.RowIndex].Cells[9] = cell;
                cell.ReadOnly = true;
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {

                    id = Int32.Parse(row.Cells[0].Value.ToString());
                    Builder = user.login.ToString();
                    status = row.Cells[5].Value.ToString();

                    row.Cells[7].Value = Builder;
                    newApp.updateSatus(id, status, Builder);
                }
                dataGridView1.Refresh();
                newApp.planningList = newApp.getPlanningList();
            }
            else if(dataGridView1.CurrentCell.Value.ToString() == "Click to see parts")
            {
                string partsToShow = dataGridView1.Rows[e.RowIndex].Cells[4].ToolTipText.ToString();
                MessageBox.Show(partsToShow);
            }

                

        }
        public void assembler_Planning_Load(object sender, EventArgs e)
        {


        }

        private void Assembler_Planning_Load_1(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            var plans = newApp.planningList.Select(x => x.weekName).ToList();
            comboBox1.DataSource = plans;
            //comboBox1.Items = test;

            /*List<string> where_conditions = new List<string>();
            List<string> Type_Size_Color = new List<string>();
            int i = 0;
            foreach (var planning in newApp.getPlanningList())
            { 
                if (planningWeek == planning.weekName)
                {
                    foreach (var bike in planning.planningDetails)
                    {        
                        where_conditions.Add("Bike_Type");
                        where_conditions.Add("Bike_Size");
                        where_conditions.Add("Bike_Color");
                        List<List<string>> bikeModel =  newApp.getFromDBWhere("Order_Details", where_conditions, "Id_Order_Details ='" + bike[1] +"'");

                        foreach(var elem in bikeModel)
                        {
                            foreach(var value in elem)
                            {
                                Type_Size_Color.Add(value);
                            }
                        }
                        List<BikePart> bikePartList = newApp.getBikePart(Type_Size_Color);
                        string toPrint = "";
                        int time=0;
                        foreach(var bikePart in bikePartList)
                        {
                            toPrint += bikePart.part_Id + " | " + bikePart.name + " | " + bikePart.location + " | \n";
                            time += bikePart.timeToBuild;
                            int result = bikePart.getQuantity();
                            Console.WriteLine(result);
                        }
                        //Console.WriteLine(Type_Size_Color[0]);
                        dataGridView1.Rows.Add();
                        dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dataGridView1.Rows[i].Cells[0].Value = bike[0].ToString();
                        dataGridView1.Rows[i].Cells[1].Value = Type_Size_Color[0];
                        dataGridView1.Rows[i].Cells[2].Value = Type_Size_Color[2];
                        dataGridView1.Rows[i].Cells[3].Value = Type_Size_Color[1]; // must be order date and date add to planning
                        dataGridView1.Rows[i].Cells[4].Value = toPrint;
                        dataGridView1.Rows[i].Cells[5].Value = bike[3].ToString();
                        dataGridView1.Rows[i].Cells[6].Value = time.ToString();
                        i++;
                    }
                }            
            }*/

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Assembler_MainHome amh = new Assembler_MainHome(user);// create new window
            amh.FormClosed += (s, args) => this.Close();
            amh.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //string Builder, status;
            //int id;
            //foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            //{
            //    id = Int32.Parse(row.Cells[0].Value.ToString());
            //    Builder = user.login.ToString();
            //    status = row.Cells[5].Value.ToString();

            //    row.Cells[7].Value = Builder;
            //    newApp.updateSatus(id, status, Builder);
            //}
            //dataGridView1.Refresh();
            //newApp.planningList = newApp.getPlanningList();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //newApp.planningList = newApp.getPlanningList();
            dataGridView1.Rows.Clear();
            string week = comboBox1.Text;
            int i = 0;

            var planningWeek = newApp.planningList.FirstOrDefault(x => x.weekName == week);
            foreach (var bike in planningWeek.getBikesToBuild())
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
                i++;
            }
            /*foreach (var plan in newApp.getFromDbInnerJoin(week))
            {
                Console.WriteLine(plan[0] + "|" + plan[1] + "|" + plan[2] + "|" + plan[3] + "" + plan[8]);

                dataGridView1.Rows.Add();
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.Rows[i].Cells[0].Value = plan[0];
                dataGridView1.Rows[i].Cells[1].Value = plan[1];
                dataGridView1.Rows[i].Cells[2].Value = plan[2];
                dataGridView1.Rows[i].Cells[3].Value = plan[3]; // must be order date and date add to planning
                //dataGridView1.Rows[i].Cells[4].Value = plan[5];
                dataGridView1.Rows[i].Cells[5].Value = plan[5];
                dataGridView1.Rows[i].Cells[6].Value = plan[8];
                dataGridView1.Rows[i].Cells[7].Value = plan[6];
                i++;
            }*/
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
