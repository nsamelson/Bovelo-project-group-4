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
        private static (MySqlDataReader, MySqlConnection) OpenConnection(string sql)
        {
            var connStr = "server=193.191.240.67;user=USER2;database=Bovelo;port=63304;password=USER2";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            var connection =(rdr,conn);

            return connection;
        }
        private static void CloseConnection(MySqlDataReader rdr, MySqlConnection conn)
        {
            rdr.Close();
            conn.Close();
        }
        public static List<List<string>> GetFromDB(string DBTable)
        {
            var listFromDB = new List<List<string>>();
            string sql = "SELECT * FROM " + DBTable + ";";
            var (rdr,conn) = OpenConnection(sql);

            while (rdr.Read())
            {
                var col = new List<string>();
                for (int j = 0; j < rdr.FieldCount; j++)
                {
                    col.Add(rdr[j].ToString());
                }
                listFromDB.Add(col);
            }
            CloseConnection(rdr, conn);
            return listFromDB;
        }

    }
}
