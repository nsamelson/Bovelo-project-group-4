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
        public int price = 0;
        public string name;
        public string location;
        //public string provider;
        //public int part_Id = 0;

        public BikePart(string name,int timeToBuild, int price, string location)
        {
            this.name = name;
            this.timeToBuild = timeToBuild;
            this.price = price;
            this.location = location;
            //this.part_Id = part_Id;
        }
 
    }//end class BikePart

}//end namespace