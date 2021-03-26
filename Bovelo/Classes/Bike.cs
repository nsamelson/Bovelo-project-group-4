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
        public string assembler;
        public Bike(int bikeId,BikeModel bikeModel) //ID OF THE BIKE NOT BIKEMODEL
        {
            this.bikeId = bikeId;
            _model = bikeModel;
            /*this.Type = Type;
            this.Size = Size;
            this.Color = Color;*/
            //call app to get the model of the bike


            /*App newApp = new App();
            var bikeModels = newApp.getBikeModelList();
            _model = bikeModels.FirstOrDefault(x => x.Color == Color && x.Size == Size && x.Type == Type);*/

            Type = _model.Type;
            Color = _model.Color;
            Size = _model.Size;
            Price = _model.Price;
            TotalTime = _model.TotalTime;
            bikeParts = _model.bikeParts;
            

        }
        public void setNewState(string status,string assemblerName ="")
        {
            state[getCurrentState()] = false;// change the actual state to false
            if (status == "New" || status == "Active" || status == "Closed")
            {
                state[status] = true;
            }
            else { }//error
            if (assemblerName != "")
            {
                assembler = assemblerName;
            }
            
        }
        public string getCurrentState()
        {
            var status = state.FirstOrDefault(x => x.Value == true).Key;
            return status;
        }
    }

}




