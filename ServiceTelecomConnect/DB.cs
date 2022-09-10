using MySql.Data.MySqlClient;

namespace ServiceTelecomConnect
{
    class DB
    {
        static volatile DB Class;
        static object SyncObject = new object();
        public static DB GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new DB();
                    }
                return Class;
            }
        }
        ///"server=31.31.198.62;port=3306;username=u1748936_default;password=55gxqSH5Lv0ZpGRb;database=u1748936_root;charset=utf8"
        /// "server=31.31.198.62;port=3306;username=u1748936_u17;password=55gxqSH5Lv0ZpGRb;database=u1748936_radiostantion;charset=utf8"ц


        /// <summary>
        /// подключении к базе данных
        /// </summary>
        MySqlConnection connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_default;password=55gxqSH5Lv0ZpGRb;database=u1748936_root;charset=utf8");

        /// <summary>
        /// проверка соединения если закрыто открыть
        /// </summary>
        public void openConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
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
