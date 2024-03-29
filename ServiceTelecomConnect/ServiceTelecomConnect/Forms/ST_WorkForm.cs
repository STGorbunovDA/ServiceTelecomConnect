﻿using Microsoft.Win32;
using ServiceTelecomConnect.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WinForms = System.Windows.Forms;


namespace ServiceTelecomConnect
{

    #region состояние Rows
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
        int selectedRow;
        private readonly CheakUser _user;
        #endregion

        public ST_WorkForm(CheakUser user)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            dataGridView1.DoubleBuffered(true);
            cmB_seach.Text = cmB_seach.Items[2].ToString();
            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            _user = user;
            IsAdmin();
        }
        void IsAdmin()
        {
            if (_user.IsAdmin == "Дирекция связи")
            {
                panel3.Enabled = false;
                Functional_loading_panel.Enabled = false;
                panel_date.Enabled = false;
                panel_remont_information_company.Enabled = false;

                foreach (Control element in panel1.Controls)
                    element.Enabled = false;

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
                mTrip_funcionalpanel.Visible = false;
                mTrip_show.Visible = false;
                mTrip_FormTag.Visible = false;
                mTrip_Add_Fill_Full_ActTO.Visible = false;
                mTrip_Add_Signature_ActTO.Visible = false;
                mTrip_print.Visible = false;
                mTrip_file.Visible = false;
                txB_serialNumber.Enabled = true;
                button_save_in_file.Enabled = true;
                mTrip_PrintStatementParameters.Visible = false;
                mTrip_PrintReportAKB.Visible = false;
            }
            if (_user.IsAdmin == "Куратор" || _user.IsAdmin == "Руководитель")
                mTrip_funcionalpanel.Visible = false;

            if (_user.IsAdmin == "Начальник участка")
            {
                mTrip_Curator.Visible = false;
                mTrip_funcionalpanel.Visible = false;
            }
            if (_user.IsAdmin == "Инженер")
            {
                button_change_rst_form.Enabled = false;
                mTrip_Curator.Visible = false;
                mTrip_funcionalpanel.Visible = false;
                mTrip_change_rst.Visible = false;
                mTrip_delete.Visible = false;
                mTrip_decommission.Visible = false;
                mTrip_Curator.Visible = false;
                mTrip_Add_Fill_Full_ActTO.Visible = false;
                mTrip_Add_Signature_ActTO.Visible = false;
                mTrip_funcionalpanel.Visible = false;
                panel4.Visible = false;
            }
        }
        void STWorkFormLoad(object sender, EventArgs e)
        {
            QuerySettingDataBase.GettingTeamData(lbl_ChiefFIO, lbl_EngineerFIO, lbL_doverennost, lbL_road, lbL_numberPrintDocument, _user, cmB_road);
            QuerySettingDataBase.CmbGettingModelRST(cmB_model);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12f, FontStyle.Bold); //жирный курсив размера 16
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; //цвет текста
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; //цвет ячейки
            QuerySettingDataBase.SelectCityGropBy(cmB_city, cmB_road);
            QuerySettingDataBase.CreateColums(dataGridView1);
            QuerySettingDataBase.CreateColums(dataGridView2);
            this.dataGridView1.Sort(this.dataGridView1.Columns["dateTO"], ListSortDirection.Ascending);
            dataGridView1.Columns["dateTO"].ValueType = typeof(DateTime);
            dataGridView1.Columns["dateTO"].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns["dateTO"].ValueType = System.Type.GetType("System.Date");
            RegistryKey reg1 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\City");
            if (reg1 != null)
            {
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\City");
                cmB_city.Text = helloKey.GetValue("Город проведения проверки").ToString();

                helloKey.Close();
            }
            QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text, cmB_road.Text);
            Counters();
            /// получение актов который не заполенны из реестра, которые указал пользователь
            RegistryKey reg2 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
            if (reg2 != null)
            {
                string registry = String.Empty;
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                registry = helloKey.GetValue("Акты_незаполненные").ToString();
                string[] split = registry.Split(new Char[] { ';' });

                foreach (string s in split)
                    if (!String.IsNullOrWhiteSpace(s))
                        cmB_AddFillFullActTO.Items.Add(s);
                helloKey.Close();
                cmB_AddSignature.Sorted = true;
                if (cmB_AddFillFullActTO.Items.Count > 0)
                    cmB_AddFillFullActTO.Text = cmB_AddFillFullActTO.Items[cmB_AddFillFullActTO.Items.Count - 1].ToString();
            }
            RegistryKey reg3 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
            if (reg3 != null)
            {
                string registry2 = String.Empty;
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                registry2 = helloKey.GetValue("Акты_на_подпись").ToString();
                string[] split = registry2.Split(new Char[] { ';' });

                foreach (string s in split)
                    if (!String.IsNullOrWhiteSpace(s))
                        cmB_AddSignature.Items.Add(s);

                helloKey.Close();
                cmB_AddSignature.Sorted = true;
                if (cmB_AddSignature.Items.Count > 0)
                    cmB_AddSignature.Text = cmB_AddSignature.Items[cmB_AddSignature.Items.Count - 1].ToString();
            }

            ///Таймер
            WinForms::Timer timer = new WinForms::Timer();
            timer.Interval = (31 * 60 * 1000); // 15 mins
            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Start();

            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
        }
        void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            if (dataGridView1.Rows.Count == 0)
                return;

            string taskCity = cmB_city.Text;
            string road = cmB_road.Text;
            QuerySettingDataBase.RefreshDataGridTimerEventProcessor(dataGridView2, taskCity, road);
            new Thread(() => { FunctionalPanel.GetSaveDataGridViewInJson(dataGridView2, taskCity); }) { IsBackground = true }.Start();
            new Thread(() => { SaveFileDataGridViewPC.AutoSaveFilePC(dataGridView2, taskCity); }) { IsBackground = true }.Start();
            new Thread(() => { QuerySettingDataBase.CopyDataBaseRadiostantionInRadiostantionCopy(); }) { IsBackground = true }.Start();
        }

        #region Счётчики
        void Counters()
        {
            decimal sumTO = 0;
            int colRemont = 0;
            decimal sumRemont = 0;
            int inRepair = 0;
            int verified = 0;
            int decommission = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                if ((Boolean)(dataGridView1.Rows[i].Cells["category"].Value.ToString() != "")) colRemont++;
                if ((Boolean)(dataGridView1.Rows[i].Cells["verifiedRST"].Value.ToString() == "+")) verified++;
                if ((Boolean)(dataGridView1.Rows[i].Cells["verifiedRST"].Value.ToString() == "?")) inRepair++;
                if ((Boolean)(dataGridView1.Rows[i].Cells["verifiedRST"].Value.ToString() == "0")) decommission++;
                sumTO += Convert.ToDecimal(dataGridView1.Rows[i].Cells["price"].Value);
                sumRemont += Convert.ToDecimal(dataGridView1.Rows[i].Cells["priceRemont"].Value);
            }
            lbl_verified.Text = verified.ToString();
            lbl_inRepair.Text = inRepair.ToString();
            lbL_count.Text = dataGridView1.Rows.Count.ToString();
            lbL_summ.Text = sumTO.ToString();
            lbL_countRemont.Text = colRemont.ToString();
            lbL_sumRemont.Text = sumRemont.ToString();
            lbl_decommission.Text = decommission.ToString();
        }
        #endregion

        #region загрузка всей таблицы ТО в текущем году
        void BtnAllDataBaseClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавь радиостанцию", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            QuerySettingDataBase.FullDataBase(dataGridView1, cmB_road.Text);
            Counters();
            txb_flag_all_BD.Text = "Вся БД";
        }
        #endregion

        #region загрузка городов CmB_city_Click
        void CmbCityClick(object sender, EventArgs e)
        {
            QuerySettingDataBase.SelectCityGropBy(cmB_city, cmB_road);
        }
        void CmbCitySelectionChangeCommitted(object sender, EventArgs e)
        {
            BtnSeachDataBaseCityClick(sender, e);
        }
        #endregion

        #region загрузка городов в cmB_road
        void CmbRoadSelectionChangeCommitted(object sender, EventArgs e)
        {
            QuerySettingDataBase.SelectCityGropBy(cmB_city, cmB_road);
        }
        #endregion

        #region Сохранение поля город проведения проверки
        void BtnAddCityClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cmB_city.Text))
            {
                MessageBox.Show("Комбобокс \"Город\" пуст, необходимо добавить новую радиостанцию\n P.s. Ввводи город правильно", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.CreateSubKey("SOFTWARE\\ServiceTelekom_Setting");
            helloKey.SetValue("Город проведения проверки", $"{cmB_city.Text}");
            helloKey.Close();
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
                cmB_road.Text = row.Cells[40].Value.ToString();
            }
        }
        #endregion

        #region Clear contorl-ы
        void ClearFields()
        {
            foreach (Control control in panel1.Controls)
                if (control is TextBox)
                    control.Text = String.Empty;
            foreach (Control control in panel2.Controls)
                if (control is TextBox)
                    control.Text = String.Empty;
        }

        void PictureBox1_clear_Click(object sender, EventArgs e)
        {
            string Mesage = "Вы действительно хотите очистить все введенные вами поля?";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            ClearFields();
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
        void BtnSaveInFileClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавь радиостанцию", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            pnL_printBase.Visible = true;
        }

        #region панель для выбора выгрузки базы
        void PnL_printBaseClose_Click(object sender, EventArgs e)
        {
            pnL_printBase.Visible = false;
        }
        void Btn_SaveDirectorateBase_Click(object sender, EventArgs e)
        {
            pnL_printBase.Visible = false;
            SaveFileDataGridViewPC.DirectorateSaveFilePC(dataGridView1, cmB_city.Text);
        }
        void Btn_SaveFullBase_Click(object sender, EventArgs e)
        {
            pnL_printBase.Visible = false;
            SaveFileDataGridViewPC.SaveFullBasePC(dataGridView1, cmB_city.Text);
        }
        #endregion

        #endregion

        #region Взаимодействие на форме Key-Press-ы, Button_click
        void CmbNumberUniqueActsSelectionChangeCommitted(object sender, EventArgs e)
        {
            QuerySettingDataBase.Search(dataGridView1, cmB_seach.Text, cmB_city.Text, textBox_search.Text, cmb_number_unique_acts.Text, cmB_road.Text, txb_flag_all_BD.Text);
            Counters();
        }
        void TxbSearchKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                QuerySettingDataBase.Search(dataGridView1, cmB_seach.Text, cmB_city.Text, textBox_search.Text, cmb_number_unique_acts.Text, cmB_road.Text, txb_flag_all_BD.Text);
                Counters();
            }
        }
        void BtnSearchClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавь радиостанцию", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            QuerySettingDataBase.Search(dataGridView1, cmB_seach.Text, cmB_city.Text, textBox_search.Text, cmb_number_unique_acts.Text, cmB_road.Text, txb_flag_all_BD.Text);
            Counters();
        }
        void BtnSeachDataBaseCityClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cmB_city.Text))
            {
                MessageBox.Show("Комбобокс \"Город\" пуст, необходимо добавить новую радиостанцию\n P.s. Ввводи город правильно", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.CreateSubKey("SOFTWARE\\ServiceTelekom_Setting\\City");
            helloKey.SetValue("Город проведения проверки", $"{cmB_city.Text}");
            helloKey.Close();
            QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text, cmB_road.Text);
            QuerySettingDataBase.SelectCityGropBy(cmB_city, cmB_road);
            Counters();
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\ServiceTelekom_Setting\\City");
            if (reg != null)
            {
                RegistryKey currentUserKey2 = Registry.CurrentUser;
                RegistryKey helloKey2 = currentUserKey2.OpenSubKey("SOFTWARE\\ServiceTelekom_Setting\\City");
                cmB_city.Text = helloKey2.GetValue("Город проведения проверки").ToString();
            }
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
                QuerySettingDataBase.UpdateDataGridViewNumberAct(dataGridView1, cmB_city.Text, txB_numberAct.Text, cmB_road.Text);
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
            QuerySettingDataBase.UpdateDataGridViewNumberAct(dataGridView1, cmB_city.Text, txB_numberAct.Text, cmB_road.Text);
            Counters();
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

        #region поиск отсутсвующих рст исходя из предыдущего года
        void PicbSeachDatadridReplayClick(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            panel3.Enabled = false;
            menuStrip1.Enabled = false;
            QuerySettingDataBase.SeachDataGridReplayRST(dataGridView1, txb_flag_all_BD, cmB_city.Text, cmB_road.Text);
            Counters();
        }

        #endregion

        #region АКТ => excel
        void BtnActTOPrintClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
            {
                MessageBox.Show($"Нельзя напечатать акт ТО, на радиостанцию номер: {txB_serialNumber.Text} от предприятия {txB_company.Text}, есть списание!");
                return;
            }
            if (String.IsNullOrWhiteSpace(txB_numberAct.Text))
            {
                MessageBox.Show("Нельзя напечатать \"Акт ТО\"! Выбери \"Акт ТО\" в таблице", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int dgwRowsCount = QuerySettingDataBase.UpdateDataGridViewNumberAct(dataGridView1, txB_city.Text, txB_numberAct.Text, cmB_road.Text);
            if (dgwRowsCount == 0)
                return;
            if (dgwRowsCount > 20)
            {
                MessageBox.Show("Нельзя напечатать \"Акт ТО\"! В Акте более 20 радиостанций", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int currRowIndex = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.ClearSelection();
            if (dataGridView1.CurrentCell.RowIndex >= 0)
                dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
            RefreshValuesTxbCmb(currRowIndex);
            if (!String.IsNullOrWhiteSpace(txB_numberAct.Text))
                dataGridView1.Sort(dataGridView1.Columns["model"], ListSortDirection.Ascending);
            PrintExcel.PrintExcelActTo(dataGridView1, txB_numberAct.Text, txB_dateTO.Text, txB_company.Text, txB_location.Text,
                lbl_ChiefFIO.Text, txB_post.Text, txB_representative.Text, txB_numberIdentification.Text, lbl_EngineerFIO.Text,
                lbL_doverennost.Text, cmB_road.Text, txB_dateIssue.Text, txB_city.Text, cmB_poligon.Text);
            QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text, cmB_road.Text);
        }
        void BtnContinueRemontActClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txB_Full_name_company.Text)
                && !String.IsNullOrWhiteSpace(txB_OKPO_remont.Text)
                && !String.IsNullOrWhiteSpace(txB_BE_remont.Text)
                && !String.IsNullOrWhiteSpace(txB_director_FIO_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_director_post_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_chairman_FIO_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_chairman_post_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_1_FIO_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_1_post_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_2_FIO_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_2_post_remont_company.Text))
            {
                panel_remont_information_company.Visible = false;
                panel_remont_information_company.Enabled = false;
                string mainMeans = QuerySettingDataBase.LoadingValuesOC6(txB_serialNumber.Text, cmB_city.Text, cmB_road.Text).Item1;
                string nameProductRepaired = QuerySettingDataBase.LoadingValuesOC6(txB_serialNumber.Text, cmB_city.Text, cmB_road.Text).Item2;
                PrintExcel.PrintExcelActRemont(dataGridView1, txB_dateTO.Text, txB_company.Text, txB_location.Text,
                     lbl_ChiefFIO.Text, txB_post.Text, txB_representative.Text, txB_numberIdentification.Text, lbl_EngineerFIO.Text,
                     lbL_doverennost.Text, cmB_road.Text, txB_dateIssue.Text, txB_city.Text, cmB_poligon.Text, cmB_сategory.Text,
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
            if (!String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
            {
                string Mesage = $"На РСТ №: {txB_serialNumber.Text}, предприятия: {txB_company.Text} есть списание. Точно удалить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            if (!String.IsNullOrWhiteSpace(txB_numberActRemont.Text))
            {
                string Mesage = $"На РСТ №: {txB_serialNumber.Text}, предприятия: {txB_company.Text} есть ремонт. Точно удалить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            QuerySettingDataBase.DeleteRowCellRadiostantion(dataGridView1);
            txB_serialNumber.Clear();
            txB_numberAct.Clear();
            txB_numberActRemont.Clear();
            int currRowIndex = dataGridView1.CurrentCell.RowIndex;
            QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text, cmB_road.Text);
            txB_numberAct.Text = String.Empty;
            dataGridView1.ClearSelection();
            if (dataGridView1.RowCount - currRowIndex > 0)
                dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
            Counters();
        }

        #endregion

        #region обновление БД
        void BtnUpdateClick(object sender, EventArgs e)
        {
            txb_flag_all_BD.Text = String.Empty; // для получения данных отст. РСТ по городу(исправлена ошибка при получении полной бд => обновление )
            if (dataGridView1.Rows.Count > 0)
            {
                int currRowIndex = dataGridView1.CurrentCell.RowIndex;
                int index = dataGridView1.CurrentRow.Index;
                QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text, cmB_road.Text);
                Counters();
                dataGridView1.ClearSelection();
                if (currRowIndex >= 0)
                {
                    dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
                    dataGridView1.FirstDisplayedScrollingRowIndex = index;
                }
            }
            else
            {
                QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text, cmB_road.Text);
                Counters();
                dataGridView1.ClearSelection();
            }
        }
        void PicbUpdateClick(object sender, EventArgs e)
        {
            BtnUpdateClick(sender, e);
        }

        #endregion

        #region Форма добавления РСТ
        void BtnNewAddRadiostantionFormClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                AddRSTForm addRSTForm = new AddRSTForm();
                if (Application.OpenForms["AddRSTForm"] == null)
                {
                    addRSTForm.DoubleBufferedForm(true);
                    addRSTForm.txB_numberAct.Text = lbL_numberPrintDocument.Text + "/";
                    addRSTForm.lbL_city.Text = cmB_city.Text;
                    if (String.IsNullOrWhiteSpace(txB_city.Text))
                        addRSTForm.txB_city.Text = cmB_city.Text;
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
                    addRSTForm.lbL_road.Text = cmB_road.Text;
                    addRSTForm.Show();
                }
            }
        }
        #endregion

        #region Параметры радиостанции
        void AddRadioStationParameters(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txB_serialNumber.Text))
                return;
            if (!String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
            {
                MessageBox.Show("Нельзя добавить параметры на радиостанцию, есть списание");
                return;
            }
            if (InternetCheck.CheackSkyNET())
            {
                AddRadioStationParametersForm addParameters = new AddRadioStationParametersForm();
                if (Application.OpenForms["AddRadioStationParametersForm"] == null)
                {
                    addParameters.DoubleBufferedForm(true);
                    addParameters.txB_serialNumber.Text = txB_serialNumber.Text;
                    addParameters.txB_model.Text = cmB_model.Text;
                    String dateTO = Convert.ToDateTime(txB_dateTO.Text).ToString("dd.MM.yyyy");
                    addParameters.txB_dateTO.Text = dateTO;
                    addParameters.txB_numberAct.Text = txB_numberAct.Text;
                    addParameters.lbL_nameAKB.Text = txB_AKB.Text;
                    addParameters.lbL_BatteryChargerAccessories.Text = txB_batteryСharger.Text;
                    addParameters.lbL_ManipulatorAccessories.Text = txB_manipulator.Text;
                    addParameters.lbL_city.Text = txB_city.Text;
                    addParameters.lbL_road.Text = cmB_road.Text;
                    addParameters.lbL_company.Text = txB_company.Text;
                    addParameters.lbl_location.Text = txB_location.Text;
                    addParameters.Show();
                }
            }
        }
        #endregion

        #region отк. формы изменения РСТ
        void BtnChangeRadiostantionFormClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                if (!String.IsNullOrWhiteSpace(txB_serialNumber.Text))
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
                        changeRSTForm.lbL_road.Text = cmB_road.Text;
                        if (!String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
                            changeRSTForm.txB_decommissionSerialNumber.Text = txB_decommissionSerialNumber.Text;
                        if (txB_dateIssue.Text == String.Empty)
                            txB_dateIssue.Text = DateTime.Now.ToString("dd.MM.yyyy");
                        changeRSTForm.txB_dateIssue.Text = txB_dateIssue.Text;
                        if (txB_antenna.Text == String.Empty)
                            txB_antenna.Text = "-";
                        changeRSTForm.txB_antenna.Text = txB_antenna.Text;
                        if (txB_manipulator.Text == String.Empty)
                            txB_manipulator.Text = "-";
                        changeRSTForm.txB_manipulator.Text = txB_manipulator.Text;
                        if (txB_batteryСharger.Text == String.Empty)
                            txB_batteryСharger.Text = "-";
                        changeRSTForm.txB_batteryСharger.Text = txB_batteryСharger.Text;
                        if (txB_AKB.Text == String.Empty)
                            txB_AKB.Text = "-";
                        changeRSTForm.txB_AKB.Text = txB_AKB.Text;
                        changeRSTForm.Show();
                    }
                }
            }
        }
        #endregion

        #region panel date information
        void BtnClosePanelDateInfoClick(object sender, EventArgs e)
        {
            panel_date.Visible = false;
            dataGridView1.Enabled = true;
            panel1.Enabled = true;
            panel3.Enabled = true;
        }
        #endregion   

        #region Печать ведомости с параметрами => excel
        void PrintStatementParameters(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
            {
                MessageBox.Show($"Нельзя напечатать акт ТО, на радиостанцию номер: {txB_serialNumber.Text} от предприятия {txB_company.Text}, есть списание!");
                return;
            }
            if (String.IsNullOrWhiteSpace(txB_numberAct.Text))
            {
                MessageBox.Show("Нельзя напечатать \"Ведомость с параметрами\"! Выбери \"Акт ТО\" в таблице", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int dgwRowsCount = QuerySettingDataBase.UpdateDataGridViewNumberAct(dataGridView1, txB_city.Text, txB_numberAct.Text, cmB_road.Text);
            if (dgwRowsCount == 0)
                return;
            if (dgwRowsCount > 20)
            {
                MessageBox.Show("Нельзя напечатать \"Акт ТО\"! В Акте более 20 радиостанций", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int currRowIndex = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.ClearSelection();
            if (dataGridView1.CurrentCell.RowIndex >= 0)
                dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
            RefreshValuesTxbCmb(currRowIndex);
            if (!String.IsNullOrWhiteSpace(txB_numberAct.Text))
                dataGridView1.Sort(dataGridView1.Columns["model"], ListSortDirection.Ascending);
            PrintExcel.PrintExcelStatementParameters(dataGridView1, txB_numberAct.Text, txB_dateTO.Text, txB_company.Text, txB_location.Text,
               lbl_ChiefFIO.Text, txB_post.Text, txB_representative.Text, txB_numberIdentification.Text, lbl_EngineerFIO.Text,
               lbL_doverennost.Text, cmB_road.Text, txB_dateIssue.Text, txB_city.Text, cmB_poligon.Text);
            QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text, cmB_road.Text);
        }
        #endregion

        #region Печать отчёта по Манипуляторам

        void PrintReportManipulator(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cmB_poligon.Text))
            {
                MessageBox.Show("Нажми на строчку в таблице.");
                return;
            }
            PrintExcel.PrintReportManipulator(cmB_city.Text, cmB_road.Text, cmB_poligon.Text, txb_flag_all_BD);
        }

        void PrintReportFullManipulator(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cmB_poligon.Text))
            {
                MessageBox.Show("Нажми на строчку в таблице.");
                return;
            }
            PrintExcel.PrintExcelFullManipulator(cmB_city.Text, cmB_road.Text, cmB_poligon.Text);
        }

        #endregion

        #region Печать отчёт по АКБ
        void PrintReportAKB(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cmB_poligon.Text))
            {
                MessageBox.Show("Нажми на строчку в таблице.");
                return;
            }

            PrintExcel.PrintExcelReportAKB(cmB_city.Text, cmB_road.Text, cmB_poligon.Text, txb_flag_all_BD);
        }

        void PrintReportFullAKB(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cmB_poligon.Text))
            {
                MessageBox.Show("Нажми на строчку в таблице.");
                return;
            }

            PrintExcel.PrintExcelFullAKB(cmB_city.Text, cmB_road.Text, cmB_poligon.Text);
        }
        #endregion

        #region ContextMenu datagrid
        void DataGridView1MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (_user.IsAdmin == "Дирекция связи")
                {
                    ContextMenu m3 = new ContextMenu();
                    m3.MenuItems.Add(new MenuItem("Сохранение базы", BtnSaveInFileClick));
                    m3.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClick));
                    m3.Show(dataGridView1, new Point(e.X, e.Y));
                }
                else if (_user.IsAdmin == "Куратор" || _user.IsAdmin == "Руководитель")
                {
                    if (dataGridView1.Rows.Count > 0 && panel1.Enabled == true && panel3.Enabled == true)
                    {
                        ContextMenu m = new ContextMenu();
                        int addNewRadiostation = m.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", BtnNewAddRadiostantionFormClick));
                        if (!String.IsNullOrWhiteSpace(txB_serialNumber.Text))
                        {
                            m.MenuItems.Add(new MenuItem("Изменить радиостанцию", BtnChangeRadiostantionFormClick));
                            m.MenuItems.Add(new MenuItem("Добавить/изменить ремонт", BtnNewAddRadiostantionFormClickRemont));
                            m.MenuItems.Add(new MenuItem("Печатать акт ТО", BtnActTOPrintClick));
                            m.MenuItems.Add(new MenuItem("Печатать акт Ремонта", BtnRemontActClick));
                            m.MenuItems.Add(new MenuItem("Удалить радиостанцию", BtnDeleteClick));
                            m.MenuItems.Add(new MenuItem("Удалить ремонт", DeleteRadiostantionRemontClick));
                            m.MenuItems.Add(new MenuItem("Заполняем акт", AddFillFullActTO));
                            m.MenuItems.Add(new MenuItem("На подписание акт", AddSignatureActTO));
                            m.MenuItems.Add(new MenuItem("Списать РСТ", DecommissionSerialNumber));
                            m.MenuItems.Add(new MenuItem("Добавить в выполнение", AddExecution));
                        }
                        if (!String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
                        {
                            m.MenuItems.Add(new MenuItem("Печатать акт списания", PrintWordActDecommission));
                            m.MenuItems.Add(new MenuItem("Удалить списание", DeleteRadiostantionDecommissionClick));
                        }
                        m.MenuItems.Add(new MenuItem("Обновить базу", BtnUpdateClick));
                        m.MenuItems.Add(new MenuItem("Сохранение базы", BtnSaveInFileClick));
                        m.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                    else if (dataGridView1.Rows.Count == 0 && panel1.Enabled == true && panel3.Enabled == true)
                    {
                        ContextMenu m1 = new ContextMenu();
                        m1.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", BtnNewAddRadiostantionFormClick));
                        m1.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClick));
                        m1.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                    else if (dataGridView1.Rows.Count > 0 || dataGridView1.Rows.Count == 0 && panel1.Enabled == false && panel3.Enabled == false)
                    {
                        ContextMenu m2 = new ContextMenu();
                        m2.MenuItems.Add(new MenuItem("Сохранение базы", BtnSaveInFileClick));
                        m2.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClickAfterSeachDataGridReplayRadiostantion));
                        m2.Show(dataGridView1, new Point(e.X, e.Y));
                        if (e.Button == MouseButtons.Left)
                            dataGridView1.ClearSelection();
                    }
                }
                else if (_user.IsAdmin == "Инженер")
                {
                    if (dataGridView1.Rows.Count > 0 && panel1.Enabled == true && panel3.Enabled == true)
                    {
                        ContextMenu m = new ContextMenu();
                        int addNewRadiostation = m.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", BtnNewAddRadiostantionFormClick));
                        if (!String.IsNullOrWhiteSpace(txB_serialNumber.Text))
                        {
                            m.MenuItems.Add(new MenuItem("Добавить параметры радиостанции", AddRadioStationParameters));
                            m.MenuItems.Add(new MenuItem("Добавить/изменить ремонт", BtnNewAddRadiostantionFormClickRemont));
                            m.MenuItems.Add(new MenuItem("Печатать акт ТО", BtnActTOPrintClick));
                            m.MenuItems.Add(new MenuItem("Печатать акт Ремонта", BtnRemontActClick));
                            m.MenuItems.Add(new MenuItem("Печатать ведомость с параметрами", PrintStatementParameters));
                        }
                        m.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClick));
                        m.MenuItems.Add(new MenuItem("Сохранение базы", BtnSaveInFileClick));
                        m.MenuItems.Add(new MenuItem("Печатать бирки", FormTag));
                        m.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                    else if (dataGridView1.Rows.Count == 0 && panel1.Enabled == true && panel3.Enabled == true)
                    {
                        ContextMenu m1 = new ContextMenu();
                        m1.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", BtnNewAddRadiostantionFormClick));
                        m1.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClick));
                        m1.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                    else if (dataGridView1.Rows.Count > 0 || dataGridView1.Rows.Count == 0 && panel1.Enabled == false && panel3.Enabled == false)
                    {
                        ContextMenu m2 = new ContextMenu();
                        m2.MenuItems.Add(new MenuItem("Сохранение базы", BtnSaveInFileClick));
                        m2.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClickAfterSeachDataGridReplayRadiostantion));
                        m2.Show(dataGridView1, new Point(e.X, e.Y));
                        if (e.Button == MouseButtons.Left)
                            dataGridView1.ClearSelection();
                    }
                }
                else if (_user.IsAdmin == "Начальник участка")
                {
                    if (dataGridView1.Rows.Count > 0 && panel1.Enabled == true && panel3.Enabled == true)
                    {
                        ContextMenu m = new ContextMenu();
                        int addNewRadiostation = m.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", BtnNewAddRadiostantionFormClick));
                        if (!String.IsNullOrWhiteSpace(txB_serialNumber.Text))
                        {
                            m.MenuItems.Add(new MenuItem("Изменить радиостанцию", BtnChangeRadiostantionFormClick));
                            m.MenuItems.Add(new MenuItem("Добавить/изменить ремонт", BtnNewAddRadiostantionFormClickRemont));
                            m.MenuItems.Add(new MenuItem("Добавить параметры радиостанции", AddRadioStationParameters));
                            m.MenuItems.Add(new MenuItem("Печатать акт ТО", BtnActTOPrintClick));
                            m.MenuItems.Add(new MenuItem("Печатать акт Ремонта", BtnRemontActClick));
                            m.MenuItems.Add(new MenuItem("Удалить радиостанцию", BtnDeleteClick));
                            m.MenuItems.Add(new MenuItem("Удалить ремонт", DeleteRadiostantionRemontClick));
                            m.MenuItems.Add(new MenuItem("Заполняем акт", AddFillFullActTO));
                            m.MenuItems.Add(new MenuItem("На подписание акт", AddSignatureActTO));
                            m.MenuItems.Add(new MenuItem("Списать РСТ", DecommissionSerialNumber));
                            m.MenuItems.Add(new MenuItem("Изменить номер акта", ChangeNumberAct));
                            m.MenuItems.Add(new MenuItem("Печатать ведомость с параметрами", PrintStatementParameters));
                        }
                        if (!String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
                        {
                            m.MenuItems.Add(new MenuItem("Печатать акт списания", PrintWordActDecommission));
                            m.MenuItems.Add(new MenuItem("Удалить списание", DeleteRadiostantionDecommissionClick));
                        }
                        m.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClick));
                        m.MenuItems.Add(new MenuItem("Сохранение базы", BtnSaveInFileClick));
                        m.MenuItems.Add(new MenuItem("Сформировать бирки", FormTag));
                        m.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                    else if (dataGridView1.Rows.Count == 0 && panel1.Enabled == true && panel3.Enabled == true)
                    {
                        ContextMenu m1 = new ContextMenu();
                        m1.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", BtnNewAddRadiostantionFormClick));
                        m1.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClick));
                        m1.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                    else if (dataGridView1.Rows.Count > 0 || dataGridView1.Rows.Count == 0 && panel1.Enabled == false && panel3.Enabled == false)
                    {
                        ContextMenu m2 = new ContextMenu();
                        m2.MenuItems.Add(new MenuItem("Сохранение базы", BtnSaveInFileClick));
                        m2.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClickAfterSeachDataGridReplayRadiostantion));
                        m2.Show(dataGridView1, new Point(e.X, e.Y));
                        if (e.Button == MouseButtons.Left)
                            dataGridView1.ClearSelection();
                    }
                }
                else if (_user.IsAdmin == "Admin")
                {
                    if (dataGridView1.Rows.Count > 0 && panel1.Enabled == true && panel3.Enabled == true)
                    {
                        ContextMenu m = new ContextMenu();
                        int addNewRadiostation = m.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", BtnNewAddRadiostantionFormClick));
                        if (!String.IsNullOrWhiteSpace(txB_serialNumber.Text))
                        {
                            m.MenuItems.Add(new MenuItem("Изменить радиостанцию", BtnChangeRadiostantionFormClick));
                            m.MenuItems.Add(new MenuItem("Добавить/изменить ремонт", BtnNewAddRadiostantionFormClickRemont));
                            m.MenuItems.Add(new MenuItem("Добавить параметры радиостанции", AddRadioStationParameters));
                            m.MenuItems.Add(new MenuItem("Печатать акт ТО", BtnActTOPrintClick));
                            m.MenuItems.Add(new MenuItem("Печатать акт Ремонта", BtnRemontActClick));
                            m.MenuItems.Add(new MenuItem("Удалить радиостанцию", BtnDeleteClick));
                            m.MenuItems.Add(new MenuItem("Удалить ремонт", DeleteRadiostantionRemontClick));
                            m.MenuItems.Add(new MenuItem("Заполняем акт", AddFillFullActTO));
                            m.MenuItems.Add(new MenuItem("На подписание акт", AddSignatureActTO));
                            m.MenuItems.Add(new MenuItem("Списать РСТ", DecommissionSerialNumber));
                            m.MenuItems.Add(new MenuItem("Добавить в выполнение", AddExecution));
                            m.MenuItems.Add(new MenuItem("Изменить номер акта", ChangeNumberAct));
                            m.MenuItems.Add(new MenuItem("Печатать ведомость с параметрами", PrintStatementParameters));
                        }
                        if (!String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
                        {
                            m.MenuItems.Add(new MenuItem("Сформировать акт списания", PrintWordActDecommission));
                            m.MenuItems.Add(new MenuItem("Удалить списание", DeleteRadiostantionDecommissionClick));
                        }
                        m.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClick));
                        m.MenuItems.Add(new MenuItem("Сохранение базы", BtnSaveInFileClick));
                        m.MenuItems.Add(new MenuItem("Сформировать бирки", FormTag));

                        m.Show(dataGridView1, new Point(e.X, e.Y));

                    }
                    else if (dataGridView1.Rows.Count == 0 && panel1.Enabled == true && panel3.Enabled == true)
                    {
                        ContextMenu m1 = new ContextMenu();
                        m1.MenuItems.Add(new MenuItem("Добавить новую радиостанцию", BtnNewAddRadiostantionFormClick));
                        m1.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClick));
                        m1.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                    else if (dataGridView1.Rows.Count > 0 || dataGridView1.Rows.Count == 0 && panel1.Enabled == false && panel3.Enabled == false)
                    {
                        ContextMenu m2 = new ContextMenu();
                        m2.MenuItems.Add(new MenuItem("Сохранение базы", BtnSaveInFileClick));
                        m2.MenuItems.Add(new MenuItem("Обновить", BtnUpdateClickAfterSeachDataGridReplayRadiostantion));
                        m2.Show(dataGridView1, new Point(e.X, e.Y));
                        if (e.Button == MouseButtons.Left)
                            dataGridView1.ClearSelection();
                    }
                }
            }
        }
        #endregion

        #region обновляем БД после показа отсутсвующих радиостанций после проверки на участке
        void BtnUpdateClickAfterSeachDataGridReplayRadiostantion(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                if (dataGridView1.Rows.Count >= 0)
                {
                    panel1.Enabled = true;
                    panel3.Enabled = true;
                    menuStrip1.Enabled = true;
                    QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text, cmB_road.Text);
                    Counters();
                }
            }
        }

        #endregion

        #region Удаление ремонта
        void DeleteRadiostantionRemontClick(object sender, EventArgs e)
        {
            string Mesage = $"Вы действительно хотите удалить ремонт у радиостанции: {txB_serialNumber.Text}, предприятия: {txB_company.Text}?";
            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            QuerySettingDataBase.DeleteRadiostantionRemont(txB_numberActRemont.Text, txB_serialNumber.Text, cmB_city.Text, cmB_road.Text);
            BtnUpdateClick(sender, e);
        }

        #endregion

        #region отк. формы добавления ремонтов
        private void BtnNewAddRadiostantionFormClickRemont(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                if (!String.IsNullOrWhiteSpace(txB_serialNumber.Text))
                {
                    if (!String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
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
                        remontRSTForm.lbL_city.Text = cmB_city.Text;
                        remontRSTForm.lbL_road.Text = cmB_road.Text;
                        if (String.IsNullOrWhiteSpace(txB_dateTO.Text))
                            txB_dateTO.Text = DateTime.Now.ToString("dd.MM.yyyy");
                        remontRSTForm.txB_data_remont.Text = txB_dateTO.Text;
                        remontRSTForm.txB_model.Text = cmB_model.Text;
                        remontRSTForm.label_company.Text = txB_company.Text;
                        remontRSTForm.txB_serialNumber.Text = txB_serialNumber.Text;
                        if (String.IsNullOrWhiteSpace(txB_numberActRemont.Text))
                            remontRSTForm.txB_numberActRemont.Text = lbL_numberPrintDocument.Text + "/";
                        else remontRSTForm.txB_numberActRemont.Text = txB_numberActRemont.Text;
                        remontRSTForm.Show();
                    }
                }
            }
        }
        #endregion

        #region panel_remont_info 
        void BtnRemontActClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                if (String.IsNullOrWhiteSpace(txB_numberActRemont.Text))
                {
                    MessageBox.Show("Нельзя напечатать \"Акт ремонта\"! Выбери \"Акт ремонта\" в таблице", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
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
                        if (!String.IsNullOrWhiteSpace(txB_Full_name_company.Text)
                            && !String.IsNullOrWhiteSpace(txB_OKPO_remont.Text)
                            && !String.IsNullOrWhiteSpace(txB_BE_remont.Text)
                            && !String.IsNullOrWhiteSpace(txB_director_FIO_remont_company.Text)
                            && !String.IsNullOrWhiteSpace(txB_director_post_remont_company.Text)
                            && !String.IsNullOrWhiteSpace(txB_chairman_FIO_remont_company.Text)
                            && !String.IsNullOrWhiteSpace(txB_chairman_post_remont_company.Text)
                            && !String.IsNullOrWhiteSpace(txB_1_FIO_remont_company.Text)
                            && !String.IsNullOrWhiteSpace(txB_1_post_remont_company.Text)
                            && !String.IsNullOrWhiteSpace(txB_2_FIO_remont_company.Text)
                            && !String.IsNullOrWhiteSpace(txB_2_post_remont_company.Text))
                            btn_ContinueRemontActExcel.Enabled = true;

                        helloKey.Close();
                    }
                    else
                    {
                        btn_ContinueRemontActExcel.Enabled = false;
                        txB_Full_name_company.Text = String.Empty;
                        txB_OKPO_remont.Text = String.Empty;
                        txB_BE_remont.Text = String.Empty;
                        txB_director_FIO_remont_company.Text = String.Empty;
                        txB_director_post_remont_company.Text = $"Начальник {txB_company.Text}";
                        txB_chairman_FIO_remont_company.Text = String.Empty;
                        txB_chairman_post_remont_company.Text = String.Empty;
                        txB_1_FIO_remont_company.Text = String.Empty;
                        txB_1_post_remont_company.Text = String.Empty;
                        txB_2_FIO_remont_company.Text = String.Empty;
                        txB_2_post_remont_company.Text = String.Empty;
                        txB_3_FIO_remont_company.Text = String.Empty;
                        txB_3_post_remont_company.Text = String.Empty;
                    }
                }
            }
        }
        void BtnCloseRemontPanelClick(object sender, EventArgs e)
        {
            panel_remont_information_company.Visible = false;
            panel_remont_information_company.Enabled = false;
            dataGridView1.Enabled = true;
            panel1.Enabled = true;
        }
        void BtnInformationRemontCompanyRegeditClick(object sender, EventArgs e)
        {

            #region проверка пустых строк
            if (String.IsNullOrWhiteSpace(txB_Full_name_company.Text))
            {
                string Mesage2 = "Вы не заполнили поле \"Полное наименование предприятия\"!";
                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    return;
            }
            if (String.IsNullOrWhiteSpace(txB_OKPO_remont.Text))
            {
                string Mesage2 = "Вы не заполнили поле \"ОКПО\"!";
                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    return;
            }
            if (String.IsNullOrWhiteSpace(txB_BE_remont.Text))
            {
                string Mesage2 = "Вы не заполнили поле \"БЕ\"!";
                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    return;
            }
            if (String.IsNullOrWhiteSpace(txB_director_FIO_remont_company.Text))
            {
                string Mesage2 = "Вы не заполнили поле \"Руководитель ФИО\"!";
                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    return;
            }
            if (String.IsNullOrWhiteSpace(txB_director_post_remont_company.Text))
            {
                string Mesage2 = "Вы не заполнили поле \"Руководитель Должность\"!";
                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    return;
            }
            if (String.IsNullOrWhiteSpace(txB_chairman_FIO_remont_company.Text))
            {
                string Mesage2 = "Вы не заполнили поле \"Председатель ФИО\"!";
                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    return;
            }
            if (String.IsNullOrWhiteSpace(txB_chairman_post_remont_company.Text))
            {
                string Mesage2 = "Вы не заполнили поле \"Председатель Должность\"!";
                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    return;
            }
            if (String.IsNullOrWhiteSpace(txB_1_FIO_remont_company.Text))
            {
                string Mesage2 = "Вы не заполнили поле \"1 член комиссии ФИО\"!";
                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    return;
            }
            if (String.IsNullOrWhiteSpace(txB_1_post_remont_company.Text))
            {
                string Mesage2 = "Вы не заполнили поле \"1 член комиссии Должность\"!";
                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    return;
            }
            if (String.IsNullOrWhiteSpace(txB_2_FIO_remont_company.Text))
            {
                string Mesage2 = "Вы не заполнили поле \"2 член комиссии ФИО\"!";
                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    return;
            }
            if (String.IsNullOrWhiteSpace(txB_2_post_remont_company.Text))
            {
                string Mesage2 = "Вы не заполнили поле \"2 член комиссии Должность\"!";
                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    return;
            }
            #endregion

            if (!Regex.IsMatch(txB_OKPO_remont.Text, @"^[0-9]{8,}$"))
            {
                MessageBox.Show("Введите корректно поле \"ОКПО\"\nP.s. пример: 00083262", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_OKPO_remont.Select();
                string Mesage = "Вы действительно хотите продолжить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            if (!Regex.IsMatch(txB_BE_remont.Text, @"^[0-9]{4,}$"))
            {
                MessageBox.Show("Введите корректно поле \"БЕ\"\nP.s. пример: 5374", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_BE_remont.Select();
                string Mesage = "Вы действительно хотите продолжить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            if (!Regex.IsMatch(txB_Full_name_company.Text, @"[А-Яа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
            {
                MessageBox.Show("Введите корректно поле \"Полное наименование предприятия\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_Full_name_company.Select();
                string Mesage = "Вы действительно хотите продолжить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
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
            if (!Regex.IsMatch(txB_director_post_remont_company.Text, @"[А-ЯЁа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
            {
                MessageBox.Show("Введите корректно поле \"Должность руководителя\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_director_post_remont_company.Select();
                string Mesage = "Вы действительно хотите продолжить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            if (!Regex.IsMatch(txB_chairman_post_remont_company.Text, @"[А-ЯЁа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
            {
                MessageBox.Show("Введите корректно поле \"Должность председателя\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_chairman_post_remont_company.Select();
                string Mesage = "Вы действительно хотите продолжить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            if (!Regex.IsMatch(txB_1_post_remont_company.Text, @"[А-ЯЁа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
            {
                MessageBox.Show("Введите корректно поле \"Должность 1 члена комиссии\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_1_post_remont_company.Select();
                string Mesage = "Вы действительно хотите продолжить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            if (!Regex.IsMatch(txB_2_post_remont_company.Text, @"[А-ЯЁа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
            {
                MessageBox.Show("Введите корректно поле \"Должность 2 члена комиссии\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_2_post_remont_company.Select();
                string Mesage = "Вы действительно хотите продолжить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            if (!chb_pass_txB_3_FIO.Checked)
            {
                if (!Regex.IsMatch(txB_3_post_remont_company.Text, @"[А-ЯЁа-яё]*[\s]*[\-]*[""]*[\.]*[0-9]*"))
                {
                    MessageBox.Show("Введите корректно поле \"Должность 3 члена комиссии\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_3_post_remont_company.Select();
                    string Mesage = "Вы действительно хотите продолжить?";
                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;
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

            if (!String.IsNullOrWhiteSpace(txB_Full_name_company.Text)
                && !String.IsNullOrWhiteSpace(txB_OKPO_remont.Text)
                && !String.IsNullOrWhiteSpace(txB_BE_remont.Text)
                && !String.IsNullOrWhiteSpace(txB_director_FIO_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_director_post_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_chairman_FIO_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_chairman_post_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_1_FIO_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_1_post_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_2_FIO_remont_company.Text)
                && !String.IsNullOrWhiteSpace(txB_2_post_remont_company.Text))
                btn_ContinueRemontActExcel.Enabled = true;
        }
        #endregion

        #region для выбора значения в Control(TXB)
        void RefreshValuesTxbCmb(int currRowIndex)
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
        #endregion

        #region поиск по dataGrid без запроса к БД и открытие функциональной панели Control + K
        void DataGridView1KeyDown(object sender, KeyEventArgs e)
        {
            if (_user.IsAdmin == "Дирекция связи")
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
                {
                    panel_seach_datagrid.Enabled = true;
                    panel_seach_datagrid.Visible = true;
                    this.ActiveControl = txB_seach_panel_seach_datagrid;
                }
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
                {
                    if (!String.IsNullOrWhiteSpace(txB_representative.Text))
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
                    BtnFunctionalLoadingPanel(sender, e);
                // открываем панель инфы о ФИО и номере баланосдержателя
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
                {
                    if (!String.IsNullOrWhiteSpace(txB_representative.Text))
                    {
                        panel_info_phone_FIO.Enabled = true;
                        panel_info_phone_FIO.Visible = true;
                        panel_txB_FIO_representative.Text = txB_representative.Text;
                        panel_txB_FIO_phoneNumber.Text = txB_phoneNumber.Text;
                    }
                }
                //инфа о бригаде
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
        void BtnClosePanelInfoPhoneFIOClick(object sender, EventArgs e)
        {
            panel_info_phone_FIO.Enabled = false;
            panel_info_phone_FIO.Visible = false;
        }
        void SeachDatagrid()
        {
            bool found = false;
            if (!String.IsNullOrWhiteSpace(txB_seach_panel_seach_datagrid.Text))
            {
                string searchValue = txB_seach_panel_seach_datagrid.Text;
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
                            RefreshValuesTxbCmb(currRowIndex);
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
                    txB_seach_panel_seach_datagrid.Text = String.Empty;
                    panel_seach_datagrid.Enabled = false;
                    panel_seach_datagrid.Visible = false;
                }
            }
            else
            {
                string Mesage2 = "Поле поиска не должно быть пустым!";
                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
        }
        void BtnClosePanelSeachDatagridClick(object sender, EventArgs e)
        {
            panel_seach_datagrid.Enabled = false;
            panel_seach_datagrid.Visible = false;
        }
        void BtnSeachPanelSeachDatagridClick(object sender, EventArgs e)
        {
            SeachDatagrid();
        }
        void TxbSeachPanelSeachDatagridKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                SeachDatagrid();
        }
        void TxbSeachPanelSeachDatagridKeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            char ch = e.KeyChar;
            if ((ch < 'A' || ch > 'Z') && (ch <= 47 || ch >= 58) && ch != '/' && ch != '\b' && ch != '.')
                e.Handled = true;
        }
        void TxbSeachPanelSeachDatagridKeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        #endregion

        #region при выборе строк ползьзователем и их подсчёт
        void DataGridView1SelectionChanged(object sender, EventArgs e)
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

        #region Функциональная панель
        void BtnFunctionalLoadingPanel(object sender, EventArgs e)
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
        void CloseFunctionalLoadingPanelClick(object sender, EventArgs e)
        {
            Functional_loading_panel.Visible = false;
            Functional_loading_panel.Enabled = false;
            dataGridView1.Enabled = true;
            panel1.Enabled = true;
            panel3.Enabled = true;
        }

        #region добавление из файла
        void LoadingFileCurrentDataBaseClick(object sender, EventArgs e)
        {
            FunctionalPanel.LoadingFileCurrentDatabase();
        }
        void BtnLoadingFileLastYearClick(object sender, EventArgs e)
        {
            FunctionalPanel.LoadingFileLastYear();
        }
        void LoadingFileFullDataBaseClick(object sender, EventArgs e)
        {
            FunctionalPanel.LoadingFileFullDatabase();
        }
        #endregion

        #region загрузка и обновление json в radiostantion
        void LoadingJsonFileDataBaseClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
                FunctionalPanel.LoadingJsonFileInDatabase(dataGridView1, cmB_city.Text);
        }
        #endregion

        #region выгрузка всех данных из datagrid в JSON
        void BtnUploadingJsonFileClick(object sender, EventArgs e)
        {
            FunctionalPanel.GetSaveDataGridViewInJson(dataGridView1, cmB_city.Text);
        }
        #endregion

        #region копирование текущей таблицы radiostantion в radiostantion_last_year к концу года 
        void BtnCopyingCurrentDataBaseEndOfTheYearClick(object sender, EventArgs e)
        {
            string Mesage = "Вы действительно хотите скопировать всю базу данных?";
            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            string Mesage2 = "Данное действие нужно делать к концу года, для следующего года, действительно хотите продолжить?";
            if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            FunctionalPanel.CopyingCurrentLastYear();
        }
        #endregion

        #region функцональная панель ручное-резервное копирование радиостанций из текущей radiostantion в radiostantion_copy
        void ManualBackupCurrentDataBaseClick(object sender, EventArgs e)
        {
            string Mesage = "Вы действительно хотите скопировать всю базу данных?";
            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            FunctionalPanel.ManualBackupCurrent();
        }
        #endregion

        #region очистка текущей БД, текущий год (radiostantion)
        void ClearDataBaseCurrentYearClick(object sender, EventArgs e)
        {
            string Mesage = "Вы действительно хотите удалить всё содержимое базы данных?";
            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            string Mesage2 = "Всё удалится безвозратно!!!Точно хотите удалить всё содержимое Базы данных?";
            if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            FunctionalPanel.ClearCurrentYear();
            QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text, cmB_road.Text);
        }

        #endregion

        #region показать БД прошлого года по участку
        void BtnShowDataBaseRadiostantionLastYearClick(object sender, EventArgs e)
        {
            CloseFunctionalLoadingPanelClick(sender, e);
            FunctionalPanel.ShowRadiostantionLastYear(dataGridView1, cmB_city.Text, cmB_road.Text);
            Counters();
        }
        #endregion

        #region показать общую БД по всем радиостанциям
        void BtnShowDataBaseRadiostantionFullClick(object sender, EventArgs e)
        {
            CloseFunctionalLoadingPanelClick(sender, e);
            FunctionalPanel.ShowRadiostantionFull(dataGridView1, cmB_city.Text, cmB_road.Text);
            Counters();
        }
        #endregion
        #endregion

        #region close form
        void STWorkFormFormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
        void STWorkFormFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = FormClose.GetInstance.FClose(_user.Login);
        }
        #endregion

        #region добавление актов на заполнение
        void BtnAddFillFullActTOClick(object sender, EventArgs e)
        {
            AddFillFullActTO(sender, e);
        }
        void AddFillFullActTO(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txB_numberAct.Text))
            {
                if (!cmB_AddFillFullActTO.Items.Contains(txB_numberAct.Text))
                {
                    cmB_AddFillFullActTO.Items.Add(txB_numberAct.Text);
                    string registry3 = String.Empty;
                    foreach (var CmBItem in cmB_AddFillFullActTO.Items)
                    {
                        registry3 += CmBItem.ToString() + ";";
                        RegistryKey currentUserKey = Registry.CurrentUser;
                        RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                        helloKey.SetValue("Акты_незаполненные", $"{registry3}");
                        helloKey.Close();
                    }
                    cmB_AddFillFullActTO.Sorted = true;
                    cmB_AddFillFullActTO.Text = cmB_AddFillFullActTO.Items[cmB_AddFillFullActTO.Items.Count - 1].ToString();
                }
            }
        }
        void PicbDeleteItemFillFullActTOClick(object sender, EventArgs e)
        {
            if (cmB_AddFillFullActTO.Items.Count > 0)
                cmB_AddFillFullActTO.Items.Remove(cmB_AddFillFullActTO.Text);
            if (cmB_AddFillFullActTO.Items.Count == 0)
            {
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                helloKey.SetValue("Акты_незаполненные", $"");
                helloKey.Close();
            }
            string registry4 = String.Empty;
            foreach (var CmBItem in cmB_AddFillFullActTO.Items)
            {
                registry4 += CmBItem.ToString() + ";";
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                helloKey.SetValue("Акты_незаполненные", $"{registry4}");
                helloKey.Close();
                cmB_AddFillFullActTO.Text = cmB_AddFillFullActTO.Items[cmB_AddFillFullActTO.Items.Count - 1].ToString();
            }
        }
        #endregion

        #region добавление актов на подпись представителю ПП
        void BtnAddSignatureActTOClick(object sender, EventArgs e)
        {
            AddSignatureActTO(sender, e);
        }
        void AddSignatureActTO(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txB_numberAct.Text))
            {
                if (!cmB_AddSignature.Items.Contains(txB_numberAct.Text))
                {
                    cmB_AddSignature.Items.Add(txB_numberAct.Text);
                    string registry5 = String.Empty;
                    foreach (var CmBItem in cmB_AddSignature.Items)
                    {
                        registry5 += CmBItem.ToString() + ";";
                        RegistryKey currentUserKey = Registry.CurrentUser;
                        RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                        helloKey.SetValue("Акты_на_подпись", $"{registry5}");
                        helloKey.Close();
                    }
                    cmB_AddSignature.Sorted = true;
                    cmB_AddSignature.Text = cmB_AddSignature.Items[cmB_AddSignature.Items.Count - 1].ToString();
                }
            }
        }
        void PicbDeleteItemSignatureClick(object sender, EventArgs e)
        {
            if (cmB_AddSignature.Items.Count > 0)
                cmB_AddSignature.Items.Remove(cmB_AddSignature.Text);
            if (cmB_AddSignature.Items.Count == 0)
            {
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                helloKey.SetValue("Акты_на_подпись", $"");
                helloKey.Close();
            }
            string registry6 = String.Empty;
            foreach (var CmBItem in cmB_AddSignature.Items)
            {
                registry6 += CmBItem.ToString() + ";";
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.CreateSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                helloKey.SetValue("Акты_на_подпись", $"{registry6}");
                helloKey.Close();
                cmB_AddSignature.Text = cmB_AddSignature.Items[cmB_AddSignature.Items.Count - 1].ToString();
            }
        }
        #endregion

        #region списание РСТ
        void BtnDecommissionSerialNumberCloseClick(object sender, EventArgs e)
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
            if (!String.IsNullOrWhiteSpace(txB_serialNumber.Text))
            {
                if (!String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
                {
                    MessageBox.Show($"На радиостанцию номер: {txB_serialNumber.Text} от предприятия {txB_company.Text}, уже есть списание!");
                    return;
                }
                string Mesage = $"Вы действительно хотите списать радиостанцию? Номер: {txB_serialNumber.Text} от предприятия {txB_company.Text}";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
                QuerySettingDataBase.LoadingLastDecommissionSerialNumber(lbL_last_decommission, cmB_city.Text, cmB_road.Text);
                panel1.Enabled = false;
                panel2.Enabled = false;
                panel3.Enabled = false;
                dataGridView1.Enabled = false;
                panel_decommissionSerialNumber.Visible = true;
                panel_decommissionSerialNumber.Enabled = true;
                txB1_decommissionSerialNumber.Text = txB_numberAct.Text + "C";
                if (cmB_model.Text == "Comrade R5")
                    txB_reason_decommission.Text = "Выходная мощность несущей передатчика: номинальная – 5 Вт, максимальная – 9 Вт, что не соответствует нормам ГОСТ 12252 – 86г, для радиостанций третьего типа и техническим параметрам изготовителя, указанных в паспорте.";
                else txB_reason_decommission.Text = "Коррозия основной печатной платы с многочисленными обрывами проводников, вызванная попаданием влаги внутрь радиостанции. Восстановлению не подлежит.";
            }
        }
        void BtnRecordDecommissionSerialNumberClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txB1_decommissionSerialNumber.Text) && !String.IsNullOrWhiteSpace(txB_reason_decommission.Text))
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
                        return;
                }
                Regex re = new Regex(Environment.NewLine);
                txB_reason_decommission.Text = re.Replace(txB_reason_decommission.Text, " ");//удаление новой строки
                txB_reason_decommission.Text.Trim();
                txB1_decommissionSerialNumber.Text.Trim();
                QuerySettingDataBase.RecordDecommissionSerialNumber(txB_serialNumber.Text, txB1_decommissionSerialNumber.Text,
                    txB_city.Text, cmB_poligon.Text, txB_company.Text, txB_location.Text, cmB_model.Text, txB_dateTO.Text,
                    txB_price.Text, txB_representative.Text, txB_post.Text, txB_numberIdentification.Text, txB_dateIssue.Text,
                    txB_phoneNumber.Text, txB_antenna.Text, txB_manipulator.Text, txB_AKB.Text, txB_batteryСharger.Text,
                    txB_reason_decommission.Text, cmB_road.Text);
                BtnUpdateClick(sender, e);
                panel_decommissionSerialNumber.Visible = false;
                panel_decommissionSerialNumber.Enabled = false;
                panel1.Enabled = true;
                panel2.Enabled = true;
                panel3.Enabled = true;
                dataGridView1.Enabled = true;
                txB1_decommissionSerialNumber.Text = "";
            }
            else MessageBox.Show("Вы не заполнили поле Номер Акта Списания или поле Причина!");
        }

        #region Удаление списания
        void DeleteRadiostantionDecommissionClick(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = $"Вы действительно хотите удалить списание на данную радиостанцию: {txB_serialNumber.Text}, предприятия: {txB_company.Text}?";
            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            QuerySettingDataBase.DeleteDecommissionSerialNumberRadiostantion(dataGridView2, txB_decommissionSerialNumber.Text,
                txB_serialNumber.Text, txB_city.Text, cmB_model, txB_numberAct, cmB_road.Text);
            BtnUpdateClick(sender, e);
        }
        #endregion

        #region показать списания
        void ShowRadiostantionDecommissionClick(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            panel3.Enabled = false;
            menuStrip1.Enabled = false;
            QuerySettingDataBase.ShowRadiostantionDecommission(dataGridView1, txB_city.Text, cmB_road.Text);
            Counters();
        }

        #endregion

        #region сформировать акт списания
        void PrintWordActDecommission(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
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


        #endregion

        #endregion

        #region показать кол-во уникальных актов
        void CmbSeachSelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавь радиостанцию", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cmB_seach.SelectedIndex == 0)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                textBox_search.Clear();
                if (txb_flag_all_BD.Text == "Вся БД")
                    QuerySettingDataBase.NumberUniqueCompanyFullDataBase(cmb_number_unique_acts, cmB_road.Text);
                else QuerySettingDataBase.NumberUniqueCompany(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 1)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                textBox_search.Clear();

                if (txb_flag_all_BD.Text == "Вся БД")
                    QuerySettingDataBase.NumberUniqueLocationFullDataBase(cmb_number_unique_acts, cmB_road.Text);
                else QuerySettingDataBase.NumberUniqueLocation(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 2)
            {
                cmb_number_unique_acts.Visible = false;
                textBox_search.Visible = true;
            }
            else if (cmB_seach.SelectedIndex == 3)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                textBox_search.Clear();

                if (txb_flag_all_BD.Text == "Вся БД")
                    QuerySettingDataBase.NumberUniqueDateTOFullDataBase(cmb_number_unique_acts, cmB_road.Text);
                else QuerySettingDataBase.NumberUniqueDateTO(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 4)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                textBox_search.Clear();

                if (txb_flag_all_BD.Text == "Вся БД")
                    QuerySettingDataBase.NumberUniqueNumberActFullDataBase(cmb_number_unique_acts, cmB_road.Text);
                else QuerySettingDataBase.NumberUniqueNumberAct(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 5)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                textBox_search.Clear();

                if (txb_flag_all_BD.Text == "Вся БД")
                    QuerySettingDataBase.NumberUniqueNumberActRemontFullDataBase(cmb_number_unique_acts, cmB_road.Text);
                else QuerySettingDataBase.NumberUniqueNumberActRemont(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 6)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                textBox_search.Clear();

                if (txb_flag_all_BD.Text == "Вся БД")
                    QuerySettingDataBase.NumberUniqueRepresentativeFullDataBase(cmb_number_unique_acts, cmB_road.Text);
                else QuerySettingDataBase.NumberUniqueRepresentative(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 7)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                textBox_search.Clear();

                if (txb_flag_all_BD.Text == "Вся БД")
                    QuerySettingDataBase.NumberUniqueDecommissionActsFullDataBase(cmb_number_unique_acts, cmB_road.Text);
                else QuerySettingDataBase.NumberUniqueDecommissionActs(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else if (cmB_seach.SelectedIndex == 8)
            {
                cmb_number_unique_acts.Visible = true;
                textBox_search.Visible = false;
                textBox_search.Clear();
                if (txb_flag_all_BD.Text == "Вся БД")
                    QuerySettingDataBase.NumberUniqueModelFullDataBase(cmb_number_unique_acts, cmB_road.Text);
                else QuerySettingDataBase.NumberUniqueModel(cmB_city.Text, cmb_number_unique_acts, cmB_road.Text);
            }
            else
            {
                cmb_number_unique_acts.Visible = false;
                textBox_search.Visible = true;
            }
            cmb_number_unique_acts.SelectedIndex = 0;
        }
        #endregion

        #region показать РСТ без списаний по участку
        void BtnRefreshDataGridWithoutDecommission(object sender, EventArgs e)
        {
            QuerySettingDataBase.RefreshDataGridWithoutDecommission(dataGridView1, cmB_city.Text, cmB_road.Text);
            Counters();
        }
        #endregion

        #region показать списанные РСТ по участку
        void BtnRefreshDataGridtDecommissionByPlot(object sender, EventArgs e)
        {
            QuerySettingDataBase.RefreshDataGridtDecommissionByPlot(dataGridView1, cmB_city.Text, cmB_road.Text);
            Counters();
        }
        #endregion

        #region Бирка
        void FormTag(object sender, EventArgs e)
        {
            panel_Tag.Visible = true;
            panel_Tag.Enabled = true;
            txB_Date_panel_Tag.Select();
        }
        void TxbDatePnlTagClick(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }
        void MonthCalendar1DateSelected(object sender, DateRangeEventArgs e)
        {
            txB_Date_panel_Tag.Text = e.End.ToString("dd.MM.yyyy");
            monthCalendar1.Visible = false;
        }
        void BtnClosePnlTagClick(object sender, EventArgs e)
        {
            panel_Tag.Visible = false;
        }
        void BtnFormTagClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txB_Date_panel_Tag.Text))
            {
                string cheak = "РСТ";
                string month2;
                DateTime dateTag = Convert.ToDateTime(txB_Date_panel_Tag.Text);
                string mothCheackTag = DateTime.DaysInMonth(dateTag.Year, dateTag.Month).ToString();
                if (dateTag.ToString("dd") == mothCheackTag)
                    month2 = dateTag.AddMonths(1).ToString("MM");
                else month2 = dateTag.ToString("MM");
                string month = dateTag.ToString("MM");
                string day = dateTag.ToString("dd");
                string year = dateTag.ToString("yyyy");
                string day2 = dateTag.AddDays(1).ToString("dd");
                string year2 = dateTag.AddYears(1).ToString("yyyy");
                if (chbManipulator.Checked)
                    cheak = "МАН";
                var items2 = new Dictionary<string, string>
                {
                    {"cheak", cheak },
                    {"day", day },
                    {"month", month },
                    {"month2", month2 },
                    {"year", year },
                    {"day2", day2 },
                    {"year2", year2 },
                    {"Engineer", lbl_EngineerFIO.Text },
                    {"road", cmB_road.Text }

                };
                PrintDocExcel.GetInstance.ProcessPrintWordTag(items2, txB_Date_panel_Tag.Text);
            }
            else MessageBox.Show("Заполни дату!");
        }
        #endregion

        #region Добавление радиостанций в выполнение для куратора
        void AddExecution(object sender, EventArgs e)
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
        #endregion

        #region Поиск по номеру акта из Combobox-ов (на подпись и заполнем до полного акты)
        void CmbAddSignatureSelectionChangeCommitted(object sender, EventArgs e)
        {
            QuerySettingDataBase.SearchNumberActCombobox(dataGridView1, cmB_city.Text, cmB_road.Text, cmB_AddSignature.Text);
            Counters();
        }
        void CmbAddFillFullActTOSelectionChangeCommitted(object sender, EventArgs e)
        {
            QuerySettingDataBase.SearchNumberActCombobox(dataGridView1, cmB_city.Text, cmB_road.Text, cmB_AddFillFullActTO.Text);
            Counters();
        }
        #endregion

        #region MenuTrip
        void MTripPrintStatementParametersClick(object sender, EventArgs e)
        {
            PrintStatementParameters(sender, e);
        }
        void MTripAddRadioStationParametersClick(object sender, EventArgs e)
        {
            AddRadioStationParameters(sender, e);
        }
        void MTripPnlChangeNumberActTOFullClick(object sender, EventArgs e)
        {
            ChangeNumberAct(sender, e);
        }
        void MTripNewAddRadiostantionClick(object sender, EventArgs e)
        {
            BtnNewAddRadiostantionFormClick(sender, e);
        }
        void MTripChangeRadiostantionClick(object sender, EventArgs e)
        {
            BtnChangeRadiostantionFormClick(sender, e);
        }
        void MTripNewAddRadiostantionRemontClick(object sender, EventArgs e)
        {
            BtnNewAddRadiostantionFormClickRemont(sender, e);
        }
        void MTripDeleteRadiostantionClick(object sender, EventArgs e)
        {
            BtnDeleteClick(sender, e);
        }
        void MTripDeleteRadiostantionRemontClick(object sender, EventArgs e)
        {
            DeleteRadiostantionRemontClick(sender, e);
        }
        void MTripDeleteRadiostantionDecommissionClick(object sender, EventArgs e)
        {
            DeleteRadiostantionDecommissionClick(sender, e);
        }
        void MTripRadiostantionDecommissionClick(object sender, EventArgs e)
        {
            DecommissionSerialNumber(sender, e);
        }
        void MTripAddExecutionClick(object sender, EventArgs e)
        {
            AddExecution(sender, e);
        }
        void MTripBtnUpdateClick(object sender, EventArgs e)
        {
            BtnUpdateClick(sender, e);
        }
        void MTripBtnActTOPrintClick(object sender, EventArgs e)
        {
            BtnActTOPrintClick(sender, e);
        }
        void MTripBtnRemontActClick(object sender, EventArgs e)
        {
            BtnRemontActClick(sender, e);
        }
        void MTripPrintWordActDecommissionClick(object sender, EventArgs e)
        {
            PrintWordActDecommission(sender, e);
        }
        void MTripBtnSaveInFileClick(object sender, EventArgs e)
        {
            BtnSaveInFileClick(sender, e);
        }
        void MTripFormTagClick(object sender, EventArgs e)
        {
            FormTag(sender, e);
        }
        void MTripAddFillFullActTOClick(object sender, EventArgs e)
        {
            AddFillFullActTO(sender, e);
        }
        void MTripAddSignatureActTOClick(object sender, EventArgs e)
        {
            AddSignatureActTO(sender, e);
        }
        void MTripBtnRefreshDataGridWithoutDecommissionClick(object sender, EventArgs e)
        {
            BtnRefreshDataGridWithoutDecommission(sender, e);
        }
        void MTripBtnRefreshDataGridtDecommissionByPlotClick(object sender, EventArgs e)
        {
            BtnRefreshDataGridtDecommissionByPlot(sender, e);
        }
        void MTripPictureBoxSeachDatadridReplayClick(object sender, EventArgs e)
        {
            PicbSeachDatadridReplayClick(sender, e);
        }
        void MTripShowRadiostantionDecommissionClick(object sender, EventArgs e)
        {
            ShowRadiostantionDecommissionClick(sender, e);
        }
        void MTripBtnClearDataBaseCurrentYearClick(object sender, EventArgs e)
        {
            ClearDataBaseCurrentYearClick(sender, e);
        }
        void MTripBtnManualBackupCurrentDataBaseClick(object sender, EventArgs e)
        {
            ManualBackupCurrentDataBaseClick(sender, e);
        }
        void MTripBtnLoadingFileCurrentDataBaseClick(object sender, EventArgs e)
        {
            LoadingFileCurrentDataBaseClick(sender, e);
        }
        void MTripBtnCopyingCurrentDateBaseEndOfTheYearClick(object sender, EventArgs e)
        {
            BtnCopyingCurrentDataBaseEndOfTheYearClick(sender, e);
        }
        void MTripBtnLoadingFileLastYearClick(object sender, EventArgs e)
        {
            BtnLoadingFileLastYearClick(sender, e);
        }
        void MTripBtnLoadingFileFullDataBaseClick(object sender, EventArgs e)
        {
            LoadingFileFullDataBaseClick(sender, e);
        }
        void MTripBtnLoadingJsonFileDataBaseClick(object sender, EventArgs e)
        {
            LoadingJsonFileDataBaseClick(sender, e);
        }
        void MTripBtnUploadingJsonFileClick(object sender, EventArgs e)
        {
            BtnUploadingJsonFileClick(sender, e);
        }
        void MTripBtnShowDataBaseRadiostantionLastYearClick(object sender, EventArgs e)
        {
            BtnShowDataBaseRadiostantionLastYearClick(sender, e);
        }
        void MTripBtnShowDataBaseRadiostantionFullClick(object sender, EventArgs e)
        {
            BtnShowDataBaseRadiostantionFullClick(sender, e);
        }
        void MTripPrintReportAKBClick(object sender, EventArgs e)
        {
            PrintReportAKB(sender, e);
        }
        void MTripPrintReportFullAKB_Click(object sender, EventArgs e)
        {
            PrintReportFullAKB(sender, e);
        }

        void MTrip_PrintReportManipulator_Click(object sender, EventArgs e)
        {
            PrintReportManipulator(sender, e);
        }
        void MTripPrintReportFullManipulator_Click(object sender, EventArgs e)
        {
            PrintReportFullManipulator(sender, e);
        }
        #endregion

        #region изменить номер акта у радиостанции
        void BtnClosePnlChangeNumberActTOFullClick(object sender, EventArgs e)
        {
            pnl_ChangeNumberActTOFull.Visible = false;
            dataGridView1.Enabled = true;
            panel1.Enabled = true;
        }
        void ChangeNumberAct(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txB_numberAct.Text))
                return;
            if (dataGridView1.Rows.Count == 0)
                return;
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            if (dataGridView1.SelectedRows.Count > 20)
            {
                string Mesage = $"Вы выбрали более 20 радиостанций. В Акте не должно быть более 20 радиостанций.";
                MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                return;
            }

            if (dataGridView1.SelectedRows.Count > 0)
            {
                string Mesage = $"Вы действительно хотите изменить текущий номер акта {txB_numberAct.Text}?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            txB_pnl_ChangeNumberActTOFull.Text = txB_numberAct.Text;
            pnl_ChangeNumberActTOFull.Visible = true;
            dataGridView1.Enabled = false;
            panel1.Enabled = false;
        }
        void BtnChangePnlNumberActTOFullClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txB_pnl_ChangeNumberActTOFull.Text))
            {
                MessageBox.Show("\"Заводской номер\" не должен быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_pnl_ChangeNumberActTOFull.Select();
                return;
            }
            if (!Regex.IsMatch(txB_numberAct.Text, @"[0-9]{2,2}/([0-9]+([A-Z]?[А-Я]?)*[.\-]?[0-9]?[0-9]?[0-9]?[A-Z]?[А-Я]?)$"))
            {
                MessageBox.Show("Введите корректно \"№ Акта ТО\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_numberAct.Select();
                return;
            }
            QuerySettingDataBase.ChangeNumberAct(dataGridView1, txB_pnl_ChangeNumberActTOFull.Text, cmB_city.Text, cmB_road.Text);
            int currRowIndex = dataGridView1.CurrentCell.RowIndex;
            QuerySettingDataBase.RefreshDataGrid(dataGridView1, cmB_city.Text, cmB_road.Text);
            txB_numberAct.Text = String.Empty;
            dataGridView1.ClearSelection();
            if (dataGridView1.RowCount - currRowIndex > 0)
                dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
            txB_pnl_ChangeNumberActTOFull.Clear();
            Counters();
            BtnClosePnlChangeNumberActTOFullClick(sender, e);
        }



        #endregion

        
    }
}


