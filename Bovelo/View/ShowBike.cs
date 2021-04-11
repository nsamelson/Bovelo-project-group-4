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
        public string path = @"../../View";
        //public string path = Directory.GetCurrentDirectory();// recup the position in repositories
        private User _currentUser;
        private App app = new App();
        //private Bike bikeToAdd;
        internal ShowBike(string TypeBike,ref User current_user)
        {
            TypeOfBike = TypeBike;
            _currentUser = current_user;
            app.SetBikeModelList();
            // bikeToAdd = new Bike(0, TypeBike, "black", 26);
            InitializeComponent();
            // display the first bykeview at the first time
            pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_profilv1.jpg");// assign to bykeimg an image 
            button5.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_profilicone.jpg");// assign to bykeimg an image
            button6.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_biaisicone.jpg");// assign to bykeimg an image
            button8.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_guidonicone.jpg");// assign to bykeimg an image
            button9.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_derailleuricone.jpg");// assign to bykeimg an image
            button10.Image = Image.FromFile(path + @".\Pictures\" + TypeOfBike + @"\" + TypeOfBike + "_roueicone.jpg");// assign to bykeimg an image
            numericUpDown1.Value = 1;
            isChecked();
        }

       public int getBikePrice()
        {
            int price = app.bikeModels.FirstOrDefault(x=> x.Type == TypeOfBike).Price;
            return price;
       }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            isChecked();
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
            var all_models = new List<string>();

            foreach(var model in app.bikeModels)
            {
                if (!all_models.Contains(model.Type))
                {
                    all_models.Add(model.Type);
                }
            }
            comboBox2.DataSource = all_models;
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
            numericUpDown1.Minimum = 1;
            Decimal B = this.numericUpDown1.Value * getBikePrice();
            this.label6.Text = B.ToString();
            isChecked();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string i = comboBox1.Text;
            string color = " ";
            if (radioButton1.Checked)
            {
                color = radioButton1.Text;
                
            }
            else if (radioButton2.Checked)
            {
                color = radioButton2.Text;
                
            }
            else if (radioButton3.Checked)
            {
                color = radioButton3.Text;
                
            }
            
            int _i = Convert.ToInt32(i);

            var _model = app.bikeModels.FirstOrDefault(x => x.Color == color && x.Size == _i && x.Type == TypeOfBike);

            Bike BikeToAdd = new Bike(0, _model);//id is set to 0 MAYBE NEED TO CHANGE

            bool isInCart = false;
            foreach (var elem in _currentUser.cart)
            {
                if (elem.bike.Type == BikeToAdd.Type && elem.bike.Color == BikeToAdd.Color && elem.bike.Size == BikeToAdd.Size)
                {
                    Console.WriteLine("Already in cart");
                    isInCart = true;
                }
            }
            if (!isInCart)
            {
                _currentUser.addToCart(BikeToAdd, Convert.ToInt32(numericUpDown1.Value));
                MessageBox.Show("Added to cart");
            }       
            
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //button4.Enabled = true;
            isChecked();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //button4.Enabled = true;
            isChecked();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //button4.Enabled = true;
            isChecked();
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void isChecked()
        {            
            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked)
            {
                if (numericUpDown1 != null && comboBox1.SelectedItem != null)
                {                 
                    button4.Enabled = true;                    
                }
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
