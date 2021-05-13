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

    }
}