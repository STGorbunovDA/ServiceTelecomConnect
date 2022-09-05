using Microsoft.Win32;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using WinForms = System.Windows.Forms;



namespace ServiceTelecomConnect
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

    public partial class ST_WorkForm : Form
    {
        #region global perem

        //DB dB = new DB();//для интерфейса
        //DB_2 dB_2 = new DB_2();// для загрузки файлов
        //DB_3 dB_3 = new DB_3();// для копирования каждые 30 минут таблицы
        //DB_4 dB_4 = new DB_4();// для формирования отчётов excel

        private delegate DialogResult ShowOpenFileDialogInvoker();

        /// <summary>
        /// переменная для индекса dataGridView1 
        /// </summary>
        int selectedRow;

        private readonly cheakUser _user;

        #endregion

        public ST_WorkForm(cheakUser user)
        {
            try
            {
                InitializeComponent();

                StartPosition = FormStartPosition.CenterScreen;
                comboBox_seach.Text = comboBox_seach.Items[6].ToString();

                dataGridView1.DoubleBuffered(true);
                this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
                this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                _user = user;
                IsAdmin();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Проверка пользователя кто постучался
        /// </summary>
        void IsAdmin()
        {
            if (_user.IsAdmin == "Дирекция связи" || _user.IsAdmin == "Инженер")
            {
                //panel1.Enabled = false;
                panel3.Enabled = false;
                Functional_loading_panel.Enabled = false;
                panel_date.Enabled = false;
                panel_remont_information_company.Enabled = false;

                foreach (Control element in panel1.Controls)
                {
                    element.Enabled = false;
                }

                button_form_act.Enabled = true;
                comboBox_city.Enabled = true;
                button_seach_BD_city.Enabled = true;
                button_add_city.Enabled = true;
                button_all_BD.Enabled = true;
                pictureBox2_update.Enabled = true;
                comboBox_seach.Enabled = true;
                textBox_search.Enabled = true;
                button_search.Enabled = true;
            }
        }

        #region заполнение datagrid
        /// <summary>
        /// заполняем dataGridView1 колонки
        /// </summary>
        void CreateColums()
        {
            try
            {
                dataGridView1.Columns.Add("id", "№");
                dataGridView1.Columns.Add("poligon", "Полигон");
                dataGridView1.Columns.Add("company", "Предприятие");
                dataGridView1.Columns.Add("location", "Место нахождения");
                dataGridView1.Columns.Add("model", "Модель радиостанции");
                dataGridView1.Columns.Add("serialNumber", "Заводской номер");
                dataGridView1.Columns.Add("inventoryNumber", "Инвентарный номер");
                dataGridView1.Columns.Add("networkNumber", "Сетевой номер");
                dataGridView1.Columns.Add("dateTO", "Дата ТО");
                dataGridView1.Columns.Add("numberAct", "№ акта ТО");
                dataGridView1.Columns.Add("city", "Город");
                dataGridView1.Columns.Add("price", "Цена ТО");
                dataGridView1.Columns.Add("representative", "Представитель предприятия");
                dataGridView1.Columns.Add("post", "Должность");
                dataGridView1.Columns.Add("numberIdentification", "Номер удостоверения");
                dataGridView1.Columns.Add("dateIssue", "Дата выдачи удостоверения");
                dataGridView1.Columns.Add("phoneNumber", "Номер телефона");
                dataGridView1.Columns.Add("numberActRemont", "№ акта ремонта");
                dataGridView1.Columns.Add("category", "Категория");
                dataGridView1.Columns.Add("priceRemont", "Цена ремонта");
                dataGridView1.Columns.Add("antenna", "Антенна");
                dataGridView1.Columns.Add("manipulator", "Манипулятор");
                dataGridView1.Columns.Add("AKB", "АКБ");
                dataGridView1.Columns.Add("batteryСharger", "ЗУ");
                dataGridView1.Columns.Add("completed_works_1", "Выполненные работы_1");
                dataGridView1.Columns.Add("completed_works_2", "Выполненные работы_1");
                dataGridView1.Columns.Add("completed_works_3", "Выполненные работы_1");
                dataGridView1.Columns.Add("completed_works_4", "Выполненные работы_1");
                dataGridView1.Columns.Add("completed_works_5", "Выполненные работы_1");
                dataGridView1.Columns.Add("completed_works_6", "Выполненные работы_1");
                dataGridView1.Columns.Add("completed_works_7", "Выполненные работы_1");
                dataGridView1.Columns.Add("parts_1", "Израсходованные материалы и детали_1");
                dataGridView1.Columns.Add("parts_2", "Израсходованные материалы и детали_2");
                dataGridView1.Columns.Add("parts_3", "Израсходованные материалы и детали_3");
                dataGridView1.Columns.Add("parts_4", "Израсходованные материалы и детали_4");
                dataGridView1.Columns.Add("parts_5", "Израсходованные материалы и детали_5");
                dataGridView1.Columns.Add("parts_6", "Израсходованные материалы и детали_6");
                dataGridView1.Columns.Add("parts_7", "Израсходованные материалы и детали_7");
                dataGridView1.Columns.Add("decommissionSerialNumber", "№ акта списания");
                dataGridView1.Columns.Add("comment", "Примечание");
                dataGridView1.Columns.Add("IsNew", String.Empty);
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;
                dataGridView1.Columns[14].Visible = false;
                dataGridView1.Columns[15].Visible = false;
                dataGridView1.Columns[16].Visible = false;
                dataGridView1.Columns[20].Visible = false;
                dataGridView1.Columns[21].Visible = false;
                dataGridView1.Columns[22].Visible = false;
                dataGridView1.Columns[23].Visible = false;
                dataGridView1.Columns[24].Visible = false;
                dataGridView1.Columns[25].Visible = false;
                dataGridView1.Columns[26].Visible = false;
                dataGridView1.Columns[27].Visible = false;
                dataGridView1.Columns[28].Visible = false;
                dataGridView1.Columns[29].Visible = false;
                dataGridView1.Columns[30].Visible = false;
                dataGridView1.Columns[31].Visible = false;
                dataGridView1.Columns[32].Visible = false;
                dataGridView1.Columns[33].Visible = false;
                dataGridView1.Columns[34].Visible = false;
                dataGridView1.Columns[35].Visible = false;
                dataGridView1.Columns[36].Visible = false;
                dataGridView1.Columns[37].Visible = false;
                dataGridView1.Columns[40].Visible = false;
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
        void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            try
            {
                dataGridView1.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4),
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
        void RefreshDataGrid(DataGridView dgw)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (comboBox_city.Text != "")
                    {
                        var myCulture = new CultureInfo("ru-RU");
                        myCulture.NumberFormat.NumberDecimalSeparator = ".";
                        Thread.CurrentThread.CurrentCulture = myCulture;
                        dgw.Rows.Clear();
                        string queryString = $"SELECT * FROM radiostantion WHERE city LIKE N'%{comboBox_city.Text.Trim()}%'";

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

                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                    dataGridView1.Columns[0].Width = 45;
                    dataGridView1.Columns[3].Width = 170;
                    dataGridView1.Columns[4].Width = 180;
                    dataGridView1.Columns[5].Width = 150;
                    dataGridView1.Columns[6].Width = 178;
                    dataGridView1.Columns[7].Width = 178;
                    dataGridView1.Columns[8].Width = 100;
                    dataGridView1.Columns[9].Width = 110;
                    dataGridView1.Columns[10].Width = 100;
                    dataGridView1.Columns[11].Width = 100;
                    dataGridView1.Columns[17].Width = 120;
                    dataGridView1.Columns[39].Width = 300;

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

        #region проверка реестра на наличе записи
        /// <summary>
        /// для проверки реестра на наличие записи
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public bool ValueExists(RegistryKey Key, string Value)
        {
            try
            {
                return Key.GetValue(Value) != null && Key.GetValue(Value) != "";
            }
            catch
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// при загрузке формы вызываем методы заполнения самих столбцов, подключение к базе данных и метода подчёта количества строк
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ST_WorkForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (Internet_check.AvailabilityChanged_bool())
                {
                    try
                    {
                        DB.GetInstance.openConnection();
                        string querystring = $"SELECT city FROM radiostantion GROUP BY city";
                        MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());
                        DataTable city_table = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                        adapter.Fill(city_table);

                        comboBox_city.DataSource = city_table;
                        comboBox_city.DisplayMember = "city";

                        DB.GetInstance.closeConnection();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Города не добавленны в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        DB.GetInstance.closeConnection();
                    }
                }

                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ServiceTelekom_Setting\\");

                if (reg != null)
                {
                    RegistryKey currentUserKey = Registry.CurrentUser;
                    RegistryKey helloKey = currentUserKey.OpenSubKey("SOFTWARE\\ServiceTelekom_Setting");
                    RegistryKey helloKey_record = currentUserKey.CreateSubKey("SOFTWARE\\ServiceTelekom_Setting");

                    string[] regKey = {$"Город проведения проверки", "Начальник по ТО и Р СРС", "Доверенность",
                             "Инженер по ТО и Р СРС", "Полигон РЖД full", "Номер печати"};

                    for (int i = 0; i < regKey.Length; i++)
                    {
                        bool flag = ValueExists(helloKey, regKey[i]);
                        if (flag == false)
                        {
                            helloKey_record.SetValue($"{regKey[i]}", $"");
                            Block_ST_Work_Form_control();
                            panel_date.Visible = true;
                            panel_date.Enabled = true;
                        }
                    }

                    textBox_GD_city.Text = helloKey.GetValue(regKey[0]).ToString();
                    textBox_FIO_chief.Text = helloKey.GetValue(regKey[1]).ToString();
                    textBox_doverennost.Text = helloKey.GetValue(regKey[2]).ToString();
                    textBox_FIO_Engineer.Text = helloKey.GetValue(regKey[3]).ToString();
                    textBox_polinon_full.Text = helloKey.GetValue(regKey[4]).ToString();
                    textBox_number_printing_doc_datePanel.Text = helloKey.GetValue(regKey[5]).ToString();

                    comboBox_city.Text = helloKey.GetValue(regKey[0]).ToString();
                    label_FIO_chief.Text = helloKey.GetValue(regKey[1]).ToString();
                    label_doverennost.Text = helloKey.GetValue(regKey[2]).ToString();
                    label_FIO_Engineer.Text = helloKey.GetValue(regKey[3]).ToString();
                    label_polinon_full.Text = helloKey.GetValue(regKey[4]).ToString();

                    TextBox[] textBoxes = { textBox_GD_city, textBox_FIO_chief, textBox_doverennost, textBox_FIO_Engineer,
                                            textBox_polinon_full, textBox_number_printing_doc_datePanel};
                    foreach (TextBox textBox in textBoxes)
                    {
                        if (textBox.Text == "")
                        {
                            this.ActiveControl = textBox;
                            //MessageBox.Show($"Заполни поле {textBox.ToString()}");
                        }
                    }

                    helloKey.Close();
                }
                else
                {
                    if (_user.IsAdmin == "Дирекция связи" || _user.IsAdmin == "Инженер")
                    {
                        label_doverennost.Text = "Доверенность";
                        label_FIO_chief.Text = "Начальник";
                        label_FIO_Engineer.Text = "Инженер";
                        label_FIO_Engineer.Text = "Инженер";
                        label_polinon_full.Text = "Полигон";
                        textBox_number_printing_doc_datePanel.Text = "Печать";
                    }
                    else
                    {
                        Block_ST_Work_Form_control();
                        panel_date.Visible = true;
                        panel_date.Enabled = true;
                    }

                }
                CreateColums();
                RefreshDataGrid(dataGridView1);
                UpdateCountRST();
                UpdateSumTOrst();
                UpdateSumTOrstRemont();
                UpdateCountRSTRemont();

                this.dataGridView1.Sort(this.dataGridView1.Columns["dateTO"], ListSortDirection.Ascending);

                dataGridView1.Columns["dateTO"].ValueType = typeof(DateTime);
                dataGridView1.Columns["dateTO"].DefaultCellStyle.Format = "dd.MM.yyyy";
                dataGridView1.Columns["dateTO"].ValueType = System.Type.GetType("System.Date");

                ///Таймер
                WinForms::Timer timer = new WinForms::Timer();
                timer.Interval = (15 * 60 * 1000); // 15 mins
                timer.Tick += new EventHandler(TimerEventProcessor);
                timer.Start();

                /// получение актов который не заполенны из реестра, которые указал пользователь
                RegistryKey reg2 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                if (reg2 != null)
                {
                    RegistryKey currentUserKey = Registry.CurrentUser;
                    RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                    lbl_full_complete_act.Text = helloKey.GetValue("Акты_незаполненные").ToString();
                    if (lbl_full_complete_act.Text != "")
                    {
                        label_complete.Visible = true;
                        lbl_full_complete_act.Visible = true;
                    }
                    helloKey.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); ;
                MessageBox.Show("Ошибка считывания реестра!");
            }
        }

        void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            Get_date_save_datagridview();

            if (Internet_check.AvailabilityChanged_bool() == true)
            {
                new Thread(() => { Copy_BD_radiostantion_in_radiostantion_copy(); }) { IsBackground = true }.Start();
            }
        }

        #region для счётчика резервное копирование радиостанций из текущей radiostantion в radiostantion_copy
        void Copy_BD_radiostantion_in_radiostantion_copy()
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

        #region загрузка всей таблицы ТО в текущем году
        void Button_all_BD_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dataGridView1.Rows.Clear();
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
                                    ReedSingleRow(dataGridView1, reader);
                                }
                                reader.Close();
                            }
                        }
                        command.ExecuteNonQuery();
                        DB.GetInstance.closeConnection();
                    }


                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                    dataGridView1.Columns[0].Width = 45;
                    dataGridView1.Columns[3].Width = 170;
                    dataGridView1.Columns[4].Width = 180;
                    dataGridView1.Columns[5].Width = 150;
                    dataGridView1.Columns[6].Width = 178;
                    dataGridView1.Columns[7].Width = 178;
                    dataGridView1.Columns[8].Width = 100;
                    dataGridView1.Columns[9].Width = 110;
                    dataGridView1.Columns[10].Width = 100;
                    dataGridView1.Columns[11].Width = 100;
                    dataGridView1.Columns[17].Width = 120;
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
            UpdateCountRST();
            UpdateSumTOrst();
            UpdateSumTOrstRemont();
            UpdateCountRSTRemont();
            txb_flag_all_BD.Text = "Вся БД";
        }

        #endregion

        #region panel date information
        /// <summary>
        /// Запись в реестр инфор. о бригаде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_record_date_Click(object sender, EventArgs e)
        {
            try
            {
                #region проверка на пустые поля
                if (textBox_doverennost.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле доверенность!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_FIO_chief.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле ФИО Начальника!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_FIO_Engineer.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле ФИО Инженера!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_GD_city.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле Город проведения проверки!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_polinon_full.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле Полигон!(нужен для печати акта)";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_number_printing_doc_datePanel.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле № печати!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                #endregion

                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.CreateSubKey("SOFTWARE\\ServiceTelekom_Setting");
                helloKey.SetValue("Доверенность", $"{textBox_doverennost.Text}");
                helloKey.SetValue("Начальник по ТО и Р СРС", $"{textBox_FIO_chief.Text}");
                helloKey.SetValue("Инженер по ТО и Р СРС", $"{textBox_FIO_Engineer.Text}");
                helloKey.SetValue("Город проведения проверки", $"{textBox_GD_city.Text}");
                helloKey.SetValue("Полигон РЖД full", $"{textBox_polinon_full.Text}");
                helloKey.SetValue("Номер печати", $"{textBox_number_printing_doc_datePanel.Text}");
                helloKey.Close();

                panel_date.Visible = false;
                panel_date.Enabled = false;

                dataGridView1.Enabled = true;
                panel1.Enabled = true;
                panel3.Enabled = true;

                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// для блокирования Контролов
        /// </summary>
        void Block_ST_Work_Form_control()
        {
            dataGridView1.Enabled = false;
            panel1.Enabled = false;
            panel3.Enabled = false;
        }

        /// <summary>
        /// Закрыть панель инормации о бригаде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_close_panel_date_info_Click(object sender, EventArgs e)
        {
            try
            {
                #region проверка на пустые поля

                if (textBox_GD_city.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле Город проведения проверки!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_FIO_chief.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле ФИО Начальника!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_doverennost.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле № и Дату доверенности!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_FIO_Engineer.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле ФИО Инженера!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_polinon_full.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле Полигон!(нужен для печати акта)";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_number_printing_doc_datePanel.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле № печати!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            panel_date.Enabled = false;
            panel_date.Visible = false;
            dataGridView1.Enabled = true;
            panel1.Enabled = true;
            panel3.Enabled = true;
        }
        #endregion

        #region Сохранение поля город проведения проверки
        void Button_add_city_Click(object sender, EventArgs e)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.CreateSubKey("SOFTWARE\\ServiceTelekom_Setting");
            helloKey.SetValue("Город проведения проверки", $"{comboBox_city.Text}");
            helloKey.Close();
        }
        #endregion

        #region получение данных в Control-ы, button right mouse

        /// <summary>
        /// метод получения данных в Control из dataGridView1 при нажатии на клавишу мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.ReadOnly = false;
                selectedRow = e.RowIndex;

                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[selectedRow];
                    textBox_id.Text = row.Cells[0].Value.ToString();
                    comboBox_poligon.Text = row.Cells[1].Value.ToString();
                    textBox_company.Text = row.Cells[2].Value.ToString();
                    textBox_location.Text = row.Cells[3].Value.ToString();
                    comboBox_model.Text = row.Cells[4].Value.ToString();
                    textBox_serialNumber.Text = row.Cells[5].Value.ToString();
                    textBox_inventoryNumber.Text = row.Cells[6].Value.ToString();
                    textBox_networkNumber.Text = row.Cells[7].Value.ToString();
                    textBox_dateTO.Text = row.Cells[8].Value.ToString();
                    textBox_numberAct.Text = row.Cells[9].Value.ToString();
                    textBox_city.Text = row.Cells[10].Value.ToString();
                    textBox_price.Text = row.Cells[11].Value.ToString();
                    textBox_representative.Text = row.Cells[12].Value.ToString();
                    textBox_post.Text = row.Cells[13].Value.ToString();
                    textBox_numberIdentification.Text = row.Cells[14].Value.ToString();
                    textBox_dateIssue.Text = row.Cells[15].Value.ToString();
                    textBox_phoneNumber.Text = row.Cells[16].Value.ToString();
                    textBox_numberActRemont.Text = row.Cells[17].Value.ToString();
                    comboBox_сategory.Text = row.Cells[18].Value.ToString();
                    textBox_priceRemont.Text = row.Cells[19].Value.ToString();
                    textBox_antenna.Text = row.Cells[20].Value.ToString();
                    textBox_manipulator.Text = row.Cells[21].Value.ToString();
                    textBox_AKB.Text = row.Cells[22].Value.ToString();
                    textBox_batteryСharger.Text = row.Cells[23].Value.ToString();
                    textBox_сompleted_works_1.Text = row.Cells[24].Value.ToString();
                    textBox_сompleted_works_2.Text = row.Cells[25].Value.ToString();
                    textBox_сompleted_works_3.Text = row.Cells[26].Value.ToString();
                    textBox_сompleted_works_4.Text = row.Cells[27].Value.ToString();
                    textBox_сompleted_works_5.Text = row.Cells[28].Value.ToString();
                    textBox_сompleted_works_6.Text = row.Cells[29].Value.ToString();
                    textBox_сompleted_works_7.Text = row.Cells[30].Value.ToString();
                    textBox_parts_1.Text = row.Cells[31].Value.ToString();
                    textBox_parts_2.Text = row.Cells[32].Value.ToString();
                    textBox_parts_3.Text = row.Cells[33].Value.ToString();
                    textBox_parts_4.Text = row.Cells[34].Value.ToString();
                    textBox_parts_5.Text = row.Cells[35].Value.ToString();
                    textBox_parts_6.Text = row.Cells[36].Value.ToString();
                    textBox_parts_7.Text = row.Cells[37].Value.ToString();
                    txB_decommissionSerialNumber.Text = row.Cells[38].Value.ToString();
                    txB_comment.Text = row.Cells[39].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region поиск по БД
        /// <summary>
        /// метод поиска по базе данных, подключение к базе, выполнение запроса так-же внутри  вызываем метод ReedSingleRow для вывода данных из базы
        /// </summary>
        /// <param name="dgw"></param>
        async void Search(DataGridView dgw)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    string perem_comboBox = "numberAct";

                    dgw.Rows.Clear();

                    if (comboBox_seach.Text == "Полигон")
                    {
                        perem_comboBox = "poligon";
                    }
                    else if (comboBox_seach.Text == "Предприятие")
                    {
                        perem_comboBox = "company";
                    }
                    else if (comboBox_seach.Text == "Станция")
                    {
                        perem_comboBox = "location";
                    }
                    else if (comboBox_seach.Text == "Модель")
                    {
                        perem_comboBox = "model";
                    }
                    else if (comboBox_seach.Text == "Заводской номер")
                    {
                        perem_comboBox = "serialNumber";
                    }
                    else if (comboBox_seach.Text == "Дата ТО")
                    {
                        perem_comboBox = "dateTO";
                    }
                    else if (comboBox_seach.Text == "Номер акта ТО")
                    {
                        perem_comboBox = "numberAct";
                    }
                    else if (comboBox_seach.Text == "Номер акта Ремонта")
                    {
                        perem_comboBox = "numberActRemont";
                    }
                    else if (comboBox_seach.Text == "Представитель ПП")
                    {
                        perem_comboBox = "representative";
                    }
                    else if (comboBox_seach.Text == "Номер Акта списания")
                    {
                        perem_comboBox = "decommissionSerialNumber";
                    }

                    string searchString = $"SELECT * FROM radiostantion WHERE city = '{comboBox_city.Text}' AND CONCAT ({perem_comboBox}) LIKE '%" + textBox_search.Text + "%'";

                    using (MySqlCommand command = new MySqlCommand(searchString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.openConnection();

                        IAsyncResult res = command.BeginExecuteReader();

                        using (MySqlDataReader reader = command.EndExecuteReader(res))
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
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

        #region Clear contorl-ы
        /// <summary>
        /// Очищаем Control-ы
        /// </summary>
        void ClearFields()
        {
            try
            {
                foreach (Control control in panel1.Controls)
                {
                    if (control is TextBox)
                    {
                        control.Text = "";
                    }
                }
                foreach (Control control in panel2.Controls)
                {
                    if (control is TextBox)
                    {
                        control.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// при нажатии на картинку очистить, вызываем метод очистки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pictureBox1_clear_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите очистить все введенные вами поля?";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            ClearFields();
        }
        #endregion

        #region Счётчики
        /// <summary>
        /// метод подсчёта кол-ва строк в базе
        /// </summary>
        void UpdateCountRST()
        {
            try
            {
                int numRows = dataGridView1.Rows.Count;

                label_count.Text = numRows.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        /// <summary>
        /// подсчёт кол-во ремонтов в БД
        /// </summary>
        void UpdateCountRSTRemont()
        {
            try
            {
                int sum = 0;

                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    if ((Boolean)(dataGridView1.Rows[i].Cells["category"].Value.ToString() != ""))
                    {
                        sum++;
                    }
                }
                label_count_remont.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        /// <summary>
        /// подсчёт суммый ТО
        /// </summary>
        void UpdateSumTOrst()
        {
            try
            {
                decimal sum = 0;

                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells["price"].Value);
                }
                label_summ.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        /// <summary>
        /// подсчёт суммы ремонтов
        /// </summary>
        void UpdateSumTOrstRemont()
        {
            try
            {
                decimal sum = 0;

                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells["priceRemont"].Value);
                }
                label_summ_remont.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
        #endregion

        #region Удаление из БД
        /// <summary>
        ///  метод удаления РСТ из БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_delete_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    string Mesage;
                    Mesage = "Вы действительно хотите удалить выделенную запись";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                    DeleteRowСell();

                    int currRowIndex = dataGridView1.CurrentCell.RowIndex;

                    RefreshDataGrid(dataGridView1);
                    textBox_numberAct.Text = "";

                    dataGridView1.ClearSelection();

                    if (dataGridView1.RowCount - currRowIndex > 0)
                    {
                        dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
                    }
                    UpdateCountRST();
                    UpdateSumTOrst();
                    UpdateSumTOrstRemont();
                    UpdateCountRSTRemont();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        /// <summary>
        /// метод удаления значения из базы данных
        /// </summary>
        void DeleteRowСell()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows[row.Index].Cells[40].Value = RowState.Deleted;
                }

                DB.GetInstance.openConnection();

                for (int index = 0; index < dataGridView1.Rows.Count; index++)
                {
                    var rowState = (RowState)dataGridView1.Rows[index].Cells[40].Value;//проверить индекс

                    if (rowState == RowState.Deleted)
                    {
                        var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                        var deleteQuery = $"DELETE FROM radiostantion WHERE id = {id}";

                        using (MySqlCommand command = new MySqlCommand(deleteQuery, DB.GetInstance.GetConnection()))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                DB.GetInstance.closeConnection();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region обновление БД
        /// <summary>
        /// метод обновления базы данных 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_update_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (dataGridView1.Rows.Count > 0)
                    {
                        int currRowIndex = dataGridView1.CurrentCell.RowIndex;
                        int index = dataGridView1.CurrentRow.Index;
                        RefreshDataGrid(dataGridView1);
                        UpdateCountRST();
                        UpdateSumTOrst();
                        UpdateSumTOrstRemont();
                        UpdateCountRSTRemont();
                        dataGridView1.ClearSelection();

                        if (currRowIndex >= 0)
                        {
                            dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];

                            dataGridView1.FirstDisplayedScrollingRowIndex = index;
                        }
                    }
                    else if (dataGridView1.Rows.Count == 0)
                    {
                        RefreshDataGrid(dataGridView1);
                        UpdateCountRST();
                        UpdateSumTOrst();
                        UpdateSumTOrstRemont();
                        UpdateCountRSTRemont();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        /// <summary>
        /// при нажатии на картинку обновить вызываем метод подключения к базе данных RefreshDataGridб обновляем кол-во строк и очищаем поля методом ClearFields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pictureBox2_update_Click(object sender, EventArgs e)
        {
            Button_update_Click(sender, e);
        }

        #endregion

        #region Форма добавления РСТ
        /// <summary>
        ///  Вызываем  форму создания новой записи(радиостанции)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_new_add_rst_form_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    AddRSTForm addRSTForm = new AddRSTForm();
                    addRSTForm.DoubleBufferedForm(true);
                    addRSTForm.textBox_numberAct.Text = textBox_number_printing_doc_datePanel.Text + "/";
                    if (textBox_city.Text == "")
                    {
                        addRSTForm.textBox_city.Text = comboBox_city.Text;
                    }
                    else addRSTForm.textBox_city.Text = textBox_city.Text;
                    addRSTForm.comboBox_poligon.Text = comboBox_poligon.Text;
                    addRSTForm.textBox_company.Text = textBox_company.Text;
                    addRSTForm.textBox_location.Text = textBox_location.Text;
                    addRSTForm.comboBox_model.Text = comboBox_model.Text;
                    addRSTForm.comboBox_model.Text = comboBox_model.Text;
                    addRSTForm.textBox_representative.Text = textBox_representative.Text;
                    addRSTForm.textBox_numberIdentification.Text = textBox_numberIdentification.Text;
                    addRSTForm.textBox_phoneNumber.Text = textBox_phoneNumber.Text;
                    addRSTForm.textBox_post.Text = textBox_post.Text;
                    addRSTForm.textBox_dateIssue.Text = textBox_dateIssue.Text;

                    addRSTForm.ShowDialog();
                    //обновляем datagrid по городу всё
                    RefreshDataGrid(dataGridView1);

                    if (dataGridView1.RowCount == 0)
                    {
                        //dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                    }
                    else
                    {
                        dataGridView1.ClearSelection();
                        this.dataGridView1.Sort(this.dataGridView1.Columns["dateTO"], ListSortDirection.Ascending);
                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
                        DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                        textBox_numberAct.Text = row.Cells[9].Value.ToString();
                    }
                    // обновляем по акту
                    Update_datagridview_number_act(dataGridView1);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }
        #endregion

        #region проверка ввода текст боксов

        /// <summary>
        /// для копирования Ctrl + c, Ctrl + v
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void processKbdCtrlShortcuts(object sender, KeyEventArgs e)
        {
            try
            {
                TextBox t = (TextBox)sender;
                if (e.KeyData == (Keys.C | Keys.Control))
                {
                    t.Copy();
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.X | Keys.Control))
                {
                    t.Cut();
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.V | Keys.Control))
                {
                    t.Paste();
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.A | Keys.Control))
                {
                    t.SelectAll();
                    e.Handled = true;
                }
                else if (e.KeyData == (Keys.Z | Keys.Control))
                {
                    t.Undo();
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void textBox_GD_city_Click(object sender, EventArgs e)
        {
            textBox_company.MaxLength = 25;
        }
        void textBox_GD_city_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }
        void textBox_GD_city_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }
        void textBox_FIO_chief_Click(object sender, EventArgs e)
        {
            textBox_company.MaxLength = 25;
        }

        void textBox_FIO_chief_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }
        void textBox_FIO_chief_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }
        void textBox_doverennost_Click(object sender, EventArgs e)
        {
            textBox_company.MaxLength = 25;
        }
        void textBox_doverennost_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }
        void textBox_doverennost_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ' && ch != '/')
            {
                e.Handled = true;
            }
        }
        void textBox_FIO_Engineer_Click(object sender, EventArgs e)
        {
            textBox_company.MaxLength = 25;
        }
        void textBox_FIO_Engineer_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }
        void textBox_FIO_Engineer_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }
        #endregion

        #region АКТ => excel

        void Button_form_act_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                Update_datagridview_number_act(dataGridView1);
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
                DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                textBox_id.Text = row.Cells[0].Value.ToString();
                comboBox_poligon.Text = row.Cells[1].Value.ToString();
                textBox_company.Text = row.Cells[2].Value.ToString();
                textBox_location.Text = row.Cells[3].Value.ToString();
                comboBox_model.Text = row.Cells[4].Value.ToString();
                textBox_serialNumber.Text = row.Cells[5].Value.ToString();
                textBox_inventoryNumber.Text = row.Cells[6].Value.ToString();
                textBox_networkNumber.Text = row.Cells[7].Value.ToString();
                textBox_dateTO.Text = row.Cells[8].Value.ToString();
                textBox_numberAct.Text = row.Cells[9].Value.ToString();
                textBox_city.Text = row.Cells[10].Value.ToString();
                textBox_price.Text = row.Cells[11].Value.ToString();
                textBox_representative.Text = row.Cells[12].Value.ToString();
                textBox_post.Text = row.Cells[13].Value.ToString();
                textBox_numberIdentification.Text = row.Cells[14].Value.ToString();
                textBox_dateIssue.Text = row.Cells[15].Value.ToString();
                textBox_phoneNumber.Text = row.Cells[16].Value.ToString();
                textBox_numberActRemont.Text = row.Cells[17].Value.ToString();
                comboBox_сategory.Text = row.Cells[18].Value.ToString();
                textBox_priceRemont.Text = row.Cells[19].Value.ToString();
                textBox_antenna.Text = row.Cells[20].Value.ToString();
                textBox_manipulator.Text = row.Cells[21].Value.ToString();
                textBox_AKB.Text = row.Cells[22].Value.ToString();
                textBox_batteryСharger.Text = row.Cells[23].Value.ToString();
                textBox_сompleted_works_1.Text = row.Cells[24].Value.ToString();
                textBox_сompleted_works_2.Text = row.Cells[25].Value.ToString();
                textBox_сompleted_works_3.Text = row.Cells[26].Value.ToString();
                textBox_сompleted_works_4.Text = row.Cells[27].Value.ToString();
                textBox_сompleted_works_5.Text = row.Cells[28].Value.ToString();
                textBox_сompleted_works_6.Text = row.Cells[29].Value.ToString();
                textBox_сompleted_works_7.Text = row.Cells[30].Value.ToString();
                textBox_parts_1.Text = row.Cells[31].Value.ToString();
                textBox_parts_2.Text = row.Cells[32].Value.ToString();
                textBox_parts_3.Text = row.Cells[33].Value.ToString();
                textBox_parts_4.Text = row.Cells[34].Value.ToString();
                textBox_parts_5.Text = row.Cells[35].Value.ToString();
                textBox_parts_6.Text = row.Cells[36].Value.ToString();
                textBox_parts_7.Text = row.Cells[37].Value.ToString();
                txB_decommissionSerialNumber.Text = row.Cells[38].Value.ToString();
                txB_comment.Text = row.Cells[39].Value.ToString();
                ExportToExcelAct();
                RefreshDataGrid(dataGridView1);
            }
        }
        /// <summary>
        /// АКТ ТО
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ExportToExcelAct()
        {
            Excel.Application exApp = new Excel.Application();

            try
            {
                if (textBox_numberAct.Text != "")
                {
                    if (dataGridView1.Rows.Count > 21)
                    {
                        string Mesage2;

                        Mesage2 = "В акте может быть только 20-ать радиостанций! Нажмите \"Enter\" или \"два раза мышью\" на панель в графе \"Акт №:\"";

                        if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        Type officeType = Type.GetTypeFromProgID("Excel.Application");

                        if (officeType == null)
                        {
                            string Mesage2 = "У Вас не установлен Excel!";

                            if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                return;
                            }
                        }
                        else
                        {
                            exApp.SheetsInNewWorkbook = 3;
                            exApp.Workbooks.Add();
                            exApp.DisplayAlerts = false;

                            Excel.Worksheet workSheet = (Excel.Worksheet)exApp.Worksheets.get_Item(1);
                            Excel.Worksheet workSheet2 = (Excel.Worksheet)exApp.Worksheets.get_Item(2);
                            Excel.Worksheet workSheet3 = (Excel.Worksheet)exApp.Worksheets.get_Item(3);

                            workSheet.Name = $"Накладная №{textBox_numberAct.Text.Replace('/', '.')}";
                            workSheet2.Name = $"Ведомость №{textBox_numberAct.Text.Replace('/', '.')}";
                            workSheet3.Name = $"Акт №{textBox_numberAct.Text.Replace('/', '.')}";


                            #region Накладная ТО 1 Item

                            workSheet.PageSetup.Zoom = false;
                            workSheet.PageSetup.FitToPagesWide = 1;
                            workSheet.PageSetup.FitToPagesTall = 1;

                            workSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;

                            workSheet.Rows.Font.Size = 9.5;
                            workSheet.Rows.Font.Name = "Times New Roman";

                            //workSheet.PageSetup.PrintArea = "A1:N39";

                            Excel.Range _excelCells2 = (Excel.Range)workSheet.get_Range("E1", "G1").Cells;
                            Excel.Range _excelCells1 = (Excel.Range)workSheet.get_Range("M1", "O1").Cells;
                            Excel.Range _excelCells3 = (Excel.Range)workSheet.get_Range("C3", "D3").Cells;
                            Excel.Range _excelCells4 = (Excel.Range)workSheet.get_Range("K3", "L3").Cells;
                            Excel.Range _excelCells5 = (Excel.Range)workSheet.get_Range("A5", "B5").Cells;
                            Excel.Range _excelCells6 = (Excel.Range)workSheet.get_Range("C5", "D5").Cells;
                            Excel.Range _excelCells7 = (Excel.Range)workSheet.get_Range("E5", "G5").Cells;
                            Excel.Range _excelCells8 = (Excel.Range)workSheet.get_Range("I5", "J5").Cells;
                            Excel.Range _excelCells9 = (Excel.Range)workSheet.get_Range("E3").Cells;
                            Excel.Range _excelCells10 = (Excel.Range)workSheet.get_Range("M3").Cells;
                            Excel.Range _excelCells11 = (Excel.Range)workSheet.get_Range("K5", "L5").Cells;
                            Excel.Range _excelCells12 = (Excel.Range)workSheet.get_Range("M5", "O5").Cells;
                            Excel.Range _excelCells13 = (Excel.Range)workSheet.get_Range("A6", "B6").Cells;
                            Excel.Range _excelCells14 = (Excel.Range)workSheet.get_Range("C6", "D6").Cells;
                            Excel.Range _excelCells15 = (Excel.Range)workSheet.get_Range("I6", "J6").Cells;
                            Excel.Range _excelCells16 = (Excel.Range)workSheet.get_Range("K6", "L6").Cells;
                            Excel.Range _excelCells17 = (Excel.Range)workSheet.get_Range("A7", "A27").Cells;
                            Excel.Range _excelCells18 = (Excel.Range)workSheet.get_Range("B7", "C7").Cells;
                            Excel.Range _excelCells19 = (Excel.Range)workSheet.get_Range("A7", "O7").Cells;
                            Excel.Range _excelCells20 = (Excel.Range)workSheet.get_Range("I7", "I27").Cells;
                            Excel.Range _excelCells21 = (Excel.Range)workSheet.get_Range("J7", "K7").Cells;
                            Excel.Range _excelCells32 = (Excel.Range)workSheet.get_Range("A8", "O27").Cells;
                            Excel.Range _excelCells33 = (Excel.Range)workSheet.get_Range("A7", "G27").Cells;
                            Excel.Range _excelCells34 = (Excel.Range)workSheet.get_Range("I7", "O27").Cells;
                            Excel.Range _excelCells35 = (Excel.Range)workSheet.get_Range("A28", "G28").Cells;
                            Excel.Range _excelCells36 = (Excel.Range)workSheet.get_Range("I28", "O28").Cells;
                            Excel.Range _excelCells37 = (Excel.Range)workSheet.get_Range("A29", "C30").Cells;
                            Excel.Range _excelCells38 = (Excel.Range)workSheet.get_Range("A31", "C31").Cells;
                            Excel.Range _excelCells39 = (Excel.Range)workSheet.get_Range("A33", "C33").Cells;
                            Excel.Range _excelCells40 = (Excel.Range)workSheet.get_Range("B33", "C33").Cells;
                            Excel.Range _excelCells41 = (Excel.Range)workSheet.get_Range("B34", "C34").Cells;
                            Excel.Range _excelCells42 = (Excel.Range)workSheet.get_Range("A36", "C37").Cells;
                            Excel.Range _excelCells43 = (Excel.Range)workSheet.get_Range("A38", "C38").Cells;
                            Excel.Range _excelCells44 = (Excel.Range)workSheet.get_Range("A40", "C40").Cells;
                            Excel.Range _excelCells45 = (Excel.Range)workSheet.get_Range("B40", "C40").Cells;
                            Excel.Range _excelCells46 = (Excel.Range)workSheet.get_Range("B41", "C41").Cells;
                            Excel.Range _excelCells47 = (Excel.Range)workSheet.get_Range("E29", "G30").Cells;
                            Excel.Range _excelCells48 = (Excel.Range)workSheet.get_Range("E31", "G31").Cells;
                            Excel.Range _excelCells49 = (Excel.Range)workSheet.get_Range("E33", "G33").Cells;
                            Excel.Range _excelCells50 = (Excel.Range)workSheet.get_Range("F33", "G33").Cells;
                            Excel.Range _excelCells51 = (Excel.Range)workSheet.get_Range("F34", "G34").Cells;
                            Excel.Range _excelCells52 = (Excel.Range)workSheet.get_Range("E36", "G37").Cells;
                            Excel.Range _excelCells53 = (Excel.Range)workSheet.get_Range("E38", "G38").Cells;
                            Excel.Range _excelCells54 = (Excel.Range)workSheet.get_Range("E40", "G40").Cells;
                            Excel.Range _excelCells55 = (Excel.Range)workSheet.get_Range("F40", "G40").Cells;
                            Excel.Range _excelCells56 = (Excel.Range)workSheet.get_Range("F41", "G41").Cells;
                            Excel.Range _excelCells57 = (Excel.Range)workSheet.get_Range("D42", "G42").Cells;
                            Excel.Range _excelCells58 = (Excel.Range)workSheet.get_Range("D43", "G43").Cells;
                            Excel.Range _excelCells59 = (Excel.Range)workSheet.get_Range("I29", "K30").Cells;
                            Excel.Range _excelCells60 = (Excel.Range)workSheet.get_Range("I31", "K31").Cells;
                            Excel.Range _excelCells61 = (Excel.Range)workSheet.get_Range("I33", "K33").Cells;
                            Excel.Range _excelCells62 = (Excel.Range)workSheet.get_Range("J33", "K33").Cells;
                            Excel.Range _excelCells63 = (Excel.Range)workSheet.get_Range("J34", "K34").Cells;
                            Excel.Range _excelCells64 = (Excel.Range)workSheet.get_Range("I36", "K37").Cells;
                            Excel.Range _excelCells65 = (Excel.Range)workSheet.get_Range("I38", "K38").Cells;
                            Excel.Range _excelCells66 = (Excel.Range)workSheet.get_Range("I40", "K40").Cells;
                            Excel.Range _excelCells67 = (Excel.Range)workSheet.get_Range("J40", "K40").Cells;
                            Excel.Range _excelCells68 = (Excel.Range)workSheet.get_Range("J41", "K41").Cells;
                            Excel.Range _excelCells69 = (Excel.Range)workSheet.get_Range("M29", "O30").Cells;
                            Excel.Range _excelCells70 = (Excel.Range)workSheet.get_Range("M31", "O31").Cells;
                            Excel.Range _excelCells71 = (Excel.Range)workSheet.get_Range("M33", "O33").Cells;
                            Excel.Range _excelCells72 = (Excel.Range)workSheet.get_Range("N33", "O33").Cells;
                            Excel.Range _excelCells73 = (Excel.Range)workSheet.get_Range("M36", "O37").Cells;
                            Excel.Range _excelCells74 = (Excel.Range)workSheet.get_Range("M38", "O38").Cells;
                            Excel.Range _excelCells75 = (Excel.Range)workSheet.get_Range("M40", "O40").Cells;
                            Excel.Range _excelCells76 = (Excel.Range)workSheet.get_Range("N40", "O40").Cells;
                            Excel.Range _excelCells77 = (Excel.Range)workSheet.get_Range("N41", "O41").Cells;
                            Excel.Range _excelCells78 = (Excel.Range)workSheet.get_Range("L42", "O42").Cells;
                            Excel.Range _excelCells79 = (Excel.Range)workSheet.get_Range("L43", "O43").Cells;

                            _excelCells1.Merge(Type.Missing);
                            _excelCells2.Merge(Type.Missing);
                            _excelCells3.Merge(Type.Missing);
                            _excelCells4.Merge(Type.Missing);
                            _excelCells5.Merge(Type.Missing);
                            _excelCells6.Merge(Type.Missing);
                            _excelCells7.Merge(Type.Missing);
                            _excelCells8.Merge(Type.Missing);
                            _excelCells11.Merge(Type.Missing);
                            _excelCells12.Merge(Type.Missing);
                            _excelCells13.Merge(Type.Missing);
                            _excelCells14.Merge(Type.Missing);
                            _excelCells15.Merge(Type.Missing);
                            _excelCells16.Merge(Type.Missing);
                            _excelCells18.Merge(Type.Missing);
                            _excelCells21.Merge(Type.Missing);
                            _excelCells35.Merge(Type.Missing);
                            _excelCells36.Merge(Type.Missing);
                            _excelCells37.Merge(Type.Missing);
                            _excelCells38.Merge(Type.Missing);
                            _excelCells40.Merge(Type.Missing);
                            _excelCells41.Merge(Type.Missing);
                            _excelCells42.Merge(Type.Missing);
                            _excelCells43.Merge(Type.Missing);
                            _excelCells45.Merge(Type.Missing);
                            _excelCells46.Merge(Type.Missing);
                            _excelCells47.Merge(Type.Missing);
                            _excelCells48.Merge(Type.Missing);
                            _excelCells50.Merge(Type.Missing);
                            _excelCells51.Merge(Type.Missing);
                            _excelCells52.Merge(Type.Missing);
                            _excelCells53.Merge(Type.Missing);
                            _excelCells55.Merge(Type.Missing);
                            _excelCells56.Merge(Type.Missing);
                            _excelCells57.Merge(Type.Missing);
                            _excelCells58.Merge(Type.Missing);
                            _excelCells59.Merge(Type.Missing);
                            _excelCells60.Merge(Type.Missing);
                            _excelCells62.Merge(Type.Missing);
                            _excelCells63.Merge(Type.Missing);
                            _excelCells64.Merge(Type.Missing);
                            _excelCells65.Merge(Type.Missing);
                            _excelCells67.Merge(Type.Missing);
                            _excelCells68.Merge(Type.Missing);
                            _excelCells69.Merge(Type.Missing);
                            _excelCells70.Merge(Type.Missing);
                            _excelCells72.Merge(Type.Missing);
                            _excelCells73.Merge(Type.Missing);
                            _excelCells74.Merge(Type.Missing);
                            _excelCells76.Merge(Type.Missing);
                            _excelCells77.Merge(Type.Missing);
                            _excelCells78.Merge(Type.Missing);
                            _excelCells79.Merge(Type.Missing);

                            _excelCells1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells4.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells5.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells6.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells7.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells8.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells11.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells12.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells13.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells14.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells15.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells16.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells17.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells18.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells19.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells19.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells20.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells21.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells32.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells35.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells36.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells38.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells40.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells41.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells43.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells45.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells46.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells47.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells48.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells50.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells51.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells52.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells53.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells55.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells56.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells57.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells58.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells59.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells60.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells62.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells63.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells64.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells65.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells67.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells68.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells69.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells70.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells72.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells73.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells74.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells76.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells77.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells78.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells79.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells1.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells2.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells9.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells10.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells14.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells16.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells37.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells39.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells42.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells44.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells47.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells49.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells52.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells54.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells59.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells61.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells64.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells66.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells69.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells71.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells73.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells75.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells33.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells33.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells34.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells34.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells34.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells34.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells34.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells34.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                            Excel.Range rowColum = workSheet.get_Range("A7", "A27");
                            rowColum.EntireColumn.ColumnWidth = 5; //

                            Excel.Range rowHeight = workSheet.get_Range("A7", "O7");
                            rowHeight.EntireRow.RowHeight = 25; //   

                            Excel.Range rowColum2 = workSheet.get_Range("I7", "I27");
                            rowColum2.EntireColumn.ColumnWidth = 5; //

                            Excel.Range rowColum3 = workSheet.get_Range("D7", "D27");
                            rowColum3.EntireColumn.ColumnWidth = 15; //

                            Excel.Range rowColum4 = workSheet.get_Range("L7", "L27");
                            rowColum4.EntireColumn.ColumnWidth = 15; //

                            Excel.Range rowColum5 = workSheet.get_Range("B7", "C7");
                            rowColum5.EntireColumn.ColumnWidth = 10; //

                            Excel.Range rowColum6 = workSheet.get_Range("J7", "K7");
                            rowColum6.EntireColumn.ColumnWidth = 10; //

                            Excel.Range rowHeight2 = workSheet.get_Range("A31", "O31");
                            rowHeight2.EntireRow.RowHeight = 10; //

                            Excel.Range rowHeight3 = workSheet.get_Range("A34", "O34");
                            rowHeight3.EntireRow.RowHeight = 10; //

                            Excel.Range rowHeight4 = workSheet.get_Range("A38", "O38");
                            rowHeight4.EntireRow.RowHeight = 10; //

                            Excel.Range rowHeight5 = workSheet.get_Range("A41", "O41");
                            rowHeight5.EntireRow.RowHeight = 10; //

                            Excel.Range range_Consolidated = workSheet.Rows.get_Range("E1", "O1");
                            Excel.Range range_Consolidated2 = workSheet.Rows.get_Range("C3", "M3");
                            Excel.Range range_Consolidated3 = workSheet.Rows.get_Range("A7", "O7");
                            Excel.Range range_Consolidated4 = workSheet.Rows.get_Range("A8", "O27");
                            Excel.Range range_Consolidated5 = workSheet.Rows.get_Range("A28", "O28");
                            Excel.Range range_Consolidated6 = workSheet.Rows.get_Range("A31", "O31");
                            Excel.Range range_Consolidated7 = workSheet.Rows.get_Range("A34", "O34");
                            Excel.Range range_Consolidated8 = workSheet.Rows.get_Range("A38", "O38");
                            Excel.Range range_Consolidated9 = workSheet.Rows.get_Range("A41", "O41");
                            Excel.Range range_Consolidated10 = workSheet.Rows.get_Range("A42", "O43");
                            Excel.Range range_Consolidated11 = workSheet.Rows.get_Range("B8", "C27");
                            Excel.Range range_Consolidated12 = workSheet.Rows.get_Range("J8", "K27");

                            range_Consolidated.Font.Bold = true;
                            range_Consolidated.Font.Size = 10;
                            range_Consolidated2.Font.Bold = true;
                            range_Consolidated2.Font.Size = 12;
                            range_Consolidated3.Font.Bold = true;
                            range_Consolidated4.Font.Size = 8;
                            range_Consolidated5.Font.Size = 7;
                            range_Consolidated6.Font.Size = 7;
                            range_Consolidated7.Font.Size = 7;
                            range_Consolidated8.Font.Size = 7;
                            range_Consolidated9.Font.Size = 7;
                            range_Consolidated10.Font.Size = 8;
                            range_Consolidated10.Font.Bold = true;
                            range_Consolidated11.NumberFormat = "@";
                            range_Consolidated12.NumberFormat = "@";

                            workSheet.Cells[1, 5] = $"{textBox_dateTO.Text.Remove(textBox_dateTO.Text.IndexOf(" "))}";
                            workSheet.Cells[1, 13] = $"{textBox_dateTO.Text.Remove(textBox_dateTO.Text.IndexOf(" "))}";
                            workSheet.Cells[3, 3] = $"НАКЛАДНАЯ №";
                            workSheet.Cells[3, 5] = $"{textBox_numberAct.Text}";
                            workSheet.Cells[3, 11] = $"НАКЛАДНАЯ №";
                            workSheet.Cells[3, 13] = $"{textBox_numberAct.Text}";
                            workSheet.Cells[5, 1] = $"От кого";
                            workSheet.Cells[5, 3] = $"{textBox_company.Text}";
                            workSheet.Cells[5, 5] = $"{textBox_location.Text}";
                            workSheet.Cells[5, 9] = $"От кого";
                            workSheet.Cells[5, 11] = $"{textBox_company.Text}";
                            workSheet.Cells[5, 13] = $"{textBox_location.Text}";
                            workSheet.Cells[6, 1] = $"Кому";
                            workSheet.Cells[6, 3] = $" ООО \"СервисТелеком\"";
                            workSheet.Cells[6, 9] = $"Кому";
                            workSheet.Cells[6, 11] = $" ООО \"СервисТелеком\"";
                            workSheet.Cells[7, 1] = $"№";
                            workSheet.Cells[7, 2] = $"Заводской номер";
                            workSheet.Cells[7, 4] = $"№ АКБ";
                            workSheet.Cells[7, 5] = $"ЗУ\n(шт.)";
                            workSheet.Cells[7, 6] = $"АНТ\n(шт.)";
                            workSheet.Cells[7, 7] = $"МАН\n(шт.)";
                            workSheet.Cells[7, 9] = $"№";
                            workSheet.Cells[7, 10] = $"Заводской номер";
                            workSheet.Cells[7, 12] = $"№ АКБ";
                            workSheet.Cells[7, 13] = $"ЗУ\n(шт.)";
                            workSheet.Cells[7, 14] = $"АНТ\n(шт.)";
                            workSheet.Cells[7, 15] = $"МАН\n(шт.)";
                            workSheet.Cells[28, 1] = $"Комплектность и работоспособность проверены в присутствии Заказчика. Знаки соответствия нанесены.";
                            workSheet.Cells[28, 9] = $"Комплектность и работоспособность проверены в присутствии Заказчика. Знаки соответствия нанесены.";
                            workSheet.Cells[29, 1] = $"Принял: Начальник участка\n по ТО и ремонту СРС";
                            workSheet.Cells[31, 1] = $"должность";
                            workSheet.Cells[33, 2] = $"{label_FIO_chief.Text}";
                            workSheet.Cells[34, 1] = $"подпись";
                            workSheet.Cells[34, 2] = $"расшифровка подписи";
                            workSheet.Cells[36, 1] = $"Сдал: Начальник участка\n по ТО и ремонту СРС";
                            workSheet.Cells[38, 1] = $"должность";
                            workSheet.Cells[40, 2] = $"{label_FIO_chief.Text}";
                            workSheet.Cells[41, 1] = $"подпись";
                            workSheet.Cells[41, 2] = $"расшифровка подписи";
                            workSheet.Cells[29, 5] = $"Сдал: {textBox_post.Text}\n";
                            workSheet.Cells[31, 5] = $"должность";
                            workSheet.Cells[33, 6] = $"{textBox_representative.Text}";
                            workSheet.Cells[34, 5] = $"подпись";
                            workSheet.Cells[34, 6] = $"расшифровка подписи";
                            workSheet.Cells[36, 5] = $"Принял: {textBox_post.Text}\n";
                            workSheet.Cells[38, 5] = $"должность";
                            workSheet.Cells[40, 6] = $"{textBox_representative.Text}";
                            workSheet.Cells[41, 5] = $"подпись";
                            workSheet.Cells[41, 6] = $"расшифровка подписи";
                            workSheet.Cells[42, 4] = $"Ведомость измерения параметров получил";
                            workSheet.Cells[43, 4] = $"Удостоверение \"№\":{textBox_numberIdentification.Text}";
                            workSheet.Cells[29, 9] = $"Принял: Начальник участка\n по ТО и ремонту СРС";
                            workSheet.Cells[31, 9] = $"должность";
                            workSheet.Cells[33, 10] = $"{label_FIO_chief.Text}";
                            workSheet.Cells[34, 9] = $"подпись";
                            workSheet.Cells[34, 10] = $"расшифровка подписи";
                            workSheet.Cells[36, 9] = $"Сдал: Начальник участка\n по ТО и ремонту СРС";
                            workSheet.Cells[38, 9] = $"подпись";
                            workSheet.Cells[40, 10] = $"{label_FIO_chief.Text}";
                            workSheet.Cells[41, 9] = $"подпись";
                            workSheet.Cells[41, 10] = $"расшифровка подписи";
                            workSheet.Cells[29, 13] = $"Сдал: {textBox_post.Text}\n";
                            workSheet.Cells[31, 13] = $"должность";
                            workSheet.Cells[33, 14] = $"{textBox_representative.Text}";
                            workSheet.Cells[34, 13] = $"подпись";
                            workSheet.Cells[34, 14] = $"расшифровка подписи";
                            workSheet.Cells[36, 13] = $"Принял: {textBox_post.Text}\n";
                            workSheet.Cells[38, 13] = $"должность";
                            workSheet.Cells[40, 14] = $"{textBox_representative.Text}";
                            workSheet.Cells[41, 13] = $"подпись";
                            workSheet.Cells[41, 14] = $"расшифровка подписи";
                            workSheet.Cells[42, 12] = $"Ведомость измерения параметров получил";
                            workSheet.Cells[43, 12] = $"Удостоверение \"№\":{textBox_numberIdentification.Text}";

                            int s = 1;
                            int j = 8;

                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                workSheet.Cells[7 + s, 1] = s;
                                workSheet.Cells[7 + s, 9] = s;

                                Excel.Range _excelCells22 = (Excel.Range)workSheet.get_Range($"B{j}", $"C{j}").Cells;
                                _excelCells22.Merge(Type.Missing);
                                _excelCells22.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 2] = dataGridView1.Rows[i].Cells["serialNumber"].Value.ToString();

                                Excel.Range _excelCells23 = (Excel.Range)workSheet.get_Range($"J{j}", $"K{j}").Cells;
                                _excelCells23.Merge(Type.Missing);
                                _excelCells23.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 10] = dataGridView1.Rows[i].Cells["serialNumber"].Value.ToString();

                                Excel.Range _excelCells24 = (Excel.Range)workSheet.get_Range($"D{j}").Cells;
                                _excelCells24.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 4] = dataGridView1.Rows[i].Cells["AKB"].Value.ToString();

                                Excel.Range _excelCells25 = (Excel.Range)workSheet.get_Range($"K{j}").Cells;
                                _excelCells25.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 12] = dataGridView1.Rows[i].Cells["AKB"].Value.ToString();

                                Excel.Range _excelCells26 = (Excel.Range)workSheet.get_Range($"E{j}").Cells;
                                _excelCells26.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 5] = dataGridView1.Rows[i].Cells["batteryСharger"].Value.ToString();

                                Excel.Range _excelCells27 = (Excel.Range)workSheet.get_Range($"L{j}").Cells;
                                _excelCells27.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 13] = dataGridView1.Rows[i].Cells["batteryСharger"].Value.ToString();

                                Excel.Range _excelCells28 = (Excel.Range)workSheet.get_Range($"F{j}").Cells;
                                _excelCells28.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 6] = dataGridView1.Rows[i].Cells["antenna"].Value.ToString();

                                Excel.Range _excelCells29 = (Excel.Range)workSheet.get_Range($"M{j}").Cells;
                                _excelCells29.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 14] = dataGridView1.Rows[i].Cells["antenna"].Value.ToString();

                                Excel.Range _excelCells30 = (Excel.Range)workSheet.get_Range($"G{j}").Cells;
                                _excelCells30.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 7] = dataGridView1.Rows[i].Cells["manipulator"].Value.ToString();

                                Excel.Range _excelCells31 = (Excel.Range)workSheet.get_Range($"N{j}").Cells;
                                _excelCells31.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 15] = dataGridView1.Rows[i].Cells["manipulator"].Value.ToString();

                                s++;
                                j++;
                            }

                            while (s <= 20)
                            {
                                workSheet.Cells[7 + s, 1] = s;
                                workSheet.Cells[7 + s, 9] = s;

                                Excel.Range _excelCells22 = (Excel.Range)workSheet.get_Range($"B{j}", $"C{j}").Cells;
                                _excelCells22.Merge(Type.Missing);
                                _excelCells22.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells23 = (Excel.Range)workSheet.get_Range($"J{j}", $"K{j}").Cells;
                                _excelCells23.Merge(Type.Missing);
                                _excelCells23.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells24 = (Excel.Range)workSheet.get_Range($"D{j}").Cells;
                                _excelCells24.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells25 = (Excel.Range)workSheet.get_Range($"K{j}").Cells;
                                _excelCells25.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells26 = (Excel.Range)workSheet.get_Range($"E{j}").Cells;
                                _excelCells26.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells27 = (Excel.Range)workSheet.get_Range($"L{j}").Cells;
                                _excelCells27.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells28 = (Excel.Range)workSheet.get_Range($"F{j}").Cells;
                                _excelCells28.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells29 = (Excel.Range)workSheet.get_Range($"M{j}").Cells;
                                _excelCells29.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells30 = (Excel.Range)workSheet.get_Range($"G{j}").Cells;
                                _excelCells30.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells31 = (Excel.Range)workSheet.get_Range($"N{j}").Cells;
                                _excelCells31.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                s++;
                                j++;
                            }

                            #endregion

                            #region Ведомость То 2 Item


                            workSheet2.PageSetup.Zoom = false;
                            workSheet2.PageSetup.FitToPagesWide = 1;
                            workSheet2.PageSetup.FitToPagesTall = 1;

                            workSheet2.Rows.Font.Size = 15;
                            workSheet2.Rows.Font.Name = "Times New Roman";

                            workSheet2.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                            workSheet2.PageSetup.CenterHorizontally = true;
                            workSheet2.PageSetup.CenterVertically = true;
                            workSheet2.PageSetup.TopMargin = 0;
                            workSheet2.PageSetup.BottomMargin = 0;
                            workSheet2.PageSetup.LeftMargin = 0;
                            workSheet2.PageSetup.RightMargin = 0;

                            Excel.Range _excelCells200 = (Excel.Range)workSheet2.get_Range("A1", "G1").Cells;
                            Excel.Range _excelCells201 = (Excel.Range)workSheet2.get_Range("H1", "I1").Cells;
                            Excel.Range _excelCells202 = (Excel.Range)workSheet2.get_Range("J1", "K1").Cells;
                            Excel.Range _excelCells203 = (Excel.Range)workSheet2.get_Range("L1", "M1").Cells;
                            Excel.Range _excelCells204 = (Excel.Range)workSheet2.get_Range("T1", "V1").Cells;
                            Excel.Range _excelCells205 = (Excel.Range)workSheet2.get_Range("A2", "A19").Cells;
                            Excel.Range _excelCells206 = (Excel.Range)workSheet2.get_Range("B2", "C2").Cells;
                            Excel.Range _excelCells207 = (Excel.Range)workSheet2.get_Range("B3", "B19").Cells;
                            Excel.Range _excelCells208 = (Excel.Range)workSheet2.get_Range("C3", "C19").Cells;
                            Excel.Range _excelCells209 = (Excel.Range)workSheet2.get_Range("D2", "I2").Cells;
                            Excel.Range _excelCells210 = (Excel.Range)workSheet2.get_Range("D3", "E6").Cells;
                            Excel.Range _excelCells211 = (Excel.Range)workSheet2.get_Range("D7", "D19").Cells;
                            Excel.Range _excelCells212 = (Excel.Range)workSheet2.get_Range("E7", "E19").Cells;
                            Excel.Range _excelCells213 = (Excel.Range)workSheet2.get_Range("F3", "F19").Cells;
                            Excel.Range _excelCells214 = (Excel.Range)workSheet2.get_Range("G3", "G19").Cells;
                            Excel.Range _excelCells215 = (Excel.Range)workSheet2.get_Range("H3", "H19").Cells;
                            Excel.Range _excelCells216 = (Excel.Range)workSheet2.get_Range("I3", "I19").Cells;
                            Excel.Range _excelCells217 = (Excel.Range)workSheet2.get_Range("J2", "O2").Cells;
                            Excel.Range _excelCells218 = (Excel.Range)workSheet2.get_Range("J3", "J19").Cells;
                            Excel.Range _excelCells219 = (Excel.Range)workSheet2.get_Range("K3", "L18").Cells;
                            Excel.Range _excelCells220 = (Excel.Range)workSheet2.get_Range("K19").Cells;
                            Excel.Range _excelCells221 = (Excel.Range)workSheet2.get_Range("L19").Cells;
                            Excel.Range _excelCells222 = (Excel.Range)workSheet2.get_Range("M3", "M19").Cells;
                            Excel.Range _excelCells223 = (Excel.Range)workSheet2.get_Range("N3", "N19").Cells;
                            Excel.Range _excelCells224 = (Excel.Range)workSheet2.get_Range("O3", "O19").Cells;
                            Excel.Range _excelCells225 = (Excel.Range)workSheet2.get_Range("P2", "R5").Cells;
                            Excel.Range _excelCells226 = (Excel.Range)workSheet2.get_Range("P6", "P19").Cells;
                            Excel.Range _excelCells227 = (Excel.Range)workSheet2.get_Range("Q6", "Q19").Cells;
                            Excel.Range _excelCells228 = (Excel.Range)workSheet2.get_Range("R6", "R19").Cells;
                            Excel.Range _excelCells229 = (Excel.Range)workSheet2.get_Range("S2", "S19").Cells;
                            Excel.Range _excelCells230 = (Excel.Range)workSheet2.get_Range("T2", "U4").Cells;
                            Excel.Range _excelCells231 = (Excel.Range)workSheet2.get_Range("T5", "T19").Cells;
                            Excel.Range _excelCells232 = (Excel.Range)workSheet2.get_Range("U5", "U19").Cells;
                            Excel.Range _excelCells233 = (Excel.Range)workSheet2.get_Range("V2", "Y2").Cells;
                            Excel.Range _excelCells234 = (Excel.Range)workSheet2.get_Range("V3", "W3").Cells;
                            Excel.Range _excelCells235 = (Excel.Range)workSheet2.get_Range("X3", "Y3").Cells;
                            Excel.Range _excelCells238 = (Excel.Range)workSheet2.get_Range("A2", "U39").Cells;
                            Excel.Range _excelCells240 = (Excel.Range)workSheet2.get_Range("V4", "Y39").Cells;
                            Excel.Range _excelCells241 = (Excel.Range)workSheet2.get_Range("V3", "Y3").Cells;
                            Excel.Range _excelCells243 = (Excel.Range)workSheet2.get_Range("A41").Cells;
                            Excel.Range _excelCells244 = (Excel.Range)workSheet2.get_Range("B41", "G41").Cells;
                            Excel.Range _excelCells245 = (Excel.Range)workSheet2.get_Range("H41", "J41").Cells;
                            Excel.Range _excelCells246 = (Excel.Range)workSheet2.get_Range("K41", "P41").Cells;
                            Excel.Range _excelCells247 = (Excel.Range)workSheet2.get_Range("T41", "Y41").Cells;
                            Excel.Range _excelCells248 = (Excel.Range)workSheet2.get_Range("B42", "G42").Cells;
                            Excel.Range _excelCells249 = (Excel.Range)workSheet2.get_Range("H42", "J42").Cells;
                            Excel.Range _excelCells250 = (Excel.Range)workSheet2.get_Range("K42", "P42").Cells;
                            Excel.Range _excelCells251 = (Excel.Range)workSheet2.get_Range("B44", "G44").Cells;
                            Excel.Range _excelCells252 = (Excel.Range)workSheet2.get_Range("H44", "J44").Cells;
                            Excel.Range _excelCells253 = (Excel.Range)workSheet2.get_Range("K44", "P44").Cells;
                            Excel.Range _excelCells254 = (Excel.Range)workSheet2.get_Range("B45", "G45").Cells;
                            Excel.Range _excelCells255 = (Excel.Range)workSheet2.get_Range("H45", "J45").Cells;
                            Excel.Range _excelCells256 = (Excel.Range)workSheet2.get_Range("K45", "P45").Cells;
                            Excel.Range _excelCells257 = (Excel.Range)workSheet2.get_Range("T42", "Y42").Cells;
                            Excel.Range _excelCells258 = (Excel.Range)workSheet2.get_Range("S44", "U44").Cells;
                            Excel.Range _excelCells259 = (Excel.Range)workSheet2.get_Range("V44", "X44").Cells;
                            Excel.Range _excelCells260 = (Excel.Range)workSheet2.get_Range("B47", "F47").Cells;
                            Excel.Range _excelCells261 = (Excel.Range)workSheet2.get_Range("H47", "L47").Cells;
                            Excel.Range _excelCells262 = (Excel.Range)workSheet2.get_Range("D2", "I39").Cells;
                            Excel.Range _excelCells263 = (Excel.Range)workSheet2.get_Range("J2", "O39").Cells;
                            Excel.Range _excelCells264 = (Excel.Range)workSheet2.get_Range("B2", "C39").Cells;
                            Excel.Range _excelCells265 = (Excel.Range)workSheet2.get_Range("P2", "S39").Cells;
                            Excel.Range _excelCells266 = (Excel.Range)workSheet2.get_Range("A2", "A39").Cells;
                            Excel.Range _excelCells267 = (Excel.Range)workSheet2.get_Range("T2", "U39").Cells;
                            Excel.Range _excelCells268 = (Excel.Range)workSheet2.get_Range("V2", "Y39").Cells;
                            Excel.Range _excelCells269 = (Excel.Range)workSheet2.get_Range("A20", "U39").Cells;

                            _excelCells200.Merge(Type.Missing);
                            _excelCells201.Merge(Type.Missing);
                            _excelCells202.Merge(Type.Missing);
                            _excelCells203.Merge(Type.Missing);
                            _excelCells204.Merge(Type.Missing);
                            _excelCells205.Merge(Type.Missing);
                            _excelCells206.Merge(Type.Missing);
                            _excelCells207.Merge(Type.Missing);
                            _excelCells208.Merge(Type.Missing);
                            _excelCells209.Merge(Type.Missing);
                            _excelCells210.Merge(Type.Missing);
                            _excelCells211.Merge(Type.Missing);
                            _excelCells212.Merge(Type.Missing);
                            _excelCells213.Merge(Type.Missing);
                            _excelCells214.Merge(Type.Missing);
                            _excelCells215.Merge(Type.Missing);
                            _excelCells216.Merge(Type.Missing);
                            _excelCells217.Merge(Type.Missing);
                            _excelCells218.Merge(Type.Missing);
                            _excelCells219.Merge(Type.Missing);
                            _excelCells222.Merge(Type.Missing);
                            _excelCells223.Merge(Type.Missing);
                            _excelCells224.Merge(Type.Missing);
                            _excelCells225.Merge(Type.Missing);
                            _excelCells226.Merge(Type.Missing);
                            _excelCells227.Merge(Type.Missing);
                            _excelCells228.Merge(Type.Missing);
                            _excelCells229.Merge(Type.Missing);
                            _excelCells230.Merge(Type.Missing);
                            _excelCells231.Merge(Type.Missing);
                            _excelCells232.Merge(Type.Missing);
                            _excelCells233.Merge(Type.Missing);
                            _excelCells234.Merge(Type.Missing);
                            _excelCells235.Merge(Type.Missing);
                            _excelCells244.Merge(Type.Missing);
                            _excelCells245.Merge(Type.Missing);
                            _excelCells246.Merge(Type.Missing);
                            _excelCells247.Merge(Type.Missing);
                            _excelCells248.Merge(Type.Missing);
                            _excelCells249.Merge(Type.Missing);
                            _excelCells250.Merge(Type.Missing);
                            _excelCells251.Merge(Type.Missing);
                            _excelCells252.Merge(Type.Missing);
                            _excelCells253.Merge(Type.Missing);
                            _excelCells254.Merge(Type.Missing);
                            _excelCells255.Merge(Type.Missing);
                            _excelCells256.Merge(Type.Missing);
                            _excelCells257.Merge(Type.Missing);
                            _excelCells258.Merge(Type.Missing);
                            _excelCells259.Merge(Type.Missing);
                            _excelCells260.Merge(Type.Missing);
                            _excelCells261.Merge(Type.Missing);

                            _excelCells238.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells238.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells238.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells238.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells238.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells238.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells233.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells233.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells233.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells233.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells241.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells241.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells241.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells241.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells240.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells240.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells240.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells240.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells240.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells240.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells244.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells245.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells246.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells247.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells251.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells252.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells253.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells259.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells262.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells262.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells262.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells262.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells263.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells263.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells263.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells263.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells264.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells264.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells264.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells264.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells265.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells265.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells265.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells265.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells266.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells266.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells266.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells266.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells267.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells267.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells267.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells267.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells268.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells268.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells268.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells268.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells269.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlDash;
                            //_excelCells269.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlDash;

                            _excelCells200.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells201.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells202.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells203.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells204.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells205.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells205.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells206.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells207.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells207.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells207.Orientation = 90;

                            _excelCells208.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells208.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells208.Orientation = 90;

                            _excelCells209.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells210.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells210.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells211.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells211.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells211.Orientation = 90;

                            _excelCells212.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells212.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells212.Orientation = 90;

                            _excelCells213.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells213.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells213.Orientation = 90;

                            _excelCells214.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells214.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells214.Orientation = 90;

                            _excelCells215.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells215.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells215.Orientation = 90;

                            _excelCells216.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells216.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells216.Orientation = 90;

                            _excelCells217.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells218.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells218.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells218.Orientation = 90;

                            _excelCells219.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells219.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells219.Orientation = 90;

                            _excelCells220.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells221.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells222.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells222.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells222.Orientation = 90;

                            _excelCells223.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells223.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells223.Orientation = 90;

                            _excelCells224.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells224.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells224.Orientation = 90;

                            _excelCells225.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells225.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells226.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells226.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells226.Orientation = 90;

                            _excelCells227.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells227.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells227.Orientation = 90;

                            _excelCells228.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells228.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells228.Orientation = 90;

                            _excelCells229.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells229.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells229.Orientation = 90;

                            _excelCells230.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells230.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells231.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells231.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells231.Orientation = 90;

                            _excelCells232.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells232.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells232.Orientation = 90;

                            _excelCells233.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells234.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells235.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells243.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells244.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells246.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells247.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells248.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells249.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells250.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells251.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells252.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells253.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells254.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells255.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells256.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells257.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells258.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells259.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells260.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells261.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            Excel.Range rowHeight200 = workSheet2.get_Range("A1", "G1");
                            rowHeight200.EntireRow.RowHeight = 15; //

                            Excel.Range rowColum200 = workSheet2.get_Range("A1", "G1");
                            rowColum200.EntireColumn.ColumnWidth = 5; //

                            Excel.Range rowHeight201 = workSheet2.get_Range("A2", "A19");
                            rowHeight201.EntireRow.RowHeight = 15; //

                            Excel.Range rowColum201 = workSheet2.get_Range("A2", "A19");
                            rowColum201.EntireColumn.ColumnWidth = 25; //

                            Excel.Range rowColum202 = workSheet2.get_Range("B2", "C2");
                            rowColum202.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum203 = workSheet2.get_Range("D7", "E7");
                            rowColum203.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum204 = workSheet2.get_Range("F3", "F14");
                            rowColum204.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum205 = workSheet2.get_Range("G3", "G14");
                            rowColum205.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum206 = workSheet2.get_Range("H3", "H14");
                            rowColum206.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum207 = workSheet2.get_Range("I3", "I14");
                            rowColum207.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum208 = workSheet2.get_Range("V4", "V19");
                            rowColum208.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum209 = workSheet2.get_Range("X3", "Y3");
                            rowColum209.EntireColumn.ColumnWidth = 15;

                            Excel.Range rowHeight210 = workSheet2.get_Range("A20", "A39");
                            rowHeight210.EntireRow.RowHeight = 25; //

                            Excel.Range rowHeight211 = workSheet2.get_Range("A42", "Y42");
                            rowHeight211.EntireRow.RowHeight = 12; //

                            Excel.Range rowHeight212 = workSheet2.get_Range("A45", "P45");
                            rowHeight212.EntireRow.RowHeight = 12; //

                            Excel.Range rowHeight213 = workSheet2.get_Range("B47", "L47");
                            rowHeight213.EntireRow.RowHeight = 12; //

                            Excel.Range rowColum214 = workSheet2.get_Range("V3", "W3");
                            rowColum214.EntireColumn.ColumnWidth = 2;

                            Excel.Range rowHeight215 = workSheet2.get_Range("V4", "V19");
                            rowHeight215.EntireRow.RowHeight = 20;

                            Excel.Range rowColum216 = workSheet2.get_Range("B3", "B19");
                            rowColum216.EntireColumn.ColumnWidth = 15;

                            Excel.Range rowHeight217 = workSheet2.get_Range("A1", "Y1");
                            rowHeight217.EntireRow.RowHeight = 25;

                            Excel.Range rowHeight218 = workSheet2.get_Range("A2", "Y2");
                            rowHeight218.EntireRow.RowHeight = 20;

                            Excel.Range rowHeight219 = workSheet2.get_Range("A20", "A39");
                            rowHeight219.EntireRow.RowHeight = 25;

                            Excel.Range range_Consolidated200 = workSheet2.Rows.get_Range("H1", "I1");
                            Excel.Range range_Consolidated201 = workSheet2.Rows.get_Range("L1", "M1");
                            Excel.Range range_Consolidated202 = workSheet2.Rows.get_Range("T1", "V1");
                            Excel.Range range_Consolidated203 = workSheet2.Rows.get_Range("V4", "V19");
                            Excel.Range range_Consolidated204 = workSheet2.Rows.get_Range("A42", "Y42");
                            Excel.Range range_Consolidated205 = workSheet2.Rows.get_Range("A45", "P45");
                            Excel.Range range_Consolidated206 = workSheet2.Rows.get_Range("V44", "X44");
                            Excel.Range range_Consolidated207 = workSheet2.Rows.get_Range("B47", "L47");
                            Excel.Range range_Consolidated208 = workSheet2.Rows.get_Range("A2", "U19");
                            Excel.Range range_Consolidated209 = workSheet2.Rows.get_Range("A1", "G1");
                            Excel.Range range_Consolidated210 = workSheet2.Rows.get_Range("A20", "A39");
                            Excel.Range range_Consolidated211 = workSheet2.Rows.get_Range("K41", "P41");
                            Excel.Range range_Consolidated212 = workSheet2.Rows.get_Range("T41", "Y41");
                            Excel.Range range_Consolidated213 = workSheet2.Rows.get_Range("A20", "A39");

                            range_Consolidated200.Font.Size = 18;
                            range_Consolidated200.Font.Bold = true;
                            range_Consolidated201.Font.Size = 18;
                            range_Consolidated201.Font.Bold = true;
                            range_Consolidated202.Font.Size = 18;
                            range_Consolidated202.Font.Bold = true;
                            range_Consolidated203.Font.Size = 10;
                            range_Consolidated203.Font.Bold = true;
                            range_Consolidated204.Font.Size = 10;
                            range_Consolidated205.Font.Size = 10;
                            range_Consolidated206.Font.Bold = true;
                            range_Consolidated207.Font.Size = 10;
                            range_Consolidated208.Font.Bold = true;
                            range_Consolidated209.Font.Size = 16;
                            range_Consolidated210.Font.Size = 18;
                            range_Consolidated210.Font.Bold = true;
                            range_Consolidated211.Font.Bold = true;
                            range_Consolidated212.Font.Bold = true;
                            range_Consolidated213.NumberFormat = "@";

                            workSheet2.Cells[1, 1] = $"Ведомость проверки параметров радиостанций №:";
                            workSheet2.Cells[1, 8] = $"{textBox_numberAct.Text}";
                            workSheet2.Cells[1, 10] = $"Предприятие:";
                            workSheet2.Cells[1, 12] = $"{textBox_company.Text}";
                            workSheet2.Cells[1, 20] = $"{textBox_location.Text}";
                            workSheet2.Cells[2, 1] = $"№ р/с";
                            workSheet2.Cells[2, 2] = $"АКБ";
                            workSheet2.Cells[3, 2] = $"серия, № АКБ";
                            workSheet2.Cells[3, 3] = $"Остаточная ёмкость АКБ, %";
                            workSheet2.Cells[2, 4] = $"Параметры передатчика";
                            workSheet2.Cells[3, 4] = $"Выходная\n мощность\n передатчика, Вт";
                            workSheet2.Cells[7, 4] = $"Низкий уровень";
                            workSheet2.Cells[7, 5] = $"Высокий уровень";
                            workSheet2.Cells[3, 6] = $"Отклонение частоты\n от номинала, Гц";
                            workSheet2.Cells[3, 7] = $"КНИ, %";
                            workSheet2.Cells[3, 8] = $"Чувствительность\n модуляционного входа, мВ";
                            workSheet2.Cells[3, 9] = $"Максимальная девиация\n частоты, кГц";
                            workSheet2.Cells[2, 10] = $"Параметры приёмника";
                            workSheet2.Cells[3, 10] = $"Чувствительность\n приемника, мкВ";
                            workSheet2.Cells[3, 11] = $"Выходная мощность приёмника, В";
                            workSheet2.Cells[19, 11] = $"В";
                            workSheet2.Cells[19, 12] = $"Вт";
                            workSheet2.Cells[3, 13] = $"Избирательность\n по соседнему каналу, дБ";
                            workSheet2.Cells[3, 14] = $"КНИ, %";
                            workSheet2.Cells[3, 15] = $"Порог срабатывания\n шумоподавителя, мкВ";
                            workSheet2.Cells[2, 16] = $"Потребляемый ток";
                            workSheet2.Cells[6, 16] = $"\"Дежурный режим\", мА";
                            workSheet2.Cells[6, 17] = $"\"Режим приём, мА\", мА";
                            workSheet2.Cells[6, 18] = $"\"Режим передачи\n (высокая мощность)\", А";
                            workSheet2.Cells[2, 19] = $"Сигнализация разряда АКБ, В";
                            workSheet2.Cells[2, 20] = $"Аксессуары \n(при наличии)";
                            workSheet2.Cells[5, 20] = $"ЗУ испр / неиспр";
                            workSheet2.Cells[5, 21] = $"Манипулятор: \n испр / неиспр";
                            workSheet2.Cells[2, 22] = $"Частоты (МГц)";
                            workSheet2.Cells[3, 22] = $"";
                            workSheet2.Cells[3, 24] = $"передача / приём    ";
                            workSheet2.Cells[41, 1] = $"Исполнитель работ:";
                            workSheet2.Cells[41, 2] = $"Инженер по ТО и ремонту СРС";
                            workSheet2.Cells[41, 8] = $"/                                     /";
                            workSheet2.Cells[41, 11] = $"{label_FIO_Engineer.Text}";
                            workSheet2.Cells[41, 20] = $"{textBox_dateTO.Text.Remove(textBox_dateTO.Text.IndexOf(" "))} г.";
                            workSheet2.Cells[42, 2] = $"должность";
                            workSheet2.Cells[42, 8] = $"подпись";
                            workSheet2.Cells[42, 11] = $"расшифровка подписи";
                            workSheet2.Cells[42, 20] = $"дата проведения технического обслуживания";
                            workSheet2.Cells[44, 1] = $"Представитель РЦС:";
                            workSheet2.Cells[44, 2] = $"";
                            workSheet2.Cells[44, 8] = $"/                                     /";
                            workSheet2.Cells[44, 11] = $"";
                            workSheet2.Cells[45, 2] = $"должность";
                            workSheet2.Cells[45, 8] = $"подпись";
                            workSheet2.Cells[45, 11] = $"расшифровка подписи";
                            workSheet2.Cells[44, 19] = $"Частота проверки:";
                            workSheet2.Cells[44, 22] = $"151.825";
                            workSheet2.Cells[47, 2] = $"Примечание: 1. \" - \" - не предоставлено для ТО";
                            workSheet2.Cells[47, 8] = $"2. \" б/н \" - без номера (номер отсутсвует)";


                            int s3 = 1;
                            int j3 = 4;

                            for (int i = 0; i < 16; i++)
                            {
                                workSheet2.Cells[3 + s3, 22] = s3;
                                Excel.Range _excelCells236 = (Excel.Range)workSheet2.get_Range($"V{j3}").Cells;
                                _excelCells236.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;


                                Excel.Range _excelCells237 = (Excel.Range)workSheet2.get_Range($"W{j3}", $"Y{j3}").Cells;
                                _excelCells237.Merge(Type.Missing);

                                s3++;
                                j3++;

                            }
                            int j4 = 20;
                            int s4 = 1;
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                Excel.Range _excelCells242 = (Excel.Range)workSheet2.get_Range($"A{j4}").Cells;
                                _excelCells242.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                _excelCells242.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                workSheet2.Cells[19 + s4, 1] = dataGridView1.Rows[i].Cells["serialNumber"].Value.ToString();

                                Excel.Range _excelCells239 = (Excel.Range)workSheet2.get_Range($"V{j4}", $"Y{j4}").Cells;
                                _excelCells239.Merge(Type.Missing);

                                s4++;
                                j4++;

                            }
                            while (s4 <= 20)
                            {
                                Excel.Range _excelCells242 = (Excel.Range)workSheet2.get_Range($"A{j4}").Cells;
                                _excelCells242.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells239 = (Excel.Range)workSheet2.get_Range($"V{j4}", $"Y{j4}").Cells;
                                _excelCells239.Merge(Type.Missing);

                                s4++;
                                j4++;
                            }


                            #endregion

                            #region АКТ ТО 3 Item
                            workSheet3.PageSetup.Zoom = false;
                            workSheet3.PageSetup.FitToPagesWide = 1;
                            workSheet3.PageSetup.FitToPagesTall = 1;

                            workSheet3.Rows.Font.Size = 11;
                            workSheet3.Rows.Font.Name = "Times New Roman";

                            workSheet2.PageSetup.CenterHorizontally = true;
                            workSheet2.PageSetup.CenterVertically = true;
                            workSheet3.PageSetup.TopMargin = 50;
                            workSheet3.PageSetup.BottomMargin = 0;
                            workSheet3.PageSetup.LeftMargin = 90;
                            workSheet3.PageSetup.RightMargin = 0;

                            workSheet3.PageSetup.Zoom = 97;

                            Excel.Range _excelCells101 = (Excel.Range)workSheet3.get_Range("A1", "I1").Cells;
                            Excel.Range _excelCells102 = (Excel.Range)workSheet3.get_Range("A2", "I2").Cells;
                            Excel.Range _excelCells103 = (Excel.Range)workSheet3.get_Range("A4", "C4").Cells;
                            Excel.Range _excelCells104 = (Excel.Range)workSheet3.get_Range("H4", "I4").Cells;
                            Excel.Range _excelCells105 = (Excel.Range)workSheet3.get_Range("A5", "C5").Cells;
                            Excel.Range _excelCells106 = (Excel.Range)workSheet3.get_Range("H5", "I5").Cells;
                            Excel.Range _excelCells107 = (Excel.Range)workSheet3.get_Range("A6", "G6").Cells;
                            Excel.Range _excelCells108 = (Excel.Range)workSheet3.get_Range("A7", "I7").Cells;
                            Excel.Range _excelCells109 = (Excel.Range)workSheet3.get_Range("A8", "G8").Cells;
                            Excel.Range _excelCells110 = (Excel.Range)workSheet3.get_Range("H8", "I8").Cells;
                            Excel.Range _excelCells111 = (Excel.Range)workSheet3.get_Range("A9", "I9").Cells;
                            Excel.Range _excelCells112 = (Excel.Range)workSheet3.get_Range("A10", "I10").Cells;
                            Excel.Range _excelCells113 = (Excel.Range)workSheet3.get_Range("A11", "C11").Cells;
                            Excel.Range _excelCells114 = (Excel.Range)workSheet3.get_Range("A12", "C12").Cells;
                            Excel.Range _excelCells115 = (Excel.Range)workSheet3.get_Range("G12", "I12").Cells;
                            Excel.Range _excelCells116 = (Excel.Range)workSheet3.get_Range("A13", "C13").Cells;
                            Excel.Range _excelCells117 = (Excel.Range)workSheet3.get_Range("D13", "E13").Cells;
                            Excel.Range _excelCells118 = (Excel.Range)workSheet3.get_Range("F13", "G13").Cells;
                            Excel.Range _excelCells119 = (Excel.Range)workSheet3.get_Range("A14", "I14").Cells;
                            Excel.Range _excelCells120 = (Excel.Range)workSheet3.get_Range("A15", "I15").Cells;
                            Excel.Range _excelCells121 = (Excel.Range)workSheet3.get_Range("A16", "I16").Cells;
                            Excel.Range _excelCells122 = (Excel.Range)workSheet3.get_Range("A18", "A37").Cells;
                            Excel.Range _excelCells123 = (Excel.Range)workSheet3.get_Range("A17").Cells;
                            Excel.Range _excelCells124 = (Excel.Range)workSheet3.get_Range("B17", "C17").Cells;
                            Excel.Range _excelCells125 = (Excel.Range)workSheet3.get_Range("D17", "F17").Cells;
                            Excel.Range _excelCells126 = (Excel.Range)workSheet3.get_Range("G17", "I17").Cells;
                            Excel.Range _excelCells127 = (Excel.Range)workSheet3.get_Range("A17", "I37").Cells;
                            Excel.Range _excelCells131 = (Excel.Range)workSheet3.get_Range("A38", "I38").Cells;
                            Excel.Range _excelCells132 = (Excel.Range)workSheet3.get_Range("A39", "I39").Cells;
                            Excel.Range _excelCells133 = (Excel.Range)workSheet3.get_Range("A40", "I40").Cells;
                            Excel.Range _excelCells134 = (Excel.Range)workSheet3.get_Range("A41", "I41").Cells;
                            Excel.Range _excelCells135 = (Excel.Range)workSheet3.get_Range("A42", "C42").Cells;
                            Excel.Range _excelCells136 = (Excel.Range)workSheet3.get_Range("F42", "H42").Cells;
                            Excel.Range _excelCells137 = (Excel.Range)workSheet3.get_Range("A43", "C43").Cells;
                            Excel.Range _excelCells138 = (Excel.Range)workSheet3.get_Range("C45", "D45").Cells;
                            Excel.Range _excelCells139 = (Excel.Range)workSheet3.get_Range("F44", "G44").Cells;
                            Excel.Range _excelCells140 = (Excel.Range)workSheet3.get_Range("H44", "I44").Cells;
                            Excel.Range _excelCells141 = (Excel.Range)workSheet3.get_Range("A46", "B46").Cells;
                            Excel.Range _excelCells142 = (Excel.Range)workSheet3.get_Range("C46", "D46").Cells;
                            Excel.Range _excelCells143 = (Excel.Range)workSheet3.get_Range("F45", "G45").Cells;
                            Excel.Range _excelCells144 = (Excel.Range)workSheet3.get_Range("A47", "B47").Cells;
                            Excel.Range _excelCells145 = (Excel.Range)workSheet3.get_Range("A49", "D49").Cells;
                            Excel.Range _excelCells146 = (Excel.Range)workSheet3.get_Range("A50", "B50").Cells;
                            Excel.Range _excelCells147 = (Excel.Range)workSheet3.get_Range("C50", "D50").Cells;
                            Excel.Range _excelCells148 = (Excel.Range)workSheet3.get_Range("G11", "I11").Cells;
                            Excel.Range _excelCells150 = (Excel.Range)workSheet3.get_Range("D47");
                            Excel.Range _excelCells151 = (Excel.Range)workSheet3.get_Range("G47");
                            Excel.Range _excelCells152 = (Excel.Range)workSheet3.get_Range("A45", "B45").Cells;
                            Excel.Range _excelCells153 = (Excel.Range)workSheet3.get_Range("H45", "I45").Cells;
                            Excel.Range _excelCells154 = (Excel.Range)workSheet3.get_Range("H13", "I13").Cells;
                            Excel.Range _excelCells155 = (Excel.Range)workSheet3.get_Range("A17", "I17").Cells;

                            _excelCells101.Merge(Type.Missing);
                            _excelCells102.Merge(Type.Missing);
                            _excelCells103.Merge(Type.Missing);
                            _excelCells104.Merge(Type.Missing);
                            _excelCells105.Merge(Type.Missing);
                            _excelCells106.Merge(Type.Missing);
                            _excelCells107.Merge(Type.Missing);
                            _excelCells108.Merge(Type.Missing);
                            _excelCells109.Merge(Type.Missing);
                            _excelCells110.Merge(Type.Missing);
                            _excelCells111.Merge(Type.Missing);
                            _excelCells112.Merge(Type.Missing);
                            _excelCells113.Merge(Type.Missing);
                            _excelCells114.Merge(Type.Missing);
                            _excelCells115.Merge(Type.Missing);
                            _excelCells116.Merge(Type.Missing);
                            _excelCells117.Merge(Type.Missing);
                            _excelCells118.Merge(Type.Missing);
                            _excelCells119.Merge(Type.Missing);
                            _excelCells120.Merge(Type.Missing);
                            _excelCells121.Merge(Type.Missing);
                            _excelCells124.Merge(Type.Missing);
                            _excelCells125.Merge(Type.Missing);
                            _excelCells126.Merge(Type.Missing);
                            _excelCells131.Merge(Type.Missing);
                            _excelCells132.Merge(Type.Missing);
                            _excelCells133.Merge(Type.Missing);
                            _excelCells134.Merge(Type.Missing);
                            _excelCells135.Merge(Type.Missing);
                            _excelCells136.Merge(Type.Missing);
                            _excelCells137.Merge(Type.Missing);
                            _excelCells138.Merge(Type.Missing);
                            _excelCells139.Merge(Type.Missing);
                            _excelCells140.Merge(Type.Missing);
                            _excelCells141.Merge(Type.Missing);
                            _excelCells142.Merge(Type.Missing);
                            _excelCells143.Merge(Type.Missing);
                            _excelCells144.Merge(Type.Missing);
                            _excelCells145.Merge(Type.Missing);
                            _excelCells146.Merge(Type.Missing);
                            _excelCells147.Merge(Type.Missing);
                            _excelCells148.Merge(Type.Missing);
                            _excelCells152.Merge(Type.Missing);
                            _excelCells153.Merge(Type.Missing);
                            _excelCells154.Merge(Type.Missing);

                            _excelCells101.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells102.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells103.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells104.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells105.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells106.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells107.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells108.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells109.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells110.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells111.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells112.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells113.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells114.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells115.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells116.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells117.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells118.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells119.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells120.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells121.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells122.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells123.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells124.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells125.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells126.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells131.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells132.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells133.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells134.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells135.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells136.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells137.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells138.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells140.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells141.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells142.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells143.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells144.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells145.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells146.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells147.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells148.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells150.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells151.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells153.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells154.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells155.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells155.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;


                            _excelCells103.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells104.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells108.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells111.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells112.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells113.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells117.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;

                            _excelCells127.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells127.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells127.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells127.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells127.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells127.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlDot;

                            _excelCells138.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells139.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells140.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells145.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells148.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells152.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells154.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;

                            Excel.Range range_Consolidated102 = workSheet3.Rows.get_Range("A1", "H2");
                            Excel.Range range_Consolidated103 = workSheet3.Rows.get_Range("A4", "B4");
                            Excel.Range range_Consolidated104 = workSheet3.Rows.get_Range("A5", "I5");
                            Excel.Range range_Consolidated105 = workSheet3.Rows.get_Range("A6", "G6");
                            Excel.Range range_Consolidated106 = workSheet3.Rows.get_Range("A7", "I7");
                            Excel.Range range_Consolidated107 = workSheet3.Rows.get_Range("B8", "C8");
                            Excel.Range range_Consolidated108 = workSheet3.Rows.get_Range("H8", "I8");
                            Excel.Range range_Consolidated109 = workSheet3.Rows.get_Range("A9", "I9");
                            Excel.Range range_Consolidated110 = workSheet3.Rows.get_Range("A10", "I10");
                            Excel.Range range_Consolidated111 = workSheet3.Rows.get_Range("A11", "I11");
                            Excel.Range range_Consolidated112 = workSheet3.Rows.get_Range("B12", "C12");
                            Excel.Range range_Consolidated113 = workSheet3.Rows.get_Range("A12", "H12");
                            Excel.Range range_Consolidated114 = workSheet3.Rows.get_Range("A13", "C13");
                            Excel.Range range_Consolidated115 = workSheet3.Rows.get_Range("D13", "E13");
                            Excel.Range range_Consolidated116 = workSheet3.Rows.get_Range("F13", "I13");
                            Excel.Range range_Consolidated117 = workSheet3.Rows.get_Range("A14", "I14");
                            Excel.Range range_Consolidated118 = workSheet3.Rows.get_Range("A15", "I15");
                            Excel.Range range_Consolidated119 = workSheet3.Rows.get_Range("A16", "I16");
                            Excel.Range range_Consolidated120 = workSheet3.Rows.get_Range("B18", "I37");
                            Excel.Range range_Consolidated121 = workSheet3.Rows.get_Range("A17", "I17");
                            Excel.Range range_Consolidated122 = workSheet3.Rows.get_Range("A38", "I41");
                            Excel.Range range_Consolidated123 = workSheet3.Rows.get_Range("A42", "I43");
                            Excel.Range range_Consolidated124 = workSheet3.Rows.get_Range("A44", "I44");
                            Excel.Range range_Consolidated125 = workSheet3.Rows.get_Range("A46", "I46");
                            Excel.Range range_Consolidated126 = workSheet3.Rows.get_Range("A47", "B47");
                            Excel.Range range_Consolidated127 = workSheet3.Rows.get_Range("A50", "D50");
                            Excel.Range range_Consolidated128 = workSheet3.Rows.get_Range("D51");
                            Excel.Range range_Consolidated129 = workSheet3.Rows.get_Range("C45", "D45");
                            Excel.Range range_Consolidated130 = workSheet3.Rows.get_Range("F45", "I45");
                            Excel.Range range_Consolidated131 = workSheet3.Rows.get_Range("A18", "I37");
                            Excel.Range range_Consolidated132 = workSheet3.Rows.get_Range("D18", "F37");


                            Excel.Range rowHeight100 = workSheet3.get_Range("A38", "I41");
                            rowHeight100.EntireRow.RowHeight = 10; //

                            Excel.Range rowColum100 = workSheet3.get_Range("A17", "A37");
                            rowColum100.EntireColumn.ColumnWidth = 3; //

                            Excel.Range rowColum102 = workSheet3.get_Range("B17", "C37");
                            rowColum102.EntireColumn.ColumnWidth = 10; //

                            Excel.Range rowColum103 = workSheet3.get_Range("D17", "F37");
                            rowColum103.EntireColumn.ColumnWidth = 8; //

                            Excel.Range rowHeight101 = workSheet3.get_Range("A14", "I15");
                            rowHeight101.EntireRow.RowHeight = 12;

                            Excel.Range rowHeight102 = workSheet3.get_Range("A5", "I5");
                            rowHeight102.EntireRow.RowHeight = 11;

                            Excel.Range rowHeight103 = workSheet3.get_Range("A8", "I8");
                            rowHeight103.EntireRow.RowHeight = 11;

                            Excel.Range rowHeight104 = workSheet3.get_Range("A12", "I12");
                            rowHeight104.EntireRow.RowHeight = 11;

                            Excel.Range rowHeight105 = workSheet3.get_Range("A42", "I42");
                            rowHeight105.EntireRow.RowHeight = 18; //

                            Excel.Range rowHeight106 = workSheet3.get_Range("A14", "I14");
                            rowHeight106.EntireRow.RowHeight = 16;

                            Excel.Range rowHeight107 = workSheet3.get_Range("A38", "I38");
                            rowHeight107.EntireRow.RowHeight = 12;

                            Excel.Range rowHeight108 = workSheet3.get_Range("A17", "I17");
                            rowHeight108.EntireRow.RowHeight = 30;

                            range_Consolidated102.Font.Bold = true;
                            range_Consolidated102.Font.Size = 10;
                            range_Consolidated103.Font.Bold = true;
                            range_Consolidated103.Font.Size = 10;
                            range_Consolidated104.Font.Size = 8;
                            range_Consolidated105.Font.Size = 9;
                            range_Consolidated106.Font.Size = 9;
                            range_Consolidated107.Font.Size = 8;
                            range_Consolidated108.Font.Size = 8;
                            range_Consolidated109.Font.Size = 9;
                            range_Consolidated110.Font.Size = 9;
                            range_Consolidated111.Font.Size = 9;
                            range_Consolidated112.Font.Size = 8;
                            range_Consolidated113.Font.Size = 8;
                            range_Consolidated114.Font.Size = 9;
                            range_Consolidated115.Font.Size = 9;
                            range_Consolidated116.Font.Size = 9;
                            range_Consolidated117.Font.Size = 9;
                            range_Consolidated118.Font.Size = 9;
                            range_Consolidated119.Font.Size = 7;
                            range_Consolidated120.Font.Size = 8.5;
                            range_Consolidated121.Font.Size = 9;
                            range_Consolidated121.Font.Bold = true;
                            range_Consolidated122.Font.Size = 7;
                            range_Consolidated123.Font.Bold = true;
                            range_Consolidated123.Font.Size = 8;
                            range_Consolidated124.Font.Bold = true;
                            range_Consolidated124.Font.Size = 8;
                            range_Consolidated125.Font.Size = 6;
                            range_Consolidated126.Font.Size = 8;
                            range_Consolidated126.Font.Bold = true;
                            range_Consolidated127.Font.Size = 6;
                            range_Consolidated128.Font.Size = 6;
                            range_Consolidated130.Font.Size = 6;
                            range_Consolidated129.Font.Bold = true;
                            range_Consolidated129.Font.Size = 8;
                            range_Consolidated131.Font.Size = 7;
                            range_Consolidated132.NumberFormat = "@";

                            workSheet3.Cells[1, 1] = $"ПЕРВИЧНЫЙ ТЕХНИЧЕСКИЙ АКТ № {textBox_numberAct.Text}";
                            workSheet3.Cells[2, 1] = $"ОКАЗАННЫХ УСЛУГ ПО ТЕХНИЧЕСКОМУ ОБСЛУЖИВАНИЮ СИСТЕМ РАДИОСВЯЗИ";
                            workSheet3.Cells[4, 1] = $"{textBox_city.Text}";
                            workSheet3.Cells[5, 1] = $"город";
                            workSheet3.Cells[5, 8] = $"дата";
                            workSheet3.Cells[6, 1] = $"Мы, нижеподписавшиеся, представитель Исполнителя:";
                            workSheet3.Cells[7, 1] = $"Начальник участка по техническому обслуживанию и ремонту систем радиосвязи:            {label_FIO_chief.Text}";
                            workSheet3.Cells[8, 2] = $"должность";
                            workSheet3.Cells[8, 8] = $"фамилия, инициалы";
                            workSheet3.Cells[9, 1] = $"действующий по доверенности № {label_doverennost.Text} с одной стороны и представитель Заказчика";
                            workSheet3.Cells[10, 1] = $"(эксплуатирующей организации):             {textBox_company.Text}             {label_polinon_full.Text} (полигон {comboBox_poligon.Text})";
                            workSheet3.Cells[11, 1] = $"{textBox_post.Text}";
                            workSheet3.Cells[11, 7] = $"{textBox_representative.Text}";
                            workSheet3.Cells[12, 1] = $"должность";
                            workSheet3.Cells[12, 7] = $"фамилия, инициалы";
                            workSheet3.Cells[13, 1] = $"служебное удостоверение №";
                            workSheet3.Cells[13, 4] = $"{textBox_numberIdentification.Text}";
                            workSheet3.Cells[13, 6] = $"дата выдачи:";
                            workSheet3.Cells[13, 8] = $"{textBox_dateIssue.Text} г.";
                            workSheet3.Cells[14, 1] = $"с другой стороны составили настоящий акт в том, что во исполнение договора № 4176190 от 07 декабря 2020 г.,";
                            workSheet3.Cells[15, 1] = $"были  оказаны  услуги по техническому обслуживанию систем радиосвязи:";
                            workSheet3.Cells[16, 1] = $"Заводские номера  и марки портативных (носимых)  систем радиосвязи:";
                            workSheet3.Cells[17, 1] = $"№";
                            workSheet3.Cells[17, 2] = $"Тип РЭС";
                            workSheet3.Cells[17, 4] = $"Заводской № РЭС";
                            workSheet3.Cells[17, 7] = $"Место нахождения РЭС";
                            workSheet3.Cells[38, 1] = $"Вышеперечисленные носимые радиостанции участвуют в технологических процессах, требующих режима немедленной связи, и включены";
                            workSheet3.Cells[39, 1] = $"в перечень носимых радиостанций по данному структурному подразделению подлежащих периодической проверке. Системы радиосвязи исправны,";
                            workSheet3.Cells[40, 1] = $"технические характеристики вышеперечисленных систем радиосвязи после проведенного технического обслуживания соответствуют нормам.";
                            workSheet3.Cells[41, 1] = $"Представитель эксплуатирующей организации по качеству оказанных услуг претензий к Исполнителю не имеет.";
                            workSheet3.Cells[42, 1] = $"Представитель Заказчика";
                            workSheet3.Cells[42, 6] = $"Представитель Исполнителя: ";
                            workSheet3.Cells[43, 1] = $"(эксплуатирующей организации):";
                            workSheet3.Cells[44, 8] = $"{label_FIO_chief.Text}";
                            workSheet3.Cells[45, 3] = $"{textBox_representative.Text}";
                            workSheet3.Cells[45, 6] = $"подпись";
                            workSheet3.Cells[45, 8] = $"расшифровка  подписи";
                            workSheet3.Cells[46, 1] = $"подпись";
                            workSheet3.Cells[46, 3] = $"расшифровка  подписи";
                            workSheet3.Cells[46, 7] = $"МП";
                            workSheet3.Cells[47, 1] = $"Представитель РЦС:";
                            workSheet3.Cells[50, 1] = $"подпись";
                            workSheet3.Cells[50, 3] = $"расшифровка  подписи";
                            workSheet3.Cells[51, 4] = $"МП";

                            int s2 = 1;
                            int j2 = 18;

                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                workSheet3.Cells[17 + s2, 1] = s2;

                                Excel.Range _excelCells128 = (Excel.Range)workSheet3.get_Range($"B{j2}", $"C{j2}").Cells;
                                _excelCells128.Merge(Type.Missing);
                                _excelCells128.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet3.Cells[17 + s2, 2] = dataGridView1.Rows[i].Cells["model"].Value.ToString();

                                Excel.Range _excelCells129 = (Excel.Range)workSheet3.get_Range($"D{j2}", $"F{j2}").Cells;
                                _excelCells129.Merge(Type.Missing);
                                _excelCells129.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet3.Cells[17 + s2, 4] = dataGridView1.Rows[i].Cells["serialNumber"].Value.ToString();

                                Excel.Range _excelCells130 = (Excel.Range)workSheet3.get_Range($"G{j2}", $"I{j2}").Cells;
                                _excelCells130.Merge(Type.Missing);
                                _excelCells130.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet3.Cells[17 + s2, 7] = dataGridView1.Rows[i].Cells["location"].Value.ToString();

                                s2++;
                                j2++;
                            }

                            while (s2 <= 20)
                            {
                                workSheet3.Cells[17 + s2, 1] = s2;

                                Excel.Range _excelCells128 = (Excel.Range)workSheet3.get_Range($"B{j2}", $"C{j2}").Cells;
                                _excelCells128.Merge(Type.Missing);
                                _excelCells128.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells129 = (Excel.Range)workSheet3.get_Range($"D{j2}", $"F{j2}").Cells;
                                _excelCells129.Merge(Type.Missing);
                                _excelCells129.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells130 = (Excel.Range)workSheet3.get_Range($"G{j2}", $"I{j2}").Cells;
                                _excelCells130.Merge(Type.Missing);
                                _excelCells130.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                s2++;
                                j2++;
                            }
                            #endregion

                            var file = $"{textBox_numberAct.Text.Replace('/', '.')}-{textBox_company.Text}_Акт.xlsx";

                            if (!File.Exists($@"С:\Documents_ServiceTelekom\Акты ТО\{textBox_city.Text}\"))
                            {
                                try
                                {
                                    Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\Акты ТО\{textBox_city.Text}\");

                                    workSheet3.SaveAs($@"C:\Documents_ServiceTelekom\Акты ТО\{textBox_city.Text}\" + file);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    MessageBox.Show("Не удаётся сохранить файл.");
                                }
                            }
                            else
                            {
                                try
                                {
                                    workSheet3.SaveAs($@"C:\Documents_ServiceTelekom\Акты ТО\{textBox_city.Text}\" + file);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    MessageBox.Show("Не удаётся сохранить файл.");
                                }
                            }
                            exApp.Visible = true;
                        }
                    }
                }
                else
                {
                    string Mesage2;

                    Mesage2 = "Выберете акт, который хотите напечатать!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                if (exApp != null)
                {
                    exApp = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Environment.Exit(0);
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// АКТ Ремонта => excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_Continue_remont_act_excel_Click(object sender, EventArgs e)
        {
            Excel.Application exApp = new Excel.Application();

            try
            {
                if (textBox_Full_name_company.Text != "" && textBox_OKPO_remont.Text != "" && textBox_BE_remont.Text != ""
                                && textBox_director_FIO_remont_company.Text != "" && textBox_director_post_remont_company.Text != ""
                                && textBox_chairman_FIO_remont_company.Text != "" && textBox_chairman_post_remont_company.Text != ""
                                && textBox_1_FIO_remont_company.Text != "" && textBox_1_post_remont_company.Text != ""
                                && textBox_2_FIO_remont_company.Text != "" && textBox_2_post_remont_company.Text != "")
                {
                    panel_remont_information_company.Visible = false;
                    panel_remont_information_company.Enabled = false;

                    Type officeType = Type.GetTypeFromProgID("Excel.Application");

                    if (officeType == null)
                    {
                        string Mesage2 = "У Вас не установлен Excel!";

                        if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {

                        exApp.SheetsInNewWorkbook = 2;

                        exApp.Workbooks.Add();
                        exApp.DisplayAlerts = false;

                        Excel.Worksheet workSheet = (Excel.Worksheet)exApp.Worksheets.get_Item(1);
                        Excel.Worksheet workSheet2 = (Excel.Worksheet)exApp.Worksheets.get_Item(2);

                        workSheet.Name = $"Акт ремонта №{textBox_numberActRemont.Text.Replace('/', '.')}";
                        workSheet2.Name = $"ФОУ №{textBox_numberActRemont.Text.Replace('/', '.')}";

                        #region Акт ремонта

                        workSheet.Rows.Font.Size = 11;
                        workSheet.Rows.Font.Name = "Times New Roman";



                        workSheet.PageSetup.CenterHorizontally = true;
                        //workSheet.PageSetup.CenterVertically = true;
                        workSheet.PageSetup.TopMargin = 0.0;
                        workSheet.PageSetup.BottomMargin = 0;
                        workSheet.PageSetup.LeftMargin = 0;
                        workSheet.PageSetup.RightMargin = 0;

                        workSheet.PageSetup.Zoom = 90;

                        Excel.Range _excelCells1 = (Excel.Range)workSheet.get_Range("A1", "F1").Cells;
                        Excel.Range _excelCells2 = (Excel.Range)workSheet.get_Range("G1", "I1").Cells;
                        Excel.Range _excelCells3 = (Excel.Range)workSheet.get_Range("G1").Cells;
                        Excel.Range _excelCells4 = (Excel.Range)workSheet.get_Range("A2", "I2").Cells;
                        Excel.Range _excelCells5 = (Excel.Range)workSheet.get_Range("A4", "C4").Cells;
                        Excel.Range _excelCells6 = (Excel.Range)workSheet.get_Range("G4", "I4").Cells;
                        Excel.Range _excelCells7 = (Excel.Range)workSheet.get_Range("A5", "C5").Cells;
                        Excel.Range _excelCells8 = (Excel.Range)workSheet.get_Range("G5", "I5").Cells;
                        Excel.Range _excelCells9 = (Excel.Range)workSheet.get_Range("A6", "F6").Cells;
                        Excel.Range _excelCells10 = (Excel.Range)workSheet.get_Range("A7", "D7").Cells;
                        Excel.Range _excelCells11 = (Excel.Range)workSheet.get_Range("G7", "I7").Cells;
                        Excel.Range _excelCells12 = (Excel.Range)workSheet.get_Range("A8", "D8").Cells;
                        Excel.Range _excelCells13 = (Excel.Range)workSheet.get_Range("G8", "I8").Cells;
                        Excel.Range _excelCells14 = (Excel.Range)workSheet.get_Range("A9", "I9").Cells;
                        Excel.Range _excelCells15 = (Excel.Range)workSheet.get_Range("A10", "C10").Cells;
                        Excel.Range _excelCells16 = (Excel.Range)workSheet.get_Range("D10", "E10").Cells;
                        Excel.Range _excelCells17 = (Excel.Range)workSheet.get_Range("F10", "I10").Cells;
                        Excel.Range _excelCells18 = (Excel.Range)workSheet.get_Range("A11", "E11").Cells;
                        Excel.Range _excelCells19 = (Excel.Range)workSheet.get_Range("F11", "I11").Cells;
                        Excel.Range _excelCells20 = (Excel.Range)workSheet.get_Range("A12", "E12").Cells;
                        Excel.Range _excelCells21 = (Excel.Range)workSheet.get_Range("F12", "I12").Cells;
                        Excel.Range _excelCells22 = (Excel.Range)workSheet.get_Range("A13", "B13").Cells;
                        Excel.Range _excelCells23 = (Excel.Range)workSheet.get_Range("C13", "D13").Cells;
                        Excel.Range _excelCells24 = (Excel.Range)workSheet.get_Range("E13", "G13").Cells;
                        Excel.Range _excelCells25 = (Excel.Range)workSheet.get_Range("H13", "I13").Cells;
                        Excel.Range _excelCells26 = (Excel.Range)workSheet.get_Range("A14", "I14").Cells;
                        Excel.Range _excelCells27 = (Excel.Range)workSheet.get_Range("A15").Cells;
                        Excel.Range _excelCells28 = (Excel.Range)workSheet.get_Range("B15").Cells;
                        Excel.Range _excelCells29 = (Excel.Range)workSheet.get_Range("C15", "D15").Cells;
                        Excel.Range _excelCells30 = (Excel.Range)workSheet.get_Range("E15", "F15").Cells;
                        Excel.Range _excelCells31 = (Excel.Range)workSheet.get_Range("G15", "H15").Cells;
                        Excel.Range _excelCells32 = (Excel.Range)workSheet.get_Range("I15").Cells;
                        Excel.Range _excelCells33 = (Excel.Range)workSheet.get_Range("A15", "I23").Cells;
                        Excel.Range _excelCells34 = (Excel.Range)workSheet.get_Range("A24", "I24").Cells;
                        Excel.Range _excelCells38 = (Excel.Range)workSheet.get_Range("A26", "D26").Cells;
                        Excel.Range _excelCells39 = (Excel.Range)workSheet.get_Range("E26", "F26").Cells;
                        Excel.Range _excelCells40 = (Excel.Range)workSheet.get_Range("G26", "I26").Cells;
                        Excel.Range _excelCells41 = (Excel.Range)workSheet.get_Range("A27", "D27").Cells;
                        Excel.Range _excelCells42 = (Excel.Range)workSheet.get_Range("E27", "F27").Cells;
                        Excel.Range _excelCells43 = (Excel.Range)workSheet.get_Range("G27", "I27").Cells;
                        Excel.Range _excelCells44 = (Excel.Range)workSheet.get_Range("A28", "E28").Cells;
                        Excel.Range _excelCells45 = (Excel.Range)workSheet.get_Range("F28", "I28").Cells;
                        Excel.Range _excelCells46 = (Excel.Range)workSheet.get_Range("A30", "I30").Cells;
                        Excel.Range _excelCells47 = (Excel.Range)workSheet.get_Range("C30", "D30").Cells;
                        Excel.Range _excelCells48 = (Excel.Range)workSheet.get_Range("H30", "I30").Cells;
                        Excel.Range _excelCells49 = (Excel.Range)workSheet.get_Range("A31", "B31").Cells;
                        Excel.Range _excelCells50 = (Excel.Range)workSheet.get_Range("A31", "I31").Cells;
                        Excel.Range _excelCells51 = (Excel.Range)workSheet.get_Range("C31", "D31").Cells;
                        Excel.Range _excelCells52 = (Excel.Range)workSheet.get_Range("F31", "G31").Cells;
                        Excel.Range _excelCells53 = (Excel.Range)workSheet.get_Range("H31", "I31").Cells;
                        Excel.Range _excelCells54 = (Excel.Range)workSheet.get_Range("A32", "E32").Cells;
                        Excel.Range _excelCells55 = (Excel.Range)workSheet.get_Range("A34", "D34").Cells;
                        Excel.Range _excelCells56 = (Excel.Range)workSheet.get_Range("A35", "D35").Cells;
                        Excel.Range _excelCells57 = (Excel.Range)workSheet.get_Range("A35", "B35").Cells;
                        Excel.Range _excelCells58 = (Excel.Range)workSheet.get_Range("C35", "D35").Cells;
                        Excel.Range _excelCells59 = (Excel.Range)workSheet.get_Range("C36", "D36").Cells;
                        Excel.Range _excelCells60 = (Excel.Range)workSheet.get_Range("F32", "G32").Cells;

                        int xy = 16;
                        int xz = 16;
                        for (int i = 0; i < 8; i++)
                        {
                            Excel.Range _excelCells35 = (Excel.Range)workSheet.get_Range($"C{xy}", $"D{xy}").Cells;
                            Excel.Range _excelCells36 = (Excel.Range)workSheet.get_Range($"E{xz}", $"F{xz}").Cells;
                            Excel.Range _excelCells37 = (Excel.Range)workSheet.get_Range($"G{xz}", $"H{xz}").Cells;

                            _excelCells35.Merge(Type.Missing);
                            _excelCells36.Merge(Type.Missing);
                            _excelCells37.Merge(Type.Missing);

                            xy++;
                            xz++;
                        }

                        _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDash;
                        _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDash;
                        _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDash;
                        _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDash;
                        _excelCells33.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlDash;
                        _excelCells33.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlDash;

                        _excelCells1.Merge(Type.Missing);
                        _excelCells2.Merge(Type.Missing);
                        _excelCells4.Merge(Type.Missing);
                        _excelCells5.Merge(Type.Missing);
                        _excelCells6.Merge(Type.Missing);
                        _excelCells7.Merge(Type.Missing);
                        _excelCells8.Merge(Type.Missing);
                        _excelCells9.Merge(Type.Missing);
                        _excelCells10.Merge(Type.Missing);
                        _excelCells11.Merge(Type.Missing);
                        _excelCells12.Merge(Type.Missing);
                        _excelCells13.Merge(Type.Missing);
                        _excelCells14.Merge(Type.Missing);
                        _excelCells15.Merge(Type.Missing);
                        _excelCells16.Merge(Type.Missing);
                        _excelCells17.Merge(Type.Missing);
                        _excelCells18.Merge(Type.Missing);
                        _excelCells19.Merge(Type.Missing);
                        _excelCells20.Merge(Type.Missing);
                        _excelCells21.Merge(Type.Missing);
                        _excelCells22.Merge(Type.Missing);
                        _excelCells23.Merge(Type.Missing);
                        _excelCells24.Merge(Type.Missing);
                        _excelCells25.Merge(Type.Missing);
                        _excelCells26.Merge(Type.Missing);
                        _excelCells29.Merge(Type.Missing);
                        _excelCells30.Merge(Type.Missing);
                        _excelCells31.Merge(Type.Missing);
                        _excelCells34.Merge(Type.Missing);
                        _excelCells38.Merge(Type.Missing);
                        _excelCells39.Merge(Type.Missing);
                        _excelCells40.Merge(Type.Missing);
                        _excelCells41.Merge(Type.Missing);
                        _excelCells42.Merge(Type.Missing);
                        _excelCells43.Merge(Type.Missing);
                        _excelCells44.Merge(Type.Missing);
                        _excelCells45.Merge(Type.Missing);
                        _excelCells47.Merge(Type.Missing);
                        _excelCells48.Merge(Type.Missing);
                        _excelCells49.Merge(Type.Missing);
                        _excelCells51.Merge(Type.Missing);
                        _excelCells52.Merge(Type.Missing);
                        _excelCells53.Merge(Type.Missing);
                        _excelCells54.Merge(Type.Missing);
                        _excelCells57.Merge(Type.Missing);
                        _excelCells58.Merge(Type.Missing);
                        _excelCells59.Merge(Type.Missing);
                        _excelCells60.Merge(Type.Missing);

                        _excelCells1.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        _excelCells2.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells4.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells5.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells6.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells7.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells8.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells9.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells10.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells11.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells12.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells13.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells14.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells15.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells16.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells17.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells18.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells19.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells20.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells21.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells22.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        _excelCells23.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells24.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells25.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells26.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells27.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells27.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells28.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells28.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells29.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells29.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells30.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells30.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells31.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells31.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells32.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells32.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells32.Orientation = 90;

                        _excelCells33.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells33.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells34.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells38.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells39.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells40.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells41.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells42.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells43.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells44.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells45.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells46.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells50.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells54.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells56.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells59.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells60.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                        _excelCells3.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells5.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells6.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells10.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells11.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells15.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells16.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells17.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells18.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells19.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells23.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells25.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells38.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells39.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells40.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells46.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells55.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                        Excel.Range range_Consolidated = workSheet.Rows.get_Range("A1", "I2");
                        Excel.Range range_Consolidated2 = workSheet.Rows.get_Range("A4", "I4");
                        Excel.Range range_Consolidated3 = workSheet.Rows.get_Range("A5", "I5");
                        Excel.Range range_Consolidated4 = workSheet.Rows.get_Range("A8", "I8");
                        Excel.Range range_Consolidated5 = workSheet.Rows.get_Range("A12", "I12");
                        Excel.Range range_Consolidated6 = workSheet.Rows.get_Range("A15", "I15");
                        Excel.Range range_Consolidated7 = workSheet.Rows.get_Range("A16", "I23");
                        Excel.Range range_Consolidated8 = workSheet.Rows.get_Range("A27", "I27");
                        Excel.Range range_Consolidated9 = workSheet.Rows.get_Range("A28", "I28");
                        Excel.Range range_Consolidated10 = workSheet.Rows.get_Range("A31", "I31");
                        Excel.Range range_Consolidated11 = workSheet.Rows.get_Range("A32", "E33");
                        Excel.Range range_Consolidated12 = workSheet.Rows.get_Range("A35", "I36");
                        Excel.Range range_Consolidated13 = workSheet.Rows.get_Range("F32", "G32");
                        Excel.Range range_Consolidated14 = workSheet.Rows.get_Range("C17", "D17");
                        Excel.Range range_Consolidated15 = workSheet.Rows.get_Range("C18", "D18");
                        Excel.Range range_Consolidated16 = workSheet.Rows.get_Range("C16", "D16");

                        range_Consolidated.Font.Bold = true;
                        range_Consolidated.Font.Size = 14;
                        range_Consolidated2.Font.Bold = true;
                        range_Consolidated2.Font.Size = 12;
                        range_Consolidated3.Font.Size = 8;
                        range_Consolidated4.Font.Size = 8;
                        range_Consolidated5.Font.Size = 8;
                        range_Consolidated6.Font.Size = 11;
                        range_Consolidated7.Font.Size = 8.5;
                        range_Consolidated8.Font.Size = 8;
                        range_Consolidated9.Font.Bold = true;
                        range_Consolidated10.Font.Size = 8;
                        range_Consolidated11.Font.Bold = true;
                        range_Consolidated12.Font.Size = 8;
                        range_Consolidated13.Font.Size = 8;
                        range_Consolidated14.NumberFormat = "@";
                        range_Consolidated15.NumberFormat = "@";
                        range_Consolidated16.NumberFormat = "@";

                        Excel.Range rowHeight = workSheet.get_Range("A5", "I5");
                        rowHeight.EntireRow.RowHeight = 12;

                        Excel.Range rowHeight2 = workSheet.get_Range("A7", "I7");
                        rowHeight2.EntireRow.RowHeight = 20;

                        Excel.Range rowHeight3 = workSheet.get_Range("A8", "I8");
                        rowHeight3.EntireRow.RowHeight = 12;

                        Excel.Range rowHeight4 = workSheet.get_Range("A12", "I12");
                        rowHeight4.EntireRow.RowHeight = 12;

                        Excel.Range rowHeight5 = workSheet.get_Range("A14", "I14");
                        rowHeight5.EntireRow.RowHeight = 45;

                        Excel.Range rowHeight6 = workSheet.get_Range("A15", "I15");
                        rowHeight6.EntireRow.RowHeight = 65;

                        Excel.Range rowColum7 = workSheet.get_Range("A15");
                        rowColum7.EntireColumn.ColumnWidth = 10;
                        Excel.Range rowColum8 = workSheet.get_Range("B15");
                        rowColum8.EntireColumn.ColumnWidth = 14;
                        Excel.Range rowColum9 = workSheet.get_Range("C15", "D15");
                        rowColum9.EntireColumn.ColumnWidth = 11;
                        Excel.Range rowColum10 = workSheet.get_Range("E15", "F15");
                        rowColum10.EntireColumn.ColumnWidth = 10;
                        Excel.Range rowColum11 = workSheet.get_Range("G15", "H15");
                        rowColum11.EntireColumn.ColumnWidth = 9;

                        Excel.Range rowHeight12 = workSheet.get_Range("A16", "I16");
                        rowHeight12.EntireRow.RowHeight = 40;
                        Excel.Range rowHeight13 = workSheet.get_Range("A17", "I17");
                        rowHeight13.EntireRow.RowHeight = 40;
                        Excel.Range rowHeight14 = workSheet.get_Range("A18", "I18");
                        rowHeight14.EntireRow.RowHeight = 40;
                        Excel.Range rowHeight15 = workSheet.get_Range("A19", "I19");
                        rowHeight15.EntireRow.RowHeight = 40;
                        Excel.Range rowHeight16 = workSheet.get_Range("A20", "I20");
                        rowHeight16.EntireRow.RowHeight = 40;
                        Excel.Range rowHeight17 = workSheet.get_Range("A21", "I21");
                        rowHeight17.EntireRow.RowHeight = 40;
                        Excel.Range rowHeight18 = workSheet.get_Range("A22", "I22");
                        rowHeight18.EntireRow.RowHeight = 40;
                        Excel.Range rowHeight19 = workSheet.get_Range("A23", "I23");
                        rowHeight19.EntireRow.RowHeight = 40;

                        Excel.Range rowHeight20 = workSheet.get_Range("A24", "I24");
                        rowHeight20.EntireRow.RowHeight = 35;

                        Excel.Range rowHeight21 = workSheet.get_Range("A27", "I27");
                        rowHeight21.EntireRow.RowHeight = 12;
                        Excel.Range rowHeight22 = workSheet.get_Range("A31", "I31");
                        rowHeight22.EntireRow.RowHeight = 12;
                        Excel.Range rowHeight23 = workSheet.get_Range("A35", "I35");
                        rowHeight23.EntireRow.RowHeight = 12;


                        workSheet.Cells[1, 1] = $"ПЕРВИЧНЫЙ ТЕХНИЧЕСКИЙ АКТ №";
                        workSheet.Cells[1, 7] = $"{textBox_numberActRemont.Text}";
                        workSheet.Cells[2, 1] = $"выполненных работ по ремонту систем радиосвязи";
                        workSheet.Cells[4, 1] = $"{textBox_city.Text}";
                        workSheet.Cells[5, 1] = $"город";
                        workSheet.Cells[5, 7] = $"дата";
                        workSheet.Cells[6, 1] = $"Мы, нижеподписавшиеся, представитель Исполнителя :";
                        workSheet.Cells[7, 1] = $"Начальник участка по ТО и ремонту СРС";
                        workSheet.Cells[7, 7] = $"{label_FIO_chief.Text}";
                        workSheet.Cells[8, 1] = $"должность";
                        workSheet.Cells[8, 7] = $"фамилия, инициалы";
                        workSheet.Cells[9, 1] = $"действующий по доверенности № {label_doverennost.Text} с одной стороны и представитель";
                        workSheet.Cells[10, 1] = $"эксплуатирующей организации:";
                        workSheet.Cells[10, 4] = $"{textBox_company.Text}";
                        workSheet.Cells[10, 6] = $"{label_polinon_full.Text} (полигон {comboBox_poligon.Text})";
                        workSheet.Cells[11, 1] = $"{textBox_post.Text}";
                        workSheet.Cells[11, 6] = $"{textBox_representative.Text}";
                        workSheet.Cells[12, 1] = $"должность";
                        workSheet.Cells[12, 6] = $"фамилия, инициалы";
                        workSheet.Cells[13, 1] = $"дата выдачи:";
                        workSheet.Cells[13, 3] = $"{textBox_dateIssue.Text}";
                        workSheet.Cells[13, 5] = $"служебное удостоверение №:";
                        workSheet.Cells[13, 8] = $"{textBox_numberIdentification.Text}";
                        workSheet.Cells[14, 1] = $"с другой стороны составили настоящий акт в том, что во исполнение договора № 4176190 от 07 декабря 2020 г.,\n" +
                                                 $"были выполнены работы по ремонту систем радиосвязи и использованы заменяемые детали и расходные\n" +
                                                 $"материалы в количестве:";
                        workSheet.Cells[15, 1] = $"Категория\nсложности\nремонтных работ";
                        workSheet.Cells[15, 2] = $"Модель";
                        workSheet.Cells[15, 3] = $"Учетный номер\nрадиостанции";
                        workSheet.Cells[15, 5] = $"Выполненные работы";
                        workSheet.Cells[15, 7] = $"Израсходованные\nматериалы и детали";
                        workSheet.Cells[15, 9] = $"Кол-во\n(шт.)";
                        workSheet.Cells[16, 1] = $"{comboBox_сategory.Text}";
                        workSheet.Cells[16, 2] = $"{comboBox_model.Text}";
                        workSheet.Cells[16, 3] = $"{textBox_serialNumber.Text}";
                        workSheet.Cells[17, 3] = $"инв№ {textBox_inventoryNumber.Text}";
                        workSheet.Cells[18, 3] = $"сет№ {textBox_networkNumber.Text}";
                        workSheet.Cells[17, 5] = $"\n{textBox_сompleted_works_1.Text}\n";
                        workSheet.Cells[17, 7] = $"\n{textBox_parts_1.Text}\n";
                        if (textBox_сompleted_works_1.Text != "" || textBox_parts_1.Text != "")
                        {
                            workSheet.Cells[17, 9] = $"1";
                        }
                        workSheet.Cells[18, 5] = $"\n{textBox_сompleted_works_2.Text}\n";
                        workSheet.Cells[18, 7] = $"\n{textBox_parts_2.Text}\n";
                        if (textBox_сompleted_works_2.Text != "" || textBox_parts_2.Text != "")
                        {
                            workSheet.Cells[18, 9] = $"1";
                        }
                        workSheet.Cells[19, 5] = $"\n{textBox_сompleted_works_3.Text}\n";
                        workSheet.Cells[19, 7] = $"\n{textBox_parts_3.Text}\n";
                        if (textBox_сompleted_works_3.Text != "" || textBox_parts_3.Text != "")
                        {
                            workSheet.Cells[19, 9] = $"1";
                        }
                        workSheet.Cells[20, 5] = $"\n{textBox_сompleted_works_4.Text}\n";
                        workSheet.Cells[20, 7] = $"\n{textBox_parts_4.Text}\n";
                        if (textBox_сompleted_works_4.Text != "" || textBox_parts_4.Text != "")
                        {
                            workSheet.Cells[20, 9] = $"1";
                        }
                        workSheet.Cells[21, 5] = $"\n{textBox_сompleted_works_5.Text}\n";
                        workSheet.Cells[21, 7] = $"\n{textBox_parts_5.Text}\n";
                        if (textBox_сompleted_works_5.Text != "" || textBox_parts_5.Text != "")
                        {
                            workSheet.Cells[21, 9] = $"1";
                        }
                        workSheet.Cells[22, 5] = $"\n{textBox_сompleted_works_6.Text}\n";
                        workSheet.Cells[22, 7] = $"\n{textBox_parts_6.Text}\n";
                        if (textBox_сompleted_works_6.Text != "" || textBox_parts_6.Text != "")
                        {
                            workSheet.Cells[22, 9] = $"1";
                        }
                        workSheet.Cells[23, 5] = $"\n{textBox_сompleted_works_7.Text}\n";
                        workSheet.Cells[23, 7] = $"\n{textBox_parts_7.Text}\n";
                        if (textBox_сompleted_works_7.Text != "" || textBox_parts_7.Text != "")
                        {
                            workSheet.Cells[23, 9] = $"1";
                        }
                        workSheet.Cells[24, 1] = $"Системы радиосвязи работоспособны, технические характеристики вышеперечисленных соответствуют нормам.\n" +
                                                 $"Представитель эксплуатирующей организации по качеству выполненных работ претензий к исполнителю не имеет.";

                        workSheet.Cells[26, 1] = $"Исполнитель работ: инженер по ТО и ремонту СРС";
                        workSheet.Cells[26, 7] = $"{label_FIO_Engineer.Text}";
                        workSheet.Cells[27, 1] = $"должность";
                        workSheet.Cells[27, 5] = $"подпись";
                        workSheet.Cells[27, 7] = $"расшифровка подписи";
                        workSheet.Cells[28, 1] = $"Представитель Заказчика (зксплуатирующей организации):";
                        workSheet.Cells[28, 6] = $"Представитель Исполнителя:";
                        workSheet.Cells[30, 3] = $"{textBox_representative.Text}";
                        workSheet.Cells[30, 8] = $"{label_FIO_chief.Text}";
                        workSheet.Cells[31, 1] = $"подпись";
                        workSheet.Cells[31, 3] = $"расшифровка подписи";
                        workSheet.Cells[31, 6] = $"подпись";
                        workSheet.Cells[31, 8] = $"расшифровка подписи";
                        workSheet.Cells[32, 1] = $"Представитель РЦС:";
                        workSheet.Cells[35, 1] = $"подпись";
                        workSheet.Cells[35, 3] = $"расшифровка подписи";
                        workSheet.Cells[36, 3] = $"М.П.";
                        workSheet.Cells[32, 6] = $"М.П.";

                        #endregion

                        #region ФОУ-18

                        workSheet2.Rows.Font.Size = 9;
                        workSheet2.Rows.Font.Name = "Times New Roman";

                        workSheet2.PageSetup.CenterHorizontally = true;
                        workSheet2.PageSetup.TopMargin = 0;
                        workSheet2.PageSetup.BottomMargin = 0;
                        workSheet2.PageSetup.LeftMargin = 0;
                        workSheet2.PageSetup.RightMargin = 0;

                        workSheet2.PageSetup.Zoom = 90;

                        Excel.Range _excelCells100 = (Excel.Range)workSheet2.get_Range("G1", "K1").Cells;
                        Excel.Range _excelCells101 = (Excel.Range)workSheet2.get_Range("F2", "K2").Cells;
                        Excel.Range _excelCells102 = (Excel.Range)workSheet2.get_Range("I5", "J5").Cells;
                        Excel.Range _excelCells103 = (Excel.Range)workSheet2.get_Range("J6", "J7").Cells;
                        Excel.Range _excelCells104 = (Excel.Range)workSheet2.get_Range("J8", "J9").Cells;
                        Excel.Range _excelCells105 = (Excel.Range)workSheet2.get_Range("K4", "K9").Cells;
                        Excel.Range _excelCells106 = (Excel.Range)workSheet2.get_Range("K6", "K7").Cells;
                        Excel.Range _excelCells107 = (Excel.Range)workSheet2.get_Range("K8", "K9").Cells;
                        Excel.Range _excelCells108 = (Excel.Range)workSheet2.get_Range("K5", "K9").Cells;
                        Excel.Range _excelCells109 = (Excel.Range)workSheet2.get_Range("A7", "I7").Cells;
                        Excel.Range _excelCells110 = (Excel.Range)workSheet2.get_Range("A7", "I10").Cells;
                        Excel.Range _excelCells111 = (Excel.Range)workSheet2.get_Range("A8", "I8").Cells;
                        Excel.Range _excelCells112 = (Excel.Range)workSheet2.get_Range("A9", "I9").Cells;
                        Excel.Range _excelCells113 = (Excel.Range)workSheet2.get_Range("A10", "I10").Cells;
                        Excel.Range _excelCells114 = (Excel.Range)workSheet2.get_Range("J11", "K12").Cells;
                        Excel.Range _excelCells115 = (Excel.Range)workSheet2.get_Range("H12", "I12").Cells;
                        Excel.Range _excelCells116 = (Excel.Range)workSheet2.get_Range("J13", "K13").Cells;
                        Excel.Range _excelCells117 = (Excel.Range)workSheet2.get_Range("J14", "K14").Cells;
                        Excel.Range _excelCells118 = (Excel.Range)workSheet2.get_Range("I14", "K14").Cells;
                        Excel.Range _excelCells119 = (Excel.Range)workSheet2.get_Range("J15", "K15").Cells;
                        Excel.Range _excelCells120 = (Excel.Range)workSheet2.get_Range("D17", "H17").Cells;
                        Excel.Range _excelCells121 = (Excel.Range)workSheet2.get_Range("D18", "F18").Cells;
                        Excel.Range _excelCells122 = (Excel.Range)workSheet2.get_Range("G18", "H18").Cells;
                        Excel.Range _excelCells123 = (Excel.Range)workSheet2.get_Range("D18", "H19").Cells;
                        Excel.Range _excelCells124 = (Excel.Range)workSheet2.get_Range("D19", "F19").Cells;
                        Excel.Range _excelCells125 = (Excel.Range)workSheet2.get_Range("G19", "H19").Cells;
                        Excel.Range _excelCells126 = (Excel.Range)workSheet2.get_Range("A21", "D21").Cells;
                        Excel.Range _excelCells127 = (Excel.Range)workSheet2.get_Range("E21", "G21").Cells;
                        Excel.Range _excelCells128 = (Excel.Range)workSheet2.get_Range("H21", "K21").Cells;
                        Excel.Range _excelCells129 = (Excel.Range)workSheet2.get_Range("A22", "B22").Cells;
                        Excel.Range _excelCells130 = (Excel.Range)workSheet2.get_Range("C22", "E22").Cells;
                        Excel.Range _excelCells131 = (Excel.Range)workSheet2.get_Range("F22", "G22").Cells;
                        Excel.Range _excelCells132 = (Excel.Range)workSheet2.get_Range("H22", "K22").Cells;
                        Excel.Range _excelCells133 = (Excel.Range)workSheet2.get_Range("A23", "C23").Cells;
                        Excel.Range _excelCells134 = (Excel.Range)workSheet2.get_Range("D23", "G23").Cells;
                        Excel.Range _excelCells135 = (Excel.Range)workSheet2.get_Range("A24", "B24").Cells;
                        Excel.Range _excelCells136 = (Excel.Range)workSheet2.get_Range("C24", "K24").Cells;
                        Excel.Range _excelCells137 = (Excel.Range)workSheet2.get_Range("A25", "K25").Cells;
                        Excel.Range _excelCells138 = (Excel.Range)workSheet2.get_Range("C25", "G25").Cells;
                        Excel.Range _excelCells139 = (Excel.Range)workSheet2.get_Range("A26", "H26").Cells;
                        Excel.Range _excelCells140 = (Excel.Range)workSheet2.get_Range("I26", "K26").Cells;
                        Excel.Range _excelCells141 = (Excel.Range)workSheet2.get_Range("I27", "K27").Cells;
                        Excel.Range _excelCells143 = (Excel.Range)workSheet2.get_Range("D28", "K28").Cells;
                        Excel.Range _excelCells144 = (Excel.Range)workSheet2.get_Range("B29").Cells;
                        Excel.Range _excelCells145 = (Excel.Range)workSheet2.get_Range("C29", "K29").Cells;
                        Excel.Range _excelCells146 = (Excel.Range)workSheet2.get_Range("A28", "A36").Cells;
                        Excel.Range _excelCells148 = (Excel.Range)workSheet2.get_Range("F30", "K30").Cells;
                        Excel.Range _excelCells149 = (Excel.Range)workSheet2.get_Range("B33", "K33").Cells;
                        Excel.Range _excelCells150 = (Excel.Range)workSheet2.get_Range("B34", "K34").Cells;
                        Excel.Range _excelCells152 = (Excel.Range)workSheet2.get_Range("G35", "K35").Cells;
                        Excel.Range _excelCells153 = (Excel.Range)workSheet2.get_Range("B36", "K36").Cells;
                        Excel.Range _excelCells154 = (Excel.Range)workSheet2.get_Range("A38", "K38").Cells;
                        Excel.Range _excelCells155 = (Excel.Range)workSheet2.get_Range("B38", "C38").Cells;
                        Excel.Range _excelCells156 = (Excel.Range)workSheet2.get_Range("D38", "E38").Cells;
                        Excel.Range _excelCells157 = (Excel.Range)workSheet2.get_Range("A38", "K46").Cells;
                        Excel.Range _excelCells158 = (Excel.Range)workSheet2.get_Range("A40", "K46").Cells;
                        Excel.Range _excelCells161 = (Excel.Range)workSheet2.get_Range("B49", "D49").Cells;
                        Excel.Range _excelCells162 = (Excel.Range)workSheet2.get_Range("I49", "J49").Cells;
                        Excel.Range _excelCells163 = (Excel.Range)workSheet2.get_Range("B50", "D50").Cells;
                        Excel.Range _excelCells164 = (Excel.Range)workSheet2.get_Range("F50", "G50").Cells;
                        Excel.Range _excelCells165 = (Excel.Range)workSheet2.get_Range("I50", "J50").Cells;
                        Excel.Range _excelCells166 = (Excel.Range)workSheet2.get_Range("F49", "G49").Cells;
                        Excel.Range _excelCells167 = (Excel.Range)workSheet2.get_Range("B52", "D52").Cells;
                        Excel.Range _excelCells168 = (Excel.Range)workSheet2.get_Range("F52", "G52").Cells;
                        Excel.Range _excelCells169 = (Excel.Range)workSheet2.get_Range("I52", "J52").Cells;
                        Excel.Range _excelCells170 = (Excel.Range)workSheet2.get_Range("B53", "D53").Cells;
                        Excel.Range _excelCells171 = (Excel.Range)workSheet2.get_Range("F53", "G53").Cells;
                        Excel.Range _excelCells172 = (Excel.Range)workSheet2.get_Range("I53", "J53").Cells;
                        Excel.Range _excelCells173 = (Excel.Range)workSheet2.get_Range("B55", "D55").Cells;
                        Excel.Range _excelCells174 = (Excel.Range)workSheet2.get_Range("F55", "G55").Cells;
                        Excel.Range _excelCells175 = (Excel.Range)workSheet2.get_Range("I55", "J55").Cells;
                        Excel.Range _excelCells176 = (Excel.Range)workSheet2.get_Range("B56", "D56").Cells;
                        Excel.Range _excelCells177 = (Excel.Range)workSheet2.get_Range("F56", "G56").Cells;
                        Excel.Range _excelCells178 = (Excel.Range)workSheet2.get_Range("I56", "J56").Cells;
                        Excel.Range _excelCells179 = (Excel.Range)workSheet2.get_Range("B58", "D58").Cells;
                        Excel.Range _excelCells180 = (Excel.Range)workSheet2.get_Range("F58", "G58").Cells;
                        Excel.Range _excelCells181 = (Excel.Range)workSheet2.get_Range("I58", "J58").Cells;
                        Excel.Range _excelCells182 = (Excel.Range)workSheet2.get_Range("B59", "D59").Cells;
                        Excel.Range _excelCells183 = (Excel.Range)workSheet2.get_Range("F59", "G59").Cells;
                        Excel.Range _excelCells184 = (Excel.Range)workSheet2.get_Range("I59", "J59").Cells;
                        Excel.Range _excelCells185 = (Excel.Range)workSheet2.get_Range("A63", "D63").Cells;
                        Excel.Range _excelCells186 = (Excel.Range)workSheet2.get_Range("F62", "G62").Cells;
                        Excel.Range _excelCells187 = (Excel.Range)workSheet2.get_Range("I62", "J62").Cells;
                        Excel.Range _excelCells188 = (Excel.Range)workSheet2.get_Range("F63", "G63").Cells;
                        Excel.Range _excelCells189 = (Excel.Range)workSheet2.get_Range("I63", "J63").Cells;



                        int z1 = 39;
                        int z2 = 39;
                        for (int i = 0; i < 8; i++)
                        {
                            Excel.Range _excelCells159 = (Excel.Range)workSheet2.get_Range($"B{z1}", $"C{z1}").Cells;
                            Excel.Range _excelCells160 = (Excel.Range)workSheet2.get_Range($"D{z2}", $"E{z2}").Cells;
                            _excelCells159.Merge(Type.Missing);
                            _excelCells160.Merge(Type.Missing);

                            z1++;
                            z2++;
                        }

                        _excelCells108.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells108.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells108.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells108.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells108.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells108.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells109.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells112.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells114.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells118.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                        _excelCells123.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells123.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells123.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells123.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells123.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells123.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                        _excelCells127.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells128.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells130.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells132.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells134.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells136.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells137.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells143.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells145.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells148.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells149.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells150.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells152.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells153.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                        _excelCells157.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells157.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells157.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells157.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells157.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells157.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells161.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells162.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells166.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells167.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells168.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells169.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells173.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells174.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells175.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells179.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells180.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells181.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells186.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        _excelCells187.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;


                        _excelCells100.Merge(Type.Missing);
                        _excelCells101.Merge(Type.Missing);
                        _excelCells102.Merge(Type.Missing);
                        _excelCells103.Merge(Type.Missing);
                        _excelCells104.Merge(Type.Missing);
                        _excelCells106.Merge(Type.Missing);
                        _excelCells107.Merge(Type.Missing);
                        _excelCells109.Merge(Type.Missing);
                        _excelCells111.Merge(Type.Missing);
                        _excelCells112.Merge(Type.Missing);
                        _excelCells113.Merge(Type.Missing);
                        _excelCells114.Merge(Type.Missing);
                        _excelCells115.Merge(Type.Missing);
                        _excelCells116.Merge(Type.Missing);
                        _excelCells117.Merge(Type.Missing);
                        _excelCells119.Merge(Type.Missing);
                        _excelCells120.Merge(Type.Missing);
                        _excelCells121.Merge(Type.Missing);
                        _excelCells122.Merge(Type.Missing);
                        _excelCells124.Merge(Type.Missing);
                        _excelCells125.Merge(Type.Missing);
                        _excelCells126.Merge(Type.Missing);
                        _excelCells127.Merge(Type.Missing);
                        _excelCells128.Merge(Type.Missing);
                        _excelCells129.Merge(Type.Missing);
                        _excelCells130.Merge(Type.Missing);
                        _excelCells131.Merge(Type.Missing);
                        _excelCells132.Merge(Type.Missing);
                        _excelCells133.Merge(Type.Missing);
                        _excelCells134.Merge(Type.Missing);
                        _excelCells135.Merge(Type.Missing);
                        _excelCells136.Merge(Type.Missing);
                        _excelCells138.Merge(Type.Missing);
                        _excelCells139.Merge(Type.Missing);
                        _excelCells140.Merge(Type.Missing);
                        _excelCells141.Merge(Type.Missing);
                        _excelCells144.Merge(Type.Missing);
                        _excelCells155.Merge(Type.Missing);
                        _excelCells156.Merge(Type.Missing);
                        _excelCells161.Merge(Type.Missing);
                        _excelCells162.Merge(Type.Missing);
                        _excelCells163.Merge(Type.Missing);
                        _excelCells164.Merge(Type.Missing);
                        _excelCells165.Merge(Type.Missing);
                        _excelCells166.Merge(Type.Missing);
                        _excelCells167.Merge(Type.Missing);
                        _excelCells168.Merge(Type.Missing);
                        _excelCells169.Merge(Type.Missing);
                        _excelCells170.Merge(Type.Missing);
                        _excelCells171.Merge(Type.Missing);
                        _excelCells172.Merge(Type.Missing);
                        _excelCells173.Merge(Type.Missing);
                        _excelCells174.Merge(Type.Missing);
                        _excelCells175.Merge(Type.Missing);
                        _excelCells176.Merge(Type.Missing);
                        _excelCells177.Merge(Type.Missing);
                        _excelCells178.Merge(Type.Missing);
                        _excelCells179.Merge(Type.Missing);
                        _excelCells180.Merge(Type.Missing);
                        _excelCells181.Merge(Type.Missing);
                        _excelCells182.Merge(Type.Missing);
                        _excelCells183.Merge(Type.Missing);
                        _excelCells184.Merge(Type.Missing);
                        _excelCells185.Merge(Type.Missing);
                        _excelCells186.Merge(Type.Missing);
                        _excelCells187.Merge(Type.Missing);
                        _excelCells188.Merge(Type.Missing);
                        _excelCells189.Merge(Type.Missing);

                        _excelCells100.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        _excelCells101.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        _excelCells102.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        _excelCells103.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        _excelCells104.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        _excelCells105.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells109.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells110.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells112.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells112.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells114.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells114.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells115.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        _excelCells116.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells117.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells119.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells120.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells120.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells123.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells126.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells127.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells128.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells129.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells130.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells131.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells132.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells132.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells134.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells135.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells136.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells138.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells139.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells140.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells141.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells144.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        _excelCells146.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells154.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells154.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells158.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells158.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells161.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells162.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells163.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells164.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells165.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells167.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells169.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells170.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells171.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells172.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells173.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells174.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells175.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells176.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells177.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells178.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells179.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells180.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells181.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells182.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells183.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells184.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells185.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells186.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells187.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells188.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        _excelCells189.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                        Excel.Range rowHeight100 = workSheet2.get_Range("A9", "I9");
                        rowHeight100.EntireRow.RowHeight = 40;

                        Excel.Range rowHeight101 = workSheet2.get_Range("J11", "K11");
                        rowHeight101.EntireRow.RowHeight = 15;

                        Excel.Range rowHeight102 = workSheet2.get_Range("J14", "K14");
                        rowHeight102.EntireRow.RowHeight = 15;

                        Excel.Range rowHeight103 = workSheet2.get_Range("A27", "K27");
                        rowHeight103.EntireRow.RowHeight = 8;

                        Excel.Range rowColum104 = workSheet2.get_Range("A28", "A36");
                        rowColum104.EntireColumn.ColumnWidth = 5;

                        Excel.Range rowColum105 = workSheet2.get_Range("B28", "B36");
                        rowColum105.EntireColumn.ColumnWidth = 10;

                        Excel.Range rowHeight106 = workSheet2.get_Range("A38", "K38");
                        rowHeight106.EntireRow.RowHeight = 55;

                        Excel.Range rowHeight107 = workSheet2.get_Range("A37", "K37");
                        rowHeight107.EntireRow.RowHeight = 6;

                        Excel.Range rowColum108 = workSheet2.get_Range("D38", "E38");
                        rowColum108.EntireColumn.ColumnWidth = 6;

                        Excel.Range rowColum109 = workSheet2.get_Range("F38");
                        rowColum109.EntireColumn.ColumnWidth = 14;

                        Excel.Range rowColum110 = workSheet2.get_Range("B38", "C38");
                        rowColum110.EntireColumn.ColumnWidth = 9;

                        Excel.Range rowColum111 = workSheet2.get_Range("H38");
                        rowColum111.EntireColumn.ColumnWidth = 10;

                        Excel.Range rowColum112 = workSheet2.get_Range("I38");
                        rowColum112.EntireColumn.ColumnWidth = 10;

                        Excel.Range rowColum113 = workSheet2.get_Range("K38");
                        rowColum113.EntireColumn.ColumnWidth = 10;

                        Excel.Range rowHeight114 = workSheet2.get_Range("A39", "K39");
                        rowHeight114.EntireRow.RowHeight = 6;

                        Excel.Range rowHeight115 = workSheet2.get_Range("A40", "A46");
                        rowHeight115.EntireRow.RowHeight = 35;

                        Excel.Range rowHeight116 = workSheet2.get_Range("A28", "A36");
                        rowHeight116.EntireRow.RowHeight = 8;

                        Excel.Range rowHeight117 = workSheet2.get_Range("A47", "K47");
                        rowHeight117.EntireRow.RowHeight = 6;

                        Excel.Range rowHeight118 = workSheet2.get_Range("A50", "K50");
                        rowHeight118.EntireRow.RowHeight = 10;

                        Excel.Range rowHeight119 = workSheet2.get_Range("A49", "K49");
                        rowHeight119.EntireRow.RowHeight = 20;

                        Excel.Range rowHeight120 = workSheet2.get_Range("A53", "K53");
                        rowHeight120.EntireRow.RowHeight = 10;

                        Excel.Range rowHeight121 = workSheet2.get_Range("A56", "K56");
                        rowHeight121.EntireRow.RowHeight = 10;

                        Excel.Range rowHeight122 = workSheet2.get_Range("A59", "K59");
                        rowHeight122.EntireRow.RowHeight = 10;

                        Excel.Range rowHeight123 = workSheet2.get_Range("A63", "K63");
                        rowHeight123.EntireRow.RowHeight = 10;


                        Excel.Range range_Consolidated100 = workSheet2.Rows.get_Range("F1", "K2");
                        Excel.Range range_Consolidated101 = workSheet2.Rows.get_Range("A7", "I7");
                        Excel.Range range_Consolidated102 = workSheet2.Rows.get_Range("A9", "I9");
                        Excel.Range range_Consolidated103 = workSheet2.Rows.get_Range("J11", "K12");
                        Excel.Range range_Consolidated104 = workSheet2.Rows.get_Range("J13", "K13");
                        Excel.Range range_Consolidated105 = workSheet2.Rows.get_Range("J14", "K14");
                        Excel.Range range_Consolidated106 = workSheet2.Rows.get_Range("I15", "K15");
                        Excel.Range range_Consolidated107 = workSheet2.Rows.get_Range("D17", "H17");
                        Excel.Range range_Consolidated108 = workSheet2.Rows.get_Range("D19", "H19");
                        Excel.Range range_Consolidated109 = workSheet2.Rows.get_Range("H22", "K22");
                        Excel.Range range_Consolidated110 = workSheet2.Rows.get_Range("C22", "E22");
                        Excel.Range range_Consolidated111 = workSheet2.Rows.get_Range("A26", "H36");
                        Excel.Range range_Consolidated112 = workSheet2.Rows.get_Range("I26", "K27");
                        Excel.Range range_Consolidated113 = workSheet2.Rows.get_Range("A26", "K36");
                        Excel.Range range_Consolidated114 = workSheet2.Rows.get_Range("A38", "K38");
                        Excel.Range range_Consolidated115 = workSheet2.Rows.get_Range("A40", "K46");
                        Excel.Range range_Consolidated116 = workSheet2.Rows.get_Range("A50", "K50");
                        Excel.Range range_Consolidated117 = workSheet2.Rows.get_Range("A49", "K49");
                        Excel.Range range_Consolidated118 = workSheet2.Rows.get_Range("A52", "K52");
                        Excel.Range range_Consolidated119 = workSheet2.Rows.get_Range("A53", "K53");
                        Excel.Range range_Consolidated120 = workSheet2.Rows.get_Range("A55", "K55");
                        Excel.Range range_Consolidated121 = workSheet2.Rows.get_Range("A56", "K56");
                        Excel.Range range_Consolidated122 = workSheet2.Rows.get_Range("A58", "K58");
                        Excel.Range range_Consolidated123 = workSheet2.Rows.get_Range("A59", "K59");
                        Excel.Range range_Consolidated124 = workSheet2.Rows.get_Range("A62", "E62");
                        Excel.Range range_Consolidated125 = workSheet2.Rows.get_Range("A63", "K63");
                        Excel.Range range_Consolidated126 = workSheet2.Rows.get_Range("I62", "J62");

                        range_Consolidated100.Font.Size = 9;
                        range_Consolidated101.Font.Bold = true;
                        range_Consolidated102.Font.Bold = true;
                        range_Consolidated103.Font.Size = 9;
                        range_Consolidated103.Font.Bold = true;
                        range_Consolidated104.Font.Size = 9;
                        range_Consolidated105.Font.Bold = true;
                        range_Consolidated105.Font.Size = 9;
                        range_Consolidated106.Font.Size = 9;
                        range_Consolidated107.Font.Size = 12;
                        range_Consolidated107.Font.Bold = true;
                        range_Consolidated108.Font.Bold = true;
                        range_Consolidated109.NumberFormat = "@";
                        range_Consolidated110.NumberFormat = "@";
                        range_Consolidated111.Font.Italic = true;
                        range_Consolidated111.Font.Size = 7;
                        range_Consolidated112.Font.Size = 8;
                        range_Consolidated112.Font.Bold = true;
                        range_Consolidated112.Font.Italic = true;
                        range_Consolidated113.Font.Italic = true;
                        range_Consolidated114.Font.Bold = true;
                        range_Consolidated115.Font.Size = 7;
                        range_Consolidated116.Font.Size = 7;
                        range_Consolidated117.Font.Bold = true;
                        range_Consolidated118.Font.Bold = true;
                        range_Consolidated119.Font.Size = 7;
                        range_Consolidated120.Font.Bold = true;
                        range_Consolidated121.Font.Size = 7;
                        range_Consolidated122.Font.Bold = true;
                        range_Consolidated123.Font.Size = 7;
                        range_Consolidated124.Font.Bold = true;
                        range_Consolidated124.Font.Underline = true;
                        range_Consolidated125.Font.Size = 7;
                        range_Consolidated126.Font.Bold = true;

                        workSheet2.Cells[1, 7] = $"Специализированная форма № ФОУ-18";
                        workSheet2.Cells[2, 6] = $"Утверждена распоряжением ОАО «РЖД» от 29.01.2015 № 190р";
                        workSheet2.Cells[4, 11] = $"Код";
                        workSheet2.Cells[5, 9] = $"Форма по ОКУД";
                        workSheet2.Cells[5, 11] = $"0306831";
                        workSheet2.Cells[6, 10] = $"по ОКПО";
                        workSheet2.Cells[6, 11] = $"{textBox_OKPO_remont.Text}";
                        workSheet2.Cells[7, 1] = $"ОАО \"Российские железные дороги\"";
                        workSheet2.Cells[8, 1] = $"организация";
                        workSheet2.Cells[8, 10] = $"БЕ";
                        workSheet2.Cells[8, 11] = $"{textBox_BE_remont.Text}";
                        workSheet2.Cells[9, 1] = $"\n{textBox_Full_name_company.Text}\n";
                        workSheet2.Cells[10, 1] = $"структурное подразделение";
                        workSheet2.Cells[11, 10] = $"Начальник";
                        workSheet2.Cells[12, 8] = $"УТВЕРЖДАЮ:";
                        workSheet2.Cells[13, 10] = $"(должность)";
                        workSheet2.Cells[14, 10] = $"{textBox_director_FIO_remont_company.Text}";
                        workSheet2.Cells[15, 9] = $"(подпись)";
                        workSheet2.Cells[15, 10] = $"(расшифровка подписи)";
                        workSheet2.Cells[17, 4] = $"ДЕФЕКТНАЯ ВЕДОМОСТЬ";
                        workSheet2.Cells[18, 4] = $"Номер документа";
                        workSheet2.Cells[18, 7] = $"Дата составления";
                        workSheet2.Cells[19, 4] = $"{textBox_numberActRemont.Text}";
                        workSheet2.Cells[19, 7] = $"{textBox_dateTO.Text.Remove(textBox_dateTO.Text.IndexOf(" "))}";
                        workSheet2.Cells[21, 1] = $"Основное средство (здание, оборудование):";
                        workSheet2.Cells[21, 5] = $"{comboBox_model.Text}";
                        workSheet2.Cells[21, 8] = $"Заводской № {textBox_serialNumber.Text}";
                        workSheet2.Cells[22, 1] = $"Инвентарный номер:";
                        workSheet2.Cells[22, 3] = $"{textBox_inventoryNumber.Text}";
                        workSheet2.Cells[22, 6] = $"Сетевой номер:";
                        workSheet2.Cells[22, 8] = $"{textBox_networkNumber.Text}";
                        workSheet2.Cells[23, 1] = $"Местонахождение объекта:";
                        workSheet2.Cells[23, 4] = $"{textBox_city.Text}";
                        workSheet2.Cells[24, 1] = $"Комиссия в составе:";
                        workSheet2.Cells[24, 3] = $"{textBox_chairman_post_remont_company.Text} {textBox_chairman_FIO_remont_company.Text}," +
                            $" {textBox_1_post_remont_company.Text} {textBox_1_FIO_remont_company.Text}";
                        workSheet2.Cells[25, 3] = $"{textBox_2_post_remont_company.Text} {textBox_2_FIO_remont_company.Text}, " +
                            $"{textBox_3_post_remont_company.Text} {textBox_3_FIO_remont_company.Text}";
                        workSheet2.Cells[26, 1] = $"произвела осмотр объектов (указать наименование) и отметила следующее:";
                        workSheet2.Cells[26, 9] = $"(заполняется при капитальном";
                        workSheet2.Cells[27, 9] = $"ремонте зданий и сооружений)";
                        workSheet2.Cells[28, 1] = $"I:";
                        workSheet2.Cells[28, 2] = $"Общие сведения по объекту:";
                        workSheet2.Cells[29, 2] = $"Год постройки:";
                        workSheet2.Cells[30, 2] = $"Этажность, общая высота, площать, протяженность и др.:";
                        workSheet2.Cells[31, 1] = $"II:";
                        workSheet2.Cells[31, 2] = $"Подробное описание конструкций (с указанием материала) и технического состояния";
                        workSheet2.Cells[32, 2] = $"объекта (основания, фундаменты, стены, колонны, перекрытия и др.):";
                        workSheet2.Cells[35, 1] = $"III:";
                        workSheet2.Cells[35, 2] = $"Выводы и предложения по проведению ремонта с перечислением состава работ:";
                        workSheet2.Cells[38, 1] = $"№\nп/п";
                        workSheet2.Cells[38, 2] = $"\nНаименование изделия, узла, агрегата, конструкции, подлежащего ремонту\n";
                        workSheet2.Cells[38, 4] = $"\nНаименование деталей, элементов\n";
                        workSheet2.Cells[38, 6] = $"\nНаименование работ по устранению дефектов\n";
                        workSheet2.Cells[38, 7] = $"\nФормула подсчета\n";
                        workSheet2.Cells[38, 8] = $"\nЕдиница измерения\n";
                        workSheet2.Cells[38, 9] = $"\nКоличество, объем\n";
                        workSheet2.Cells[38, 10] = $"\nДефект (степень износа)\n";
                        workSheet2.Cells[38, 11] = $"\nПримечание\n";
                        workSheet2.Cells[40, 1] = $"1";
                        workSheet2.Cells[41, 1] = $"2";
                        workSheet2.Cells[42, 1] = $"3";
                        workSheet2.Cells[43, 1] = $"4";
                        workSheet2.Cells[44, 1] = $"5";
                        workSheet2.Cells[45, 1] = $"6";
                        workSheet2.Cells[46, 1] = $"6";
                        workSheet2.Cells[46, 1] = $"7";
                        workSheet2.Cells[40, 2] = $"{comboBox_model.Text}";
                        workSheet2.Cells[40, 4] = $"\n{textBox_parts_1.Text}\n";
                        workSheet2.Cells[40, 6] = $"\n{textBox_сompleted_works_1.Text}\n";
                        if (textBox_parts_1.Text != "" || textBox_сompleted_works_1.Text != "")
                        {
                            workSheet2.Cells[40, 8] = $"шт.";
                            workSheet2.Cells[40, 9] = $"1";
                            workSheet2.Cells[40, 10] = $"100%";
                        }
                        workSheet2.Cells[41, 4] = $"\n{textBox_parts_2.Text}\n";
                        workSheet2.Cells[41, 6] = $"\n{textBox_сompleted_works_2.Text}\n";
                        if (textBox_parts_2.Text != "" || textBox_сompleted_works_2.Text != "")
                        {
                            workSheet2.Cells[41, 8] = $"шт.";
                            workSheet2.Cells[41, 9] = $"1";
                            workSheet2.Cells[41, 10] = $"100%";
                        }
                        workSheet2.Cells[42, 4] = $"\n{textBox_parts_3.Text}\n";
                        workSheet2.Cells[42, 6] = $"\n{textBox_сompleted_works_3.Text}\n";
                        if (textBox_parts_3.Text != "" || textBox_сompleted_works_3.Text != "")
                        {
                            workSheet2.Cells[42, 8] = $"шт.";
                            workSheet2.Cells[42, 9] = $"1";
                            workSheet2.Cells[42, 10] = $"100%";
                        }
                        workSheet2.Cells[43, 4] = $"\n{textBox_parts_4.Text}\n";
                        workSheet2.Cells[43, 6] = $"\n{textBox_сompleted_works_4.Text}\n";
                        if (textBox_parts_4.Text != "" || textBox_сompleted_works_4.Text != "")
                        {
                            workSheet2.Cells[43, 8] = $"шт.";
                            workSheet2.Cells[43, 9] = $"1";
                            workSheet2.Cells[43, 10] = $"100%";
                        }
                        workSheet2.Cells[44, 4] = $"\n{textBox_parts_5.Text}\n";
                        workSheet2.Cells[44, 6] = $"\n{textBox_сompleted_works_5.Text}\n";
                        if (textBox_parts_5.Text != "" || textBox_сompleted_works_5.Text != "")
                        {
                            workSheet2.Cells[44, 8] = $"шт.";
                            workSheet2.Cells[44, 9] = $"1";
                            workSheet2.Cells[44, 10] = $"100%";
                        }
                        workSheet2.Cells[45, 4] = $"\n{textBox_parts_6.Text}\n";
                        workSheet2.Cells[45, 6] = $"\n{textBox_сompleted_works_6.Text}\n";
                        if (textBox_parts_6.Text != "" || textBox_сompleted_works_6.Text != "")
                        {
                            workSheet2.Cells[45, 8] = $"шт.";
                            workSheet2.Cells[45, 9] = $"1";
                            workSheet2.Cells[45, 10] = $"100%";
                        }
                        workSheet2.Cells[46, 4] = $"\n{textBox_parts_7.Text}\n";
                        workSheet2.Cells[46, 6] = $"\n{textBox_сompleted_works_7.Text}\n";
                        if (textBox_parts_7.Text != "" || textBox_сompleted_works_7.Text != "")
                        {
                            workSheet2.Cells[46, 8] = $"шт.";
                            workSheet2.Cells[46, 9] = $"1";
                            workSheet2.Cells[46, 10] = $"100%";
                        }
                        workSheet2.Cells[48, 1] = $"Комиссия:";
                        workSheet2.Cells[49, 2] = $"{textBox_chairman_post_remont_company.Text}";
                        workSheet2.Cells[49, 9] = $"{textBox_chairman_FIO_remont_company.Text}";
                        workSheet2.Cells[50, 2] = $"(должность)";
                        workSheet2.Cells[50, 6] = $"(подпись)";
                        workSheet2.Cells[50, 9] = $"(расшифровка подписи)";
                        workSheet2.Cells[52, 2] = $"{textBox_1_post_remont_company.Text}";
                        workSheet2.Cells[52, 9] = $"{textBox_1_FIO_remont_company.Text}";
                        workSheet2.Cells[53, 2] = $"(должность)";
                        workSheet2.Cells[53, 6] = $"(подпись)";
                        workSheet2.Cells[53, 9] = $"(расшифровка подписи)";
                        workSheet2.Cells[55, 2] = $"{textBox_2_post_remont_company.Text}";
                        workSheet2.Cells[55, 9] = $"{textBox_2_FIO_remont_company.Text}";
                        workSheet2.Cells[56, 2] = $"(должность)";
                        workSheet2.Cells[56, 6] = $"(подпись)";
                        workSheet2.Cells[56, 9] = $"(расшифровка подписи)";
                        workSheet2.Cells[58, 2] = $"{textBox_3_post_remont_company.Text}";
                        workSheet2.Cells[58, 9] = $"{textBox_3_FIO_remont_company.Text}";
                        workSheet2.Cells[59, 2] = $"(должность)";
                        workSheet2.Cells[59, 6] = $"(подпись)";
                        workSheet2.Cells[59, 9] = $"(расшифровка подписи)";
                        workSheet2.Cells[60, 1] = $"Исполнитель:";
                        workSheet2.Cells[62, 1] = $"Начальник участка по ТО и ремонту СРС";
                        workSheet2.Cells[62, 9] = $"{label_FIO_chief.Text}";
                        workSheet2.Cells[63, 2] = $"(должность)";
                        workSheet2.Cells[63, 6] = $"(подпись)";
                        workSheet2.Cells[63, 9] = $"(расшифровка подписи)";

                        #endregion

                        var file = $"{textBox_numberActRemont.Text.Replace('/', '.')}-{textBox_company.Text}_Акт.xlsx";

                        if (!File.Exists($@"C:\Documents_ServiceTelekom\Акты_ремонта\{textBox_city.Text}\{textBox_company.Text}\"))
                        {
                            try
                            {
                                Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\Акты_ремонта\{textBox_city.Text}\{textBox_company.Text}\");

                                workSheet.SaveAs($@"C:\Documents_ServiceTelekom\Акты_ремонта\{textBox_city.Text}\{textBox_company.Text}\" + file);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                MessageBox.Show("Не удаётся сохранить файл.");
                            }
                        }
                        else
                        {
                            try
                            {
                                workSheet.SaveAs($@"C:\Documents_ServiceTelekom\Акты_ремонта\{textBox_city.Text}\{textBox_company.Text}\" + file);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                MessageBox.Show("Не удаётся сохранить файл.");
                            }

                        }

                        exApp.Visible = true;
                    }

                    dataGridView1.Enabled = true;
                    panel1.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                if (exApp != null)
                {
                    exApp = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();

                Environment.Exit(0);
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Сохранение БД на PC

        /// <summary>
        /// сохранение БД на H(S)DD
        /// </summary>
        void SaveFile()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    string filename = sfd.FileName;

                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Unicode))
                    {
                        string note = string.Empty;

                        note += $"Номер\tПолигон\tПредприятие\tМесто нахождения\tМодель\tЗаводской номер\t" +
                            $"Инвентарный номер\tСетевой номер\tДата проведения ТО\tНомер акта\tГород\tЦена ТО\t" +
                            $"Представитель предприятия\tДолжность\tНомер удостоверения\tДата выдачи\tНомер телефона\t" +
                            $"Номер Акта ремонта\tКатегория\tЦена ремонта\tАнтенна\tМанипулятор\tАКБ\tЗУ\tВыполненные работы_1\t" +
                            $"Выполненные работы_2\tВыполненные работы_3\tВыполненные работы_4\tВыполненные работы_5\t" +
                            $"Выполненные работы_6\tВыполненные работы_7\tДеталь_1\tДеталь_2\tДеталь_3\tДеталь_4\tДеталь_5\t" +
                            $"Деталь_6\tДеталь_7\t";

                        sw.WriteLine(note);

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                sw.Write((dataGridView1.Rows[i].Cells[j].Value + "\t").ToString());
                            }

                            sw.WriteLine();
                        }

                        MessageBox.Show("Файл успешно сохранен");
                    }
                }
                else
                {
                    string Mesage;
                    Mesage = "Файл не сохранён";

                    if (MessageBox.Show(Mesage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                string Mesage;
                Mesage = "Файл не сохранён!";

                if (MessageBox.Show(Mesage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return;
                }
                MessageBox.Show(ex.ToString());
            }
        }
        void Button_save_in_file_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        #endregion

        #region Взаимодействие на форме Key-Press-ы, Button_click
        void TextBox_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Search(dataGridView1);
                UpdateCountRST();
                UpdateSumTOrst();
                UpdateSumTOrstRemont();
                UpdateCountRSTRemont();
            }
        }

        void Button_search_Click(object sender, EventArgs e)
        {
            Search(dataGridView1);
            UpdateCountRST();
            UpdateSumTOrst();
            UpdateSumTOrstRemont();
            UpdateCountRSTRemont();
        }

        void Button_seach_BD_city_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            UpdateCountRST();
            UpdateSumTOrst();
            UpdateSumTOrstRemont();
            UpdateCountRSTRemont();
        }

        void TextBox_numberAct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Update_datagridview_number_act(dataGridView1);
                UpdateCountRST();
                UpdateSumTOrst();
                UpdateSumTOrstRemont();
                UpdateCountRSTRemont();
            }
        }

        void TextBox_numberAct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (textBox_numberAct.Text != "")
            {
                Update_datagridview_number_act(dataGridView1);
                UpdateCountRST();
                UpdateSumTOrst();
                UpdateSumTOrstRemont();
                UpdateCountRSTRemont();
            }
        }

        /// <summary>
        /// Отмена редактирования datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region dataGridView1.Update() для добавления или удаление строки
        void DataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                dataGridView1.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void DataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                dataGridView1.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region открываем панель инфо о бригаде при double_click и Key press Key UP для печати

        void TextBox_number_printing_doc_datePanel_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b')
            {
                e.Handled = true;

            }
        }

        void TextBox_number_printing_doc_datePanel_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void Label_FIO_chief_DoubleClick(object sender, EventArgs e)
        {
            Change_information_ServiceTelecom();
        }

        void Label_FIO_Engineer_DoubleClick(object sender, EventArgs e)
        {
            Change_information_ServiceTelecom();
        }

        void Label_doverennost_DoubleClick(object sender, EventArgs e)
        {
            Change_information_ServiceTelecom();
        }

        void Label_polinon_full_DoubleClick(object sender, EventArgs e)
        {
            Change_information_ServiceTelecom();
        }

        void Label_number_printing_doc_reg_DoubleClick(object sender, EventArgs e)
        {
            Change_information_ServiceTelecom();
        }

        void Change_information_ServiceTelecom()
        {
            try
            {
                string Mesage;
                Mesage = "Вы действительно хотите изменить основную информацию?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                textBox_GD_city.Text = comboBox_city.Text;
                textBox_FIO_chief.Text = label_FIO_chief.Text;
                textBox_doverennost.Text = label_doverennost.Text;
                textBox_FIO_Engineer.Text = label_FIO_Engineer.Text;
                textBox_polinon_full.Text = label_polinon_full.Text;
                Block_ST_Work_Form_control();
                panel_date.Enabled = true;
                panel_date.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        #endregion

        #region подсветка label_indo
        void Label_FIO_chief_MouseEnter(object sender, EventArgs e)
        {
            label_FIO_chief.ForeColor = Color.White;
        }

        void Label_FIO_chief_MouseLeave(object sender, EventArgs e)
        {
            label_FIO_chief.ForeColor = Color.Black;
        }

        void Label_FIO_Engineer_MouseEnter(object sender, EventArgs e)
        {
            label_FIO_Engineer.ForeColor = Color.White;
        }

        void Label_FIO_Engineer_MouseLeave(object sender, EventArgs e)
        {
            label_FIO_Engineer.ForeColor = Color.Black;
        }

        void Label_doverennost_MouseEnter(object sender, EventArgs e)
        {
            label_doverennost.ForeColor = Color.White;
        }

        void Label_doverennost_MouseLeave(object sender, EventArgs e)
        {
            label_doverennost.ForeColor = Color.Black;
        }

        void Label_polinon_full_MouseEnter(object sender, EventArgs e)
        {
            label_polinon_full.ForeColor = Color.White;
        }

        void Label_polinon_full_MouseLeave(object sender, EventArgs e)
        {
            label_polinon_full.ForeColor = Color.Black;
        }
        #endregion

        #region поиск отсутсвующих рст исходя из предыдущего года

        void PictureBox_seach_datadrid_replay_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                panel1.Enabled = false;
                panel3.Enabled = false;
                Seach_DataGrid_Replay_RST(dataGridView1);
                UpdateCountRST();
                UpdateSumTOrst();
                UpdateSumTOrstRemont();
                UpdateCountRSTRemont();
            }
        }

        void Seach_DataGrid_Replay_RST(DataGridView dgw)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (txb_flag_all_BD.Text == "Вся БД")
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

                    else if (comboBox_city.Text != "")
                    {
                        var myCulture = new CultureInfo("ru-RU");
                        myCulture.NumberFormat.NumberDecimalSeparator = ".";
                        Thread.CurrentThread.CurrentCulture = myCulture;
                        dgw.Rows.Clear();

                        string queryString = $"SELECT radiostantion_last_year. * FROM radiostantion_last_year LEFT JOIN radiostantion ON (radiostantion_last_year.serialNumber=radiostantion.serialNumber) WHERE radiostantion.serialNumber IS NULL AND radiostantion_last_year.city LIKE '%" + comboBox_city.Text + "%'";

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

                    txb_flag_all_BD.Text = "";

                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                    dataGridView1.Columns[0].Width = 45;
                    dataGridView1.Columns[3].Width = 170;
                    dataGridView1.Columns[4].Width = 180;
                    dataGridView1.Columns[5].Width = 150;
                    dataGridView1.Columns[6].Width = 178;
                    dataGridView1.Columns[7].Width = 178;
                    dataGridView1.Columns[8].Width = 100;
                    dataGridView1.Columns[9].Width = 110;
                    dataGridView1.Columns[10].Width = 100;
                    dataGridView1.Columns[11].Width = 100;
                    dataGridView1.Columns[17].Width = 120;

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

        #region ContextMenu datagrid
        void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (_user.IsAdmin == "Дирекция связи" || _user.IsAdmin == "Инженер")
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        ContextMenu m3 = new ContextMenu();
                        m3.MenuItems.Add(new MenuItem("Сохранение базы", Button_save_in_file_Click));
                        m3.MenuItems.Add(new MenuItem("Обновить базу", Button_update_Click));
                        m3.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                }
                else if (_user.IsAdmin == "Начальник участка" || _user.IsAdmin == "Куратор" || _user.IsAdmin == "Руководитель" || _user.IsAdmin == "Admin")
                {
                    if (dataGridView1.Rows.Count > 0 && panel1.Enabled == true && panel3.Enabled == true)
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            ContextMenu m = new ContextMenu();
                            m.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", Button_new_add_rst_form_Click));
                            m.MenuItems.Add(new MenuItem("Изменить добавленную радиостанцию", button_new_add_rst_form_Click_change));
                            m.MenuItems.Add(new MenuItem("Добавить/изменить ремонт", button_new_add_rst_form_click_remont));
                            m.MenuItems.Add(new MenuItem("Обновить базу", Button_update_Click));
                            m.MenuItems.Add(new MenuItem("Сформировать акт ТО", Button_form_act_Click));
                            m.MenuItems.Add(new MenuItem("Сформировать акт Ремонта", Button_remont_act_Click));
                            m.MenuItems.Add(new MenuItem("Удалить радиостанцию", Button_delete_Click));
                            m.MenuItems.Add(new MenuItem("Удалить ремонт", Delete_rst_remont_click));
                            m.MenuItems.Add(new MenuItem("Сохранение базы", Button_save_in_file_Click));
                            m.MenuItems.Add(new MenuItem("Показать совпадение с предыдущим годом", PictureBox_seach_datadrid_replay_Click));
                            m.MenuItems.Add(new MenuItem("Отметить акт", DataGridView1_DefaultCellStyleChanged));
                            m.MenuItems.Add(new MenuItem("Списать РСТ", DecommissionSerialNumber));
                            m.MenuItems.Add(new MenuItem("Показать списания", Show_radiostantion_decommission_Click));
                            m.MenuItems.Add(new MenuItem("Сформировать акт списания", PrintWord_Act_decommission));

                            m.Show(dataGridView1, new Point(e.X, e.Y));
                        }
                    }
                    else if (dataGridView1.Rows.Count == 0 && panel1.Enabled == true && panel3.Enabled == true)
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            ContextMenu m1 = new ContextMenu();
                            m1.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", Button_new_add_rst_form_Click));
                            m1.MenuItems.Add(new MenuItem("Обновить базу", Button_update_Click));

                            m1.Show(dataGridView1, new Point(e.X, e.Y));
                        }
                    }
                    else if (dataGridView1.Rows.Count > 0 || dataGridView1.Rows.Count == 0 && panel1.Enabled == false && panel3.Enabled == false)
                    {
                        if (e.Button == MouseButtons.Right)
                        {
                            ContextMenu m2 = new ContextMenu();
                            m2.MenuItems.Add(new MenuItem("Сохранение базы", Button_save_in_file_Click));
                            m2.MenuItems.Add(new MenuItem("Обновить базу", Button_update_Click_after_Seach_DataGrid_Replay_RST));

                            m2.Show(dataGridView1, new Point(e.X, e.Y));
                        }
                        if (e.Button == MouseButtons.Left)
                        {
                            dataGridView1.ClearSelection();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region обновляем БД после показа отсутсвующих радиостанций после проверки на участке

        /// <summary>
        /// TODO Костыль из-за ошибки выбора строки после обновления БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_update_Click_after_Seach_DataGrid_Replay_RST(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (dataGridView1.Rows.Count > 0 || dataGridView1.Rows.Count == 0)
                    {
                        panel1.Enabled = true;
                        panel3.Enabled = true;
                        RefreshDataGrid(dataGridView1);
                        UpdateCountRST();
                        UpdateSumTOrst();
                        UpdateSumTOrstRemont();
                        UpdateCountRSTRemont();
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
        void Delete_rst_remont_click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите удалить ремонт?";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (textBox_numberActRemont.Text != "")
                    {
                        if (CheackNumberAct_radiostantion(textBox_numberActRemont.Text) == true)
                        {
                            string serialNumber = textBox_serialNumber.Text;

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
                    Button_update_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        Boolean CheackNumberAct_radiostantion(string numberActRemont)
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

        #region отк. формы добавления ремонтов
        private void button_new_add_rst_form_click_remont(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool() == true)
            {
                try
                {
                    if (textBox_serialNumber.Text == "")
                    {

                    }
                    else
                    {
                        using (remontRSTForm remontRSTForm = new remontRSTForm())
                        {
                            remontRSTForm.DoubleBufferedForm(true);

                            remontRSTForm.comboBox_сategory.Text = comboBox_сategory.Text;
                            if (textBox_numberActRemont.Text == "")
                            {
                                remontRSTForm.textBox_numberActRemont.Text = textBox_number_printing_doc_datePanel.Text + "/";
                            }
                            else remontRSTForm.textBox_numberActRemont.Text = textBox_numberActRemont.Text;
                            remontRSTForm.textBox_priceRemont.Text = textBox_priceRemont.Text;
                            remontRSTForm.textBox_сompleted_works_1.Text = textBox_сompleted_works_1.Text;
                            remontRSTForm.textBox_сompleted_works_2.Text = textBox_сompleted_works_2.Text;
                            remontRSTForm.textBox_сompleted_works_3.Text = textBox_сompleted_works_3.Text;
                            remontRSTForm.textBox_сompleted_works_4.Text = textBox_сompleted_works_4.Text;
                            remontRSTForm.textBox_сompleted_works_5.Text = textBox_сompleted_works_5.Text;
                            remontRSTForm.textBox_сompleted_works_6.Text = textBox_сompleted_works_6.Text;
                            remontRSTForm.textBox_сompleted_works_7.Text = textBox_сompleted_works_7.Text;
                            remontRSTForm.textBox_parts_1.Text = textBox_parts_1.Text;
                            remontRSTForm.textBox_parts_2.Text = textBox_parts_2.Text;
                            remontRSTForm.textBox_parts_3.Text = textBox_parts_3.Text;
                            remontRSTForm.textBox_parts_4.Text = textBox_parts_4.Text;
                            remontRSTForm.textBox_parts_5.Text = textBox_parts_5.Text;
                            remontRSTForm.textBox_parts_6.Text = textBox_parts_6.Text;
                            remontRSTForm.textBox_parts_7.Text = textBox_parts_7.Text;

                            if (textBox_dateTO.Text != "")
                            {
                                textBox_dateTO.Text = DateTime.Now.ToString("dd.MM.yyyy");
                            }

                            remontRSTForm.textBox_data_remont.Text = textBox_dateTO.Text;
                            remontRSTForm.textBox_model.Text = comboBox_model.Text;
                            remontRSTForm.label_company.Text = textBox_company.Text;
                            remontRSTForm.textBox_serialNumber.Text = textBox_serialNumber.Text;

                            remontRSTForm.ShowDialog();
                            int currRowIndex = dataGridView1.CurrentCell.RowIndex;
                            RefreshDataGrid(dataGridView1);
                            dataGridView1.ClearSelection();

                            if (dataGridView1.CurrentCell.RowIndex >= 0)
                            {
                                dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
                            }
                            Refresh_values_TXB_CMB();
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

        #region отк. формы изменения РСТ
        private void button_new_add_rst_form_Click_change(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool() == true)
            {
                try
                {
                    if (textBox_serialNumber.Text == "")
                    {

                    }
                    else
                    {
                        using (changeRSTForm changeRSTForm = new changeRSTForm())
                        {
                            changeRSTForm.DoubleBufferedForm(true);
                            changeRSTForm.textBox_city.Text = textBox_city.Text;
                            changeRSTForm.comboBox_poligon.Text = comboBox_poligon.Text;
                            changeRSTForm.textBox_company.Text = textBox_company.Text;
                            changeRSTForm.textBox_location.Text = textBox_location.Text;
                            //changeRSTForm.comboBox_model.Text = comboBox_model.Text;
                            changeRSTForm.comboBox_model.Items.Add(comboBox_model.Text).ToString();
                            changeRSTForm.textBox_serialNumber.Text = textBox_serialNumber.Text;
                            changeRSTForm.textBox_inventoryNumber.Text = textBox_inventoryNumber.Text;
                            changeRSTForm.textBox_networkNumber.Text = textBox_networkNumber.Text;
                            changeRSTForm.textBox_dateTO.Text = textBox_dateTO.Text.Remove(textBox_dateTO.Text.IndexOf(" "));
                            changeRSTForm.textBox_numberAct.Text = textBox_numberAct.Text;
                            changeRSTForm.textBox_representative.Text = textBox_representative.Text;
                            changeRSTForm.textBox_numberIdentification.Text = textBox_numberIdentification.Text;
                            changeRSTForm.textBox_phoneNumber.Text = textBox_phoneNumber.Text;
                            changeRSTForm.textBox_post.Text = textBox_post.Text;
                            changeRSTForm.txB_comment.Text = txB_comment.Text;

                            if (textBox_dateIssue.Text == "")
                            {
                                textBox_dateIssue.Text = DateTime.Now.ToString("dd.MM.yyyy");
                            }
                            changeRSTForm.textBox_dateIssue.Text = textBox_dateIssue.Text;

                            if (textBox_antenna.Text == "")
                            {
                                textBox_antenna.Text = "-";
                            }
                            changeRSTForm.textBox_antenna.Text = textBox_antenna.Text;
                            if (textBox_manipulator.Text == "")
                            {
                                textBox_manipulator.Text = "-";
                            }
                            changeRSTForm.textBox_manipulator.Text = textBox_manipulator.Text;
                            if (textBox_batteryСharger.Text == "")
                            {
                                textBox_batteryСharger.Text = "-";
                            }
                            changeRSTForm.textBox_batteryСharger.Text = textBox_batteryСharger.Text;
                            if (textBox_AKB.Text == "")
                            {
                                textBox_AKB.Text = "-";
                            }
                            changeRSTForm.textBox_AKB.Text = textBox_AKB.Text;

                            changeRSTForm.ShowDialog();

                            int currRowIndex = dataGridView1.CurrentCell.RowIndex;
                            RefreshDataGrid(dataGridView1);
                            //update_datagridview_number_act(dataGridView1);
                            dataGridView1.ClearSelection();

                            if (dataGridView1.CurrentCell.RowIndex >= 0)
                            {
                                dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
                            }
                            Refresh_values_TXB_CMB();
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

        #region update_datagridview_number_act
        /// <summary>
        /// Обновляем datagrid по номеру акта
        /// </summary>
        /// <param name="dgw"></param>
        async void Update_datagridview_number_act(DataGridView dgw)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    dgw.Rows.Clear();
                    dgw.AllowUserToAddRows = false;
                    string searchString = $"SELECT * FROM radiostantion WHERE city = '{comboBox_city.Text}' AND numberAct LIKE '" + textBox_numberAct.Text.Trim() + "'";

                    using (MySqlCommand command = new MySqlCommand(searchString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.openConnection();

                        IAsyncResult res = command.BeginExecuteReader();

                        using (MySqlDataReader reader = command.EndExecuteReader(res))
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    ReedSingleRow(dgw, reader);
                                }
                                reader.Close();
                            }
                        }
                        DB.GetInstance.closeConnection();
                    }
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
                    dataGridView1.Columns[0].Width = 45;
                    dataGridView1.Columns[3].Width = 170;
                    dataGridView1.Columns[4].Width = 180;
                    dataGridView1.Columns[5].Width = 150;
                    dataGridView1.Columns[6].Width = 178;
                    dataGridView1.Columns[7].Width = 178;
                    dataGridView1.Columns[8].Width = 100;
                    dataGridView1.Columns[9].Width = 110;
                    dataGridView1.Columns[10].Width = 100;
                    dataGridView1.Columns[11].Width = 100;
                    dataGridView1.Columns[17].Width = 120;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Ошибка! Невозможно найти по данному акту!");
                }
            }
        }
        #endregion

        #region panel_remont_info 

        /// <summary>
        /// откр. панели и считывание данных из реестра для ремонта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_remont_act_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                if (textBox_numberActRemont.Text == "")
                {
                    string Mesage;
                    Mesage = "На данной радиостанции нет ремонта!";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    try
                    {
                        dataGridView1.Enabled = false;
                        panel1.Enabled = false;
                        panel_remont_information_company.Enabled = true;
                        panel_remont_information_company.Visible = true;
                        label_company_remont.Text = textBox_company.Text;
                        RegistryKey reg = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\{textBox_company.Text}");
                        if (reg != null)
                        {
                            RegistryKey currentUserKey = Registry.CurrentUser;
                            RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\{textBox_company.Text}");

                            textBox_Full_name_company.Text = helloKey.GetValue("Полное наименование предприятия").ToString();
                            textBox_OKPO_remont.Text = helloKey.GetValue("ОКПО").ToString();
                            textBox_BE_remont.Text = helloKey.GetValue("БЕ").ToString();
                            textBox_director_FIO_remont_company.Text = helloKey.GetValue("Руководитель ФИО").ToString();
                            textBox_director_post_remont_company.Text = helloKey.GetValue("Руководитель Должность").ToString();
                            textBox_chairman_FIO_remont_company.Text = helloKey.GetValue("Председатель ФИО").ToString();
                            textBox_chairman_post_remont_company.Text = helloKey.GetValue("Председатель Должность").ToString();
                            textBox_1_FIO_remont_company.Text = helloKey.GetValue("1 член комиссии ФИО").ToString();
                            textBox_1_post_remont_company.Text = helloKey.GetValue("1 член комиссии Должность").ToString();
                            textBox_2_FIO_remont_company.Text = helloKey.GetValue("2 член комиссии ФИО").ToString();
                            textBox_2_post_remont_company.Text = helloKey.GetValue("2 член комиссии Должность").ToString();
                            textBox_3_FIO_remont_company.Text = helloKey.GetValue("3 член комиссии ФИО").ToString();
                            textBox_3_post_remont_company.Text = helloKey.GetValue("3 член комиссии Должность").ToString();

                            if (textBox_Full_name_company.Text != "" && textBox_OKPO_remont.Text != "" && textBox_BE_remont.Text != ""
                                && textBox_director_FIO_remont_company.Text != "" && textBox_director_post_remont_company.Text != ""
                                && textBox_chairman_FIO_remont_company.Text != "" && textBox_chairman_post_remont_company.Text != ""
                                && textBox_1_FIO_remont_company.Text != "" && textBox_1_post_remont_company.Text != ""
                                && textBox_2_FIO_remont_company.Text != "" && textBox_2_post_remont_company.Text != "")
                            {
                                button_Continue_remont_act_excel.Enabled = true;
                            }
                            helloKey.Close();
                        }
                        else
                        {
                            button_Continue_remont_act_excel.Enabled = false;
                            textBox_Full_name_company.Text = "";
                            textBox_OKPO_remont.Text = "";
                            textBox_BE_remont.Text = "";
                            textBox_director_FIO_remont_company.Text = "";
                            textBox_director_post_remont_company.Text = $"Начальник {textBox_company.Text}";
                            textBox_chairman_FIO_remont_company.Text = "";
                            textBox_chairman_post_remont_company.Text = "";
                            textBox_1_FIO_remont_company.Text = "";
                            textBox_1_post_remont_company.Text = "";
                            textBox_2_FIO_remont_company.Text = "";
                            textBox_2_post_remont_company.Text = "";
                            textBox_3_FIO_remont_company.Text = "";
                            textBox_3_post_remont_company.Text = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString()); ;
                        MessageBox.Show("Ошибка считывания реестра!");
                    }
                }
            }

        }

        void Button_close_remont_panel_Click(object sender, EventArgs e)
        {
            panel_remont_information_company.Visible = false;
            panel_remont_information_company.Enabled = false;
            dataGridView1.Enabled = true;
            panel1.Enabled = true;
        }

        #region проверка текста panel_remont_info
        void TextBox_Full_name_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b'
                && ch != '-' && ch != '.' && ch != ' ' && ch != '=' && ch != '!' && ch != '*')
            {
                e.Handled = true;
            }
        }

        void TextBox_Full_name_company_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void TextBox_BE_remont_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        void TextBox_OKPO_remont_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        void TextBox_OKPO_remont_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void TextBox_BE_remont_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void TextBox_director_FIO_remont_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        void TextBox_director_post_remont_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        void TextBox_director_FIO_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void TextBox_director_post_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void TextBox_chairman_FIO_remont_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        void TextBox_chairman_post_remont_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        void TextBox_chairman_FIO_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void TextBox_chairman_post_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void TextBox_1_FIO_remont_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        void TextBox_1_post_remont_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        void TextBox_1_FIO_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void TextBox_1_post_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void TextBox_2_FIO_remont_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        void TextBox_2_FIO_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void TextBox_2_post_remont_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        void TextBox_2_post_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void TextBox_3_FIO_remont_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        void TextBox_3_FIO_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        void TextBox_3_post_remont_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        void TextBox_3_post_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }
        #endregion

        /// <summary>
        /// считыванеи данных из реестра в panel_info_remont
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_information_remont_company_regedit_Click(object sender, EventArgs e)
        {
            try
            {
                #region проверка пустых строк
                if (textBox_Full_name_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"Полное наименование предприятия\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_OKPO_remont.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"ОКПО\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_BE_remont.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"БЕ\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_director_FIO_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"Руководитель ФИО\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }

                if (textBox_director_post_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"Руководитель Должность\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }

                if (textBox_chairman_FIO_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"Председатель ФИО\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }

                if (textBox_chairman_post_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"Председатель Должность\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }

                if (textBox_1_FIO_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"1 член комиссии ФИО\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }

                if (textBox_1_post_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"1 член комиссии Должность\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (textBox_2_FIO_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"2 член комиссии ФИО\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }

                if (textBox_2_post_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"2 член комиссии Должность\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                #endregion

                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\{textBox_company.Text}");
                helloKey.SetValue("Полное наименование предприятия", $"{textBox_Full_name_company.Text}");
                helloKey.SetValue("ОКПО", $"{textBox_OKPO_remont.Text}");
                helloKey.SetValue("БЕ", $"{textBox_BE_remont.Text}");
                helloKey.SetValue("Руководитель ФИО", $"{textBox_director_FIO_remont_company.Text}");
                helloKey.SetValue("Руководитель Должность", $"{textBox_director_post_remont_company.Text}");
                helloKey.SetValue("Председатель ФИО", $"{textBox_chairman_FIO_remont_company.Text}");
                helloKey.SetValue("Председатель Должность", $"{textBox_chairman_post_remont_company.Text}");
                helloKey.SetValue("1 член комиссии ФИО", $"{textBox_1_FIO_remont_company.Text}");
                helloKey.SetValue("1 член комиссии Должность", $"{textBox_1_post_remont_company.Text}");
                helloKey.SetValue("2 член комиссии ФИО", $"{textBox_2_FIO_remont_company.Text}");
                helloKey.SetValue("2 член комиссии Должность", $"{textBox_2_post_remont_company.Text}");
                helloKey.SetValue("3 член комиссии ФИО", $"{textBox_3_FIO_remont_company.Text}");
                helloKey.SetValue("3 член комиссии Должность", $"{textBox_3_post_remont_company.Text}");

                helloKey.Close();

                if (textBox_Full_name_company.Text != "" && textBox_OKPO_remont.Text != "" && textBox_BE_remont.Text != ""
                            && textBox_director_FIO_remont_company.Text != "" && textBox_director_post_remont_company.Text != ""
                            && textBox_chairman_FIO_remont_company.Text != "" && textBox_chairman_post_remont_company.Text != ""
                            && textBox_1_FIO_remont_company.Text != "" && textBox_1_post_remont_company.Text != ""
                            && textBox_2_FIO_remont_company.Text != "" && textBox_2_post_remont_company.Text != "")
                {
                    button_Continue_remont_act_excel.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); ;
                MessageBox.Show("Ошибка записи в реестр!");
            }
        }


        #endregion

        #region для выбора значения в Control(TXB)
        /// <summary>
        /// после добавления ремонта или изменения данных РСТ, присваиваем значения textBox и comBox
        /// </summary>
        void Refresh_values_TXB_CMB()
        {
            DataGridViewRow row = dataGridView1.Rows[selectedRow];
            textBox_id.Text = row.Cells[0].Value.ToString();
            comboBox_poligon.Text = row.Cells[1].Value.ToString();
            textBox_company.Text = row.Cells[2].Value.ToString();
            textBox_location.Text = row.Cells[3].Value.ToString();
            comboBox_model.Text = row.Cells[4].Value.ToString();
            textBox_serialNumber.Text = row.Cells[5].Value.ToString();
            textBox_inventoryNumber.Text = row.Cells[6].Value.ToString();
            textBox_networkNumber.Text = row.Cells[7].Value.ToString();
            textBox_dateTO.Text = row.Cells[8].Value.ToString();
            textBox_numberAct.Text = row.Cells[9].Value.ToString();
            textBox_city.Text = row.Cells[10].Value.ToString();
            textBox_price.Text = row.Cells[11].Value.ToString();
            textBox_representative.Text = row.Cells[12].Value.ToString();
            textBox_post.Text = row.Cells[13].Value.ToString();
            textBox_numberIdentification.Text = row.Cells[14].Value.ToString();
            textBox_dateIssue.Text = row.Cells[15].Value.ToString();
            textBox_phoneNumber.Text = row.Cells[16].Value.ToString();
            textBox_numberActRemont.Text = row.Cells[17].Value.ToString();
            comboBox_сategory.Text = row.Cells[18].Value.ToString();
            textBox_priceRemont.Text = row.Cells[19].Value.ToString();
            textBox_antenna.Text = row.Cells[20].Value.ToString();
            textBox_manipulator.Text = row.Cells[21].Value.ToString();
            textBox_AKB.Text = row.Cells[22].Value.ToString();
            textBox_batteryСharger.Text = row.Cells[23].Value.ToString();
            textBox_сompleted_works_1.Text = row.Cells[24].Value.ToString();
            textBox_сompleted_works_2.Text = row.Cells[25].Value.ToString();
            textBox_сompleted_works_3.Text = row.Cells[26].Value.ToString();
            textBox_сompleted_works_4.Text = row.Cells[27].Value.ToString();
            textBox_сompleted_works_5.Text = row.Cells[28].Value.ToString();
            textBox_сompleted_works_6.Text = row.Cells[29].Value.ToString();
            textBox_сompleted_works_7.Text = row.Cells[30].Value.ToString();
            textBox_parts_1.Text = row.Cells[31].Value.ToString();
            textBox_parts_2.Text = row.Cells[32].Value.ToString();
            textBox_parts_3.Text = row.Cells[33].Value.ToString();
            textBox_parts_4.Text = row.Cells[34].Value.ToString();
            textBox_parts_5.Text = row.Cells[35].Value.ToString();
            textBox_parts_6.Text = row.Cells[36].Value.ToString();
            textBox_parts_7.Text = row.Cells[37].Value.ToString();
            txB_decommissionSerialNumber.Text = row.Cells[38].Value.ToString();
            txB_comment.Text = row.Cells[39].Value.ToString();
        }
        #endregion

        #region поиск по dataGrid без запроса к БД и открытие функциональной панели Control + K
        void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (_user.IsAdmin == "Дирекция связи" || _user.IsAdmin == "Инженер")
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
                {
                    panel_seach_datagrid.Enabled = true;
                    panel_seach_datagrid.Visible = true;
                    this.ActiveControl = textBox_seach_panel_seach_datagrid;
                }

                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
                {
                    if (textBox_representative.Text != "")
                    {
                        panel_info_phone_FIO.Enabled = true;
                        panel_info_phone_FIO.Visible = true;
                        panel_textbox_FIO_representative.Text = textBox_representative.Text;
                        panel_textbox_FIO_phoneNumber.Text = textBox_phoneNumber.Text;
                    }
                }
            }
            else
            {
                // открывем панель поиска по гриду по зав номеру РСТ
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
                {
                    panel_seach_datagrid.Enabled = true;
                    panel_seach_datagrid.Visible = true;
                    this.ActiveControl = textBox_seach_panel_seach_datagrid;
                }
                // открываем функциональную панель
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.K)
                {
                    Button_Functional_loading_panel(sender, e);
                }
                // открываем панель инфы о ФИО и номере баланосдержателя
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
                {
                    if (textBox_representative.Text != "")
                    {
                        panel_info_phone_FIO.Enabled = true;
                        panel_info_phone_FIO.Visible = true;
                        panel_textbox_FIO_representative.Text = textBox_representative.Text;
                        panel_textbox_FIO_phoneNumber.Text = textBox_phoneNumber.Text;
                    }
                }
            }
        }
        /// <summary>
        /// Закрываем панель инфы о ФИО и номере баланосдержателя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_close_panel_info_phone_FIO_Click(object sender, EventArgs e)
        {
            panel_info_phone_FIO.Enabled = false;
            panel_info_phone_FIO.Visible = false;
        }

        void Seach_datagrid()
        {
            if (textBox_seach_panel_seach_datagrid.Text != "")
            {
                string searchValue = textBox_seach_panel_seach_datagrid.Text;

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    dataGridView1.ClearSelection();
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Equals(searchValue))
                            {
                                dataGridView1.Rows[i].Cells[j].Selected = true;
                                dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.Rows[i].Cells[j].RowIndex];
                                break;
                            }
                        }
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
                textBox_seach_panel_seach_datagrid.Text = "";
                panel_seach_datagrid.Enabled = false;
                panel_seach_datagrid.Visible = false;
            }
            else
            {
                string Mesage2;

                Mesage2 = "Поле поиска не должно быть пустым!";

                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }

        }
        void Button_close_panel_seach_datagrid_Click(object sender, EventArgs e)
        {
            panel_seach_datagrid.Enabled = false;
            panel_seach_datagrid.Visible = false;
        }
        void Button_seach_panel_seach_datagrid_Click(object sender, EventArgs e)
        {
            Seach_datagrid();
        }
        void TextBox_seach_panel_seach_datagrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Seach_datagrid();
            }
        }

        void TextBox_seach_panel_seach_datagrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            char ch = e.KeyChar;
            if ((ch < 'A' || ch > 'Z') && (ch <= 47 || ch >= 58) && ch != '/' && ch != '\b' && ch != '.')
            {
                e.Handled = true;
            }
        }

        void TextBox_seach_panel_seach_datagrid_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }
        #endregion

        #region toolTip для Control-ов формы

        #region свойства toolTip Popup Draw
        void ToolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            Font tooltipFont = new Font("TimesNewRoman", 12.0f);
            e.DrawBackground();
            e.DrawBorder();
            e.Graphics.DrawString(e.ToolTipText, tooltipFont, Brushes.Black, new PointF(1, 1));
        }

        void ToolTip1_Popup(object sender, PopupEventArgs e)
        {
            e.ToolTipSize = TextRenderer.MeasureText(toolTip1.GetToolTip(e.AssociatedControl), new Font("TimesNewRoman", 13.0f));
        }
        #endregion

        void ComboBox_city_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(comboBox_city, $"Выберите названиe города");
        }
        void Button_seach_BD_city_Click_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(button_seach_BD_city, $"Выполнить");
        }

        void Button_add_city_click_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(button_add_city, $"Добавить в реестр\nназвание города");
        }

        void TextBox_search_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(textBox_search, $"Введи искомое");
        }

        void ComboBox_seach_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(comboBox_seach, $"Поиск по:");
        }

        void Button_search_click_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(button_search, $"Выполнить");
        }

        void PictureBox2_update_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(pictureBox2_update, $"Обновить БД");
        }

        void PictureBox1_clear_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(pictureBox1_clear, $"Очистить Control-ы");
        }

        void PictureBox_clear_BD_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
        }

        void PictureBox_copy_BD_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
        }

        void ST_WorkForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                toolTip1.Active = toolTip1.Active ? false : true;
            }
        }
        void TextBox_GD_city_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(textBox_GD_city, $"Например: Москва");
        }
        void TextBox_FIO_chief_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(textBox_FIO_chief, $"Например: Иванов И.А.");
        }

        void TextBox_doverennost_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(textBox_doverennost, $"Например: 11/23 от 10.01.2023 года");
        }

        void TextBox_FIO_Engineer_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(textBox_FIO_Engineer, $"Например: Иванов И.А.");
        }

        void TextBox_polinon_full_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(textBox_polinon_full, $"Горьковская ЖД");
        }

        void Button_information_remont_company_regedit_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(button_information_remont_company_regedit, $"Запись данных ПП в реестр");
        }

        void PictureBox_seach_datadrid_replay_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(pictureBox_seach_datadrid_replay, $"Отбразить отсутствующие РСТ исходя из выполнения предыдущего года");
        }

        #endregion

        #region при выборе строк ползьзователем и их подсчёт

        void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                decimal sum = 0;

                HashSet<int> rowIndexes = new HashSet<int>();

                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    if (!rowIndexes.Contains(cell.RowIndex))
                    {
                        rowIndexes.Add(cell.RowIndex);
                        sum += Convert.ToDecimal(dataGridView1.Rows[cell.RowIndex].Cells["price"].Value);
                    }
                }

                label_cell_rows.Text = rowIndexes.Count.ToString();
                label_sum_TO_selection.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        #endregion

        #region Функциональная панель
        void Close_Functional_loading_panel_Click(object sender, EventArgs e)
        {
            Functional_loading_panel.Visible = false;
            Functional_loading_panel.Enabled = false;
            dataGridView1.Enabled = true;
            panel1.Enabled = true;
            panel3.Enabled = true;
        }

        void Button_Functional_loading_panel(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                Block_ST_Work_Form_control();
                Functional_loading_panel.Visible = true;
                Functional_loading_panel.Enabled = true;
            }

        }

        #region добавление из файла

        async void Loading_file_current_BD_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool() == true)
            {
                clear_BD_current_year.Enabled = false;
                manual_backup_current_DB.Enabled = false;
                loading_json_file_BD.Enabled = false;
                button_Copying_current_BD_end_of_the_year.Enabled = false;
                button_Loading_file_last_year.Enabled = false;
                loading_file_full_BD.Enabled = false;
                loading_file_current_DB.Enabled = false;
                button_Uploading_JSON_file.Enabled = false;
                btn_Show_DB_radiostantion_last_year.Enabled = false;
                btn_Show_DB_radiostantion_full.Enabled = false;
                await Task.Run(() => Loading_file_current_BD());
                clear_BD_current_year.Enabled = true;
                manual_backup_current_DB.Enabled = true;
                loading_json_file_BD.Enabled = true;
                button_Copying_current_BD_end_of_the_year.Enabled = true;
                button_Loading_file_last_year.Enabled = true;
                loading_file_full_BD.Enabled = true;
                loading_file_current_DB.Enabled = true;
                button_Uploading_JSON_file.Enabled = true;
                btn_Show_DB_radiostantion_last_year.Enabled = true;
                btn_Show_DB_radiostantion_full.Enabled = true;
            }
        }
        void Loading_file_current_BD()
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    OpenFileDialog openFile = new OpenFileDialog();

                    openFile.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

                    ShowOpenFileDialogInvoker invoker = new ShowOpenFileDialogInvoker(openFile.ShowDialog);

                    this.Invoke(invoker);

                    if (openFile.FileName != "")
                    {
                        string filename = openFile.FileName;
                        string text = File.ReadAllText(filename);

                        var lineNumber = 0;

                        if (Internet_check.AvailabilityChanged_bool() == true)
                        {
                            using (var connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_db_2;password=war74_89;database=u1748936_root;charset=utf8"))
                            {
                                if (Internet_check.AvailabilityChanged_bool() == true)
                                {
                                    connection.Open();

                                    using (StreamReader reader = new StreamReader(filename))
                                    {
                                        while (!reader.EndOfStream)
                                        {
                                            var line = reader.ReadLine();

                                            if (lineNumber != 0)
                                            {
                                                var values = line.Split(';');

                                                if (!CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantion(values[4]))
                                                {

                                                    var mySql = $"insert into radiostantion (poligon, company, location, model, serialNumber, inventoryNumber, " +
                                                    $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, phoneNumber, " +
                                                    $"numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, completed_works_2, " +
                                                    $"completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1, parts_2, parts_3, " +
                                                    $"parts_4, parts_5, parts_6, parts_7 ) values ('{values[0].Trim()}', '{values[1].Trim()}', '{values[2].Trim()}', '{values[3].Trim()}', " +
                                                       $"'{values[4].Trim()}', '{values[5].Trim()}', '{values[6].Trim()}', '{values[7].Trim()}', " +
                                                       $"'{values[8].Trim()}','{values[9].Trim()}','{values[10].Trim()}','{""}','{""}','{""}','{""}'," +
                                                       $"'{""}','{""}','{""}','{0.00}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}'," +
                                                       $"'{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}')";

                                                    using (MySqlCommand command = new MySqlCommand(mySql, connection))
                                                    {
                                                        command.CommandText = mySql;
                                                        command.CommandType = System.Data.CommandType.Text;
                                                        command.ExecuteNonQuery();
                                                    }
                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                            }
                                            lineNumber++;
                                        }
                                        if (reader.EndOfStream == true)
                                        {
                                            MessageBox.Show("Радиостанции успешно добавлены!");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Радиостанции не добавленны.Системная ошибка ");
                                        }
                                    }
                                    connection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("1.Радиостанции не добавленны, нет соединения с интернетом.");
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("2.Радиостанции не добавленны, нет соединения с интернетом.");

                        }
                    }
                    else
                    {
                        string Mesage;
                        Mesage = "Вы не выбрали файл .csv который нужно добавить";

                        if (MessageBox.Show(Mesage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            return;
                        }
                    }

                }
                catch (Exception ex)
                {
                    string Mesage = $"Радиостанции не добавленны!";

                    if (MessageBox.Show(Mesage, "Обратите внимание на содержимое файла", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
            }
        }

        async void Button_Loading_file_last_year_Click(object sender, EventArgs e)
        {
            clear_BD_current_year.Enabled = false;
            manual_backup_current_DB.Enabled = false;
            loading_json_file_BD.Enabled = false;
            button_Copying_current_BD_end_of_the_year.Enabled = false;
            button_Loading_file_last_year.Enabled = false;
            loading_file_full_BD.Enabled = false;
            loading_file_current_DB.Enabled = false;
            button_Uploading_JSON_file.Enabled = false;
            btn_Show_DB_radiostantion_last_year.Enabled = false;
            btn_Show_DB_radiostantion_full.Enabled = false;
            await Task.Run(() => Loading_file_last_year());
            clear_BD_current_year.Enabled = true;
            manual_backup_current_DB.Enabled = true;
            loading_json_file_BD.Enabled = true;
            button_Copying_current_BD_end_of_the_year.Enabled = true;
            button_Loading_file_last_year.Enabled = true;
            loading_file_full_BD.Enabled = true;
            loading_file_current_DB.Enabled = true;
            button_Uploading_JSON_file.Enabled = true;
            btn_Show_DB_radiostantion_last_year.Enabled = true;
            btn_Show_DB_radiostantion_full.Enabled = true;
        }
        void Loading_file_last_year()
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    OpenFileDialog openFile = new OpenFileDialog();

                    openFile.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

                    ShowOpenFileDialogInvoker invoker = new ShowOpenFileDialogInvoker(openFile.ShowDialog);

                    this.Invoke(invoker);

                    if (openFile.FileName != "")
                    {
                        string filename = openFile.FileName;
                        string text = File.ReadAllText(filename);

                        var lineNumber = 0;

                        if (Internet_check.AvailabilityChanged_bool() == true)
                        {
                            using (var connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_db_2;password=war74_89;database=u1748936_root;charset=utf8"))
                            {
                                if (Internet_check.AvailabilityChanged_bool() == true)
                                {
                                    connection.Open();

                                    using (StreamReader reader = new StreamReader(filename))
                                    {
                                        while (!reader.EndOfStream)
                                        {
                                            var line = reader.ReadLine();

                                            if (lineNumber != 0)
                                            {
                                                var values = line.Split(';');

                                                if (!CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantion_last_year(values[4]))
                                                {
                                                    var mySql = $"insert into radiostantion_last_year (poligon, company, location, model, serialNumber, inventoryNumber, " +
                                                    $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, phoneNumber, " +
                                                    $"numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, completed_works_2, " +
                                                    $"completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1, parts_2, parts_3, " +
                                                    $"parts_4, parts_5, parts_6, parts_7 ) values ('{values[0].Trim()}', '{values[1].Trim()}', '{values[2].Trim()}', '{values[3].Trim()}', " +
                                                    $"'{values[4].Trim()}', '{values[5].Trim()}', '{values[6].Trim()}', '{values[7].Trim()}', " +
                                                    $"'{values[8].Trim()}','{values[9].Trim()}','{values[10].Trim()}','{""}','{""}','{""}','{""}'," +
                                                    $"'{""}','{""}','{""}','{0.00}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}'," +
                                                    $"'{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}')";

                                                    using (MySqlCommand command = new MySqlCommand(mySql, connection))
                                                    {
                                                        command.CommandText = mySql;
                                                        command.CommandType = System.Data.CommandType.Text;
                                                        command.ExecuteNonQuery();
                                                    }
                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                            }
                                            lineNumber++;
                                        }
                                        if (reader.EndOfStream == true)
                                        {
                                            MessageBox.Show("Радиостанции успешно добавлены!");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Радиостанции не добавленны.Системная ошибка ");
                                        }
                                    }
                                    connection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("1.Радиостанции не добавленны, нет соединения с интернетом.");
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("2.Радиостанции не добавленны, нет соединения с интернетом.");

                        }
                    }
                    else
                    {
                        string Mesage;
                        Mesage = "Вы не выбрали файл .csv который нужно добавить";

                        if (MessageBox.Show(Mesage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            return;
                        }
                    }

                }
                catch (Exception ex)
                {
                    string Mesage = $"Радиостанции не добавленны!";

                    if (MessageBox.Show(Mesage, "Обратите внимание на содержимое файла", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
            }
        }

        async void Loading_file_full_BD_Click(object sender, EventArgs e)
        {
            clear_BD_current_year.Enabled = false;
            manual_backup_current_DB.Enabled = false;
            loading_json_file_BD.Enabled = false;
            button_Copying_current_BD_end_of_the_year.Enabled = false;
            button_Loading_file_last_year.Enabled = false;
            loading_file_full_BD.Enabled = false;
            loading_file_current_DB.Enabled = false;
            button_Uploading_JSON_file.Enabled = false;
            btn_Show_DB_radiostantion_last_year.Enabled = false;
            btn_Show_DB_radiostantion_full.Enabled = false;
            await Task.Run(() => Loading_file_full_BD_method());
            clear_BD_current_year.Enabled = true;
            manual_backup_current_DB.Enabled = true;
            loading_json_file_BD.Enabled = true;
            button_Copying_current_BD_end_of_the_year.Enabled = true;
            button_Loading_file_last_year.Enabled = true;
            loading_file_full_BD.Enabled = true;
            loading_file_current_DB.Enabled = true;
            button_Uploading_JSON_file.Enabled = true;
            btn_Show_DB_radiostantion_last_year.Enabled = true;
            btn_Show_DB_radiostantion_full.Enabled = true;
        }
        void Loading_file_full_BD_method()
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    OpenFileDialog openFile = new OpenFileDialog();

                    openFile.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

                    ShowOpenFileDialogInvoker invoker = new ShowOpenFileDialogInvoker(openFile.ShowDialog);

                    this.Invoke(invoker);

                    if (openFile.FileName != "")
                    {
                        string filename = openFile.FileName;
                        string text = File.ReadAllText(filename);

                        var lineNumber = 0;

                        if (Internet_check.AvailabilityChanged_bool() == true)
                        {
                            using (var connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_db_2;password=war74_89;database=u1748936_root;charset=utf8"))
                            {
                                if (Internet_check.AvailabilityChanged_bool() == true)
                                {
                                    connection.Open();

                                    using (StreamReader reader = new StreamReader(filename))
                                    {
                                        while (!reader.EndOfStream)
                                        {
                                            var line = reader.ReadLine();

                                            if (lineNumber != 0)
                                            {
                                                var values = line.Split(';');

                                                if (!CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantion_full(values[4]))
                                                {
                                                    var mySql = $"insert into radiostantion_full (poligon, company, location, model, serialNumber, inventoryNumber, " +
                                                    $"networkNumber, dateTO, numberAct, city, price, representative, post, numberIdentification, dateIssue, phoneNumber, " +
                                                    $"numberActRemont, category, priceRemont, antenna, manipulator, AKB, batteryСharger, completed_works_1, completed_works_2, " +
                                                    $"completed_works_3, completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1, parts_2, parts_3, " +
                                                    $"parts_4, parts_5, parts_6, parts_7 ) values ('{values[0].Trim()}', '{values[1].Trim()}', '{values[2].Trim()}', '{values[3].Trim()}', " +
                                                    $"'{values[4].Trim()}', '{values[5].Trim()}', '{values[6].Trim()}', '{values[7].Trim()}', " +
                                                    $"'{(values[8].Replace(" ", "").Trim())}','{values[9].Trim()}','{values[10].Trim()}','{""}','{""}','{""}','{""}'," +
                                                    $"'{""}','{""}','{""}','{0.00}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}'," +
                                                    $"'{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}','{""}')";

                                                    using (MySqlCommand command = new MySqlCommand(mySql, connection))
                                                    {
                                                        command.CommandText = mySql;
                                                        command.CommandType = System.Data.CommandType.Text;
                                                        command.ExecuteNonQuery();
                                                    }
                                                }
                                                else
                                                {
                                                    continue;
                                                }
                                            }
                                            lineNumber++;
                                        }
                                        if (reader.EndOfStream == true)
                                        {
                                            MessageBox.Show("Радиостанции успешно добавлены!");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Радиостанции не добавленны.Системная ошибка ");
                                        }
                                    }
                                    connection.Close();
                                }
                                else
                                {
                                    MessageBox.Show("1.Радиостанции не добавленны, нет соединения с интернетом.");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("2.Радиостанции не добавленны, нет соединения с интернетом.");

                        }
                    }
                    else
                    {
                        string Mesage;
                        Mesage = "Вы не выбрали файл .csv который нужно добавить";

                        if (MessageBox.Show(Mesage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            return;
                        }
                    }

                }
                catch (Exception ex)
                {
                    string Mesage = $"Радиостанции не добавленны!";

                    if (MessageBox.Show(Mesage, "Обратите внимание на содержимое файла", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
            }
        }

        #endregion

        #region загрузка json в datagridview

        async void Loading_json_file_BD_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                clear_BD_current_year.Enabled = false;
                manual_backup_current_DB.Enabled = false;
                loading_json_file_BD.Enabled = false;
                button_Copying_current_BD_end_of_the_year.Enabled = false;
                button_Loading_file_last_year.Enabled = false;
                loading_file_full_BD.Enabled = false;
                loading_file_current_DB.Enabled = false;
                button_Uploading_JSON_file.Enabled = false;
                btn_Show_DB_radiostantion_last_year.Enabled = false;
                btn_Show_DB_radiostantion_full.Enabled = false;
                await Task.Run(() => Loading_json_file_BD_method());
                clear_BD_current_year.Enabled = true;
                manual_backup_current_DB.Enabled = true;
                loading_json_file_BD.Enabled = true;
                button_Copying_current_BD_end_of_the_year.Enabled = true;
                button_Loading_file_last_year.Enabled = true;
                loading_file_full_BD.Enabled = true;
                loading_file_current_DB.Enabled = true;
                button_Uploading_JSON_file.Enabled = true;
                btn_Show_DB_radiostantion_last_year.Enabled = true;
                btn_Show_DB_radiostantion_full.Enabled = true;
            }

        }
        void Loading_json_file_BD_method()
        {
            try
            {
                dataGridView2.Columns.Add("id", "№");
                dataGridView2.Columns.Add("poligon", "Полигон");
                dataGridView2.Columns.Add("company", "Предприятие");
                dataGridView2.Columns.Add("location", "Место нахождения");
                dataGridView2.Columns.Add("model", "Модель радиостанции");
                dataGridView2.Columns.Add("serialNumber", "Заводской номер");
                dataGridView2.Columns.Add("inventoryNumber", "Инвентарный номер");
                dataGridView2.Columns.Add("networkNumber", "Сетевой номер");
                dataGridView2.Columns.Add("dateTO", "Дата ТО");
                dataGridView2.Columns.Add("numberAct", "№ акта ТО");
                dataGridView2.Columns.Add("city", "Город");
                dataGridView2.Columns.Add("price", "Цена ТО");
                dataGridView2.Columns.Add("representative", "Представитель предприятия");
                dataGridView2.Columns.Add("post", "Должность");
                dataGridView2.Columns.Add("numberIdentification", "Номер удостоверения");
                dataGridView2.Columns.Add("dateIssue", "Дата выдачи удостоверения");
                dataGridView2.Columns.Add("phoneNumber", "Номер телефона");
                dataGridView2.Columns.Add("numberActRemont", "№ акта ремонта");
                dataGridView2.Columns.Add("category", "Категория");
                dataGridView2.Columns.Add("priceRemont", "Цена ремонта");
                dataGridView2.Columns.Add("antenna", "Антенна");
                dataGridView2.Columns.Add("manipulator", "Манипулятор");
                dataGridView2.Columns.Add("AKB", "АКБ");
                dataGridView2.Columns.Add("batteryСharger", "ЗУ");
                dataGridView2.Columns.Add("completed_works_1", "Выполненные работы_1");
                dataGridView2.Columns.Add("completed_works_2", "Выполненные работы_1");
                dataGridView2.Columns.Add("completed_works_3", "Выполненные работы_1");
                dataGridView2.Columns.Add("completed_works_4", "Выполненные работы_1");
                dataGridView2.Columns.Add("completed_works_5", "Выполненные работы_1");
                dataGridView2.Columns.Add("completed_works_6", "Выполненные работы_1");
                dataGridView2.Columns.Add("completed_works_7", "Выполненные работы_1");
                dataGridView2.Columns.Add("parts_1", "Израсходованные материалы и детали_1");
                dataGridView2.Columns.Add("parts_2", "Израсходованные материалы и детали_2");
                dataGridView2.Columns.Add("parts_3", "Израсходованные материалы и детали_3");
                dataGridView2.Columns.Add("parts_4", "Израсходованные материалы и детали_4");
                dataGridView2.Columns.Add("parts_5", "Израсходованные материалы и детали_5");
                dataGridView2.Columns.Add("parts_6", "Израсходованные материалы и детали_6");
                dataGridView2.Columns.Add("parts_7", "Израсходованные материалы и детали_7");
                dataGridView2.Columns.Add("decommissionSerialNumber", "Номер  акта списания");
                dataGridView2.Columns.Add("comment", "Примечание");
                dataGridView2.Columns.Add("IsNew", String.Empty);

                if (File.Exists("s.json"))
                {
                    dataGridView2.Rows.Clear();
                    string result;
                    using (var reader = new StreamReader("s.json"))
                    {
                        result = reader.ReadToEnd();
                    }

                    JArray fetch = JArray.Parse(result);

                    if (fetch.Count() > 0)
                    {
                        for (int i = 0; fetch.Count() > i; i++)
                        {
                            int n = dataGridView2.Rows.Add();
                            dataGridView2.Rows[n].Cells[0].Value = fetch[i]["id"].ToString();
                            dataGridView2.Rows[n].Cells[1].Value = fetch[i]["poligon"].ToString();
                            dataGridView2.Rows[n].Cells[2].Value = fetch[i]["company"].ToString();
                            dataGridView2.Rows[n].Cells[3].Value = fetch[i]["location"].ToString();
                            dataGridView2.Rows[n].Cells[4].Value = fetch[i]["model"].ToString();
                            dataGridView2.Rows[n].Cells[5].Value = fetch[i]["serialNumber"].ToString();
                            dataGridView2.Rows[n].Cells[6].Value = fetch[i]["inventoryNumber"].ToString();
                            dataGridView2.Rows[n].Cells[7].Value = fetch[i]["networkNumber"].ToString();
                            dataGridView2.Rows[n].Cells[8].Value = fetch[i]["dateTO"].ToString();
                            dataGridView2.Rows[n].Cells[9].Value = fetch[i]["numberAct"].ToString();
                            dataGridView2.Rows[n].Cells[10].Value = fetch[i]["city"].ToString();
                            dataGridView2.Rows[n].Cells[11].Value = fetch[i]["price"].ToString();
                            dataGridView2.Rows[n].Cells[12].Value = fetch[i]["representative"].ToString();
                            dataGridView2.Rows[n].Cells[13].Value = fetch[i]["post"].ToString();
                            dataGridView2.Rows[n].Cells[14].Value = fetch[i]["numberIdentification"].ToString();
                            dataGridView2.Rows[n].Cells[15].Value = fetch[i]["dateIssue"].ToString();
                            dataGridView2.Rows[n].Cells[16].Value = fetch[i]["phoneNumber"].ToString();
                            dataGridView2.Rows[n].Cells[17].Value = fetch[i]["numberActRemont"].ToString();
                            dataGridView2.Rows[n].Cells[18].Value = fetch[i]["category"].ToString();
                            dataGridView2.Rows[n].Cells[19].Value = fetch[i]["priceRemont"].ToString();
                            dataGridView2.Rows[n].Cells[20].Value = fetch[i]["antenna"].ToString();
                            dataGridView2.Rows[n].Cells[21].Value = fetch[i]["manipulator"].ToString();
                            dataGridView2.Rows[n].Cells[22].Value = fetch[i]["AKB"].ToString();
                            dataGridView2.Rows[n].Cells[23].Value = fetch[i]["batteryСharger"].ToString();
                            dataGridView2.Rows[n].Cells[24].Value = fetch[i]["completed_works_1"].ToString();
                            dataGridView2.Rows[n].Cells[25].Value = fetch[i]["completed_works_2"].ToString();
                            dataGridView2.Rows[n].Cells[26].Value = fetch[i]["completed_works_3"].ToString();
                            dataGridView2.Rows[n].Cells[27].Value = fetch[i]["completed_works_4"].ToString();
                            dataGridView2.Rows[n].Cells[28].Value = fetch[i]["completed_works_5"].ToString();
                            dataGridView2.Rows[n].Cells[29].Value = fetch[i]["completed_works_6"].ToString();
                            dataGridView2.Rows[n].Cells[30].Value = fetch[i]["completed_works_7"].ToString();
                            dataGridView2.Rows[n].Cells[31].Value = fetch[i]["parts_1"].ToString();
                            dataGridView2.Rows[n].Cells[32].Value = fetch[i]["parts_2"].ToString();
                            dataGridView2.Rows[n].Cells[33].Value = fetch[i]["parts_3"].ToString();
                            dataGridView2.Rows[n].Cells[34].Value = fetch[i]["parts_4"].ToString();
                            dataGridView2.Rows[n].Cells[35].Value = fetch[i]["parts_5"].ToString();
                            dataGridView2.Rows[n].Cells[36].Value = fetch[i]["parts_6"].ToString();
                            dataGridView2.Rows[n].Cells[37].Value = fetch[i]["parts_7"].ToString();
                            dataGridView2.Rows[n].Cells[38].Value = fetch[i]["decommissionSerialNumber"].ToString();
                            dataGridView2.Rows[n].Cells[38].Value = fetch[i]["comment"].ToString();
                        }
                    }
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        var id = dataGridView2.Rows[i].Cells["id"].Value;
                        var poligon = dataGridView2.Rows[i].Cells["poligon"].Value.ToString();
                        var company = dataGridView2.Rows[i].Cells["company"].Value.ToString();
                        var location = dataGridView2.Rows[i].Cells["location"].Value.ToString();
                        var model = dataGridView2.Rows[i].Cells["model"].Value.ToString();
                        var serialNumber = dataGridView2.Rows[i].Cells["serialNumber"].Value.ToString();
                        var inventoryNumber = dataGridView2.Rows[i].Cells["inventoryNumber"].Value.ToString();
                        var networkNumber = dataGridView2.Rows[i].Cells["networkNumber"].Value.ToString();
                        var dateTO = dataGridView2.Rows[i].Cells["dateTO"].Value.ToString();
                        var numberAct = dataGridView2.Rows[i].Cells["numberAct"].Value.ToString();
                        var city = dataGridView2.Rows[i].Cells["city"].Value.ToString();
                        var price = dataGridView2.Rows[i].Cells["price"].Value;
                        var representative = dataGridView2.Rows[i].Cells["representative"].Value.ToString();
                        var post = dataGridView2.Rows[i].Cells["post"].Value.ToString();
                        var numberIdentification = dataGridView2.Rows[i].Cells["numberIdentification"].Value.ToString();
                        var dateIssue = dataGridView2.Rows[i].Cells["dateIssue"].Value.ToString();
                        var phoneNumber = dataGridView2.Rows[i].Cells["phoneNumber"].Value.ToString();
                        var numberActRemont = dataGridView2.Rows[i].Cells["numberActRemont"].Value.ToString();
                        var category = dataGridView2.Rows[i].Cells["category"].Value.ToString();
                        var priceRemont = dataGridView2.Rows[i].Cells["priceRemont"].Value;
                        var antenna = dataGridView2.Rows[i].Cells["antenna"].Value.ToString();
                        var manipulator = dataGridView2.Rows[i].Cells["antenna"].Value.ToString();
                        var AKB = dataGridView2.Rows[i].Cells["AKB"].Value.ToString();
                        var batteryСharger = dataGridView2.Rows[i].Cells["batteryСharger"].Value.ToString();
                        var completed_works_1 = dataGridView2.Rows[i].Cells["completed_works_1"].Value.ToString();
                        var completed_works_2 = dataGridView2.Rows[i].Cells["completed_works_2"].Value.ToString();
                        var completed_works_3 = dataGridView2.Rows[i].Cells["completed_works_3"].Value.ToString();
                        var completed_works_4 = dataGridView2.Rows[i].Cells["completed_works_4"].Value.ToString();
                        var completed_works_5 = dataGridView2.Rows[i].Cells["completed_works_5"].Value.ToString();
                        var completed_works_6 = dataGridView2.Rows[i].Cells["completed_works_6"].Value.ToString();
                        var completed_works_7 = dataGridView2.Rows[i].Cells["completed_works_7"].Value.ToString();
                        var parts_1 = dataGridView2.Rows[i].Cells["parts_1"].Value.ToString();
                        var parts_2 = dataGridView2.Rows[i].Cells["parts_2"].Value.ToString();
                        var parts_3 = dataGridView2.Rows[i].Cells["parts_3"].Value.ToString();
                        var parts_4 = dataGridView2.Rows[i].Cells["parts_4"].Value.ToString();
                        var parts_5 = dataGridView2.Rows[i].Cells["parts_5"].Value.ToString();
                        var parts_6 = dataGridView2.Rows[i].Cells["parts_6"].Value.ToString();
                        var parts_7 = dataGridView2.Rows[i].Cells["parts_7"].Value.ToString();
                        var decommissionSerialNumber = dataGridView2.Rows[i].Cells["decommissionSerialNumber"].Value.ToString();
                        var comment = dataGridView2.Rows[i].Cells["comment"].Value.ToString();

                        string queryString = $"UPDATE radiostantion SET poligon = '{poligon}', company = '{company}', location = '{location}', " +
                            $"model = '{model}', serialNumber = '{serialNumber}', inventoryNumber = '{inventoryNumber}', networkNumber = '{networkNumber}', " +
                            $"dateTO = '{dateTO}', numberAct = '{numberAct}', city = '{city}', price = '{price}', representative = '{representative}', " +
                            $"post = '{post}', numberIdentification = '{numberIdentification}', dateIssue = '{dateIssue}', phoneNumber = '{phoneNumber}', " +
                            $"numberActRemont = '{numberActRemont}', category = '{category}', priceRemont = '{priceRemont}', antenna = '{antenna}', " +
                            $"manipulator = '{manipulator}', AKB = '{AKB}', batteryСharger = '{batteryСharger}', completed_works_1 = '{completed_works_1}', " +
                            $"completed_works_2 = '{completed_works_2}', completed_works_3 = '{completed_works_3}', completed_works_4 = '{completed_works_4}', " +
                            $"completed_works_5 = '{completed_works_5}', completed_works_6 = '{completed_works_6}', completed_works_7 = '{completed_works_7}', " +
                            $"parts_1 = '{parts_1}', parts_2 = '{parts_2}', parts_3 = '{parts_3}',  parts_4 = '{parts_4}',  parts_5 = '{parts_5}', parts_6 = '{parts_6}',  " +
                            $"parts_7 = '{parts_7}', decommissionSerialNumber = '{decommissionSerialNumber}', comment = '{comment}'  WHERE id = '{id}'";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB_2.GetInstance.GetConnection()))
                        {
                            DB_2.GetInstance.openConnection();
                            command.ExecuteNonQuery();
                            DB_2.GetInstance.closeConnection();

                        }
                    }
                }
                MessageBox.Show("Радиостанции успешно загруженны из JSON");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region выгрузка всех данных из datagrid

        async void Button_Uploading_JSON_file_Click(object sender, EventArgs e)
        {
            clear_BD_current_year.Enabled = false;
            manual_backup_current_DB.Enabled = false;
            loading_json_file_BD.Enabled = false;
            button_Copying_current_BD_end_of_the_year.Enabled = false;
            button_Loading_file_last_year.Enabled = false;
            loading_file_full_BD.Enabled = false;
            loading_file_current_DB.Enabled = false;
            button_Uploading_JSON_file.Enabled = false;
            btn_Show_DB_radiostantion_last_year.Enabled = false;
            btn_Show_DB_radiostantion_full.Enabled = false;
            await Task.Run(() => Get_date_save_datagridview());
            clear_BD_current_year.Enabled = true;
            manual_backup_current_DB.Enabled = true;
            loading_json_file_BD.Enabled = true;
            button_Copying_current_BD_end_of_the_year.Enabled = true;
            button_Loading_file_last_year.Enabled = true;
            loading_file_full_BD.Enabled = true;
            loading_file_current_DB.Enabled = true;
            button_Uploading_JSON_file.Enabled = true;
            btn_Show_DB_radiostantion_last_year.Enabled = true;
            btn_Show_DB_radiostantion_full.Enabled = true;
        }
        void Get_date_save_datagridview()
        {
            try
            {
                JArray products = new JArray();

                foreach (DataGridViewRow row in dataGridView1.Rows)
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
                        comment = row.Cells[38].Value
                    });
                    products.Add(product);
                }

                string json = JsonConvert.SerializeObject(products);

                File.WriteAllText("s.json", json);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); ;
            }
        }

        #endregion

        #region копирование текущей таблицы radiostantion в radiostantion_last_year к концу года 

        /// <summary>
        /// Копирование текущей таблицы radiostantion в radiostantion_last_year
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_Copying_current_BD_end_of_the_year_Click(object sender, EventArgs e)
        {
            try
            {
                string Mesage;
                Mesage = "Вы действительно хотите скопировать всю базу данных?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                string Mesage2;
                Mesage2 = "Данное действие нужно делать к концу года, для следующего года, действительно хотите продолжить?";

                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                clear_BD_current_year.Enabled = false;
                manual_backup_current_DB.Enabled = false;
                loading_json_file_BD.Enabled = false;
                button_Copying_current_BD_end_of_the_year.Enabled = false;
                button_Loading_file_last_year.Enabled = false;
                loading_file_full_BD.Enabled = false;
                loading_file_current_DB.Enabled = false;
                button_Uploading_JSON_file.Enabled = false;
                btn_Show_DB_radiostantion_last_year.Enabled = false;
                btn_Show_DB_radiostantion_full.Enabled = false;
                var clearBD = "TRUNCATE TABLE radiostantion_last_year";

                using (MySqlCommand command = new MySqlCommand(clearBD, DB_2.GetInstance.GetConnection()))
                {
                    DB_2.GetInstance.openConnection();
                    command.ExecuteNonQuery();
                    DB_2.GetInstance.closeConnection();
                }

                var copyBD = "INSERT INTO radiostantion_last_year SELECT * FROM radiostantion";

                using (MySqlCommand command2 = new MySqlCommand(copyBD, DB_2.GetInstance.GetConnection()))
                {
                    DB_2.GetInstance.openConnection();
                    command2.ExecuteNonQuery();
                    DB_2.GetInstance.closeConnection();
                }

                MessageBox.Show("База данных успешно скопирована!");

                clear_BD_current_year.Enabled = true;
                manual_backup_current_DB.Enabled = true;
                loading_json_file_BD.Enabled = true;
                button_Copying_current_BD_end_of_the_year.Enabled = true;
                button_Loading_file_last_year.Enabled = true;
                loading_file_full_BD.Enabled = true;
                loading_file_current_DB.Enabled = true;
                button_Uploading_JSON_file.Enabled = true;
                btn_Show_DB_radiostantion_last_year.Enabled = true;
                btn_Show_DB_radiostantion_full.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); ;
            }
        }
        #endregion

        #region функцональная панель ручное-резервное копирование радиостанций из текущей radiostantion в radiostantion_copy
        /// <summary>
        /// Копирование данных БД(radiostantion) в резерв (radiostantion_copy) для дальнейшего пользования к концу года
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        void Manual_backup_current_DB_Click(object sender, EventArgs e)
        {
            try
            {
                string Mesage;
                Mesage = "Вы действительно хотите скопировать всю базу данных?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                clear_BD_current_year.Enabled = false;
                manual_backup_current_DB.Enabled = false;
                loading_json_file_BD.Enabled = false;
                button_Copying_current_BD_end_of_the_year.Enabled = false;
                button_Loading_file_last_year.Enabled = false;
                loading_file_full_BD.Enabled = false;
                loading_file_current_DB.Enabled = false;
                button_Uploading_JSON_file.Enabled = false;
                btn_Show_DB_radiostantion_last_year.Enabled = false;
                btn_Show_DB_radiostantion_full.Enabled = false;
                var clearBD = "TRUNCATE TABLE radiostantion_copy";

                using (MySqlCommand command = new MySqlCommand(clearBD, DB_2.GetInstance.GetConnection()))
                {
                    DB_2.GetInstance.openConnection();
                    command.ExecuteNonQuery();
                    DB_2.GetInstance.closeConnection();
                }

                var copyBD = "INSERT INTO radiostantion_copy SELECT * FROM radiostantion";

                using (MySqlCommand command2 = new MySqlCommand(copyBD, DB_2.GetInstance.GetConnection()))
                {
                    DB_2.GetInstance.openConnection();
                    command2.ExecuteNonQuery();
                    DB_2.GetInstance.closeConnection();
                }
                MessageBox.Show("База данных успешно скопирована!");

                clear_BD_current_year.Enabled = true;
                manual_backup_current_DB.Enabled = true;
                loading_json_file_BD.Enabled = true;
                button_Copying_current_BD_end_of_the_year.Enabled = true;
                button_Loading_file_last_year.Enabled = true;
                loading_file_full_BD.Enabled = true;
                loading_file_current_DB.Enabled = true;
                button_Uploading_JSON_file.Enabled = true;
                btn_Show_DB_radiostantion_last_year.Enabled = true;
                btn_Show_DB_radiostantion_full.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); ;
            }
        }
        #endregion

        #region очистка текущей БД, текущий год (radiostantion)
        /// <summary>
        /// очистка БД (radiostantion)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Clear_BD_current_year_Click(object sender, EventArgs e)
        {
            try
            {
                string Mesage;
                Mesage = "Вы действительно хотите удалить всё содержимое базы данных?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                string Mesage2;
                Mesage2 = "Всё удалится безвозратно!!!Точно хотите удалить всё содержимое Базы данных?";

                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                clear_BD_current_year.Enabled = false;
                manual_backup_current_DB.Enabled = false;
                loading_json_file_BD.Enabled = false;
                button_Copying_current_BD_end_of_the_year.Enabled = false;
                button_Loading_file_last_year.Enabled = false;
                loading_file_full_BD.Enabled = false;
                loading_file_current_DB.Enabled = false;
                button_Uploading_JSON_file.Enabled = false;
                btn_Show_DB_radiostantion_last_year.Enabled = false;
                btn_Show_DB_radiostantion_full.Enabled = false;

                var clearBD = "TRUNCATE TABLE radiostantion";

                using (MySqlCommand command = new MySqlCommand(clearBD, DB_2.GetInstance.GetConnection()))
                {
                    DB_2.GetInstance.openConnection();
                    command.ExecuteNonQuery();
                    DB_2.GetInstance.closeConnection();
                }

                MessageBox.Show("База данных успешно очищенна!");
                RefreshDataGrid(dataGridView1);

                clear_BD_current_year.Enabled = true;
                manual_backup_current_DB.Enabled = true;
                loading_json_file_BD.Enabled = true;
                button_Copying_current_BD_end_of_the_year.Enabled = true;
                button_Loading_file_last_year.Enabled = true;
                loading_file_full_BD.Enabled = true;
                loading_file_current_DB.Enabled = true;
                button_Uploading_JSON_file.Enabled = true;
                btn_Show_DB_radiostantion_last_year.Enabled = true;
                btn_Show_DB_radiostantion_full.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); ;
            }
        }

        #endregion

        #region показать БД прошлго года по участку

        void Btn_Show_DB_radiostantion_last_year_Click(object sender, EventArgs e)
        {
            clear_BD_current_year.Enabled = false;
            manual_backup_current_DB.Enabled = false;
            loading_json_file_BD.Enabled = false;
            button_Copying_current_BD_end_of_the_year.Enabled = false;
            button_Loading_file_last_year.Enabled = false;
            loading_file_full_BD.Enabled = false;
            loading_file_current_DB.Enabled = false;
            button_Uploading_JSON_file.Enabled = false;
            btn_Show_DB_radiostantion_last_year.Enabled = false;
            btn_Show_DB_radiostantion_full.Enabled = false;
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    Close_Functional_loading_panel_Click(sender, e);
                    panel1.Enabled = false;
                    panel3.Enabled = false;
                    if (comboBox_city.Text != "")
                    {
                        var myCulture = new CultureInfo("ru-RU");
                        myCulture.NumberFormat.NumberDecimalSeparator = ".";
                        Thread.CurrentThread.CurrentCulture = myCulture;
                        dataGridView1.Rows.Clear();
                        string queryString = $"SELECT * FROM radiostantion_last_year WHERE city LIKE N'%{comboBox_city.Text.Trim()}%'";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB_2.GetInstance.GetConnection()))
                        {
                            DB_2.GetInstance.openConnection();

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        ReedSingleRow(dataGridView1, reader);
                                    }
                                    reader.Close();
                                }
                            }
                            command.ExecuteNonQuery();
                            DB_2.GetInstance.closeConnection();
                        }
                    }

                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                    dataGridView1.Columns[0].Width = 45;
                    dataGridView1.Columns[3].Width = 170;
                    dataGridView1.Columns[4].Width = 180;
                    dataGridView1.Columns[5].Width = 150;
                    dataGridView1.Columns[6].Width = 178;
                    dataGridView1.Columns[7].Width = 178;
                    dataGridView1.Columns[8].Width = 100;
                    dataGridView1.Columns[9].Width = 110;
                    dataGridView1.Columns[10].Width = 100;
                    dataGridView1.Columns[11].Width = 100;
                    dataGridView1.Columns[17].Width = 120;
                    dataGridView1.Columns[39].Width = 300;
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
            clear_BD_current_year.Enabled = true;
            manual_backup_current_DB.Enabled = true;
            loading_json_file_BD.Enabled = true;
            button_Copying_current_BD_end_of_the_year.Enabled = true;
            button_Loading_file_last_year.Enabled = true;
            loading_file_full_BD.Enabled = true;
            loading_file_current_DB.Enabled = true;
            button_Uploading_JSON_file.Enabled = true;
            btn_Show_DB_radiostantion_last_year.Enabled = true;
            btn_Show_DB_radiostantion_full.Enabled = true;
            UpdateCountRST();
            UpdateSumTOrst();
            UpdateSumTOrstRemont();
            UpdateCountRSTRemont();
        }


        #endregion

        #region показать общую БД по всем радиостанциям

        void btn_Show_DB_radiostantion_full_Click(object sender, EventArgs e)
        {
            clear_BD_current_year.Enabled = false;
            manual_backup_current_DB.Enabled = false;
            loading_json_file_BD.Enabled = false;
            button_Copying_current_BD_end_of_the_year.Enabled = false;
            button_Loading_file_last_year.Enabled = false;
            loading_file_full_BD.Enabled = false;
            loading_file_current_DB.Enabled = false;
            button_Uploading_JSON_file.Enabled = false;
            btn_Show_DB_radiostantion_last_year.Enabled = false;
            btn_Show_DB_radiostantion_full.Enabled = false;
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    Close_Functional_loading_panel_Click(sender, e);
                    panel1.Enabled = false;
                    panel3.Enabled = false;
                    if (comboBox_city.Text != "")
                    {
                        var myCulture = new CultureInfo("ru-RU");
                        myCulture.NumberFormat.NumberDecimalSeparator = ".";
                        Thread.CurrentThread.CurrentCulture = myCulture;
                        dataGridView1.Rows.Clear();
                        string queryString = $"SELECT * FROM radiostantion_full";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB_2.GetInstance.GetConnection()))
                        {
                            DB_2.GetInstance.openConnection();

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        ReedSingleRow(dataGridView1, reader);
                                    }
                                    reader.Close();
                                }
                            }
                            command.ExecuteNonQuery();
                            DB_2.GetInstance.closeConnection();
                        }
                    }

                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                    dataGridView1.Columns[0].Width = 45;
                    dataGridView1.Columns[3].Width = 170;
                    dataGridView1.Columns[4].Width = 180;
                    dataGridView1.Columns[5].Width = 150;
                    dataGridView1.Columns[6].Width = 178;
                    dataGridView1.Columns[7].Width = 178;
                    dataGridView1.Columns[8].Width = 100;
                    dataGridView1.Columns[9].Width = 110;
                    dataGridView1.Columns[10].Width = 100;
                    dataGridView1.Columns[11].Width = 100;
                    dataGridView1.Columns[17].Width = 120;
                    dataGridView1.Columns[39].Width = 300;
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
            clear_BD_current_year.Enabled = true;
            manual_backup_current_DB.Enabled = true;
            loading_json_file_BD.Enabled = true;
            button_Copying_current_BD_end_of_the_year.Enabled = true;
            button_Loading_file_last_year.Enabled = true;
            loading_file_full_BD.Enabled = true;
            loading_file_current_DB.Enabled = true;
            button_Uploading_JSON_file.Enabled = true;
            btn_Show_DB_radiostantion_last_year.Enabled = true;
            btn_Show_DB_radiostantion_full.Enabled = true;
            UpdateCountRST();
            UpdateSumTOrst();
            UpdateSumTOrstRemont();
            UpdateCountRSTRemont();
        }
        #endregion

        #endregion

        #region close form
        void ST_WorkForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void ST_WorkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = FormClose.GetInstance.FClose();
        }
        #endregion

        #region подсветка акта цветом и его подсчёт

        void DataGridView1_DefaultCellStyleChanged(object sender, EventArgs e)
        {
            if (textBox_numberAct.Text != "")
            {
                int c = 0;
                decimal sum = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["numberAct"].Value.ToString().Equals(textBox_numberAct.Text))
                    {
                        //dataGridView1.Columns["numberAct"].DefaultCellStyle.ForeColor = Color.Gray;
                        dataGridView1.Rows[i].Cells["numberAct"].Style.BackColor = Color.Red;
                        sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells["price"].Value);
                        c++;
                    }
                }

                label_complete.Visible = true;
                lbl_full_complete_act.Visible = true;
                //lbl_full_complete_act.Text += textBox_numberAct.Text + ", ";
                if(lbl_full_complete_act.Text != $"{textBox_numberAct.Text} - {textBox_company.Text} - {c} шт.,")

                try
                {
                    RegistryKey currentUserKey = Registry.CurrentUser;
                    RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                    helloKey.SetValue("Акты_незаполненные", $"{lbl_full_complete_act.Text}");
                    helloKey.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                //label_cell_rows.Text = c.ToString();
                //label_sum_TO_selection.Text = sum.ToString();
            }
        }
        #region для редактирования актов заполняемых до конца

        void Lbl_full_complete_act_DoubleClick(object sender, EventArgs e)
        {
            lbl_full_complete_act.Visible = false;
            txB_lbl_full_complete_act.Text = lbl_full_complete_act.Text;
            txB_lbl_full_complete_act.Visible = true;
        }

        void Panel3_Click(object sender, EventArgs e)
        {
            if (txB_lbl_full_complete_act.Visible)
            {
                try
                {
                    RegistryKey currentUserKey = Registry.CurrentUser;
                    RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                    helloKey.SetValue("Акты_незаполненные", $"{txB_lbl_full_complete_act.Text}");
                    helloKey.Close();

                    RegistryKey reg2 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                    if (reg2 != null)
                    {
                        RegistryKey currentUserKey2 = Registry.CurrentUser;
                        RegistryKey helloKey2 = currentUserKey2.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                        lbl_full_complete_act.Text = helloKey2.GetValue("Акты_незаполненные").ToString();

                        label_complete.Visible = true;
                        lbl_full_complete_act.Visible = true;
                        txB_lbl_full_complete_act.Visible = false;

                        helloKey2.Close();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        void TxB_lbl_full_complete_act_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (txB_lbl_full_complete_act.Visible)
                {
                    try
                    {
                        RegistryKey currentUserKey = Registry.CurrentUser;
                        RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                        helloKey.SetValue("Акты_незаполненные", $"{txB_lbl_full_complete_act.Text}");
                        helloKey.Close();

                        RegistryKey reg2 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                        if (reg2 != null)
                        {
                            RegistryKey currentUserKey2 = Registry.CurrentUser;
                            RegistryKey helloKey2 = currentUserKey2.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                            lbl_full_complete_act.Text = helloKey2.GetValue("Акты_незаполненные").ToString();

                            label_complete.Visible = true;
                            lbl_full_complete_act.Visible = true;
                            txB_lbl_full_complete_act.Visible = false;

                            helloKey2.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        #endregion

        #endregion

        #region списание РСТ

        void Btn_decommissionSerialNumber_close_Click(object sender, EventArgs e)
        {
            panel_decommissionSerialNumber.Visible = false;
            panel_decommissionSerialNumber.Enabled = false;
        }

        void DecommissionSerialNumber(object sender, EventArgs e)
        {
            if (textBox_serialNumber.Text != "")
            {
                string Mesage;
                Mesage = "Вы действительно хотите списать радиостанцию?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                panel1.Enabled = false;
                panel2.Enabled = false;
                panel3.Enabled = false;
                dataGridView1.Enabled = false;
                panel_decommissionSerialNumber.Visible = true;
                panel_decommissionSerialNumber.Enabled = true;
            }

        }

        void Btn_record_decommissionSerialNumber_Click(object sender, EventArgs e)
        {
            if (textBox_decommissionSerialNumber.Text != "")
            {
                if (Internet_check.AvailabilityChanged_bool())
                {
                    try
                    {
                        string serialNumber = textBox_serialNumber.Text;
                        var decommissionSerialNumber = textBox_decommissionSerialNumber.Text;
                        if (textBox_serialNumber.Text != "")
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
                                var city = textBox_city.Text;
                                var poligon = comboBox_poligon.Text;
                                var company = textBox_company.Text;
                                var location = textBox_location.Text;
                                var model = comboBox_model.Text;
                                var inventoryNumber = textBox_inventoryNumber.Text;
                                var networkNumber = textBox_networkNumber.Text;
                                var numberAct = textBox_numberAct.Text;
                                var dateTO = textBox_dateTO.Text;
                                var price = textBox_price.Text;
                                var representative = textBox_representative.Text;
                                var post = textBox_post.Text;
                                var numberIdentification = textBox_numberIdentification.Text;
                                var dateIssue = textBox_dateIssue.Text;
                                var phoneNumber = textBox_phoneNumber.Text;
                                var antenna = textBox_antenna.Text;
                                var manipulator = textBox_manipulator.Text;
                                var AKB = textBox_AKB.Text;
                                var batteryСharger = textBox_batteryСharger.Text;
                                var comment = txB_comment.Text;

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

                        Button_update_Click(sender, e);
                        panel_decommissionSerialNumber.Visible = false;
                        panel_decommissionSerialNumber.Enabled = false;
                        panel1.Enabled = true;
                        panel2.Enabled = true;
                        panel3.Enabled = true;
                        dataGridView1.Enabled = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else { MessageBox.Show("Вы не заполнили поле Номер Акта Списания!"); }
        }

        #region показать списания
        void Show_radiostantion_decommission_Click(object sender, EventArgs e)
        {

            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    panel1.Enabled = false;
                    panel3.Enabled = false;

                    if (comboBox_city.Text != "")
                    {
                        var myCulture = new CultureInfo("ru-RU");
                        myCulture.NumberFormat.NumberDecimalSeparator = ".";
                        Thread.CurrentThread.CurrentCulture = myCulture;
                        dataGridView1.Rows.Clear();
                        string queryString = $"SELECT * FROM radiostantion_decommission WHERE city LIKE N'%{comboBox_city.Text.Trim()}%'";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.openConnection();

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        ReedSingleRow(dataGridView1, reader);
                                    }
                                    reader.Close();
                                }
                            }
                            command.ExecuteNonQuery();
                            DB.GetInstance.closeConnection();
                        }
                    }

                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                    dataGridView1.Columns[0].Width = 45;
                    dataGridView1.Columns[3].Width = 170;
                    dataGridView1.Columns[4].Width = 180;
                    dataGridView1.Columns[5].Width = 150;
                    dataGridView1.Columns[6].Width = 178;
                    dataGridView1.Columns[7].Width = 178;
                    dataGridView1.Columns[8].Width = 100;
                    dataGridView1.Columns[9].Width = 110;
                    dataGridView1.Columns[10].Width = 100;
                    dataGridView1.Columns[11].Width = 100;
                    dataGridView1.Columns[17].Width = 120;

                    UpdateCountRST();
                    UpdateSumTOrst();

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

        #region сформировать акт списания

        void PrintWord_Act_decommission(object sender, EventArgs e)
        {
            if (txB_decommissionSerialNumber.Text != "")
            {
                string decommissionSerialNumber_company = $"{txB_decommissionSerialNumber.Text}-{textBox_company.Text}";
                DateTime dateTime = DateTime.Today;
                string dateDecommission = dateTime.ToString("dd.MM.yyyy");
                string city = textBox_city.Text;

                var items = new Dictionary<string, string>
                {
                    {"<numberActTZPP>", decommissionSerialNumber_company },
                    {"<model>", comboBox_model.Text },
                    {"<serialNumber>", textBox_serialNumber.Text },
                    {"<company>", textBox_company.Text },
                    {"<dateDecommission>", dateDecommission },
                };

                WordHelper.GetInstance.Process(items, decommissionSerialNumber_company, dateDecommission, city);
            }
        }




        #endregion

        #endregion


    }
}


