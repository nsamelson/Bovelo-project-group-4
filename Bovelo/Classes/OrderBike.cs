using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;

namespace Bovelo
{
    class OrderBike
    {
        public int orderId;
        public bool isReadyToShip ;
        public string clientName;
        public int totalPrice;
        public DateTime orderDate=DateTime.Now;
        public DateTime shippingDate=DateTime.Now;
        public List<List<string>> orderDetail; //Details of the order : [id,Client_Name,Bike_Type,Bike_Color,Bike_Size,Quantity,Price,Order_Time]
        public List<Bike> bikeList;

        public OrderBike(string clientName, List<List<string>> orderDetail, int id)//needs to insert clientId
        {
            this.clientName = clientName;
            this.orderDetail = orderDetail;
            this.orderId = id;
            this.totalPrice = getTotalPrice();
            // this.orderDate = DateTime.Now;
            // this.shippingDate = DateTime.Today.AddDays(7);
            this.bikeList = getBikeList();
            this.isReadyToShip = getOrderState();
        }

        public List<Bike> getBikeList()
        {
            var bikes = new List<Bike>();
            foreach(var elem in orderDetail)
            {
                //Console.WriteLine("id : " + elem[0] + "type: " + elem[1] + " size : " + elem[2]+ " color: " + elem[3]+ " price : " + elem[4]);
                string type = elem[1];
                int size = Int32.Parse(elem[2]);
                string color = elem[3];
                int price = Int32.Parse(elem[4]);
                int id = Int32.Parse(elem[0]);

                bikes.Add(new Bike(id,type, color, size));
                //int quantity = Int16.Parse(elem[3]);


                /*for (int i = 0; i < 2; i++)//quantity of this bike
                {
                    bikes.Add(new Bike(type,color,size,price));
                }*/
            }
            return bikes;
        }
        public int getTotalPrice()
        {
            int totPrice = 0;
/*            foreach(var item in orderDetail)
            {
                totPrice += Int32.Parse(item[4]);
            }*/
            foreach(var item in getBikeList())
            {
                totPrice += item.Price;
            }
            return totPrice;
        }
        public bool getOrderState()//needs to be tested
        {
            if (bikeList.TrueForAll(x => x.state["Closed"]))
            {
                return true;
            }
            else { return false; }
        }
        
    }
}
