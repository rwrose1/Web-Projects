using System;
using System.IO;
using System.Collections.Generic;
using NazdaqSearch.Models;
using CsvHelper;

namespace NazdaqSearch.Logic.CSVConversions 
{

    class NazdaqCSV
    {

        public static void dataToCSV(List<NazdaqData> toBeRecorded) 
        {
            String path;
            if (toBeRecorded == null || toBeRecorded.Count == 0) 
            { 
                Console.WriteLine("List empty. Stop process");

            } else

            {

                using (StreamWriter writer = new StreamWriter("data.csv"))
                {
                    var csv = new CsvWriter(writer);
                    csv.WriteRecords(toBeRecorded);
                }
            }
        }

    }

}