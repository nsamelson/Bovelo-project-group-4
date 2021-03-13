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
		public int price;
		public int totalTime;
		public string Type;
		public string Color;
		public int Size;
		public int idBikeModel;
		private List<BikePart> bikeParts;
		//private App newApp = new App();


		public BikeModel(int idBikeModel, string Type, string Color, int Size)
		{

			this.type = Type;
			this.Color = Color;
			this.Size = Size;
			this.idBikeModel = idBikeModel;
            List<string> TypeSizeColor = new List<string>();
            TypeSizeColor.Add(Type);
            TypeSizeColor.Add(Size.ToString());
            TypeSizeColor.Add(Color);
            this.bikeParts = getBikePart(TypeSizeColor);
            foreach(var elem in bikeParts)
            {
                this.price += elem.price;
                this.totalTime += elem.timeToBuild;
                Console.WriteLine(this.price);
            }
            

        }

		internal List<BikePart> getBikePart(List<string> TypeSizeColor)
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

        public List<List<string>> getFromDBWhere(string DBTable, List<string> argumentList, string whereClause)
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
        }
    }
}