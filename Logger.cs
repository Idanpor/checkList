using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckListTool
{
    public class Logger
    {
        public static string filePath = @"log-checkList.txt";
        public static void Log(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(DateTime.Now.ToShortTimeString() + "  "  + message);
                streamWriter.Close();
            }
        }
    }
}
