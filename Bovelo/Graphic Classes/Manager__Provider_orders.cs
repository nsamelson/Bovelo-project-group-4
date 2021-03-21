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
    public partial class Manager__Provider_orders : Form
    {
        private User user = new User("Manager", false, false, false);
        internal Manager__Provider_orders(User currentUser)
        {
            this.user = currentUser;
            InitializeComponent();
        }

        private void Manager__Provider_orders_Load(object sender, EventArgs e)
        {

        }
    }
}
