using MySql.Data.MySqlClient;
using System;
using System.Data;

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
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT serialNumber FROM radiostantion_decommission WHERE serialNumber = '{serialNumber}'";

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
            return true;
        }


        public Boolean CheacSerialNumber_radiostantion(string serialNumber)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT serialNumber FROM radiostantion WHERE serialNumber = '{serialNumber}'";

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
            return true;
        }
        public Boolean CheacSerialNumber_radiostantionCurator(string serialNumber)
        {
            if (Internet_check.CheackSkyNET())
            {

                string querystring = $"SELECT serialNumber FROM radiostantion_сomparison WHERE serialNumber = '{serialNumber}'";

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
            return true;
        }

        public Boolean CheackNumberAct_radiostantion_changeForm_2(string numberAct)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT numberAct FROM radiostantion WHERE numberAct = '{numberAct}'";

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
            return false;
        }

        public Boolean CheackNumberAct_radiostantion(string numberAct)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT numberAct FROM radiostantion WHERE numberAct = '{numberAct}'";

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
            return true;
        }
        public Boolean CheacSerialNumber_radiostantion_full(string serialNumber)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT serialNumber FROM radiostantion_full WHERE serialNumber = '{serialNumber}'";

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
            return true;
        }
        public Boolean CheacSerialNumber_radiostantion_last_year(string serialNumber)
        {

            string querystring = $"SELECT serialNumber FROM radiostantion_last_year WHERE serialNumber = '{serialNumber}'";

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
        public Boolean CheacSerialNumber_OC6(string serialNumber)
        {
            string querystring = $"SELECT serialNumber FROM OC6 WHERE serialNumber = '{serialNumber}'";

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
    }
}
