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
        private User user = new User("Manager", false, false, false);
        private App newApp = new App();
        internal Manager__Provider_catalog(User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();
            catalogLoad();
            //cartLoad();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager_MainHome mmh = new Manager_MainHome(user);// maybe send the userType with it
            mmh.FormClosed += (s, args) => this.Close(); // close the login Form
            mmh.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void catalogLoad()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            Dictionary<int, int> resultWeek = newApp.getWeekPieces("Week : 13");
            Dictionary<int, int> resultCompute = newApp.computeMissingPieces(resultWeek);
            foreach(var elem in resultWeek)
            {
                Console.WriteLine("Key " +elem.Key +" Value : "+ elem.Value);
            }
            foreach (var elem in resultCompute)
            {
                Console.WriteLine("Key " + elem.Key + " Value : " + elem.Value);
            }
            foreach (var part in newApp.bikePartList)
            {
                dataGridView1.Rows.Add();
                dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGridView1.Rows[i].Cells[0].Value = part.part_Id;
                dataGridView1.Rows[i].Cells[1].Value = part.name;
                dataGridView1.Rows[i].Cells[2].Value = part.price;
                dataGridView1.Rows[i].Cells[3].Value = part.provider;
                dataGridView1.Rows[i].Cells[4].Value = newApp.getQuantity(part.part_Id);
                dataGridView1.Rows[i].Cells[5].Value = 0;
                foreach (var elem in resultCompute)
                {
                    
                    if (part.part_Id == elem.Key)
                    {
                        dataGridView1.Rows[i].Cells[5].Value = elem.Value;
                    }
                    /*else
                    {
                        dataGridView1.Rows[i].Cells[5].Value = "-";
                    }*/

                }
                i++;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            Console.WriteLine(dataGridView1.CurrentCell.Value);
            if (dataGridView1.CurrentCell.Value== "Add")
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
            if (dataGridView2.CurrentCell.Value == "Remove")
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
                i++;
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide(); //hides the current form
            Manager__Provider_orders mpo = new Manager__Provider_orders(user);// maybe send the userType with it
            mpo.FormClosed += (s, args) => this.Close(); // close the login Form
            mpo.Show();
        }
    }// end of Manager__Provider_catalog : Form
}//end of namespace Bovelo
