using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovelo
{
    class OrderBikePart
    {
        public int orderId;
        public bool isReadyToShip = false;
        public int totalPrice;
        public DateTime orderDate = DateTime.Now;
        public DateTime shippingDate = DateTime.Today;
        public Dictionary<BikePart,int> partListQuantity=new Dictionary<BikePart, int>();


        public OrderBikePart(Dictionary<BikePart, int> partListQuantity,int id)
        {
            this.orderId = id;
            this.partListQuantity = partListQuantity;
            GetPrice();
        }

        public void GetPrice()
        {
            foreach(var elem in this.partListQuantity)
            {
                this.totalPrice += elem.Key.price*elem.Value;
            }
        }
    }
}
