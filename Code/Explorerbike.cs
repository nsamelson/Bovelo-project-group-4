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
    }
}
