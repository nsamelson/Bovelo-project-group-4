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
        public int workingMinute;
        public int maxWorkingMinute = 3 * 8 * 5 * 60;// number of min per week : 3 workers working 8 hours per day and 5 days a week
        public Planning(List<List<string>> bikeDetails, string weekName,int planningId,List<List<string>>planningDetails,List<BikeModel> bikeModels)
        {
            this.weekName = weekName;
            this.planningId = planningId;
            setBikesToBuild(bikeDetails,bikeModels, planningDetails);
        }
        private void setBikesToBuild(List<List<string>> bikeDetails, List<BikeModel> bikeModels, List<List<string>> planningDetails)
        {
            foreach (var elem in bikeDetails)
            {
                int idOrderDetails = Int32.Parse(elem[0]);
                string bikeType = elem[1];
                int bikeSize = Int32.Parse(elem[2]);
                string bikeColor = elem[3];
                string status = elem[5];
                //string assemblerName = assemblerNames.FirstOrDefault(x => Int32.Parse(x.Key) == idOrderDetails).Value;
                int findIndex = planningDetails.FindIndex(x => Int32.Parse(x[1]) == idOrderDetails);
                string assemblerName = planningDetails[findIndex][2];
                string startDate = planningDetails[findIndex][3];
                string endDate = planningDetails[findIndex][4];
                //takes Corresponding model, creates a bike, adds a status and assembler's name, adds bike to the list
                BikeModel model = bikeModels.FirstOrDefault(x => x.Color == bikeColor && x.Size == bikeSize && x.Type == bikeType);//gets the specific model
                var newBike = new Bike(idOrderDetails, model) { assembler = assemblerName,startBuildTime = startDate,endBuildTime=endDate };
                newBike.setNewState(status);
                bikesToBuild.Add(newBike);


                workingMinute += newBike.TotalTime;
            }
        }
        public List<Bike> getBikesToBuild()
        {
            return bikesToBuild;
        }
       
    }
}
