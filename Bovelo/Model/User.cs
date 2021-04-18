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
        public List<ItemBike> cart = new List<ItemBike>();//cart of bikes used by representative
        public List<ItemPart> cartPart = new List<ItemPart>();
        public List<List<string>> planningCart = new List<List<string>>();

        //public List<BikePart> bikePartCart = new List<BikePart>(); //cart of bikePart used by production manager to order parts
        //public List<Bike> planningCart = new List<Bike>();//"cart" of Bikes used by production manager to create a planning

        public User(string login, bool isRepresentative, bool isProductionManager,bool isAssembler)
        {
            this.login = login;
            userType.Add("Representative", isRepresentative);
            userType.Add("Production Manager", isProductionManager);
            userType.Add("Assembler", isAssembler);

        }
        //Will generalize the functions : emptyCart();increment;decrement;deleteItem and maybe addtocart and getCart list


        //REPRESENTATIVE METHODS
        public void AddToCart(Bike bike, int quantity) //adds a bike to cart with the quantity
        {
            cart.Add(new ItemBike(bike, quantity));
        }

        public List<List<string>> GetCartList() //returns the cart into a list of list of string
        {
            var cartAsList = new List<List<string>>();
            int i = 1;
            foreach (var item in cart)
            {
                var bikeInfo = new List<string>();
                //bikeInfo.Add(i.ToString()); //I used this to have a corresponding list between orderBike when we pass orders from the cart and when we take from DB
                bikeInfo.Add(item.bike.type);             //elem 0
                bikeInfo.Add(item.bike.size.ToString());  //elem 1
                bikeInfo.Add(item.bike.color);            //elem 2
                bikeInfo.Add(item.quantity.ToString());   //elem 3
                bikeInfo.Add(item.GetPrice().ToString()); //elem 4
                cartAsList.Add(bikeInfo);
                i++;
            }

            return cartAsList;
        }

        public int GetCartPrice()
        {
            int price = 0;
            foreach(var item in cart)
            {
                price += item.price;
            }
            return price;
        }
        public void IncrementItem(int idList) //increment the quantity of a bike in cart
        {
            if (cart[idList].quantity < 100)
            {
                cart[idList].Increment();
            }
            else
            {
                Console.WriteLine("Could not increment 1 Bike of type :" + cart[idList].bike.type);
            }
        }
        public void DecrementItem(int idList) //decrement the quantity of a bike in cart
        {
            if (cart[idList].quantity > 0)
            {
                cart[idList].Decrement();
            }
            else
            {
                Console.WriteLine("Could not increment 1 Bike of type :" + cart[idList].bike.type);
            }
        }

        public void DeleteItem(int idList) //deletes an item from the cart
        {
            cart.RemoveAt(idList);
        }

        public void EmptyCart() //empty the cart
        {
            cart.Clear();
        }
        public void GetTimeBeforeShipping() { }


        //PRODUCTION MANAGER METHODS

        
        public List<List<string>> GetPlanningCartList()
        {
            /*var planningAsList = new List<List<string>>();
            int id = 0;
            foreach(var item in planningCart)
            {
                var bikeInfo = new List<string>();
                bikeInfo.Add(item[0]);  //elem 0
                bikeInfo.Add(item[1]);  //elem 1
                bikeInfo.Add(item[2]);  //elem 2
                bikeInfo.Add(item[3]);  //elem 3
                //bikeInfo.Add();
                planningAsList.Add(bikeInfo);
            }*/
            var Plan = new List<List<string>>();
            

            return Plan;
        }

        public void AddToPlanningCart(Bike bike,int id)//adds a bike from an order to the planning of the week
        {
            List<string> newBike = new List<string>() { id.ToString(), bike.type, bike.size.ToString(), bike.color  };
            planningCart.Add(newBike);
        }
        public void EmptyPlanningCart()
        {
            planningCart.Clear();
        }
        public void GetBikePartInvoice() { }//Maybe in app
        public void AddBikePartToCart() { } //and create a bikePart cart

        //ASSEMBLER METHODS
        public void GetPlanning() { }//already in app
        public void GetBikeParts() { }//location of the bikeParts
        public void SetBikeState() { }//Maybe better in the planning class

    }
}