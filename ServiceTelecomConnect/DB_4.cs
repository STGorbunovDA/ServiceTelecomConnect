using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceTelecomConnect
{
    class DB_4
    {
        static volatile DB_4 Class;
        static object SyncObject = new object();
        public static DB_4 GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new DB_4();
                    }
                return Class;
            }
        }

        // <summary>
        /// подключении к базе данных
        /// </summary>
        MySqlConnection connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_db_4;password=war74_91;database=u1748936_root;charset=utf8");

        /// <summary>
        /// проверка соединения если закрыто открыть
        /// </summary>
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// проверка соединения если открыто закрыть
        /// </summary>
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// для вызова строки подключения
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
