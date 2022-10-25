using MySql.Data.MySqlClient;
using System;
using System.Data;
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
            if (Internet_check.CheackSkyNET())
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

                catch (Exception)
                {
                    MessageBox.Show("Ошибка метода проверки нахождения радиостанции в таблице radiostantion_decommission (CheacSerialNumber_radiostantion_decommission)");
                    return true;
                }
            }
            return true;
        }
        public Boolean CheacSerialNumber_radiostantion(string serialNumber)
        {
            if (Internet_check.CheackSkyNET())
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

                catch (Exception)
                {
                    MessageBox.Show("Ошибка метода проверки нахождения радиостанции в таблице radiostantion (CheacSerialNumber_radiostantion)");
                    return true;
                }
            }
            return true;
        }
        public Boolean CheacSerialNumber_radiostantionCurator(string serialNumber)
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    string querystring = $"SELECT * FROM radiostantion_сomparison WHERE serialNumber = '{serialNumber}'";

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

                catch (Exception)
                {
                    MessageBox.Show("Ошибка метода проверки нахождения радиостанции в таблице radiostantion (CheacSerialNumber_radiostantion)");
                    return true;
                }
            }
            return true;
        }
        public Boolean CheackNumberAct_radiostantion(string numberAct)
        {
            if (Internet_check.CheackSkyNET())
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
                catch (Exception)
                {
                    MessageBox.Show("Ошибка метода нахождения радиостанций в акте не более 20 в таблице radiostantion (CheackNumberAct_radiostantion)");
                    return true;
                }
            }
            return true;
        }
        public Boolean CheacSerialNumber_radiostantion_full(string serialNumber)
        {
            if (Internet_check.CheackSkyNET())
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

                catch (Exception)
                {
                    MessageBox.Show("Ошибка метода проверки нахождения радиостанции в таблице radiostantion_full (CheacSerialNumber_radiostantion_full)");
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
            catch (Exception)
            {
                MessageBox.Show("Ошибка метода проверки нахождения радиостанции в таблице radiostantion_last_year (CheacSerialNumber_radiostantion_last_year)");
                return true;
            }
        }
        public Boolean CheacSerialNumber_OC6(string serialNumber)
        {
            try
            {
                string querystring = $"SELECT * FROM OC6 WHERE serialNumber = '{serialNumber}'";

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
            catch (Exception)
            {
                MessageBox.Show("Ошибка метода проверки нахождения радиостанции в таблице OC6 (CheacSerialNumber_OC6)");
                return true;
            }
        }
    }
}
