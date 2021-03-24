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
    public partial class Manager_Create_Model : Form
    {
        private User user;
        private App app = new App();
        internal Manager_Create_Model(User user)
        {
            this.user = user;
            InitializeComponent();
            app.updateBikeModelList();
            app.updateBikePartList();
        }

        private void button3_Click(object sender, EventArgs e)//orders
        {
            this.Hide(); //hides the current form
            Manager__Provider_orders mpo = new Manager__Provider_orders(user);// maybe send the userType with it
            mpo.FormClosed += (s, args) => this.Close(); // close the login Form
            mpo.Show();
        }

        private void button7_Click(object sender, EventArgs e)//menu
        {
            this.Hide(); //hides the current form
            Manager_MainHome mmh = new Manager_MainHome(user);// maybe send the userType with it
            mmh.FormClosed += (s, args) => this.Close(); // close the login Form
            mmh.Show();
        }

        private void button4_Click(object sender, EventArgs e)//catalog
        {
            this.Hide(); //hides the current form
            Manager__Provider_catalog mpc = new Manager__Provider_catalog(user);// maybe send the userType with it
            mpc.FormClosed += (s, args) => this.Close(); // close the login Form
            mpc.Show();
        }

        private void button1_Click(object sender, EventArgs e)//login
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//Create a model
        {
            string type = textBox1.Text;
            var colors = checkedListBox1.CheckedItems;
            var sizes = checkedListBox2.CheckedItems;
            if(colors.Count !=0 && sizes.Count != 0)
            {
                foreach(var color in colors)
                {
                    foreach (var size in sizes)
                    {
                        app.setNewBikeModel(type, Int32.Parse(size.ToString()), color.ToString());
                        //Console.WriteLine(type + " Size : "+Int32.Parse(size.ToString()) + " Color : " + color.ToString());
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)//create BikePart
        {
            string name = textBox2.Text;
            var colors = checkedListBox4.CheckedItems;
            var sizes = checkedListBox3.CheckedItems;
            int price;
            if (textBox3.Text != "")
            {
                price = Int32.Parse(textBox3.Text);
            }
            else
            {
                price = 0;
            }
            if (colors.Count != 0 && sizes.Count != 0)
            {
                foreach (var color in colors)
                {
                    foreach (var size in sizes)
                    {
                        app.setNewBikePart(name, price,Int32.Parse(size.ToString()), color.ToString());
                    }
                }
            }
            else if (colors.Count != 0 && sizes.Count == 0)
            {
                foreach (var color in colors)
                {
                    app.setNewBikePart(name,price, 0, color.ToString());
                }
            }
            else if (colors.Count == 0 && sizes.Count != 0)
            {
                foreach (var size in sizes)
                {
                    app.setNewBikePart(name, price,Int32.Parse(size.ToString()));
                }
            }
            else
            {
                app.setNewBikePart(name,price);
            }

        }

        private void button5_Click(object sender, EventArgs e)//Load bikeModels
        {
            dataGridView1.Rows.Clear();
            var models = app.bikeModels;
            int i= 0;
            List<int> idModels = new List<int>();
            foreach(var bike in models)
            {
                string parts = "";
                foreach (var part in bike.bikeParts)
                {
                    parts += part.part_Id + " | " + part.name + " | " + part.location + " \n";
                }
                int id = bike.idBikeModel;
                string type = bike.Type;
                string color = bike.Color;
                int size = bike.Size;
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = id;
                dataGridView1.Rows[i].Cells[1].Value = type;
                dataGridView1.Rows[i].Cells[2].Value = color;
                dataGridView1.Rows[i].Cells[3].Value = size;
                dataGridView1.Rows[i].Cells[4].ToolTipText = parts;
                dataGridView1.Rows[i].Cells[4].Value = "Click to see";
                i++;
                idModels.Add(id);
            }
            comboBox1.DataSource = idModels;
        }

        private void button6_Click(object sender, EventArgs e)//Link a model to a bike
        {
            List<int> partsToLink = new List<int>();
            int idModel;
            if (comboBox1.Text != "")
            {
                idModel = Int32.Parse(comboBox1.Text);

                foreach(DataGridViewRow row in dataGridView2.Rows)
                {
                    int qty = Int32.Parse(row.Cells[3].Value.ToString());
                    if (qty != 0)
                    {
                        for(int j = 0; j<qty; j++)
                        {
                            partsToLink.Add(Int32.Parse(row.Cells[0].Value.ToString()));
                        }
                        
                        row.Cells[3].Value = "0";
                    }
                }
                app.setLinkBikePartsToBikeModel(idModel, partsToLink);
            }
        }

        private void button9_Click(object sender, EventArgs e)//Load bikeParts
        {
            dataGridView2.Rows.Clear();
            app.updateBikePartList();
            var parts = app.bikePartList;
            int i = 0;
            foreach (var part in parts)
            {
                int id = part.part_Id;
                int price = part.price;
                string name = part.name;
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = id;
                dataGridView2.Rows[i].Cells[1].Value = name;
                dataGridView2.Rows[i].Cells[2].Value = price;
                dataGridView2.Rows[i].Cells[3].Value = "0";

                i++;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string partsToShow = dataGridView1.Rows[e.RowIndex].Cells[4].ToolTipText.ToString();
            MessageBox.Show(partsToShow);
        }
    }
}
