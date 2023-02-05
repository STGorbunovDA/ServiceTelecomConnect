using System;
using System.Net;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class InternetCheck
    { 
        public static bool CheackSkyNET()
        {
            try
            {
                Dns.GetHostEntry("dotnet.beget.tech");
                    return true;               
            }
            catch (Exception)
            {
                MessageBox.Show(@"Отсутствует подключение к Интернету. Проверьте настройки сети и повторите попытку",
                        "Сеть недоступна");
                return false;
            }
        }
    }
}
