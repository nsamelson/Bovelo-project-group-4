using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bovelo
{

    class User
    {
        public string login;
        private string _password;
        public string password { get => _password; }
        public bool isAdmin = false;
        public int idUser;
        //public Cart cart_show;
        public List<Item> cart = new List<Item>();
        public List<List<string>> orderList;
        public User(string login, string password,int idUser)
        {
            this.login = login;
            this._password = password;
            this.idUser = idUser;
            this.orderList = getOrderList();

        }
        public User(string login, string password)
        {
            this.login = login;
            this._password = password;

        }
        public void setOrderBike()
        {
            var orderList = getCartList();
            OrderBike newOrder = new OrderBike(this);

        }
        public List<List<string>> getOrderList()
        {
            List<List<string>> orderList = new List<List<string>>();
            

            var userFromDB = new List<User>();

            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = "SELECT * FROM Order_Bikes WHERE id_User =" + idUser + "; ";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                List<string> bikeInfo = new List<string>();
                string idOrder = Convert.ToString(rdr[0]);
                string bikeType = Convert.ToString(rdr[1]);
                string bikeSize = Convert.ToString(rdr[2]);
                string bikeColor = Convert.ToString(rdr[3]);
                string quantity = Convert.ToString(rdr[4]);
                string shipping_time = Convert.ToString(rdr[5]);
                string price = Convert.ToString(rdr[6]);
                Console.WriteLine(rdr[0].ToString());
                bikeInfo.Add(bikeType );
                bikeInfo.Add(bikeSize);
                bikeInfo.Add(bikeColor);
                bikeInfo.Add(quantity);
                bikeInfo.Add(shipping_time);
                bikeInfo.Add(price);
                orderList.Add(bikeInfo);
            }
            rdr.Close();
            conn.Close();


            return orderList;
        }
        public void addToCart(Bike bike, int quantity)
        {
            cart.Add(new Item(bike, quantity));
        }
        public List<List<string>> getCartList()
        {
            var cartAsList = new List<List<string>>();

            foreach (var item in cart)
            {
                var bikeInfo = new List<string>();
                bikeInfo.Add(item.bike.Type);
                bikeInfo.Add(item.bike.Color);
                bikeInfo.Add(item.bike.Size.ToString());
                bikeInfo.Add(item.quantity.ToString());
                cartAsList.Add(bikeInfo);
            }

            return cartAsList;
        }
        public void incrementItem(int idList)
        {
            if (cart[idList].quantity < 100)
            {
                cart[idList].quantity++;
            }
            else
            {
                Console.WriteLine("Could not increment 1 Bike of type :" + cart[idList].bike.Type);
            }
        }
        public void decrementItem(int idList)
        {
            if (cart[idList].quantity > 0)
            {
                cart[idList].quantity--;
            }
            else
            {
                Console.WriteLine("Could not increment 1 Bike of type :" + cart[idList].bike.Type);
            }
        }
        public void deleteItem(int idList)
        {
            cart.RemoveAt(idList);
        }
        public void emptyCart()
        {
            cart.Clear();
        }


        public void getTimeBeforeShipping() { }
        public void getPlanning() { }
        public void getBikePartInvoice() { }
        public void setBikeState() { }
    }
}