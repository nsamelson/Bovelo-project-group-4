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
    public partial class MainHome : Form
    {
        private User _currentUser;
        private App app = new App();
        private string path = @"../../View";
        internal MainHome(User incomingUser)
        {
            InitializeComponent();
            _currentUser = incomingUser;
        }

        private void button1_Click(object sender, EventArgs e)//go to login page
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)//go to cart page
        {
            this.Hide();// Hiding the MainHome Window
            Cart cart = new Cart(ref _currentUser);// create new window
            cart.FormClosed += (s, args) => this.Close();
            cart.Show();// Showing the Cart window
        }

        private void button3_Click(object sender, EventArgs e)//go to orders page
        {
            this.Hide();// Hiding the Explorer Bike Window
            Order order = new Order(ref _currentUser);// create new window
            order.FormClosed += (s, args) => this.Close();
            order.Show();// Showing the Order window
        }

        private void MainHome_Load(object sender, EventArgs e)//Load
        {
            var models = app.GetDifferentModels();
            int i = 0;
            if (_currentUser.userType["Production Manager"] == true)
            {
                button5.Visible = true;
            }
            else
            {
                button5.Visible = false;
            }
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
            buttonPanel.AutoScroll = true;
            buttonPanel.Size = new Size(this.Size.Width -200, this.Size.Height-150);
            buttonPanel.Location = new Point(200, 100);
            foreach(var model in models)
            {
                Button button = new Button();
                try
                {
                    button.BackgroundImage = Image.FromFile(path + @".\Pictures\" + model + @"\" + model + "_profilv1.jpg");
                    button.BackgroundImageLayout = ImageLayout.Zoom;
                }
                catch
                {
                    button.BackgroundImage = Image.FromFile(path + @".\Pictures\" + "broken.png");
                    button.BackgroundImageLayout = ImageLayout.Zoom;
                }
                button.Text = model;
                button.TextAlign = ContentAlignment.BottomCenter;
                button.Size = new Size(300, 200);
                //button.Location = new Point(300 + i*(button.Size.Width +10), 150);
                button.Click += new EventHandler(ButtonClickOneEvent);
                button.Tag = i;
                //this.Controls.Add(button);
                buttonPanel.Controls.Add(button);
                i++;
            }
            this.Controls.Add(buttonPanel);
        }

        void ButtonClickOneEvent(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                // now you know the button that was clicked
                this.Hide();
                try
                {
                    ShowBike explorerbike = new ShowBike(button.Text, ref _currentUser);
                    explorerbike.FormClosed += (s, args) => this.Close();
                    explorerbike.Show();
                }
                catch
                {
                    ShowBike explorerbike = new ShowBike("City", ref _currentUser);
                    explorerbike.FormClosed += (s, args) => this.Close();
                    explorerbike.Show();
                }
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            if (_currentUser.userType["Production Manager"] == true )
            {
                this.Hide();
                Manager_MainHome mmh = new Manager_MainHome(_currentUser);// create new window
                mmh.FormClosed += (s, args) => this.Close();
                mmh.Show();// Showing the Login window
            }
        }

        private void MainHome_ResizeEnd(Object sender, EventArgs e)
        {
            this.Controls.RemoveAt(this.Controls.Count - 1);
            MainHome_Load(sender, e);
        }
    }
}
