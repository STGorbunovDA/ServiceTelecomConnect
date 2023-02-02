using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WinForms = System.Windows.Forms;


namespace ServiceTelecomConnect
{
    public partial class ComparisonForm : Form
    {
        #region global perem

        private delegate DialogResult ShowOpenFileDialogInvoker();

        int selectedRow;

        private readonly CheakUser _user;

        #endregion
        public ComparisonForm(CheakUser user)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            cmB_seach.Items.Clear();
            cmB_seach.Items.Add("Предприятие");
            cmB_seach.Items.Add("Станция");
            cmB_seach.Items.Add("Заводской номер");
            cmB_seach.Items.Add("Дата ТО");
            cmB_seach.Items.Add("Номер акта ТО");
            cmB_seach.Items.Add("Номер акта Ремонта");
            cmB_seach.Items.Add("Номер Акта списания");
            cmB_seach.Items.Add("Месяц");
            cmB_seach.Items.Add("Модель");
            cmB_seach.Text = cmB_seach.Items[2].ToString();
            dataGridView1.DoubleBuffered(true);
            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            _user = user;
            IsAdmin();
        }
        void IsAdmin()
        {
            if (_user.IsAdmin == "Куратор" || _user.IsAdmin == "Руководитель")
                mTrip_funcionalpanel.Visible = false;
            else if (_user.IsAdmin == "Admin")
            {

            }
            else
            {
                panel1.Enabled = false;
                dataGridView1.Enabled = false;
                panel3.Enabled = false;
            }
        }
        private void ComparisonForm_Load(object sender, EventArgs e)
        {
            QuerySettingDataBase.GettingTeamData(lbL_FIO_chief, lbL_FIO_Engineer, lbL_doverennost, lbL_road, lbL_numberPrintDocument, _user, cmB_road);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12f, FontStyle.Bold); //жирный курсив размера 16
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; //цвет текста
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; //цвет ячейки

            QuerySettingDataBase.SelectCityGropByCurator(cmB_city, cmB_road);
            QuerySettingDataBase.SelectCityGropByMonthRoad(cmB_road, cmB_month);
            QuerySettingDataBase.CreateColumsСurator(dataGridView1);
            QuerySettingDataBase.CreateColumsСurator(dataGridView2);
            RegistryKey reg1 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Куратор\\");
            if (reg1 != null)
            {
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Куратор\\");
                cmB_road.Text = helloKey.GetValue("Дорога").ToString();

                helloKey.Close();
            }
            this.dataGridView1.Sort(this.dataGridView1.Columns["dateTO"], ListSortDirection.Ascending);
            dataGridView1.Columns["dateTO"].ValueType = typeof(DateTime);
            dataGridView1.Columns["dateTO"].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns["dateTO"].ValueType = System.Type.GetType("System.Date");
            QuerySettingDataBase.RefreshDataGridСurator(dataGridView1, cmB_road.Text);
            Counters();
            ///Таймер
            WinForms::Timer timer = new WinForms::Timer();
            timer.Interval = (30 * 60 * 1000); // 15 mins
            timer.Tick += new EventHandler(TimerEventProcessorCurator);
            timer.Start();

            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;

