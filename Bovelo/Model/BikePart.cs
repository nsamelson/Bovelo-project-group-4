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

        public void setQuantity(ref App newApp,int quantity)//should avoid passing App here
        {
            string query = "UPDATE Bike_Parts SET Quantity=" + quantity + " WHERE Id_Bike_Parts = " + this.part_Id;
            DataBase.SendToDB(query);
        }

    }//end class BikePart

}//end namespace