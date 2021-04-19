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
        internal Manager_Stock_Popup(List<string> bikeToChange,int incomingOrder)
        {
            InitializeComponent();
            bikeType = bikeToChange;
            orderId = incomingOrder;
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
            Manager.ReplaceBikeFromTheStock(bikeType,Int32.Parse(textBox1.Text),orderId);

            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
