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
    public partial class Manager__Provider_orders : Form
    {
        private User user = new User("Manager", false, false, false);
        private App newApp = new App();//because i need part name
        private List<string> currentOrder = new List<string>();
        private List<List<string>> updatedSatusts = new List<List<string>>();


        internal Manager__Provider_orders(User currentUser)
        {
            currentOrder.Add("0");
            this.user = currentUser;
            InitializeComponent();
            newApp.SetBikePartList();
            orderLoad();
            var idOrder = DataBase.GetFromDBSelect("Order_Part", new List<string> { "id_Order_Part" });
            int i = 0;
            foreach (var value in idOrder)
            {
                foreach (var elem in value)
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[i].Cells[0].Value = elem;
                    i++;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager_MainHome mmh = new Manager_MainHome(user);// maybe send the userType with it
            mmh.FormClosed += (s, args) => this.Close(); // close the login Form
            mmh.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.Value.ToString() == "Remove")
            {
                string query = "DELETE FROM Order_Detailed_Part WHERE Id_Order=" + dataGridView1.Rows[e.RowIndex].Cells[0].Value + " AND idOrder_Detailed_Part=" + dataGridView1.Rows[e.RowIndex].Cells[1].Value + " AND Id_Bike_Parts =" + dataGridView1.Rows[e.RowIndex].Cells[2].Value + " AND Quantity=" + dataGridView1.Rows[e.RowIndex].Cells[4].Value + ";";
                Console.WriteLine(query);
                DataBase.SendToDB(query);
                orderLoad();
                MessageBox.Show(@"Deleted :" +
                    "\nname = " + dataGridView1.Rows[e.RowIndex].Cells[3].Value+
                    "\nId_Order= " + dataGridView1.Rows[e.RowIndex].Cells[0].Value +
                    "\nId_Order_Detailed_Part = " + dataGridView1.Rows[e.RowIndex].Cells[1].Value +
                    "\nId_Bike_Parts = " + dataGridView1.Rows[e.RowIndex].Cells[2].Value +
                    "\nQuantity = " + dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                    
            }

            if (dataGridView1.CurrentCell.Value.ToString() == "Received")
            {
                List<string> updatedLine = new List<string>();
                if (dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString() == "Not Received")
                {
                    dataGridView1.Rows[e.RowIndex].Cells[7].Value = "Received";
                    updatedLine.Add(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    updatedLine.Add(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    updatedLine.Add(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
                    updatedLine.Add(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    updatedSatusts.Add(updatedLine);
                    BikePart.AddReceivedBikePart(Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString()), Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()));
                    MessageBox.Show(@"Changed State :" +
                        "\nname = " + dataGridView1.Rows[e.RowIndex].Cells[3].Value +
                        "\nId_Order= " + dataGridView1.Rows[e.RowIndex].Cells[0].Value +
                        "\nId_Order_Detailed_Part = " + dataGridView1.Rows[e.RowIndex].Cells[1].Value +
                        "\nId_Bike_Parts = " + dataGridView1.Rows[e.RowIndex].Cells[2].Value +
                        "\nQuantity = " + dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                    DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                    dataGridView1.Rows[e.RowIndex].Cells[8] = cell;
                    cell.ReadOnly = true;
                    dataGridView1.Refresh();
                }
                
            }
        }
        

        private void orderLoad()
        {
            int i = 0;
            List<string> all = new List<string>();
            all.Add("*");
            string whereCondiction = "Id_Order = ";
            foreach(var elem in this.currentOrder)
            {
                if (elem != this.currentOrder.Last())
                    whereCondiction += elem + " OR Id_Order = ";
                else
                {
                    whereCondiction += elem;
                }
            }
            //Console.WriteLine(whereCondiction);
            var orderPartDetails = DataBase.GetFromDBWhere("Order_Detailed_Part",all, whereCondiction);
            dataGridView1.Rows.Clear();
            foreach(var elem in orderPartDetails)
            {
                dataGridView1.Rows.Add();
                //Console.WriteLine(elem);
                dataGridView1.Rows[i].Cells[0].Value = orderPartDetails[i][1];
                dataGridView1.Rows[i].Cells[1].Value = orderPartDetails[i][0];
                dataGridView1.Rows[i].Cells[2].Value = orderPartDetails[i][2];
                
                foreach (var value in newApp.bikePartList)
                {
                    if (value.part_Id == Int32.Parse(orderPartDetails[i][2]))
                    {
                        dataGridView1.Rows[i].Cells[3].Value = value.name.ToString();
                    }
                }
                var orderPart = DataBase.GetFromDBWhere("Order_Part", all, "id_Order_Part =" + orderPartDetails[i][1]);
                dataGridView1.Rows[i].Cells[4].Value = orderPartDetails[i][3];
                dataGridView1.Rows[i].Cells[5].Value = orderPartDetails[i][4];
                dataGridView1.Rows[i].Cells[6].Value = orderPart[0][3];
                dataGridView1.Rows[i].Cells[7].Value = orderPartDetails[i][5];
                if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "Received")
                {
                    DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                    dataGridView1.Rows[i].Cells[8] = cell;
                    cell.ReadOnly = true;
                }
                
                i++;
                
                
            }
            /*List <List<string>> result = DataBase.GetFromDB("Order_Detailed_Part");
            List<List<string>> result2 = DataBase.GetFromDB("Order_Part");
            int currentOrder = Int32.Parse(result[i][1]);
            int previousOrder = 0;
            dataGridView1.Rows.Clear();          
            foreach (var elem in result)
            {
                dataGridView1.Rows.Add();
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                currentOrder = Int32.Parse(result[i][1]);
                if (currentOrder != previousOrder)
                {
                    result2 = DataBase.GetFromDBWhere("Order_Part", all, "id_Order_Part=" + currentOrder);
                    previousOrder = currentOrder;
                }
                dataGridView1.Rows[i].Cells[0].Value = result[i][1];
                dataGridView1.Rows[i].Cells[1].Value = result[i][0];
                dataGridView1.Rows[i].Cells[2].Value = result[i][2];
                
                foreach(var value in newApp.bikePartList)
                {
                    if (value.part_Id == Int32.Parse(result[i][2]))
                    {
                        dataGridView1.Rows[i].Cells[3].Value = value.name.ToString();
                    }
                }
                dataGridView1.Rows[i].Cells[4].Value = result[i][3];
                dataGridView1.Rows[i].Cells[5].Value = result[i][4];
                dataGridView1.Rows[i].Cells[6].Value = result2[0][3];
                dataGridView1.Rows[i].Cells[7].Value = result[i][5];
                //dataGridView1.Rows[i].Cells[4].Value = newApp.getQuantity(elem.part.part_Id);
                i++;
            }*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentOrder.Clear();
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Choose an Order_ID from the table below");
            }
            else
            {
                string order = textBox1.Text.ToString();
                //Console.WriteLine(Order);
                currentOrder.Add(textBox1.Text.ToString());

                //Console.WriteLine(currentOrder[1]);
                try
                {
                    orderLoad();
                }
                catch
                {
                    MessageBox.Show("Must have ID Order");
                }
                textBox1.Clear();
            }
        
            
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            currentOrder.Clear();
            currentOrder.Add("0");
            orderLoad();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Manager__Provider_orders_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manager.updateOrderStatus(updatedSatusts);
            orderLoad();
        }
    }
}
