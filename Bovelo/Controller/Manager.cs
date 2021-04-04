using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovelo
{
    public static class Manager
    {
        //SET METHODS
        public static void SetNewBikeModel(string type, int size, string color)//is used to add a new model (for ex: Electric)
        {
            string query = "INSERT INTO Bike_Model (Color,Size,Type_Model) VALUES ('" + color + "','" + size + "', '" + type + "')";
            DataBase.SendToDB(query);
        }
        public static void SetLinkBikePartsToBikeModel(int idBikeModel, List<int> idBikeParts)//id of the bikeModel and List of id's of BikeParts
        {
            foreach (var idPart in idBikeParts)
            {
                string query = "INSERT INTO Parts (Id_Bike_Parts,Bikes_Id) VALUES ('" + idPart + "',' " + idBikeModel + "')";
                DataBase.SendToDB(query);
            }
        }
        public static void SetNewPlanning(List<List<string>> planningCartList, string week)//NEED TO SET THE TABLES
        {
            int orderDetailsId; //NEED TO LINK ORDER DETAILS WITH THE CORRECT ID

            foreach (var bikesToBuild in planningCartList)
            {
                orderDetailsId = Int32.Parse(bikesToBuild[0]);
                string queryPD = "INSERT INTO Detailed_Schedules (Week_Name,Id_Order_Details) VALUES('" + week + "' , '" + orderDetailsId + "'); ";
                DataBase.SendToDB(queryPD);
            }
        }
        public static void SetNewBikePart(string name,string storageLocation, int price , int size = 0, string color = "null")//is used to create a new bikePart : takes name, size(0 for default,26,28) and color (null for default,black,red,blue)PROBLEM : Price/time differs from color and sizes
        {
            var rand = new Random();
            string provider = RandomString(8);
            int timeToBuild = rand.Next(1, 15);
            string RandomString(int length)//create a random string of letters and numbers
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[rand.Next(s.Length)]).ToArray());
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
        public static void DeletePlanifiedBike(int idOrderDetail, string week)
        {
            string deleteQuery = "delete from Detailed_Schedules where Week_Name = '" + week + "' and Id_Order_Details='" + idOrderDetail + "';";
            DataBase.SendToDB(deleteQuery);
        }
        public static void UpdateSchedule(int id, string newWeek, string currentWeek)
        {
            string modifyQuery = "UPDATE Detailed_Schedules SET Week_name = '" + newWeek + "' WHERE Id_Order_Details = '" + id + "' and Week_Name = '" + currentWeek + "';";
            DataBase.SendToDB(modifyQuery);
        }

        //GET METHODS
        public static List<List<string>> GetAssemblerWork(string assemblerName)
        {
            string sql = "SELECT * FROM Order_Details inner join  Bovelo.Detailed_Schedules on Detailed_Schedules.Id_Order_Details = Order_Details.Id_Order_Details where Order_Details.Id_Order_Details In (select Id_Order_Details from Detailed_Schedules) and Assembled_by = '" + assemblerName + "';";
            var assemblerWork = DataBase.ConnectToDB(sql);
            return assemblerWork;
        }
        public static List<List<string>> GetAssemblers()
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
        public static List<List<string>> GetPlanifiedBikesByWeekName(string week)
        {
            string sql = "SELECT * FROM Order_Details inner join  Bovelo.Detailed_Schedules on Detailed_Schedules.Id_Order_Details = Order_Details.Id_Order_Details where Order_Details.Id_Order_Details In (select Id_Order_Details from Detailed_Schedules) and Week_Name = '" + week + "';";
            var planifiedByWeek = DataBase.ConnectToDB(sql);
            return planifiedByWeek;
        }
    }
}
