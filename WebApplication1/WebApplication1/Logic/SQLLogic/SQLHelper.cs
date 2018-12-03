using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using NazdaqSearch.Models;

namespace NazdaqSearch.Logic.SQLLogic
{
    public static class SQLHelper
    {

        public static List<NazdaqData> CompareData(List<NazdaqData> symbols, String date)
        {

            List<NazdaqData> returnList = new List<NazdaqData>();

            foreach (NazdaqData item in symbols)
            {
                if (item.TimeandDate.Contains(date))
                {
                    returnList.Add(item);
                }
            }

            return returnList;
        }

        public static void Insert(NazdaqData item)
        {

            string connectionString;
            SqlConnection cnn;
            SqlDataReader crn;
            connectionString = @"Server=localhost;Database=NazdaqSearch;Trusted_Connection=True;";
            cnn = new SqlConnection(connectionString);
            cnn.Open();

            SqlCommand cmd = new SqlCommand("IntoNazdata", cnn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

#pragma warning disable CS0618 // Type or member is obsolete
            cmd.Parameters.Add("@Title", item.Title);
            cmd.Parameters.Add("@Date", item.TimeandDate);
            cmd.Parameters.Add("@Symbol", item.Symbol);
            cmd.Parameters.Add("@Data", item.Data);
#pragma warning restore CS0618 // Type or member is obsolete

            crn = cmd.ExecuteReader();
            crn.Close();

        }

        public static List<NazdaqData> GetAllWithSymbol(String symbol)
        {
            string connectionString;
            connectionString = @"Server=localhost;Database=NazdaqSearch;Trusted_Connection=True;";

            List<NazdaqData> data = new List<NazdaqData>();

            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("select * from NazData", cnn)) 
            {
                cnn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String test = reader["Symbol"].ToString();
                        if (test == symbol)
                        {
                            NazdaqData entry = new NazdaqData();
                            entry.Symbol = test;
                            entry.Title = reader["Title"].ToString();
                            entry.Data = reader["DataText"].ToString();
                            entry.TimeandDate = reader["DateandTime"].ToString();

                            data.Add(entry);
                        }
                    }

                }
                cnn.Close();
            }

            return data;
        }

        public static List<NazdaqData> GetAllWithDate(String Date)
        {

            string connectionString;
            connectionString = @"Server=localhost;Database=NazdaqSearch;Trusted_Connection=True;";

            List<NazdaqData> data = new List<NazdaqData>();

            using (SqlConnection cnn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("select * from NazData", cnn))
            {
                cnn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        String test = reader["DateandTime"].ToString();
                        if (test.Contains(Date))
                        {
                            NazdaqData entry = new NazdaqData();
                            entry.TimeandDate = test;
                            entry.Title = reader["Title"].ToString();
                            entry.Data = reader["DataText"].ToString();
                            entry.Symbol = reader["Symbol"].ToString();

                            data.Add(entry);
                        }
                    }

                }
                cnn.Close();
            }

            return data;
        }

    }
}