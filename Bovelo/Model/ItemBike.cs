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
            price = GetPrice();
        }
        public override int GetPrice()
        {
            return quantity * bike.Price;
        }
        public int GetTotalTime()
        {
            return quantity * bike.TotalTime;
        }
        public void SetQuantity(int quantity)
        {
            this.quantity = quantity;
            this.price = quantity * this.bike.Price;
        }
    }
}