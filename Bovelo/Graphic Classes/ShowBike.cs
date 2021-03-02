using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Bovelo
{
    public partial class ShowBike : Form
    {
        public string TypeOfBike;
        public string path = @"../../Graphic Classes";
        //public string path = Directory.GetCurrentDirectory();// recup the position in repositories
        private User _currentUser= new User(" ", false, false, false);
        private App app = new App();
        internal ShowBike(string TypeBike,ref User current_user)
        {
            TypeOfBike = TypeBike;
            _currentUser = current_user; 
            InitializeComponent();
            // display the first bykeview at the first time
            pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_profilv1.jpg");// assign to bykeimg an image 
            button5.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_profilicone.jpg");// assign to bykeimg an image
            button6.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_biaisicone.jpg");// assign to bykeimg an image
            button8.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_guidonicone.jpg");// assign to bykeimg an image
            button9.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_derailleuricone.jpg");// assign to bykeimg an image
            button10.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_roueicone.jpg");// assign to bykeimg an image
        }

       public int getBikePrice()
        {
            int price = 0;
            foreach (var bike in app.bikeModel)
            {
                if (bike.Type == TypeOfBike)
                {
                    price = bike.Price;
                }
            }
            return price;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_profilv1.jpg");// assign to bykeimg an image and displaying into picturebox
        }


        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_biaisv1.jpg");// assign to bykeimg an image and displaying into picturebox
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_guidonv1.jpg");// assign to bykeimg an image and displaying into picturebox
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_derailleurv1.jpg");// assign to bykeimg an image and displaying into picturebox
        }

        private void button10_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_rouev1.jpg");// assign to bykeimg an image and displaying into picturebox
        }

        private void Explorerbike_Load(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label8.Text = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                label8.Text = radioButton2.Text;
            }
            else if (radioButton3.Checked)
            {
                label8.Text = radioButton1.Text;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            var MainHome = new MainHome(_currentUser);// create new window
            MainHome.FormClosed += (s, args) => this.Close();
            MainHome.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the MainHome Window
            Cart cart = new Cart(ref _currentUser);// create new window
            cart.FormClosed += (s, args) => this.Close();
            cart.Show();// Showing the Cart window
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
            Decimal B = this.numericUpDown1.Value * getBikePrice();
            this.label6.Text = B.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string i = comboBox1.Text;
            if (radioButton1.Checked)
            {
                label8.Text = radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                label8.Text = radioButton2.Text;
            }
            else if (radioButton3.Checked)
            {
                label8.Text = radioButton1.Text;
            }
            int _i = Convert.ToInt32(i);
            Bike BikeToAdd = new Bike(TypeOfBike, label8.Text.ToString(), _i, getBikePrice());
            _currentUser.addToCart(BikeToAdd, Convert.ToInt32(numericUpDown1.Value));






            //Cart cart = new Cart();// create new window
            //cart.row = new string[] { panel1.Name.ToString(), comboBox1.Text.ToString(), label8.Text.ToString(), numericUpDown1.Value.ToString(), DateTime.Now.ToString() };
            //cart.Show();// Showing the Main Home window

            //this.Close();// Hiding the Explorerbike Window
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {



        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the Explorer Bike Window
            Order order = new Order(ref _currentUser);// create new window
            order.FormClosed += (s, args) => this.Close();
            order.Show();// Showing the Order window
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
