using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovelo
{
    public class ItemPart
    {
        public int quantity;
        internal BikePart part;
        public int price;
        ItemPart(BikePart part, int quantity)
        {
            this.quantity = quantity;
            this.part = part;
            this.price = getPrice();
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
        public int getPrice()
        {
            return quantity * this.price;
        }
    }
}
