using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ServiceTelecomConnect.Classes.Other
{
    class LogUser
    {
        internal static void LogMethodUserSaveFilePC(string user, string method)
        {
            try
            {
                DateTime today = DateTime.Now;

                string fileNamePath = $@"C:\Documents_ServiceTelekom\Log\{user}\{user}-{today.ToString("dd.MM.yyyy")}.txt";

                if (!File.Exists($@"C:\Documents_ServiceTelekom\Log\{user}\"))
                {
                    Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\Log\{user}\");
                }

                using (StreamWriter sw = new StreamWriter(fileNamePath, true, Encoding.Unicode))
                {
                    sw.WriteLine($"{user}\t{method}\t{today}");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Файл не сохранен!(Log)");
            }
        }
    }
}
