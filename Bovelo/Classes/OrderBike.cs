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

        public string clientName;
        public int totalPrice;
        public DateTime orderDate;
        public DateTime shippingDate;
        public List<List<string>> orderDetail; //Details of the order : [id, OrderBike_Id, Client_Name,Bike_Type,Bike_Color,Bike_Size,Quantity,Price,Order_Time]


        public OrderBike(string clientName, List<List<string>> orderDetail,int id)//needs to insert clientId
        {
            this.clientName = clientName;
            this.orderDetail = orderDetail;
            this.orderId = id;
            this.totalPrice = getTotalPrice();
            this.orderDate = DateTime.Now;
            this.shippingDate = DateTime.Now.AddDays(14);
        }
        public int getTotalPrice()
        {
            int totPrice = 0;
            foreach(var item in orderDetail)
            {
                totalPrice += Int32.Parse(item[7]);
            }
            return totPrice;
        }
        
    }
}
