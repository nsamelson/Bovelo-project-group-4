using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Bovelo 
{
    class BikePart
    {
        public int timeToBuild = 0;
        public string provider;
        public int price = 0;
        public string location;
        public string name;
        public int part_Id = 0;
        public int quantity=0;
        public BikePart(int part_Id,string name, string location,int price, string provider, int timeToBuild)
        {
            this.name = name;
            this.timeToBuild = timeToBuild;
            this.price = price;
            this.location = location;
            this.part_Id = part_Id;
            this.provider = provider;
        }
        static public int getQuantity(int ID)
        {
            List<List<string>> result = DataBase.GetFromDBWhere("Bike_Parts",new List<string>{ "Quantity" }, "Id_Bike_Parts =" + ID);
            return Int32.Parse(result[0][0]);
        }
        public void setQuantity(ref App newApp,int quantity)//should avoid passing App here
        {
            string query = "UPDATE Bike_Parts SET Quantity=" + quantity + " WHERE Id_Bike_Parts = " + this.part_Id;
            DataBase.SendToDB(query);
        }

        static public void substractClosedBike(string data)
        {
            string[] partsToUpdate = data.Split('|','\n');
            int i = 0;
            int quantity = 0;
            foreach (var elem in partsToUpdate)
            {
                Console.WriteLine("substring :" + elem +", i value :"+i);
                if (i==0 && elem != "")
                {
                    quantity = getQuantity(Int32.Parse(elem));
                    quantity--;
                    string query = "UPDATE Bike_Parts SET Quantity=" + quantity + " WHERE Id_Bike_Parts = " + elem;
                    DataBase.SendToDB(query);
                }
                i++;
                if (i == 3) { i = 0; }               
            }
        }
    }//end class BikePart

}//end namespace