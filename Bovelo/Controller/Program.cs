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

        private static App app = new App();
        [STAThread]
        
        static void Main(string[] args)
        {
            Application.Run(new Login());
            /*var user = app.getUserList()[0];
            Application.Run(new Manager_Create_Model(user));*/
            /*app.setNewBikePart("Phare");
            app.setNewBikePart("guidon",26);
            app.setNewBikePart("fourhce",28,"red");
            app.setNewBikePart("sticker",0,"black");*/
        }
    }
}
