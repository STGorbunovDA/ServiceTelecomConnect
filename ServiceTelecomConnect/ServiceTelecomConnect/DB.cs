using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

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

        ///"server=31.31.198.62;port=3306;username=u1748936_default;password=55gxqSH5Lv0ZpGRb;database=u1748936_root;charset=utf8" /// для всех остальных
        /// "server=31.31.198.62;port=3306;username=u1748936_u17;password=55gxqSH5Lv0ZpGRb;database=u1748936_radiostantion;charset=utf8" /// для работы

        readonly MySqlConnection connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_u17;password=55gxqSH5Lv0ZpGRb;database=u1748936_radiostantion;charset=utf8");

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
