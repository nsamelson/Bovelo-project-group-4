using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Globalization;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Bovelo
{
    public static class DataBase
    {

        public static void SendToDB(string query) //is used to send anything to the database
        {
            string connStr = "server=193.191.240.67;user=USER3;database=Bovelo;port=63304;password=USER3";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            cmd.Dispose();
            conn.Close();
        }

        private static List<List<string>> ConnectToDB(string sql)
        {
            var listFromDB = new List<List<string>>();
            var connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
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

        public static List<List<string>> GetFromDB(string DBTable)
        {
            string sql = "SELECT * FROM " + DBTable + ";";
            return ConnectToDB(sql);
        }
        public static List<List<string>> GetFromDBSelect(string DBTable, List<string> argumentList)
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
        public static List<List<string>> GetFromDBWhere(string DBTable, List<string> argumentList, string whereClause)
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
        public static string GetFromDBLastIdFromColumn(string table, string column)
        {
            string sql = "SELECT Id_Order FROM "+table+" ORDER BY "+ column + " DESC LIMIT 1;";
            return ConnectToDB(sql).Last()[0];//returns the last id
        }
        public static List<List<string>> GetPlanifiedOrderDetails(string sql)
        {
            return ConnectToDB(sql);
        }
    }
}