            if (dataGridView1.Rows.Count != 0)
                cmB_month.Text = cmB_month.Items[0].ToString();
            else MessageBox.Show("Добавь радиостанцию в выполнение!");
        }
        void TimerEventProcessorCurator(Object myObject, EventArgs myEventArgs)
        {
            string taskCity = cmB_city.Text;
            string road = cmB_road.Text;
            QuerySettingDataBase.RefreshDataGridСuratorTimerEventProcessor(dataGridView2, taskCity, road);
            new Thread(() => { FunctionalPanel.GetSaveDataGridViewInJsonCurator(dataGridView2, taskCity); }) { IsBackground = true }.Start();
            new Thread(() => { SaveFileDataGridViewPC.AutoSaveFileCurator(dataGridView2, road); }) { IsBackground = true }.Start();
            new Thread(() => { QuerySettingDataBase.CopyDataBaseRadiostantionComparisonInRadiostantionComparisonCopy(); }) { IsBackground = true }.Start();
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
                    colRemont++;
                sumTO += Convert.ToDecimal(dataGridView1.Rows[i].Cells["price"].Value);
                sumRemont += Convert.ToDecimal(dataGridView1.Rows[i].Cells["priceRemont"].Value);
            }

            lbL_count.Text = dataGridView1.Rows.Count.ToString();
            lbL_summ.Text = sumTO.ToString();
            lbL_countRemont.Text = colRemont.ToString();
            lbL_summRemont.Text = sumRemont.ToString();
        }
        #endregion

        #region Сохранение поля город проведения проверки
        void BtnAddCityClick(object sender, EventArgs e)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.CreateSubKey("SOFTWARE\\ServiceTelekom_Setting\\Куратор");
            helloKey.SetValue("Дорога", $"{cmB_road.Text}");
            helloKey.Close();
        }
        #endregion

        #region загрузка городов в cmB_road
        void CmbRoadSelectionChangeCommitted(object sender, EventArgs e)
        {
            QuerySettingDataBase.RefreshDataGridСurator(dataGridView1, cmB_road.Text);
            QuerySettingDataBase.SelectCityGropByCurator(cmB_city, cmB_road);
            QuerySettingDataBase.SelectCityGropByMonthRoad(cmB_road, cmB_month);
            Counters();
        }
        #endregion

        #region загрузка базы согласно месяцам по дороге
        void CmbMonthSelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Добавь радиостанцию в выполнение!");
                return;
            }
            QuerySettingDataBase.RefreshDataGridСuratorMonth(dataGridView1, cmB_road.Text, cmB_month.Text);
            Counters();
        }
        #endregion

        #region загрузка базы согласно городу
        void CmbCitySelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Добавь радиостанцию в выполнение!");
                return;
            }
            QuerySettingDataBase.RefreshDataGridСuratorCity(dataGridView1, cmB_city.Text, cmB_road.Text);
            QuerySettingDataBase.SelectCityGropByMonthCity(cmB_city, cmB_road, cmB_month);

            Counters();
        }
        #endregion

        #region получение данных в Control-ы, button right mouse
        void DataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
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
        #endregion

        #region Clear contorl-ы
        void ClearControlForm(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите очистить все введенные вами поля?";
            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            foreach (Control control in panel1.Controls)
                if (control is TextBox)
                    control.Text = "";
            foreach (Control control in panel2.Controls)
                if (control is TextBox)
                    control.Text = "";
        }
        #endregion

        #region Удаление из БД
        void BtnDeleteClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 1)
            {
                string Mesage = $"Вы действительно хотите удалить радиостанции у предприятия: {txB_company.Text}?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            else
            {
                string Mesage = $"Вы действительно хотите удалить радиостанцию: {txB_serialNumber.Text}, предприятия: {txB_company.Text}?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }

            QuerySettingDataBase.DeleteRowСellCurator(dataGridView1);
            int currRowIndex = dataGridView1.CurrentCell.RowIndex;
            QuerySettingDataBase.RefreshDataGridСurator(dataGridView1, cmB_road.Text);
            txB_numberAct.Text = "";
            dataGridView1.ClearSelection();
            if (dataGridView1.RowCount - currRowIndex > 0)
                dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
            Counters();
        }
        #endregion

        #region обновление БД
        void BtnUpdateClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int currRowIndex = dataGridView1.CurrentCell.RowIndex;
                int index = dataGridView1.CurrentRow.Index;
                QuerySettingDataBase.RefreshDataGridСurator(dataGridView1, cmB_road.Text);
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
                QuerySettingDataBase.RefreshDataGridСurator(dataGridView1, cmB_road.Text);
                Counters();
            }
        }
        void UpdateClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Добавь радиостанцию в выполнение!");
                return;
            }
            BtnUpdateClick(sender, e);
        }
        #endregion

        #region ProcessKbdCtrlShortcuts
        void ProcessKbdCtrlShortcuts(object sender, KeyEventArgs e)
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
        #endregion

        #region Сохранение БД на PC
        void BtnSaveInFileCuratorClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Добавь радиостанцию в выполнение!");
                return;
            }
            SaveFileDataGridViewPC.UserSaveFileCuratorPC(dataGridView1, cmB_road.Text);
        }
        #endregion

        #region показать кол-во уникальных записей БД в Combobox
        void ComboBox_seach_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Добавь радиостанцию в выполнение!");
                return;
            }
            if (cmB_seach.SelectedIndex == 0)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                QuerySettingDataBase.NumberUniqueCompanyCurator(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 1)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                QuerySettingDataBase.NumberUniqueLocationCurator(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 3)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                QuerySettingDataBase.NumberUniqueDateTOCurator(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 4)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                QuerySettingDataBase.NumberUniqueNumberActCurator(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 5)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                QuerySettingDataBase.NumberUniqueNumberActRemontCurator(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 6)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                QuerySettingDataBase.NumberUniqueDecommissionActsCurator(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 7)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                QuerySettingDataBase.NumberUniqueMonthCurator(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 8)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                QuerySettingDataBase.NumberUniqueModelCurator(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else
            {
                cmb_number_unique_acts.Visible = false;
                textBox_search.Visible = true;
            }
            cmb_number_unique_acts.SelectedIndex = 0;
        }
        #endregion

        #region Взаимодействие на search, cформировать на форме panel1
        void CmbNumberUniqueActsSelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Добавь радиостанцию в выполнение!");
                return;
            }
            QuerySettingDataBase.SearchCurator(dataGridView1, cmB_seach.Text, cmB_city.Text, textBox_search.Text, cmb_number_unique_acts.Text, cmB_road.Text);
            Counters();
        }
        void TxbSearchKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                QuerySettingDataBase.SearchCurator(dataGridView1, cmB_seach.Text, cmB_city.Text, textBox_search.Text, cmb_number_unique_acts.Text, cmB_road.Text);
                Counters();
            }
        }
        void BtnSearchClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Добавь радиостанцию в выполнение!");
                return;
            }
            QuerySettingDataBase.SearchCurator(dataGridView1, cmB_seach.Text, cmB_city.Text, textBox_search.Text, cmb_number_unique_acts.Text, cmB_road.Text);
            Counters();
        }
        void TxbNumberActKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Добавь радиостанцию в выполнение!");
                    return;
                }
                QuerySettingDataBase.UpdateDataGridViewNumberActCurator(dataGridView1, cmB_city.Text, txB_numberAct.Text);
                Counters();
            }
        }
        void TxbNumberActMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Добавь радиостанцию в выполнение!");
                return;
            }
            if (!String.IsNullOrEmpty(txB_numberAct.Text))
            {
                QuerySettingDataBase.UpdateDataGridViewNumberActCurator(dataGridView1, cmB_city.Text, txB_numberAct.Text);
                Counters();
            }
        }
        void DataGridView1CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
                e.Cancel = true;
        }
        #endregion

        #region dataGridView1.Update() для добавления или удаление строки
        void DataGridView1UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            dataGridView1.Update();
        }
        void DataGridView1UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            dataGridView1.Update();
        }
        void DataGridView1CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Update();
        }
        #endregion

        #region отк. формы изменения РСТ
        void Сhange_rst_form_curator_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Добавь радиостанцию в выполнение!");
                    return;
                }
                if (!String.IsNullOrEmpty(txB_serialNumber.Text))
                {
                    СhangeRSTFormCurator сhangeRSTFormCurator = new СhangeRSTFormCurator();
                    if (Application.OpenForms["СhangeRSTFormCurator"] == null)
                    {
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
                        сhangeRSTFormCurator.lbL_road.Text = cmB_road.Text;
                        сhangeRSTFormCurator.Show();
                    }
                }
            }
        }

        #endregion

        #region изменения РСТ в выполнение по плану

        void AddExecutionCurator(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 1)
            {
                string Mesage = $"Вы действительно хотите добавить радиостанции в выполнение: {txB_company.Text}?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            else
            {
                string Mesage = $"Вы действительно хотите добавить радиостанцию в выполнение: {txB_serialNumber.Text}, предприятия: {txB_company.Text}?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            ContextMenu m = new ContextMenu();
            m.MenuItems.Add(new MenuItem("Январь", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Январь", cmB_road, cmB_month)));
            m.MenuItems.Add(new MenuItem("Февраль", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Февраль", cmB_road, cmB_month)));
            m.MenuItems.Add(new MenuItem("Март", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Март", cmB_road, cmB_month)));
            m.MenuItems.Add(new MenuItem("Апрель", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Апрель", cmB_road, cmB_month)));
            m.MenuItems.Add(new MenuItem("Май", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Май", cmB_road, cmB_month)));
            m.MenuItems.Add(new MenuItem("Июнь", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Июнь", cmB_road, cmB_month)));
            m.MenuItems.Add(new MenuItem("Июль", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Июль", cmB_road, cmB_month)));
            m.MenuItems.Add(new MenuItem("Август", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Август", cmB_road, cmB_month)));
            m.MenuItems.Add(new MenuItem("Сентябрь", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Сентябрь", cmB_road, cmB_month)));
            m.MenuItems.Add(new MenuItem("Октябрь", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Октябрь", cmB_road, cmB_month)));
            m.MenuItems.Add(new MenuItem("Ноябрь", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Ноябрь", cmB_road, cmB_month)));
            m.MenuItems.Add(new MenuItem("Декабрь", (s, ee) => AddExecutionСurator.AddExecutionRowСellCurator(dataGridView1, "Декабрь", cmB_road, cmB_month)));
            m.Show(dataGridView1, new Point(dataGridView1.Location.X + 700, dataGridView1.Location.Y));
        }

        #endregion

        #region ContextMenu datagrid
        void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (!String.IsNullOrEmpty(txB_serialNumber.Text))
                {
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("Изменить выполнение РСТ", AddExecutionCurator));
                    m.MenuItems.Add(new MenuItem("Изменить радиостанцию", Сhange_rst_form_curator_Click));
                    m.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClick));
                    m.MenuItems.Add(new MenuItem("Убрать из выполнения", BtnDeleteClick));
                    m.MenuItems.Add(new MenuItem("Сохранение БД", BtnSaveInFileCuratorClick));
                    m.Show(dataGridView1, new Point(e.X, e.Y));
                }
            }
        }
        #endregion

        #region для выбора значения в Control(TXB)

        void Refresh_values_TXB_CMB(int currRowIndex)
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
        #endregion

        #region поиск по dataGrid без запроса к БД и открытие функциональной панели Control + K
        void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            // открывем панель поиска по гриду по зав номеру РСТ
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
            {
                panel_seach_datagrid_curator.Enabled = true;
                panel_seach_datagrid_curator.Visible = true;
                this.ActiveControl = txB_seach_panel_datagrid_curator;
            }
            // открываем функциональную панель
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.K)
                Button_Functional_loading_panel(sender, e);
        }

        void Seach_datagrid_curator()
        {
            if (txB_seach_panel_datagrid_curator.Text != "")
            {
                string searchValue = txB_seach_panel_datagrid_curator.Text;

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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

                txB_seach_panel_datagrid_curator.Text = "";
                panel_seach_datagrid_curator.Enabled = false;
                panel_seach_datagrid_curator.Visible = false;
            }
            else
            {
                string Mesage2 = "Поле поиска не должно быть пустым!";

                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }

        }
        void Button_close_panel_seach_datagrid_Click(object sender, EventArgs e)
        {
            panel_seach_datagrid_curator.Enabled = false;
            panel_seach_datagrid_curator.Visible = false;
        }
        void Button_seach_panel_seach_datagrid_Click(object sender, EventArgs e)
        {
            Seach_datagrid_curator();
        }
        void TextBox_seach_panel_seach_datagrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                Seach_datagrid_curator();
        }

        void TextBox_seach_panel_seach_datagrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            char ch = e.KeyChar;
            if ((ch < 'A' || ch > 'Z') && (ch <= 47 || ch >= 58) && ch != '/' && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TextBox_seach_panel_seach_datagrid_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }


        #endregion

        #region при выборе строк ползьзователем и их подсчёт

        void DataGridView1_SelectionChanged(object sender, EventArgs e)
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

        #endregion

        #region close form

        void ComparisonForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        void ComparisonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = FormClose.GetInstance.FClose(_user.Login);
        }

        #endregion

        #region Функциональная панель
        void Btn_close_Functional_loading_panel_Click(object sender, EventArgs e)
        {
            Functional_loading_panel.Visible = false;
            dataGridView1.Enabled = true;
            panel1.Enabled = true;
            panel3.Enabled = true;
        }

        void Button_Functional_loading_panel(object sender, EventArgs e)
        {
            if (_user.Login == "Admin")
            {
                Functional_loading_panel.Enabled = true;
                Functional_loading_panel.Visible = true;
                dataGridView1.Enabled = false;
                panel1.Enabled = false;
                panel3.Enabled = false;
            }
        }

        #region Очистка текущей БД 
        void Btn_clear_BD_current_year_Curator_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите удалить всё содержимое базы данных?";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            string Mesage2 = "Всё удалится безвозратно!!!Точно хотите удалить всё содержимое Базы данных?";

            if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            FunctionalPanel.ClearCurrentYearCurator();
            QuerySettingDataBase.RefreshDataGridСurator(dataGridView1, cmB_road.Text);
        }

        #endregion

        #region Ручное-резервное копирование текущей БД
        void Btn_manual_backup_current_DB_Click(object sender, EventArgs e)
        {
            FunctionalPanel.ManualBackupCurrentCurator();
        }


        #endregion

        #region Загрузка из файла для текущей БД
        void Btn_loading_file_current_DB_Click(object sender, EventArgs e)
        {
            FunctionalPanel.LoadingFileCurrentDatabaseCurator();
        }

        #region Загрузка и обновление резервного файла JSON
        void Btn_loading_json_file_BD_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                FunctionalPanel.LoadingJsonFileInDatabaseCurator(dataGridView1, cmB_city.Text);
            }
        }



        #endregion

        #endregion

        #region Выгрузка файла JSON
        void Btn_Uploading_JSON_file_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
                FunctionalPanel.GetSaveDataGridViewInJsonCurator(dataGridView1, cmB_city.Text);
        }

        #endregion

        #endregion

        #region  MenuTrip

        void MTrip_mTrip_AddExecutionCurator_Click(object sender, EventArgs e)
        {
            AddExecutionCurator(sender, e);
        }

        void MTrip_change_rst_Click(object sender, EventArgs e)
        {
            Сhange_rst_form_curator_Click(sender, e);
        }

        void MTrip_delete_rst_Click(object sender, EventArgs e)
        {
            BtnDeleteClick(sender, e);
        }

        void MTrip_Button_update_Click(object sender, EventArgs e)
        {
            BtnUpdateClick(sender, e);
        }

        void MTrip_Button_save_in_file_Click(object sender, EventArgs e)
        {
            BtnSaveInFileCuratorClick(sender, e);
        }

        void MTrip_btn_clear_BD_current_year_Click(object sender, EventArgs e)
        {
            Btn_clear_BD_current_year_Curator_Click(sender, e);
        }

        void MTrip_btn_manual_backup_current_DB_Click(object sender, EventArgs e)
        {
            Btn_manual_backup_current_DB_Click(sender, e);
        }

        void MTrip_btn_loading_file_current_DB_Click(object sender, EventArgs e)
        {
            Btn_loading_file_current_DB_Click(sender, e);
        }

        void MTrip_btn_loading_json_file_BD_Click(object sender, EventArgs e)
        {
            Btn_loading_json_file_BD_Click(sender, e);
        }

        void MTrip_btn_Uploading_JSON_file_Click(object sender, EventArgs e)
        {
            Btn_Uploading_JSON_file_Click(sender, e);
        }
        #endregion
    }
}


