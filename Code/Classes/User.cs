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
        public Dictionary<string,bool> userType =new Dictionary<string, bool>();
        
        public List<Item> cart = new List<Item>();

        public User(string login, bool isRepresentative, bool isProductionManager,bool isAssembler)
        {
            this.login = login;
            userType.Add("Representative", isRepresentative);
            userType.Add("ProductionManager", isProductionManager);
            userType.Add("Assembler", isAssembler);

        }

        /*public void setNewOrder(int clientId)//Need to move in app
        {
            
            if (userType["Representative"]==true)
            {
                OrderBike newOrder = new OrderBike(clientId);
                newOrder.addOrderBike(getCartList());
            }
        }
        public List<List<string>> getOrderList(int clientId)//Need to move in app
        {

            OrderBike ordersUser = new OrderBike(clientId);
            return ordersUser.getOrderBike();
        }*/
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
                bikeInfo.Add(item.bike.Price.ToString());
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