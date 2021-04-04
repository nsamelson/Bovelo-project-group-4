using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovelo
{
    public static class Representative
    {
        public static void SetNewOrderBike(List<List<string>> newOrder, string clientName, int totPrice, int shippingWeek)
        {
            var orderId =1;
            var daysToAdd = shippingWeek * 7;
            var values = "";
            var i = 0;

            //first request
            string queryOB = "INSERT INTO Order_Bikes(Customer_Name,Total_Price,Order_Date,Shipping_Time) VALUES('" + clientName + "', '" + totPrice + "' ,'" + DateTime.Now.ToString() + "','" + DateTime.Today.AddDays(daysToAdd).ToString() + "');";
            DataBase.SendToDB(queryOB);
            string id = DataBase.GetFromDBLastIdFromColumn("Order_Bikes", "Id_Order");

            //second request
            if (id != string.Empty || id != "0")//if orderList is not empty and id is not 0
            {
                //orderId = 1;
                orderId = Int32.Parse(id);
            }
            foreach (var element in newOrder)
            {
                var type = element[0];
                var size = Int32.Parse(element[1]);
                var color = element[2];
                var quantity = Int32.Parse(element[3]);
                var price = Int32.Parse(element[4]) / quantity;

                for (int q = 0; q < quantity; q++)
                {
                    values += "('" + type + "', '" + size + "','" + color + "' , '" + price + "', 'New' , '" + orderId + "')";
                    if (i == newOrder.Count - 1 && q == quantity - 1)
                    {
                        values += ";";
                    }
                    else
                    {
                        values += ",";
                    }

                }
                i++;
            }
            string queryOD = "INSERT INTO Order_Details (Bike_Type,Bike_Size,Bike_Color,Price,Bike_Status,Id_Order) VALUES" + values;
            DataBase.SendToDB(queryOD);

        }

        //maybe transfer "App.getEstimatedTimeBeforeShipping()" here
    }
}
