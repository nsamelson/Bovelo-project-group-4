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
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MySql.Data.MySqlClient;



namespace Bovelo
{
    public partial class Cart : Form
    {
        
        public string[] row;
        private User _currentUser;

        private App app = new App();
        List<Bike> stockBike;
        private int _estimatedShippingWeek= 0;

        internal Cart(ref User incomingUser)
        {
            InitializeComponent();

            _currentUser = incomingUser;
            stockBike =Representative.GetBikesInStock();
            
        }
        private void Cart_Load() 
        {
            int i = 0;
            int quantity = 0;
            dataGridView1.Rows.Clear();//clear the grid
           
            foreach (ItemBike elem in _currentUser.cart)//for each item in cart
            {   
                var stock =  stockBike.FindAll(x=> x.Type == elem.bike.Type && x.Color == elem.bike.Color && x.Size == elem.bike.Size);
                quantity = stock.Count();
                int price = elem.getPrice();

                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = elem.bike.Type;
                dataGridView1.Rows[i].Cells[1].Value = elem.bike.Size;
                dataGridView1.Rows[i].Cells[2].Value = elem.bike.Color;
                dataGridView1.Rows[i].Cells[3].Value = elem.quantity;
                dataGridView1.Rows[i].Cells[4].Value = quantity;
                dataGridView1.Rows[i].Cells[5].Value = price;
                i++;
            }
            Decimal B = _currentUser.getCartPrice();
            this.labelPrice.Text = B.ToString() + " €";
            if (_currentUser.login == "Manager")
            {
                textBox1.Text = "Stock";
            }
            else
            {
                textBox1.Text = "";
            }

        }

        private void Cart_Load(object sender, EventArgs e)
        {

            Cart_Load();            
        }

        private void button1_Click(object sender, EventArgs e)//login button
        {
            this.Hide();
            var login = new Login();// create new window
            login.FormClosed += (s, args) => this.Close();
            login.Show();// Showing the Login window
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//set a new quantity
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            int bikeQuantity = Int32.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
            Console.WriteLine(bikeQuantity);
            if (bikeQuantity != 0)
            {
                _currentUser.cart[i].setQuantity(Int32.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()));
                this.Cart_Load();
            }
            else
            {
                _currentUser.deleteItem(bikeQuantity);
                this.Cart_Load();
            }
        }

        private void button7_Click(object sender, EventArgs e)//mainhome button
        {
            this.Hide();// Hiding the Explorerbike Window
            var MainHome = new MainHome(_currentUser);// create new window
            MainHome.FormClosed += (s, args) => this.Close();
            MainHome.Show();// Showing the Login window
        }

        private void button2_Click(object sender, EventArgs e)//cart reload
        {
            Cart_Load();
        }

        private void button4_Click(object sender, EventArgs e)//pass order
        {
            string client = textBox1.Text;        
            int RowCount = _currentUser.cart.Count();

            if (client != ""&&RowCount!=0)
            {
                string text ="";
                if(_estimatedShippingWeek == 0)
                {
                    _estimatedShippingWeek = app.GetEstimatedTimeBeforeShipping(_currentUser.cart);
                }

                //pass order
                Representative.SetNewOrderBike(_currentUser.getCartList(), client, _currentUser.getCartPrice(), _estimatedShippingWeek);
                MessageBox.Show("The order has been validated!");

                //print an invoice
                foreach(var elem in _currentUser.getCartList())
                {                   
                    foreach(var value in elem)
                    {
                        text += value + ";";
                    }
                    text += "\n";
                }
                string path = @"../../Data/list_part.csv";               
                File.WriteAllText(path, text);
                printInvoice(client);

                //Reset the cart
                _currentUser.emptyCart();
                Cart_Load();
                //this.labelPrice.Text = "0 €";               
                textBox1.Text = "";
                label5.Text = "0 Weeks";
                
            }
            else
            {
                MessageBox.Show("Please enter a valid name or add items to the cart!");
            }
            
        }

        private void printInvoice(string client)
        {
            List<string> column = new List<string>();
            column.Add("Bike");
            column.Add("Size");
            column.Add("Color");
            column.Add("Quantity");
            column.Add("Price");
            var unselectedData = _currentUser.getCartList();
            List<List<string>> selectedData= new List<List<string>>();
            foreach (var elem in unselectedData)
            {
                List<string> row = new List<string>();
                row.Add(elem[0]);
                row.Add(elem[1]);
                row.Add(elem[2]);
                row.Add(elem[3]);
                row.Add(elem[4]);
                selectedData.Add(row);
            }
            ExportData.CreateInvoice(client, column, selectedData);
        }



        private void button3_Click(object sender, EventArgs e)//order button
        {
            this.Hide();
            Order order = new Order(ref _currentUser);// create new window
            order.FormClosed += (s, args) => this.Close();
            order.Show();// Showing the Order window
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            _currentUser.emptyCart();
            Cart_Load();
        }
        private void button6_Click(object sender, EventArgs e)//estimate Time
        {
            _estimatedShippingWeek = app.GetEstimatedTimeBeforeShipping(_currentUser.cart);
            label5.Text = _estimatedShippingWeek + " Weeks";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
