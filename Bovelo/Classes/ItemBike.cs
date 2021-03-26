using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bovelo
{
    class ItemBike : Item //: Bike should maybe put it that way because of composition
    {
        public Bike bike;
        public ItemBike(Bike bike, int quantity) : base(quantity)
        {
            this.quantity = quantity;
            this.bike = bike;
            price = getPrice();
        }
        public override int getPrice()
        {
            return quantity * bike.Price;
        }
        public void setQuantity(int quantity)
        {
            this.quantity = quantity;
            this.price = quantity * this.bike.Price;
        }
    }
}