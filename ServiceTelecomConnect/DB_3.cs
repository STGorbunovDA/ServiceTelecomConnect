using MySql.Data.MySqlClient;

namespace ServiceTelecomConnect
{
    class DB_3
    {

        static volatile DB_3 Class;
        static object SyncObject = new object();
        public static DB_3 GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new DB_3();
                    }
                return Class;
            }
        }
        /// "server=31.31.198.62;port=3306;username=u1748936_db_3;password=war74_89;database=u1748936_root;charset=utf8" /// для просмотра
        /// "server=31.31.198.62;port=3306;username=u1748936_db_3_2;password=war74_89;database=u1748936_radiostantion;charset=utf8" /// для работы

        // <summary>
        /// подключении к базе данных
        /// </summary>
        MySqlConnection connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_db_3_2;password=war74_90;database=u1748936_radiostantion;charset=utf8");

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
