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
        internal List<User> userList; //All users from DB( Representative, Assembler, ProductionManager)
        internal List<BikePart> bikePartList = new List<BikePart>();
        internal List<BikeModel> bikeModels; //All bike types (Adventure, city and explorer)
        internal List<OrderBike> orderBikeList; //takes all the orders from the DB 
        internal List<Planning> planningList; //takes all the plannings from the DB
        private List<List<string>> _linkingPartList = new List<List<string>>();
        public App(bool getAll = false, bool getUpdatable = false)
        {
            if (getAll)
            {
                this.bikePartList = getBikePartList();
                this.bikeModels = getBikeModelList();
                this.userList = getUserList();
                updateOrderBikeList();
                updatePlanningList();
                updateLinkingPartList();
            }
            if (getUpdatable)
            {
                updateOrderBikeList();
                updatePlanningList();
            }
        }

        //SET To the DB methods

        
        internal void setNewUser(User user) //is used to add a new user (for ex: a new Assembler joins the team)
        {
            string query = "INSERT INTO Users (Login, Password, Role) VALUES ('" + user.login + "','NULL','" + user.userType.FirstOrDefault(x => x.Value == true).Key + "')";
            DataBase.SendToDB(query);
            updateUserList(); //At the end of set, put a get to update App class
            //userList.Add(user); //if latency problems, uncomment this line and comment "userList = getUserList();"
        }


        //UPDATE lists from DB
        internal void updateUserList()
        {
            userList = getUserList();
        }
        internal void updateBikeModelList()
        {
            bikeModels = getBikeModelList();
        }
        internal void updateBikePartList()
        {
            bikePartList = getBikePartList();
        }
        internal void updateOrderBikeList()
        {
            orderBikeList = getOrderBikeList();
        }
        internal void updatePlanningList()
        {
            planningList = getPlanningList();
        }
        internal void updateLinkingPartList()
        {
            _linkingPartList = getLinkingPartList();
        }

        //GET from the DB methods
        internal List<List<string>> getLinkingPartList()
        {
            return DataBase.GetFromDB("Parts");
        }
        internal List<BikePart> getBikePartList()
        {
            List<BikePart> bikeParts = new List<BikePart>();
            var BikePartsFromDB = DataBase.GetFromDB("Bike_Parts");

            foreach (var row in BikePartsFromDB)
            {
                bikeParts.Add(new BikePart(Int32.Parse(row[0]), row[1], row[3], Int32.Parse(row[4]), row[5], Int32.Parse(row[6])) { quantity = Int32.Parse(row[2])});
            }

            return bikeParts;
        }

        internal List<Planning> getPlanningList() //maybe transfer into Assembler Controller
        {
            updateBikeModelList();
            List<Planning> plannings = new List<Planning>();
            var detailedSchedules = DataBase.GetFromDB("Detailed_Schedules");
            var allorders = getOrderDetails();
            /*Dictionary<string, List<string>> weeks = new Dictionary<string, List<string>>();//dictionnary of WeekName as key and List of id_OrderDetails as value
            Dictionary<string, string> assemblerPerBikeId = new Dictionary<string, string>();*/
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
                    //Console.WriteLine(string.Join("\t", bikes.Last()) + week.Key);
                }
                plannings.Add(new Planning(bikes, week.Key, id, week.Value, bikeModels));
                id++;
            }

            return plannings;

        }
        internal List<OrderBike> getOrderBikeList() //is used to get all Bike Orders NEED TO TRY
        {
            List<OrderBike> orderBikeList = new List<OrderBike>();
            var orderList = DataBase.GetFromDB("Order_Bikes");
            var orderDetailList = DataBase.GetFromDB("Order_Details");
            updateBikeModelList();

            foreach (var row in orderList)
            {
                List<List<string>> details = new List<List<string>>(orderDetailList.FindAll(x => x[6] == row[0]));//takes each lists with the same order_Id
                OrderBike newOrder = new OrderBike(row[1], details, Int32.Parse(row[0]), Convert.ToDateTime(row[3]), Convert.ToDateTime(row[4]), Int32.Parse(row[2]),bikeModels);

                orderBikeList.Add(newOrder);//row[1] is the column where the name of the client is put

            }

            return orderBikeList;
        }
        internal List<List<string>> getOrderDetails()
        {
            var orderDetailList = DataBase.GetFromDB("Order_Details");
            return orderDetailList;
        }


        internal List<User> getUserList() //is used to get all users 
        {
            var userFromDB = new List<User>();
            //createBikeModel();
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
        internal List<BikeModel> getBikeModelList() //is used to get all bike models
        {
            List<BikeModel> bikeList = new List<BikeModel>();//list to return
            List<List<string>> modelList = DataBase.GetFromDB("Bike_Model");//bikemodels from db
            if(_linkingPartList.Count == 0)
            {
                updateLinkingPartList();
            }
            if(bikePartList.Count == 0)
            {
                updateBikePartList();
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
        /*internal List<BikeModel> getBikeModelList() //is used to get all bike models
        {
            List<BikeModel> bikeList = new List<BikeModel>();
            List<List<string>> modelList = getFromDB("Bikes");
            foreach (var row in modelList)
            {
                string Type = row[1];
                var newBikeModel = new BikeModel(Int32.Parse(row[0]), Type);
                newBikeModel.setBikeParts(getBikePartList());
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
        internal List<BikePart> getSpecificBikePart(List<string> TypeSizeColor)
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
        }
        internal Dictionary<int, int> getWeekPieces(string weekName) //really with weekName ?!
        {

            List<Bike> BikesToGetPieces = new List<Bike>();

            foreach (var planning in this.getPlanningList())
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
        public void addQuantity(int value, int part_Id)
        {
            int quantity = getQuantity(part_Id);
            quantity += value;
            DataBase.SendToDB("UPDATE Bike_Parts SET Quantity =" + quantity + " WHERE Id_Bike_Parts = " + part_Id + ";");
        }
        public int getQuantity(int part_Id)
        {
            List<string> argumentList = new List<string>();
            argumentList.Add("Quantity");
            string whereclause = "Id_Bike_Parts =" + part_Id;
            List<List<string>> result = DataBase.GetFromDBWhere("Bike_Parts", argumentList, whereclause);
            int quantity = Int32.Parse(result[0][0]);
            return quantity;
        }
        internal Dictionary<int, int> computeMissingPieces(ref Dictionary<int, int> PartIdQuantity)
        {
            updateBikePartList();
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
        internal int getEstimatedTimeBeforeShipping(List<ItemBike> bikesToOrder)//Maybe transfer it into Representative controller
        {
            float days = 0;
            int weeks = 0;
            int minutes = 0;
            float hours = 0;
            //updateOrderBikeList();
            updatePlanningList();
            updateBikeModelList();
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
        internal List<Bike> getStockBikesID()
        {
            var stockBikeID = DataBase.GetFromDBWhere("Order_Bikes", new List<string>() { "Id_Order" }, "Customer_Name='Stock'");
            List<string> bikes = stockBikeID.SelectMany(x => x).ToList();
            Console.WriteLine(string.Join("\t", bikes));

            var stockBike = DataBase.GetFromDBWhere("Order_Details", new List<string>() { "Bike_Type", "Bike_Size", "Bike_Color","Id_Order"}, "Bike_Status = 'Closed'");

            //List<string> stock = new List<string>();
            List<Bike> test = new List<Bike>();
            foreach(var row in stockBike)
            {
                if (bikes.Any(x => x == row[3]))
                {
                    test.Add(new Bike(0, new BikeModel(row[0],row[2],Int32.Parse(row[1]))));
                    //stock.Add(string.Join("\t", row));                   
                }
            }
            //Console.WriteLine(string.Join("\n", test[0].Size));
            return test;
        }
        internal void createInvoice(string client,List<string> column,List<List<String>> Data)
        {
            string date = DateTime.Now.ToString();
            date = date.Replace('/', '_');
            date = date.Replace(' ', '_');
            date = date.Replace(':', '_');
            string path = Environment.CurrentDirectory;
            PdfWriter writer = new PdfWriter(@"../../facture/" + client + "_" + date + ".pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Header
            Paragraph header = new Paragraph("Bovelo")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20);

            // New line
            Paragraph newline = new Paragraph(new Text("\n"));
            document.Add(newline);
            document.Add(header);
            // Add sub-header
            Paragraph subheader = new Paragraph(client)
               .SetTextAlignment(TextAlignment.CENTER)
               .SetFontSize(15);
            document.Add(subheader);
            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);
            // Add paragraph1
            Paragraph paragraph1 = new Paragraph("Récapitulatif de commande " + date.Substring(0,8));
            paragraph1.SetTextAlignment(TextAlignment.CENTER);
            document.Add(paragraph1);
            document.Add(ls);
            document.Add(newline);

            // Table
            Table table = new Table(column.Count(), false);

            foreach (var elem in column)
            {
                Cell cell = new Cell(1, 1)
                    .SetBackgroundColor(ColorConstants.GRAY)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetWidth(100)
                    .Add(new Paragraph(elem));
                table.AddCell(cell);
            }
            int price = 0;
            foreach (var item in Data)
            {
                foreach (var value in item)
                {
                    Cell cell = new Cell(1, 1)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .Add(new Paragraph(value));
                    table.AddCell(cell);
                    if (value==item[item.Count()-1])
                    {
                        price += Int32.Parse(value);
                    }
                }
            }
            document.Add(table);
            //Total
            document.Add(newline);
            document.Add(ls);
            document.Add(newline);
            Paragraph paragraph2 = new Paragraph("Total : " + price);
            paragraph2.SetTextAlignment(TextAlignment.RIGHT);
            document.Add(paragraph2);
            // Page numbers
            int n = pdf.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format(i + "/" + n)),
                   559, 806, i, TextAlignment.RIGHT,
                   VerticalAlignment.TOP, 0);
            }

            // Close document
            document.Close();
        }       
    } // end App Class
} // end namespace Bovelo