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
                Filling_datagridview.CreateColums(dataGridView1);
                Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
                Counters();

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
                new Thread(() => { Filling_datagridview.Copy_BD_radiostantion_in_radiostantion_copy(); }) { IsBackground = true }.Start();
            }
        }

        #region Счётчики

        void Counters()
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

        #region Удаление из БД
        /// <summary>
        ///  метод удаления РСТ из БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Button_delete_Click(object sender, EventArgs e)
        {

            try
            {
                string Mesage;
                Mesage = "Вы действительно хотите удалить выделенную запись";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
                    Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);

                    if (dataGridView1.RowCount != 0)
                    {
                        dataGridView1.ClearSelection();
                        dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
                        DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                        textBox_numberAct.Text = row.Cells[9].Value.ToString();
                    }
                    // обновляем по акту
                    Filling_datagridview.Update_datagridview_number_act(dataGridView1, textBox_city.Text, textBox_numberAct.Text);

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

        #region cell_click_datagridview для печати акта ТО и ремонта

        void cellClickDatagridview_printActTO_Remont()
        {
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
        }

        #endregion

        #region АКТ => excel

        void Button_form_act_Click(object sender, EventArgs e)
        {
            Filling_datagridview.Update_datagridview_number_act(dataGridView1, textBox_city.Text, textBox_numberAct.Text);
            cellClickDatagridview_printActTO_Remont();
            PrintDocOffice.PrintExcelActTo(dataGridView1, textBox_numberAct.Text, textBox_dateTO.Text, textBox_company.Text, textBox_location.Text,
                label_FIO_chief.Text, textBox_post.Text, textBox_representative.Text, textBox_numberIdentification.Text, label_FIO_Engineer.Text,
                label_doverennost.Text, label_polinon_full.Text, textBox_dateIssue.Text, textBox_city.Text, comboBox_poligon.Text);
            Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);

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
                cellClickDatagridview_printActTO_Remont();
                PrintDocOffice.PrintExcelActRemont(dataGridView1, textBox_numberAct.Text, textBox_dateTO.Text, textBox_company.Text, textBox_location.Text,
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
                Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
                panel1.Enabled = true;
            }
        }

        #endregion

        #region Сохранение БД на PC

        void Button_save_in_file_Click(object sender, EventArgs e)
        {
            SaveFileDataGridViewPC.SaveFilePC(dataGridView1);
        }
        #endregion

        #region Взаимодействие на форме Key-Press-ы, Button_click
        void TextBox_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Filling_datagridview.Search(dataGridView1, comboBox_seach.Text, textBox_city.Text, textBox_search.Text);
                Counters();
            }
        }

        void Button_search_Click(object sender, EventArgs e)
        {
            Filling_datagridview.Search(dataGridView1, comboBox_seach.Text, textBox_city.Text, textBox_search.Text);
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
                Filling_datagridview.Update_datagridview_number_act(dataGridView1, textBox_city.Text, textBox_numberAct.Text);
                Counters();
            }
        }

        void TextBox_numberAct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (textBox_numberAct.Text != "")
            {
                Filling_datagridview.Update_datagridview_number_act(dataGridView1, textBox_city.Text, textBox_numberAct.Text);
                Counters();
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
                        Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
                        Counters();
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
            Filling_datagridview.Delete_rst_remont(textBox_numberActRemont.Text, textBox_serialNumber.Text);
            Button_update_Click(sender, e);
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
                            Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
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
                            Filling_datagridview.RefreshDataGrid(dataGridView1, comboBox_city.Text);
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
                                        Filling_datagridview.ReedSingleRow(dataGridView1, reader);
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
            Counters();
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
                                        Filling_datagridview.ReedSingleRow(dataGridView1, reader);
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
            Counters();
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
                if (lbl_full_complete_act.Text != $"{textBox_numberAct.Text} - {textBox_company.Text} - {c} шт.,")

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
                                        Filling_datagridview.ReedSingleRow(dataGridView1, reader);
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

                    Counters();

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


