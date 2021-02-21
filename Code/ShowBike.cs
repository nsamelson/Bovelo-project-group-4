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
    public partial class ShowBike : Form
    {
        public string TypeOfBike;
        private User _currentUser=new User(" ", " ");
        internal ShowBike(string TypeBike, User current_user)
        {
            TypeOfBike = TypeBike;
            User _currentUser = current_user; 
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
            /*MainHome mh = new MainHome();// create new window
            mh.Show();// Showing the Main Home window
            this.Hide();// Hiding the Explorerbike Window*/
            this.Hide();
            var MainHome = new MainHome(_currentUser);// create new window
            MainHome.FormClosed += (s, args) => this.Close();
            MainHome.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Cart cart = new Cart(_currentUser);// create new window
            cart.Show() ;// Showing the Cart window
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
            string i = comboBox1.Text;
            int _i = Convert.ToInt32(i);
            Bike BikeToAdd = new Bike(TypeOfBike, label8.Text.ToString(), _i);

            _currentUser.addToCart(BikeToAdd, Convert.ToInt32(numericUpDown1.Value));

            foreach (Item elem in _currentUser.cart)
            {
                Console.WriteLine(elem.bike.Type + " " + elem.quantity);
            }

            //Cart cart = new Cart();// create new window
            //cart.row = new string[] { panel1.Name.ToString(), comboBox1.Text.ToString(), label8.Text.ToString(), numericUpDown1.Value.ToString(), DateTime.Now.ToString() };
            //cart.Show();// Showing the Main Home window

            //this.Hide();// Hiding the Explorerbike Window
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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
