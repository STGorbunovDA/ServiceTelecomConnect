using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class Filling_datagridview
    {
        #region состояние Rows
        /// <summary>
        /// для значений к базе данных, по данному статусу будем или удалять или редактировать
        /// </summary>
        enum RowState
        {
            Existed,
            New,
            Modifield,
            ModifieldNew,
            Deleted
        }
        #endregion

        #region заполнение datagrid
        /// <summary>
        /// заполняем dataGridView1 колонки
        /// </summary>
        internal static void CreateColums(DataGridView dgw)
        {
            try
            {
                dgw.Columns.Add("id", "№");
                dgw.Columns.Add("poligon", "Полигон");
                dgw.Columns.Add("company", "Предприятие");
                dgw.Columns.Add("location", "Место нахождения");
                dgw.Columns.Add("model", "Модель радиостанции");
                dgw.Columns.Add("serialNumber", "Заводской номер");
                dgw.Columns.Add("inventoryNumber", "Инвентарный номер");
                dgw.Columns.Add("networkNumber", "Сетевой номер");
                dgw.Columns.Add("dateTO", "Дата ТО");
                dgw.Columns.Add("numberAct", "№ акта ТО");
                dgw.Columns.Add("city", "Город");
                dgw.Columns.Add("price", "Цена ТО");
                dgw.Columns.Add("representative", "Представитель предприятия");
                dgw.Columns.Add("post", "Должность");
                dgw.Columns.Add("numberIdentification", "Номер удостоверения");
                dgw.Columns.Add("dateIssue", "Дата выдачи удостоверения");
                dgw.Columns.Add("phoneNumber", "Номер телефона");
                dgw.Columns.Add("numberActRemont", "№ акта ремонта");
                dgw.Columns.Add("category", "Категория");
                dgw.Columns.Add("priceRemont", "Цена ремонта");
                dgw.Columns.Add("antenna", "Антенна");
                dgw.Columns.Add("manipulator", "Манипулятор");
                dgw.Columns.Add("AKB", "АКБ");
                dgw.Columns.Add("batteryСharger", "ЗУ");
                dgw.Columns.Add("completed_works_1", "Выполненные работы_1");
                dgw.Columns.Add("completed_works_2", "Выполненные работы_1");
                dgw.Columns.Add("completed_works_3", "Выполненные работы_1");
                dgw.Columns.Add("completed_works_4", "Выполненные работы_1");
                dgw.Columns.Add("completed_works_5", "Выполненные работы_1");
                dgw.Columns.Add("completed_works_6", "Выполненные работы_1");
                dgw.Columns.Add("completed_works_7", "Выполненные работы_1");
                dgw.Columns.Add("parts_1", "Израсходованные материалы и детали_1");
                dgw.Columns.Add("parts_2", "Израсходованные материалы и детали_2");
                dgw.Columns.Add("parts_3", "Израсходованные материалы и детали_3");
                dgw.Columns.Add("parts_4", "Израсходованные материалы и детали_4");
                dgw.Columns.Add("parts_5", "Израсходованные материалы и детали_5");
                dgw.Columns.Add("parts_6", "Израсходованные материалы и детали_6");
                dgw.Columns.Add("parts_7", "Израсходованные материалы и детали_7");
                dgw.Columns.Add("decommissionSerialNumber", "№ акта списания");
                dgw.Columns.Add("comment", "Примечание");
                dgw.Columns.Add("IsNew", String.Empty);
                dgw.Columns[12].Visible = false;
                dgw.Columns[13].Visible = false;
                dgw.Columns[14].Visible = false;
                dgw.Columns[15].Visible = false;
                dgw.Columns[16].Visible = false;
                dgw.Columns[20].Visible = false;
                dgw.Columns[21].Visible = false;
                dgw.Columns[22].Visible = false;
                dgw.Columns[23].Visible = false;
                dgw.Columns[24].Visible = false;
                dgw.Columns[25].Visible = false;
                dgw.Columns[26].Visible = false;
                dgw.Columns[27].Visible = false;
                dgw.Columns[28].Visible = false;
                dgw.Columns[29].Visible = false;
                dgw.Columns[30].Visible = false;
                dgw.Columns[31].Visible = false;
                dgw.Columns[32].Visible = false;
                dgw.Columns[33].Visible = false;
                dgw.Columns[34].Visible = false;
                dgw.Columns[35].Visible = false;
                dgw.Columns[36].Visible = false;
                dgw.Columns[37].Visible = false;
               // dgw.Columns[40].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Заполняем колонки значениями из базы данных из RefreshDataGrid
        /// </summary>
        /// <param name="dgw"></param>
        /// <param name="record"></param>
        internal static void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            try
            {
                dgw.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4),
                         record.GetString(5), record.GetString(6), record.GetString(7), Convert.ToDateTime(record.GetString(8)), record.GetString(9),
                         record.GetString(10), record.GetDecimal(11), record.GetString(12), record.GetString(13), record.GetString(14),
                         record.GetString(15), record.GetString(16), record.GetString(17), record.GetString(18), record.GetDecimal(19),
                         record.GetString(20), record.GetString(21), record.GetString(22), record.GetString(23), record.GetString(24),
                         record.GetString(25), record.GetString(26), record.GetString(27), record.GetString(28), record.GetString(29),
                         record.GetString(30), record.GetString(31), record.GetString(32), record.GetString(33), record.GetString(34),
                         record.GetString(35), record.GetString(36), record.GetString(37), record.GetString(38), record.GetString(39), RowState.ModifieldNew)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// выполняем подключение к базе данных, выполняем команду запроса и передаём данные ReedSingleRow
        /// </summary>
        /// <param name="dgw"></param>
        internal static void RefreshDataGrid(DataGridView dgw, string city)
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
                        string queryString = $"SELECT * FROM radiostantion WHERE city LIKE N'%{city.Trim()}%'";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.openConnection();

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        ReedSingleRow(dgw, reader);
                                    }
                                    reader.Close();
                                }
                            }
                            command.ExecuteNonQuery();
                            DB.GetInstance.closeConnection();
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
                catch (MySqlException)
                {
                    string Mesage2;
                    Mesage2 = "Что-то полшло не так, мы обязательно разберёмся";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                finally
                {
                    DB.GetInstance.closeConnection();
                }
            }
        }
        #endregion

        #region загрузка всей таблицы ТО в текущем году

        internal static void Full_BD(DataGridView dgw)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();
                    string queryString = $"SELECT * FROM radiostantion";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.openConnection();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ReedSingleRow(dgw, reader);
                                }
                                reader.Close();
                            }
                        }
                        command.ExecuteNonQuery();
                        DB.GetInstance.closeConnection();
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
                }
                catch (MySqlException)
                {
                    string Mesage2;
                    Mesage2 = "Системная ошибка загрузки всей таблицы текущих ТО РСТ";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                finally
                {
                    DB.GetInstance.closeConnection();
                }
            }
        }


        #endregion

        #region поиск по БД
        /// <summary>
        /// метод поиска по базе данных, подключение к базе, выполнение запроса так-же внутри  вызываем метод ReedSingleRow для вывода данных из базы
        /// </summary>
        /// <param name="dgw"></param>
        internal static void Search(DataGridView dgw, string comboBox_seach, string city, string textBox_search)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    string perem_comboBox = "numberAct";

                    dgw.Rows.Clear();

                    if (comboBox_seach == "Полигон")
                    {
                        perem_comboBox = "poligon";
                    }
                    else if (comboBox_seach == "Предприятие")
                    {
                        perem_comboBox = "company";
                    }
                    else if (comboBox_seach == "Станция")
                    {
                        perem_comboBox = "location";
                    }
                    else if (comboBox_seach == "Модель")
                    {
                        perem_comboBox = "model";
                    }
                    else if (comboBox_seach == "Заводской номер")
                    {
                        perem_comboBox = "serialNumber";
                    }
                    else if (comboBox_seach == "Дата ТО")
                    {
                        perem_comboBox = "dateTO";
                    }
                    else if (comboBox_seach == "Номер акта ТО")
                    {
                        perem_comboBox = "numberAct";
                    }
                    else if (comboBox_seach == "Номер акта Ремонта")
                    {
                        perem_comboBox = "numberActRemont";
                    }
                    else if (comboBox_seach == "Представитель ПП")
                    {
                        perem_comboBox = "representative";
                    }
                    else if (comboBox_seach == "Номер Акта списания")
                    {
                        perem_comboBox = "decommissionSerialNumber";
                    }

                    string searchString = $"SELECT * FROM radiostantion WHERE city = '{city}' AND CONCAT ({perem_comboBox}) LIKE '%" + textBox_search + "%'";

                    using (MySqlCommand command = new MySqlCommand(searchString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.openConnection();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ReedSingleRow(dgw, reader);
                                }
                                reader.Close();
                            }
                        }
                        DB.GetInstance.closeConnection();
                    }
                }
                catch (MySqlException ex)
                {
                    string Mesage2;
                    Mesage2 = "Ошибка поиска!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        #endregion

        #region поиск отсутсвующих рст исходя из предыдущего года

        internal static void Seach_DataGrid_Replay_RST(DataGridView dgw, string txb_flag_all_BD, string city)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (txb_flag_all_BD == "Вся БД")
                    {
                        var myCulture = new CultureInfo("ru-RU");
                        myCulture.NumberFormat.NumberDecimalSeparator = ".";
                        Thread.CurrentThread.CurrentCulture = myCulture;
                        dgw.Rows.Clear();
                        string queryString = $"SELECT radiostantion_last_year. * FROM radiostantion_last_year LEFT JOIN radiostantion ON (radiostantion_last_year.serialNumber=radiostantion.serialNumber) WHERE radiostantion.serialNumber IS NULL";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.openConnection();

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        ReedSingleRow(dgw, reader);
                                    }
                                    reader.Close();
                                }
                            }
                            command.ExecuteNonQuery();
                            DB.GetInstance.closeConnection();
                        }
                    }

                    else if (city != "")
                    {
                        var myCulture = new CultureInfo("ru-RU");
                        myCulture.NumberFormat.NumberDecimalSeparator = ".";
                        Thread.CurrentThread.CurrentCulture = myCulture;
                        dgw.Rows.Clear();

                        string queryString = $"SELECT radiostantion_last_year. * FROM radiostantion_last_year LEFT JOIN radiostantion ON (radiostantion_last_year.serialNumber=radiostantion.serialNumber) WHERE radiostantion.serialNumber IS NULL AND radiostantion_last_year.city LIKE '%" + city + "%'";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.openConnection();

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        ReedSingleRow(dgw, reader);
                                    }
                                    reader.Close();
                                }
                            }
                            command.ExecuteNonQuery();
                            DB.GetInstance.closeConnection();
                        }
                    }

                    txb_flag_all_BD = "";

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

                }
                catch (MySqlException)
                {
                    string Mesage2;
                    Mesage2 = "Что-то полшло не так, мы обязательно разберёмся";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                finally
                {
                    DB.GetInstance.closeConnection();
                }
            }

        }

        #endregion

        #region update_datagridview_number_act

        internal static void Update_datagridview_number_act(DataGridView dgw, string city, string numberAct)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    dgw.Rows.Clear();
                    dgw.AllowUserToAddRows = false;

                    string searchString = $"SELECT * FROM radiostantion WHERE city = '{city.Trim()}' AND numberAct LIKE '" + numberAct.Trim() + "'";

                    using (MySqlCommand command = new MySqlCommand(searchString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.openConnection();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ReedSingleRow(dgw, reader);
                                }
                                reader.Close();
                            }
                        }
                        DB.GetInstance.closeConnection();
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

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Ошибка! Невозможно найти по данному акту!");
                }
            }
        }

        #endregion

        #region для счётчика резервное копирование радиостанций из текущей radiostantion в radiostantion_copy
        internal static void Copy_BD_radiostantion_in_radiostantion_copy()
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    var clearBD = "TRUNCATE TABLE radiostantion_copy";

                    using (MySqlCommand command = new MySqlCommand(clearBD, DB_3.GetInstance.GetConnection()))
                    {
                        if (Internet_check.AvailabilityChanged_bool() == true)
                        {
                            DB_3.GetInstance.openConnection();
                            command.ExecuteNonQuery();
                            DB_3.GetInstance.closeConnection();
                        }
                    }

                    var copyBD = "INSERT INTO radiostantion_copy SELECT * FROM radiostantion";

                    using (MySqlCommand command2 = new MySqlCommand(copyBD, DB_3.GetInstance.GetConnection()))
                    {
                        if (Internet_check.AvailabilityChanged_bool() == true)
                        {
                            DB_3.GetInstance.openConnection();
                            command2.ExecuteNonQuery();
                            DB_3.GetInstance.closeConnection();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString()); ;
                }
            }
        }

        #endregion

        #region Удаление

        /// <summary>
        /// метод удаления значения из базы данных
        /// </summary>
        internal static void DeleteRowСell(DataGridView dgw)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    foreach (DataGridViewRow row in dgw.SelectedRows)
                    {
                        dgw.Rows[row.Index].Cells[40].Value = RowState.Deleted;
                    }

                    for (int index = 0; index < dgw.Rows.Count; index++)
                    {
                        var rowState = (RowState)dgw.Rows[index].Cells[40].Value;//проверить индекс

                        if (rowState == RowState.Deleted)
                        {
                            var id = Convert.ToInt32(dgw.Rows[index].Cells[0].Value);
                            var deleteQuery = $"DELETE FROM radiostantion WHERE id = {id}";

                            using (MySqlCommand command = new MySqlCommand(deleteQuery, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.openConnection();
                                command.ExecuteNonQuery();
                                DB.GetInstance.closeConnection();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #endregion

        #region Удаление ремонта

        internal static void Delete_rst_remont(string numberActRemont, string serialNumber)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (numberActRemont != "")
                    {
                        if (CheacknumberActRemont_radiostantion(numberActRemont))
                        {

                            var changeQuery = $"UPDATE radiostantion SET numberActRemont = '', category = '', " +
                                $"priceRemont = '', completed_works_1 = '', completed_works_2 = '', " +
                                $"completed_works_3 = '', completed_works_4 = '', " +
                                $"completed_works_5 = '', completed_works_6 = '', " +
                                $"completed_works_7 = '', parts_1 = '', parts_2 = '', " +
                                $"parts_3 = '', parts_4 = '', parts_5 = '', parts_6 = '', parts_7 = ''" +
                                $"WHERE serialNumber = '{serialNumber}' ";

                            using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.openConnection();
                                command.ExecuteNonQuery();
                                DB.GetInstance.closeConnection();
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        static Boolean CheacknumberActRemont_radiostantion(string numberActRemont)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    string querystring = $"SELECT * FROM radiostantion WHERE numberActRemont = '{numberActRemont}'";

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

        #endregion

        #region списание рст

        internal static void Record_decommissionSerialNumber(string serialNumber, string decommissionSerialNumber, 
            string city, string poligon, string company, string location, string model, string dateTO, string price, string representative, string post,
            string numberIdentification, string dateIssue, string phoneNumber, string antenna, string manipulator,
            string AKB, string batteryСharger, string comment)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (serialNumber != "")
                    {
                        var changeQuery = $"UPDATE radiostantion SET inventoryNumber = 'списание', networkNumber = 'списание', " +
                            $"decommissionSerialNumber = '{decommissionSerialNumber}', numberAct = 'списание', numberActRemont = 'списание', " +
                            $"category = '', completed_works_1 = '', completed_works_2 = '', completed_works_3 = '', completed_works_4 = ''," +
                            $"completed_works_5 = '', completed_works_6 = '', completed_works_7 = '', parts_1 = '', parts_2 = '', parts_3 = '', " +
                            $"parts_4 = '', parts_5 = '', parts_6 = '', parts_7 = '' WHERE serialNumber = '{serialNumber}'";

                        using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.openConnection();
                            command.ExecuteNonQuery();
                            DB.GetInstance.closeConnection();
                        }

                        if (CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantion_full(serialNumber))
                        {

                            var changeQuery2 = $"UPDATE radiostantion_full SET inventoryNumber = 'списание', networkNumber = 'списание', " +
                                $"decommissionSerialNumber = '{decommissionSerialNumber}', numberAct = 'списание', numberActRemont = 'списание', " +
                                $"category = '', completed_works_1 = '', completed_works_2 = '', completed_works_3 = '', completed_works_4 = ''," +
                                $"completed_works_5 = '', completed_works_6 = '', completed_works_7 = '', parts_1 = '', parts_2 = '', parts_3 = '', " +
                                $"parts_4 = '', parts_5 = '', parts_6 = '', parts_7 = '' WHERE serialNumber = '{serialNumber}'";


                            using (MySqlCommand command2 = new MySqlCommand(changeQuery2, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.openConnection();
                                command2.ExecuteNonQuery();
                                DB.GetInstance.closeConnection();
                            }
                        }

                        if (!CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantion_decommission(serialNumber))
                        {
                            var addQuery = $"INSERT INTO radiostantion_decommission (poligon, company, location, model, serialNumber," +
                                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, representative, " +
                                        $"post, numberIdentification, dateIssue, phoneNumber, numberActRemont, category, priceRemont, " +
                                        $"antenna, manipulator, AKB, batteryСharger, completed_works_1, completed_works_2, completed_works_3, " +
                                        $"completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1, parts_2, parts_3, parts_4, " +
                                        $"parts_5, parts_6, parts_7, decommissionSerialNumber, comment) VALUES ('{poligon.Trim()}', '{company.Trim()}', '{location.Trim()}'," +
                                        $"'{model.Trim()}','{serialNumber.Trim()}', 'списание', 'списание', " +
                                        $"'{dateTO.Trim()}','списание','{city.Trim()}','{price.Trim()}', '{representative.Trim()}', '{post.Trim()}', " +
                                        $"'{numberIdentification.Trim()}', '{dateIssue.Trim()}', '{phoneNumber.Trim()}', '{""}', '{""}', '{0.00}'," +
                                        $"'{antenna.Trim()}', '{manipulator.Trim()}', '{AKB.Trim()}', '{batteryСharger.Trim()}', '{""}', '{""}', " +
                                        $"'{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{decommissionSerialNumber}', '{comment}')";

                            using (MySqlCommand command3 = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.openConnection();
                                command3.ExecuteNonQuery();
                                DB.GetInstance.closeConnection();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #endregion

        #region Удалить номер списание из таблицы radiostantion

        internal static void Delete_decommissionSerialNumber_radiostantion(DataGridView dgw2, string decommissionSerialNumber, string serialNumber, string city)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (decommissionSerialNumber != "")
                    {
                        var changeQuery = $"UPDATE radiostantion SET decommissionSerialNumber = '' WHERE serialNumber = '{serialNumber}' ";

                        using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.openConnection();
                            command.ExecuteNonQuery();
                            DB.GetInstance.closeConnection();
                            MessageBox.Show("Списание удалено! Заполни инвентарный и сетевой номер заново!");          
                        }

                        CreateColums(dgw2);

                        var queryString = $"SELECT * FROM radiostantion_decommission WHERE city LIKE N'%{city.Trim()}%' AND serialNumber = '{serialNumber}'";
                        using (MySqlCommand command2 = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.openConnection();

                            using (MySqlDataReader reader = command2.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        ReedSingleRow(dgw2, reader);
                                    }
                                    reader.Close();
                                }
                            }
                            command2.ExecuteNonQuery();
                            DB.GetInstance.closeConnection();
                        }
                        dgw2.Rows[0].Cells[40].Value = RowState.Deleted;
                        var id = Convert.ToInt32(dgw2.Rows[0].Cells[0].Value);
                        var deleteQuery = $"DELETE FROM radiostantion_decommission WHERE id = {id}";

                        using (MySqlCommand command2 = new MySqlCommand(deleteQuery, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.openConnection();
                            command2.ExecuteNonQuery();
                            DB.GetInstance.closeConnection();
                        }
                    }
                }
                catch (Exception ex)
                {

                    string Mesage2 = "Что-то полшло не так, мы обязательно разберёмся (Удаление из таблиц списаной рст)";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                    MessageBox.Show(ex.ToString());
                } 
            }
        }
        #endregion


        #region показать списания

        internal static void Show_radiostantion_decommission(DataGridView dgw, string city)
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
                        string queryString = $"SELECT * FROM radiostantion_decommission WHERE city LIKE N'%{city.Trim()}%'";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.openConnection();

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        ReedSingleRow(dgw, reader);
                                    }
                                    reader.Close();
                                }
                            }
                            command.ExecuteNonQuery();
                            DB.GetInstance.closeConnection();
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
                }
                catch (Exception ex)
                {
                    string Mesage2 = "Что-то полшло не так, мы обязательно разберёмся (показать списания рст)";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    DB.GetInstance.closeConnection();
                }
            }
        }

        #endregion
    }
}
