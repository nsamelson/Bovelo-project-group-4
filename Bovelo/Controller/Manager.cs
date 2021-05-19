using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using System.Text;
using System.Threading.Tasks;


namespace Bovelo
{
    public static class Manager
    {
        //SET METHODS
        public static void SetNewBikePart(string name, string storageLocation, int price, int size = 0, string color = "null")//is used to create a new bikePart : takes name, size(0 for default,26,28) and color (null for default,black,red,blue)
        {
            var rand = new Random();
            string provider = RandomString(8);
            int timeToBuild = rand.Next(1, 15);
            string RandomString(int length)//create a random string of letters and numbers
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length).Select(s => s[rand.Next(s.Length)]).ToArray());
            }

            if (color == "null" && size != 0)
            {
                name += "_" + size.ToString();
            }
            else if (color != "null" && size == 0)
            {
                name += "_" + color;
            }
            else if (color != "null" && size != 0)
            {
                name += "_" + color + "_" + size.ToString();
            }

            string query = "INSERT INTO Bike_Parts (Bike_Parts_Name,Quantity,Location,Price,Provider,Time_To_Build) VALUES('" + name + "' , '" + 0 + "' , '" + storageLocation + "' , '" + price + "' , '" + provider + "' , '" + timeToBuild + "'); ";
            DataBase.SendToDB(query);
        }
        public static void SetNewBikeModel(string type, int size, string color)//is used to add a new model (for ex: Electric)
        {
            string query = "INSERT INTO Bike_Model (Color,Size,Type_Model) VALUES ('" + color + "','" + size + "', '" + type + "')";
            DataBase.SendToDB(query);
        }
        public static void SetLinkBikePartsToBikeModel(int idBikeModel, List<int> idBikeParts)//Links the BikeParts with a specified model
        {
            foreach (var idPart in idBikeParts)
            {
                string query = "INSERT INTO Parts (Id_Bike_Parts,Bikes_Id) VALUES ('" + idPart + "',' " + idBikeModel + "')";
                DataBase.SendToDB(query);
            }
        }
        public static void SetNewPlanning(List<List<string>> planningCartList, int week)//Sets a new planning of the week
        {
            int orderDetailsId;
            foreach (var bikesToBuild in planningCartList)
            {
                Console.WriteLine("Week: " + week +  "Order Id : " + bikesToBuild[0]  );
                orderDetailsId = Int32.Parse(bikesToBuild[0]);
                string queryPD = "INSERT INTO Detailed_Schedules (Week_Name,Id_Order_Details) VALUES('" + week + "' , '" + orderDetailsId + "'); ";
                DataBase.SendToDB(queryPD);
            }
        }
        public static void DeletePlanifiedBike(int idOrderDetail, string week)//Deletes a bike from a specified schedule
        {
            string deleteQuery = "delete from Detailed_Schedules where Week_Name = '" + week + "' and Id_Order_Details='" + idOrderDetail + "';";
            DataBase.SendToDB(deleteQuery);
        }
        public static void UpdateSchedule(int id, string newWeek, string currentWeek)
        {
            string modifyQuery = "UPDATE Detailed_Schedules SET Week_name = '" + newWeek + "' WHERE Id_Order_Details = '" + id + "' and Week_Name = '" + currentWeek + "';";
            DataBase.SendToDB(modifyQuery);
        }
        public static void AddQuantityToBikePart(int value, int part_Id)
        {
            int quantity = GetQuantity(part_Id);
            quantity += value;
            DataBase.SendToDB("UPDATE Bike_Parts SET Quantity =" + quantity + " WHERE Id_Bike_Parts = " + part_Id + ";");
        }
        public static void AddNewFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public static void updateOrderStatus(List<List<string>> orderStatus)
        {
            foreach (var elem in orderStatus)
            {
                DataBase.SendToDB("UPDATE Order_Detailed_Part SET State='Received' WHERE Id_Order=" + elem[0] + " AND idOrder_Detailed_Part=" + elem[1] + " AND Id_Bike_Parts =" + elem[2] + " AND Quantity=" + elem[3] + " ;");
                //Console.WriteLine(elem[0] + " " + elem[1] + " " + elem[2] + " " + elem[3]);
            }

            
        }

        //GET METHODS
        public static List<List<string>> GetAssemblerWork(string assemblerName)//Gets the work of the selected Assembler
        {
            string sql = "SELECT * FROM Order_Details inner join  Bovelo.Detailed_Schedules on Detailed_Schedules.Id_Order_Details = Order_Details.Id_Order_Details where Order_Details.Id_Order_Details In (select Id_Order_Details from Detailed_Schedules) and Assembled_by = '" + assemblerName + "';";
            var assemblerWork = DataBase.ConnectToDB(sql);
            return assemblerWork;
        }
        public static List<List<string>> GetAssemblers()//Gets all users where the role is an Assembler
        {
            string sql = "SELECT Login FROM Bovelo.Users where Role = 'Assembler';";
            var Assemblers = DataBase.ConnectToDB(sql);
            return Assemblers;
        }
        public static List<List<string>> GetPlanifiedBikes()
        {
            string sql = "SELECT * FROM Order_Details inner join  Bovelo.Detailed_Schedules on Detailed_Schedules.Id_Order_Details = Order_Details.Id_Order_Details where Order_Details.Id_Order_Details In (select Id_Order_Details from Detailed_Schedules);";
            var Planified = DataBase.ConnectToDB(sql);
            return Planified;
        }
        public static List<List<string>> GetNonPlanifiedBikes()
        {
            string sql = "Select * from Order_Details where Id_Order_Details Not In (select Id_Order_Details from Detailed_Schedules);";
            var nonPlanified = DataBase.ConnectToDB(sql);
            return nonPlanified;
        }
        public static string GetClientName(int idOrder)
        {
            string sql = "SELECT Customer_Name from Order_Bikes where Id_Order=" + idOrder + ";";
            var Name = DataBase.ConnectToDB(sql);
            return Name[0][0];
        }
        public static List<List<string>> GetPlanifiedBikesByWeekName(string week)
        {
            string sql = "SELECT * FROM Order_Details inner join  Bovelo.Detailed_Schedules on Detailed_Schedules.Id_Order_Details = Order_Details.Id_Order_Details where Order_Details.Id_Order_Details In (select Id_Order_Details from Detailed_Schedules) and Week_Name = '" + week + "';";
            var planifiedByWeek = DataBase.ConnectToDB(sql);
            return planifiedByWeek;
        }
        public static List<List<string>> GetPlanifiedWeekName()
        {
            string sql = "SELECT DISTINCT Week_Name FROM Bovelo.Detailed_Schedules;";
            var planifiedWeekName = DataBase.ConnectToDB(sql);
            return planifiedWeekName;
        }
        public static List<List<string>> getWeekName(string builder)
        {
            string sql = "SELECT distinct Week_Name FROM Bovelo.Detailed_Schedules where Assembled_by = '" + builder + "';";
            var weekName = DataBase.ConnectToDB(sql);
            return weekName;
        }
        public static int GetQuantity(int part_Id)
        {
            List<string> argumentList = new List<string>() { "Quantity" };
            string whereclause = "Id_Bike_Parts =" + part_Id;
            List<List<string>> result = DataBase.GetFromDBWhere("Bike_Parts", argumentList, whereclause);
            int quantity = Int32.Parse(result[0][0]);
            return quantity;
        }
        public static List<List<string>> GetUser()
        {
            string sql = "SELECT Login,Role FROM  Users;";
            var LoginRole = DataBase.ConnectToDB(sql);
            return LoginRole;
        }

        public static List<List<string>> GetOrder(string IDOrder)
        {
            string sql = "SELECT * FROM Bovelo.Order_Details WHERE Bike_Status='New' AND Id_Order=" + IDOrder + ";";
            var order = DataBase.ConnectToDB(sql);
            return order;
        }
        public static void SwapIdBike(int IdBikeStock, int IdBikeOrder)
        {
            string sql_1 = "SELECT * FROM Order_Details WHERE ID_Order_Details=" + IdBikeStock + ";";
            var bikeStock = DataBase.ConnectToDB(sql_1);
            string sql_0 = "DELETE FROM Order_Details WHERE Id_Order_Details=" + IdBikeStock + ";";
            DataBase.SendToDB(sql_0);
            if (bikeStock.Count() != 0)
            {
                string sql_2 = "UPDATE Order_Details SET " +
                                                  "Id_Order_Details =" + bikeStock[0][0] +
                                                  ",Bike_Type ='" + bikeStock[0][1] +
                                                  "',Bike_Size ='" + bikeStock[0][2] +
                                                  "',Bike_Color ='" + bikeStock[0][3] +
                                                  "',Price =" + bikeStock[0][4] +
                                                  ",Bike_Status ='" + bikeStock[0][5] +
                                                  "' WHERE Id_Order_Details = " + IdBikeOrder + ";";
                DataBase.SendToDB(sql_2);
            }

        }
        internal static List<Bike> GetBikesInStock()
        {
            string sql = "SELECT * FROM Bovelo.Order_Details WHERE Bike_Status='Closed' AND Id_Order IN (SELECT Id_Order from Order_Bikes WHERE Customer_Name='Stock');";
            var stockOfBike = DataBase.ConnectToDB(sql);
            List<Bike> bikesInStock = new List<Bike>();
            foreach (var row in stockOfBike)
            {
                bikesInStock.Add(new Bike(Int32.Parse(row[0]), new BikeModel(row[1], row[3], Int32.Parse(row[2]))));
            }
            return bikesInStock;
        }
        internal static int GetQuantityClosed(string type,int size,string color,string idOrder)
        {
            string sql = "SELECT COUNT(*) FROM Bovelo.Order_Details WHERE Bike_Type='"+type+"' AND Bike_Size="+size+" AND Bike_Color ='"+color+ "' AND (Bike_Status='Closed' OR Bike_Status='Active') AND Id_Order=" + idOrder+";";
            var stockOfBike = DataBase.ConnectToDB(sql);
            return Int32.Parse(stockOfBike[0][0]);
        }
        internal static int GetQuantity(string type, int size, string color, string idOrder)
        {
            string sql = "SELECT COUNT(*) FROM Bovelo.Order_Details WHERE Bike_Type='" + type + "' AND Bike_Size=" + size + " AND Bike_Color ='" + color + "' AND Id_Order=" + idOrder + ";";
            var stockOfBike = DataBase.ConnectToDB(sql);
            return Int32.Parse(stockOfBike[0][0]);
        }
        public static void ReplaceBikeFromTheStock(List<string> bikeType, int numberOfBike, int orderId)
        {
            var stockOfBikes = Manager.GetBikesInStock();
            var Order = Manager.GetOrder(orderId.ToString());
            Dictionary<int, int> toSwap = new Dictionary<int, int>();
            foreach (var bikeInStock in stockOfBikes)
            {
                if (bikeInStock.type == bikeType[0] && bikeInStock.color == bikeType[1] && bikeInStock.size.ToString() == bikeType[2])
                {
                    foreach (var bikeOrder in Order)
                    {
                        if (bikeOrder[1].Equals(bikeType[0]) && bikeOrder[2].Equals(bikeType[2]) && bikeOrder[3].Equals(bikeType[1]))
                        {
                            if (!toSwap.ContainsKey(bikeInStock.bikeId))
                            {
                                if (!toSwap.ContainsValue(Int32.Parse(bikeOrder[0])))
                                {
                                    toSwap.Add(bikeInStock.bikeId, Int32.Parse(bikeOrder[0]));
                                }                               
                            }
                        }
                    }
                }              
            }
            int i = 0;
            foreach (var elem in toSwap)
            {
                if (i < numberOfBike)
                {
                    SwapIdBike(elem.Key, elem.Value);
                    i++;
                }
            }
        }
    }
}
