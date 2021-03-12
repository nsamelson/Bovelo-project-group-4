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
        private User user = new User(" ", false, false, false);
        private string planningWeek;
        internal Assembler_Planning(User user, string planning)
        {
            this.user = user;
            this.planningWeek = planning;
            InitializeComponent();
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
            
        }
        public void assembler_Planning_Load(object sender, EventArgs e)
        {
            
           
        }

        private void Assembler_Planning_Load_1(object sender, EventArgs e)
        {
            
            List<string> where_conditions = new List<string>();
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
                        foreach(var bikePart in bikePartList)
                        {
                            toPrint += bikePart.part_Id + " | " + bikePart.name + " | " + bikePart.location + " | \n";
                        }
                        Console.WriteLine(Type_Size_Color[0]);
                        dataGridView1.Rows.Add();
                        dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                        dataGridView1.Rows[i].Cells[0].Value = bike[0].ToString();
                        dataGridView1.Rows[i].Cells[1].Value = Type_Size_Color[0];
                        dataGridView1.Rows[i].Cells[2].Value = Type_Size_Color[2];
                        dataGridView1.Rows[i].Cells[3].Value = Type_Size_Color[1]; // must be order date and date add to planning
                        dataGridView1.Rows[i].Cells[4].Value = toPrint;
                        dataGridView1.Rows[i].Cells[5].Value = bike[3].ToString();
                        i++;
                    }
                }
                
                
                
            }
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

            foreach (var planning in newApp.getPlanningList())
            {
                if (planningWeek == planning.weekName)
                {
                    List<List<string>> refreshed = new List<List<string>>();
                    for(int i =0;i<planning.planningDetails.Count;i++)
                    {
                        List<string> rows = new List<string>();
                        for (int j=0;j< planning.planningDetails[0].Count;j++)
                        {
                            rows.Add(dataGridView1.Rows[i].Cells[j].Value.ToString());
                        }
                        refreshed.Add(rows);
                    }
                    planning.refreshBikes(refreshed);
                    newApp.updateSatus(refreshed);
                }

            }
            
        }
    }
}
