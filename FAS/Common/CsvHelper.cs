using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FAS.Common
{
    public class CSVFile
    {
        private string filePath;

        public CSVFile(string filePath)
        {
            this.filePath = filePath;
        }

        public void WriteToCSV(List<string[]> data)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string[] row in data)
                {
                    writer.WriteLine(string.Join(",", row));
                }
            }
        }

        public List<string[]> ReadFromCSV()
        {
            List<string[]> data = new List<string[]>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] row = line.Split(',');
                    data.Add(row);
                }
            }

            return data;
        }
    }
}
