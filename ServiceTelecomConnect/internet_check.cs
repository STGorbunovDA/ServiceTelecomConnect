using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class Internet_check
    {
        
        public static bool AvailabilityChanged_bool()
        {
            try
            {//if(new Ping().Send("yandex.ru").Status == IPStatus.Success)
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
