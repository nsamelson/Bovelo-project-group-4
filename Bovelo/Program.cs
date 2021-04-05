using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Bovelo
{
    static class Program
    {
        //private static App app = new App();
        [STAThread]
        
        static void Main(string[] args)
        {
            Application.Run(new Login());

        }
    }
}
