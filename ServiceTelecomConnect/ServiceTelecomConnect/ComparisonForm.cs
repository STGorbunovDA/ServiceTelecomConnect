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
    public partial class ComparisonForm : Form
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

        public ComparisonForm()
        {
            try
            {
                InitializeComponent();

                StartPosition = FormStartPosition.CenterScreen;
                comboBox_seach.Items.Clear();

                comboBox_seach.Items.Add("Предприятие");
                comboBox_seach.Items.Add("Станция");
                comboBox_seach.Items.Add("Заводской номер");
                comboBox_seach.Items.Add("Дата ТО");
                comboBox_seach.Items.Add("Номер акта ТО");
                comboBox_seach.Items.Add("Номер акта Ремонта");
                comboBox_seach.Items.Add("Номер Акта списания");
                comboBox_seach.Items.Add("Месяц");

                comboBox_seach.Text = comboBox_seach.Items[2].ToString();

                dataGridView1.DoubleBuffered(true);
                this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
                this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка загрузки формы ST_WorkForm");
            }
        }

        private void ComparisonForm_Load(object sender, EventArgs e)
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
                        string querystring = $"SELECT city FROM radiostantion_сomparison GROUP BY city";
                        using (MySqlCommand command = new MySqlCommand(querystring, DB_4.GetInstance.GetConnection()))
                        {
                            DB_4.GetInstance.OpenConnection();
                            DataTable city_table = new DataTable();

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                            {
                                adapter.Fill(city_table);

                                comboBox_city.DataSource = city_table;
                                comboBox_city.DisplayMember = "city";
                                DB_4.GetInstance.CloseConnection();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка! Города не добавленны в comboBox!ST_WorkForm_Load");
                    }
                }

                Filling_datagridview.CreateColumsСurator(dataGridView1);
                Filling_datagridview.CreateColumsСurator(dataGridView2);
                Filling_datagridview.RefreshDataGridСurator(dataGridView1, comboBox_city.Text);
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
            try
            {
                Filling_datagridview.RefreshDataGridСuratorTimerEventProcessor(dataGridView2, taskCity);

                new Thread(() => { FunctionPanel.Get_date_save_datagridview_сurator_json(dataGridView2, taskCity); }) { IsBackground = true }.Start();

                new Thread(() => { SaveFileDataGridViewPC.AutoSaveFileCurator(dataGridView2, taskCity); }) { IsBackground = true }.Start();
                new Thread(() => { Filling_datagridview.Copy_BD_radiostantion_сomparison_in_radiostantion_сomparison_copy(); }) { IsBackground = true }.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка TimerEventProcessor!");
            }
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
            Filling_datagridview.Full_BD_Curator(dataGridView1);
            txb_flag_all_BD.Text = "Вся БД";
            Counters();
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

                    txB_id.Text = row.Cells[0].Value.ToString();
                    txB_poligon.Text = row.Cells[1].Value.ToString();
                    txB_company.Text = row.Cells[2].Value.ToString();
                    txB_location.Text = row.Cells[3].Value.ToString();
                    cmB_model.Text = row.Cells[4].Value.ToString();
                    txB_serialNumber.Text = row.Cells[5].Value.ToString();
                    txB_inventoryNumber.Text = row.Cells[6].Value.ToString();
                    txB_networkNumber.Text = row.Cells[7].Value.ToString();
                    txB_dateTO.Text = row.Cells[8].Value.ToString();
                    txB_numberAct.Text = row.Cells[9].Value.ToString();
                    txB_city.Text = row.Cells[10].Value.ToString();
                    txB_price.Text = row.Cells[11].Value.ToString();
                    txB_numberActRemont.Text = row.Cells[12].Value.ToString();
                    cmB_сategory.Text = row.Cells[13].Value.ToString();
                    txB_priceRemont.Text = row.Cells[14].Value.ToString();
                    txB_decommission.Text = row.Cells[15].Value.ToString();
                    txB_comment.Text = row.Cells[16].Value.ToString();
                    txB_month.Text = row.Cells[17].Value.ToString();
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

        #region Удаление из БД

        void Button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 1)
                {
                    string Mesage;
                    Mesage = $"Вы действительно хотите удалить радиостанции у предприятия: {txB_company.Text}?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    string Mesage;
                    Mesage = $"Вы действительно хотите удалить радиостанцию: {txB_serialNumber.Text}, предприятия: {txB_company.Text}?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                Filling_datagridview.DeleteRowСellCurator(dataGridView1);

                int currRowIndex = dataGridView1.CurrentCell.RowIndex;

                Filling_datagridview.RefreshDataGridСurator(dataGridView1, comboBox_city.Text);
                txB_numberAct.Text = "";

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
                    Filling_datagridview.RefreshDataGridСurator(dataGridView1, comboBox_city.Text);
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
                    Filling_datagridview.RefreshDataGridСurator(dataGridView1, comboBox_city.Text);
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
            txB_company.MaxLength = 25;
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
            txB_company.MaxLength = 25;
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
            txB_company.MaxLength = 25;
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
            txB_company.MaxLength = 25;
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

        #region Сохранение БД на PC

        void Button_save_in_file_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDataGridViewPC.UserSaveFileCuratorPC(dataGridView1);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка сохранения таблицы пользователем(Button_save_in_file_Click)");
            }
        }
        #endregion

        #region показать кол-во уникальных записей БД в Combobox

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

                        Filling_datagridview.Number_unique_company_curator(comboBox_city.Text, cmb_number_unique_acts);
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

                        Filling_datagridview.Number_unique_location_curator(comboBox_city.Text, cmb_number_unique_acts);
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

                        Filling_datagridview.Number_unique_dateTO_curator(comboBox_city.Text, cmb_number_unique_acts);
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

                        Filling_datagridview.Number_unique_numberAct_curator(comboBox_city.Text, cmb_number_unique_acts);
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

                        Filling_datagridview.Number_unique_numberActRemont_curator(comboBox_city.Text, cmb_number_unique_acts);
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

                        Filling_datagridview.Number_unique_decommissionActs_curator(comboBox_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Акты списаний не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (comboBox_seach.SelectedIndex == 7)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        Filling_datagridview.Number_unique_month_curator(comboBox_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка добавления в comboBox выполнение плана (Number_unique_AddExecution_curator)");
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

        #region Взаимодействие на search, cформировать на форме panel1

        void TextBox_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Filling_datagridview.SearchCurator(dataGridView1, comboBox_seach.Text, comboBox_city.Text, textBox_search.Text, cmb_number_unique_acts.Text);
                Counters();
            }
        }

        void Button_search_Click(object sender, EventArgs e)
        {
            Filling_datagridview.SearchCurator(dataGridView1, comboBox_seach.Text, comboBox_city.Text, textBox_search.Text, cmb_number_unique_acts.Text);
            Counters();
        }

        void Button_seach_BD_city_Click(object sender, EventArgs e)
        {
            Filling_datagridview.RefreshDataGridСurator(dataGridView1, comboBox_city.Text);
            Counters();
        }

        void TextBox_numberAct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Filling_datagridview.Update_datagridview_number_act_curator(dataGridView1, comboBox_city.Text, txB_numberAct.Text);
                Counters();
            }
        }

        void TextBox_numberAct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txB_numberAct.Text != "")
            {
                Filling_datagridview.Update_datagridview_number_act_curator(dataGridView1, comboBox_city.Text, txB_numberAct.Text);
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


        #region отк. формы изменения РСТ
        private void Button_new_add_rst_form_Click_change_curator(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    if (txB_serialNumber.Text != "")
                    {
                        СhangeRSTFormCurator сhangeRSTFormCurator = new СhangeRSTFormCurator();
                        сhangeRSTFormCurator.DoubleBufferedForm(true);
                        сhangeRSTFormCurator.txB_city.Text = txB_city.Text;
                        сhangeRSTFormCurator.cmB_poligon.Text = txB_poligon.Text;
                        сhangeRSTFormCurator.txB_company.Text = txB_company.Text;
                        сhangeRSTFormCurator.txB_location.Text = txB_location.Text;
                        сhangeRSTFormCurator.cmB_model.Items.Add(cmB_model.Text).ToString();
                        сhangeRSTFormCurator.txB_serialNumber.Text = txB_serialNumber.Text;
                        сhangeRSTFormCurator.txB_inventoryNumber.Text = txB_inventoryNumber.Text;
                        сhangeRSTFormCurator.txB_networkNumber.Text = txB_networkNumber.Text;
                        сhangeRSTFormCurator.txB_dateTO.Text = txB_dateTO.Text.Remove(txB_dateTO.Text.IndexOf(" "));
                        сhangeRSTFormCurator.txB_numberAct.Text = txB_numberAct.Text;
                        сhangeRSTFormCurator.txB_numberActRemont.Text = txB_numberActRemont.Text;
                        сhangeRSTFormCurator.cmB_сategory.Text = cmB_сategory.Text;
                        сhangeRSTFormCurator.txB_priceRemont.Text = txB_priceRemont.Text;
                        сhangeRSTFormCurator.txB_decommission.Text = txB_decommission.Text;
                        сhangeRSTFormCurator.txB_comment.Text = txB_comment.Text;
                        сhangeRSTFormCurator.cmB_month.Text = txB_month.Text;
                        if (Application.OpenForms["СhangeRSTFormCurator"] == null)
                        {
                            сhangeRSTFormCurator.Show();
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка открытия формы изменения радиостанции СhangeRSTForm (Button_new_add_rst_form_Click_change_curator)");
                }
            }
        }
        #endregion

        #region изменения РСТ в выполнение по плану

        void AddExecutionCurator(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 1)
            {
                string Mesage;
                Mesage = $"Вы действительно хотите добавить радиостанции в выполнение: {txB_company.Text}?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                string Mesage;
                Mesage = $"Вы действительно хотите добавить радиостанцию в выполнение: {txB_serialNumber.Text}, предприятия: {txB_company.Text}?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }
            ContextMenu m = new ContextMenu();
            m.MenuItems.Add(new MenuItem("Январь", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Январь", comboBox_city.Text)));
            m.MenuItems.Add(new MenuItem("Февраль", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Февраль", comboBox_city.Text)));
            m.MenuItems.Add(new MenuItem("Март", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Март", comboBox_city.Text)));
            m.MenuItems.Add(new MenuItem("Апрель", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Апрель", comboBox_city.Text)));
            m.MenuItems.Add(new MenuItem("Май", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Май", comboBox_city.Text)));
            m.MenuItems.Add(new MenuItem("Июнь", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Июнь", comboBox_city.Text)));
            m.MenuItems.Add(new MenuItem("Июль", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Июль", comboBox_city.Text)));
            m.MenuItems.Add(new MenuItem("Август", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Август", comboBox_city.Text)));
            m.MenuItems.Add(new MenuItem("Сентябрь", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Сентябрь", comboBox_city.Text)));
            m.MenuItems.Add(new MenuItem("Октябрь", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Октябрь", comboBox_city.Text)));
            m.MenuItems.Add(new MenuItem("Ноябрь", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Ноябрь", comboBox_city.Text)));
            m.MenuItems.Add(new MenuItem("Декабрь", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Декабрь", comboBox_city.Text)));

            m.Show(dataGridView1, new Point(dataGridView1.Location.X + 700, dataGridView1.Location.Y));

        }

        #endregion


        #region ContextMenu datagrid
        void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (txB_serialNumber.Text != "")
                {
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("Изменить выполнение РСТ", AddExecutionCurator));
                    m.MenuItems.Add(new MenuItem("Изменить радиостанцию", Button_new_add_rst_form_Click_change_curator));
                    m.MenuItems.Add(new MenuItem("Убрать из выполнения", Button_delete_Click));
                    m.MenuItems.Add(new MenuItem("Сохранение БД", Button_save_in_file_Click));
                    m.Show(dataGridView1, new Point(e.X, e.Y));
                }
            }
        }
        #endregion

        #region для выбора значения в Control(TXB)

        void Refresh_values_TXB_CMB(int currRowIndex)
        {
            try
            {
                DataGridViewRow row = dataGridView1.Rows[currRowIndex];

                txB_id.Text = row.Cells[0].Value.ToString();
                txB_poligon.Text = row.Cells[1].Value.ToString();
                txB_company.Text = row.Cells[2].Value.ToString();
                txB_location.Text = row.Cells[3].Value.ToString();
                cmB_model.Text = row.Cells[4].Value.ToString();
                txB_serialNumber.Text = row.Cells[5].Value.ToString();
                txB_inventoryNumber.Text = row.Cells[6].Value.ToString();
                txB_networkNumber.Text = row.Cells[7].Value.ToString();
                txB_dateTO.Text = row.Cells[8].Value.ToString();
                txB_numberAct.Text = row.Cells[9].Value.ToString();
                txB_city.Text = row.Cells[10].Value.ToString();
                txB_price.Text = row.Cells[11].Value.ToString();
                txB_numberActRemont.Text = row.Cells[12].Value.ToString();
                cmB_сategory.Text = row.Cells[13].Value.ToString();
                txB_priceRemont.Text = row.Cells[14].Value.ToString();
                txB_decommission.Text = row.Cells[15].Value.ToString();
                txB_comment.Text = row.Cells[16].Value.ToString();
                txB_month.Text = row.Cells[17].Value.ToString();

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
            // открывем панель поиска по гриду по зав номеру РСТ
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
            {
                panel_seach_datagrid.Enabled = true;
                panel_seach_datagrid.Visible = true;
                this.ActiveControl = textBox_seach_panel_seach_datagrid;
            }
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


        #region close form
        void ST_WorkForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void ST_WorkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = FormClose.GetInstance.FClose();
        }
        #endregion

    }
}


