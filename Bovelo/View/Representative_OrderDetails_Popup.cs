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
    internal partial class Representative_OrderDetails_Popup : Form
    {
        private List<Bike> bikes;
        public Representative_OrderDetails_Popup(List<Bike> bikes)
        {
            InitializeComponent();
            this.bikes = bikes;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Representative_OrderDetails_Popup_Load(object sender, EventArgs e)
        {
            int i = 0;
            foreach (var bike in bikes)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = bike.bikeId.ToString();
                dataGridView1.Rows[i].Cells[1].Value = bike.type;
                dataGridView1.Rows[i].Cells[2].Value = bike.color;
                dataGridView1.Rows[i].Cells[3].Value = bike.size;
                dataGridView1.Rows[i].Cells[4].Value = bike.price;
                dataGridView1.Rows[i].Cells[5].Value = bike.GetCurrentState();
                i++;
            }
        }
    }
}
