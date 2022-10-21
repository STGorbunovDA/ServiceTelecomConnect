using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ServiceTelecomConnect
{
    class PrintDocExcel
    {
        static volatile PrintDocExcel Class;
        static object SyncObject = new object();
        public static PrintDocExcel GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                        {
                            Class = new PrintDocExcel("documents\\TAG.xls");
                        }
                    }
                return Class;
            }
        }

        private FileInfo _fileInfo;

        public PrintDocExcel(string filename)
        {
            Type officeType = Type.GetTypeFromProgID("Excel.Application");

            if (officeType != null)
            {
                if (File.Exists(filename))
                {
                    _fileInfo = new FileInfo(filename);
                }
                else
                {
                    throw new ArgumentException("File not found!");
                }
            }
            else
            {
                string Mesage2 = "У Вас не установлен пакет Office(Excel + Word)!";

                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }
        }

        internal void ProcessPrintWordTag(Dictionary<string, string> items2, string date_Tag)
        {
            Excel.Application app = new Excel.Application();
            try
            {
                String file = _fileInfo.FullName;
                object m = Type.Missing;

                // open the workbook. 
                Excel.Workbook wb = app.Workbooks.Open(
                    file,
                    m, false, m, m, m, m, m, m, m, m, m, m, m, m);

                // get the active worksheet. (Replace this if you need to.) 
                Excel.Worksheet ws = (Excel.Worksheet)wb.ActiveSheet;

                // get the used range. 
                Excel.Range r = (Excel.Range)ws.UsedRange;

                foreach (var item in items2)
                {
                    bool success = (bool)r.Replace(
                   item.Key,
                   item.Value,
                   Excel.XlLookAt.xlWhole,
                   Excel.XlSearchOrder.xlByRows,
                   true, m, m, m);
                }

                var word_file = $"Бирка_{date_Tag}";

                if (!File.Exists($@"С:\Documents_ServiceTelekom\Бирки\"))
                {
                    try
                    {
                        Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\Бирки");

                        wb.SaveAs($@"C:\Documents_ServiceTelekom\Бирки\" + word_file);
                        app.Visible = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Не удаётся сохранить файл excel");
                    }
                }
                else
                {
                    try
                    {
                        wb.SaveAs($@"C:\Documents_ServiceTelekom\Бирки\" + word_file);
                        app.Visible = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Не удаётся сохранить файл excel");
                    }
                }
            }
            catch (Exception ex)
            {
                if (app != null)
                {
                    app = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                MessageBox.Show(ex.ToString());
                Environment.Exit(0);
            }


        }
    }
}
