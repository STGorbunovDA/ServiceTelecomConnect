﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;


namespace ServiceTelecomConnect
{
    public partial class СhangeRSTForm : Form
    {
        private delegate DialogResult ShowOpenFileDialogInvoker();
        public СhangeRSTForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            monthCalendar1.Visible = false;
            var myCulture = new CultureInfo("ru-RU");
            myCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = myCulture;
            txB_dateTO.ReadOnly = true;
        }
        void ChangeRSTFormLoad(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
            {
                txB_city.Enabled = false;
                cmB_poligon.Enabled = false;
                txB_company.Enabled = false;
                txB_location.Enabled = false;
                cmB_model.Enabled = false;
                txB_serialNumber.Enabled = false;
                txB_inventoryNumber.Enabled = false;
                txB_networkNumber.Enabled = false;
                txB_price.Enabled = false;
                txB_price.Text = "0.00";
                txB_numberAct.Enabled = false;
                chB_numberActTO_Enable.Enabled = false;
                txB_numberAct.Text = "";
                txB_representative.Enabled = false;
                txB_numberIdentification.Enabled = false;
                txB_phoneNumber.Enabled = false;
                txB_post.Enabled = false;
                txB_dateIssue.Enabled = false;
                txB_antenna.Enabled = false;
                txB_manipulator.Enabled = false;
                txB_batteryСharger.Enabled = false;
                txB_AKB.Enabled = false;
                picB_clear.Enabled = false;
                pictureBox6.Enabled = false;
                pictureBox5.Enabled = false;
                btn_identityCard_change_rst_act.Enabled = false;
                btn_identityCard_change_rst_company.Enabled = false;
                chB_analog.Enabled = false;
                chB_digital.Enabled = false;
                lbL_Date.Text = "Дата списания:";
                txB_decommissionSerialNumber.Focus();
            }
            else txB_decommissionSerialNumber.Enabled = false;
            cmB_model.Text = cmB_model.Items[0].ToString();
            if (cmB_model.Text == "Icom IC-F3GT" || cmB_model.Text == "Icom IC-F11" || cmB_model.Text == "Icom IC-F16" ||
               cmB_model.Text == "Icom IC-F3GS" || cmB_model.Text == "Motorola P040" || cmB_model.Text == "Motorola P080" ||
               cmB_model.Text == "Motorola GP-300" || cmB_model.Text == "Motorola GP-320" || cmB_model.Text == "Motorola GP-340" ||
               cmB_model.Text == "Motorola GP-360" || cmB_model.Text == "Альтавия-301М" || cmB_model.Text == "Comrade R5" ||
               cmB_model.Text == "Гранит Р33П-1" || cmB_model.Text == "Гранит Р-43" || cmB_model.Text == "Радий-301" ||
               cmB_model.Text == "Kenwood ТК-2107" || cmB_model.Text == "Vertex - 261" || cmB_model.Text == "РА-160")
            {
                txB_price.Text = "1411.18";
                chB_analog.CheckState = CheckState.Checked;
                chB_digital.CheckState = CheckState.Unchecked;
            }
            else
            {
                txB_price.Text = "1919.57";
                chB_digital.CheckState = CheckState.Checked;
                chB_analog.CheckState = CheckState.Unchecked;
            }
        }
        void CmbModelClick(object sender, EventArgs e)
        {
            QuerySettingDataBase.CmbGettingModelRST(cmB_model);
        }

