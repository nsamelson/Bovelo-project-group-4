using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;


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
        //public List<List<string>> orderDetail; //Details of the order : [id,Client_Name,Bike_Type,Bike_Color,Bike_Size,Quantity,Price,Order_Time]
        public List<Bike> bikeList = new List<Bike>();

        public OrderBike(string clientName, List<List<string>> orderDetail, int id,DateTime orderDate,DateTime shippingDate,int totalPrice,List<BikeModel> bikeModels)
        {
            this.clientName = clientName;
            this.orderId = id;
            this.orderDate = orderDate;
            this.shippingDate = shippingDate;
            this.totalPrice = totalPrice;
            SetBikeList(bikeModels, orderDetail);
            this.isReadyToShip = GetOrderState();
            
        }

        public float GetTotalBuildingTime()
        {
            int hours = 0;
            foreach(var bike in bikeList)
            {
                hours += bike.totalTime;
            }
            return hours / 60;
        }

        public List<Bike> GetBikeList()
        {
            return bikeList;
        }

        private void SetBikeList(List<BikeModel> bikeModels,List<List<string>> orderDetail)
        {
            foreach(var elem in orderDetail)
            {
                int id = Int32.Parse(elem[0]);
                string type = elem[1];
                int size = Int32.Parse(elem[2]);
                string color = elem[3];
                //int price = Int32.Parse(elem[4]);

                BikeModel model = bikeModels.FirstOrDefault(x => x.color == color && x.size == size && x.type == type);//gets the specific model
                Bike bike = new Bike(id, model);
                bike.SetNewState(elem[5]);
                bikeList.Add(bike );//adds a corresponding Bike
            }
            
        }
        
        public bool GetOrderState()//needs to be tested
        {
            return bikeList.TrueForAll(x => x.state["Closed"]);
        }
        
    } 
}
