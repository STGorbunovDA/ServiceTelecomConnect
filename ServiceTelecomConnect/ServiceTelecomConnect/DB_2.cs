using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class DB_2 //для копирования radiostantion в radiostantion_copy в отдельном потоке по таймеру
    {
        static volatile DB_2 Class;
        static object SyncObject = new object();
        public static DB_2 GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new DB_2();
                    }
                return Class;
            }
        }

        /// "server=31.31.198.62;port=3306;username=u1748936_db_2;password=war74_89;database=u1748936_root;charset=utf8" /// для просмотра
        /// "server=31.31.198.62;port=3306;username=u1748936_db_2_1;password=war74_89;database=u1748936_radiostantion;charset=utf8" /// для работы

        readonly MySqlConnection connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_db_2_1;password=war74_89;database=u1748936_radiostantion;charset=utf8");

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка метода OpenConnection");
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка метода CloseConnection");
            }
        }
    }
}