        #region изменяем рст по номеру акта
        void BtnChangeRadiostantionActClick(object sender, EventArgs e)
        {
            if (CheacSerialNumber.GetInstance.CheackNumberActRadiostantion(lbL_road.Text, txB_city.Text, txB_numberAct.Text))
            {
                MessageBox.Show($"В данном акте: {txB_numberAct.Text} уже есть 20-ать радиостанций", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_numberAct.Select();
                return;
            }

            Regex re = new Regex(Environment.NewLine);
            txB_numberAct.Text = re.Replace(txB_numberAct.Text, " ");
            if (String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
            {
                if (!Regex.IsMatch(txB_numberAct.Text, @"[0-9]{2,2}/([0-9]+([A-Z]?[А-Я]?)*[.\-]?[0-9]?[0-9]?[0-9]?[A-Z]?[А-Я]?)$"))
                {
                    MessageBox.Show("Введите корректно \"№ Акта ТО\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_numberAct.Select();
                    return;
                }
            }
            if (InternetCheck.CheackSkyNET())
            {
                string numberAct = txB_numberAct.Text;
                Regex reg = new Regex(Environment.NewLine);
                numberAct = reg.Replace(numberAct, " ");
                string serialNumber = txB_serialNumber.Text;
                string road = lbL_road.Text;
                string changeQuery = $"UPDATE radiostantion SET numberAct = '{numberAct.Trim()}' WHERE " +
                    $"serialNumber = '{serialNumber.Trim()}' AND road = '{road}'";
                string changeQuery2 = $"UPDATE radiostantion_full SET numberAct = '{numberAct.Trim()}' WHERE " +
                    $"serialNumber = '{serialNumber.Trim()}' AND road = '{road}'";
                using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                }
                using (MySqlCommand command2 = new MySqlCommand(changeQuery2, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command2.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                }
                MessageBox.Show("Акт успешно изменён");
            }
        }

        #endregion

        #region изменяем рст full
        void BtnChangeRadiostantionFullClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
            {
                if (String.IsNullOrWhiteSpace(txB_serialNumber.Text))
                {
                    MessageBox.Show("\"Заводской номер\" не должен быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_serialNumber.Select();
                    return;
                }
            }
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    Regex re = new Regex(Environment.NewLine);
                    control.Text = re.Replace(control.Text, " ");
                    control.Text.Trim();
                }
            }
            ChangeRadiostantion();
        }
        void ChangeRadiostantion()
        {
            if (String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
            {
                if (!Regex.IsMatch(txB_numberAct.Text, @"[0-9]{2,2}/([0-9]+([A-Z]?[А-Я]?)*[.\-]?[0-9]?[0-9]?[0-9]?[A-Z]?[А-Я]?)$"))
                {
                    MessageBox.Show("Введите корректно \"№ Акта ТО\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_numberAct.Select();
                    return;
                }
            }
            if (InternetCheck.CheackSkyNET())
            {
                string decommission = txB_decommissionSerialNumber.Text;
                string city = txB_city.Text;
                if (!Regex.IsMatch(city, @"^[А-Я][а-я]*(?:[\s-][А-Я][а-я]*)*$"))
                {
                    MessageBox.Show("Введите корректно поле \"Город\".\n P.s. название города должно быть с большой буквы.\nпример: \"Нижний-Новгород\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_city.Select();
                    string Mesage = "Вы действительно хотите изменить радиостанцию?";
                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;
                }
                string poligon = cmB_poligon.Text;
                string company = txB_company.Text;
                if (!Regex.IsMatch(company, @"^[А-Я]*([/s-]?[0-9]*)$"))
                {
                    MessageBox.Show("Введите корректно поле \"Предприятие\"\n P.s. В РЖД наименование предприятий с большой буквы\nпример: \"ПЧИССО-2\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_company.Select();
                    string Mesage = "Вы действительно хотите изменить радиостанцию?";
                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;
                }
                string location = txB_location.Text;
                if (!Regex.IsMatch(location, @"^[с][т][.][\s][А-Я][а-я]*(([\s-]?[0-9])*$)?([\s-]?[А-Я][а-я]*)*$"))
                {
                    MessageBox.Show("Введите корректно поле \"Место нахождения\"\n P.s. пример: \"ст. Сейма\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_location.Select();
                    string Mesage = "Вы действительно хотите изменить радиостанцию?";
                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;
                }
                string model = cmB_model.GetItemText(cmB_model.SelectedItem);
                string serialNumber = txB_serialNumber.Text;
                #region
                if (model == "Motorola GP-340")
                {
                    if (!Regex.IsMatch(serialNumber, @"^([6][7][2]([A-Z]{3,3}[0-9]{4,4}))?([6][7][2][A-Z]{4,4}[0-9]{3,3})*$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Motorola GP-340 - \"672TTD0000 или 672TTDE000\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();
                        return;
                    }
                }
                else if (model == "Motorola GP-360")
                {
                    if (!Regex.IsMatch(serialNumber, @"^([7][4][9]([A-Z]{3,3}[0-9]{4,4}))?([7][4][9][A-Z]{4,4}[0-9]{3,3})*$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Motorola GP-360 \"749TTD0000 или 749TTDE000\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();
                        return;
                    }
                }
                else if (model == "Motorola DP-2400е" || model == "Motorola DP-2400")
                {
                    if (!Regex.IsMatch(serialNumber, @"^([4][4][6]([A-Z]{3,3}[0-9]{4,4}))?([4][4][6][A-Z]{4,4}[0-9]{3,3})*$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Motorola DP-2400 - \"446TTD0000 или 446TTDE000\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();
                        return;
                    }
                }
                else if (model == "Comrade R5")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[2][0][1][0][R][5]([0-9]{6,6})$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Comrade R5 - \"2010R5107867\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();
                        return;
                    }
                }
                else if (model == "Icom IC-F3GS")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[5][4]([0-9]{5,5})$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Icom IC-F3GS -\"5468318\r\n\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";

                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "Icom IC-F3GT")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[0][4]([0-9]{5,5})$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Icom IC-F3GT -\"0432600\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "Icom IC-F16")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[0][7]([0-9]{5,5})$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Icom IC-F16 -\"0726630\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";

                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "Icom IC-F11")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[1][0]([0-9]{4,4})$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Icom IC-F11 -\"109025\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "Альтавия-301М")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[0-9]{9,9}$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Альтавия-301М -\"160401173\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "Элодия-351М")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[0-9]{9,9}$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Элодия-351М -\"160403711\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "Комбат T-44")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[T][4][4][.][0-9]{2,2}[.]+[0-9]{2,2}[.][0-9]{4,4}$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Комбат T-44 -\"T44.19.10.0248\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "Шеврон T-44 V2")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[T][4][4][.][0-9]{2,2}[.]+[0-9]{1,2}[.][0-9]{4,4}$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Комбат T-44 -\"T44.20.9.0192\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "РН311М")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[0-9]{1,20}((([\S][0-9])*$)?([\s][0-9]{2,2}[.]?[0-9]{2,2}?)*$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: РН311М -\"0132 09.18\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "Motorola DP-4400")
                {
                    if (!Regex.IsMatch(serialNumber, @"^([8][0][7]([A-Z]{3,3}[0-9]{4,4}))?([8][0][7][A-Z]{4,4}[0-9]{3,3})*$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Motorola DP-4400 - \"807TTD0000 или 807TTDE000\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();
                        return;
                    }
                }
                else if (model == "Motorola DP-1400")
                {
                    if (!Regex.IsMatch(serialNumber, @"^([7][5][2]([A-Z]{3,3}[0-9]{4,4}))?([7][5][2][A-Z]{4,4}[0-9]{3,3})*$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Motorola DP-1400 - \"752TTD0000 или 752TTDE000\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();
                        return;
                    }
                }
                else if (model == "Motorola GP-320")
                {
                    if (!Regex.IsMatch(serialNumber, @"^([0-9]{3,3}([A-Z]{3,3}[0-9]{4,4}))?([0-9]{3,3}[A-Z]{4,4}[0-9]{3,3})*$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Motorola GP-320 - \"000TTD0000 или 000TTDE000\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();
                        return;
                    }
                }
                else if (model == "Motorola GP-300")
                {
                    if (!Regex.IsMatch(serialNumber, @"^([1][7][4]([A-Z]{3,3}[0-9]{4,4}))?([1][7][4][A-Z]{4,4}[0-9]{3,3})*$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Motorola GP-300 - \"174TTD0000 или 174TTDE000\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();
                        return;
                    }
                }
                else if (model == "Motorola P080")
                {
                    if (!Regex.IsMatch(serialNumber, @"^([4][2][2]([A-Z]{3,3}[0-9]{4,4}))?([4][2][2][A-Z]{4,4}[0-9]{3,3})*$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Motorola P080 - \"452TTD0000 или 452TTDE000\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();
                        return;
                    }
                }
                else if (model == "Motorola P040")
                {
                    if (!Regex.IsMatch(serialNumber, @"^([4][2][2]([A-Z]{3,3}[0-9]{4,4}))?([4][2][2][A-Z]{4,4}[0-9]{3,3})*$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Motorola P040 - \"452TTD0000 или 452TTDE000\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();
                        return;
                    }
                }
                else if (model == "Гранит Р33П-1")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[0-9]{2,2}[\s][0-9]{5,5}$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Гранит Р33П-1 - \"03 29121\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();
                        string Mesage = "Вы действительно хотите добавить радиостанцию?";

                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "Гранит Р-43")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[0-9]{2,2}[\s][0-9]{6,6}$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Гранит Р-43 - \"01 195580\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "Радий-301")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[0-9]{6,6}$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: Радий-301 - \"425266\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "РНД-500")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[0-9]{1,}[[\s]?[0-9]{2,}[\.]?[0-9]{2,}$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: РНД-500 - \"03169 10.20\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                else if (model == "РНД-512")
                {
                    if (!Regex.IsMatch(serialNumber, @"^[0-9]{1,}[[\s]?[0-9]{2,}[\.]?[0-9]{2,}$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Заводской номер\"\n P.s. пример: РНД-512 - \"03169 10.20\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_serialNumber.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                #endregion
                string inventoryNumber = txB_inventoryNumber.Text;
                if (inventoryNumber.Contains("\\"))
                    inventoryNumber = inventoryNumber.Replace("\\", "\\\\");
                if (String.IsNullOrWhiteSpace(decommission))
                {
                    if (!Regex.IsMatch(inventoryNumber, @"^[0-9]{1,}([\-]*[\/]*[\\]*[0-9]*[\\]*[\/]*[0-9]*[\/]*[0-9]*[\*]*[\-]*[0-9]*[\/]*[0-9]*)$"))
                    {
                        MessageBox.Show("Введите корректно поле: \"Инвентарный номер\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_inventoryNumber.Select();
                        string Mesage = "Вы действительно хотите продолжить?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                string networkNumber = txB_networkNumber.Text;
                if (networkNumber.Contains("\\"))
                    networkNumber = networkNumber.Replace("\\", "\\\\");
                if (String.IsNullOrWhiteSpace(decommission))
                {
                    if (!Regex.IsMatch(networkNumber, @"^[0-9]{1,}([\-]*[\/]*[\\]*[0-9]*[\\]*[\/]*[0-9]*[\/]*[0-9]*[\*]*[\-]*[0-9]*[\/]*[0-9]*)$"))
                    {
                        MessageBox.Show("Введите корректно поле: \"Сетевой номер\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_networkNumber.Select();
                        string Mesage = "Вы действительно хотите продолжить?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                string dateTO = Convert.ToDateTime(txB_dateTO.Text).ToString("yyyy-MM-dd");
                if (String.IsNullOrWhiteSpace(dateTO))
                {
                    MessageBox.Show("Поле \"№ Дата ТО\" не должно быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_dateTO.Select();
                    return;
                }
                string price = txB_price.Text;
                if (String.IsNullOrWhiteSpace(price))
                {
                    MessageBox.Show("Поле \"№ Цена ТО\" не должно быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_price.Select();
                    return;
                }
                string representative = txB_representative.Text;
                if (!representative.Contains("-"))
                {
                    if (!Regex.IsMatch(representative, @"^[А-ЯЁ][а-яё]*(([\s]+[А-Я][\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"Представитель ФИО\"\nP.s. пример: Иванов В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_representative.Select();
                        return;
                    }
                }
                if (representative.Contains("-"))
                {
                    if (!Regex.IsMatch(representative, @"^[А-ЯЁ][а-яё]*(([\-][А-Я][а-яё]*[\s]+[А-Я]+[\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"Представитель ФИО\"\nP.s. пример: Иванов-Петров В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_representative.Select();
                        return;
                    }
                }
                string post = txB_post.Text;
                if (String.IsNullOrWhiteSpace(post))
                {
                    MessageBox.Show("Поле \"№ Должность\" не должно быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_post.Select();
                    return;
                }
                string numberIdentification = txB_numberIdentification.Text;
                if (!Regex.IsMatch(numberIdentification, @"^[V][\s]([0-9]{6,})$"))
                {
                    MessageBox.Show("Введите корректно поле \"Номер удостоверения\"\nP.s. пример: V 149062", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_numberIdentification.Select();
                    string Mesage = "Вы действительно хотите продолжить?";
                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;
                }
                string dateIssue = txB_dateIssue.Text;
                if (String.IsNullOrWhiteSpace(dateIssue))
                {
                    MessageBox.Show("Поле \"№ Дата выдачи\" не должно быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_dateIssue.Select();
                    return;
                }
                string phoneNumber = txB_phoneNumber.Text;
                if (String.IsNullOrWhiteSpace(decommission))
                {
                    if (!Regex.IsMatch(phoneNumber, @"^[+][7][9][0-9]{9,9}$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Номер телефона\"\nP.s. пример: +79246291675", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_phoneNumber.Select();
                        string Mesage = "Вы действительно хотите продолжить?";
                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (!String.IsNullOrWhiteSpace(decommission))
                {
                    if (!Regex.IsMatch(decommission, @"^[0-9]{2,2}/([0-9]+([A-Z]?[А-Я]?)*[.\-]?[0-9]?[0-9]?[0-9]?[A-Z]?[А-Я]?[СC]{1,1}([.\-][0-9]+)?)$"))
                    {
                        MessageBox.Show("Введите корректно \"№ Акта списания\"\nP.s. 53/778C или 53/778C-1", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_decommissionSerialNumber.Select();
                        return;
                    }
                }
                string antenna = txB_antenna.Text;
                string manipulator = txB_manipulator.Text;
                string AKB = txB_AKB.Text;
                string batteryСharger = txB_batteryСharger.Text;
                string comment = txB_comment.Text;
                string road = lbL_road.Text;
                if (!String.IsNullOrWhiteSpace(poligon) && !String.IsNullOrWhiteSpace(company) && !String.IsNullOrWhiteSpace(location)
                    && !String.IsNullOrWhiteSpace(model) && !String.IsNullOrWhiteSpace(serialNumber) && !String.IsNullOrWhiteSpace(dateTO)
                    && !String.IsNullOrWhiteSpace(city) && !String.IsNullOrWhiteSpace(representative) && !String.IsNullOrWhiteSpace(post)
                    && !String.IsNullOrWhiteSpace(numberIdentification) && !String.IsNullOrWhiteSpace(dateIssue)
                    && !String.IsNullOrWhiteSpace(phoneNumber) && !String.IsNullOrWhiteSpace(antenna) && !String.IsNullOrWhiteSpace(manipulator)
                    && !String.IsNullOrWhiteSpace(AKB) && !String.IsNullOrWhiteSpace(batteryСharger))
                {

                    string changeQuery = $"UPDATE radiostantion SET city = '{city}', poligon = '{poligon}', company = '{company}', " +
                         $"location = '{location}', model = '{model}', inventoryNumber = '{inventoryNumber}', " +
                         $"networkNumber = '{networkNumber}', dateTO = '{dateTO}', " +
                         $"price = '{Convert.ToDecimal(price)}', representative = '{representative}', " +
                         $"numberIdentification = '{numberIdentification}', dateIssue = '{dateIssue}', " +
                         $"phoneNumber = '{phoneNumber}', post = '{post}', antenna = '{antenna}', manipulator = '{manipulator}', AKB = '{AKB}', " +
                         $"batteryСharger = '{batteryСharger}', decommissionSerialNumber ='{decommission}', comment = '{comment}' " +
                         $"WHERE serialNumber = '{serialNumber}' AND road = '{road}'";

                    string changeQuery2 = $"UPDATE radiostantion_full SET city = '{city}', poligon = '{poligon}', company = '{company}', " +
                        $"location = '{location}', model = '{model}', inventoryNumber = '{inventoryNumber}', " +
                        $"networkNumber = '{networkNumber}', dateTO = '{dateTO}', " +
                        $"price = '{Convert.ToDecimal(price)}', representative = '{representative}', " +
                        $"numberIdentification = '{numberIdentification}', dateIssue = '{dateIssue}', " +
                        $"phoneNumber = '{phoneNumber}', post = '{post}', antenna = '{antenna}', manipulator = '{manipulator}', AKB = '{AKB}', " +
                        $"batteryСharger = '{batteryСharger}', decommissionSerialNumber ='{decommission}', comment = '{comment}' " +
                        $"WHERE serialNumber = '{serialNumber}' AND road = '{road}'";

                    using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        DB.GetInstance.CloseConnection();
                    }
                    using (MySqlCommand command2 = new MySqlCommand(changeQuery2, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        command2.ExecuteNonQuery();
                        DB.GetInstance.CloseConnection();
                    }
                    MessageBox.Show("Радиостанция успешно изменена!");
                }
                else MessageBox.Show("Вы не заполнили нужные поля со (*)!");
            }
        }
        #endregion

        #region Очищаем Conrol-ы
        void ClearControlForm(object sender, EventArgs e)
        {
            string Mesage = "Вы действительно хотите очистить все введенные вами поля?";
            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            foreach (Control control in this.Controls)
                if (control is TextBox)
                    control.Text = String.Empty;

            txB_antenna.Text = "-";
            txB_manipulator.Text = "-";
            txB_AKB.Text = "-";
            txB_batteryСharger.Text = "-";
        }
        #endregion

        #region Показываем календарь у даты ТО
        void TextBox_dateTO_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }
        void MonthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            txB_dateTO.Text = e.End.ToString("dd.MM.yyyy");
            monthCalendar1.Visible = false;
        }
        void TextBox_dateIssue_Click(object sender, EventArgs e)
        {
            monthCalendar2.Visible = true;
        }
        void MonthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            txB_dateIssue.Text = e.End.ToString("dd.MM.yyyy");
            monthCalendar2.Visible = false;
        }
        #endregion

        #region Очищаем дату выдачи удостоверения и дату ТО
        void ClearControlDataIssue(object sender, EventArgs e)
        {
            txB_dateIssue.Text = String.Empty;
        }
        void ClearControlDateTO(object sender, EventArgs e)
        {
            txB_dateTO.Text = String.Empty;
        }
        #endregion

        #region KeyUp KeyPress для Control-ов
        void TxbPriceKeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            char decimalSeparatorChar = Convert.ToChar(Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
            if (ch == decimalSeparatorChar && txB_price.Text.IndexOf(decimalSeparatorChar) != -1)
            {
                e.Handled = true;
                return;
            }
            if (!Char.IsDigit(ch) && ch != 8 && ch != decimalSeparatorChar)
                e.Handled = true;
        }
        void CmbModelSelectionChangeCommitted(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txB_decommissionSerialNumber.Text))
            {
                if (cmB_model.Text == "Icom IC-F3GT" || cmB_model.Text == "Icom IC-F11" || cmB_model.Text == "Icom IC-F16" ||
                cmB_model.Text == "Icom IC-F3GS" || cmB_model.Text == "Motorola P040" || cmB_model.Text == "Motorola P080" ||
                cmB_model.Text == "Motorola GP-300" || cmB_model.Text == "Motorola GP-320" || cmB_model.Text == "Motorola GP-340" ||
                cmB_model.Text == "Motorola GP-360" || cmB_model.Text == "Альтавия-301М" || cmB_model.Text == "Comrade R5" ||
                cmB_model.Text == "Гранит Р33П-1" || cmB_model.Text == "Гранит Р-43" || cmB_model.Text == "Радий-301" ||
                cmB_model.Text == "Kenwood ТК-2107" || cmB_model.Text == "Vertex - 261" || cmB_model.Text == "РА-160")
                {
                    txB_price.Text = "1411.18";
                    chB_analog.CheckState = CheckState.Checked;
                    chB_digital.CheckState = CheckState.Unchecked;
                }
                else
                {
                    txB_price.Text = "1919.57";
                    chB_digital.CheckState = CheckState.Checked;
                    chB_analog.CheckState = CheckState.Unchecked;
                }
            }
            else txB_price.Text = "0.00";
        }
        void TxbLocationClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txB_location.Text))
                txB_location.Text = $"ст. {txB_city.Text}";
        }
        void TxbSerialNumberKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
            {
                if (!String.IsNullOrWhiteSpace(txB_serialNumber.Text))
                {
                    string serialNumber = txB_serialNumber.Text;
                    string querystring = $"SELECT * FROM radiostantion_full WHERE serialNumber = '{serialNumber}'";
                    MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        cmB_poligon.Text = table.Rows[0].ItemArray[1].ToString();
                        txB_company.Text = table.Rows[0].ItemArray[2].ToString();
                        txB_location.Text = table.Rows[0].ItemArray[3].ToString();
                        cmB_model.Text = table.Rows[0].ItemArray[4].ToString();
                        txB_inventoryNumber.Text = table.Rows[0].ItemArray[6].ToString();
                        txB_networkNumber.Text = table.Rows[0].ItemArray[7].ToString();
                        txB_numberAct.Text = table.Rows[0].ItemArray[9].ToString();
                        txB_city.Text = table.Rows[0].ItemArray[10].ToString();
                        txB_representative.Text = table.Rows[0].ItemArray[12].ToString();
                        txB_post.Text = table.Rows[0].ItemArray[13].ToString();
                        txB_numberIdentification.Text = table.Rows[0].ItemArray[14].ToString();
                        txB_dateIssue.Text = table.Rows[0].ItemArray[15].ToString();
                        txB_phoneNumber.Text = table.Rows[0].ItemArray[16].ToString();
                    }
                }
            }

            if (e.KeyCode == Keys.Return)
            {
                if (!String.IsNullOrWhiteSpace(txB_serialNumber.Text))
                {
                    string serialNumber = txB_serialNumber.Text;

                    string querystring = $"SELECT * FROM radiostantion_full WHERE serialNumber = '{serialNumber}'";

                    MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        cmB_poligon.Text = table.Rows[0].ItemArray[1].ToString();
                        txB_company.Text = table.Rows[0].ItemArray[2].ToString();
                        txB_location.Text = table.Rows[0].ItemArray[3].ToString();
                        cmB_model.Text = table.Rows[0].ItemArray[4].ToString();
                        txB_inventoryNumber.Text = table.Rows[0].ItemArray[6].ToString();
                        txB_networkNumber.Text = table.Rows[0].ItemArray[7].ToString();
                        txB_city.Text = table.Rows[0].ItemArray[10].ToString();
                    }
                    else
                    {
                        txB_inventoryNumber.Text = "";
                        txB_networkNumber.Text = "";
                    }
                }
            }
        }
        void TxbSerialNumberKeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TxbSerialNumberKeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmB_model.Text == "Icom IC-F3GT" || cmB_model.Text == "Icom IC-F11" || cmB_model.Text == "Icom IC-F16"
                || cmB_model.Text == "Icom IC-F3GS" || cmB_model.Text == "Альтавия-301М" || cmB_model.Text == "Элодия-351М"
                || cmB_model.Text == "Гранит Р33П-1" || cmB_model.Text == "Гранит Р-43" || cmB_model.Text == "Радий-301"
                || cmB_model.Text == "РНД-500")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
                {

                }
                else e.Handled = true;
            }

            if (cmB_model.Text == "Motorola P040" || cmB_model.Text == "Motorola P080" || cmB_model.Text == "Motorola DP-1400" ||
                cmB_model.Text == "Motorola DP-2400" || cmB_model.Text == "Motorola DP-2400е" || cmB_model.Text == "Motorola DP-4400" ||
                cmB_model.Text == "Motorola GP-300" || cmB_model.Text == "Motorola GP-320" || cmB_model.Text == "Motorola GP-340" ||
                cmB_model.Text == "Motorola GP-360" || cmB_model.Text == "Comrade R5")
            {
                if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
                {

                }
                else e.Handled = true;
            }

            if (cmB_model.Text == "РН311М" || cmB_model.Text == "РНД-512")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == ' ')
                {

                }
                else e.Handled = true;
            }

            if (cmB_model.Text == "Комбат T-44")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == 84)
                {

                }
                else e.Handled = true;
            }
        }
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
        void TxbAntennaClick(object sender, EventArgs e)
        {
            txB_antenna.Text = String.Empty;
        }
        void TxbAntennaKeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TxbAntennaKeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != '\b' && ch != '-' && ch != '1')
                e.Handled = true;
        }
        void TxbAntennaLeave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txB_antenna.Text))
                txB_antenna.Text = "-";
        }
        void TxbManipulatorClick(object sender, EventArgs e)
        {
            txB_manipulator.Text = String.Empty;
        }
        void TxbManipulatorKeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TxbManipulatorKeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != '\b' && ch != '-' && ch != '1')
                e.Handled = true;
        }
        void TxbManipulatorLeave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txB_manipulator.Text))
                txB_manipulator.Text = "-";
        }
        void TxbAKBLeave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txB_AKB.Text))
                txB_AKB.Text = "-";
        }
        void TxbBatteryСhargerClick(object sender, EventArgs e)
        {
            txB_batteryСharger.Text = String.Empty;
        }
        void TxbBatteryСhargerKeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TxbBatteryСhargerKeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != '\b' && ch != '-' && ch != '1')
                e.Handled = true;
        }
        void TxbBatteryСhargerLeave(object sender, EventArgs e)
        {
            if (txB_batteryСharger.Text == String.Empty)
                txB_batteryСharger.Text = "-";
        }
        void TxbDateIssueKeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TxbDateIssueKeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }
        void TxbAKBClick(object sender, EventArgs e)
        {
            if (txB_AKB.Text == "-")
                txB_AKB.Text = String.Empty;
        }
        #endregion

        #region смена удостоврения сразу у всех рст по номеру акта или по пп
        void BtnIdentityCardChangeRadiostantionActClick(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    Regex re = new Regex(Environment.NewLine);
                    control.Text = re.Replace(control.Text, " ");
                    control.Text.Trim();
                }
            }
            if (MessageBox.Show("Вы действительно хотите сменить удостоверение представителя у всего акта?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            string representative = txB_representative.Text;
            if (!representative.Contains("-"))
            {
                if (!Regex.IsMatch(representative, @"^[А-Я][а-яё]*(([\s]+[А-Я][\.]+[А-Я]+[\.])$)"))
                {
                    MessageBox.Show("Введите корректно поле \"Представитель ФИО\"\nP.s. пример: Иванов В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_representative.Select();
                    return;
                }
                else if (representative.Contains("-"))
                {
                    if (!Regex.IsMatch(representative, @"^[А-Я][а-яё]*(([\-][А-Я][а-яё]*[\s]+[А-Я]+[\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"Представитель ФИО\"\nP.s. пример: Иванов-Петров В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_representative.Select();
                        return;
                    }
                }
            }
            string post = txB_post.Text;
            if (String.IsNullOrWhiteSpace(post))
            {
                MessageBox.Show("Поле \"№ Должность\" не должно быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_post.Select();
                return;
            }
            string numberIdentification = txB_numberIdentification.Text;
            if (!Regex.IsMatch(numberIdentification, @"^[V][\s]([0-9]{6,})$"))
            {
                MessageBox.Show("Введите корректно поле \"Номер удостоверения\"\nP.s. пример: V 149062", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_numberIdentification.Select();
                string Mesage = "Вы действительно хотите продолжить?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            string dateIssue = txB_dateIssue.Text;
            if (String.IsNullOrWhiteSpace(dateIssue))
            {
                MessageBox.Show("Поле \"№ Дата выдачи\" не должно быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_dateIssue.Select();
                return;
            }
            string phoneNumber = txB_phoneNumber.Text;
            if (!Regex.IsMatch(phoneNumber, @"^[+][7][9][0-9]{9,9}$"))
            {
                MessageBox.Show("Введите корректно поле \"Номер телефона\"\nP.s. пример: +79246291675", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_phoneNumber.Select();
                string Mesage = "Вы действительно хотите продолжить?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            if (!Regex.IsMatch(txB_numberAct.Text, @"[0-9]{2,2}/([0-9]+([A-Z]?[А-Я]?)*[.\-]?[0-9]?[0-9]?[0-9]?[A-Z]?[А-Я]?)$"))
            {
                MessageBox.Show("Введите корректно \"№ Акта ТО\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_numberAct.Select();
                return;
            }
            string road = lbL_road.Text;
            string city = txB_city.Text;
            string numberAct = txB_numberAct.Text;
            if (CheacSerialNumber.GetInstance.CheackNumberActRadiostantionChangeForm2(road, city, numberAct))
            {
                string queryUpdateClient = $"UPDATE radiostantion SET representative = '{representative}', post = '{post}', " +
                $"numberIdentification = '{numberIdentification}', dateIssue = '{dateIssue}',  phoneNumber = '{phoneNumber}' " +
                $"WHERE numberAct = '{numberAct}' AND road = '{road}'";

                using (MySqlCommand command = new MySqlCommand(queryUpdateClient, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                    MessageBox.Show($"Всё данные удостоверния по номеру акта изменены", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else MessageBox.Show("В БД нет акта по которому вы хотите поменять удостоверение представителя");
        }
        void BtnIdentityCardChangeRadiostantionCompanyClick(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    Regex re = new Regex(Environment.NewLine);
                    control.Text = re.Replace(control.Text, " ");
                    control.Text.Trim();
                }
            }

            if (MessageBox.Show("Вы действительно хотите сменить удостоверение представителя у всего предприятия?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            string representative = txB_representative.Text;
            if (!representative.Contains("-"))
            {
                if (!Regex.IsMatch(representative, @"^[А-Я][а-яё]*(([\s]+[А-Я][\.]+[А-Я]+[\.])$)"))
                {
                    MessageBox.Show("Введите корректно поле \"Представитель ФИО\"\nP.s. пример: Иванов В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_representative.Select();
                    return;
                }
                else if (representative.Contains("-"))
                {
                    if (!Regex.IsMatch(representative, @"^[А-Я][а-яё]*(([\-][А-Я][а-яё]*[\s]+[А-Я]+[\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"Представитель ФИО\"\nP.s. пример: Иванов-Петров В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_representative.Select();
                        return;
                    }
                }
            }
            string post = txB_post.Text;
            if (String.IsNullOrWhiteSpace(post))
            {
                MessageBox.Show("Поле \"№ Должность\" не должно быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_post.Select();
                return;
            }
            string numberIdentification = txB_numberIdentification.Text;
            if (!Regex.IsMatch(numberIdentification, @"^[V][\s]([0-9]{6,})$"))
            {
                MessageBox.Show("Введите корректно поле \"Номер удостоверения\"\nP.s. пример: V 149062", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_numberIdentification.Select();
                string Mesage = "Вы действительно хотите продолжить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            string dateIssue = txB_dateIssue.Text;
            if (String.IsNullOrWhiteSpace(dateIssue))
            {
                MessageBox.Show("Поле \"№ Дата выдачи\" не должно быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_dateIssue.Select();
                return;
            }
            string phoneNumber = txB_phoneNumber.Text;
            if (!Regex.IsMatch(phoneNumber, @"^[+][7][9][0-9]{9,9}$"))
            {
                MessageBox.Show("Введите корректно поле \"Номер телефона\"\nP.s. пример: +79246291675", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_phoneNumber.Select();
                string Mesage = "Вы действительно хотите продолжить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            string company = txB_company.Text;
            if (!Regex.IsMatch(company, @"^[А-Я]*([/s-]?[0-9]*)$"))
            {
                MessageBox.Show("Введите корректно поле \"Предприятие\"\n P.s. В РЖД наименование предприятий с большой буквы\nпример: \"ПЧИССО-2\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_company.Select();
                string Mesage = "Вы действительно хотите добавить радиостанцию?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            string road = lbL_road.Text;
            string queryUpdateClient = $"UPDATE radiostantion SET representative = '{representative}', post = '{post}', " +
                $"numberIdentification = '{numberIdentification}', dateIssue = '{dateIssue}',  phoneNumber = '{phoneNumber}'" +
                $"WHERE company = '{company}' AND road = '{road}'";

            using (MySqlCommand command = new MySqlCommand(queryUpdateClient, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                command.ExecuteNonQuery();
                DB.GetInstance.CloseConnection();
                MessageBox.Show($"Всё данные удостоверния по предприятию изменены ", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        void ChbNumberActTOEnableClick(object sender, EventArgs e)
        {
            if (chB_numberActTO_Enable.Checked)
            {
                txB_numberAct.Enabled = true;
                btn_change_rst_act.Enabled = true;
                btn_change_rst_full.Enabled = false;
            }
            else if (!chB_numberActTO_Enable.Checked)
            {
                txB_numberAct.Enabled = false;
                btn_change_rst_act.Enabled = false;
                btn_change_rst_full.Enabled = true;
            }
        }
        void ChbAnalogClick(object sender, EventArgs e)
        {
            chB_digital.CheckState = CheckState.Unchecked;
            txB_price.Text = "1411.18";
            if (chB_analog.CheckState == CheckState.Unchecked)
            {
                chB_digital.CheckState = CheckState.Checked;
                txB_price.Text = "1919.57";
            }
        }
        void ChbDigitalClick(object sender, EventArgs e)
        {
            chB_analog.CheckState = CheckState.Unchecked;
            txB_price.Text = "1919.57";
            if (chB_digital.CheckState == CheckState.Unchecked)
            {
                chB_analog.CheckState = CheckState.Checked;
                txB_price.Text = "1411.18";
            }
        }
    }
}

