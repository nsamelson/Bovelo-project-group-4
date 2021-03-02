using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovelo.Classes
{
    class Planning
    {
        public string week;
        public List<Bike> BikesToBuild= new List<Bike>();
        public List<OrderBike> orderList;
        public int workingHours;
        public Planning(List<OrderBike> orders, int numberOfAssemblers)
        {
            this.orderList = orders;
            this.workingHours = numberOfAssemblers * 8 * 5;// number of hours per week : 3 workers working 8 hours per day and 5 days a week
        }
        public void setPlanning()
        {
            
        }
    }
}
