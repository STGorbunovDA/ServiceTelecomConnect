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


        readonly MySqlConnection connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_db_3_2;password=war74_90;database=u1748936_radiostantion;charset=utf8");

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
