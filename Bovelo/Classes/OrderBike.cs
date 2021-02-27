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


        public OrderBike(string clientName, List<List<string>> orderDetail)//needs to insert clientId
        {
            this.clientName = clientName;
            this.orderDetail = orderDetail;
        }
        
    }
}
