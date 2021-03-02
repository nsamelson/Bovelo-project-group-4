namespace Bovelo 
{
    class BikePart
    {
        public int timeToBuild = 0;
        public int price = 0;
        public string partName = " ";
        public string location = " ";
        public string provider = " ";
        public int part_Id = 0;

        public BikePart(string partName,int timeToBuild, int price, string location, int part_Id)
        {
            this.partName = partName;
            this.timeToBuild = timeToBuild;
            this.price = price;
            this.location = location;
            this.part_Id = part_Id;
        }
        

        //public Storage storage_information = new Storage();


    }



}