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
        public int workingMinute;
        public int maxWorkingMinute = 3 * 8 * 5 * 60;// number of hours per week : 3 workers working 8 hours per day and 5 days a week
        public Planning(List<List<string>> planningDetails, string weekName,int planningId)
        {
            this.weekName = weekName;
            this.planningId = planningId;
            this.planningDetails = planningDetails;
            this.bikesToBuild = getBikesToBuild();
        }
        public List<Bike> getBikesToBuild()
        {
            App newApp = new App();
            var bikeModels = newApp.getBikeModelList();//get the model list

            var bikes = new List<Bike>();
            
            foreach (var elem in planningDetails)
            {
                int idOrderDetails = Int32.Parse(elem[0]);
                string bikeType = elem[1];
                int bikeSize = Int32.Parse(elem[2]);
                string bikeColor = elem[3];
                string status = elem[5];
                string assemblerName = elem[6];
                //takes Corresponding model, creates a bike, adds a status and assembler's name, adds bike to the list
                BikeModel model = bikeModels.FirstOrDefault(x => x.Color == bikeColor && x.Size == bikeSize && x.Type == bikeType);//gets the specific model
                var newBike = new Bike(idOrderDetails,model) { assembler = assemblerName };
                newBike.setNewState(status);
                bikes.Add(newBike);


                workingMinute += newBike.TotalTime;
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
