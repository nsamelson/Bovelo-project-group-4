using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovelo
{
    class Planning
    {
        public string weekId;
        public List<Bike> bikesToBuild= new List<Bike>();
        public List<OrderBike> orderList;
        public int workingHours;
        public Planning(List<OrderBike> orders,string weekId)
        {
            this.weekId = weekId;
            this.orderList = orders;
            this.workingHours = 3 * 8 * 5;// number of hours per week : 3 workers working 8 hours per day and 5 days a week
        }
        public void setPlanning()//maybe put it in app
        {
            
        }
        public void setBikesToBuild()//NEED TO CHANGE
        {
            foreach(var order in orderList)
            {
                /*foreach (var bike in order.bikeList)
                {
                    bikesToBuild.Add(bike);
                }*/
            }
        }
        public void setBikeState()
        {

        }
        public void addToPlanning(int id) { }
        public void removeFromPlanning(int id) { }
    }
}
