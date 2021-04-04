using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bovelo
{
    class Item //: Bike should maybe put it that way because of composition
    {
        public int quantity ;
        public int price = 0;

        public Item(int quantity)
        {
            this.quantity = quantity;
        }

        public void increment()
        {
            quantity++;
            price = getPrice();
        }
        public void decrement()
        {
            quantity--;
            price = getPrice();
        }
        public virtual int getPrice()
        {
            return price;
        }


    }
}