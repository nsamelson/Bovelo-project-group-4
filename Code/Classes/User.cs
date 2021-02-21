using System;
using System.Collections.Generic;
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
        //public Cart cart_show;
        public List<Item> cart = new List<Item>();
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
        public void getBikeInvoice()
        {

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


        public void getTimeBeforeShipping() { }
        public void getPlanning() { }
        public void getBikePartInvoice() { }
        public void setBikeState() { }
    }
}