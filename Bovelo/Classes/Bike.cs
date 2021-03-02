using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace Bovelo
{
    //maybe differenciate into 2 classes (bikeModel which is linked to BikePart(with totaltime,price,type)) and this class which is used only for the orders or be me more logical between app and bike...
    class Bike 
    {
        public DateTime TotalTime = DateTime.Now;
        public int Price = 0;
        public bool  isBuilt = false;
        public Dictionary<string, bool> state = new Dictionary<string, bool>();
        

        public string Type;
        public string Color;
        public int Size;

        public Bike(string Type,string Color,int Size,int price)//need to move price in bikeModel and depends from the model from the DB
        {
            this.Type = Type;
            this.Color = Color;
            this.Size = Size;
            this.Price = price;
            state.Add("New", true);
            state.Add("Active", false);
            state.Add("Closed", false);
        }

    }

}




