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
        public DateTime orderDate;
        public DateTime shippingDate;
        public List<List<string>> orderDetail; //Details of the order : [id,Client_Name,Bike_Type,Bike_Color,Bike_Size,Quantity,Price,Order_Time]
        public List<Bike> bikeList;

        public OrderBike(string clientName, List<List<string>> orderDetail,int id)//needs to insert clientId
        {
            this.clientName = clientName;
            this.orderDetail = orderDetail;
            this.orderId = id;
            this.totalPrice = getTotalPrice();
            this.orderDate = DateTime.Now;
            this.shippingDate = DateTime.Today.AddDays(14);
            this.bikeList = getBikeList();
            this.isReadyToShip = getOrderState();
        }
        public List<Bike> getBikeList()
        {
            var bikes = new List<Bike>();
            foreach(var elem in orderDetail)
            {
                for(int i=0; i<Int32.Parse(elem[4]);i++)//quantity of this bike
                {
                    bikes.Add(new Bike(elem[1], elem[3], Int32.Parse(elem[2]), Int32.Parse(elem[5])));
                }
            }
            return bikes;
        }
        public int getTotalPrice()
        {
            int totPrice = 0;
            foreach(var item in orderDetail)
            {
                totPrice += Int32.Parse(item[4]);
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
