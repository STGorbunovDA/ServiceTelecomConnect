using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class QuerySettingDataBase
    {

        #region состояние Rows

        enum RowState
        {
            Existed,
            New,
            Modifield,
            ModifieldNew,
            Change,
            Deleted
        }

        #endregion

        #region заполнение datagridview 

        #region Начальника участка
        internal static void CreateColums(DataGridView dgw)
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
            dgw.Columns.Add("road", "Дорога");
            dgw.Columns.Add("IsNew", "RowState");
            dgw.Columns[12].Visible = true;
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
            dgw.Columns[40].Visible = false;
            dgw.Columns[41].Visible = false;
        }

        internal static void RefreshDataGrid(DataGridView dgw, string city, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (!String.IsNullOrEmpty(city))
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();
                    string queryString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                        $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                        $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                        $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                        $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion " +
                        $"WHERE city = '{city.Trim()}' AND road = '{road}'";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();

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
                        DB.GetInstance.CloseConnection();
                    }
                }
                else dgw.Rows.Clear();

                dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                dgw.Columns[0].Width = 45;
                dgw.Columns[3].Width = 170;
                dgw.Columns[4].Width = 170;
                dgw.Columns[5].Width = 170;
                dgw.Columns[6].Width = 170;
                dgw.Columns[7].Width = 178;
                dgw.Columns[8].Width = 100;
                dgw.Columns[9].Width = 110;
                dgw.Columns[10].Width = 100;
                dgw.Columns[11].Width = 100;
                dgw.Columns[17].Width = 120;
                dgw.Columns[39].Width = 300;

                //dgw.Sort(dgw.Columns["numberAct"], ListSortDirection.Ascending);
                if (dgw.Rows.Count > 1)
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[0];
            }
        }

        internal static void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4),
                     record.GetString(5), record.GetString(6), record.GetString(7), Convert.ToDateTime(record.GetString(8)), record.GetString(9),
                     record.GetString(10), record.GetDecimal(11), record.GetString(12), record.GetString(13), record.GetString(14),
                     record.GetString(15), record.GetString(16), record.GetString(17), record.GetString(18), record.GetDecimal(19),
                     record.GetString(20), record.GetString(21), record.GetString(22), record.GetString(23), record.GetString(24),
                     record.GetString(25), record.GetString(26), record.GetString(27), record.GetString(28), record.GetString(29),
                     record.GetString(30), record.GetString(31), record.GetString(32), record.GetString(33), record.GetString(34),
                     record.GetString(35), record.GetString(36), record.GetString(37), record.GetString(38), record.GetString(39),
                     record.GetString(40), RowState.ModifieldNew)));

        }

        internal static void RefreshDataGridTimerEventProcessor(DataGridView dgw, string city, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (city != "")
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();
                    string queryString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                        $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                        $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                        $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                        $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion " +
                        $"WHERE city = '{city.Trim()}' AND road = '{road}'";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ReedSingleRowTimerEventProcessor(dgw, reader);
                                }
                                reader.Close();
                            }
                        }
                        command.ExecuteNonQuery();
                        DB.GetInstance.CloseConnection();
                    }
                }

                dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                dgw.Columns[0].Width = 45;
                dgw.Columns[3].Width = 170;
                dgw.Columns[4].Width = 170;
                dgw.Columns[5].Width = 170;
                dgw.Columns[6].Width = 170;
                dgw.Columns[7].Width = 178;
                dgw.Columns[8].Width = 100;
                dgw.Columns[9].Width = 110;
                dgw.Columns[10].Width = 100;
                dgw.Columns[11].Width = 100;
                dgw.Columns[17].Width = 120;
                dgw.Columns[39].Width = 300;

                //dgw.Sort(dgw.Columns["numberAct"], ListSortDirection.Ascending);
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[0];
            }
        }

        internal static void ReedSingleRowTimerEventProcessor(DataGridView dgw, IDataRecord record)
        {
            dgw.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4),
                     record.GetString(5), record.GetString(6), record.GetString(7), Convert.ToDateTime(record.GetString(8)), record.GetString(9),
                     record.GetString(10), record.GetDecimal(11), record.GetString(12), record.GetString(13), record.GetString(14),
                     record.GetString(15), record.GetString(16), record.GetString(17), record.GetString(18), record.GetDecimal(19),
                     record.GetString(20), record.GetString(21), record.GetString(22), record.GetString(23), record.GetString(24),
                     record.GetString(25), record.GetString(26), record.GetString(27), record.GetString(28), record.GetString(29),
                     record.GetString(30), record.GetString(31), record.GetString(32), record.GetString(33), record.GetString(34),
                     record.GetString(35), record.GetString(36), record.GetString(37), record.GetString(38), record.GetString(39),
                     record.GetString(40), RowState.ModifieldNew)));
        }
        #endregion

        #region Инженера

        internal static void CreateColumsEngineer(DataGridView dgw)
        {
            dgw.Columns.Add("id", "№");
            dgw.Columns.Add("modelRST", "Модель");
            dgw.Columns.Add("problem", "Неисправность");
            dgw.Columns.Add("info", "Описание неисправности");
            dgw.Columns.Add("actions", "Виды работ по устраненнию дефекта");
            dgw.Columns.Add("author", "Автор");
            dgw.Columns.Add("IsNew", "RowState");
            dgw.Columns[6].Visible = false;
        }
        internal static void RefreshDataGridEngineer(DataGridView dgw)
        {
            if (Internet_check.CheackSkyNET())
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = myCulture;
                dgw.Rows.Clear();

                string queryString = $"SELECT id, model, problem, info, actions, author FROM problem_engineer";

                using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ReedSingleRowEnginer(dgw, reader);
                            }
                            reader.Close();
                        }
                    }
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                }
                //dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                //dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);

                dgw.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                dgw.Columns[0].Width = 40;
                dgw.Columns[1].Width = 200;
                dgw.Columns[2].Width = 200;
                dgw.Columns[3].Width = 424;
                dgw.Columns[4].Width = 300;
                dgw.Columns[5].Width = 142;

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    dgw.Rows[i].Height = 140;
                }
            }
        }

        internal static void ReedSingleRowEnginer(DataGridView dgw, IDataRecord record)
        {
            dgw.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3),
                record.GetString(4), record.GetString(5), RowState.ModifieldNew)));
        }

        internal static void RefreshDataGridEngineerModel(DataGridView dgw, string model)
        {
            if (Internet_check.CheackSkyNET())
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = myCulture;
                dgw.Rows.Clear();

                string queryString = $"SELECT id, model, problem, info, actions, author FROM problem_engineer WHERE model = '{model}'";

                using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ReedSingleRowEnginer(dgw, reader);
                            }
                            reader.Close();
                        }
                    }
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                }
                dgw.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                dgw.Columns[0].Width = 40;
                dgw.Columns[1].Width = 200;
                dgw.Columns[2].Width = 200;
                dgw.Columns[3].Width = 300;
                dgw.Columns[4].Width = 424;
                dgw.Columns[5].Width = 142;

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    dgw.Rows[i].Height = 140;
                }
            }
        }

        internal static void RefreshDataGridEngineerModelProblem(DataGridView dgw, string model, string problem)
        {
            if (Internet_check.CheackSkyNET())
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = myCulture;
                dgw.Rows.Clear();

                string queryString = $"SELECT id, model, problem, info, actions, author FROM problem_engineer WHERE model = '{model}' AND problem = '{problem}'";

                using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ReedSingleRowEnginer(dgw, reader);
                            }
                            reader.Close();
                        }
                    }
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                }
                dgw.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                dgw.Columns[0].Width = 40;
                dgw.Columns[1].Width = 200;
                dgw.Columns[2].Width = 200;
                dgw.Columns[3].Width = 300;
                dgw.Columns[4].Width = 424;
                dgw.Columns[5].Width = 142;

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    dgw.Rows[i].Height = 140;
                }
            }
        }

        internal static void RefreshDataGridEngineerAuthor(DataGridView dgw, string author)
        {
            if (Internet_check.CheackSkyNET())
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = myCulture;
                dgw.Rows.Clear();

                string queryString = $"SELECT id, model, problem, info, actions, author FROM problem_engineer WHERE author = '{author}'";

                using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ReedSingleRowEnginer(dgw, reader);
                            }
                            reader.Close();
                        }
                    }
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                }
                dgw.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                dgw.Columns[0].Width = 40;
                dgw.Columns[1].Width = 200;
                dgw.Columns[2].Width = 200;
                dgw.Columns[3].Width = 300;
                dgw.Columns[4].Width = 424;
                dgw.Columns[5].Width = 142;

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    dgw.Rows[i].Height = 140;
                }
            }
        }

        #endregion

        #region Куратор
        internal static void CreateColumsСurator(DataGridView dgw)
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
            dgw.Columns.Add("numberActRemont", "№ акта ремонта");
            dgw.Columns.Add("category", "Категория");
            dgw.Columns.Add("priceRemont", "Цена ремонта");
            dgw.Columns.Add("decommissionSerialNumber", "№ акта списания");
            dgw.Columns.Add("comment", "Примечание");
            dgw.Columns.Add("month", "Месяц выполнения");
            dgw.Columns.Add("road", "Дорога");
            dgw.Columns.Add("IsNew", "RowState");
            dgw.Columns[19].Visible = false;
        }

        internal static void ReedSingleRowСurator(DataGridView dgw, IDataRecord record)
        {
            dgw.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4),
                     record.GetString(5), record.GetString(6), record.GetString(7), Convert.ToDateTime(record.GetString(8)), record.GetString(9),
                     record.GetString(10), record.GetDecimal(11), record.GetString(12), record.GetString(13), record.GetDecimal(14),
                     record.GetString(15), record.GetString(16), record.GetString(17), record.GetString(18), RowState.ModifieldNew)));

        }

        internal static void RefreshDataGridСurator(DataGridView dgw, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (!String.IsNullOrEmpty(road))
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();
                    string queryString = $"SELECT id, poligon, company, location, model, serialNumber, " +
                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont, " +
                        $"category, priceRemont, decommissionSerialNumber, comment, month, road FROM " +
                        $"radiostantion_сomparison WHERE road = '{road}'";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB_4.GetInstance.GetConnection()))
                    {
                        DB_4.GetInstance.OpenConnection();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ReedSingleRowСurator(dgw, reader);
                                }
                                reader.Close();
                            }
                        }
                        command.ExecuteNonQuery();
                        DB_4.GetInstance.CloseConnection();
                    }
                }
                else dgw.Rows.Clear();


                dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                dgw.Columns[0].Width = 45;
                dgw.Columns[3].Width = 170;
                dgw.Columns[4].Width = 170;
                dgw.Columns[5].Width = 170;
                dgw.Columns[6].Width = 170;
                dgw.Columns[7].Width = 178;
                dgw.Columns[8].Width = 100;
                dgw.Columns[9].Width = 110;
                dgw.Columns[10].Width = 100;
                dgw.Columns[11].Width = 100;
                dgw.Columns[17].Width = 120;
                if (dgw.Rows.Count > 1)
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[0];

            }
        }

        internal static void RefreshDataGridСuratorCity(DataGridView dgw, string city, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (!String.IsNullOrEmpty(city))
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();
                    string queryString = $"SELECT id, poligon, company, location, model, serialNumber, " +
                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont, " +
                        $"category, priceRemont, decommissionSerialNumber, comment, month, road FROM " +
                        $"radiostantion_сomparison WHERE city = '{city}' AND road = '{road}'";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB_4.GetInstance.GetConnection()))
                    {
                        DB_4.GetInstance.OpenConnection();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ReedSingleRowСurator(dgw, reader);
                                }
                                reader.Close();
                            }
                        }
                        command.ExecuteNonQuery();
                        DB_4.GetInstance.CloseConnection();
                    }
                }
                else dgw.Rows.Clear();


                dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                dgw.Columns[0].Width = 45;
                dgw.Columns[3].Width = 170;
                dgw.Columns[4].Width = 170;
                dgw.Columns[5].Width = 170;
                dgw.Columns[6].Width = 170;
                dgw.Columns[7].Width = 178;
                dgw.Columns[8].Width = 100;
                dgw.Columns[9].Width = 110;
                dgw.Columns[10].Width = 100;
                dgw.Columns[11].Width = 100;
                dgw.Columns[17].Width = 120;
                if (dgw.Rows.Count > 1)
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[0];
            }
        }

        internal static void RefreshDataGridСuratorMonth(DataGridView dgw, string road, string month)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (!String.IsNullOrEmpty(road))
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();
                    string queryString = $"SELECT id, poligon, company, location, model, serialNumber, " +
                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont, " +
                        $"category, priceRemont, decommissionSerialNumber, comment, month, road FROM " +
                        $"radiostantion_сomparison WHERE road = '{road}' AND month = '{month}'";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB_4.GetInstance.GetConnection()))
                    {
                        DB_4.GetInstance.OpenConnection();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ReedSingleRowСurator(dgw, reader);
                                }
                                reader.Close();
                            }
                        }
                        command.ExecuteNonQuery();
                        DB_4.GetInstance.CloseConnection();
                    }
                }
                else dgw.Rows.Clear();


                dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                dgw.Columns[0].Width = 45;
                dgw.Columns[3].Width = 170;
                dgw.Columns[4].Width = 170;
                dgw.Columns[5].Width = 170;
                dgw.Columns[6].Width = 170;
                dgw.Columns[7].Width = 178;
                dgw.Columns[8].Width = 100;
                dgw.Columns[9].Width = 110;
                dgw.Columns[10].Width = 100;
                dgw.Columns[11].Width = 100;
                dgw.Columns[17].Width = 120;
                if (dgw.Rows.Count > 1)
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[0];
            }
        }

        internal static void ReedSingleRowСuratorTimerEventProcessor(DataGridView dgw, IDataRecord record)
        {
            dgw.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4),
                     record.GetString(5), record.GetString(6), record.GetString(7), Convert.ToDateTime(record.GetString(8)), record.GetString(9),
                     record.GetString(10), record.GetDecimal(11), record.GetString(12), record.GetString(13), record.GetDecimal(14),
                     record.GetString(15), record.GetString(16), record.GetString(17), record.GetString(18), RowState.ModifieldNew)));
        }

        internal static void RefreshDataGridСuratorTimerEventProcessor(DataGridView dgw, string city, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (!String.IsNullOrEmpty(city))
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();
                    string queryString = $"SELECT id, poligon, company, location, model, serialNumber, " +
                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont, " +
                        $"category, priceRemont, decommissionSerialNumber, comment, month, road " +
                        $"FROM radiostantion_сomparison WHERE city = '{city.Trim()}' AND road = '{road}'";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB_4.GetInstance.GetConnection()))
                    {
                        DB_4.GetInstance.OpenConnection();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ReedSingleRowСuratorTimerEventProcessor(dgw, reader);
                                }
                                reader.Close();
                            }
                        }
                        command.ExecuteNonQuery();
                        DB_4.GetInstance.CloseConnection();
                    }
                }

                dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                dgw.Columns[0].Width = 45;
                dgw.Columns[3].Width = 170;
                dgw.Columns[4].Width = 170;
                dgw.Columns[5].Width = 170;
                dgw.Columns[6].Width = 170;
                dgw.Columns[7].Width = 178;
                dgw.Columns[8].Width = 100;
                dgw.Columns[9].Width = 110;
                dgw.Columns[10].Width = 100;
                dgw.Columns[11].Width = 100;
                dgw.Columns[17].Width = 120;
                //dgw.Columns[39].Width = 300;
                if (dgw.Rows.Count > 1)
                    dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[0];
            }
        }
        #endregion

        #endregion

        #region загрузка всей таблицы ТО в текущем году

        internal static void Full_BD(DataGridView dgw, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = myCulture;
                dgw.Rows.Clear();
                string queryString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                        $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                        $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                        $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                        $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road " +
                        $"FROM radiostantion WHERE road = '{road.Trim()}'";

                using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();

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
                    DB.GetInstance.CloseConnection();
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
        }

        internal static void Full_BD_Curator(DataGridView dgw, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = myCulture;
                dgw.Rows.Clear();

                string queryString = $"SELECT id, poligon, company, location, model, serialNumber, " +
                       $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont, " +
                       $"category, priceRemont, decommissionSerialNumber, comment, month, road FROM " +
                       $"radiostantion_сomparison WHERE road = '{road.Trim()}'";

                using (MySqlCommand command = new MySqlCommand(queryString, DB_4.GetInstance.GetConnection()))
                {
                    DB_4.GetInstance.OpenConnection();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ReedSingleRowСurator(dgw, reader);
                            }
                            reader.Close();
                        }
                    }
                    command.ExecuteNonQuery();
                    DB_4.GetInstance.CloseConnection();
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
        }

        #endregion

        #region поиск по БД 

        internal static void SearchEngineer(DataGridView dgw, string cmb_unique, string txB_search, string cmb_number_unique)
        {
            if (Internet_check.CheackSkyNET())
            {
                var searchString = string.Empty;
                string perem_comboBox = string.Empty;
                dgw.Rows.Clear();

                if (cmb_unique == "Модель")
                {
                    perem_comboBox = "model";
                }
                else if (cmb_unique == "Неисправность")
                {
                    perem_comboBox = "problem";
                }
                else if (cmb_unique == "Автор")
                {
                    perem_comboBox = "author";
                }
                else if (cmb_unique == "Описание неисправности")
                {
                    perem_comboBox = "info";
                }

                txB_search = txB_search.ToUpper();

                if (txB_search == "ВСЕ" || txB_search == "ВСЁ")
                {
                    searchString = $"SELECT id, model, problem, info, actions, author FROM problem_engineer";
                }
                else if (perem_comboBox == "model" || perem_comboBox == "problem" || perem_comboBox == "author")
                {
                    searchString = $"SELECT id, model, problem, info, actions, author FROM problem_engineer WHERE CONCAT ({perem_comboBox}) LIKE '%" + cmb_number_unique + "%'";
                }
                else if (perem_comboBox == "info")
                {
                    searchString = $"SELECT id, model, problem, info, actions, author FROM problem_engineer WHERE CONCAT ({perem_comboBox}) LIKE '%" + txB_search + "%'";
                }

                using (MySqlCommand command = new MySqlCommand(searchString, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ReedSingleRowEnginer(dgw, reader);
                            }
                            reader.Close();
                        }
                    }
                    DB.GetInstance.CloseConnection();
                }

                dgw.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dgw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                dgw.Columns[0].Width = 40;
                dgw.Columns[1].Width = 200;
                dgw.Columns[2].Width = 200;
                dgw.Columns[3].Width = 424;
                dgw.Columns[4].Width = 300;
                dgw.Columns[5].Width = 142;

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    dgw.Rows[i].Height = 140;
                }
            }
        }


        internal static void Search(DataGridView dgw, string comboBox_seach, string city, string textBox_search,
            string cmb_number_unique, string road, string txb_flag_all_BD)
        {
            if (Internet_check.CheackSkyNET())
            {
                var searchString = string.Empty;
                if (txb_flag_all_BD == "Вся БД")
                {
                    string perem_comboBox = "serialNumber";

                    dgw.Rows.Clear();

                    if (comboBox_seach == "Предприятие")
                    {
                        perem_comboBox = "company";
                    }
                    else if (comboBox_seach == "Модель")
                    {
                        perem_comboBox = "model";
                    }
                    else if (comboBox_seach == "Станция")
                    {
                        perem_comboBox = "location";
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

                    var provSeach = textBox_search;
                    provSeach = provSeach.ToUpper();

                    if (provSeach == "ВСЕ" || provSeach == "ВСЁ")
                    {
                        searchString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                            $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                            $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                            $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                            $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion WHERE road = '{road}' AND CONCAT ({perem_comboBox})";
                    }
                    else if (perem_comboBox == "numberAct")
                    {
                        searchString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                            $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                            $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                            $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                            $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion WHERE road = '{road}' AND CONCAT ({perem_comboBox}) LIKE '" + cmb_number_unique + "'";
                    }
                    else if (perem_comboBox == "location" || perem_comboBox == "company" || perem_comboBox == "dateTO" || perem_comboBox == "numberActRemont"
                        || perem_comboBox == "representative" || perem_comboBox == "decommissionSerialNumber" || perem_comboBox == "model" || perem_comboBox == "numberAct")
                    {
                        searchString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                            $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                            $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                            $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                            $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion WHERE road = '{road}' AND CONCAT ({perem_comboBox}) LIKE '%" + cmb_number_unique + "%'";
                    }
                    else
                    {
                        searchString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                            $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                            $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                            $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                            $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion WHERE road = '{road}' AND CONCAT ({perem_comboBox}) LIKE '%" + textBox_search + "%'";
                    }
                    using (MySqlCommand command = new MySqlCommand(searchString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();

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
                        DB.GetInstance.CloseConnection();
                    }
                    if (perem_comboBox == "numberActRemont")
                    {
                        dgw.Sort(dgw.Columns["numberActRemont"], ListSortDirection.Ascending);
                    }
                }
                else
                {
                    string perem_comboBox = "serialNumber";

                    dgw.Rows.Clear();

                    if (comboBox_seach == "Предприятие")
                    {
                        perem_comboBox = "company";
                    }
                    else if (comboBox_seach == "Модель")
                    {
                        perem_comboBox = "model";
                    }
                    else if (comboBox_seach == "Станция")
                    {
                        perem_comboBox = "location";
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

                    var provSeach = textBox_search;
                    provSeach = provSeach.ToUpper();

                    if (provSeach == "ВСЕ" || provSeach == "ВСЁ")
                    {
                        searchString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                            $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                            $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                            $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                            $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion WHERE city = '{city}' AND road = '{road}' AND CONCAT ({perem_comboBox})";
                    }
                    else if (perem_comboBox == "numberAct")
                    {
                        searchString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                            $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                            $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                            $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                            $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion WHERE city = '{city}' AND road = '{road}' AND CONCAT ({perem_comboBox}) LIKE '" + cmb_number_unique + "'";
                    }
                    else if (perem_comboBox == "location" || perem_comboBox == "company" || perem_comboBox == "numberActRemont"
                        || perem_comboBox == "representative" || perem_comboBox == "decommissionSerialNumber" || perem_comboBox == "model" || perem_comboBox == "numberAct")
                    {
                        searchString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                            $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                            $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                            $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                            $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion WHERE city = '{city}' AND road = '{road}' AND CONCAT ({perem_comboBox}) LIKE '%" + cmb_number_unique + "%'";
                    }
                    else if (perem_comboBox == "dateTO")
                    {
                        cmb_number_unique = Convert.ToDateTime(cmb_number_unique).ToString("yyyy-MM-dd");
                        searchString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                            $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                            $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                            $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                            $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion WHERE city = '{city}' AND road = '{road}' AND CONCAT ({perem_comboBox}) LIKE '%" + cmb_number_unique + "%'";
                    }
                    else
                    {
                        searchString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                            $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                            $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                            $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                            $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion WHERE city = '{city}' AND road = '{road}' AND CONCAT ({perem_comboBox}) LIKE '%" + textBox_search + "%'";
                    }
                    using (MySqlCommand command = new MySqlCommand(searchString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();

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
                        DB.GetInstance.CloseConnection();
                    }
                    if (perem_comboBox == "numberActRemont")
                    {
                        dgw.Sort(dgw.Columns["numberActRemont"], ListSortDirection.Ascending);
                    }
                }
            }
        }

        internal static void SearchCurator(DataGridView dgw, string comboBox_seach, string city, string textBox_search, string cmb_number_unique, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                var searchString = string.Empty;
                string perem_comboBox = "serialNumber";

                dgw.Rows.Clear();

                if (comboBox_seach == "Предприятие")
                {
                    perem_comboBox = "company";
                }
                else if (comboBox_seach == "Станция")
                {
                    perem_comboBox = "location";
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
                else if (comboBox_seach == "Номер Акта списания")
                {
                    perem_comboBox = "decommissionSerialNumber";
                }
                else if (comboBox_seach == "Месяц")
                {
                    perem_comboBox = "month";
                }
                else if (comboBox_seach == "Модель")
                {
                    perem_comboBox = "model";
                }

                var provSeach = textBox_search;
                provSeach = provSeach.ToUpper();

                if (provSeach == "ВСЕ" || provSeach == "ВСЁ")
                {
                    searchString = $"SELECT id, poligon, company, location, model, serialNumber, " +
                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont, " +
                        $"category, priceRemont, decommissionSerialNumber, comment, month, road " +
                        $"FROM radiostantion_сomparison WHERE city = '{city}' AND road = '{road}' AND CONCAT ({perem_comboBox})";
                }
                else if (perem_comboBox == "numberAct")
                {
                    searchString = $"SELECT id, poligon, company, location, model, serialNumber, " +
                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont, " +
                        $"category, priceRemont, decommissionSerialNumber, comment, month, road " +
                        $" FROM radiostantion_сomparison WHERE city = '{city}' AND road = '{road}' AND CONCAT ({perem_comboBox}) LIKE '" + cmb_number_unique + "'";
                }
                else if (perem_comboBox == "location" || perem_comboBox == "company" || perem_comboBox == "numberActRemont"
                    || perem_comboBox == "representative" || perem_comboBox == "decommissionSerialNumber" || perem_comboBox == "month" || perem_comboBox == "model")
                {
                    searchString = $"SELECT id, poligon, company, location, model, serialNumber, " +
                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont, " +
                        $"category, priceRemont, decommissionSerialNumber, comment, month, road " +
                        $" FROM radiostantion_сomparison WHERE city = '{city}' AND road = '{road}' AND CONCAT ({perem_comboBox}) LIKE '%" + cmb_number_unique + "%'";
                }
                else if (perem_comboBox == "dateTO")
                {
                    cmb_number_unique = Convert.ToDateTime(cmb_number_unique).ToString("yyyy-MM-dd");
                    searchString = $"SELECT id, poligon, company, location, model, serialNumber, " +
                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont, " +
                        $"category, priceRemont, decommissionSerialNumber, comment, month, road " +
                        $" FROM radiostantion_сomparison WHERE city = '{city}' AND road = '{road}' AND CONCAT ({perem_comboBox}) LIKE '%" + cmb_number_unique + "%'";
                }
                else
                {
                    searchString = $"SELECT id, poligon, company, location, model, serialNumber, " +
                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont, " +
                        $"category, priceRemont, decommissionSerialNumber, comment, month, road " +
                        $" FROM radiostantion_сomparison WHERE city = '{city}' AND CONCAT ({perem_comboBox}) LIKE '%" + textBox_search + "%'";
                }

                using (MySqlCommand command = new MySqlCommand(searchString, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ReedSingleRowСurator(dgw, reader);
                            }
                            reader.Close();
                        }
                    }
                    DB.GetInstance.CloseConnection();
                }
                if (perem_comboBox == "numberActRemont")
                {
                    dgw.Sort(dgw.Columns["numberActRemont"], ListSortDirection.Ascending);
                }
            }
        }

        #endregion

        #region поиск по номеру акта для Combobox на подпись и акты до Full
        internal static void SearchNumberActCombobox(DataGridView dgw, string city, string road, string numberAct)
        {
            dgw.Rows.Clear();
            var searchString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                        $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                        $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                        $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                        $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion WHERE " +
                        $"numberAct = '{numberAct}' AND city = '{city}' AND road = '{road}'";

            using (MySqlCommand command = new MySqlCommand(searchString, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();

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
                DB.GetInstance.CloseConnection();
            }
        }
        #endregion

        #region поиск отсутсвующих рст исходя из предыдущего года

        internal static void Seach_DataGrid_Replay_RST(DataGridView dgw, TextBox txb_flag_all_BD, string city, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (txb_flag_all_BD.Text == "Вся БД")
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();

                    string queryString = $"SELECT radiostantion_full. * FROM radiostantion_full LEFT JOIN " +
                        $"radiostantion ON (radiostantion_full.serialNumber=radiostantion.serialNumber) " +
                        $"WHERE radiostantion.serialNumber IS NULL AND radiostantion_full.road LIKE '" + road + "'";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();

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
                        DB.GetInstance.CloseConnection();
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

                else if (!String.IsNullOrEmpty(city))
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();

                    string queryString = $"SELECT radiostantion_full. * FROM radiostantion_full LEFT JOIN radiostantion " +
                        $"ON (radiostantion_full.serialNumber=radiostantion.serialNumber) WHERE radiostantion.serialNumber IS NULL " +
                        $"AND radiostantion_full.city LIKE '" + city + "'AND radiostantion_full.road LIKE '" + road + "'";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();

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
                        DB.GetInstance.CloseConnection();
                    }
                }

                txb_flag_all_BD.Text = "";

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

        }

        #endregion

        #region update_datagridview_number_act

        internal static int Update_datagridview_number_act(DataGridView dgw, string city, string numberAct, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                dgw.Rows.Clear();
                dgw.AllowUserToAddRows = false;

                string queryString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                        $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                        $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                        $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                        $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road FROM radiostantion " +
                        $"WHERE city = '{city.Trim()}' AND numberAct = '{numberAct.Trim()}' AND road = '{road}'";

                using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();

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
                    DB.GetInstance.CloseConnection();
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

                return dgw.RowCount;
            }
            else return 0;
        }

        internal static void Update_datagridview_number_act_curator(DataGridView dgw, string city, string numberAct)
        {
            if (Internet_check.CheackSkyNET())
            {
                dgw.Rows.Clear();
                dgw.AllowUserToAddRows = false;

                string searchString = $"SELECT * FROM radiostantion_сomparison WHERE city = '{city.Trim()}' AND numberAct = '{numberAct.Trim()}'";

                using (MySqlCommand command = new MySqlCommand(searchString, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ReedSingleRowСurator(dgw, reader);
                            }
                            reader.Close();
                        }
                    }
                    DB.GetInstance.CloseConnection();
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
        }

        #endregion

        #region для счётчика резервное копирование радиостанций из текущей radiostantion в radiostantion_copy
        internal static void Copy_BD_radiostantion_in_radiostantion_copy()
        {
            if (Internet_check.CheackSkyNET())
            {
                var clearBD = "TRUNCATE TABLE radiostantion_copy";

                using (MySqlCommand command = new MySqlCommand(clearBD, DB_2.GetInstance.GetConnection()))
                {
                    if (Internet_check.CheackSkyNET())
                    {
                        DB_2.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        DB_2.GetInstance.CloseConnection();
                    }
                }

                var copyBD = "INSERT INTO radiostantion_copy SELECT * FROM radiostantion";

                using (MySqlCommand command2 = new MySqlCommand(copyBD, DB_2.GetInstance.GetConnection()))
                {
                    if (Internet_check.CheackSkyNET())
                    {
                        DB_2.GetInstance.OpenConnection();
                        command2.ExecuteNonQuery();
                        DB_2.GetInstance.CloseConnection();
                    }
                }
            }
        }

        internal static void Copy_BD_radiostantion_сomparison_in_radiostantion_сomparison_copy()
        {
            if (Internet_check.CheackSkyNET())
            {
                var clearBD = "TRUNCATE TABLE radiostantion_сomparison_copy";

                using (MySqlCommand command = new MySqlCommand(clearBD, DB_2.GetInstance.GetConnection()))
                {
                    if (Internet_check.CheackSkyNET())
                    {
                        DB_2.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        DB_2.GetInstance.CloseConnection();
                    }
                }

                var copyBD = "INSERT INTO radiostantion_сomparison_copy SELECT * FROM radiostantion_сomparison";

                using (MySqlCommand command2 = new MySqlCommand(copyBD, DB_2.GetInstance.GetConnection()))
                {
                    if (Internet_check.CheackSkyNET())
                    {
                        DB_2.GetInstance.OpenConnection();
                        command2.ExecuteNonQuery();
                        DB_2.GetInstance.CloseConnection();
                    }
                }
            }
        }


        #endregion

        #region изменить номер акта у радиостанции

        internal static void ChangeNumberAct(DataGridView dgw, string txB_pnl_ChangeNumberActTOFull, string city, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                foreach (DataGridViewRow row in dgw.SelectedRows)
                {
                    dgw.Rows[row.Index].Cells[41].Value = RowState.Change;
                }

                for (int index = 0; index < dgw.Rows.Count; index++)
                {
                    var rowState = (RowState)dgw.Rows[index].Cells[41].Value;//проверить индекс

                    if (rowState == RowState.Change)
                    {
                        var id = Convert.ToInt32(dgw.Rows[index].Cells[0].Value);
                        var numberAct = dgw.Rows[index].Cells[9].Value;
                        //UPDATE radiostantion SET numberAct = '51/1' WHERE numberAct = '53/1'
                        var changeQuery = $"UPDATE radiostantion SET numberAct = '{txB_pnl_ChangeNumberActTOFull}' WHERE numberAct = '{numberAct}' " +
                            $"AND city = '{city}' AND road = '{road}' AND id = '{id}'";

                        using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            command.ExecuteNonQuery();
                            DB.GetInstance.CloseConnection();

                        }
                    }
                }
            }
        }

        #endregion

        #region Удаление

        internal static void DeleteRowCell(DataGridView dgw)
        {
            if (Internet_check.CheackSkyNET())
            {
                foreach (DataGridViewRow row in dgw.SelectedRows)
                {
                    dgw.Rows[row.Index].Cells[41].Value = RowState.Deleted;
                }

                for (int index = 0; index < dgw.Rows.Count; index++)
                {
                    var rowState = (RowState)dgw.Rows[index].Cells[41].Value;//проверить индекс

                    if (rowState == RowState.Deleted)
                    {
                        var id = Convert.ToInt32(dgw.Rows[index].Cells[0].Value);
                        var deleteQuery = $"DELETE FROM radiostantion WHERE id = {id}";

                        using (MySqlCommand command = new MySqlCommand(deleteQuery, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            command.ExecuteNonQuery();
                            DB.GetInstance.CloseConnection();

                        }
                    }
                }
            }
        }

        internal static void DeleteRowСellCurator(DataGridView dgw)
        {
            if (Internet_check.CheackSkyNET())
            {
                foreach (DataGridViewRow row in dgw.SelectedRows)
                {
                    dgw.Rows[row.Index].Cells[19].Value = RowState.Deleted;
                }

                for (int index = 0; index < dgw.Rows.Count; index++)
                {
                    var rowState = (RowState)dgw.Rows[index].Cells[19].Value;//проверить индекс

                    if (rowState == RowState.Deleted)
                    {
                        var id = Convert.ToInt32(dgw.Rows[index].Cells[0].Value);
                        var deleteQuery = $"DELETE FROM radiostantion_сomparison WHERE id = {id}";

                        using (MySqlCommand command = new MySqlCommand(deleteQuery, DB_4.GetInstance.GetConnection()))
                        {
                            DB_4.GetInstance.OpenConnection();
                            command.ExecuteNonQuery();
                            DB_4.GetInstance.CloseConnection();
                        }
                    }
                }
            }
        }
        #endregion

        #region Удаление ремонта

        internal static void Delete_rst_remont(string numberActRemont, string serialNumber, string city, string road)
        {
            if (Internet_check.CheackSkyNET())
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
                            $"WHERE serialNumber = '{serialNumber}' AND city = '{city}' AND road = '{road}'";

                        using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            command.ExecuteNonQuery();
                            DB.GetInstance.CloseConnection();
                        }
                    }
                }
            }
        }

        static Boolean CheacknumberActRemont_radiostantion(string numberActRemont)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT numberActRemont FROM radiostantion WHERE numberActRemont = '{numberActRemont}'";

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

        #endregion

        #region списание рст

        internal static void Record_decommissionSerialNumber(string serialNumber, string decommissionSerialNumber,
            string city, string poligon, string company, string location, string model, string dateTO, string price, string representative, string post,
            string numberIdentification, string dateIssue, string phoneNumber, string antenna, string manipulator,
            string AKB, string batteryСharger, string txB_reason_decommission, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (serialNumber != "")
                {
                    var changeQuery = $"UPDATE radiostantion SET inventoryNumber = 'списание', networkNumber = 'списание', price = '{0.00}', " +
                        $"decommissionSerialNumber = '{decommissionSerialNumber}', numberAct = '', numberActRemont = '', " +
                        $"category = '', completed_works_1 = '', completed_works_2 = '', completed_works_3 = '', completed_works_4 = ''," +
                        $"completed_works_5 = '', completed_works_6 = '', completed_works_7 = '', parts_1 = '', parts_2 = '', parts_3 = '', " +
                        $"parts_4 = '', parts_5 = '', parts_6 = '', parts_7 = '', comment = '{txB_reason_decommission}' " +
                        $"WHERE serialNumber = '{serialNumber}' AND city = '{city}' AND road = '{road}'";

                    using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        DB.GetInstance.CloseConnection();
                    }

                    if (CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantion_full(serialNumber))
                    {

                        var changeQuery2 = $"UPDATE radiostantion_full SET inventoryNumber = 'списание', networkNumber = 'списание', price = '{0.00}', " +
                            $"decommissionSerialNumber = '{decommissionSerialNumber}', numberAct = 'списание', numberActRemont = 'списание', " +
                            $"category = '', completed_works_1 = '', completed_works_2 = '', completed_works_3 = '', completed_works_4 = ''," +
                            $"completed_works_5 = '', completed_works_6 = '', completed_works_7 = '', parts_1 = '', parts_2 = '', parts_3 = '', " +
                            $"parts_4 = '', parts_5 = '', parts_6 = '', parts_7 = '', comment = '{txB_reason_decommission}' WHERE serialNumber = '{serialNumber}' AND city = '{city}' AND road = '{road}'";


                        using (MySqlCommand command2 = new MySqlCommand(changeQuery2, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            command2.ExecuteNonQuery();
                            DB.GetInstance.CloseConnection();
                        }
                    }

                    if (!CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantion_decommission(serialNumber))
                    {
                        dateTO = Convert.ToDateTime(dateTO).ToString("yyyy-MM-dd");
                        var addQuery = $"INSERT INTO radiostantion_decommission (poligon, company, location, model, serialNumber," +
                                    $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, representative, " +
                                    $"post, numberIdentification, dateIssue, phoneNumber, numberActRemont, category, priceRemont, " +
                                    $"antenna, manipulator, AKB, batteryСharger, completed_works_1, completed_works_2, completed_works_3, " +
                                    $"completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1, parts_2, parts_3, parts_4, " +
                                    $"parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road) VALUES ('{poligon.Trim()}', '{company.Trim()}', '{location.Trim()}'," +
                                    $"'{model.Trim()}','{serialNumber.Trim()}', 'списание', 'списание', " +
                                    $"'{dateTO.Trim()}','списание','{city.Trim()}','{price.Trim()}', '{representative.Trim()}', '{post.Trim()}', " +
                                    $"'{numberIdentification.Trim()}', '{dateIssue.Trim()}', '{phoneNumber.Trim()}', '{""}', '{""}', '{0.00}'," +
                                    $"'{antenna.Trim()}', '{manipulator.Trim()}', '{AKB.Trim()}', '{batteryСharger.Trim()}', '{""}', '{""}', " +
                                    $"'{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', " +
                                    $"'{decommissionSerialNumber}', '{txB_reason_decommission}', '{road}')";

                        using (MySqlCommand command3 = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            command3.ExecuteNonQuery();
                            DB.GetInstance.CloseConnection();
                        }
                    }

                }
            }
        }

        #endregion

        #region Удалить номер списание из таблицы radiostantion

        internal static void Delete_decommissionSerialNumber_radiostantion(DataGridView dgw2, string decommissionSerialNumber, string serialNumber,
            string city, ComboBox cmB_model, TextBox txB_numberAct, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                var price = "";
                if (!String.IsNullOrEmpty(decommissionSerialNumber))
                {

                    if (cmB_model.Text == "Icom IC-F3GT" || cmB_model.Text == "Icom IC-F11" || cmB_model.Text == "Icom IC-F16" ||
                        cmB_model.Text == "Icom IC-F3GS" || cmB_model.Text == "Motorola P040" || cmB_model.Text == "Motorola P080" ||
                        cmB_model.Text == "Motorola GP-300" || cmB_model.Text == "Motorola GP-320" || cmB_model.Text == "Motorola GP-340" ||
                        cmB_model.Text == "Motorola GP-360" || cmB_model.Text == "Альтавия-301М" || cmB_model.Text == "Comrade R5" ||
                        cmB_model.Text == "Гранит Р33П-1" || cmB_model.Text == "Гранит Р-43" || cmB_model.Text == "Радий-301" ||
                        cmB_model.Text == "Kenwood ТК-2107" || cmB_model.Text == "Vertex - 261" || cmB_model.Text == "РА-160")
                    {
                        price = "1411.18";
                    }
                    else
                    {
                        price = "1919.57";
                    }

                    var reg = new Regex("C");
                    txB_numberAct.Text = reg.Replace(txB_numberAct.Text, "");

                    var changeQuery = $"UPDATE radiostantion SET inventoryNumber = 'Измени', networkNumber = 'Измени', " +
                        $"price = '{price}', numberAct = '{txB_numberAct.Text}', decommissionSerialNumber = '', comment = '' WHERE serialNumber = '{serialNumber}' AND city = '{city}' AND road = '{road}' ";

                    using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        DB.GetInstance.CloseConnection();
                        MessageBox.Show("Списание удалено! Заполни \"номер акта\", \"инвентарный и сетевой номер\" заново!");
                    }

                    var deleteQuery = $"DELETE FROM radiostantion_decommission WHERE serialNumber = '{serialNumber}'";

                    using (MySqlCommand command2 = new MySqlCommand(deleteQuery, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        command2.ExecuteNonQuery();
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }
        #endregion

        #region показать списания

        internal static void Show_radiostantion_decommission(DataGridView dgw, string city, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (!String.IsNullOrEmpty(city))
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();
                    string queryString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                        $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                        $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                        $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                        $" parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road " +
                        $"FROM radiostantion_decommission WHERE road = '{road}'";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();

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
                        DB.GetInstance.CloseConnection();
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
        }

        #endregion

        #region показать уникальные данные по поиску

        #region инженер

        internal static void Cmb_unique_model_engineer(ComboBox cmb_unique)
        {
            string querystring2 = $"SELECT DISTINCT model FROM problem_engineer ORDER BY model";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable table = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);

                    cmb_unique.DataSource = table;
                    cmb_unique.DisplayMember = "model";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Cmb_unique_problem_engineer(ComboBox cmb_unique)
        {
            string querystring2 = $"SELECT DISTINCT problem FROM problem_engineer ORDER BY problem";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable table = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);

                    cmb_unique.DataSource = table;
                    cmb_unique.DisplayMember = "problem";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Cmb_unique_author_engineer(ComboBox cmb_unique)
        {
            string querystring2 = $"SELECT DISTINCT author FROM problem_engineer ORDER BY author";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable table = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);

                    cmb_unique.DataSource = table;
                    cmb_unique.DisplayMember = "author";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        #endregion

        #region Куратор

        internal static void Number_unique_model_curator(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT model FROM radiostantion_сomparison WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY model";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "model";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_company_curator(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {

            string querystring2 = $"SELECT DISTINCT company FROM radiostantion_сomparison WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY company";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "company";
                    DB.GetInstance.CloseConnection();
                }
            }

        }

        internal static void Number_unique_location_curator(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {

            string querystring2 = $"SELECT DISTINCT location FROM radiostantion_сomparison WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY location";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "location";
                    DB.GetInstance.CloseConnection();
                }
            }

        }

        internal static void Number_unique_dateTO_curator(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT dateTO FROM radiostantion_сomparison WHERE city = '{comboBox_city}'  AND road = '{road}' ORDER BY dateTO";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "dateTO";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_numberAct_curator(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT numberAct FROM radiostantion_сomparison WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY numberAct";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "numberAct";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_numberActRemont_curator(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT numberActRemont FROM radiostantion_сomparison WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY numberActRemont";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "numberActRemont";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_decommissionActs_curator(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {

            string querystring2 = $"SELECT DISTINCT decommissionSerialNumber FROM radiostantion_сomparison WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY decommissionSerialNumber";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "decommissionSerialNumber";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_month_curator(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT month FROM radiostantion_сomparison WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY month";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "month";
                    DB.GetInstance.CloseConnection();
                }
            }
        }
        #endregion

        #region Начальник участка вся БД
        internal static void Number_unique_company_full_BD(ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT company FROM radiostantion WHERE road = '{road}' ORDER BY company";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "company";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_model_full_BD(ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT model FROM radiostantion WHERE road = '{road}' ORDER BY model";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "model";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_location_full_BD(ComboBox cmb_number_unique_acts, string road)
        {

            string querystring2 = $"SELECT DISTINCT location FROM radiostantion WHERE road = '{road}' ORDER BY location";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "location";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_dateTO_full_BD(ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT dateTO FROM radiostantion WHERE road = '{road}' ORDER BY dateTO";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);
                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "dateTO";
                    cmb_number_unique_acts.ValueMember = "dateTO";

                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_numberAct_full_BD(ComboBox cmb_number_unique_acts, string road)
        {

            string querystring2 = $"SELECT DISTINCT numberAct FROM radiostantion WHERE road = '{road}' ORDER BY numberAct";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "numberAct";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_numberActRemont_full_BD(ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT numberActRemont FROM radiostantion WHERE road = '{road}' ORDER BY numberActRemont";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "numberActRemont";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_representative_full_BD(ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT representative FROM radiostantion WHERE road = '{road}' ORDER BY representative";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "representative";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_decommissionActs_full_BD(ComboBox cmb_number_unique_acts, string road)
        {

            string querystring2 = $"SELECT DISTINCT decommissionSerialNumber FROM radiostantion WHERE road = '{road}' ORDER BY decommissionSerialNumber";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "decommissionSerialNumber";
                    DB.GetInstance.CloseConnection();
                }
            }

        }

        #endregion

        #region Начальник участка для города

        internal static void Number_unique_company(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT company FROM radiostantion WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY company";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "company";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_model(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT model FROM radiostantion WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY model";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "model";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_location(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {

            string querystring2 = $"SELECT DISTINCT location FROM radiostantion WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY location";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "location";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_dateTO(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {
            List<string> newList = new List<string>();
            string querystring2 = $"SELECT DISTINCT dateTO FROM radiostantion WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY dateTO DESC";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            newList.Add(reader.GetDateTime(0).ToString("dd.MM.yyyy"));
                        }
                        reader.Close();
                    }
                }
                DB.GetInstance.CloseConnection();
                var result = newList.Distinct().Reverse().Reverse().ToList();
                cmb_number_unique_acts.DataSource = result;
            }
        }

        internal static void Number_unique_numberAct(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {

            string querystring2 = $"SELECT DISTINCT numberAct FROM radiostantion WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY numberAct";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "numberAct";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_numberActRemont(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT numberActRemont FROM radiostantion WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY numberActRemont";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "numberActRemont";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_representative(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {
            string querystring2 = $"SELECT DISTINCT representative FROM radiostantion WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY representative";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "representative";
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        internal static void Number_unique_decommissionActs(string comboBox_city, ComboBox cmb_number_unique_acts, string road)
        {

            string querystring2 = $"SELECT DISTINCT decommissionSerialNumber FROM radiostantion WHERE city = '{comboBox_city}' AND road = '{road}' ORDER BY decommissionSerialNumber";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable act_table_unique = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(act_table_unique);

                    cmb_number_unique_acts.DataSource = act_table_unique;
                    cmb_number_unique_acts.DisplayMember = "decommissionSerialNumber";
                    DB.GetInstance.CloseConnection();
                }
            }

        }

        #endregion

        #endregion

        #region показать все радиостанции по участку без списаний

        internal static void RefreshDataGridtDecommissionByPlot(DataGridView dgw, string city, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (!String.IsNullOrEmpty(city))
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();

                    string queryString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                        $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                        $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                        $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                        $"parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road " +
                        $"FROM radiostantion WHERE city LIKE N'%{city.Trim()}%' AND decommissionSerialNumber != '' AND road = '{road}'";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();

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
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }

        internal static void RefreshDataGridWithoutDecommission(DataGridView dgw, string city, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (!String.IsNullOrEmpty(city))
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();

                    string queryString = $"SELECT id, poligon, company, location, model, serialNumber, inventoryNumber, " +
                        $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, " +
                        $"phoneNumber, numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, " +
                        $"completed_works_2, completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1," +
                        $"parts_2, parts_3, parts_4, parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road " +
                        $"FROM radiostantion WHERE city LIKE N'%{city.Trim()}%' AND decommissionSerialNumber = '' AND road = '{road}'";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();

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
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }

        #endregion

        #region заполнение cmB_city из таблицы

        internal static void SelectCityGropByCurator(ComboBox cmB_city, ComboBox cmB_road)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT city FROM radiostantion_сomparison WHERE road = '{cmB_road.Text}' GROUP BY city";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable city_table = new DataTable();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(city_table);
                        if (city_table.Rows.Count > 0)
                        {
                            cmB_city.DataSource = city_table;
                            cmB_city.DisplayMember = "city";
                        }
                        else
                        {
                            MessageBox.Show("Добавь радиостанцию в выполнение!");
                            cmB_city.DataSource = null;
                            return;
                        }
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }

        internal static void SelectCityGropByMonthRoad(ComboBox cmB_road, ComboBox cmB_month)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT month FROM radiostantion_сomparison WHERE road = '{cmB_road.Text}' GROUP BY month";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable table = new DataTable();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            cmB_month.DataSource = table;
                            cmB_month.DisplayMember = "month";
                        }
                        else
                        {
                            cmB_month.DataSource = null;
                        }
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }

        internal static void SelectCityGropByMonthCity(ComboBox cmB_city, ComboBox cmB_road, ComboBox cmB_month)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT month FROM radiostantion_сomparison WHERE city = '{cmB_city.Text}' AND road = '{cmB_road.Text}' GROUP BY month";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable table = new DataTable();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            cmB_month.DataSource = table;
                            cmB_month.DisplayMember = "month";
                        }
                        else
                        {
                            cmB_month.DataSource = null;
                        }
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }

        internal static void ProblemGetEngineerAuthor(ComboBox cmB_problem, string author)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT problem FROM problem_engineer WHERE author = '{author}' GROUP BY problem";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable table = new DataTable();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            cmB_problem.DataSource = table;
                            cmB_problem.DisplayMember = "problem";
                        }
                        else
                        {
                            cmB_problem.DataSource = null;
                        }
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }

        internal static void ModelGetEngineerAuthor(ComboBox cmB_model, string author)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT model FROM problem_engineer WHERE author = '{author}' GROUP BY model";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable table = new DataTable();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            cmB_model.DataSource = table;
                            cmB_model.DisplayMember = "model";
                        }
                        else
                        {
                            cmB_model.DataSource = null;
                        }
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }

        internal static void SelectCityGropBy(ComboBox cmB_city, ComboBox cmB_road)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT city FROM radiostantion WHERE road = '{cmB_road.Text}' GROUP BY city";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable city_table = new DataTable();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(city_table);
                        if (city_table.Rows.Count > 0)
                        {
                            cmB_city.DataSource = city_table;
                            cmB_city.DisplayMember = "city";
                        }
                        else
                        {
                            cmB_city.DataSource = null;
                        }
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }
        #endregion

        #region OC-6 для ремонтов

        internal static Tuple<string, string> Loading_OC_6_values(string serialNumber, string city, string road)
        {
            string mainMeans = "";
            string nameProductRepaired = "";
            try
            {
                if (Internet_check.CheackSkyNET())
                {
                    string querySelectOC = $"SELECT mainMeans, nameProductRepaired FROM radiostantion_full WHERE serialNumber = '{serialNumber}' AND city = '{city}' AND road = '{road}'";

                    using (MySqlCommand command = new MySqlCommand(querySelectOC, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mainMeans = reader[0].ToString();
                                nameProductRepaired = reader[1].ToString();
                            }
                            reader.Close();
                        }
                    }
                }
                return Tuple.Create(mainMeans, nameProductRepaired);
            }
            catch
            {
                return Tuple.Create(mainMeans, nameProductRepaired);
            }

        }

        #endregion

        #region получ. крайнего номера акта ремонта из БД

        internal static void LoadingLastNumberActRemont(Label lbL_last_act_remont, string city, string road)
        {
            try
            {
                var queryLastNumberActRemont = $"SELECT numberActRemont FROM radiostantion WHERE city = '{city}' AND road = '{road}' ORDER BY numberActRemont DESC LIMIT 1";
                using (MySqlCommand command = new MySqlCommand(queryLastNumberActRemont, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lbL_last_act_remont.Text = reader[0].ToString();
                        }
                        reader.Close();
                    }
                    DB.GetInstance.CloseConnection();
                }
            }
            catch (Exception)
            {
                DB.GetInstance.CloseConnection();
                lbL_last_act_remont.Text = "Пустой";
            }
        }

        internal static void LoadingLastDecommissionSerialNumber(Label lbL_last_decommission, string cmB_city, string road)
        {
            try
            {
                var queryLastNumberActRemont = $"SELECT decommissionSerialNumber FROM (SELECT decommissionSerialNumber FROM radiostantion WHERE city = '{cmB_city}' AND road = '{road}' ORDER BY id DESC LIMIT 100) t ORDER BY decommissionSerialNumber DESC LIMIT 1";
                using (MySqlCommand command = new MySqlCommand(queryLastNumberActRemont, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lbL_last_decommission.Text = reader[0].ToString();
                        }
                        reader.Close();
                    }
                    DB.GetInstance.CloseConnection();
                }
            }
            catch (Exception)
            {
                DB.GetInstance.CloseConnection();
                lbL_last_decommission.Text = "Пустой";
            }
        }
        internal static void LoadingLastNumberActTO(Label lbL_last_act, string cmB_city, string road)
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    var queryLastNumberActRemont = $"SELECT numberAct FROM (SELECT numberAct FROM radiostantion WHERE city = '{cmB_city}' AND road = '{road}' ORDER BY id DESC LIMIT 100) t ORDER BY numberAct DESC LIMIT 1";
                    using (MySqlCommand command = new MySqlCommand(queryLastNumberActRemont, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lbL_last_act.Text = reader[0].ToString();
                            }
                            reader.Close();
                        }
                        DB.GetInstance.CloseConnection();
                    }
                }
                catch (Exception)
                {
                    DB.GetInstance.CloseConnection();
                    lbL_last_act.Text = "Пустой";
                }
            }
        }

        #endregion

        #region получение данных о бриагде ФИО Начальника и Инженера, Доверенность, № печати, Дорога

        //private readonly cheakUser _user;
        internal static void GettingTeamData(Label lbL_FIO_chief, Label lbL_FIO_Engineer, Label lbL_doverennost, Label lbL_road, Label lbL_numberPrintDocument, cheakUser _user, ComboBox cmB_road)
        {
            if (_user.Login == "Admin" || _user.IsAdmin == "Руководитель")
            {
                cmB_road.Text = cmB_road.Items[0].ToString();
            }
            else
            {
                string queryString = $"SELECT id, section_foreman_FIO, engineers_FIO, attorney, road, numberPrintDocument, " +
                    $"curator, departmentCommunications FROM сharacteristics_вrigade WHERE section_foreman_FIO = '{_user.Login}' " +
                    $"OR engineers_FIO = '{_user.Login}' OR curator = '{_user.Login}' OR departmentCommunications = '{_user.Login}'";

                if (Internet_check.CheackSkyNET())
                {
                    using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lbL_FIO_chief.Text = reader[1].ToString();
                                lbL_FIO_Engineer.Text = reader[2].ToString();
                                lbL_doverennost.Text = reader[3].ToString();
                                lbL_road.Text = reader[4].ToString();
                                lbL_numberPrintDocument.Text = reader[5].ToString();
                            }
                            reader.Close();
                        }
                    }
                }

                string querystring = $"SELECT id, road FROM сharacteristics_вrigade WHERE section_foreman_FIO = '{_user.Login}' " +
                    $"OR engineers_FIO = '{_user.Login}' OR curator = '{_user.Login}' OR departmentCommunications = '{_user.Login}'";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable table = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            cmB_road.DataSource = table;
                            cmB_road.ValueMember = "id";
                            cmB_road.DisplayMember = "road";
                        }
                        else
                        {
                            MessageBox.Show($"Системная ошибка добавления дороги в Control ComboBox ({_user.Login})");
                            System.Environment.Exit(0);
                        }
                    }
                }

            }
        }

        #endregion

        #region получение Даты регистрации входа в программу для табеля

        public static DateTime CheacDateTimeInput_logUserDB(string user)
        {
            if (Internet_check.CheackSkyNET())
            {
                DateTime Date = DateTime.Now;
                string querystring = $"SELECT dateTimeInput FROM logUserDB WHERE user = '{user}' AND dateTimeInput LIKE '%{Date.ToString("yyyy-MM-dd")}%'";

                MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());

                MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                DataTable table = new DataTable();

                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    return Convert.ToDateTime(table.Rows[table.Rows.Count - 1].ItemArray[0]);
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            return DateTime.MinValue;
        }

        #endregion

        #region Получение моделей радиостанций 

        internal static void GettingModelRST_CMB(ComboBox cmB_model)
        {
            string querystring = $"SELECT id, model_radiostation_name FROM model_radiostation";
            using (MySqlCommand command2 = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable table = new DataTable();

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command2))
                {
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        cmB_model.DataSource = table;
                        cmB_model.ValueMember = "id";
                        cmB_model.DisplayMember = "model_radiostation_name";
                    }
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        #endregion
    }
}
