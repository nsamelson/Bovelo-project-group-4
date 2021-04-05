using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Globalization;


namespace Bovelo
{
    public static class DataBase
    {
        private static string _connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";

        public static void SendToDB(string query) //is used to send anything to the DB
        {
            MySqlConnection conn = new MySqlConnection(_connStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            cmd.Dispose();
            conn.Close();
        }
        public static List<List<string>> ConnectToDB(string sql)//is used to get anything from the DB
        {
            var listFromDB = new List<List<string>>();
            MySqlConnection conn = new MySqlConnection(_connStr);
            conn.Open();
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

        public static List<List<string>> GetFromDB(string DBTable)//Gets everything from the specified Table
        {
            string sql = "SELECT * FROM " + DBTable + ";";
            return ConnectToDB(sql);
        }
        public static List<List<string>> GetFromDBSelect(string DBTable, List<string> argumentList)//Gets all the the specified columns from the specified Table
        {
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
            sql += " FROM " + DBTable + ";";
            return ConnectToDB(sql);
        }
        public static List<List<string>> GetFromDBWhere(string DBTable, List<string> argumentList, string whereClause)//Gets all the the specified columns from the specified Table with a condition
        {
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
            return ConnectToDB(sql);
        }
        public static List<List<string>> GetFromDBInnerJoin(string selectTable, string innerJoinCondition, string whereColumn, string whereCondition)
        {
            string sql = "SELECT * FROM " + selectTable + "inner join  " + innerJoinCondition + " where " + whereColumn + " = '" + whereCondition + "';";
            return ConnectToDB(sql);
        }
        public static string GetFromDBLastIdFromColumn(string table, string column)//Gets the last id of a selected table
        {
            string sql = "SELECT Id_Order FROM "+table+" ORDER BY "+ column + " DESC LIMIT 1;";
            return ConnectToDB(sql).Last()[0];//returns the last id
        }
        public static void Backup()
        {
            string MachineName1 = Environment.MachineName;
            Console.WriteLine("Your Machine Name: " + MachineName1);
            string file = "C:\\Users\\"+MachineName1+"\\Documents\\backup_"+DateTime.Today.DayOfWeek+".sql";
            using (MySqlConnection conn = new MySqlConnection(_connStr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup mb = new MySqlBackup(cmd))
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        mb.ExportToFile(@file);
                        conn.Close();
                    }
                }

            }
        }
    }
}
