using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CheckListTool.Properties;
using Excel = Microsoft.Office.Interop.Excel;

namespace CheckListTool
{
    public class ExcelManagerController
    {
        static Excel.Application xlApp;
        static Excel.Workbook xlWorkBook;
        static Excel.Worksheet xlWorkSheet;
        private static long nextRow = 0;
        private static string filePath = Settings.Default.ImpactAnalysisExcelPath;//Directory.GetCurrentDirectory() + "\\test2.xlsx";

        public static void OpenExcel()
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(filePath);
        }
        public static void OpenAndSet(int column, string value)
        {
            object misValue = System.Reflection.Missing.Value;

            //xlApp = new Excel.Application();
            //    xlWorkBook = xlApp.Workbooks.Open(filePath);
                xlWorkSheet = (Excel.Worksheet) xlWorkBook.Sheets[1];
                if (nextRow == 0)
                {
                    var xlRange = (Excel.Range) xlWorkSheet.Cells[xlWorkSheet.Rows.Count, 1];
                    var lastRow = (long) xlRange.get_End(Excel.XlDirection.xlUp).Row;
                    nextRow = lastRow + 1;

                }

                xlWorkSheet.Cells[nextRow, column] = value;
                //xlWorkSheet.Cells[1, 1] = "333";
                //MessageBox.Show(xlWorkSheet.Cells[1,1].Value2.ToString());
                //Close();
        }

        public static bool IsFileLocked()
        {
            FileStream stream = null;
            var file = new FileInfo(filePath);
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                MessageBox.Show("File in use");
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        public static void InsertValueToSheet(int column, string value)
        {
            xlWorkSheet.Cells[nextRow, column] = value;
        }

        public static void Close()
        {
            xlWorkBook.Close(true);//, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
