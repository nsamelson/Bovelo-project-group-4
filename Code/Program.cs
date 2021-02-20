using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bovelo
{
    static class Program
    {

        private static App app = new App();
        [STAThread]
        
        static void Main(string[] args)
        {

            //un panel ou autre contenur tu peux mettre this par exemple
            app.InitializeBikeModel();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainHome(app,1));
            //Application.Run(new Login());
            //Application.Run(new Explorerbike());
            /*app.addNewAdmin(new User("admin1", "admin1"));
            app.addNewUser(new User("user1", "user1"));
            app.userList[0].addToCart(new Bike("Adventure", "red", 28), 3);
            app.userList[0].addToCart(new Bike("City", "blue", 26), 15);
            app.userList[0].addToCart(new Bike("Explorer", "black", 28), 50);
            app.userList[0].addToCart(new Bike("Explorer", "red", 26), 20);


            Console.WriteLine("Before modifications");
            foreach (var a in app.userList[0].getCartList())
            {
                string z = "Bike : ";
                foreach (var b in a)
                {
                    z += b + ", ";
                }
                Console.WriteLine(z);
            }
            app.userList[0].incrementItem(0);
            app.userList[0].deleteItem(2);
            Console.WriteLine("After modifications");
            // app.userList[0].getCartList();
            foreach (var a in app.userList[0].getCartList())
            {
                string z = "Bike : ";
                foreach (var b in a)
                {
                    z += b + ", ";
                }
                Console.WriteLine(z);
            }

         */
         }
    }
}
