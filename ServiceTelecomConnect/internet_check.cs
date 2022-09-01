using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class Internet_check
    {
        static volatile Internet_check Class;
        static object SyncObject = new object();
        public static Internet_check GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new Internet_check();
                    }
                return Class;
            }
        }
        public bool AvailabilityChanged_bool()
        {
            try
            {
                if (new Ping().Send("yandex.ru").Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(@"Отсутствует подключение к Интернету. Проверьте настройки сети и повторите попытку",
                        "Сеть недоступна");
            }
            return false;
        }
    }
}
