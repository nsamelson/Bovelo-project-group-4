using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;
using System.Globalization;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Bovelo
{
    public class App //Super class, it takes everything from the database and will send anything to it
    {
        internal List<User> userList = new List<User>(); 
        internal List<BikePart> bikePartList = new List<BikePart>();
        internal List<BikeModel> bikeModels = new List<BikeModel>(); 
        internal List<OrderBike> orderBikeList = new List<OrderBike>(); 
        internal List<Planning> planningList = new List<Planning>(); 

        private List<List<string>> _linkingPartList = new List<List<string>>();

        public App(bool getAll = false)
        {
            if (getAll)
            {
                SetBikePartList();
                SetBikeModelList();
                SetUserList();
                SetOrderBikeList();
                SetPlanningList();
                SetLinkingPartList();
            }
        }

        //SET METHODS
        internal void SetNewUser(List<string> loginRole) //Creates a new User (for ex: a new Assembler joins the team)
        {
            string query = "INSERT INTO Users (Login,Role) VALUES('" + loginRole[1]+ "','" + loginRole[0]+ "');";
            Console.WriteLine(query);
            DataBase.SendToDB(query);
            SetUserList(); //At the end of set, put a get to update App class
        }

        internal void SetUserList()
        {
            userList = GetUserList();
        }

        internal void SetBikeModelList()
        {
            bikeModels = GetBikeModelList();
        }

        internal void SetBikePartList()
        {
            bikePartList = GetBikePartList();
        }

        internal void SetOrderBikeList()
        {
            orderBikeList = GetOrderBikeList();
        }

        internal void SetPlanningList()
        {
            planningList = GetPlanningList();
        }

        internal void SetLinkingPartList()
        {
            _linkingPartList = GetLinkingPartList();
        }

        //GET METHODS
        internal List<User> GetUserList() //is used to get all users 
        {
            var userFromDB = new List<User>();
            List<List<string>> orderList = DataBase.GetFromDB("Users");
            foreach (var row in orderList)
            {
                string login = row[1];
                string userType = row[2];
                switch (userType)
                {
                    case "Representative":
                        userFromDB.Add(new User(login, true, false, false));
                        break;
                    case "Production Manager":
                        userFromDB.Add(new User(login, false, true, false));
                        break;
                    case "Assembler":
                        userFromDB.Add(new User(login, false, false, true));
                        break;
                    default:
                        //Console.WriteLine("user : " + login + ", is not registered correctly in the DataBase");
                        break;
                }
            }
            return userFromDB;
        }

        internal List<List<string>> GetLinkingPartList()
        {
            return DataBase.GetFromDB("Parts");
        }

        internal List<BikePart> GetBikePartList()
        {
            List<BikePart> bikeParts = new List<BikePart>();
            var bikePartsFromDB = DataBase.GetFromDB("Bike_Parts");

            foreach (var row in bikePartsFromDB)
            {
                bikeParts.Add(new BikePart(Int32.Parse(row[0]), row[1], row[3], Int32.Parse(row[4]), row[5], Int32.Parse(row[6])) { quantity = Int32.Parse(row[2])});
            }
            return bikeParts;
        }

        internal List<Planning> GetPlanningList() //maybe transfer into Assembler Controller
        {
            SetBikeModelList();
            List<Planning> plannings = new List<Planning>();
            var detailedSchedules = DataBase.GetFromDB("Detailed_Schedules");
            var allorders = DataBase.GetFromDB("Order_Details");
            Dictionary<string, Dictionary<string,string>> schedules = new Dictionary<string, Dictionary<string, string>>();//<weekName,<idOrderDetails,AssemblerName>>
            int id = 0;
            Dictionary<string, List<List<string>>> test = new Dictionary<string, List<List<string>>>(); //<weekName,<weekName,id_Order_Details,Assebled_By,Started,Finnished>
            foreach (var row in detailedSchedules)//each bike in Detailed_Schedules <weekName,id_Order_Details,Assebled_By,Started,Finnished>
            {
                if (!test.ContainsKey(row[0]))
                {
                    test.Add(row[0], new List<List<string>>() { row }); //adds the detailed schedule to the corresponding week 
                }
                else
                {
                    var values = test.FirstOrDefault(x => x.Key == row[0]).Value;
                    values.Add(row);
                    test[row[0]] = values;
                }
            }
            foreach (var week in test)
            {
                List<List<string>> bikes = new List<List<string>>();
                foreach(var row in week.Value)
                {
                    bikes.Add(allorders.FirstOrDefault(x=>x[0] ==row[1]));
                }
                plannings.Add(new Planning(bikes, week.Key, id, week.Value, bikeModels));
                id++;
            }
            return plannings;
        }

        internal List<OrderBike> GetOrderBikeList() //is used to get all Bike Orders NEED TO TRY
        {
            List<OrderBike> orderBikeList = new List<OrderBike>();
            var orderList = DataBase.GetFromDB("Order_Bikes");
            var orderDetailList = DataBase.GetFromDB("Order_Details");
            SetBikeModelList();

            foreach (var row in orderList)
            {
                List<List<string>> details = new List<List<string>>(orderDetailList.FindAll(x => x[6] == row[0]));//takes each lists with the same order_Id
                OrderBike newOrder = new OrderBike(row[1], details, Int32.Parse(row[0]), Convert.ToDateTime(row[3]), Convert.ToDateTime(row[4]), Int32.Parse(row[2]),bikeModels);
               
                orderBikeList.Add(newOrder);//row[1] is the column where the name of the client is put
            }
            return orderBikeList;
        }

        internal List<BikeModel> GetBikeModelList() //is used to get all bike models
        {
            List<BikeModel> bikeList = new List<BikeModel>();//list to return
            List<List<string>> modelList = DataBase.GetFromDB("Bike_Model");//bikemodels from db
            if(_linkingPartList.Count == 0)
            {
                SetLinkingPartList();
            }
            if(bikePartList.Count == 0)
            {
                SetBikePartList();
            }
            foreach (var row in modelList)
            {
                List<int> bikePartsIds = new List<int>();
                int id = Int32.Parse(row[0]);
                string color = row[1];
                int size = Int32.Parse(row[2]);
                string type = row[3];
                var newBikeModel = new BikeModel(type, color, size) { idBikeModel = id };

                foreach (var part in _linkingPartList)
                {
                    if (Int32.Parse(part[1]) == id)
                    {
                        bikePartsIds.Add(Int32.Parse(part[1]));
                    }
                }
                bikePartsIds.Sort();
                newBikeModel.bikeParts = bikePartsIds.Select(x => bikePartList.First(part => part.part_Id == x)).ToList(); //FOR DUPLICATES 
                //newBikeModel.bikeParts = bikePartList.FindAll(part => bikePartsIds.Contains(part.part_Id));
                newBikeModel.SetPriceAndTime();
                bikeList.Add(newBikeModel);
            }
            return bikeList;
        }

        internal Dictionary<int, int> GetWeekPieces(string weekName) //really with weekName ?!
        {
            List<Bike> bikesToGetPieces = new List<Bike>();

            foreach (var planning in this.GetPlanningList())
            {
                if (planning.weekName == weekName)
                {
                    bikesToGetPieces = planning.GetBikesToBuild();
                }
            }
            List<int> differentPartsId = new List<int>();
            List<int> allPartsId = new List<int>();
            foreach (var bike in bikesToGetPieces)
            {
                foreach (var bikepart in bike.bikeParts)
                {
                    if (!differentPartsId.Contains(bikepart.part_Id))
                    {
                        differentPartsId.Add(bikepart.part_Id);
                    }
                    allPartsId.Add(bikepart.part_Id);
                }
            }
            Dictionary<int, int> PartIdQuantity = new Dictionary<int, int>();
            foreach (var partId in differentPartsId)
            {
                List<int> elem_to_count = allPartsId.FindAll(partID => partID == partId);
                PartIdQuantity.Add(partId, elem_to_count.Count());
                elem_to_count.Clear();
            }
            return PartIdQuantity;
        }

        internal int GetEstimatedTimeBeforeShipping(List<ItemBike> bikesToOrder)//Maybe transfer it into Representative controller
        {
            float days = 0;
            int weeks = 0;
            int minutes = 0;
            float hours = 0;
            //updateOrderBikeList();
            SetPlanningList();
            SetBikeModelList();
            var nonPlannified = Manager.GetNonPlanifiedBikes();
            var nonPlannifiedBikes = new List<Bike>();
            //var order = orderBikeList.FirstOrDefault(x => x.orderId == orderId);
            foreach(var bike in nonPlannified)
            {
                BikeModel model = bikeModels.FirstOrDefault(x => x.color == bike[3] && x.size == Int32.Parse(bike[2]) && x.type == bike[1]);//gets the specific model
                nonPlannifiedBikes.Add(new Bike(Int32.Parse(bike[0]), model));//adds a corresponding Bike
            }
            foreach(var bike in nonPlannifiedBikes)
            {
                minutes += bike.totalTime;
            }
            foreach(var elem in bikesToOrder)
            {
                minutes += elem.GetTotalTime();
            }
            hours = minutes/ 60;
            days = hours / 24; //because 3 builders working 8 hours a day

            if (planningList.Count != 0)
            {
                string lastWeek = planningList.Last().weekName;
                string b = string.Empty;
                int lastWeekNumber=0;

                CultureInfo myCI = new CultureInfo("en-US");
                Calendar myCal = myCI.Calendar;
                CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
                DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
                var currentWeek = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);//gets the current week
                for (int i = 0; i < lastWeek.Length; i++)
                {
                    if (Char.IsDigit(lastWeek[i]))
                    {
                        b += lastWeek[i];
                    }
                }
                if (b.Length > 0)
                {
                    lastWeekNumber = int.Parse(b);
                }
                weeks = lastWeekNumber - currentWeek ;
                Console.WriteLine("CURRENT WEEK : " + currentWeek);
                Console.WriteLine("LAST PLANNED WEEK : " + lastWeekNumber + "LAST WEEK NAME : "+lastWeek);
                Console.WriteLine("DIFFERENCE : " + weeks);
            }
            weeks += (int)(Math.Ceiling(days) / 5) +1;
            Console.WriteLine("MINUTES " + minutes+ " HOURS " + hours + " DAYS " + days);
            Console.WriteLine("DIFFERENCE + NEW BIKES TO BUILD: " + weeks);

            return weeks;
        }

        internal List<string> GetDifferentModels()
        {
            SetBikeModelList();
            var diffModels = new List<string>();

            foreach (var model in bikeModels)
            {
                if (!diffModels.Contains(model.type))
                {
                    diffModels.Add(model.type);
                }
            }
            return diffModels;
        }

        internal List<string> GetDifferentsSize()
        {
            //SetBikeModelList();
            var diffSizes = new List<string>();

            /*foreach (var model in bikeModels)
            {
                if (!diffSizes.Contains(model.type))
                {
                    diffSizes.Add(model.type);
                }
            }*/
            diffSizes.Add("26");
            diffSizes.Add("28");
            return diffSizes;
        }

        internal List<string> GetDifferentUserTypes()
        {
            SetUserList();
            List<string> users = new List<string>();
            foreach (var user in userList)
            {
                var type = user.userType.FirstOrDefault(x => x.Value == true).Key;
                if (!users.Contains(type))
                {
                    users.Add(type);
                }
            }
            return users;
        }

        internal int GetMaxWorkingTimePerWeek()
        {
            SetUserList();
            int hours = 0;
            foreach(var user in userList.FindAll(x => x.userType["Assembler"] == true))
            {
                hours += user.GetMaxHoursPerWeek();
            }
            return hours;
        }



        //OTHER METHODS
        internal Dictionary<int, int> ComputeMissingPieces(ref Dictionary<int, int> partIdQuantity)
        {
            SetBikePartList();
            SetBikeModelList();
            int stockQuantity = 0;
            int quantityNeeded = 0;
            int numberNeededByBike = 1;
            Dictionary<int, int> partOrderDuplicateQuantity = new Dictionary<int, int>();          
            foreach (var bikeModel in bikeModels)
            {
                numberNeededByBike = 1;
                foreach (var bikePart in bikeModel.bikeParts)
                { 
                    if (!partOrderDuplicateQuantity.ContainsKey(bikePart.part_Id))
                    {
                        numberNeededByBike = bikeModel.bikeParts.Count(elem => elem.part_Id == bikePart.part_Id);
                        //Console.WriteLine(numberNeededByBike+"\t"+ bikePart.part_Id);
                        partOrderDuplicateQuantity.Add(bikePart.part_Id, numberNeededByBike);
                    }
                    else
                    {
                        numberNeededByBike = bikeModel.bikeParts.Count(elem => elem.part_Id == bikePart.part_Id);
                        //Console.WriteLine(numberNeededByBike + "\t" + bikePart.part_Id);
                        if (partOrderDuplicateQuantity[bikePart.part_Id]< numberNeededByBike)
                        {
                            partOrderDuplicateQuantity[bikePart.part_Id] = numberNeededByBike;
                        }                      
                    }
                }
            }
            Dictionary<int, int> partOrderQuantity = new Dictionary<int, int>();
            int currentNumberPartNeeded = 0;
            foreach (var elem in partIdQuantity)
            {
                //stockQuantity = getQuantity(elem.Key);
                stockQuantity = bikePartList.FirstOrDefault(x => x.part_Id == elem.Key).quantity;
                currentNumberPartNeeded = partOrderDuplicateQuantity[elem.Key];
                quantityNeeded = elem.Value;            // just to be clear
                int orderQuantity = 0;
                orderQuantity = quantityNeeded - stockQuantity + 10 * currentNumberPartNeeded; //ex : I have 5, need 20 => order 25
                if (orderQuantity > 0) //means there is enough stock
                {
                    partOrderQuantity.Add(elem.Key, orderQuantity);
                }
                //NEED TO ADD THIS ORDER WITH THE ID TO A LIST         
            }
            return partOrderQuantity;
        }
       

    } // end App Class
} // end namespace Bovelo