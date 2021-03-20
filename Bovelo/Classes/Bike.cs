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
        public Dictionary<string, bool> state = new Dictionary<string, bool>() { { "New",true},{ "Active", false },{ "Closed", false } };
        public string Color;
        public int Size;
        public string Type;
        public int Price;
        public int TotalTime;
        public int bikeId;//MAYBE REMOVE
        public List<BikePart> bikeParts;
        private BikeModel _model;
        public Bike(int bikeId,string Type, string Color, int Size) //: base( Type,Color,Size)//ID OF THE BIKE NOT BIKEMODEL
        {
            this.bikeId = bikeId;
            App newApp = new App();
            var bikeModels = newApp.getBikeModelList();
            _model = bikeModels.FirstOrDefault(x => x.Color == Color && x.Size == Size && x.Type == Type);

            this.Type = _model.Type;
            this.Color = _model.Color;
            this.Size = _model.Size;
            this.Price = _model.Price;
            this.TotalTime = _model.TotalTime;
            this.bikeParts = _model.bikeParts;
        }


        /*internal override void setBikeParts(List<BikePart> allParts)
        {
            List<int> partsId = new List<int>() { 1, 2, 15, 16, 17, 17, 17, 17, 18, 27, 27, 28, 29, 29, 33, 34, 42, 52 };
            if (Size == 26)
            {
                partsId.AddRange(new List<int>() { 31, 40, 40 });//adds fork and wheels
                switch (Type)
                {
                    case "City":
                        //partsId.AddRange(new List<int>() { 5, 21, 30, 35, 35, 39 });//by default black and 26"
                        switch (Color)
                        {
                            case "black": partsId.AddRange(new List<int>() { 5, 21, 30, 35, 35, 39 }); break;
                            case "red": partsId.AddRange(new List<int>() { 3, 19, 30, 35, 35, 39 }); break;
                            case "blue": partsId.AddRange(new List<int>() { 4, 20, 30, 35, 35, 39 }); break;
                        }
                        break;
                    case "Explorer":
                        //partsId.AddRange(new List<int>() { 5, 25, 30, 37, 37, 39 });//by default black and 26"
                        switch (Color)
                        {
                            case "black": partsId.AddRange(new List<int>() { 5, 25, 30, 37, 37, 39 }); break;
                            case "red": partsId.AddRange(new List<int>() { 3, 25, 30, 37, 37, 39 }); break;
                            case "blue": partsId.AddRange(new List<int>() { 4, 25, 30, 37, 37, 39 }); break;
                        }
                        break;
                    case "Adventure":
                        //partsId.AddRange(new List<int>() { 11, 25, 37, 37 });//by default black and 26"
                        switch (Color)
                        {
                            case "black": partsId.AddRange(new List<int>() { 11, 25, 37, 37 }); break;
                            case "red": partsId.AddRange(new List<int>() { 9, 25, 37, 37 }); break;
                            case "blue": partsId.AddRange(new List<int>() { 10, 25, 37, 37 }); break;
                        }
                        break;
                    default:
                        //errror
                        break;
                }
            }
            else if (Size == 28)
            {
                partsId.AddRange(new List<int>() { 32, 41, 41 });//adds fork and wheels
                switch (Type)
                {
                    case "City":
                        switch (Color)
                        {
                            case "black": partsId.AddRange(new List<int>() { 8, 24, 30, 36, 36, 39 }); break;
                            case "red": partsId.AddRange(new List<int>() { 6, 22, 30, 36, 36, 39 }); break;
                            case "blue": partsId.AddRange(new List<int>() { 7, 23, 30, 36, 36, 39 }); break;
                        }
                        break;
                    case "Explorer":
                        switch (Color)
                        {
                            case "black": partsId.AddRange(new List<int>() { 8, 26, 30, 38, 38, 39 }); break;
                            case "red": partsId.AddRange(new List<int>() { 6, 26, 30, 38, 38, 39 }); break;
                            case "blue": partsId.AddRange(new List<int>() { 7, 26, 30, 38, 38, 39 }); break;
                        }
                        break;
                    case "Adventure":
                        switch (Color)
                        {
                            case "black": partsId.AddRange(new List<int>() { 14, 26, 38, 38 }); break;
                            case "red": partsId.AddRange(new List<int>() { 12, 26, 38, 38 }); break;
                            case "blue": partsId.AddRange(new List<int>() { 13, 26, 38, 38 }); break;
                        }
                        break;
                    default:
                        //errror
                        break;
                }                
            }
            //bikeParts = allParts.FindAll(part => partsIndices.Contains(part.id));//adds each part into the list, without duplicates
            bikeParts = partsId.Select(id => allParts.First(part => part.part_Id == id)).ToList();//adds each part into the list, even duplicates
            setPriceAndTime(); //after getting all the parts, set the price and time to build
        }*/

    }

}




