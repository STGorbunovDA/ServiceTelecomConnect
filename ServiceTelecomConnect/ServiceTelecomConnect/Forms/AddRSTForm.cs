﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ServiceTelecomConnect
{
    public partial class AddRSTForm : Form
    {
        private delegate DialogResult ShowOpenFileDialogInvoker();
        public AddRSTForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            monthCalendar1.Visible = false;
            var myCulture = new CultureInfo("ru-RU");
            myCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = myCulture;
            txB_dateTO.ReadOnly = true;
            txB_dateTO.Text = DateTime.Now.ToString("dd.MM.yyyy");

            txB_dateIssue.Text = DateTime.Now.ToString("dd.MM.yyyy");
            cmB_poligon.Text = cmB_poligon.Items[0].ToString();
        }

        void AddRSTForm_Load(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    string querystring = $"SELECT id, model_radiostation_name FROM model_radiostation";
                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        DataTable model_RSR_table = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(model_RSR_table);

                            cmB_model.DataSource = model_RSR_table;
                            cmB_model.ValueMember = "id";
                            cmB_model.DisplayMember = "model_radiostation_name";

                            DB.GetInstance.CloseConnection();
                        }
                    }
                    QuerySettingDataBase.LoadingLastNumberActTO(lbL_last_act, lbL_cmb_city_ST_WorkForm.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка модель не добавленна в comboBox (AddRSTForm_Load)");
                }
            }

        }
        #region добавление РСТ
        void Button_save_add_rst_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txB_serialNumber.Text))
            {
                MessageBox.Show("\"Заводской номер\" не должен быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_serialNumber.Select();
                return;
            }
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    var re = new Regex(Environment.NewLine);
                    control.Text = re.Replace(control.Text, " ");
                    control.Text.Trim();
                }
            }
            Add_rst_radiostantion();
        }

        void Add_rst_radiostantion()
        {
            if (!Regex.IsMatch(txB_numberAct.Text, @"[0-9]{2,2}/([0-9]+([A-Z]?[А-Я]?)*[.\-]?[0-9]?[0-9]?[0-9]?[A-Z]?[А-Я]?)$"))
            {
                MessageBox.Show("Введите корректно \"№ Акта ТО\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_numberAct.Select();
                return;
            }

            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    var city = txB_city.Text;

                    if (!Regex.IsMatch(city, @"^[А-Я][а-я]*(?:[\s-][А-Я][а-я]*)*$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Город\".\n P.s. название города должно быть с большой буквы.\nпример: \"Нижний-Новгород\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_city.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";

                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    var poligon = cmB_poligon.Text;
                    var company = txB_company.Text;

                    if (!Regex.IsMatch(company, @"^[А-Я]*([/s-]?[0-9]*)$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Предприятие\"\n P.s. В РЖД наименование предприятий с большой буквы\nпример: \"ПЧИССО-2\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_company.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";

                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    var location = txB_location.Text;
                    if (!Regex.IsMatch(location, @"^[с][т][.][\s][А-Я][а-я]*(([\s-]?[0-9])*$)?([\s-]?[А-Я][а-я]*)*$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Место нахождения\"\n P.s. пример: \"ст. Сейма\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_location.Select();

                        string Mesage = "Вы действительно хотите добавить радиостанцию?";

                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    var model = cmB_model.GetItemText(cmB_model.SelectedItem);

                    var serialNumber = txB_serialNumber.Text;
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
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
                            {
                                return;
                            }
                        }
                    }
                    #endregion

                    var inventoryNumber = txB_inventoryNumber.Text;

                    if (!Regex.IsMatch(inventoryNumber, @"^[0-9]{1,}([\-]*[\/]*[\\]*[0-9]*[\\]*[\/]*[0-9]*[\/]*[0-9]*[\*]*[\-]*[0-9]*[\/]*[0-9]*)$"))
                    {
                        MessageBox.Show("Введите корректно поле: \"Инвентарный номер\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_inventoryNumber.Select();

                        string Mesage = "Вы действительно хотите продолжить?";

                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    var networkNumber = txB_networkNumber.Text;

                    if (!Regex.IsMatch(networkNumber, @"^[0-9]{1,}([\-]*[\/]*[\\]*[0-9]*[\\]*[\/]*[0-9]*[\/]*[0-9]*[\*]*[\-]*[0-9]*[\/]*[0-9]*)$"))
                    {
                        MessageBox.Show("Введите корректно поле: \"Сетевой номер\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_networkNumber.Select();

                        string Mesage = "Вы действительно хотите продолжить?";

                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    var numberAct = txB_numberAct.Text;
                    var dateTO = txB_dateTO.Text;
                    if (String.IsNullOrEmpty(dateTO))
                    {
                        MessageBox.Show("Поле \"№ Дата ТО\" не должно быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_dateTO.Select();
                        return;
                    }
                    var price = txB_price.Text;
                    if (String.IsNullOrEmpty(price))
                    {
                        MessageBox.Show("Поле \"№ Цена ТО\" не должно быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_price.Select();
                        return;
                    }
                    var representative = txB_representative.Text;

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

                    var post = txB_post.Text;
                    if (String.IsNullOrEmpty(post))
                    {
                        MessageBox.Show("Поле \"№ Должность\" не должно быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_post.Select();
                        return;
                    }
                    var numberIdentification = txB_numberIdentification.Text;

                    if (!Regex.IsMatch(numberIdentification, @"^[V][\s]([0-9]{6,})$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Номер удостоверения\"\nP.s. пример: V 149062", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_numberIdentification.Select();
                        string Mesage = "Вы действительно хотите продолжить?";

                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    var dateIssue = txB_dateIssue.Text;
                    if (String.IsNullOrEmpty(dateIssue))
                    {
                        MessageBox.Show("Поле \"№ Дата выдачи\" не должно быть пустым", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_dateIssue.Select();
                        return;
                    }

                    var phoneNumber = txB_phoneNumber.Text;
                    if (!Regex.IsMatch(phoneNumber, @"^[+][7][9][0-9]{9,9}$"))
                    {
                        MessageBox.Show("Введите корректно поле \"Номер телефона\"\nP.s. пример: +79246291675", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_phoneNumber.Select();
                        string Mesage = "Вы действительно хотите продолжить?";

                        if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    var antenna = txB_antenna.Text;
                    var manipulator = txB_manipulator.Text;
                    var AKB = txB_AKB.Text;
                    var batteryСharger = txB_batteryСharger.Text;
                    var comment = txB_comment.Text;
                    var road = lbL_road.Text;

                    //var x = DateTime.Parse(dateIssue).ToString("dd.MM.yyyy");

                    if (!(poligon == "") && !(company == "") && !(location == "") && !(model == "")
                    && !(serialNumber == "") && !(dateTO == "") && !(numberAct == "") && !(city == "")
                    && !(representative == "") && !(post == "") && !(numberIdentification == "")
                    && !(phoneNumber == "") && !(antenna == "")
                    && !(manipulator == "") && !(AKB == "") && !(batteryСharger == ""))
                    {
                        if (!CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantion(serialNumber))
                        {
                            if (!CheacSerialNumber.GetInstance.CheackNumberAct_radiostantion(numberAct))
                            {
                                var addQuery = $"INSERT INTO radiostantion (poligon, company, location, model, serialNumber," +
                                    $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, representative, " +
                                    $"post, numberIdentification, dateIssue, phoneNumber, numberActRemont, category, priceRemont, " +
                                    $"antenna, manipulator, AKB, batteryСharger, completed_works_1, completed_works_2, completed_works_3, " +
                                    $"completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1, parts_2, parts_3, parts_4, " +
                                    $"parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road) VALUES ('{poligon}', '{company}', '{location}'," +
                                    $"'{model}','{serialNumber}', '{inventoryNumber}', '{networkNumber}', " +
                                    $"'{dateTO}','{numberAct}','{city}','{price}', '{representative}', '{post}', " +
                                    $"'{numberIdentification}', '{dateIssue}', '{phoneNumber}', '{""}', '{""}', '{0.00}'," +
                                    $"'{antenna}', '{manipulator}', '{AKB}', '{batteryСharger}', '{""}', '{""}', " +
                                    $"'{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{comment}', '{road}')";


                                using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                                {
                                    DB.GetInstance.OpenConnection();
                                    command.ExecuteNonQuery();
                                    DB.GetInstance.CloseConnection();
                                    MessageBox.Show("Радиостанция успешно добавлена!");
                                    txB_serialNumber.Text = "";
                                    txB_inventoryNumber.Text = "";
                                    txB_networkNumber.Text = "";
                                }

                                try
                                {
                                    if (CheacSerialNumber_radiostantion_full(serialNumber) == false)
                                    {
                                        var addQuery2 = $"INSERT INTO radiostantion_full (poligon, company, location, model, serialNumber," +
                                                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, representative, " +
                                                        $"post, numberIdentification, dateIssue, phoneNumber, numberActRemont, category, priceRemont, " +
                                                        $"antenna, manipulator, AKB, batteryСharger, completed_works_1, completed_works_2, completed_works_3, " +
                                                        $"completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1, parts_2, parts_3, parts_4, " +
                                                        $"parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road) VALUES ('{poligon.Trim()}', '{company.Trim()}', '{location.Trim()}'," +
                                                        $"'{model.Trim()}','{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}', " +
                                                        $"'{dateTO.Trim()}','{numberAct.Trim()}','{city.Trim()}','{price.Trim()}', '{representative.Trim()}', '{post.Trim()}', " +
                                                        $"'{numberIdentification.Trim()}', '{dateIssue.Trim()}', '{phoneNumber.Trim()}', '{""}', '{""}', '{0.00}'," +
                                                        $"'{antenna.Trim()}', '{manipulator.Trim()}', '{AKB.Trim()}', '{batteryСharger.Trim()}', '{""}', '{""}', " +
                                                        $"'{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{comment}', '{road}')";

                                        using (MySqlCommand command2 = new MySqlCommand(addQuery2, DB.GetInstance.GetConnection()))
                                        {
                                            DB.GetInstance.OpenConnection();
                                            command2.ExecuteNonQuery();
                                            DB.GetInstance.CloseConnection();
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Ошибка! Радиостнация не добавлена в общую БД!(CheacSerialNumber_radiostantion_full)");
                                }

                            }
                            else
                            {
                                MessageBox.Show("В акте более 20 радиостанций. Создайте другой номер акта");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Данная радиостанция с таким заводским номером уже присутствует в базе данных");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы не заполнили нужные поля со (*)!");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка! Радиостнация не добавлена!(Add_rst_radiostantion)");
                }
            }

        }
        #endregion

        #region очистка Control-ов
        void PictureBox4_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите очистить все введенные вами поля?";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    control.Text = "";
                }
            }
            txB_antenna.Text = "-";
            txB_manipulator.Text = "-";
            txB_AKB.Text = "-";
            txB_batteryСharger.Text = "-";
        }
        #endregion

        #region проверка в таблице radiostantion_full и если есть изменение записей
        Boolean CheacSerialNumber_radiostantion_full(string serialNumber)
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    string querystring = $"SELECT serialNumber FROM radiostantion_full WHERE serialNumber = '{serialNumber}'";

                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                try
                                {
                                    var model = cmB_model.Text;
                                    var inventoryNumber = txB_inventoryNumber.Text;
                                    var networkNumber = txB_networkNumber.Text;
                                    var dateTO = txB_dateTO.Text;
                                    var numberAct = txB_numberAct.Text;
                                    var representative = txB_representative.Text;
                                    var numberIdentification = txB_numberIdentification.Text;
                                    var phoneNumber = txB_phoneNumber.Text;
                                    var post = txB_post.Text;
                                    var dateIssue = txB_dateIssue.Text;

                                    var updateQuery = $"UPDATE radiostantion_full SET model = '{model}', inventoryNumber = '{inventoryNumber}', " +
                                        $"networkNumber = '{networkNumber}', dateTO = '{dateTO}', numberAct = '{numberAct}', representative = '{representative}', " +
                                        $"numberIdentification = '{numberIdentification}', phoneNumber = '{phoneNumber}', post = '{post}', dateIssue = '{dateIssue}'" +
                                        $" WHERE serialNumber = '{serialNumber}'";

                                    DB.GetInstance.OpenConnection();
                                    using (MySqlCommand command5 = new MySqlCommand(updateQuery, DB.GetInstance.GetConnection()))
                                    {
                                        command5.ExecuteNonQuery();
                                    }
                                    DB.GetInstance.CloseConnection();
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show("Ошибка! При добавлении в текущую БД, найденная радиостанция в общей БД не изменена!(CheacSerialNumber_radiostantion_full)");
                                }
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

        #region календарь
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

        #region KeyUp KeyPress SelectedIndexChanged Click Leave для Control-ов
        void TextBox_dateIssue_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TextBox_dateIssue_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
            {
                e.Handled = true;
            }
        }
        void TextBox_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            char decimalSeparatorChar = Convert.ToChar(Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
            if (ch == decimalSeparatorChar && txB_price.Text.IndexOf(decimalSeparatorChar) != -1)
            {
                e.Handled = true;
                return;
            }

            if (!Char.IsDigit(ch) && ch != 8 && ch != decimalSeparatorChar)
            {
                e.Handled = true;
            }
        }
        void ComboBox_model_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmB_model.Text == "Icom IC-F3GT" || cmB_model.Text == "Icom IC-F11" || cmB_model.Text == "Icom IC-F16" ||
                cmB_model.Text == "Icom IC-F3GS" || cmB_model.Text == "Motorola P040" || cmB_model.Text == "Motorola P080" ||
                cmB_model.Text == "Motorola GP-300" || cmB_model.Text == "Motorola GP-320" || cmB_model.Text == "Motorola GP-340" ||
                cmB_model.Text == "Motorola GP-360" || cmB_model.Text == "Альтавия-301М" || cmB_model.Text == "Comrade R5" ||
                cmB_model.Text == "Гранит Р33П-1" || cmB_model.Text == "Гранит Р-43" || cmB_model.Text == "Радий-301" ||
                cmB_model.Text == "Kenwood ТК-2107" || cmB_model.Text == "Vertex - 261" || cmB_model.Text == "РА-160")
            {
                txB_price.Text = "1411.18";
            }
            else
            {
                txB_price.Text = "1919.57";
            }
        }
        void TextBox_location_Click(object sender, EventArgs e)
        {
            if (txB_location.Text == "")
            {
                txB_location.Text = $"ст. {txB_city.Text}";
                txB_location.SelectionStart = txB_location.Text.Length;
                txB_location.SelectionLength = 0;

            }
        }
        void ComboBox_model_Click(object sender, EventArgs e)
        {
            cmB_model.MaxLength = 99;
        }

        void TextBox_serialNumber_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_serialNumber_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
                {
                    if (txB_serialNumber.Text != "")
                    {
                        var serialNumber = txB_serialNumber.Text;

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
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления полных данных из общей БД!Ctrl + F в TextBox_serialNumber!(TextBox_serialNumber_KeyDown)");
            }

            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    if (txB_serialNumber.Text != "")
                    {
                        var serialNumber = txB_serialNumber.Text;

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
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления частичных данных из общей БД!Return в TextBox_serialNumber!(TextBox_serialNumber_KeyDown)");
            }
        }

        void TextBox_serialNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region проверка ввода
            if (cmB_model.Text == "Icom IC-F3GT" || cmB_model.Text == "Icom IC-F11" || cmB_model.Text == "Icom IC-F16"
                || cmB_model.Text == "Icom IC-F3GS" || cmB_model.Text == "Альтавия-301М" || cmB_model.Text == "Элодия-351М"
                || cmB_model.Text == "Гранит Р33П-1" || cmB_model.Text == "Гранит Р-43" || cmB_model.Text == "Радий-301"
                || cmB_model.Text == "РНД-500")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == ' ')
                {

                }
                else
                {
                    e.Handled = true;
                }
            }

            if (cmB_model.Text == "Motorola P040" || cmB_model.Text == "Motorola P080" || cmB_model.Text == "Motorola DP-1400" ||
                cmB_model.Text == "Motorola DP-2400" || cmB_model.Text == "Motorola DP-2400е" || cmB_model.Text == "Motorola DP-4400" ||
                cmB_model.Text == "Motorola GP-300" || cmB_model.Text == "Motorola GP-320" || cmB_model.Text == "Motorola GP-340" ||
                cmB_model.Text == "Motorola GP-360" || cmB_model.Text == "Comrade R5")
            {
                if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
                {

                }
                else
                {
                    e.Handled = true;
                }

            }

            if (cmB_model.Text == "РН311М" || cmB_model.Text == "РНД-512")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == ' ')
                {

                }
                else
                {
                    e.Handled = true;
                }
            }

            if (cmB_model.Text == "Комбат T-44")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == 'T')
                {

                }
                else
                {
                    e.Handled = true;
                }
            }
            #endregion
        }
        //Shortcuts для ctrl+c ctrl + x ctrl + V
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




        void TextBox_antenna_Click(object sender, EventArgs e)
        {
            txB_antenna.Text = "";
        }
        void TextBox_antenna_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TextBox_antenna_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != '\b' && ch != '-' && ch != '1')
            {
                e.Handled = true;
            }
        }
        void TextBox_antenna_Leave(object sender, EventArgs e)
        {
            if (txB_antenna.Text == "")
            {
                txB_antenna.Text = "-";
            }
        }
        void TextBox_manipulator_Click(object sender, EventArgs e)
        {
            txB_manipulator.Text = "";
        }
        void TextBox_manipulator_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TextBox_manipulator_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != '\b' && ch != '-' && ch != '1')
            {
                e.Handled = true;
            }
        }
        void TextBox_manipulator_Leave(object sender, EventArgs e)
        {
            if (txB_manipulator.Text == "")
            {
                txB_manipulator.Text = "-";
            }
        }

        void TextBox_AKB_Leave(object sender, EventArgs e)
        {
            if (txB_AKB.Text == "")
            {
                txB_AKB.Text = "-";
            }
        }
        void TextBox_batteryСharger_Click(object sender, EventArgs e)
        {
            txB_batteryСharger.Text = "";
        }
        void TextBox_batteryСharger_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TextBox_batteryСharger_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != '\b' && ch != '-' && ch != '1')
            {
                e.Handled = true;
            }
        }
        void TextBox_batteryСharger_Leave(object sender, EventArgs e)
        {
            if (txB_batteryСharger.Text == "")
            {
                txB_batteryСharger.Text = "-";
            }
        }

        void TxB_AKB_Click(object sender, EventArgs e)
        {
            if (txB_AKB.Text == "-")
            {
                txB_AKB.Text = "";
            }
        }

        #endregion

        #region очистка дат
        void PictureBox5_Click(object sender, EventArgs e)
        {
            txB_dateTO.Text = "";
        }

        void PictureBox6_Click(object sender, EventArgs e)
        {
            txB_dateIssue.Text = "";
        }
        #endregion

        #region возможность редактирования comBox_model
        void Button_Enable_editor_comBox_model_Click(object sender, EventArgs e)
        {
            if (cmB_model.Text != "" && cmB_model.DropDownStyle != ComboBoxStyle.DropDown)
            {
                cmB_model.DropDownStyle = ComboBoxStyle.DropDown;
                btn_model_radiostation_name.Enabled = true;
            }
        }
        #endregion

        #region добавление модели радиостанции в БД
        void Button_model_radiostation_name_MouseClick(object sender, MouseEventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите добавить модель радиостанции?";

            if (Internet_check.CheackSkyNET())
            {
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                try
                {
                    DB.GetInstance.OpenConnection();
                    var addQuery = $"insert into model_radiostation (model_radiostation_name) VALUES ('{cmB_model.Text}')";

                    MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection());
                    command.ExecuteNonQuery();

                    MessageBox.Show("Модель радиостанции успешно добавлена!");
                    DB.GetInstance.CloseConnection();


                    try
                    {
                        string querystring = $"SELECT id, model_radiostation_name FROM model_radiostation";
                        using (MySqlCommand command2 = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            DataTable model_radiostation_name = new DataTable();

                            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command2))
                            {
                                adapter.Fill(model_radiostation_name);
                                if (model_radiostation_name.Rows.Count > 0)
                                {
                                    cmB_model.DataSource = model_radiostation_name;
                                    cmB_model.ValueMember = "id";
                                    cmB_model.DisplayMember = "model_radiostation_name";
                                }
                                DB.GetInstance.CloseConnection();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка! Города не добавленны в comboBox!ST_WorkForm_Load");
                    }


                    cmB_model.DropDownStyle = ComboBoxStyle.DropDownList;
                    btn_model_radiostation_name.Enabled = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка модель не добавленна в comboBox_model(Button_model_radiostation_name_MouseClick)");
                }
            }

        }

        #endregion

    }
}
