using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovelo
{
    public static class Assembler
    {
        public static void UpdateSatus(int id, string status, string user, string started, string finished)
        {

            string query = "UPDATE Order_Details SET Bike_Status = '" + status + "'  WHERE Id_Order_Details = '" + id + "' ;" +
                           "UPDATE Detailed_Schedules SET Assembled_by = '" + user + "', Started = '" + started + "', Finished = '" + 
                           finished + "'  WHERE Id_Order_Details = '" + id + "' ;";

            DataBase.SendToDB(query);
        }

    }
}
