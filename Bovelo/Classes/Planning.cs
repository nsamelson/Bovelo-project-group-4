using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovelo
{
    class Planning
    {
        public string weekName;
        public int planningId;
        public List<Bike> bikesToBuild= new List<Bike>();
        public List<List<string>> planningDetails;
        public int workingHours;
        public int maxWorkingHours = 3 * 8 * 5;// number of hours per week : 3 workers working 8 hours per day and 5 days a week
        public Planning(List<List<string>> planningDetails, string weekName,int planningId)
        {
            this.weekName = weekName;
            this.planningId = planningId;
            this.planningDetails = planningDetails;
            this.bikesToBuild = getBikesToBuild();
        }
        public List<Bike> getBikesToBuild()
        {

            var bikes = new List<Bike>();
            
            foreach (var elem in planningDetails)
            {
                int idOrderDetails = Int32.Parse(elem[0]);
                string bikeType = elem[1];
                int bikeSize = Int32.Parse(elem[2]);
                string bikeColor = elem[3];
                string status = elem[5];
                string assemblerName = elem[6];
                var newBike = new Bike(idOrderDetails, bikeType, bikeColor, bikeSize) { assembler = assemblerName };
                newBike.setNewState(status);
                bikes.Add(newBike);
                workingHours += newBike.TotalTime;
            }
            return bikes;
        }
        public void refreshBikes(List<List<string>> refreshedBikes)
        {
            planningDetails.Clear();
            planningDetails = refreshedBikes;
        }

        

    }
}
