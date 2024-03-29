﻿using MySql.Data.MySqlClient;
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

        public Boolean CheackSerialNumberRadiostantionDecommission(string road, string city, string serialNumber)
        {
            if (InternetCheck.CheackSkyNET())
            {
                string querystring = $"SELECT serialNumber FROM radiostantion_decommission " +
                    $"WHERE road = '{road}' AND city = '{city}' AND serialNumber = '{serialNumber}'";

                MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count > 0) return true;
                else return false;
            }
            return true;
        }

        public Boolean CheackSerialNumberRadiostationParameters(string road, string city, string serialNumber)
        {
            if (InternetCheck.CheackSkyNET())
            {
                string querystring = $"SELECT serialNumber FROM radiostation_parameters " +
                    $"WHERE road = '{road}' AND city = '{city}' AND serialNumber = '{serialNumber}'";

                MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count > 0) return true;
                else return false;
            }
            return true;
        }

        public Boolean CheackSerialNumberRadiostantion(string road, string city, string serialNumber)
        {
            if (InternetCheck.CheackSkyNET())
            {
                string querystring = $"SELECT serialNumber FROM radiostantion " +
                    $"WHERE road = '{road}' AND city = '{city}' AND serialNumber = '{serialNumber}'";

                MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count > 0) return true;
                else return false;
            }
            return true;
        }
        public Boolean CheackSerialNumberRadiostantionCurator(string road, string city, string serialNumber)
        {
            if (InternetCheck.CheackSkyNET())
            {
                string querystring = $"SELECT serialNumber FROM radiostantion_сomparison " +
                    $"WHERE road = '{road}' AND city = '{city}' AND serialNumber = '{serialNumber}'";

                MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count > 0) return true;
                else return false;
            }
            return true;
        }

        public Boolean CheackNumberActRadiostantionChangeForm2(string road, string city, string numberAct)
        {
            if (InternetCheck.CheackSkyNET())
            {
                string querystring = $"SELECT numberAct FROM radiostantion " +
                    $"WHERE road = '{road}' AND city = '{city}' AND numberAct = '{numberAct}'";

                MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count > 0) return true;
                else return false;
            }
            return false;
        }

        public Boolean CheackNumberActRadiostantion(string road, string city, string numberAct)
        {
            if (InternetCheck.CheackSkyNET())
            {
                string querystring = $"SELECT numberAct FROM radiostantion " +
                    $"WHERE road = '{road}' AND city = '{city}' AND numberAct = '{numberAct}'";

                MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count < 20) return false;
                else return true;
            }
            return true;
        }
        public Boolean CheackSerialNumberRadiostantionFull(string road, string city, string serialNumber)
        {
            if (InternetCheck.CheackSkyNET())
            {
                string querystring = $"SELECT serialNumber FROM radiostantion_full " +
                    $"WHERE road = '{road}' AND city = '{city}' AND serialNumber = '{serialNumber}'";

                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count > 0) return true;
                        else return false;
                    }
                }
            }
            return true;
        }
        public Boolean CheackSerialNumberRadiostantionLastYear(string road, string city, string serialNumber)
        {
            string querystring = $"SELECT serialNumber FROM radiostantion_last_year " +
                $"WHERE road = '{road}' AND city = '{city}' AND serialNumber = '{serialNumber}'";

            using (MySqlCommand command = new MySqlCommand(querystring, DB_2.GetInstance.GetConnection()))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count > 0) return true;
                    else return false;
                }
            }
        }
    }
}
