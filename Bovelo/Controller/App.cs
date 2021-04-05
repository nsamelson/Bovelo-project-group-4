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
        internal void SetNewUser(User user) //Creates a new User (for ex: a new Assembler joins the team)
        {
            string query = "INSERT INTO Users (Login, Password, Role) VALUES ('" + user.login + "','NULL','" + user.userType.FirstOrDefault(x => x.Value == true).Key + "')";
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
            var BikePartsFromDB = DataBase.GetFromDB("Bike_Parts");

            foreach (var row in BikePartsFromDB)
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
                        bikePartsIds.Add(Int32.Parse(part[0]));
                    }
                }
                bikePartsIds.Sort();
                newBikeModel.bikeParts = bikePartsIds.Select(x => bikePartList.First(part => part.part_Id == x)).ToList(); //FOR DUPLICATES 
                //newBikeModel.bikeParts = bikePartList.FindAll(part => bikePartsIds.Contains(part.part_Id));
                newBikeModel.setPriceAndTime();
                bikeList.Add(newBikeModel);
            }
            return bikeList;
        }
        internal Dictionary<int, int> GetWeekPieces(string weekName) //really with weekName ?!
        {

            List<Bike> BikesToGetPieces = new List<Bike>();

            foreach (var planning in this.GetPlanningList())
            {
                if (planning.weekName == weekName)
                {
                    BikesToGetPieces = planning.getBikesToBuild();
                }
            }
            List<int> differentPartsId = new List<int>();
            List<int> allPartsId = new List<int>();
            foreach (var bike in BikesToGetPieces)
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
                BikeModel model = bikeModels.FirstOrDefault(x => x.Color == bike[3] && x.Size == Int32.Parse(bike[2]) && x.Type == bike[1]);//gets the specific model
                nonPlannifiedBikes.Add(new Bike(Int32.Parse(bike[0]), model));//adds a corresponding Bike
            }
            foreach(var bike in nonPlannifiedBikes)
            {
                minutes += bike.TotalTime;
            }
            foreach(var elem in bikesToOrder)
            {
                minutes += elem.getTotalTime();
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

        //OTHER METHODS
        internal Dictionary<int, int> ComputeMissingPieces(ref Dictionary<int, int> PartIdQuantity)
        {
            SetBikePartList();
            int stockQuantity = 0;
            int quantityNeeded = 0;
            Dictionary<int, int> partOrderQuantity = new Dictionary<int, int>();
            foreach (var elem in PartIdQuantity)
            {
                //stockQuantity = getQuantity(elem.Key);
                stockQuantity = bikePartList.FirstOrDefault(x => x.part_Id == elem.Key).quantity;
                quantityNeeded = elem.Value;            // just to be clear
                int orderQuantity = 0;
                orderQuantity = quantityNeeded - stockQuantity + 10; //ex : I have 5, need 20 => order 25
                if (orderQuantity > 0) //means there is enough stock
                {
                    partOrderQuantity.Add(elem.Key, orderQuantity);
                }
                //NEED TO ADD THIS ORDER WITH THE ID TO A LIST         
            }
            return partOrderQuantity;
        }
        /*internal List<List<string>> GetOrderDetails()
        {
            var orderDetailList = DataBase.GetFromDB("Order_Details");
            return orderDetailList;
        }   */
        /*internal List<BikeModel> GetBikeModelList() //is used to get all bike models
        {
            List<BikeModel> bikeList = new List<BikeModel>();
            List<List<string>> modelList = getFromDB("Bikes");
            foreach (var row in modelList)
            {
                string Type = row[1];
                var newBikeModel = new BikeModel(Int32.Parse(row[0]), Type);
                newBikeModel.setBikeParts(GetBikePartList());
                bikeList.Add(newBikeModel);
            }
            return bikeList;
        }*/
        /*internal List<string> getBikePartInvoice(List<OrderBike> orderBikeList)
        {
            string path = @"../../Classes/list_part.txt";
            IEnumerable<string> line = File.ReadLines(path);
            var BikeType =new Dictionary<int, List<string>>(); //  to stock data
            List<string> identity = new List<string>();        //  to add data to dict
            int e = 0;                                         //  key index 
            List<string> bikePart = new List<string>();        //  return value
            foreach (var elem in line)
            {
                int i = 0;
                int word = 0;
                string currentWord = "";                
                foreach (var character in elem)                             // reading word
                {
                    currentWord = "";
                    while (character != ';')                   // reading char
                    {
                        currentWord += character;                // concatenate char to make word
                        i++;
                    }
                    identity.Add(currentWord);                 // add word to list string
                    word++;                                    // next word
                    i++;                                       // pass ";" char
                    }
                BikeType.Add(e, identity);                     // add to dict list of word
                identity = new List<string>();                 // reset list of word
                e++;
            }

            foreach (var order in orderBikeList)
            {
                foreach (var bike in order.bikeList)
                {
                    
                    foreach (var elem in BikeType.Values)
                    {
                        if (bike.Type == elem[0])              // finding parts with goods size,type,color
                            if (bike.Size.ToString() == elem[1])                         
                                if (bike.Color == elem[2])
                                {
                                    for(int i = 2; i < 15; i++)
                                    { 
                                        bikePart.Add(elem[i]);
                                    }
                                }                                                               
                    }
                }
            }

            return bikePart;
        }*/
        /*internal List<BikePart> getSpecificBikePart(List<string> TypeSizeColor)
        {
            List<string> query = new List<string>();
            query.Add("*");
            List<List<string>> bikePart = new List<List<string>>();
            bikePart = DataBase.GetFromDBWhere("Bike_Parts", query, "Id_Bike_Parts IN ( SELECT Id_Bike_Parts FROM Parts WHERE Bikes_Id IN(SELECT idBike_Model FROM Bike_Model WHERE Color = '" + TypeSizeColor[2] + "' AND Type_Model = '" + TypeSizeColor[0] + "' AND Size = '" + TypeSizeColor[1] + "'))");
            List<BikePart> bikePartList = new List<BikePart>();
            foreach (var line in bikePart)
            {
                bikePartList.Add(new BikePart(Int32.Parse(line[0]), line[1], line[3], Int32.Parse(line[4]), line[5], Int32.Parse(line[6])));
            }
            return bikePartList;
        }*/

    } // end App Class
} // end namespace Bovelo