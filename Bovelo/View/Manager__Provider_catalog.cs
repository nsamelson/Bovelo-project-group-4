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
    public partial class Manager__Provider_catalog : Form
    {
        private User user;
        private App newApp = new App();
        private string plannedWeek;
        private int cartPrice;
        internal Manager__Provider_catalog(ref User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();

            newApp.SetPlanningList();
            newApp.SetBikePartList();
            var plans = newApp.planningList.Select(x => x.weekName).ToList();
            comboBox1.DataSource = plans;
            plannedWeek = comboBox1.Text;
            //cartLoad();
        }


        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager_MainHome mmh = new Manager_MainHome(user);// maybe send the userType with it
            mmh.FormClosed += (s, args) => this.Close(); // close the login Form
            mmh.Show();
        }


        private void catalogLoad()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            Dictionary<int, int> resultWeek = newApp.GetWeekPieces(this.plannedWeek);
            Dictionary<int, int> resultCompute = newApp.ComputeMissingPieces(ref resultWeek);
            /*foreach(var elem in resultWeek)
            {
                Console.WriteLine("Key " +elem.Key +" Value : "+ elem.Value);
            }
            foreach (var elem in resultCompute)
            {
                Console.WriteLine("Key " + elem.Key + " Value : " + elem.Value);
            }*/
            foreach (var part in newApp.bikePartList)
            {
                dataGridView1.Rows.Add();
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.Rows[i].Cells[0].Value = part.part_Id;
                dataGridView1.Rows[i].Cells[1].Value = part.name;
                dataGridView1.Rows[i].Cells[2].Value = part.price;
                dataGridView1.Rows[i].Cells[3].Value = part.provider;
                //dataGridView1.Rows[i].Cells[4].Value = newApp.getQuantity(part.part_Id);
                dataGridView1.Rows[i].Cells[4].Value = part.quantity;
                dataGridView1.Rows[i].Cells[5].Value = 0;
                int qty = resultCompute.FirstOrDefault(x => x.Key == part.part_Id).Value;
                dataGridView1.Rows[i].Cells[5].Value = qty;
                /*foreach (var elem in resultCompute)
                {
                    
                    if (part.part_Id == elem.Key)
                    {
                        dataGridView1.Rows[i].Cells[5].Value = elem.Value;
                    }
                    *//*else
                    {
                        dataGridView1.Rows[i].Cells[5].Value = "-";
                    }*//*

                }*/
                i++;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            Console.WriteLine(dataGridView1.CurrentCell.Value);
            if (dataGridView1.CurrentCell.Value.ToString()== "Add")
            {

                foreach (var elem in newApp.bikePartList)
                {
                    if (elem.part_Id == Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()))
                    {
                        ItemPart toAdd = new ItemPart(elem, 1);
                        user.cartPart.Add(toAdd);
                        Console.WriteLine("-------------------------------------");
                        Console.WriteLine("Part Id : " + elem.part_Id);
                    }
                }
                cartLoad();
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell.Value.ToString() == "Remove")
            {

                foreach (var elem in user.cartPart)
                {
                    if (elem.part.part_Id == Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString()))
                    {
                        user.cartPart.Remove(elem);                      
                        break;
                    }
                }
                cartLoad();
            }

        }
        public void cartLoad()
        {
            int i = 0;
            cartPrice = 0;
            dataGridView2.Rows.Clear();
            foreach (var elem in user.cartPart)
            {
                dataGridView2.Rows.Add();
                dataGridView2.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView2.Rows[i].Cells[0].Value = elem.part.part_Id;
                dataGridView2.Rows[i].Cells[1].Value = elem.part.name;
                dataGridView2.Rows[i].Cells[2].Value = elem.part.price;
                dataGridView2.Rows[i].Cells[3].Value = elem.part.provider;
                dataGridView2.Rows[i].Cells[4].Value = elem.quantity;
                dataGridView2.Rows[i].Cells[5].Value = elem.price;
                //dataGridView1.Rows[i].Cells[4].Value = newApp.getQuantity(elem.part.part_Id);
                cartPrice += elem.price;
                i++;
            }
            this.labelPrice.Text = cartPrice.ToString() +" € ";
        }      
        private void button2_Click(object sender, EventArgs e)
        { 
            for(int i = 0;i<user.cartPart.Count();i++ )
            {
                int value = Int32.Parse(dataGridView2.Rows[i].Cells[4].Value.ToString());
                user.cartPart[i].setQuantity(value);
            }      
            cartLoad();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            catalogLoad();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            user.cartPart.Clear();
            dataGridView2.Rows.Clear();
            Dictionary<int, int> resultWeek = newApp.GetWeekPieces(this.plannedWeek);
            Dictionary<int, int> resultCompute = newApp.ComputeMissingPieces(ref resultWeek);
            int i = 0;
            
            foreach (var elem in resultCompute)
            {
                var part = newApp.bikePartList.FirstOrDefault(x => x.part_Id == elem.Key);
                if (part != null)
                {
                    dataGridView1.Rows[i].Cells[5].Value = elem.Value;
                    ItemPart toAdd = new ItemPart(part, elem.Value);
                    user.cartPart.Add(toAdd);
                }
            }
            i++;
            
            cartLoad();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            plannedWeek = comboBox1.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            user.cartPart.Clear();
            cartLoad();
        }

        private void labelPrice_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            DateTime OrderTime = DateTime.Now;
            string query = "INSERT INTO Bovelo.Order_Part(Week_Name,Total_Price,Order_Date) VALUES('" + plannedWeek + "'," + cartPrice +",'"+ OrderTime.ToString() + "');";
            DataBase.SendToDB(query);
            List<string> all = new List<string>();
            all.Add("*");
            List<List<string>> result = DataBase.GetFromDBWhere("Order_Part", all, "Week_Name='" + plannedWeek + "' AND " + " Total_Price=" + cartPrice +" AND Order_Date= '"+OrderTime.ToString()+"'"); // récupérer l'id de la commande que l'on vient d'ajouter à  modifier ?
            Console.WriteLine(result);
            foreach (var elem in user.cartPart)
            {
                query = "INSERT INTO Bovelo.Order_Detailed_Part(Id_Order,Id_Bike_Parts,Quantity,Price,State) VALUES('" + result[0][0] +"'," + elem.part.part_Id +","+elem.quantity+ ","+elem.price+","+"'Not Received'"+");";
                DataBase.SendToDB(query);
            }
            printInvoice("Manager");
            user.cartPart.Clear();
            cartLoad();
        }

        private void printInvoice(string client)
        {
            List<string> column = new List<string>();
            column.Add("ID Part");
            column.Add("Name");
            column.Add("Provider");
            column.Add("Quantity");
            column.Add("Price");
            List<List<string>> selectedData = new List<List<string>>();
            foreach (var elem in user.cartPart)
            {
                List<string> row = new List<string>();
                row.Add(elem.part.part_Id.ToString());
                row.Add(elem.part.name);
                row.Add(elem.part.provider);
                row.Add(elem.quantity.ToString());
                row.Add(elem.price.ToString());
                selectedData.Add(row);
            }
            ExportData.CreateInvoice(client, column, selectedData);
        }
    }// end of Manager__Provider_catalog : Form
}//end of namespace Bovelo
