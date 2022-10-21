using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;


namespace ServiceTelecomConnect
{
    class PrintDocWord
    {
        static volatile PrintDocWord Class;
        static object SyncObject = new object();
        public static PrintDocWord GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                        {
                            Class = new PrintDocWord("documents\\DV.doc");
                        }
                    }
                return Class;
            }
        }

        private FileInfo _fileInfo;

        public PrintDocWord(string filename)
        {
            Type officeType = Type.GetTypeFromProgID("Word.Application");

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



        internal void ProcessPrintWordDecommission(Dictionary<string, string> items, string txB_decommissionSerialNumber_company, string dateDecommission, string city, string comment)
        {
            var WordApp = new Word.Application();
            try
            {
                Object file = _fileInfo.FullName;
                Object missing = Type.Missing;

                WordApp.Documents.Open(file);

                foreach (var item in items)
                {
                    Word.Find find = WordApp.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: false,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing, Replace: replace);
                }

                var word_file = $"{txB_decommissionSerialNumber_company.Replace('/', '.')}-{dateDecommission}_АКТ-Дефектовки.doc";

                if (!File.Exists($@"С:\Documents_ServiceTelekom\Списания\{city}\"))
                {
                    try
                    {
                        Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\Списания\{city}\");

                        WordApp.ActiveDocument.SaveAs($@"C:\Documents_ServiceTelekom\Списания\{city}\" + word_file);
                        WordApp.Visible = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Не удаётся сохранить файл word");
                    }
                }
                else
                {
                    try
                    {
                        WordApp.ActiveDocument.SaveAs($@"C:\Documents_ServiceTelekom\Списания\{city}\" + file);
                        WordApp.Visible = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Не удаётся сохранить файл word");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удаётся сформировать акт списания(ProcessPrintWord)");
                WordApp.ActiveDocument.Close();
                WordApp.Quit();
            }
        }

       
    }
}
