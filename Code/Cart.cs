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
    public partial class Cart : Form
    {
        
        public string[] row;
        public Cart()
        {
            InitializeComponent();
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(row);


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            //MainHome mh = new MainHome();// create new window
            /*mh.Show();// Showing the Main Home window
            this.Hide();// Hiding the Explorerbike Window*/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart();// create new window
            cart.Show();// Showing the Cart window
            this.Hide();// Hiding the MainHome Window
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i;
            OrderBike o = new OrderBike();
            dataGridView1.Rows[0].Cells[0].Value.ToString();
            o.maListe.Add(dataGridView1.Rows[0].Cells[0].Value.ToString());
            o.maListe.Add(dataGridView1.Rows[0].Cells[1].Value.ToString());
            o.maListe.Add(dataGridView1.Rows[0].Cells[2].Value.ToString());
            o.maListe.Add(dataGridView1.Rows[0].Cells[3].Value.ToString());
            o.maListe.Add(dataGridView1.Rows[0].Cells[4].Value.ToString());
            for (i = 0; i <= o.maListe.Count-1 ; i++)
            {
                label2.Text += o.maListe[i] + "/";
            }
            
            o.addOrderBike();

            //for (int i = 0; i <= dataGridView1.Rows.Count; i++)
            //{
            //label2.Text += dataGridView1.Rows[i].Cells[0].Value.ToString();

            //}
            //o.BikeType = dataGridView1.Rows[0].Cells[0].Value.ToString();
            //o.BikeSize = dataGridView1.Rows[0].Cells[1].Value.ToString();
            //o.BikeColor = dataGridView1.Rows[0].Cells[2].Value.ToString();
            //o.Quantity = int.Parse(dataGridView1.Rows[0].Cells[3].ToString());
            //o.DateTime = dataGridView1.Rows[0].Cells[4].Value.ToString();



            label3.Text = o.maListe[0];


        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
