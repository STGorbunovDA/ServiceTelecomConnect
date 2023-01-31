using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

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
            DialogResult result = MessageBox.Show("Вы действительно хотите закрыть программу?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)
            {
                if (Internet_check.CheackSkyNET())
                {
                    DateTime Date = DateTime.Now;
                    string exitDate = Date.ToString("yyyy-MM-dd HH:mm:ss");

                    DateTime dateTimeInput = QuerySettingDataBase.CheacDateTimeInputLogUserDatabase(login);

                    if (Date.ToString("yyyy-MM-dd") == dateTimeInput.ToString("yyyy-MM-dd"))
                    {
                        string addQuery = $"UPDATE logUserDB SET dateTimeExit = '{exitDate}' WHERE user = '{login}' AND dateTimeInput = '{dateTimeInput.ToString("yyyy-MM-dd HH:mm:ss")}'";

                        using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            command.ExecuteNonQuery();
                            DB.GetInstance.CloseConnection();
                        }
                    }                    
                }
                return false;
            }
            else return true;
        }
    }
}
