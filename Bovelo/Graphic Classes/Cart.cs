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
    public partial class Cart : Form
    {
        
        public string[] row;
        private User _currentUser = new User(" ",false,false,false);

        private App app = new App();
        internal Cart(ref User incomingUser)
        {
            _currentUser = incomingUser;
            InitializeComponent();
        }

        private void Cart_Load(object sender, EventArgs e)
        {
            int totalPrice = 0;
            int i = 0;
            while (i< _currentUser.cart.Count)
            {
                dataGridView1.Rows.Add();
                i++;
            }
            i = 0;
            foreach (Item elem in _currentUser.cart)
            {
                int price = 0;
                
                foreach (var bike in app.bikeModels)
                {
                    if (bike.Type == elem.bike.Type)
                    {
                        price = bike.Price;
                    }
                }
                totalPrice += price * elem.quantity;
                Console.WriteLine(elem.bike.Type + " " + elem.quantity);
                dataGridView1.Rows[i].Cells[0].Value = elem.bike.Type;
                dataGridView1.Rows[i].Cells[1].Value = elem.bike.Size;
                dataGridView1.Rows[i].Cells[2].Value = elem.bike.Color;
                dataGridView1.Rows[i].Cells[3].Value = elem.quantity;
                dataGridView1.Rows[i].Cells[4].Value = elem.bike.TotalTime.ToString();
                dataGridView1.Rows[i].Cells[5].Value = (price * elem.quantity).ToString();
                Console.WriteLine(price);
                i++;
            }
            Decimal B = totalPrice;
            this.labelPrice.Text = B.ToString() + "€";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();// Hiding the Explorerbike Window
            var MainHome = new MainHome(_currentUser);// create new window
            MainHome.FormClosed += (s, args) => this.Close();
            MainHome.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string client = textBox1.Text;        
            int RowCount = _currentUser.getCartList().Count();
            /*List<List<string>> newOrder = new List<List<string>>();
            List<string> Data = new List<string>();
            int i = 0;
            int price = 0;
            int RowCount = dataGridView1.RowCount;
            while (i < RowCount )
            {

                Data.Insert(0, dataGridView1.Rows[i].Cells[0].Value.ToString());
                Data.Insert(1, dataGridView1.Rows[i].Cells[1].Value.ToString());
                Data.Insert(2, dataGridView1.Rows[i].Cells[2].Value.ToString());
                Data.Insert(3, dataGridView1.Rows[i].Cells[3].Value.ToString());
                Data.Insert(4, dataGridView1.Rows[i].Cells[5].Value.ToString());
                Console.WriteLine("Data list contains Bike type is : " + Data[0]);
                newOrder.Insert(i,Data);
                Console.WriteLine("la longeur de new order est de : "  + newOrder.Count + newOrder[i][0] + " indice i est  : " + i  );
                
                price += Int32.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                i++;
                Data = new List<string>();
                
            }*/

            if (client != ""&&RowCount!=0)
            {
                app.setNewOrderBike(_currentUser.getCartList(), client, _currentUser.getCartPrice());
                dataGridView1.Rows.Clear();
                _currentUser.emptyCart();
                this.labelPrice.Text = "0€";
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter a valid name or add items to the cart!");
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Order order = new Order(ref _currentUser);// create new window
            order.FormClosed += (s, args) => this.Close();
            order.Show();// Showing the Order window
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            _currentUser.emptyCart();
            this.labelPrice.Text = "0€";
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
