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
        private string typeOfBike;
        private string path = @"../../View";
        private string broken_path = @"../../View\Pictures\" + "broken.png";
        private User _currentUser;
        private App app = new App();

        internal ShowBike(string typeBike,ref User current_user)
        {
            InitializeComponent();

            typeOfBike = typeBike;
            _currentUser = current_user;

            app.SetBikeModelList();
            comboBox2.DataSource = app.GetDifferentModels(); //gets a list of bikeModels
            try
            {
                comboBox2.SelectedItem = typeBike;
            }
            catch{ } ;
            numericUpDown1.Value = 1;
            label6.Text = getBikePrice().ToString() +" €";
            isChecked();
        }

       public int getBikePrice()
       {
            int price = app.bikeModels.FirstOrDefault(x=> x.Type == typeOfBike).Price;
            return price;
       }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            isChecked();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + typeOfBike + @"\" + typeOfBike + "_profilv1.jpg");// assign to bykeimg an image and displaying into picturebox
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(broken_path);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + typeOfBike + @"\" + typeOfBike + "_biaisv1.jpg");// assign to bykeimg an image and displaying into picturebox
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(broken_path);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + typeOfBike + @"\" + typeOfBike + "_guidonv1.jpg");// assign to bykeimg an image and displaying into picturebox
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(broken_path);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + typeOfBike + @"\" + typeOfBike + "_derailleurv1.jpg");// assign to bykeimg an image and displaying into picturebox
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(broken_path);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + typeOfBike + @"\" + typeOfBike + "_rouev1.jpg");// assign to bykeimg an image and displaying into picturebox
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(broken_path);
            }
        }

        private void button7_Click(object sender, EventArgs e)// go to mainhome
        {
            this.Hide();
            var mainHome = new MainHome(_currentUser);// create new window
            mainHome.FormClosed += (s, args) => this.Close();
            mainHome.Show();// Showing the Login window
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {            
            numericUpDown1.Minimum = 1;
            Decimal b = this.numericUpDown1.Value * getBikePrice();
            this.label6.Text = b.ToString() + "€";
            isChecked();
        }

        private void button4_Click(object sender, EventArgs e)//add to cart
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
            var _model = app.bikeModels.FirstOrDefault(x => x.Color == color && x.Size == _i && x.Type == typeOfBike);

            /// exception if there is no model of this bike (bike wanted by the representative) in database 
            var existing_bike_in_db = false;
            foreach (var elem2 in app.GetBikeModelList())
            {
                Console.WriteLine("type = " + elem2.Type+", size = "+ elem2.Size+", color "+elem2.Color);
                if(elem2.Color == color && elem2.Size == _i && elem2.Type == typeOfBike)
                {
                    existing_bike_in_db = true;
                }
            }
            Console.WriteLine(existing_bike_in_db);

            if (existing_bike_in_db)// if bike model exist in db 
            {
                Bike bikeToAdd = new Bike(0, _model);//id is set to 0 MAYBE NEED TO CHANGE

                bool isInCart = false;
                foreach (var elem in _currentUser.cart)
                {
                    if (elem.bike.Type == bikeToAdd.Type && elem.bike.Color == bikeToAdd.Color && elem.bike.Size == bikeToAdd.Size)
                    {
                        Console.WriteLine("Already in cart");
                        MessageBox.Show("Already in cart");
                        isInCart = true;
                    }
                }
                if (!isInCart)
                {
                    _currentUser.addToCart(bikeToAdd, Convert.ToInt32(numericUpDown1.Value));
                    MessageBox.Show("Added to cart");
                }
            }
            else
            {
                MessageBox.Show("This model don't exist , please contact the manager ");
            }
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

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            typeOfBike = comboBox2.SelectedValue.ToString();
            numericUpDown1_ValueChanged( sender,  e);//load the correct price
            Load_Pictures();
        }

        private void Load_Pictures()
        {
            
            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))+@"\View\Pictures";
            string new_path = wanted_path + @"\" + typeOfBike;
            string message = "The program could not find pictures of : '" + typeOfBike + "' in the directory : " + wanted_path;
            try
            {
                pictureBox1.Image = Image.FromFile(path + @".\Pictures\" + typeOfBike + @"\" + typeOfBike + "_profilv1.jpg");// assign to bykeimg an image       
            }
            catch
            {
                pictureBox1.Image = Image.FromFile(broken_path);
                open_directory(message, new_path);
            }
            try
            {
                button5.Image = Image.FromFile(path + @".\Pictures\" + typeOfBike + @"\" + typeOfBike + "_profilicone.jpg");// assign to bykeimg an image
                button6.Image = Image.FromFile(path + @".\Pictures\" + typeOfBike + @"\" + typeOfBike + "_biaisicone.jpg");// assign to bykeimg an image
                button8.Image = Image.FromFile(path + @".\Pictures\" + typeOfBike + @"\" + typeOfBike + "_guidonicone.jpg");// assign to bykeimg an image
                button9.Image = Image.FromFile(path + @".\Pictures\" + typeOfBike + @"\" + typeOfBike + "_derailleuricone.jpg");// assign to bykeimg an image
                button10.Image = Image.FromFile(path + @".\Pictures\" + typeOfBike + @"\" + typeOfBike + "_roueicone.jpg");// assign to bykeimg an image
            }
            catch
            {
                button5.Image = Image.FromFile(broken_path);
                button6.Image = Image.FromFile(broken_path);
                button8.Image = Image.FromFile(broken_path);
                button9.Image = Image.FromFile(broken_path);
                button10.Image = Image.FromFile(broken_path);
                open_directory(message, new_path);
            }
        }
        private void open_directory(string message,string new_path)
        {
            if (_currentUser.userType["Production Manager"] == true)
            {
                var result = MessageBox.Show(this.Owner, message + "\n \n\n Would you like to add pictures?", "No image found!", MessageBoxButtons.YesNo);
                switch (result)
                {
                    case DialogResult.No:
                        break;
                    case DialogResult.Yes:
                        Manager.AddNewFolder(new_path);
                        System.Diagnostics.Process.Start(new_path);
                        break;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the MainHome Window
            Cart cart = new Cart(ref _currentUser);// create new window
            cart.FormClosed += (s, args) => this.Close();
            cart.Show();// Showing the Cart window
        }
    }
}
