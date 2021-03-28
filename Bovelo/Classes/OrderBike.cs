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
        public List<List<string>> orderDetail; //Details of the order : [id,Client_Name,Bike_Type,Bike_Color,Bike_Size,Quantity,Price,Order_Time]
        public List<Bike> bikeList;
        private List<BikeModel> _bikeModels;

        public OrderBike(string clientName, List<List<string>> orderDetail, int id,DateTime orderDate,DateTime shippingDate,int totalPrice,List<BikeModel> bikeModels)
        {
            this.clientName = clientName;
            this.orderDetail = orderDetail;
            this.orderId = id;
            this.orderDate = orderDate;
            this.shippingDate = shippingDate;
            this.totalPrice = totalPrice;
            this._bikeModels = bikeModels;
            this.bikeList = getBikeList();
            this.isReadyToShip = getOrderState();
        }

        public float getTotalBuildingTime()
        {
            int hours = 0;
            foreach(var bike in bikeList)
            {
                hours += bike.TotalTime;
            }
            return hours / 60;
        }
        private List<Bike> getBikeList()
        {
            var bikes = new List<Bike>();
            foreach(var elem in orderDetail)
            {
                int id = Int32.Parse(elem[0]);
                string type = elem[1];
                int size = Int32.Parse(elem[2]);
                string color = elem[3];
                //int price = Int32.Parse(elem[4]);

                BikeModel model = _bikeModels.FirstOrDefault(x => x.Color == color && x.Size == size && x.Type == type);//gets the specific model
                Bike bike = new Bike(id, model);
                bike.setNewState(elem[5]);
                bikes.Add(bike );//adds a corresponding Bike
            }
            return bikes;
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
