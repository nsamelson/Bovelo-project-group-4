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
    public partial class Explorerbike : Form
    {
        public Explorerbike()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button5_Click(object sender, EventArgs e)
        {
            label3.ImageIndex = 0;// affiche l'image 0
        }


        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            label3.ImageIndex = 1;// affiche l'image 1
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label3.ImageIndex = 2;// affiche l'image 2
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label3.ImageIndex = 3;// affiche l'image 3
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label3.ImageIndex = 4;// affiche l'image 4
        }

        private void Explorerbike_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            MainHome mh = new MainHome();// create new window
            mh.Show();// Showing the Main Home window
            this.Hide();// Hiding the Explorerbike Window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart();// create new window
            cart.Show();// Showing the Cart window
            this.Hide();// Hiding the Explorerbike Window
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Decimal B = this.numericUpDown1.Value * 800;
            this.label6.Text = B.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            Cart cart = new Cart();// create new window
            cart.row = new string[] { "Explorer", comboBox1.Text.ToString(), label8.Text.ToString(), numericUpDown1.Value.ToString(), "2 jours" };
            cart.Show();// Showing the Main Home window
            this.Hide();// Hiding the Explorerbike Window
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            ColorDialog color =  new ColorDialog();
            color.ShowDialog();
            textBox1.BackColor = color.Color;
            label8.Text = color.Color.Name;

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
