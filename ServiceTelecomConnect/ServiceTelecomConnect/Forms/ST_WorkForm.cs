using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private delegate DialogResult ShowOpenFileDialogInvoker();

        private static string taskCity;

        int selectedRow;

        private readonly cheakUser _user;

        #endregion

        public ST_WorkForm(cheakUser user)
        {
            try
            {
                InitializeComponent();

                StartPosition = FormStartPosition.CenterScreen;
                cmB_seach.Text = cmB_seach.Items[2].ToString();

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
            if (_user.IsAdmin == "Дирекция связи")
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

                cmB_city.Enabled = true;
                btn_seach_BD_city.Enabled = true;
                btn_add_city.Enabled = true;
                btn_all_BD.Enabled = true;
                picB_update.Enabled = true;
                cmB_seach.Enabled = true;
                textBox_search.Enabled = true;
                btn_search.Enabled = true;
                cmb_number_unique_acts.Enabled = true;
                btn_search.Enabled = true;
            }
            else if (_user.IsAdmin == "Инженер")
            {
                //panel1.Enabled = false;
                panel3.Enabled = false;
                Functional_loading_panel.Enabled = false;
                panel_remont_information_company.Enabled = false;

                foreach (Control element in panel1.Controls)
                {
                    element.Enabled = false;
                }

                cmB_city.Enabled = true;
                btn_seach_BD_city.Enabled = true;
                btn_add_city.Enabled = true;
                btn_all_BD.Enabled = true;
                picB_update.Enabled = true;
                cmB_seach.Enabled = true;
                textBox_search.Enabled = true;
                btn_search.Enabled = true;
                cmb_number_unique_acts.Enabled = true;
                btn_search.Enabled = true;
                btn_form_act.Enabled = true;
            }
        }


        private void ST_WorkForm_Load(object sender, EventArgs e)
        {
            try
            {
                QuerySettingDataBase.GettingTeamdata(lbL_FIO_chief, lbL_FIO_Engineer, lbL_doverennost, lbL_road, lbL_numberPrintDocument, _user.Login, cmB_road);

                dataGridView1.EnableHeadersVisualStyles = false;
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12f, FontStyle.Bold); //жирный курсив размера 16
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; //цвет текста
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; //цвет ячейки

                QuerySettingDataBase.SelectCityGropBy(cmB_city);

                QuerySettingDataBase.CreateColums(dataGridView1);
                QuerySettingDataBase.CreateColums(dataGridView2);
                QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text);
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


                taskCity = cmB_city.Text;// для отдельных потоков

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
            QuerySettingDataBase.RefreshDataGridTimerEventProcessor(dataGridView2, taskCity);
            new Thread(() => { FunctionPanel.Get_date_save_datagridview_json(dataGridView2, taskCity); }) { IsBackground = true }.Start();
            new Thread(() => { SaveFileDataGridViewPC.AutoSaveFilePC(dataGridView2, taskCity); }) { IsBackground = true }.Start();
            new Thread(() => { QuerySettingDataBase.Copy_BD_radiostantion_in_radiostantion_copy(); }) { IsBackground = true }.Start();
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

                lbL_count.Text = dataGridView1.Rows.Count.ToString();
                lbL_summ.Text = sumTO.ToString();
                lbL_count_remont.Text = colRemont.ToString();
                lbL_summ_remont.Text = sumRemont.ToString();
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
            QuerySettingDataBase.Full_BD(dataGridView1);
            Counters();
            txb_flag_all_BD.Text = "Вся БД";
        }

        #endregion

        #region загрузка городов CmB_city_Click
        void CmB_city_Click(object sender, EventArgs e)
        {
            QuerySettingDataBase.SelectCityGropBy(cmB_city);
        }
        #endregion

        #region panel date information

        void Button_close_panel_date_info_Click(object sender, EventArgs e)
        {
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
                helloKey.SetValue("Город проведения проверки", $"{cmB_city.Text}");
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
                    cmB_poligon.Text = row.Cells[1].Value.ToString();
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
                    txB_representative.Text = row.Cells[12].Value.ToString();
                    txB_post.Text = row.Cells[13].Value.ToString();
                    txB_numberIdentification.Text = row.Cells[14].Value.ToString();
                    txB_dateIssue.Text = row.Cells[15].Value.ToString();
                    txB_phoneNumber.Text = row.Cells[16].Value.ToString();
                    txB_numberActRemont.Text = row.Cells[17].Value.ToString();
                    cmB_сategory.Text = row.Cells[18].Value.ToString();
                    txB_priceRemont.Text = row.Cells[19].Value.ToString();
                    txB_antenna.Text = row.Cells[20].Value.ToString();
                    txB_manipulator.Text = row.Cells[21].Value.ToString();
                    txB_AKB.Text = row.Cells[22].Value.ToString();
                    txB_batteryСharger.Text = row.Cells[23].Value.ToString();
                    txB_сompleted_works_1.Text = row.Cells[24].Value.ToString();
                    txB_сompleted_works_2.Text = row.Cells[25].Value.ToString();
                    txB_сompleted_works_3.Text = row.Cells[26].Value.ToString();
                    txB_сompleted_works_4.Text = row.Cells[27].Value.ToString();
                    txB_сompleted_works_5.Text = row.Cells[28].Value.ToString();
                    txB_сompleted_works_6.Text = row.Cells[29].Value.ToString();
                    txB_сompleted_works_7.Text = row.Cells[30].Value.ToString();
                    txB_parts_1.Text = row.Cells[31].Value.ToString();
                    txB_parts_2.Text = row.Cells[32].Value.ToString();
                    txB_parts_3.Text = row.Cells[33].Value.ToString();
                    txB_parts_4.Text = row.Cells[34].Value.ToString();
                    txB_parts_5.Text = row.Cells[35].Value.ToString();
                    txB_parts_6.Text = row.Cells[36].Value.ToString();
                    txB_parts_7.Text = row.Cells[37].Value.ToString();
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

                m.Show(dataGridView1, new Point(dataGridView1.Location.X + 700, dataGridView1.Location.Y));

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

                if (!String.IsNullOrEmpty(txB_decommissionSerialNumber.Text))
                {
                    string Mesage;
                    Mesage = $"На РСТ №: {txB_serialNumber.Text}, предприятия: {txB_company.Text} есть списание. Точно удалить?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (!String.IsNullOrEmpty(txB_numberActRemont.Text))
                {
                    string Mesage;
                    Mesage = $"На РСТ №: {txB_serialNumber.Text}, предприятия: {txB_company.Text} есть ремонт. Точно удалить?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                QuerySettingDataBase.DeleteRowCell(dataGridView1);

                int currRowIndex = dataGridView1.CurrentCell.RowIndex;

                QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text);
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
                    QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text);
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
                    QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text);
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
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    AddRSTForm addRSTForm = new AddRSTForm();
                    if (Application.OpenForms["AddRSTForm"] == null)
                    {
                        addRSTForm.DoubleBufferedForm(true);
                        addRSTForm.txB_numberAct.Text = lbL_numberPrintDocument.Text + "/";
                        addRSTForm.lbL_cmb_city_ST_WorkForm.Text = cmB_city.Text;
                        if (txB_city.Text == "")
                        {
                            addRSTForm.txB_city.Text = cmB_city.Text;
                        }
                        else addRSTForm.txB_city.Text = txB_city.Text;
                        addRSTForm.cmB_poligon.Text = cmB_poligon.Text;
                        addRSTForm.txB_company.Text = txB_company.Text;
                        addRSTForm.txB_location.Text = txB_location.Text;
                        addRSTForm.cmB_model.Text = cmB_model.Text;
                        addRSTForm.cmB_model.Text = cmB_model.Text;
                        addRSTForm.txB_representative.Text = txB_representative.Text;
                        addRSTForm.txB_numberIdentification.Text = txB_numberIdentification.Text;
                        addRSTForm.txB_phoneNumber.Text = txB_phoneNumber.Text;
                        addRSTForm.txB_post.Text = txB_post.Text;
                        addRSTForm.txB_dateIssue.Text = txB_dateIssue.Text;
                        addRSTForm.Show();
                    }

                    #region старая сортировка получения крайнего акта с помощью грида
                    //if (dataGridView1.RowCount != 0)
                    //{
                    //    this.dataGridView1.Sort(this.dataGridView1.Columns["numberAct"], ListSortDirection.Ascending);
                    //    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
                    //    DataGridViewRow row = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
                    //    addRSTForm.lbL_last_act.Text = row.Cells[9].Value.ToString();
                    //    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    //    {
                    //        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    //    }
                    //}
                    //foreach (DataGridViewColumn column in dataGridView1.Columns)
                    //{
                    //    column.SortMode = DataGridViewColumnSortMode.Automatic;
                    //}
                    #endregion
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка создания формы AddRSTForm(Button_new_add_rst_form_Click)");
                }
            }

        }
        #endregion

        #region ProcessKbdCtrlShortcuts

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
        
        #endregion

        #region АКТ => excel

        void Button_form_act_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txB_decommissionSerialNumber.Text))
                {
                    MessageBox.Show($"Нельзя напечатать акт ТО, на радиостанцию номер: {txB_serialNumber.Text} от предприятия {txB_company.Text}, есть списание!");
                    return;
                }
                QuerySettingDataBase.Update_datagridview_number_act(dataGridView1, txB_city.Text, txB_numberAct.Text);
                int currRowIndex = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.ClearSelection();

                if (dataGridView1.CurrentCell.RowIndex >= 0)
                {
                    dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
                }
                Refresh_values_TXB_CMB(currRowIndex);
                if (txB_numberAct.Text != "")
                {
                    dataGridView1.Sort(dataGridView1.Columns["model"], ListSortDirection.Ascending);
                }
                PrintExcel.PrintExcelActTo(dataGridView1, txB_numberAct.Text, txB_dateTO.Text, txB_company.Text, txB_location.Text,
                    lbL_FIO_chief.Text, txB_post.Text, txB_representative.Text, txB_numberIdentification.Text, lbL_FIO_Engineer.Text,
                    lbL_doverennost.Text, lbL_road.Text, txB_dateIssue.Text, txB_city.Text, cmB_poligon.Text);
                QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text);
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
            if (txB_Full_name_company.Text != "" && txB_OKPO_remont.Text != "" && txB_BE_remont.Text != ""
                                && txB_director_FIO_remont_company.Text != "" && txB_director_post_remont_company.Text != ""
                                && txB_chairman_FIO_remont_company.Text != "" && txB_chairman_post_remont_company.Text != ""
                                && txB_1_FIO_remont_company.Text != "" && txB_1_post_remont_company.Text != ""
                                && txB_2_FIO_remont_company.Text != "" && txB_2_post_remont_company.Text != "")
            {

                panel_remont_information_company.Visible = false;
                panel_remont_information_company.Enabled = false;

                string mainMeans = QuerySettingDataBase.Loading_OC_6_values(txB_serialNumber.Text).Item1;
                string nameProductRepaired = QuerySettingDataBase.Loading_OC_6_values(txB_serialNumber.Text).Item2;

                PrintExcel.PrintExcelActRemont(dataGridView1, txB_dateTO.Text, txB_company.Text, txB_location.Text,
                     lbL_FIO_chief.Text, txB_post.Text, txB_representative.Text, txB_numberIdentification.Text, lbL_FIO_Engineer.Text,
                     lbL_doverennost.Text, lbL_road.Text, txB_dateIssue.Text, txB_city.Text, cmB_poligon.Text, cmB_сategory.Text,
                     cmB_model.Text, txB_serialNumber.Text, txB_inventoryNumber.Text, txB_networkNumber.Text, txB_сompleted_works_1.Text,
                     txB_parts_1.Text, txB_сompleted_works_2.Text, txB_parts_2.Text, txB_сompleted_works_3.Text, txB_parts_3.Text,
                     txB_сompleted_works_4.Text, txB_parts_4.Text, txB_сompleted_works_5.Text, txB_parts_5.Text, txB_сompleted_works_6.Text,
                     txB_parts_6.Text, txB_сompleted_works_7.Text, txB_parts_7.Text, txB_OKPO_remont.Text, txB_BE_remont.Text,
                     txB_Full_name_company.Text, txB_director_FIO_remont_company.Text, txB_numberActRemont.Text,
                     txB_chairman_post_remont_company.Text, txB_chairman_FIO_remont_company.Text, txB_1_post_remont_company.Text,
                     txB_1_FIO_remont_company.Text, txB_2_post_remont_company.Text, txB_2_FIO_remont_company.Text,
                     txB_3_post_remont_company.Text, txB_3_FIO_remont_company.Text, mainMeans, nameProductRepaired);
                panel1.Enabled = true;
            }
        }

        #endregion

        #region Сохранение БД на PC

        void Button_save_in_file_Click(object sender, EventArgs e)
        {
            try
            {
                pnL_printBase.Visible = true;
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
                QuerySettingDataBase.Search(dataGridView1, cmB_seach.Text, cmB_city.Text, textBox_search.Text, cmb_number_unique_acts.Text);
                Counters();
            }
        }

        void Button_search_Click(object sender, EventArgs e)
        {
            QuerySettingDataBase.Search(dataGridView1, cmB_seach.Text, cmB_city.Text, textBox_search.Text, cmb_number_unique_acts.Text);
            Counters();
        }

        void Button_seach_BD_city_Click(object sender, EventArgs e)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.CreateSubKey("SOFTWARE\\ServiceTelekom_Setting");
            helloKey.SetValue("Город проведения проверки", $"{cmB_city.Text}");
            helloKey.Close();

            QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text);
            QuerySettingDataBase.SelectCityGropBy(cmB_city);
            Counters();

            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ServiceTelekom_Setting\\");
            if (reg != null)
            {
                RegistryKey currentUserKey2 = Registry.CurrentUser;
                RegistryKey helloKey2 = currentUserKey2.OpenSubKey("SOFTWARE\\ServiceTelekom_Setting");
                cmB_city.Text = helloKey2.GetValue("Город проведения проверки").ToString();
            }
        }

        void TextBox_numberAct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                QuerySettingDataBase.Update_datagridview_number_act(dataGridView1, cmB_city.Text, txB_numberAct.Text);
                Counters();
            }
        }

        void TextBox_numberAct_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (txB_numberAct.Text != "")
            {
                QuerySettingDataBase.Update_datagridview_number_act(dataGridView1, cmB_city.Text, txB_numberAct.Text);
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

        #region поиск отсутсвующих рст исходя из предыдущего года

        void PictureBox_seach_datadrid_replay_Click(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            panel3.Enabled = false;
            QuerySettingDataBase.Seach_DataGrid_Replay_RST(dataGridView1, txb_flag_all_BD, cmB_city.Text);
            Counters();
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
                        m3.MenuItems.Add(new MenuItem("Обновить", Button_update_Click));
                        m3.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                    else if (_user.IsAdmin == "Куратор")
                    {
                        if (dataGridView1.Rows.Count > 0 && panel1.Enabled == true && panel3.Enabled == true)
                        {
                            ContextMenu m = new ContextMenu();

                            var add_new_radio_station = m.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", Button_new_add_rst_form_Click));
                            if (txB_serialNumber.Text != "")
                            {
                                m.MenuItems.Add(new MenuItem("Изменить радиостанцию", Button_change_rst_form_Click));
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
                            m1.MenuItems.Add(new MenuItem("Обновить", Button_update_Click));

                            m1.Show(dataGridView1, new Point(e.X, e.Y));
                        }
                        else if (dataGridView1.Rows.Count > 0 || dataGridView1.Rows.Count == 0 && panel1.Enabled == false && panel3.Enabled == false)
                        {
                            ContextMenu m2 = new ContextMenu();
                            m2.MenuItems.Add(new MenuItem("Сохранение базы", Button_save_in_file_Click));
                            m2.MenuItems.Add(new MenuItem("Обновить", Button_update_Click_after_Seach_DataGrid_Replay_RST));

                            m2.Show(dataGridView1, new Point(e.X, e.Y));

                            if (e.Button == MouseButtons.Left)
                            {
                                dataGridView1.ClearSelection();
                            }
                        }
                    }
                    else if (_user.IsAdmin == "Начальник участка" || _user.IsAdmin == "Руководитель" || _user.IsAdmin == "Admin")
                    {
                        if (dataGridView1.Rows.Count > 0 && panel1.Enabled == true && panel3.Enabled == true)
                        {
                            ContextMenu m = new ContextMenu();

                            var add_new_radio_station = m.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", Button_new_add_rst_form_Click));
                            if (txB_serialNumber.Text != "")
                            {
                                m.MenuItems.Add(new MenuItem("Изменить радиостанцию", Button_change_rst_form_Click));
                                m.MenuItems.Add(new MenuItem("Добавить/изменить ремонт", Button_new_add_rst_form_click_remont));
                                m.MenuItems.Add(new MenuItem("Сформировать акт ТО", Button_form_act_Click));
                                m.MenuItems.Add(new MenuItem("Сформировать акт Ремонта", Button_remont_act_Click));
                                m.MenuItems.Add(new MenuItem("Удалить радиостанцию", Button_delete_Click));
                                m.MenuItems.Add(new MenuItem("Удалить ремонт", Delete_rst_remont_click));
                                m.MenuItems.Add(new MenuItem("Заполняем акт", DataGridView1_DefaultCellStyleChanged));
                                m.MenuItems.Add(new MenuItem("На подпись", DataGridView1_Sign));
                                m.MenuItems.Add(new MenuItem("Списать РСТ", DecommissionSerialNumber));
                                m.MenuItems.Add(new MenuItem("Показать РСТ без списаний по участку", Btn_RefreshDataGridWithoutDecommission));
                                m.MenuItems.Add(new MenuItem("Показать списанные РСТ по участку", Btn_RefreshDataGridtDecommissionByPlot));
                            }
                            if (txB_decommissionSerialNumber.Text != "")
                            {
                                m.MenuItems.Add(new MenuItem("Сформировать акт списания", PrintWord_Act_decommission));
                                m.MenuItems.Add(new MenuItem("Удалить списание", Delete_rst_decommission_click));
                            }
                            m.MenuItems.Add(new MenuItem("Обновить", Button_update_Click));
                            m.MenuItems.Add(new MenuItem("Сохранение базы", Button_save_in_file_Click));
                            m.MenuItems.Add(new MenuItem("Показать совпадение с предыдущим годом", PictureBox_seach_datadrid_replay_Click));
                            m.MenuItems.Add(new MenuItem("Показать все списания", Show_radiostantion_decommission_Click));
                            m.MenuItems.Add(new MenuItem("Сформировать бирки", FormTag));

                            m.Show(dataGridView1, new Point(e.X, e.Y));

                        }
                        else if (dataGridView1.Rows.Count == 0 && panel1.Enabled == true && panel3.Enabled == true)
                        {
                            ContextMenu m1 = new ContextMenu();
                            m1.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", Button_new_add_rst_form_Click));
                            m1.MenuItems.Add(new MenuItem("Обновить", Button_update_Click));

                            m1.Show(dataGridView1, new Point(e.X, e.Y));
                        }
                        else if (dataGridView1.Rows.Count > 0 || dataGridView1.Rows.Count == 0 && panel1.Enabled == false && panel3.Enabled == false)
                        {
                            ContextMenu m2 = new ContextMenu();
                            m2.MenuItems.Add(new MenuItem("Сохранение базы", Button_save_in_file_Click));
                            m2.MenuItems.Add(new MenuItem("Обновить", Button_update_Click_after_Seach_DataGrid_Replay_RST));

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
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    if (dataGridView1.Rows.Count >= 0)
                    {
                        panel1.Enabled = true;
                        panel3.Enabled = true;
                        QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text);
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
                Mesage = $"Вы действительно хотите удалить ремонт у радиостанции: {txB_serialNumber.Text}, предприятия: {txB_company.Text}?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                QuerySettingDataBase.Delete_rst_remont(txB_numberActRemont.Text, txB_serialNumber.Text);
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
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    if (txB_serialNumber.Text != "")
                    {
                        if (!String.IsNullOrEmpty(txB_decommissionSerialNumber.Text))
                        {
                            MessageBox.Show($"Нельзя добавить ремонт, на радиостанцию номер: {txB_serialNumber.Text} от предприятия {txB_company.Text}, есть списание!");
                            return;
                        }

                        RemontRSTForm remontRSTForm = new RemontRSTForm();
                        if (Application.OpenForms["RemontRSTForm"] == null)
                        {
                            remontRSTForm.DoubleBufferedForm(true);
                            remontRSTForm.cmB_сategory.Text = cmB_сategory.Text;
                            remontRSTForm.txB_priceRemont.Text = txB_priceRemont.Text;
                            remontRSTForm.txB_сompleted_works_1.Text = txB_сompleted_works_1.Text;
                            remontRSTForm.txB_сompleted_works_2.Text = txB_сompleted_works_2.Text;
                            remontRSTForm.txB_сompleted_works_3.Text = txB_сompleted_works_3.Text;
                            remontRSTForm.txB_сompleted_works_4.Text = txB_сompleted_works_4.Text;
                            remontRSTForm.txB_сompleted_works_5.Text = txB_сompleted_works_5.Text;
                            remontRSTForm.txB_сompleted_works_6.Text = txB_сompleted_works_6.Text;
                            remontRSTForm.txB_сompleted_works_7.Text = txB_сompleted_works_7.Text;
                            remontRSTForm.txB_parts_1.Text = txB_parts_1.Text;
                            remontRSTForm.txB_parts_2.Text = txB_parts_2.Text;
                            remontRSTForm.txB_parts_3.Text = txB_parts_3.Text;
                            remontRSTForm.txB_parts_4.Text = txB_parts_4.Text;
                            remontRSTForm.txB_parts_5.Text = txB_parts_5.Text;
                            remontRSTForm.txB_parts_6.Text = txB_parts_6.Text;
                            remontRSTForm.txB_parts_7.Text = txB_parts_7.Text;

                            if (txB_dateTO.Text != "")
                            {
                                txB_dateTO.Text = DateTime.Now.ToString("dd.MM.yyyy");
                            }

                            remontRSTForm.txB_data_remont.Text = txB_dateTO.Text;
                            remontRSTForm.txB_model.Text = cmB_model.Text;
                            remontRSTForm.label_company.Text = txB_company.Text;
                            remontRSTForm.txB_serialNumber.Text = txB_serialNumber.Text;

                            if (txB_numberActRemont.Text == "")
                            {
                                remontRSTForm.txB_numberActRemont.Text = lbL_numberPrintDocument.Text + "/";
                            }
                            else remontRSTForm.txB_numberActRemont.Text = txB_numberActRemont.Text;
                            remontRSTForm.Show();
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
        private void Button_change_rst_form_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    if (txB_serialNumber.Text != "")
                    {
                        СhangeRSTForm changeRSTForm = new СhangeRSTForm();
                        if (Application.OpenForms["СhangeRSTForm"] == null)
                        {

                            changeRSTForm.DoubleBufferedForm(true);
                            changeRSTForm.txB_city.Text = txB_city.Text;
                            changeRSTForm.cmB_poligon.Text = cmB_poligon.Text;
                            changeRSTForm.txB_company.Text = txB_company.Text;
                            changeRSTForm.txB_location.Text = txB_location.Text;
                            changeRSTForm.cmB_model.Items.Add(cmB_model.Text).ToString();
                            changeRSTForm.txB_serialNumber.Text = txB_serialNumber.Text;
                            changeRSTForm.txB_inventoryNumber.Text = txB_inventoryNumber.Text;
                            changeRSTForm.txB_networkNumber.Text = txB_networkNumber.Text;
                            String dateTO = Convert.ToDateTime(txB_dateTO.Text).ToString("dd.MM.yyyy");
                            changeRSTForm.txB_dateTO.Text = dateTO;
                            changeRSTForm.txB_numberAct.Text = txB_numberAct.Text;
                            changeRSTForm.txB_representative.Text = txB_representative.Text;
                            changeRSTForm.txB_numberIdentification.Text = txB_numberIdentification.Text;
                            changeRSTForm.txB_phoneNumber.Text = txB_phoneNumber.Text;
                            changeRSTForm.txB_post.Text = txB_post.Text;
                            changeRSTForm.txB_comment.Text = txB_comment.Text;

                            if (!String.IsNullOrEmpty(txB_decommissionSerialNumber.Text))
                            {
                                changeRSTForm.txB_decommissionSerialNumber.Text = txB_decommissionSerialNumber.Text;
                            }

                            if (txB_dateIssue.Text == "")
                            {
                                txB_dateIssue.Text = DateTime.Now.ToString("dd.MM.yyyy");
                            }
                            changeRSTForm.txB_dateIssue.Text = txB_dateIssue.Text;

                            if (txB_antenna.Text == "")
                            {
                                txB_antenna.Text = "-";
                            }
                            changeRSTForm.txB_antenna.Text = txB_antenna.Text;
                            if (txB_manipulator.Text == "")
                            {
                                txB_manipulator.Text = "-";
                            }
                            changeRSTForm.txB_manipulator.Text = txB_manipulator.Text;
                            if (txB_batteryСharger.Text == "")
                            {
                                txB_batteryСharger.Text = "-";
                            }
                            changeRSTForm.txB_batteryСharger.Text = txB_batteryСharger.Text;
                            if (txB_AKB.Text == "")
                            {
                                txB_AKB.Text = "-";
                            }
                            changeRSTForm.txB_AKB.Text = txB_AKB.Text;
                            changeRSTForm.Show();
                        }
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

        void Button_remont_act_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (txB_numberActRemont.Text == "")
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
                        label_company_remont.Text = txB_company.Text;
                        RegistryKey reg = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\{txB_company.Text}");
                        if (reg != null)
                        {
                            RegistryKey currentUserKey = Registry.CurrentUser;
                            RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\{txB_company.Text}");

                            txB_Full_name_company.Text = helloKey.GetValue("Полное наименование предприятия").ToString();
                            txB_OKPO_remont.Text = helloKey.GetValue("ОКПО").ToString();
                            txB_BE_remont.Text = helloKey.GetValue("БЕ").ToString();
                            txB_director_FIO_remont_company.Text = helloKey.GetValue("Руководитель ФИО").ToString();
                            txB_director_post_remont_company.Text = helloKey.GetValue("Руководитель Должность").ToString();
                            txB_chairman_FIO_remont_company.Text = helloKey.GetValue("Председатель ФИО").ToString();
                            txB_chairman_post_remont_company.Text = helloKey.GetValue("Председатель Должность").ToString();
                            txB_1_FIO_remont_company.Text = helloKey.GetValue("1 член комиссии ФИО").ToString();
                            txB_1_post_remont_company.Text = helloKey.GetValue("1 член комиссии Должность").ToString();
                            txB_2_FIO_remont_company.Text = helloKey.GetValue("2 член комиссии ФИО").ToString();
                            txB_2_post_remont_company.Text = helloKey.GetValue("2 член комиссии Должность").ToString();
                            txB_3_FIO_remont_company.Text = helloKey.GetValue("3 член комиссии ФИО").ToString();
                            txB_3_post_remont_company.Text = helloKey.GetValue("3 член комиссии Должность").ToString();

                            if (txB_Full_name_company.Text != "" && txB_OKPO_remont.Text != "" && txB_BE_remont.Text != ""
                                && txB_director_FIO_remont_company.Text != "" && txB_director_post_remont_company.Text != ""
                                && txB_chairman_FIO_remont_company.Text != "" && txB_chairman_post_remont_company.Text != ""
                                && txB_1_FIO_remont_company.Text != "" && txB_1_post_remont_company.Text != ""
                                && txB_2_FIO_remont_company.Text != "" && txB_2_post_remont_company.Text != "")
                            {
                                btn_Continue_remont_act_excel.Enabled = true;
                            }
                            helloKey.Close();
                        }
                        else
                        {
                            btn_Continue_remont_act_excel.Enabled = false;
                            txB_Full_name_company.Text = "";
                            txB_OKPO_remont.Text = "";
                            txB_BE_remont.Text = "";
                            txB_director_FIO_remont_company.Text = "";
                            txB_director_post_remont_company.Text = $"Начальник {txB_company.Text}";
                            txB_chairman_FIO_remont_company.Text = "";
                            txB_chairman_post_remont_company.Text = "";
                            txB_1_FIO_remont_company.Text = "";
                            txB_1_post_remont_company.Text = "";
                            txB_2_FIO_remont_company.Text = "";
                            txB_2_post_remont_company.Text = "";
                            txB_3_FIO_remont_company.Text = "";
                            txB_3_post_remont_company.Text = "";
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка открытия панели для формирования ремонта (Button_remont_act_Click)");
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
                if (txB_Full_name_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"Полное наименование предприятия\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (txB_OKPO_remont.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"ОКПО\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (txB_BE_remont.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"БЕ\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (txB_director_FIO_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"Руководитель ФИО\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }

                if (txB_director_post_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"Руководитель Должность\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }

                if (txB_chairman_FIO_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"Председатель ФИО\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }

                if (txB_chairman_post_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"Председатель Должность\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }

                if (txB_1_FIO_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"1 член комиссии ФИО\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }

                if (txB_1_post_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"1 член комиссии Должность\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                if (txB_2_FIO_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"2 член комиссии ФИО\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }

                if (txB_2_post_remont_company.Text == "")
                {
                    string Mesage2;
                    Mesage2 = "Вы не заполнили поле \"2 член комиссии Должность\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
                #endregion

                if (!Regex.IsMatch(txB_OKPO_remont.Text, @"^[0-9]{8,}$"))
                {
                    MessageBox.Show("Введите корректно поле \"ОКПО\"\nP.s. пример: 00083262", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_OKPO_remont.Select();
                    string Mesage = "Вы действительно хотите продолжить?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (!Regex.IsMatch(txB_BE_remont.Text, @"^[0-9]{4,}$"))
                {
                    MessageBox.Show("Введите корректно поле \"БЕ\"\nP.s. пример: 5374", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_BE_remont.Select();
                    string Mesage = "Вы действительно хотите продолжить?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (!Regex.IsMatch(txB_Full_name_company.Text, @"[А-Яа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
                {
                    MessageBox.Show("Введите корректно поле \"Полное наименование предприятия\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_Full_name_company.Select();
                    string Mesage = "Вы действительно хотите продолжить?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                if (!txB_director_FIO_remont_company.Text.Contains("-"))
                {
                    if (!Regex.IsMatch(txB_director_FIO_remont_company.Text, @"^[А-ЯЁ][а-яё]*(([\s]+[А-Я][\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"Руководитель ФИО\"\nP.s. пример: Иванов В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_director_FIO_remont_company.Select();
                        return;
                    }
                }
                if (txB_director_FIO_remont_company.Text.Contains("-"))
                {
                    if (!Regex.IsMatch(txB_director_FIO_remont_company.Text, @"^[А-ЯЁ][а-яё]*(([\-][А-Я][а-яё]*[\s]+[А-Я]+[\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"Руководитель ФИО\"\nP.s. пример: Иванов-Петров В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_director_FIO_remont_company.Select();
                        return;
                    }
                }

                if (!txB_chairman_FIO_remont_company.Text.Contains("-"))
                {
                    if (!Regex.IsMatch(txB_chairman_FIO_remont_company.Text, @"^[А-ЯЁ][а-яё]*(([\s]+[А-Я][\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"Председатель ФИО\"\nP.s. пример: Иванов В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_chairman_FIO_remont_company.Select();
                        return;
                    }
                }
                if (txB_chairman_FIO_remont_company.Text.Contains("-"))
                {
                    if (!Regex.IsMatch(txB_chairman_FIO_remont_company.Text, @"^[А-ЯЁ][а-яё]*(([\-][А-Я][а-яё]*[\s]+[А-Я]+[\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"Председатель ФИО\"\nP.s. пример: Иванов-Петров В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_chairman_FIO_remont_company.Select();
                        return;
                    }
                }

                if (!txB_1_FIO_remont_company.Text.Contains("-"))
                {
                    if (!Regex.IsMatch(txB_1_FIO_remont_company.Text, @"^[А-ЯЁ][а-яё]*(([\s]+[А-Я][\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"1 член Ком.: ФИО\"\nP.s. пример: Иванов В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_1_FIO_remont_company.Select();
                        return;
                    }
                }
                if (txB_1_FIO_remont_company.Text.Contains("-"))
                {
                    if (!Regex.IsMatch(txB_1_FIO_remont_company.Text, @"^[А-ЯЁ][а-яё]*(([\-][А-Я][а-яё]*[\s]+[А-Я]+[\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"1 член Ком.: ФИО\"\nP.s. пример: Иванов-Петров В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_1_FIO_remont_company.Select();
                        return;
                    }
                }

                if (!txB_2_FIO_remont_company.Text.Contains("-"))
                {
                    if (!Regex.IsMatch(txB_2_FIO_remont_company.Text, @"^[А-ЯЁ][а-яё]*(([\s]+[А-Я][\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"2 член Ком.: ФИО\"\nP.s. пример: Иванов В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_2_FIO_remont_company.Select();
                        return;
                    }
                }
                if (txB_2_FIO_remont_company.Text.Contains("-"))
                {
                    if (!Regex.IsMatch(txB_2_FIO_remont_company.Text, @"^[А-ЯЁ][а-яё]*(([\-][А-Я][а-яё]*[\s]+[А-Я]+[\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"2 член Ком.: ФИО\"\nP.s. пример: Иванов-Петров В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_2_FIO_remont_company.Select();
                        return;
                    }
                }

                if (!txB_3_FIO_remont_company.Text.Contains("-"))
                {
                    if (!Regex.IsMatch(txB_3_FIO_remont_company.Text, @"^[А-ЯЁ][а-яё]*(([\s]+[А-Я][\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"3 член Ком.: ФИО\"\nP.s. пример: Иванов В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_3_FIO_remont_company.Select();
                        return;
                    }
                }
                if (txB_3_FIO_remont_company.Text.Contains("-"))
                {
                    if (!Regex.IsMatch(txB_3_FIO_remont_company.Text, @"^[А-ЯЁ][а-яё]*(([\-][А-Я][а-яё]*[\s]+[А-Я]+[\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"3 член Ком.: ФИО\"\nP.s. пример: Иванов-Петров В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_3_FIO_remont_company.Select();
                        return;
                    }
                }

                if (!Regex.IsMatch(txB_director_post_remont_company.Text, @"[А-ЯЁа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
                {
                    MessageBox.Show("Введите корректно поле \"Должность руководителя\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_director_post_remont_company.Select();
                    string Mesage = "Вы действительно хотите продолжить?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (!Regex.IsMatch(txB_chairman_post_remont_company.Text, @"[А-ЯЁа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
                {
                    MessageBox.Show("Введите корректно поле \"Должность председателя\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_chairman_post_remont_company.Select();
                    string Mesage = "Вы действительно хотите продолжить?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (!Regex.IsMatch(txB_1_post_remont_company.Text, @"[А-ЯЁа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
                {
                    MessageBox.Show("Введите корректно поле \"Должность 1 члена комиссии\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_1_post_remont_company.Select();
                    string Mesage = "Вы действительно хотите продолжить?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (!Regex.IsMatch(txB_2_post_remont_company.Text, @"[А-ЯЁа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
                {
                    MessageBox.Show("Введите корректно поле \"Должность 2 члена комиссии\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_2_post_remont_company.Select();
                    string Mesage = "Вы действительно хотите продолжить?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (!Regex.IsMatch(txB_3_post_remont_company.Text, @"[А-ЯЁа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
                {
                    MessageBox.Show("Введите корректно поле \"Должность 3 члена комиссии\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_3_post_remont_company.Select();
                    string Mesage = "Вы действительно хотите продолжить?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\{txB_company.Text.Trim()}");
                helloKey.SetValue("Полное наименование предприятия", $"{txB_Full_name_company.Text.Trim()}");
                helloKey.SetValue("ОКПО", $"{txB_OKPO_remont.Text.Trim()}");
                helloKey.SetValue("БЕ", $"{txB_BE_remont.Text.Trim()}");
                helloKey.SetValue("Руководитель ФИО", $"{txB_director_FIO_remont_company.Text.Trim()}");
                helloKey.SetValue("Руководитель Должность", $"{txB_director_post_remont_company.Text.Trim()}");
                helloKey.SetValue("Председатель ФИО", $"{txB_chairman_FIO_remont_company.Text.Trim()}");
                helloKey.SetValue("Председатель Должность", $"{txB_chairman_post_remont_company.Text.Trim()}");
                helloKey.SetValue("1 член комиссии ФИО", $"{txB_1_FIO_remont_company.Text.Trim()}");
                helloKey.SetValue("1 член комиссии Должность", $"{txB_1_post_remont_company.Text.Trim()}");
                helloKey.SetValue("2 член комиссии ФИО", $"{txB_2_FIO_remont_company.Text.Trim()}");
                helloKey.SetValue("2 член комиссии Должность", $"{txB_2_post_remont_company.Text.Trim()}");
                helloKey.SetValue("3 член комиссии ФИО", $"{txB_3_FIO_remont_company.Text.Trim()}");
                helloKey.SetValue("3 член комиссии Должность", $"{txB_3_post_remont_company.Text.Trim()}");

                helloKey.Close();

                if (txB_Full_name_company.Text != "" && txB_OKPO_remont.Text != "" && txB_BE_remont.Text != ""
                            && txB_director_FIO_remont_company.Text != "" && txB_director_post_remont_company.Text != ""
                            && txB_chairman_FIO_remont_company.Text != "" && txB_chairman_post_remont_company.Text != ""
                            && txB_1_FIO_remont_company.Text != "" && txB_1_post_remont_company.Text != ""
                            && txB_2_FIO_remont_company.Text != "" && txB_2_post_remont_company.Text != "")
                {
                    btn_Continue_remont_act_excel.Enabled = true;
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
                txB_id.Text = row.Cells[0].Value.ToString();
                cmB_poligon.Text = row.Cells[1].Value.ToString();
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
                txB_representative.Text = row.Cells[12].Value.ToString();
                txB_post.Text = row.Cells[13].Value.ToString();
                txB_numberIdentification.Text = row.Cells[14].Value.ToString();
                txB_dateIssue.Text = row.Cells[15].Value.ToString();
                txB_phoneNumber.Text = row.Cells[16].Value.ToString();
                txB_numberActRemont.Text = row.Cells[17].Value.ToString();
                cmB_сategory.Text = row.Cells[18].Value.ToString();
                txB_priceRemont.Text = row.Cells[19].Value.ToString();
                txB_antenna.Text = row.Cells[20].Value.ToString();
                txB_manipulator.Text = row.Cells[21].Value.ToString();
                txB_AKB.Text = row.Cells[22].Value.ToString();
                txB_batteryСharger.Text = row.Cells[23].Value.ToString();
                txB_сompleted_works_1.Text = row.Cells[24].Value.ToString();
                txB_сompleted_works_2.Text = row.Cells[25].Value.ToString();
                txB_сompleted_works_3.Text = row.Cells[26].Value.ToString();
                txB_сompleted_works_4.Text = row.Cells[27].Value.ToString();
                txB_сompleted_works_5.Text = row.Cells[28].Value.ToString();
                txB_сompleted_works_6.Text = row.Cells[29].Value.ToString();
                txB_сompleted_works_7.Text = row.Cells[30].Value.ToString();
                txB_parts_1.Text = row.Cells[31].Value.ToString();
                txB_parts_2.Text = row.Cells[32].Value.ToString();
                txB_parts_3.Text = row.Cells[33].Value.ToString();
                txB_parts_4.Text = row.Cells[34].Value.ToString();
                txB_parts_5.Text = row.Cells[35].Value.ToString();
                txB_parts_6.Text = row.Cells[36].Value.ToString();
                txB_parts_7.Text = row.Cells[37].Value.ToString();
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
                    this.ActiveControl = txB_seach_panel_seach_datagrid;
                }

                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
                {
                    if (txB_representative.Text != "")
                    {
                        panel_info_phone_FIO.Enabled = true;
                        panel_info_phone_FIO.Visible = true;
                        panel_txB_FIO_representative.Text = txB_representative.Text;
                        panel_txB_FIO_phoneNumber.Text = txB_phoneNumber.Text;
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
                    this.ActiveControl = txB_seach_panel_seach_datagrid;
                }
                // открываем функциональную панель
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.K)
                {
                    Button_Functional_loading_panel(sender, e);
                }
                // открываем панель инфы о ФИО и номере баланосдержателя
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
                {
                    if (txB_representative.Text != "")
                    {
                        panel_info_phone_FIO.Enabled = true;
                        panel_info_phone_FIO.Visible = true;
                        panel_txB_FIO_representative.Text = txB_representative.Text;
                        panel_txB_FIO_phoneNumber.Text = txB_phoneNumber.Text;
                    }
                }
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.I)
                {
                    dataGridView1.Enabled = false;
                    panel1.Enabled = false;
                    panel3.Enabled = false;
                    panel_date.Enabled = true;
                    panel_date.Visible = true;
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
            bool found = false;
            if (txB_seach_panel_seach_datagrid.Text != "")
            {
                string searchValue = txB_seach_panel_seach_datagrid.Text;

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
                                found = true;
                                break;
                            }
                        }
                    }
                    if (!found)
                    {
                        MessageBox.Show($"Радиостанция {searchValue} не найдена!");
                        panel_seach_datagrid.Enabled = true;
                        panel_seach_datagrid.Visible = true;
                        this.ActiveControl = txB_seach_panel_seach_datagrid;
                    }
                    else
                    {
                        txB_seach_panel_seach_datagrid.Text = "";
                        panel_seach_datagrid.Enabled = false;
                        panel_seach_datagrid.Visible = false;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка поиска по DataGrid (Seach_datagrid)");
                }
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
            toolTip1.SetToolTip(cmB_city, $"Выберите названиe города");
        }
        void Button_seach_BD_city_Click_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(btn_seach_BD_city, $"Выполнить");
        }

        void Button_add_city_click_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(btn_add_city, $"Добавить в реестр\nназвание города");
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
            toolTip1.SetToolTip(cmB_seach, $"Поиск по:");
        }

        void Button_search_click_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(btn_search, $"Выполнить");
        }

        void PictureBox2_update_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(picB_update, $"Обновить БД");
        }

        void PictureBox1_clear_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(picB_clear, $"Очистить Control-ы");
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

        void Button_information_remont_company_regedit_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(btn_information_remont_company_regedit, $"Запись данных ПП в реестр");
        }

        void PictureBox_seach_datadrid_replay_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(picB_seach_datadrid_replay, $"Отбразить отсутствующие РСТ исходя из выполнения предыдущего года");
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

                lbL_cell_rows.Text = rowIndexes.Count.ToString();
                lbL_sum_TO_selection.Text = sum.ToString();
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

        }

        #region добавление из файла

        async void Loading_file_current_BD_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET() == true)
            {
                btn_clear_BD_current_year.Enabled = false;
                btn_manual_backup_current_DB.Enabled = false;
                btn_loading_json_file_BD.Enabled = false;
                btn_Copying_current_BD_end_of_the_year.Enabled = false;
                btn_Loading_file_last_year.Enabled = false;
                btn_loading_file_full_BD.Enabled = false;
                btn_loading_file_current_DB.Enabled = false;
                btn_Uploading_JSON_file.Enabled = false;
                btn_Show_DB_radiostantion_last_year.Enabled = false;
                btn_Show_DB_radiostantion_full.Enabled = false;
                await Task.Run(() => Loading_file_current_BD());
                btn_clear_BD_current_year.Enabled = true;
                btn_manual_backup_current_DB.Enabled = true;
                btn_loading_json_file_BD.Enabled = true;
                btn_Copying_current_BD_end_of_the_year.Enabled = true;
                btn_Loading_file_last_year.Enabled = true;
                btn_loading_file_full_BD.Enabled = true;
                btn_loading_file_current_DB.Enabled = true;
                btn_Uploading_JSON_file.Enabled = true;
                btn_Show_DB_radiostantion_last_year.Enabled = true;
                btn_Show_DB_radiostantion_full.Enabled = true;
            }
        }
        void Loading_file_current_BD()
        {
            if (Internet_check.CheackSkyNET())
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

                        if (Internet_check.CheackSkyNET() == true)
                        {
                            using (var connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_db_2;password=war74_89;database=u1748936_root;charset=utf8"))
                            {
                                if (Internet_check.CheackSkyNET() == true)
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
            btn_clear_BD_current_year.Enabled = false;
            btn_manual_backup_current_DB.Enabled = false;
            btn_loading_json_file_BD.Enabled = false;
            btn_Copying_current_BD_end_of_the_year.Enabled = false;
            btn_Loading_file_last_year.Enabled = false;
            btn_loading_file_full_BD.Enabled = false;
            btn_loading_file_current_DB.Enabled = false;
            btn_Uploading_JSON_file.Enabled = false;
            btn_Show_DB_radiostantion_last_year.Enabled = false;
            btn_Show_DB_radiostantion_full.Enabled = false;
            await Task.Run(() => Loading_file_last_year());
            btn_clear_BD_current_year.Enabled = true;
            btn_manual_backup_current_DB.Enabled = true;
            btn_loading_json_file_BD.Enabled = true;
            btn_Copying_current_BD_end_of_the_year.Enabled = true;
            btn_Loading_file_last_year.Enabled = true;
            btn_loading_file_full_BD.Enabled = true;
            btn_loading_file_current_DB.Enabled = true;
            btn_Uploading_JSON_file.Enabled = true;
            btn_Show_DB_radiostantion_last_year.Enabled = true;
            btn_Show_DB_radiostantion_full.Enabled = true;
        }
        void Loading_file_last_year()
        {
            if (Internet_check.CheackSkyNET())
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

                        if (Internet_check.CheackSkyNET() == true)
                        {
                            using (var connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_db_2;password=war74_89;database=u1748936_root;charset=utf8"))
                            {
                                if (Internet_check.CheackSkyNET() == true)
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
            btn_clear_BD_current_year.Enabled = false;
            btn_manual_backup_current_DB.Enabled = false;
            btn_loading_json_file_BD.Enabled = false;
            btn_Copying_current_BD_end_of_the_year.Enabled = false;
            btn_Loading_file_last_year.Enabled = false;
            btn_loading_file_full_BD.Enabled = false;
            btn_loading_file_current_DB.Enabled = false;
            btn_Uploading_JSON_file.Enabled = false;
            btn_Show_DB_radiostantion_last_year.Enabled = false;
            btn_Show_DB_radiostantion_full.Enabled = false;
            await Task.Run(() => Loading_file_full_BD_method());
            btn_clear_BD_current_year.Enabled = true;
            btn_manual_backup_current_DB.Enabled = true;
            btn_loading_json_file_BD.Enabled = true;
            btn_Copying_current_BD_end_of_the_year.Enabled = true;
            btn_Loading_file_last_year.Enabled = true;
            btn_loading_file_full_BD.Enabled = true;
            btn_loading_file_current_DB.Enabled = true;
            btn_Uploading_JSON_file.Enabled = true;
            btn_Show_DB_radiostantion_last_year.Enabled = true;
            btn_Show_DB_radiostantion_full.Enabled = true;
        }

        void Loading_file_full_BD_method()
        {
            if (Internet_check.CheackSkyNET())
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
                            if (Internet_check.CheackSkyNET())
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
            if (Internet_check.CheackSkyNET())
            {
                string Mesage;
                Mesage = "Вы выгрузили резервный файл json?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                btn_clear_BD_current_year.Enabled = false;
                btn_manual_backup_current_DB.Enabled = false;
                btn_loading_json_file_BD.Enabled = false;
                btn_Copying_current_BD_end_of_the_year.Enabled = false;
                btn_Loading_file_last_year.Enabled = false;
                btn_loading_file_full_BD.Enabled = false;
                btn_loading_file_current_DB.Enabled = false;
                btn_Uploading_JSON_file.Enabled = false;
                btn_Show_DB_radiostantion_last_year.Enabled = false;
                btn_Show_DB_radiostantion_full.Enabled = false;
                await Task.Run(() => FunctionPanel.Loading_json_file_BD(dataGridView2, taskCity));
                btn_clear_BD_current_year.Enabled = true;
                btn_manual_backup_current_DB.Enabled = true;
                btn_loading_json_file_BD.Enabled = true;
                btn_Copying_current_BD_end_of_the_year.Enabled = true;
                btn_Loading_file_last_year.Enabled = true;
                btn_loading_file_full_BD.Enabled = true;
                btn_loading_file_current_DB.Enabled = true;
                btn_Uploading_JSON_file.Enabled = true;
                btn_Show_DB_radiostantion_last_year.Enabled = true;
                btn_Show_DB_radiostantion_full.Enabled = true;
            }
        }
        #endregion

        #region выгрузка всех данных из datagrid

        async void Button_Uploading_JSON_file_Click(object sender, EventArgs e)
        {
            btn_clear_BD_current_year.Enabled = false;
            btn_manual_backup_current_DB.Enabled = false;
            btn_loading_json_file_BD.Enabled = false;
            btn_Copying_current_BD_end_of_the_year.Enabled = false;
            btn_Loading_file_last_year.Enabled = false;
            btn_loading_file_full_BD.Enabled = false;
            btn_loading_file_current_DB.Enabled = false;
            btn_Uploading_JSON_file.Enabled = false;
            btn_Show_DB_radiostantion_last_year.Enabled = false;
            btn_Show_DB_radiostantion_full.Enabled = false;
            await Task.Run(() => FunctionPanel.Get_date_save_datagridview_json(dataGridView1, taskCity));
            btn_clear_BD_current_year.Enabled = true;
            btn_manual_backup_current_DB.Enabled = true;
            btn_loading_json_file_BD.Enabled = true;
            btn_Copying_current_BD_end_of_the_year.Enabled = true;
            btn_Loading_file_last_year.Enabled = true;
            btn_loading_file_full_BD.Enabled = true;
            btn_loading_file_current_DB.Enabled = true;
            btn_Uploading_JSON_file.Enabled = true;
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
                btn_clear_BD_current_year.Enabled = false;
                btn_manual_backup_current_DB.Enabled = false;
                btn_loading_json_file_BD.Enabled = false;
                btn_Copying_current_BD_end_of_the_year.Enabled = false;
                btn_Loading_file_last_year.Enabled = false;
                btn_loading_file_full_BD.Enabled = false;
                btn_loading_file_current_DB.Enabled = false;
                btn_Uploading_JSON_file.Enabled = false;
                btn_Show_DB_radiostantion_last_year.Enabled = false;
                btn_Show_DB_radiostantion_full.Enabled = false;
                FunctionPanel.Copying_current_BD_end_of_the_year();
                btn_clear_BD_current_year.Enabled = true;
                btn_manual_backup_current_DB.Enabled = true;
                btn_loading_json_file_BD.Enabled = true;
                btn_Copying_current_BD_end_of_the_year.Enabled = true;
                btn_Loading_file_last_year.Enabled = true;
                btn_loading_file_full_BD.Enabled = true;
                btn_loading_file_current_DB.Enabled = true;
                btn_Uploading_JSON_file.Enabled = true;
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
                btn_clear_BD_current_year.Enabled = false;
                btn_manual_backup_current_DB.Enabled = false;
                btn_loading_json_file_BD.Enabled = false;
                btn_Copying_current_BD_end_of_the_year.Enabled = false;
                btn_Loading_file_last_year.Enabled = false;
                btn_loading_file_full_BD.Enabled = false;
                btn_loading_file_current_DB.Enabled = false;
                btn_Uploading_JSON_file.Enabled = false;
                btn_Show_DB_radiostantion_last_year.Enabled = false;
                btn_Show_DB_radiostantion_full.Enabled = false;
                FunctionPanel.Manual_backup_current_DB();
                btn_clear_BD_current_year.Enabled = true;
                btn_manual_backup_current_DB.Enabled = true;
                btn_loading_json_file_BD.Enabled = true;
                btn_Copying_current_BD_end_of_the_year.Enabled = true;
                btn_Loading_file_last_year.Enabled = true;
                btn_loading_file_full_BD.Enabled = true;
                btn_loading_file_current_DB.Enabled = true;
                btn_Uploading_JSON_file.Enabled = true;
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
                btn_clear_BD_current_year.Enabled = false;
                btn_manual_backup_current_DB.Enabled = false;
                btn_loading_json_file_BD.Enabled = false;
                btn_Copying_current_BD_end_of_the_year.Enabled = false;
                btn_Loading_file_last_year.Enabled = false;
                btn_loading_file_full_BD.Enabled = false;
                btn_loading_file_current_DB.Enabled = false;
                btn_Uploading_JSON_file.Enabled = false;
                btn_Show_DB_radiostantion_last_year.Enabled = false;
                btn_Show_DB_radiostantion_full.Enabled = false;
                FunctionPanel.Clear_BD_current_year();
                QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text);
                btn_clear_BD_current_year.Enabled = true;
                btn_manual_backup_current_DB.Enabled = true;
                btn_loading_json_file_BD.Enabled = true;
                btn_Copying_current_BD_end_of_the_year.Enabled = true;
                btn_Loading_file_last_year.Enabled = true;
                btn_loading_file_full_BD.Enabled = true;
                btn_loading_file_current_DB.Enabled = true;
                btn_Uploading_JSON_file.Enabled = true;
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
                btn_clear_BD_current_year.Enabled = false;
                btn_manual_backup_current_DB.Enabled = false;
                btn_loading_json_file_BD.Enabled = false;
                btn_Copying_current_BD_end_of_the_year.Enabled = false;
                btn_Loading_file_last_year.Enabled = false;
                btn_loading_file_full_BD.Enabled = false;
                btn_loading_file_current_DB.Enabled = false;
                btn_Uploading_JSON_file.Enabled = false;
                btn_Show_DB_radiostantion_last_year.Enabled = false;
                btn_Show_DB_radiostantion_full.Enabled = false;
                Close_Functional_loading_panel_Click(sender, e);
                panel1.Enabled = false;
                panel3.Enabled = false;
                FunctionPanel.Show_DB_radiostantion_last_year(dataGridView1, taskCity);
                btn_clear_BD_current_year.Enabled = true;
                btn_manual_backup_current_DB.Enabled = true;
                btn_loading_json_file_BD.Enabled = true;
                btn_Copying_current_BD_end_of_the_year.Enabled = true;
                btn_Loading_file_last_year.Enabled = true;
                btn_loading_file_full_BD.Enabled = true;
                btn_loading_file_current_DB.Enabled = true;
                btn_Uploading_JSON_file.Enabled = true;
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
                btn_clear_BD_current_year.Enabled = false;
                btn_manual_backup_current_DB.Enabled = false;
                btn_loading_json_file_BD.Enabled = false;
                btn_Copying_current_BD_end_of_the_year.Enabled = false;
                btn_Loading_file_last_year.Enabled = false;
                btn_loading_file_full_BD.Enabled = false;
                btn_loading_file_current_DB.Enabled = false;
                btn_Uploading_JSON_file.Enabled = false;
                btn_Show_DB_radiostantion_last_year.Enabled = false;
                btn_Show_DB_radiostantion_full.Enabled = false;
                Close_Functional_loading_panel_Click(sender, e);
                panel1.Enabled = false;
                panel3.Enabled = false;
                FunctionPanel.Show_DB_radiostantion_full(dataGridView1, taskCity);
                btn_clear_BD_current_year.Enabled = true;
                btn_manual_backup_current_DB.Enabled = true;
                btn_loading_json_file_BD.Enabled = true;
                btn_Copying_current_BD_end_of_the_year.Enabled = true;
                btn_Loading_file_last_year.Enabled = true;
                btn_loading_file_full_BD.Enabled = true;
                btn_loading_file_current_DB.Enabled = true;
                btn_Uploading_JSON_file.Enabled = true;
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
            System.Environment.Exit(0);
        }

        private void ST_WorkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = FormClose.GetInstance.FClose();
        }
        #endregion

        #region подсветка акта цветом и его подсчёт

        void DataGridView1_Sign(object sender, EventArgs e)
        {
            if (txB_numberAct.Text != "")
            {
                int c = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["numberAct"].Value.ToString().Equals(txB_numberAct.Text))
                    {
                        dataGridView1.Rows[i].Cells["numberAct"].Style.BackColor = Color.Red;
                        c++;
                    }
                }

                label_Sing.Visible = true;
                lbl_Sign.Visible = true;
                lbl_Sign.Text += $"{txB_numberAct.Text} - {txB_company.Text} - {c} шт.,";
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
            if (txB_numberAct.Text != "")
            {
                int c = 0;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["numberAct"].Value.ToString().Equals(txB_numberAct.Text))
                    {
                        dataGridView1.Rows[i].Cells["numberAct"].Style.BackColor = Color.Red;
                        c++;
                    }
                }

                label_complete.Visible = true;
                lbl_full_complete_act.Visible = true;
                lbl_full_complete_act.Text += $"{txB_numberAct.Text} - {txB_company.Text} - {c} шт.,";
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
            if (txB_serialNumber.Text != "")
            {
                if (!String.IsNullOrEmpty(txB_decommissionSerialNumber.Text))
                {
                    MessageBox.Show($"На радиостанцию номер: {txB_serialNumber.Text} от предприятия {txB_company.Text}, уже есть списание!");
                    return;
                }
                string Mesage;
                Mesage = $"Вы действительно хотите списать радиостанцию? Номер: {txB_serialNumber.Text} от предприятия {txB_company.Text}";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                QuerySettingDataBase.LoadingLastDecommissionSerialNumber(lbL_last_decommission, cmB_city.Text);
                panel1.Enabled = false;
                panel2.Enabled = false;
                panel3.Enabled = false;
                dataGridView1.Enabled = false;
                panel_decommissionSerialNumber.Visible = true;
                panel_decommissionSerialNumber.Enabled = true;
                txB1_decommissionSerialNumber.Text = txB_numberAct.Text + "C";
                if (cmB_model.Text == "Comrade R5")
                {
                    txB_reason_decommission.Text = "Выходная мощность несущей передатчика: номинальная – 5 Вт, максимальная – 9 Вт, что не соответствует нормам ГОСТ 12252 – 86г, для радиостанций третьего типа и техническим параметрам изготовителя, указанных в паспорте.";
                }
                else
                {
                    txB_reason_decommission.Text = "Коррозия основной печатной платы с многочисленными обрывами проводников, вызванная попаданием влаги внутрь радиостанции. Восстановлению не подлежит.";
                }

            }
        }


        void Btn_record_decommissionSerialNumber_Click(object sender, EventArgs e)
        {
            try
            {
                if (txB1_decommissionSerialNumber.Text != "" && txB_reason_decommission.Text != "")
                {

                    if (!Regex.IsMatch(txB1_decommissionSerialNumber.Text, @"[0-9]{2,2}/([0-9]+([A-Z]?[А-Я]?)*[.\-]?[0-9]?[0-9]?[0-9]?[A-Z]?[А-Я]?)$"))
                    {
                        MessageBox.Show("Введите корректно \"№ Акта списания\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB1_decommissionSerialNumber.Select();
                        return;
                    }

                    if (!Regex.IsMatch(txB_reason_decommission.Text, @"[А-Яа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
                    {
                        MessageBox.Show("Введите корректно поле \"Причина\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_reason_decommission.Select();
                        string Mesage = "Вы действительно хотите продолжить?";

                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }


                    var re = new Regex(Environment.NewLine);
                    txB_reason_decommission.Text = re.Replace(txB_reason_decommission.Text, " ");//удаление новой строки
                    txB_reason_decommission.Text.Trim();
                    txB1_decommissionSerialNumber.Text.Trim();

                    QuerySettingDataBase.Record_decommissionSerialNumber(txB_serialNumber.Text, txB1_decommissionSerialNumber.Text,
                        txB_city.Text, cmB_poligon.Text, txB_company.Text, txB_location.Text, cmB_model.Text, txB_dateTO.Text,
                        txB_price.Text, txB_representative.Text, txB_post.Text, txB_numberIdentification.Text, txB_dateIssue.Text,
                        txB_phoneNumber.Text, txB_antenna.Text, txB_manipulator.Text, txB_AKB.Text, txB_batteryСharger.Text,
                        txB_reason_decommission.Text);

                    Button_update_Click(sender, e);
                    panel_decommissionSerialNumber.Visible = false;
                    panel_decommissionSerialNumber.Enabled = false;
                    panel1.Enabled = true;
                    panel2.Enabled = true;
                    panel3.Enabled = true;
                    dataGridView1.Enabled = true;
                    txB1_decommissionSerialNumber.Text = "";
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
                Mesage = $"Вы действительно хотите удалить списание на данную радиостанцию: {txB_serialNumber.Text}, предприятия: {txB_company.Text}?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                QuerySettingDataBase.Delete_decommissionSerialNumber_radiostantion(dataGridView2, txB_decommissionSerialNumber.Text, txB_serialNumber.Text, txB_city.Text, cmB_model, txB_numberAct);
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
                QuerySettingDataBase.Show_radiostantion_decommission(dataGridView1, txB_city.Text);
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
                    string decommissionSerialNumber_company = $"{txB_decommissionSerialNumber.Text}-{txB_company.Text}";
                    DateTime dateTime = DateTime.Today;
                    string dateDecommission = dateTime.ToString("dd.MM.yyyy");
                    string city = txB_city.Text;
                    string comment = txB_comment.Text;

                    var items = new Dictionary<string, string>
                {
                    {"<numberActTZPP>", decommissionSerialNumber_company },
                    {"<model>", cmB_model.Text },
                    {"<serialNumber>", txB_serialNumber.Text },
                    {"<company>", txB_company.Text },
                    {"<dateDecommission>", dateDecommission },
                    {"<comment>", comment}
                };

                    PrintDocWord.GetInstance.ProcessPrintWordDecommission(items, decommissionSerialNumber_company, dateDecommission, city, comment);
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
                if (cmB_seach.SelectedIndex == 0)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        QuerySettingDataBase.Number_unique_company(cmB_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Предприятия не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (cmB_seach.SelectedIndex == 1)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        QuerySettingDataBase.Number_unique_location(cmB_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Станции не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (cmB_seach.SelectedIndex == 3)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        QuerySettingDataBase.Number_unique_dateTO(cmB_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Даты проверки ТО не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (cmB_seach.SelectedIndex == 4)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        QuerySettingDataBase.Number_unique_numberAct(cmB_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Акты ТО не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (cmB_seach.SelectedIndex == 5)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        QuerySettingDataBase.Number_unique_numberActRemont(cmB_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Акты Ремонта не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (cmB_seach.SelectedIndex == 6)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        QuerySettingDataBase.Number_unique_representative(cmB_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Представители предприятий не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (cmB_seach.SelectedIndex == 7)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        QuerySettingDataBase.Number_unique_decommissionActs(cmB_city.Text, cmb_number_unique_acts);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка! Акты списаний не добавлены в comboBox!");
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (cmB_seach.SelectedIndex == 8)
                {
                    try
                    {
                        cmb_number_unique_acts.Visible = true;
                        textBox_search.Visible = false;

                        QuerySettingDataBase.Number_unique_model(cmB_city.Text, cmb_number_unique_acts);
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

        #region показать РСТ без списаний по участку

        void Btn_RefreshDataGridWithoutDecommission(object sender, EventArgs e)
        {
            QuerySettingDataBase.RefreshDataGridWithoutDecommission(dataGridView1, cmB_city.Text);
            Counters();
        }


        #endregion

        #region показать списанные РСТ по участку

        void Btn_RefreshDataGridtDecommissionByPlot(object sender, EventArgs e)
        {
            QuerySettingDataBase.RefreshDataGridtDecommissionByPlot(dataGridView1, cmB_city.Text);
            Counters();
        }


        #endregion

        #region Бирка

        void FormTag(object sender, EventArgs e)
        {
            try
            {
                panel_Tag.Visible = true;
                panel_Tag.Enabled = true;
                txB_Date_panel_Tag.Select();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка формирования бирок (FormTag)");
            }
        }

        void TxB_Date_panel_Tag_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }

        void MonthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            txB_Date_panel_Tag.Text = e.End.ToString("dd.MM.yyyy");
            monthCalendar1.Visible = false;
        }

        void Btn_close_panel_Tag_Click(object sender, EventArgs e)
        {
            panel_Tag.Visible = false;
        }

        void Btn_FormTag_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txB_Date_panel_Tag.Text))
            {
                DateTime dateTime = Convert.ToDateTime(txB_Date_panel_Tag.Text);

                string day = dateTime.ToString("dd");
                string month = dateTime.ToString("MM");
                string year = dateTime.ToString("yyyy");
                string day2 = dateTime.AddDays(1).ToString("dd");
                string year2 = dateTime.AddYears(1).ToString("yyyy");

                var items2 = new Dictionary<string, string>
                {
                    {"day", day },
                    {"month", month },
                    {"year", year },
                    {"day2", day2 },
                    {"year2", year2 }
                };

                PrintDocExcel.GetInstance.ProcessPrintWordTag(items2, txB_Date_panel_Tag.Text);
            }

            else MessageBox.Show("Заполни дату!");

        }


        #endregion

        #region панель для выбора печати базы
        void PnL_printBaseClose_Click(object sender, EventArgs e)
        {
            pnL_printBase.Visible = false;
        }
        void Btn_SaveDirectorateBase_Click(object sender, EventArgs e)
        {
            try
            {
                pnL_printBase.Visible = false;
                SaveFileDataGridViewPC.DirectorateSaveFilePC(dataGridView1, cmB_city.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка сохранения БД для Дирекции связи.");
            }
        }
        void Btn_SaveFullBase_Click(object sender, EventArgs e)
        {
            try
            {
                pnL_printBase.Visible = false;
                SaveFileDataGridViewPC.SaveFullBasePC(dataGridView1, cmB_city.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка сохранения всей БД.");
            }
        }
        #endregion
    }
}


