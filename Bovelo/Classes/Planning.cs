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
        public Planning(List<List<string>> planningDetails, string weekName,int planningId)
        {
            this.weekName = weekName;
            this.planningId = planningId;
            this.planningDetails = planningDetails;
            this.workingHours = 3 * 8 * 5;// number of hours per week : 3 workers working 8 hours per day and 5 days a week
            this.bikesToBuild = getBikesToBuild();
        }
        public List<Bike> getBikesToBuild()
        {

            var bikes = new List<Bike>();
            
            foreach (var elem in planningDetails)
            {
                string type = elem[2];
                string status = elem[3];
                int id = Int32.Parse(elem[0]);//NOT SURE
                bikes.Add(new Bike(id,type, "red", 26));
                
                //int size = Int32.Parse(elem[2]);
                //string color = elem[3];
                //int quantity = Int32.Parse(elem[4]);
                //int price = Int32.Parse(elem[5]) / quantity;//dont need price
                //NEED ID OF THE BIKE
                /*for (int i = 0; i < quantity; i++)//quantity of this bike
                {
                    bikes.Add(new Bike(type, color, size, price));
                }*/
            }
            return bikes;
        }
        public void refreshBikes(List<List<string>> refreshedBikes)
        {
            planningDetails.Clear();
            planningDetails = refreshedBikes;
        }

        public void setBikeState(Bike bike,string state)//change the bike selected to a new state IT HAS TO CHANGE THE PLANNING DETAILS AND SEND IT INTO DB
        {
            bike.state[getBikeState(bike)] = false;// change the actual state to false
            if (state == "New"|| state == "Active" || state == "Closed")
            {
                bike.state[state] = true;
            }
            else { }//error

        }
        public string getBikeState(Bike bike)//gets the actual state of selected bike (between New, Active and Closed)
        {
            var state = bike.state.FirstOrDefault(x=>x.Value==true).Key;
            return state;
        }

    }
}
