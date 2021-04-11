﻿using System;
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
        internal MainHome(User incomingUser)
        {
            InitializeComponent();
            _currentUser = incomingUser;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // to complete
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // to complete
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // to complete
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the MainHome Window
            Cart cart = new Cart(ref _currentUser);// create new window
            cart.FormClosed += (s, args) => this.Close();
            cart.Show();// Showing the Cart window
            
        }


        private void button_Click(object sender, EventArgs e)
        {
            /*//Button Bt = new Button();
            Button Bt = (Button)sender;
            Bt.Tag = "null";*/

            /*switch (Convert.ToString(Bt.Tag)) //Needs to be linked with database
            {
                case "City":
                    this.Hide();
                    ShowBike citybike = new ShowBike("City", ref _currentUser);
                    citybike.FormClosed += (s, args) => this.Close();
                    citybike.Show();
                    break;
                case "Adventure":
                    this.Hide();
                    ShowBike adventure = new ShowBike("Adventure", ref _currentUser);
                    adventure.FormClosed += (s, args) => this.Close();
                    adventure.Show();
                    break;
                case "Explorer":
                    this.Hide();
                    ShowBike explorerbike = new ShowBike("Explorer", ref _currentUser);
                    explorerbike.FormClosed += (s, args) => this.Close();
                    explorerbike.Show();
                    break;
                case "Electric":
                    //to implement
                    break;
                default:
                    //Bt.Enabled = false;
                    break;
            }*/
        }

        private void button6_Click(object sender, EventArgs e)
        {

            this.Hide();// Hiding the MainHome Window
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the MainHome Window
        }

        private void button4_Click(object sender, EventArgs e)
        {

            this.Hide();// Hiding the MainHome Window
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the Explorer Bike Window
            Order order = new Order(ref _currentUser);// create new window
            order.FormClosed += (s, args) => this.Close();
            order.Show();// Showing the Order window
            
        }

        private void MainHome_Load(object sender, EventArgs e)
        {

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

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Showbike_other sbo = new Showbike_other(_currentUser);// create new window
            sbo.FormClosed += (s, args) => this.Close();
            sbo.Show();// Showing the Login window
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            ShowBike explorerbike = new ShowBike("Explorer", ref _currentUser);
            explorerbike.FormClosed += (s, args) => this.Close();
            explorerbike.Show();
        }
    }
}
