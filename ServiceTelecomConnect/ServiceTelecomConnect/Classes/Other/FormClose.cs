using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace ServiceTelecomConnect
{
    class FormClose
    {
        static volatile FormClose Class;
        static object SyncObject = new object();
        public static FormClose GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new FormClose();
                    }
                return Class;
            }
        }

        public bool FClose(string login)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть программу?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)
            {
                try
                {
                    if (Internet_check.CheackSkyNET())
                    {
                        DateTime Date = DateTime.Now;
                        var exitDate = Date.ToString("yyyy-MM-dd HH:mm:ss");

                        var addQuery = $"UPDATE logUserDB SET dateTimeExit = '{exitDate}' WHERE user = '{login}'";

                        using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            command.ExecuteNonQuery();
                            DB.GetInstance.CloseConnection();
                        }
                    }    
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Ошибка записи в БД(logUserDB) даты и время выхода");
                }
                return false;
            }
            else return true;
        }
    }
}
