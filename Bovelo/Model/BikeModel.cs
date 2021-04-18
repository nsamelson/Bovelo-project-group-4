using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Bovelo
{
	public class BikeModel
	{
		public string type;
		public int price = 0;
        public int size;
        public string color;
		public int totalTime;//time to build a bike
		public int idBikeModel;
		internal List<BikePart> bikeParts;


		public BikeModel( string type,string color,int size)
		{
			this.type = type;
            this.color = color;
            this.size = size;
            /*this.idBikeModel = idBikeModel;*/
        }
        internal void SetPriceAndTime()//is used to set the total price of a bike
        {
            foreach (var elem in bikeParts)
            {
                this.price += elem.price;
                this.totalTime += elem.timeToBuild;
                
            }
            //Console.WriteLine(this.type +" | Price : "+this.Price+" | Time : "+this.totalTime);
        }
        /*internal virtual void setBikeParts(List<BikePart> allParts)//used to set the bikeparts needed to assemble a bike depending on its type
        {
            List<int> partsId = new List<int>() {1,2,15,16,17,17,17,17,18,27,27,28,29,29,31,33,34,40,40,42,52};
            switch (Type)
            {
                case "City":
                    partsId.AddRange(new List<int>() { 5, 21, 30, 35, 35, 39 });//by default black and 26"
                    break;
                case "Explorer":
                    partsId.AddRange(new List<int>() {5,25,30,37,37,39 });//by default black and 26"
                    break;
                case "Adventure":
                    partsId.AddRange(new List<int>() {11,25,37,37});//by default black and 26"
                    break;
                default:
                    //errror
                    break;
            }
            //bikeParts = allParts.FindAll(part => partsIndices.Contains(part.id));//adds each part into the list, without duplicates
            bikeParts = partsId.Select(id => allParts.First(part => part.part_Id == id)).ToList();//adds each part into the list, even duplicates
            //setPriceAndTime(); //after getting all the parts, set the price and time to build
            
        }*/
        /*internal List<BikePart> getBikePart(List<string> TypeSizeColor)
		{
			List<string> query = new List<string>();
			query.Add("*");
			List<List<string>> bikePart = new List<List<string>>();
			bikePart = getFromDBWhere("Bike_Parts", query, "Id_Bike_Parts IN ( SELECT Id_Bike_Parts FROM Parts WHERE Bikes_Id IN(SELECT idBike_Model FROM Bike_Model WHERE Color = '" + TypeSizeColor[2] + "' AND Type_Model = '" + TypeSizeColor[0] + "' AND Size = '" + TypeSizeColor[1] + "'))");
			List<BikePart> bikePartList = new List<BikePart>();
			foreach (var line in bikePart)
			{
				bikePartList.Add(new BikePart(Int32.Parse(line[0]), line[1], line[3], Int32.Parse(line[4]), line[5], Int32.Parse(line[6])));
			}
			return bikePartList;
		}// end getbikepart

        public List<List<string>> getFromDBWhere(string DBTable, List<string> argumentList, string whereClause)//TO REMOVE
        {
            var listFromDB = new List<List<string>>();
            string connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            //Console.WriteLine("Connecting to MySQL at table " + DBTable + "...");
            conn.Open();
            string sql = "SELECT ";
            for (int i = 0; i < argumentList.Count; i++)
            {
                if (i != argumentList.Count - 1)
                {
                    sql += argumentList[i] + ",";
                }
                else
                {
                    sql += argumentList[i];
                }
            }

            sql += " FROM " + DBTable + " WHERE " + whereClause + ";";
            //Console.WriteLine(sql);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var col = new List<string>();
                for (int j = 0; j < rdr.FieldCount; j++)
                {
                    col.Add(rdr[j].ToString());
                }
                listFromDB.Add(col);
            }
            rdr.Close();
            conn.Close();
            return listFromDB;
        }*/
    }
}