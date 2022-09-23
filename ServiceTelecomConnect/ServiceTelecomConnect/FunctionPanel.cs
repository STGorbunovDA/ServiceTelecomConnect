using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class FunctionPanel
    {
        private delegate DialogResult ShowOpenFileDialogInvoker();

        #region загрузка общей БД всех радиостанций

        internal static void Show_DB_radiostantion_full(DataGridView dgw, string city)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (city != "")
                    {
                        var myCulture = new CultureInfo("ru-RU");
                        myCulture.NumberFormat.NumberDecimalSeparator = ".";
                        Thread.CurrentThread.CurrentCulture = myCulture;
                        dgw.Rows.Clear();
                        string queryString = $"SELECT * FROM radiostantion_full";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB_2.GetInstance.GetConnection()))
                        {
                            DB_2.GetInstance.OpenConnection();

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        Filling_datagridview.ReedSingleRow(dgw, reader);
                                    }
                                    reader.Close();
                                }
                            }
                            command.ExecuteNonQuery();
                            DB_2.GetInstance.CloseConnection();
                        }
                    }

                    dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                    dgw.Columns[0].Width = 45;
                    dgw.Columns[3].Width = 170;
                    dgw.Columns[4].Width = 180;
                    dgw.Columns[5].Width = 150;
                    dgw.Columns[6].Width = 178;
                    dgw.Columns[7].Width = 178;
                    dgw.Columns[8].Width = 100;
                    dgw.Columns[9].Width = 110;
                    dgw.Columns[10].Width = 100;
                    dgw.Columns[11].Width = 100;
                    dgw.Columns[17].Width = 120;
                    dgw.Columns[39].Width = 300;
                }
                catch (Exception)
                {
                    MessageBox.Show("Невозможно загрузить общую базу данных!(Show_DB_radiostantion_full)");
                }
            }
        }

        #endregion

        #region загрузка БД прошлого года

        internal static void Show_DB_radiostantion_last_year(DataGridView dgw, string city)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (city != "")
                    {
                        var myCulture = new CultureInfo("ru-RU");
                        myCulture.NumberFormat.NumberDecimalSeparator = ".";
                        Thread.CurrentThread.CurrentCulture = myCulture;
                        dgw.Rows.Clear();
                        string queryString = $"SELECT * FROM radiostantion_last_year WHERE city LIKE N'%{city}%'";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB_2.GetInstance.GetConnection()))
                        {
                            DB_2.GetInstance.OpenConnection();

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        Filling_datagridview.ReedSingleRow(dgw, reader);
                                    }
                                    reader.Close();
                                }
                            }
                            command.ExecuteNonQuery();
                            DB_2.GetInstance.CloseConnection();
                        }
                    }

                    dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                    dgw.Columns[0].Width = 45;
                    dgw.Columns[3].Width = 170;
                    dgw.Columns[4].Width = 180;
                    dgw.Columns[5].Width = 150;
                    dgw.Columns[6].Width = 178;
                    dgw.Columns[7].Width = 178;
                    dgw.Columns[8].Width = 100;
                    dgw.Columns[9].Width = 110;
                    dgw.Columns[10].Width = 100;
                    dgw.Columns[11].Width = 100;
                    dgw.Columns[17].Width = 120;
                    dgw.Columns[39].Width = 300;
                }
                catch (Exception)
                {
                    MessageBox.Show("Невозможно загрузить общую базу данных!(Show_DB_radiostantion_last_year)");
                }
            }
        }

        #endregion

        #region Копирование текущей Бд в резервную radiostantion в radiostantion_copy
        internal static void Manual_backup_current_DB()
        {
            try
            {
                var clearBD = "TRUNCATE TABLE radiostantion_copy";

                using (MySqlCommand command = new MySqlCommand(clearBD, DB_2.GetInstance.GetConnection()))
                {
                    DB_2.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB_2.GetInstance.CloseConnection();
                }

                var copyBD = "INSERT INTO radiostantion_copy SELECT * FROM radiostantion";

                using (MySqlCommand command2 = new MySqlCommand(copyBD, DB_2.GetInstance.GetConnection()))
                {
                    DB_2.GetInstance.OpenConnection();
                    command2.ExecuteNonQuery();
                    DB_2.GetInstance.CloseConnection();
                }
                MessageBox.Show("База данных успешно скопирована!");
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно скопировать текущую Бд в резервную radiostantion в radiostantion_copy(Manual_backup_current_DB)");
            }
        }
        #endregion

        #region очистка текущей БД
        internal static void Clear_BD_current_year()
        {
            try
            {
                var clearBD = "TRUNCATE TABLE radiostantion";

                using (MySqlCommand command = new MySqlCommand(clearBD, DB_2.GetInstance.GetConnection()))
                {
                    DB_2.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB_2.GetInstance.CloseConnection();
                }

                MessageBox.Show("База данных успешно очищенна!");
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно очисть БД!(TRUNCATE TABLE radiostantion)");
            }

        }

        #endregion

        #region копирование текущей БД в БД прошлого года для следующего года
        internal static void Copying_current_BD_end_of_the_year()
        {
            try
            {
                var clearBD = "TRUNCATE TABLE radiostantion_last_year";

                using (MySqlCommand command = new MySqlCommand(clearBD, DB_2.GetInstance.GetConnection()))
                {
                    DB_2.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB_2.GetInstance.CloseConnection();
                }

                var copyBD = "INSERT INTO radiostantion_last_year SELECT * FROM radiostantion";

                using (MySqlCommand command2 = new MySqlCommand(copyBD, DB_2.GetInstance.GetConnection()))
                {
                    DB_2.GetInstance.OpenConnection();
                    command2.ExecuteNonQuery();
                    DB_2.GetInstance.CloseConnection();
                }

                MessageBox.Show("База данных успешно скопирована!");
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно скопировать текущую БД в БД прошлого года radiostantion в radiostantion_last_year(Copying_current_BD_end_of_the_year)");
            }
        }

        #endregion

        #region Выгрузить резервынй файл JSON

        internal static void Get_date_save_datagridview_json(DataGridView dgw, string city)
        {
            try
            {
                JArray products = new JArray();

                foreach (DataGridViewRow row in dgw.Rows)
                {
                    JObject product = JObject.FromObject(new
                    {
                        id = row.Cells[0].Value,
                        poligon = row.Cells[1].Value,
                        company = row.Cells[2].Value,
                        location = row.Cells[3].Value,
                        model = row.Cells[4].Value,
                        serialNumber = row.Cells[5].Value,
                        inventoryNumber = row.Cells[6].Value,
                        networkNumber = row.Cells[7].Value,
                        dateTO = row.Cells[8].Value,
                        numberAct = row.Cells[9].Value,
                        city = row.Cells[10].Value,
                        price = row.Cells[11].Value,
                        representative = row.Cells[12].Value,
                        post = row.Cells[13].Value,
                        numberIdentification = row.Cells[14].Value,
                        dateIssue = row.Cells[15].Value,
                        phoneNumber = row.Cells[16].Value,
                        numberActRemont = row.Cells[17].Value,
                        category = row.Cells[18].Value,
                        priceRemont = row.Cells[19].Value,
                        antenna = row.Cells[20].Value,
                        manipulator = row.Cells[21].Value,
                        AKB = row.Cells[22].Value,
                        batteryСharger = row.Cells[23].Value,
                        completed_works_1 = row.Cells[24].Value,
                        completed_works_2 = row.Cells[25].Value,
                        completed_works_3 = row.Cells[26].Value,
                        completed_works_4 = row.Cells[27].Value,
                        completed_works_5 = row.Cells[28].Value,
                        completed_works_6 = row.Cells[29].Value,
                        completed_works_7 = row.Cells[30].Value,
                        parts_1 = row.Cells[31].Value,
                        parts_2 = row.Cells[32].Value,
                        parts_3 = row.Cells[33].Value,
                        parts_4 = row.Cells[34].Value,
                        parts_5 = row.Cells[35].Value,
                        parts_6 = row.Cells[36].Value,
                        parts_7 = row.Cells[37].Value,
                        decommissionSerialNumber = row.Cells[38].Value,
                        comment = row.Cells[39].Value
                    });
                    products.Add(product);
                }

                string json = JsonConvert.SerializeObject(products);

                DateTime today = DateTime.Today;

                string fileNamePath = $@"C:\Documents_ServiceTelekom\БазаДанныхJson\{city}\БазаДанныхJson.json";

                if (!File.Exists($@"С:\Documents_ServiceTelekom\БазаДанныхJson\{city}\"))
                {
                    Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\БазаДанныхJson\{city}\");
                }

                File.WriteAllText(fileNamePath, json);
            }
            catch (Exception)
            {
                MessageBox.Show($"Невозможно выгрузить JSON! C:\\Documents_ServiceTelekom\\БазаДанныхJson\\{city}\\БазаДанныхJson.json"); ;
            }
        }

        #endregion

        #region загрузка и обновление json в radiostantion

        internal static void Loading_json_file_BD(DataGridView dgw, string city)
        {
            try
            {

                Filling_datagridview.CreateColums(dgw);

                string fileNamePath = $@"C:\Documents_ServiceTelekom\БазаДанныхJson\{city}\БазаДанныхJson.json";

                if (File.Exists(fileNamePath))
                {
                    dgw.Rows.Clear();
                    string result;
                    using (var reader = new StreamReader(fileNamePath))
                    {
                        result = reader.ReadToEnd();
                    }

                    JArray fetch = JArray.Parse(result);

                    if (fetch.Count() > 0)
                    {
                        for (int i = 0; fetch.Count() > i; i++)
                        {
                            int n = dgw.Rows.Add();
                            dgw.Rows[n].Cells[0].Value = fetch[i]["id"].ToString();
                            dgw.Rows[n].Cells[1].Value = fetch[i]["poligon"].ToString();
                            dgw.Rows[n].Cells[2].Value = fetch[i]["company"].ToString();
                            dgw.Rows[n].Cells[3].Value = fetch[i]["location"].ToString();
                            dgw.Rows[n].Cells[4].Value = fetch[i]["model"].ToString();
                            dgw.Rows[n].Cells[5].Value = fetch[i]["serialNumber"].ToString();
                            dgw.Rows[n].Cells[6].Value = fetch[i]["inventoryNumber"].ToString();
                            dgw.Rows[n].Cells[7].Value = fetch[i]["networkNumber"].ToString();
                            dgw.Rows[n].Cells[8].Value = fetch[i]["dateTO"].ToString();
                            dgw.Rows[n].Cells[9].Value = fetch[i]["numberAct"].ToString();
                            dgw.Rows[n].Cells[10].Value = fetch[i]["city"].ToString();
                            dgw.Rows[n].Cells[11].Value = fetch[i]["price"].ToString();
                            dgw.Rows[n].Cells[12].Value = fetch[i]["representative"].ToString();
                            dgw.Rows[n].Cells[13].Value = fetch[i]["post"].ToString();
                            dgw.Rows[n].Cells[14].Value = fetch[i]["numberIdentification"].ToString();
                            dgw.Rows[n].Cells[15].Value = fetch[i]["dateIssue"].ToString();
                            dgw.Rows[n].Cells[16].Value = fetch[i]["phoneNumber"].ToString();
                            dgw.Rows[n].Cells[17].Value = fetch[i]["numberActRemont"].ToString();
                            dgw.Rows[n].Cells[18].Value = fetch[i]["category"].ToString();
                            dgw.Rows[n].Cells[19].Value = fetch[i]["priceRemont"].ToString();
                            dgw.Rows[n].Cells[20].Value = fetch[i]["antenna"].ToString();
                            dgw.Rows[n].Cells[21].Value = fetch[i]["manipulator"].ToString();
                            dgw.Rows[n].Cells[22].Value = fetch[i]["AKB"].ToString();
                            dgw.Rows[n].Cells[23].Value = fetch[i]["batteryСharger"].ToString();
                            dgw.Rows[n].Cells[24].Value = fetch[i]["completed_works_1"].ToString();
                            dgw.Rows[n].Cells[25].Value = fetch[i]["completed_works_2"].ToString();
                            dgw.Rows[n].Cells[26].Value = fetch[i]["completed_works_3"].ToString();
                            dgw.Rows[n].Cells[27].Value = fetch[i]["completed_works_4"].ToString();
                            dgw.Rows[n].Cells[28].Value = fetch[i]["completed_works_5"].ToString();
                            dgw.Rows[n].Cells[29].Value = fetch[i]["completed_works_6"].ToString();
                            dgw.Rows[n].Cells[30].Value = fetch[i]["completed_works_7"].ToString();
                            dgw.Rows[n].Cells[31].Value = fetch[i]["parts_1"].ToString();
                            dgw.Rows[n].Cells[32].Value = fetch[i]["parts_2"].ToString();
                            dgw.Rows[n].Cells[33].Value = fetch[i]["parts_3"].ToString();
                            dgw.Rows[n].Cells[34].Value = fetch[i]["parts_4"].ToString();
                            dgw.Rows[n].Cells[35].Value = fetch[i]["parts_5"].ToString();
                            dgw.Rows[n].Cells[36].Value = fetch[i]["parts_6"].ToString();
                            dgw.Rows[n].Cells[37].Value = fetch[i]["parts_7"].ToString();
                            dgw.Rows[n].Cells[38].Value = fetch[i]["decommissionSerialNumber"].ToString();
                            dgw.Rows[n].Cells[39].Value = fetch[i]["comment"].ToString();
                        }
                    }
                    for (int i = 0; i < dgw.Rows.Count; i++)
                    {
                        var id = dgw.Rows[i].Cells["id"].Value;
                        var poligon = dgw.Rows[i].Cells["poligon"].Value.ToString();
                        var company = dgw.Rows[i].Cells["company"].Value.ToString();
                        var location = dgw.Rows[i].Cells["location"].Value.ToString();
                        var model = dgw.Rows[i].Cells["model"].Value.ToString();
                        var serialNumber = dgw.Rows[i].Cells["serialNumber"].Value.ToString();
                        var inventoryNumber = dgw.Rows[i].Cells["inventoryNumber"].Value.ToString();
                        var networkNumber = dgw.Rows[i].Cells["networkNumber"].Value.ToString();
                        var dateTO = dgw.Rows[i].Cells["dateTO"].Value.ToString();
                        var numberAct = dgw.Rows[i].Cells["numberAct"].Value.ToString();
                        var cityDGW = dgw.Rows[i].Cells["city"].Value.ToString();
                        var price = dgw.Rows[i].Cells["price"].Value;
                        var representative = dgw.Rows[i].Cells["representative"].Value.ToString();
                        var post = dgw.Rows[i].Cells["post"].Value.ToString();
                        var numberIdentification = dgw.Rows[i].Cells["numberIdentification"].Value.ToString();
                        var dateIssue = dgw.Rows[i].Cells["dateIssue"].Value.ToString();
                        var phoneNumber = dgw.Rows[i].Cells["phoneNumber"].Value.ToString();
                        var numberActRemont = dgw.Rows[i].Cells["numberActRemont"].Value.ToString();
                        var category = dgw.Rows[i].Cells["category"].Value.ToString();
                        var priceRemont = dgw.Rows[i].Cells["priceRemont"].Value;
                        var antenna = dgw.Rows[i].Cells["antenna"].Value.ToString();
                        var manipulator = dgw.Rows[i].Cells["antenna"].Value.ToString();
                        var AKB = dgw.Rows[i].Cells["AKB"].Value.ToString();
                        var batteryСharger = dgw.Rows[i].Cells["batteryСharger"].Value.ToString();
                        var completed_works_1 = dgw.Rows[i].Cells["completed_works_1"].Value.ToString();
                        var completed_works_2 = dgw.Rows[i].Cells["completed_works_2"].Value.ToString();
                        var completed_works_3 = dgw.Rows[i].Cells["completed_works_3"].Value.ToString();
                        var completed_works_4 = dgw.Rows[i].Cells["completed_works_4"].Value.ToString();
                        var completed_works_5 = dgw.Rows[i].Cells["completed_works_5"].Value.ToString();
                        var completed_works_6 = dgw.Rows[i].Cells["completed_works_6"].Value.ToString();
                        var completed_works_7 = dgw.Rows[i].Cells["completed_works_7"].Value.ToString();
                        var parts_1 = dgw.Rows[i].Cells["parts_1"].Value.ToString();
                        var parts_2 = dgw.Rows[i].Cells["parts_2"].Value.ToString();
                        var parts_3 = dgw.Rows[i].Cells["parts_3"].Value.ToString();
                        var parts_4 = dgw.Rows[i].Cells["parts_4"].Value.ToString();
                        var parts_5 = dgw.Rows[i].Cells["parts_5"].Value.ToString();
                        var parts_6 = dgw.Rows[i].Cells["parts_6"].Value.ToString();
                        var parts_7 = dgw.Rows[i].Cells["parts_7"].Value.ToString();
                        var decommissionSerialNumber = dgw.Rows[i].Cells["decommissionSerialNumber"].Value.ToString();
                        var comment = dgw.Rows[i].Cells["comment"].Value.ToString();

                        string queryString = $"UPDATE radiostantion SET poligon = '{poligon}', company = '{company}', location = '{location}', " +
                            $"model = '{model}', serialNumber = '{serialNumber}', inventoryNumber = '{inventoryNumber}', networkNumber = '{networkNumber}', " +
                            $"dateTO = '{dateTO}', numberAct = '{numberAct}', city = '{cityDGW}', price = '{price}', representative = '{representative}', " +
                            $"post = '{post}', numberIdentification = '{numberIdentification}', dateIssue = '{dateIssue}', phoneNumber = '{phoneNumber}', " +
                            $"numberActRemont = '{numberActRemont}', category = '{category}', priceRemont = '{priceRemont}', antenna = '{antenna}', " +
                            $"manipulator = '{manipulator}', AKB = '{AKB}', batteryСharger = '{batteryСharger}', completed_works_1 = '{completed_works_1}', " +
                            $"completed_works_2 = '{completed_works_2}', completed_works_3 = '{completed_works_3}', completed_works_4 = '{completed_works_4}', " +
                            $"completed_works_5 = '{completed_works_5}', completed_works_6 = '{completed_works_6}', completed_works_7 = '{completed_works_7}', " +
                            $"parts_1 = '{parts_1}', parts_2 = '{parts_2}', parts_3 = '{parts_3}',  parts_4 = '{parts_4}',  parts_5 = '{parts_5}', parts_6 = '{parts_6}',  " +
                            $"parts_7 = '{parts_7}', decommissionSerialNumber = '{decommissionSerialNumber}', comment = '{comment}'  WHERE id = '{id}'";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB_2.GetInstance.GetConnection()))
                        {
                            DB_2.GetInstance.OpenConnection();
                            command.ExecuteNonQuery();
                            DB_2.GetInstance.CloseConnection();

                        }
                    }
                }
                else { MessageBox.Show("Отсутствует файл JSON"); };

                MessageBox.Show("Радиостанции успешно загруженны из JSON");

            }
            catch (Exception)
            {
                MessageBox.Show($"Невозможно загрузить и обновить JSON!(Loading_json_file_BD)");
            }
        }

        #endregion

      
    }
}
