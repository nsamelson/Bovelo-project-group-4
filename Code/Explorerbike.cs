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

            // test Cart 
            // Create a list of parts.
            List<String> article = new List<String>();

            // Add parts to the list.
            article.Add("explorer");
            article.Add( "26" );
            article.Add( "Blue");
            
            

            // Write out the parts in the list. This will call the overridden ToString method
            // in the Part class.
            Console.WriteLine("test");
            foreach (String elem in article)
            {
                Console.WriteLine(elem);
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // size textbox
            
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

        private void button4_Click(object sender, EventArgs e)
        {
            // test 
            // add to cart 
            string size = comboBox1.Text; // recover size
            Console.WriteLine(size);
            decimal quantitydcm = numericUpDown1.Value; // recover quantity
            string quantity = quantitydcm.ToString(); // cast decimal to string
            Console.WriteLine(quantity);
            List<String> articlebp = new List<String>();
            articlebp.Add(size);
            articlebp.Add(quantity);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //quantity text box
        }
    }
}
