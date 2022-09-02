using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class CheacSerialNumber
    {
        static volatile CheacSerialNumber Class;
        static object SyncObject = new object();
        public static CheacSerialNumber GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new CheacSerialNumber();
                    }
                return Class;
            }
        }

        public Boolean CheacSerialNumber_radiostantion_decommission(string serialNumber)
        {
            if (Internet_check.GetInstance.AvailabilityChanged_bool())
            {
                try
                {
                    string querystring = $"SELECT * FROM radiostantion_decommission WHERE serialNumber = '{serialNumber}'";

                    MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return true;
                }
            }
            return true;
        }

        /// <summary>
        /// Метод проверки наличия заводского номер в базе данных
        /// </summary>
        /// <param name="loginUser"></param>
        /// <param name="passUser"></param>
        /// <returns></returns>
        public Boolean CheacSerialNumber_radiostantion(string serialNumber)
        {
            if (Internet_check.GetInstance.AvailabilityChanged_bool())
            {
                try
                {
                    string querystring = $"SELECT * FROM radiostantion WHERE serialNumber = '{serialNumber}'";

                    MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return true;
                }
            }
            return true;
        }
        public Boolean CheackNumberAct_radiostantion(string numberAct)
        {
            if (Internet_check.GetInstance.AvailabilityChanged_bool())
            {
                try
                {
                    string querystring = $"SELECT * FROM radiostantion WHERE numberAct = '{numberAct}'";

                    MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    if (table.Rows.Count < 20)
                    {
                        return false;
                    }

                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return true;
                }
            }
            return true;
        }
        public Boolean CheacSerialNumber_radiostantion_full(string serialNumber)
        {
            if (Internet_check.GetInstance.AvailabilityChanged_bool())
            {
                try
                {
                    string querystring = $"SELECT * FROM radiostantion_full WHERE serialNumber = '{serialNumber}'";

                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {

                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                return true;
                            }

                            else
                            {
                                return false;
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return true;
                }
            }
            return true;
        }

        public Boolean CheacSerialNumber_radiostantion_last_year(string serialNumber)
        {
            try
            {
                string querystring = $"SELECT * FROM radiostantion_last_year WHERE serialNumber = '{serialNumber}'";

                using (MySqlCommand command = new MySqlCommand(querystring, DB_2.GetInstance.GetConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return true;
            }
        }
    }
}
