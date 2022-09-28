using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private static string taskCity;// для потоков
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
                comboBox_seach.Text = comboBox_seach.Items[2].ToString();

                dataGridView1.DoubleBuffered(true);
                this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
                this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                _user = user;
                IsAdmin();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка загрузки формы ST_WorkForm");
            }
        }

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


        private void ST_WorkForm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12f, FontStyle.Bold); //жирный курсив размера 16
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; //цвет текста
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; //цвет ячейки

                if (Internet_check.AvailabilityChanged_bool())
                {
                    try
                    {
                        string querystring = $"SELECT city FROM radiostantion GROUP BY city";
                        using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            DataTable city_table = new DataTable();

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                            {
                                adapter.Fill(city_table);

                                comboBox_city.DataSource = city_table;
                                comboBox_city.DisplayMember = "city";
                                DB.GetInstance.CloseConnection();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка! Города не добавленны в comboBox!ST_WorkForm_Load");
                    }
                }
                try
                {
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
                            bool flag = CheacReggedit.ValueExists(helloKey, regKey[i]);
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
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка загрузки данных из реестра!(ST_WorkForm_Load)");
                }



                Filling_datagridview.CreateColums(dataGridView1);
                Filling_datagridview.CreateColums(dataGridView2);
                Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
                Counters();

                this.dataGridView1.Sort(this.dataGridView1.Columns["dateTO"], ListSortDirection.Ascending);
                dataGridView1.Columns["dateTO"].ValueType = typeof(DateTime);
                dataGridView1.Columns["dateTO"].DefaultCellStyle.Format = "dd.MM.yyyy";
                dataGridView1.Columns["dateTO"].ValueType = System.Type.GetType("System.Date");

                try
                {
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
                    RegistryKey reg3 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                    if (reg3 != null)
                    {
                        RegistryKey currentUserKey = Registry.CurrentUser;
                        RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                        lbl_Sign.Text = helloKey.GetValue("Акты_на_подпись").ToString();
                        if (lbl_Sign.Text != "")
                        {
                            label_Sing.Visible = true;
                            lbl_Sign.Visible = true;
                        }
                        helloKey.Close();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка загрузки данных из реестра!(Акты_Заполняем_До_full, Акты_на_подпись)");
                }


                taskCity = comboBox_city.Text;// для отдельных потоков

                ///Таймер
                WinForms::Timer timer = new WinForms::Timer();
                timer.Interval = (30 * 60 * 1000); // 15 mins
                timer.Tick += new EventHandler(TimerEventProcessor);
                timer.Start();

                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;

            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка ST_WorkForm_Load!");
            }
        }

        void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            Filling_datagridview.RefreshDataGridTimerEventProcessor(dataGridView2, taskCity);

            //await Task.Run(() => FunctionPanel.Get_date_save_datagridview_json(dataGridView2, taskCity));
            new Thread(() => { FunctionPanel.Get_date_save_datagridview_json(dataGridView2, taskCity); }) { IsBackground = true }.Start();

            new Thread(() => { SaveFileDataGridViewPC.AutoSaveFilePC(dataGridView2, taskCity); }) { IsBackground = true }.Start();
            new Thread(() => { Filling_datagridview.Copy_BD_radiostantion_in_radiostantion_copy(); }) { IsBackground = true }.Start();

            //await Task.Run(() => Filling_datagridview.CreateColums(dataGridView3));
            //await Task.Run(() => Filling_datagridview.RefreshDataGrid(dataGridView3, taskCity));
            //await Task.Run(() => SaveFileDataGridViewPC.AutoSaveFilePC(dataGridView3, taskCity));

            //await Task.Run(() => Filling_datagridview.Copy_BD_radiostantion_in_radiostantion_copy());
        }

        #region Счётчики

        void Counters()
        {
            try
            {
                decimal sumTO = 0;
                int colRemont = 0;
                decimal sumRemont = 0;

                for (int i = 0; i < dataGridView1.Rows.Count; ++i)
                {
                    if ((Boolean)(dataGridView1.Rows[i].Cells["category"].Value.ToString() != ""))
                    {
                        colRemont++;
                    }
                    sumTO += Convert.ToDecimal(dataGridView1.Rows[i].Cells["price"].Value);
                    sumRemont += Convert.ToDecimal(dataGridView1.Rows[i].Cells["priceRemont"].Value);
                }

                label_count.Text = dataGridView1.Rows.Count.ToString();
                label_summ.Text = sumTO.ToString();
                label_count_remont.Text = colRemont.ToString();
                label_summ_remont.Text = sumRemont.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка Counters!");
            }
        }

        #endregion

        #region загрузка всей таблицы ТО в текущем году
        void Button_all_BD_Click(object sender, EventArgs e)
        {
            Filling_datagridview.Full_BD(dataGridView1);
            Counters();
            txb_flag_all_BD.Text = "Вся БД";
        }

        #endregion

        #region panel date information

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
            catch (Exception)
            {
                MessageBox.Show("Ошибка записи в реестр данных(Button_record_date_Click)!");
            }
        }


        void Block_ST_Work_Form_control()
        {
            dataGridView1.Enabled = false;
            panel1.Enabled = false;
            panel3.Enabled = false;
        }


        void Button_close_panel_date_info_Click(object sender, EventArgs e)
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
            try
            {
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.CreateSubKey("SOFTWARE\\ServiceTelekom_Setting");
                helloKey.SetValue("Город проведения проверки", $"{comboBox_city.Text}");
                helloKey.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка загрузки города проведния проверки в comboBox из рееестра(Button_add_city_Click)!");
            }

        }
        #endregion

        #region получение данных в Control-ы, button right mouse

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
            catch (Exception)
            {
                MessageBox.Show("Ошибка получения данных в Control-ы(DataGridView1_CellClick)");
            }
        }
        #endregion

        #region Clear contorl-ы

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
            catch (Exception)
            {
                MessageBox.Show("Ошибка очистки данных TextBox на форме (ClearFields)");
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

        #region Добавление радиостанций в выполнение

        void AddExecution(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 1)
                {
                    string Mesage;
                    Mesage = $"Вы действительно хотите добавить радиостанции в выполнение: {textBox_company.Text}?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    string Mesage;
                    Mesage = $"Вы действительно хотите добавить радиостанцию в выполнение: {textBox_serialNumber.Text}, предприятия: {textBox_company.Text}?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Январь", (s, ee) => AddExecutionСurator.AddExecutionRowСell(dataGridView1, "Январь")));
                m.MenuItems.Add(new MenuItem("Февраль", (s, ee) => AddExecutionСurator.AddExecutionRowСell(dataGridView1, "Февраль")));
                m.MenuItems.Add(new MenuItem("Март", (s, ee) => AddExecutionСurator.AddExecutionRowСell(dataGridView1, "Март")));
                m.MenuItems.Add(new MenuItem("Апрель", (s, ee) => AddExecutionСurator.AddExecutionRowСell(dataGridView1, "Апрель")));
                m.MenuItems.Add(new MenuItem("Май", (s, ee) => AddExecutionСurator.AddExecutionRowСell(dataGridView1, "Май")));
                m.MenuItems.Add(new MenuItem("Июнь", (s, ee) => AddExecutionСurator.AddExecutionRowСell(dataGridView1, "Июнь")));
                m.MenuItems.Add(new MenuItem("Июль", (s, ee) => AddExecutionСurator.AddExecutionRowСell(dataGridView1, "Июль")));
                m.MenuItems.Add(new MenuItem("Август", (s, ee) => AddExecutionСurator.AddExecutionRowСell(dataGridView1, "Август")));
                m.MenuItems.Add(new MenuItem("Сентябрь", (s, ee) => AddExecutionСurator.AddExecutionRowСell(dataGridView1, "Сентябрь")));
                m.MenuItems.Add(new MenuItem("Октябрь", (s, ee) => AddExecutionСurator.AddExecutionRowСell(dataGridView1, "Октябрь")));
                m.MenuItems.Add(new MenuItem("Ноябрь", (s, ee) => AddExecutionСurator.AddExecutionRowСell(dataGridView1, "Ноябрь")));
                m.MenuItems.Add(new MenuItem("Декабрь", (s, ee) => AddExecutionСurator.AddExecutionRowСell(dataGridView1, "Декабрь")));

                m.Show(dataGridView1, new Point(dataGridView1.Location.X+700, dataGridView1.Location.Y));

            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления радиостанции(й) в выполнение (AddExecution)");
            }
        }


        #endregion

        #region Удаление из БД

        void Button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 1)
                {
                    string Mesage;
                    Mesage = $"Вы действительно хотите удалить радиостанции у предприятия: {textBox_company.Text}?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    string Mesage;
                    Mesage = $"Вы действительно хотите удалить радиостанцию: {textBox_serialNumber.Text}, предприятия: {textBox_company.Text}?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                Filling_datagridview.DeleteRowСell(dataGridView1);

                int currRowIndex = dataGridView1.CurrentCell.RowIndex;

                Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
                textBox_numberAct.Text = "";

                dataGridView1.ClearSelection();

                if (dataGridView1.RowCount - currRowIndex > 0)
                {
                    dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
                }
                Counters();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка удаления радиостанции (Button_delete_Click)");
            }

        }

        #endregion

        #region обновление БД

        void Button_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    int currRowIndex = dataGridView1.CurrentCell.RowIndex;
                    int index = dataGridView1.CurrentRow.Index;
                    Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
                    Counters();
                    dataGridView1.ClearSelection();

                    if (currRowIndex >= 0)
                    {
                        dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];

                        dataGridView1.FirstDisplayedScrollingRowIndex = index;
                    }
                }
                else if (dataGridView1.Rows.Count == 0)
                {
                    Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
                    Counters();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка обновления dataGridView1 (Button_update_Click)");
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
                    if (dataGridView1.RowCount != 0)
                    {
                        this.dataGridView1.Sort(this.dataGridView1.Columns["numberAct"], ListSortDirection.Ascending);
                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
                        DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                        addRSTForm.lbl_last_act.Text = row.Cells[9].Value.ToString();
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            column.SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                    }
                    if (Application.OpenForms["addRSTForm"] == null)
                    {
                        addRSTForm.Show();
                    }

                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.Automatic;
                    }

                    #region для одной формы
                    //Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
                    //Counters();

                    //if (dataGridView1.RowCount != 0)
                    //{
                    //    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
                    //    DataGridViewRow row2 = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                    //    textBox_numberAct.Text = row2.Cells[9].Value.ToString();
                    //}
                    //// обновляем по акту
                    //Filling_datagridview.Update_datagridview_number_act(dataGridView1, textBox_city.Text, textBox_numberAct.Text);
                    #endregion
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка создания формы AddRSTForm(Button_new_add_rst_form_Click)");
                }
            }

        }
        #endregion

        #region проверка ввода текст боксов

        void ProcessKbdCtrlShortcuts(object sender, KeyEventArgs e)
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
            catch (Exception)
            {
                MessageBox.Show("Ошибка метода ctrl+c+v (ProcessKbdCtrlShortcuts)");
            }
        }
        void TextBox_GD_city_Click(object sender, EventArgs e)
        {
            textBox_company.MaxLength = 25;
        }
        void TextBox_GD_city_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }
        void TextBox_GD_city_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TextBox_FIO_chief_Click(object sender, EventArgs e)
        {
            textBox_company.MaxLength = 25;
        }

        void TextBox_FIO_chief_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }
        void TextBox_FIO_chief_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TextBox_doverennost_Click(object sender, EventArgs e)
        {
            textBox_company.MaxLength = 25;
        }
        void TextBox_doverennost_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TextBox_doverennost_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ' && ch != '/')
            {
                e.Handled = true;
            }
        }
        void TextBox_FIO_Engineer_Click(object sender, EventArgs e)
        {
            textBox_company.MaxLength = 25;
        }
        void TextBox_FIO_Engineer_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }
        void TextBox_FIO_Engineer_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        #endregion

        #region АКТ => excel

        void Button_form_act_Click(object sender, EventArgs e)
        {
            try
            {
                Filling_datagridview.Update_datagridview_number_act(dataGridView1, textBox_city.Text, textBox_numberAct.Text);
                int currRowIndex = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.ClearSelection();

                if (dataGridView1.CurrentCell.RowIndex >= 0)
                {
                    dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
                }
                Refresh_values_TXB_CMB(currRowIndex);
                if (textBox_numberAct.Text != "")
                {
                    dataGridView1.Sort(dataGridView1.Columns["model"], ListSortDirection.Ascending);
                }
                PrintDocExcel.PrintExcelActTo(dataGridView1, textBox_numberAct.Text, textBox_dateTO.Text, textBox_company.Text, textBox_location.Text,
                    label_FIO_chief.Text, textBox_post.Text, textBox_representative.Text, textBox_numberIdentification.Text, label_FIO_Engineer.Text,
                    label_doverennost.Text, label_polinon_full.Text, textBox_dateIssue.Text, textBox_city.Text, comboBox_poligon.Text);
                Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка создания Акта ТО (Button_form_act_Click)");
            }
        }


        /// <summary>
        /// АКТ Ремонта => excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_Continue_remont_act_excel_Click(object sender, EventArgs e)
        {
            if (textBox_Full_name_company.Text != "" && textBox_OKPO_remont.Text != "" && textBox_BE_remont.Text != ""
                                && textBox_director_FIO_remont_company.Text != "" && textBox_director_post_remont_company.Text != ""
                                && textBox_chairman_FIO_remont_company.Text != "" && textBox_chairman_post_remont_company.Text != ""
                                && textBox_1_FIO_remont_company.Text != "" && textBox_1_post_remont_company.Text != ""
                                && textBox_2_FIO_remont_company.Text != "" && textBox_2_post_remont_company.Text != "")
            {

                panel_remont_information_company.Visible = false;
                panel_remont_information_company.Enabled = false;
                PrintDocExcel.PrintExcelActRemont(dataGridView1, textBox_dateTO.Text, textBox_company.Text, textBox_location.Text,
                     label_FIO_chief.Text, textBox_post.Text, textBox_representative.Text, textBox_numberIdentification.Text, label_FIO_Engineer.Text,
                     label_doverennost.Text, label_polinon_full.Text, textBox_dateIssue.Text, textBox_city.Text, comboBox_poligon.Text, comboBox_сategory.Text,
                     comboBox_model.Text, textBox_serialNumber.Text, textBox_inventoryNumber.Text, textBox_networkNumber.Text, textBox_сompleted_works_1.Text,
                     textBox_parts_1.Text, textBox_сompleted_works_2.Text, textBox_parts_2.Text, textBox_сompleted_works_3.Text, textBox_parts_3.Text,
                     textBox_сompleted_works_4.Text, textBox_parts_4.Text, textBox_сompleted_works_5.Text, textBox_parts_5.Text, textBox_сompleted_works_6.Text,
                     textBox_parts_6.Text, textBox_сompleted_works_7.Text, textBox_parts_7.Text, textBox_OKPO_remont.Text, textBox_BE_remont.Text,
                     textBox_Full_name_company.Text, textBox_director_FIO_remont_company.Text, textBox_numberActRemont.Text,
                     textBox_chairman_post_remont_company.Text, textBox_chairman_FIO_remont_company.Text, textBox_1_post_remont_company.Text,
                     textBox_1_FIO_remont_company.Text, textBox_2_post_remont_company.Text, textBox_2_FIO_remont_company.Text,
                     textBox_3_post_remont_company.Text, textBox_3_FIO_remont_company.Text);
                panel1.Enabled = true;
            }
        }

        #endregion

        #region Сохранение БД на PC

        void Button_save_in_file_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDataGridViewPC.UserSaveFilePC(dataGridView1);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка сохранения таблицы пользователем(Button_save_in_file_Click)");
            }
        }
        #endregion

        #region Взаимодействие на форме Key-Press-ы, Button_click
        void TextBox_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Filling_datagridview.Search(dataGridView1, comboBox_seach.Text, comboBox_city.Text, textBox_search.Text, cmb_number_unique_acts.Text);
                Counters();
            }
        }

        void Button_search_Click(object sender, EventArgs e)
        {
            Filling_datagridview.Search(dataGridView1, comboBox_seach.Text, comboBox_city.Text, textBox_search.Text, cmb_number_unique_acts.Text);
            Counters();
        }

        void Button_seach_BD_city_Click(object sender, EventArgs e)
        {
            Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
            Counters();
        }

        void TextBox_numberAct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Filling_datagridview.Update_datagridview_number_act(dataGridView1, comboBox_city.Text, textBox_numberAct.Text);
                Counters();
            }
        }

        void TextBox_numberAct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (textBox_numberAct.Text != "")
            {
                Filling_datagridview.Update_datagridview_number_act(dataGridView1, comboBox_city.Text, textBox_numberAct.Text);
                Counters();
            }
        }

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
            catch (Exception)
            {
                MessageBox.Show("Ошибка метода DataGridView1_UserDeletedRow");
            }
        }

        void DataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                dataGridView1.Update();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка метода DataGridView1_UserAddedRow");
            }
        }

        void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Update();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка метода DataGridView1_CellValueChanged");
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
            ProcessKbdCtrlShortcuts(sender, e);
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
            catch (Exception)
            {
                MessageBox.Show("Ошибка метода Change_information_ServiceTelecom");
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
                Filling_datagridview.Seach_DataGrid_Replay_RST(dataGridView1, txb_flag_all_BD.Text, textBox_city.Text);
                Counters();
            }
        }

        #endregion

        #region ContextMenu datagrid
        void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (_user.IsAdmin == "Дирекция связи" || _user.IsAdmin == "Инженер")
                    {
                        ContextMenu m3 = new ContextMenu();
                        m3.MenuItems.Add(new MenuItem("Сохранение базы", Button_save_in_file_Click));
                        m3.MenuItems.Add(new MenuItem("Обновить базу", Button_update_Click));
                        m3.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                    else if(_user.IsAdmin == "Куратор")
                    {
                        if (dataGridView1.Rows.Count > 0 && panel1.Enabled == true && panel3.Enabled == true)
                        {
                            ContextMenu m = new ContextMenu();

                            var add_new_radio_station = m.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", Button_new_add_rst_form_Click));
                            if (textBox_serialNumber.Text != "")
                            {
                                m.MenuItems.Add(new MenuItem("Изменить добавленную радиостанцию", Button_new_add_rst_form_Click_change));
                                m.MenuItems.Add(new MenuItem("Добавить/изменить ремонт", Button_new_add_rst_form_click_remont));
                                m.MenuItems.Add(new MenuItem("Сформировать акт ТО", Button_form_act_Click));
                                m.MenuItems.Add(new MenuItem("Сформировать акт Ремонта", Button_remont_act_Click));
                                m.MenuItems.Add(new MenuItem("Удалить радиостанцию", Button_delete_Click));
                                m.MenuItems.Add(new MenuItem("Удалить ремонт", Delete_rst_remont_click));
                                m.MenuItems.Add(new MenuItem("Заполняем акт", DataGridView1_DefaultCellStyleChanged));
                                m.MenuItems.Add(new MenuItem("На подпись", DataGridView1_Sign));
                                m.MenuItems.Add(new MenuItem("Списать РСТ", DecommissionSerialNumber));
                                m.MenuItems.Add(new MenuItem("Добавить в выполнение", AddExecution));
                            }
                            if (txB_decommissionSerialNumber.Text != "")
                            {
                                m.MenuItems.Add(new MenuItem("Сформировать акт списания", PrintWord_Act_decommission));
                                m.MenuItems.Add(new MenuItem("Удалить списание", Delete_rst_decommission_click));
                            }
                            m.MenuItems.Add(new MenuItem("Обновить базу", Button_update_Click));
                            m.MenuItems.Add(new MenuItem("Сохранение базы", Button_save_in_file_Click));
                            m.MenuItems.Add(new MenuItem("Показать совпадение с предыдущим годом", PictureBox_seach_datadrid_replay_Click));
                            m.MenuItems.Add(new MenuItem("Показать все списания", Show_radiostantion_decommission_Click));

                            m.Show(dataGridView1, new Point(e.X, e.Y));

                        }
                        else if (dataGridView1.Rows.Count == 0 && panel1.Enabled == true && panel3.Enabled == true)
                        {
                            ContextMenu m1 = new ContextMenu();
                            m1.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", Button_new_add_rst_form_Click));
                            m1.MenuItems.Add(new MenuItem("Обновить базу", Button_update_Click));

                            m1.Show(dataGridView1, new Point(e.X, e.Y));
                        }
                        else if (dataGridView1.Rows.Count > 0 || dataGridView1.Rows.Count == 0 && panel1.Enabled == false && panel3.Enabled == false)
                        {
                            ContextMenu m2 = new ContextMenu();
                            m2.MenuItems.Add(new MenuItem("Сохранение базы", Button_save_in_file_Click));
                            m2.MenuItems.Add(new MenuItem("Обновить базу", Button_update_Click_after_Seach_DataGrid_Replay_RST));

                            m2.Show(dataGridView1, new Point(e.X, e.Y));

                            if (e.Button == MouseButtons.Left)
                            {
                                dataGridView1.ClearSelection();
                            }
                        }
                    }
                    else if (_user.IsAdmin == "Начальник участка" || _user.IsAdmin == "Руководитель" || _user.IsAdmin == "Admin" )
                    {
                        if (dataGridView1.Rows.Count > 0 && panel1.Enabled == true && panel3.Enabled == true)
                        {
                            ContextMenu m = new ContextMenu();

                            var add_new_radio_station = m.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", Button_new_add_rst_form_Click));
                            if (textBox_serialNumber.Text != "")
                            {
                                m.MenuItems.Add(new MenuItem("Изменить добавленную радиостанцию", Button_new_add_rst_form_Click_change));
                                m.MenuItems.Add(new MenuItem("Добавить/изменить ремонт", Button_new_add_rst_form_click_remont));
                                m.MenuItems.Add(new MenuItem("Сформировать акт ТО", Button_form_act_Click));
                                m.MenuItems.Add(new MenuItem("Сформировать акт Ремонта", Button_remont_act_Click));
                                m.MenuItems.Add(new MenuItem("Удалить радиостанцию", Button_delete_Click));
                                m.MenuItems.Add(new MenuItem("Удалить ремонт", Delete_rst_remont_click));
                                m.MenuItems.Add(new MenuItem("Заполняем акт", DataGridView1_DefaultCellStyleChanged));
                                m.MenuItems.Add(new MenuItem("На подпись", DataGridView1_Sign));
                                m.MenuItems.Add(new MenuItem("Списать РСТ", DecommissionSerialNumber));
                            }
                            if (txB_decommissionSerialNumber.Text != "")
                            {
                                m.MenuItems.Add(new MenuItem("Сформировать акт списания", PrintWord_Act_decommission));
                                m.MenuItems.Add(new MenuItem("Удалить списание", Delete_rst_decommission_click));
                            }
                            m.MenuItems.Add(new MenuItem("Обновить базу", Button_update_Click));
                            m.MenuItems.Add(new MenuItem("Сохранение базы", Button_save_in_file_Click));
                            m.MenuItems.Add(new MenuItem("Показать совпадение с предыдущим годом", PictureBox_seach_datadrid_replay_Click));
                            m.MenuItems.Add(new MenuItem("Показать все списания", Show_radiostantion_decommission_Click));

                            m.Show(dataGridView1, new Point(e.X, e.Y));

                        }
                        else if (dataGridView1.Rows.Count == 0 && panel1.Enabled == true && panel3.Enabled == true)
                        {
                            ContextMenu m1 = new ContextMenu();
                            m1.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", Button_new_add_rst_form_Click));
                            m1.MenuItems.Add(new MenuItem("Обновить базу", Button_update_Click));

                            m1.Show(dataGridView1, new Point(e.X, e.Y));
                        }
                        else if (dataGridView1.Rows.Count > 0 || dataGridView1.Rows.Count == 0 && panel1.Enabled == false && panel3.Enabled == false)
                        {
                            ContextMenu m2 = new ContextMenu();
                            m2.MenuItems.Add(new MenuItem("Сохранение базы", Button_save_in_file_Click));
                            m2.MenuItems.Add(new MenuItem("Обновить базу", Button_update_Click_after_Seach_DataGrid_Replay_RST));

                            m2.Show(dataGridView1, new Point(e.X, e.Y));

                            if (e.Button == MouseButtons.Left)
                            {
                                dataGridView1.ClearSelection();
                            }
                        }
                    }
                } 
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка ContextMenu (DataGridView1_MouseClick)");
            }
        }
        #endregion

        #region обновляем БД после показа отсутсвующих радиостанций после проверки на участке

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
                        Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
                        Counters();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка обновления БД после показа отсутсвующих радиостанций после проверки на участке (Button_update_Click_after_Seach_DataGrid_Replay_RST)");
                }
            }
        }

        #endregion

        #region Удаление ремонта
        void Delete_rst_remont_click(object sender, EventArgs e)
        {
            try
            {
                string Mesage;
                Mesage = $"Вы действительно хотите удалить ремонт у радиостанции: {textBox_serialNumber.Text}, предприятия: {textBox_company.Text}?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                Filling_datagridview.Delete_rst_remont(textBox_numberActRemont.Text, textBox_serialNumber.Text);
                Button_update_Click(sender, e);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка удаления ремонта (Delete_rst_remont_click)");
            }
        }

        #endregion

        #region отк. формы добавления ремонтов
        private void Button_new_add_rst_form_click_remont(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (textBox_serialNumber.Text != "")
                    {
                        using (RemontRSTForm remontRSTForm = new RemontRSTForm())
                        {
                            remontRSTForm.DoubleBufferedForm(true);

                            remontRSTForm.comboBox_сategory.Text = comboBox_сategory.Text;

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

                            if (textBox_numberActRemont.Text == "")
                            {
                                remontRSTForm.textBox_numberActRemont.Text = textBox_number_printing_doc_datePanel.Text + "/";
                            }
                            else remontRSTForm.textBox_numberActRemont.Text = textBox_numberActRemont.Text;

                            int currRowIndex = dataGridView1.CurrentCell.RowIndex;

                            remontRSTForm.lbl_last_act_remont.Text = Filling_datagridview.SortRemontAct(dataGridView1, comboBox_city.Text);

                            remontRSTForm.ShowDialog();

                            Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
                            Counters();
                            dataGridView1.ClearSelection();

                            if (dataGridView1.CurrentCell.RowIndex >= 0)
                            {
                                dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
                            }
                            Refresh_values_TXB_CMB(currRowIndex);

                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка открытия формы добавления ремонта RemontRSTForm (Button_new_add_rst_form_click_remont)");
                }
            }
        }
        #endregion

        #region отк. формы изменения РСТ
        private void Button_new_add_rst_form_Click_change(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (textBox_serialNumber.Text != "")
                    {
                        СhangeRSTForm changeRSTForm = new СhangeRSTForm();
                        changeRSTForm.DoubleBufferedForm(true);
                        changeRSTForm.textBox_city.Text = textBox_city.Text;
                        changeRSTForm.comboBox_poligon.Text = comboBox_poligon.Text;
                        changeRSTForm.textBox_company.Text = textBox_company.Text;
                        changeRSTForm.textBox_location.Text = textBox_location.Text;
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

                        if (Application.OpenForms["changeRSTForm"] == null)
                        {
                            changeRSTForm.Show();
                        }
                        //changeRSTForm.ShowDialog();


                        #region старый метод для одной рст

                        //int currRowIndex = dataGridView1.CurrentCell.RowIndex;
                        //Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
                        //dataGridView1.ClearSelection();

                        //if (dataGridView1.CurrentCell.RowIndex >= 0)
                        //{
                        //    dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
                        //}
                        //Refresh_values_TXB_CMB(currRowIndex);

                        #endregion
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка открытия формы изменения радиостанции СhangeRSTForm (Button_new_add_rst_form_Click_change)");
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
            ProcessKbdCtrlShortcuts(sender, e);
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
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_BE_remont_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
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
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_director_post_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
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
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_chairman_post_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
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
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_1_post_remont_company_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
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
            ProcessKbdCtrlShortcuts(sender, e);
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
            ProcessKbdCtrlShortcuts(sender, e);
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
            ProcessKbdCtrlShortcuts(sender, e);
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
            ProcessKbdCtrlShortcuts(sender, e);
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

        void Refresh_values_TXB_CMB(int currRowIndex)
        {
            try
            {
                DataGridViewRow row = dataGridView1.Rows[currRowIndex];
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
            catch (Exception)
            {
                MessageBox.Show("Ошибка получения данных в Control-ы из Datagrid (Refresh_values_TXB_CMB)");
            }

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
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Equals(searchValue))
                            {
                                dataGridView1.Rows[i].Cells[j].Selected = true;
                                int currRowIndex = dataGridView1.Rows[i].Cells[j].RowIndex;
                                dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
                                Refresh_values_TXB_CMB(currRowIndex);
                                break;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка поиска по DataGrid (Seach_datagrid)");
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
            ProcessKbdCtrlShortcuts(sender, e);
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
            catch (Exception)
            {
                MessageBox.Show("Ошибка подсчёта кол-ва строк Datagrid при выборе пользователя(DataGridView1_SelectionChanged)");
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
                catch (Exception)
                {
                    string Mesage = $"Ошибка загрузки данных для текущей БД! Радиостанции не добавленны!(Loading_file_current_BD)";

                    if (MessageBox.Show(Mesage, "Обратите внимание на содержимое файла", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
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
                catch (Exception)
                {
                    string Mesage = $"Ошибка загрузки данных для БД прошлого года! Радиостанции не добавленны!(Loading_file_last_year)";

                    if (MessageBox.Show(Mesage, "Обратите внимание на содержимое файла", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
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


                        using (var connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_db_2;password=war74_89;database=u1748936_root;charset=utf8"))
                        {
                            if (Internet_check.AvailabilityChanged_bool())
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
                    string Mesage = $"Ошибка загрузки данных дляо бщей БД! Радиостанции не добавленны!(Loading_file_full_BD_method)";

                    if (MessageBox.Show(Mesage, "Обратите внимание на содержимое файла", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                }
            }
        }

        #endregion

        #region загрузка и обновление json в radiostantion
        async void Loading_json_file_BD_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                string Mesage;
                Mesage = "Вы выгрузили резервный файл json?";

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
                await Task.Run(() => FunctionPanel.Loading_json_file_BD(dataGridView2, taskCity));
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
            await Task.Run(() => FunctionPanel.Get_date_save_datagridview_json(dataGridView1, taskCity));
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


        #endregion

        #region копирование текущей таблицы radiostantion в radiostantion_last_year к концу года 


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
                FunctionPanel.Copying_current_BD_end_of_the_year();
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
            catch (Exception)
            {
                MessageBox.Show("Ошибка копирования текущей БД в БД прошлого года (Button_Copying_current_BD_end_of_the_year_Click)");
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
                FunctionPanel.Manual_backup_current_DB();
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
            catch (Exception)
            {
                MessageBox.Show("Ошибка ручного-резервного копирования радиостанций из текущей radiostantion в radiostantion_copy"); ;
            }
        }
        #endregion

        #region очистка текущей БД, текущий год (radiostantion)

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
                FunctionPanel.Clear_BD_current_year();
                Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
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
            catch (Exception)
            {
                MessageBox.Show("Ошибка очистки текущей БД (Clear_BD_current_year_Click)"); ;
            }
        }

        #endregion

        #region показать БД прошлого года по участку

        void Btn_Show_DB_radiostantion_last_year_Click(object sender, EventArgs e)
        {
            try
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
                Close_Functional_loading_panel_Click(sender, e);
                panel1.Enabled = false;
                panel3.Enabled = false;
                FunctionPanel.Show_DB_radiostantion_last_year(dataGridView1, taskCity);
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
                Counters();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка загрузки БД прошлого года по участку(Btn_Show_DB_radiostantion_last_year_Click)");
            }
        }


        #endregion

        #region показать общую БД по всем радиостанциям

        void Btn_Show_DB_radiostantion_full_Click(object sender, EventArgs e)
        {
            try
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
                Close_Functional_loading_panel_Click(sender, e);
                panel1.Enabled = false;
                panel3.Enabled = false;
                FunctionPanel.Show_DB_radiostantion_full(dataGridView1, taskCity);
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
                Counters();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка загрузки общей БД прошлого года без участка (Btn_Show_DB_radiostantion_full_Click)");
            }

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

        void DataGridView1_Sign(object sender, EventArgs e)
        {
            if (textBox_numberAct.Text != "")
            {
                int c = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["numberAct"].Value.ToString().Equals(textBox_numberAct.Text))
                    {
                        dataGridView1.Rows[i].Cells["numberAct"].Style.BackColor = Color.Red;
                        c++;
                    }
                }

                label_Sing.Visible = true;
                lbl_Sign.Visible = true;
                lbl_Sign.Text += $"{textBox_numberAct.Text} - {textBox_company.Text} - {c} шт.,";
                try
                {
                    RegistryKey currentUserKey = Registry.CurrentUser;
                    RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                    helloKey.SetValue("Акты_на_подпись", $"{lbl_Sign.Text}");
                    helloKey.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        void DataGridView1_DefaultCellStyleChanged(object sender, EventArgs e)
        {
            if (textBox_numberAct.Text != "")
            {
                int c = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["numberAct"].Value.ToString().Equals(textBox_numberAct.Text))
                    {
                        dataGridView1.Rows[i].Cells["numberAct"].Style.BackColor = Color.Red;
                        c++;
                    }
                }

                label_complete.Visible = true;
                lbl_full_complete_act.Visible = true;
                lbl_full_complete_act.Text += $"{textBox_numberAct.Text} - {textBox_company.Text} - {c} шт.,";
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
            }
        }
        #region для редактирования актов заполняемых до конца

        void Lbl_full_complete_act_DoubleClick(object sender, EventArgs e)
        {
            lbl_full_complete_act.Visible = false;
            txB_lbl_full_complete_act.Text = lbl_full_complete_act.Text;
            txB_lbl_full_complete_act.Visible = true;
        }

        void Lbl_Sign_DoubleClick(object sender, EventArgs e)
        {
            lbl_Sign.Visible = false;
            txB_Sign.Text = lbl_Sign.Text;
            txB_Sign.Visible = true;
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
            if (txB_Sign.Visible)
            {
                try
                {
                    RegistryKey currentUserKey = Registry.CurrentUser;
                    RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                    helloKey.SetValue("Акты_на_подпись", $"{txB_Sign.Text}");
                    helloKey.Close();

                    RegistryKey reg4 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                    if (reg4 != null)
                    {
                        RegistryKey currentUserKey2 = Registry.CurrentUser;
                        RegistryKey helloKey2 = currentUserKey2.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                        lbl_Sign.Text = helloKey2.GetValue("Акты_на_подпись").ToString();

                        label_Sing.Visible = true;
                        lbl_Sign.Visible = true;
                        txB_Sign.Visible = false;

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
                if (txB_Sign.Visible)
                {
                    try
                    {
                        RegistryKey currentUserKey = Registry.CurrentUser;
                        RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                        helloKey.SetValue("Акты_на_подпись", $"{txB_Sign.Text}");
                        helloKey.Close();

                        RegistryKey reg4 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                        if (reg4 != null)
                        {
                            RegistryKey currentUserKey2 = Registry.CurrentUser;
                            RegistryKey helloKey2 = currentUserKey2.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                            lbl_Sign.Text = helloKey2.GetValue("Акты_на_подпись").ToString();

                            label_Sing.Visible = true;
                            lbl_Sign.Visible = true;
                            txB_Sign.Visible = false;

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
            panel1.Enabled = true;
            panel2.Enabled = true;
            panel3.Enabled = true;
            dataGridView1.Enabled = true;
        }

        void DecommissionSerialNumber(object sender, EventArgs e)
        {
            if (textBox_serialNumber.Text != "")
            {
                string Mesage;
                Mesage = $"Вы действительно хотите списать радиостанцию? Номер: {textBox_serialNumber.Text} от предприятия {textBox_company.Text}";

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
                txB_reason_decommission.Text = "Коррозия основной печатной платы с многочисленными обрывами проводников, вызванная попаданием влаги внутрь радиостанции. Восстановлению не подлежит.";

            }

        }


        void Btn_record_decommissionSerialNumber_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_decommissionSerialNumber.Text != "" && txB_reason_decommission.Text != "")
                {
                    var re = new Regex(Environment.NewLine);
                    txB_reason_decommission.Text = re.Replace(txB_reason_decommission.Text, " ");//удаление новой строки

                    Filling_datagridview.Record_decommissionSerialNumber(textBox_serialNumber.Text, textBox_decommissionSerialNumber.Text,
                        textBox_city.Text, comboBox_poligon.Text, textBox_company.Text, textBox_location.Text, comboBox_model.Text, textBox_dateTO.Text,
                        textBox_price.Text, textBox_representative.Text, textBox_post.Text, textBox_numberIdentification.Text, textBox_dateIssue.Text,
                        textBox_phoneNumber.Text, textBox_antenna.Text, textBox_manipulator.Text, textBox_AKB.Text, textBox_batteryСharger.Text,
                        txB_comment.Text, textBox_number_printing_doc_datePanel.Text, txB_reason_decommission.Text);
                    Button_update_Click(sender, e);
                    panel_decommissionSerialNumber.Visible = false;
                    panel_decommissionSerialNumber.Enabled = false;
                    panel1.Enabled = true;
                    panel2.Enabled = true;
                    panel3.Enabled = true;
                    dataGridView1.Enabled = true;
                    textBox_decommissionSerialNumber.Text = "";
                }
                else { MessageBox.Show("Вы не заполнили поле Номер Акта Списания или поле Причина!"); }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка списания РСТ (Btn_record_decommissionSerialNumber_Click)");
            }

        }

        #region Удаление списания
        void Delete_rst_decommission_click(object sender, EventArgs e)
        {
            try
            {
                string Mesage;
                Mesage = $"Вы действительно хотите удалить списание на данную радиостанцию: {textBox_serialNumber.Text}, предприятия: {textBox_company.Text}?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                Filling_datagridview.Delete_decommissionSerialNumber_radiostantion(dataGridView2, txB_decommissionSerialNumber.Text, textBox_serialNumber.Text, textBox_city.Text);
                Button_update_Click(sender, e);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка удаления списания РСТ (Delete_rst_decommission_click)");
            }

        }
        #endregion

        #region показать списания
        void Show_radiostantion_decommission_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Enabled = false;
                panel3.Enabled = false;
                Filling_datagridview.Show_radiostantion_decommission(dataGridView1, textBox_city.Text);
                Counters();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка загрузки БД списания РСТ (Show_radiostantion_decommission_Click)");
            }

        }

        #endregion

        #region сформировать акт списания

        void PrintWord_Act_decommission(object sender, EventArgs e)
        {
            try
            {
                if (txB_decommissionSerialNumber.Text != "")
                {
                    string decommissionSerialNumber_company = $"{txB_decommissionSerialNumber.Text}-{textBox_company.Text}";
                    DateTime dateTime = DateTime.Today;
                    string dateDecommission = dateTime.ToString("dd.MM.yyyy");
                    string city = textBox_city.Text;
                    string comment = txB_comment.Text;

                    var items = new Dictionary<string, string>
                {
                    {"<numberActTZPP>", decommissionSerialNumber_company },
                    {"<model>", comboBox_model.Text },
                    {"<serialNumber>", textBox_serialNumber.Text },
                    {"<company>", textBox_company.Text },
                    {"<dateDecommission>", dateDecommission },
                    {"<comment>", comment}
                };

                    PrintDocWord.GetInstance.ProcessPrintWord(items, decommissionSerialNumber_company, dateDecommission, city, comment);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка формирования акта списания (PrintWord_Act_decommission)");
            }

        }


        #endregion

        #endregion

        #region показать кол-во уникальных актов

        void ComboBox_seach_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (comboBox_seach.SelectedIndex == 0)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        Filling_datagridview.Number_unique_company(comboBox_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Предприятия не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (comboBox_seach.SelectedIndex == 1)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        Filling_datagridview.Number_unique_location(comboBox_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Станции не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (comboBox_seach.SelectedIndex == 3)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        Filling_datagridview.Number_unique_dateTO(comboBox_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Даты проверки ТО не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (comboBox_seach.SelectedIndex == 4)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        Filling_datagridview.Number_unique_numberAct(comboBox_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Акты ТО не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (comboBox_seach.SelectedIndex == 5)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        Filling_datagridview.Number_unique_numberActRemont(comboBox_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Акты Ремонта не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (comboBox_seach.SelectedIndex == 6)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        Filling_datagridview.Number_unique_representative(comboBox_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Представители предприятий не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (comboBox_seach.SelectedIndex == 7)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        Filling_datagridview.Number_unique_decommissionActs(comboBox_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Акты списаний не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    cmb_number_unique_acts.Visible = false;
                    textBox_search.Visible = true;
                }
                cmb_number_unique_acts.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка метода ComboBox_seach_SelectionChangeCommitted");
            }
        }
        #endregion
    }
}


