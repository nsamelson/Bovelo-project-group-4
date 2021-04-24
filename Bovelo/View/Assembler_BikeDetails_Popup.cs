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
    internal partial class Assembler_BikeDetails_Popup : Form
    {
        Bike bike;
        public Assembler_BikeDetails_Popup(Bike bike)
        {
            InitializeComponent();
            this.bike = bike;
        }

        private void Assembler_BikeDetails_Popup_Load(object sender, EventArgs e)
        {
            int i = 0;
            string info = bike.bikeId.ToString() + "|" + bike.type + " " + bike.color + " " + bike.size.ToString();
            label1.Text = info;
            foreach (var part in bike.bikeParts)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = part.part_Id.ToString();
                dataGridView1.Rows[i].Cells[1].Value = part.name.ToString();
                dataGridView1.Rows[i].Cells[2].Value = part.location;
                i++;
            }
        }
    }
}
