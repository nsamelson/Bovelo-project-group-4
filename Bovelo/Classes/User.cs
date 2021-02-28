using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bovelo
{

    class User //user class, is representative, ProductionManager or Assembler. Contains a cart for the representative and methods with rights depending of its status
    {
        public string login; 
        public Dictionary<string,bool> userType =new Dictionary<string, bool>();
        
        public List<Item> cart = new List<Item>();

        public User(string login, bool isRepresentative, bool isProductionManager,bool isAssembler)
        {
            this.login = login;
            userType.Add("Representative", isRepresentative);
            userType.Add("ProductionManager", isProductionManager);
            userType.Add("Assembler", isAssembler);

        }

        public void addToCart(Bike bike, int quantity) //adds a bike to cart with the quantity
        {
            cart.Add(new Item(bike, quantity));
        }
        public List<List<string>> getCartList() //returns the cart into a list of list of string
        {
            var cartAsList = new List<List<string>>();

            foreach (var item in cart)
            {
                var bikeInfo = new List<string>();
                bikeInfo.Add(item.bike.Type);
                bikeInfo.Add(item.bike.Color);
                bikeInfo.Add(item.bike.Size.ToString());
                bikeInfo.Add(item.quantity.ToString());
                bikeInfo.Add(item.getPrice().ToString());
                cartAsList.Add(bikeInfo);
            }

            return cartAsList;
        }
        public void incrementItem(int idList) //increment the quantity of a bike in cart
        {
            if (cart[idList].quantity < 100)
            {
                cart[idList].increment();
            }
            else
            {
                Console.WriteLine("Could not increment 1 Bike of type :" + cart[idList].bike.Type);
            }
        }
        public void decrementItem(int idList) //decrement the quantity of a bike in cart
        {
            if (cart[idList].quantity > 0)
            {
                cart[idList].decrement();
            }
            else
            {
                Console.WriteLine("Could not increment 1 Bike of type :" + cart[idList].bike.Type);
            }
        }
        public void deleteItem(int idList) //deletes an item from the cart
        {
            cart.RemoveAt(idList);
        }
        public void emptyCart() //empty the cart
        {
            cart.Clear();
        }


        public void getTimeBeforeShipping() { }
        public void getPlanning() { }
        public void getBikePartInvoice() { }
        public void setBikeState() { }
    }
}