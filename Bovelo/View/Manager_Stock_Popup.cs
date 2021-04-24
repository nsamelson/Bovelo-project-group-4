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
    public partial class Manager_Stock_Popup : Form
    {
        internal List<string> bikeType = new List<string>();
        int orderId;
        int maxValue;
        Manager_Replace_bike_from_stock window;
        internal Manager_Stock_Popup(Manager_Replace_bike_from_stock incomingWindow,List<string> bikeToChange,int incomingOrder,int incomingMaxValue)
        {
            InitializeComponent();
            bikeType = bikeToChange;
            orderId = incomingOrder;
            maxValue = incomingMaxValue;
            label1.Text = " Please enter number of bike to use from the stock :\n maximum value = " + maxValue;
            window = incomingWindow;
            textBox1.Text = "0";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Manager_Stock_Popup_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(bikeType[0] + " " + bikeType[1] + " " + bikeType[2] + "|" + Int32.Parse(textBox1.Text) + "|"+orderId);
            if (textBox1.Text != "" && (Int32.Parse(textBox1.Text) > maxValue))
            {
                Manager.ReplaceBikeFromTheStock(bikeType, Int32.Parse(textBox1.Text), orderId);
                this.Hide();
            }
            else
            {
                textBox1.Text = maxValue.ToString();
                MessageBox.Show("Stock quantity must be higher or bike already satisfied");
                this.Close();
            }
            window.LoadBikeStock();
            window.LoadBikeOrder();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
