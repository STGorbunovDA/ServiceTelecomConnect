using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    public partial class СhangeRSTFormCurator : Form
    {
        private delegate DialogResult ShowOpenFileDialogInvoker();
        public СhangeRSTFormCurator()
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
            cmB_model.Text = cmB_model.Items[0].ToString();
            if (String.IsNullOrWhiteSpace(txB_numberActRemont.Text))
            {
                txB_numberActRemont.Enabled = false;
                cmB_сategory.Enabled = false;
                txB_priceRemont.Enabled = false;
            }
            if (!String.IsNullOrWhiteSpace(txB_numberAct.Text))
                txB_decommission.Enabled = false;
            else
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
                txB_numberAct.Text = "";
                pictureBox5.Enabled = false;
                lbL_Date.Text = "Дата списания:";
                txB_decommission.Focus();
            }
        }
        void CmbModelClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                DB.GetInstance.OpenConnection();
                string querystring = $"SELECT id, model_radiostation_name FROM model_radiostation";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DataTable table = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                        cmB_model.DataSource = table;
                        cmB_model.ValueMember = "id";
                        cmB_model.DisplayMember = "model_radiostation_name";
                    }
                }
                DB.GetInstance.CloseConnection();
            }
        }

        #region изменяем рст
        void BtnChangeRadiostantionClick(object sender, EventArgs e)
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
            string city = txB_city.Text;
            if (!Regex.IsMatch(city, @"^[А-Я][а-я]*(?:[\s-][А-Я][а-я]*)*$"))
            {
                MessageBox.Show("Введите корректно поле \"Город\".\n P.s. название города должно быть с большой буквы.\nпример: \"Нижний-Новгород\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_city.Select();
                if (MessageBox.Show("Вы действительно хотите добавить радиостанцию?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            string poligon = cmB_poligon.Text;
            string company = txB_company.Text;
            if (!Regex.IsMatch(company, @"^[А-Я]*([/s-]?[0-9]*)$"))
            {
                MessageBox.Show("Введите корректно поле \"Предприятие\"\n P.s. В РЖД наименование предприятий с большой буквы\nпример: \"ПЧИССО-2\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_company.Select();
                string Mesage = "Вы действительно хотите добавить радиостанцию?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            string location = txB_location.Text;
            if (!Regex.IsMatch(location, @"^[с][т][.][\s][А-Я][а-я]*(([\s-]?[0-9])*$)?([\s-]?[А-Я][а-я]*)*$"))
            {
                MessageBox.Show("Введите корректно поле \"Место нахождения\"\n P.s. пример: \"ст. Сейма\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_location.Select();
                string Mesage = "Вы действительно хотите добавить радиостанцию?";
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
                if (!Regex.IsMatch(serialNumber, @"^[T][4][4][/.][0-9]{2,2}[/.]+[0-9]{2,2}[/.][0-9]{4,4}$"))
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
            if (!Regex.IsMatch(inventoryNumber, @"^[0-9]{1,}([\-]*[\/]*[\\]*[0-9]*[\\]*[\/]*[0-9]*[\/]*[0-9]*[\*]*[\-]*[0-9]*[\/]*[0-9]*)$"))
            {
                MessageBox.Show("Введите корректно поле: \"Инвентарный номер\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_inventoryNumber.Select();

                string Mesage = "Вы действительно хотите продолжить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            string networkNumber = txB_networkNumber.Text;
            if (!Regex.IsMatch(networkNumber, @"^[0-9]{1,}([\-]*[\/]*[\\]*[0-9]*[\\]*[\/]*[0-9]*[\/]*[0-9]*[\*]*[\-]*[0-9]*[\/]*[0-9]*)$"))
            {
                MessageBox.Show("Введите корректно поле: \"Сетевой номер\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_networkNumber.Select();
                string Mesage = "Вы действительно хотите продолжить?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            string numberAct = txB_numberAct.Text;
            if (!Regex.IsMatch(txB_numberAct.Text, @"[0-9]{2,2}/([0-9]+([A-Z]?[А-Я]?)*[.\-]?[0-9]?[0-9]?[0-9]?[A-Z]?[А-Я]?)$"))
            {
                MessageBox.Show("Введите корректно \"№ Акта ТО\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_numberAct.Select();
                return;
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
            string numberActRemont = txB_numberActRemont.Text;
            string сategory = cmB_сategory.Text;
            if (!String.IsNullOrWhiteSpace(numberActRemont))
            {
                if (!Regex.IsMatch(numberActRemont, @"[0-9]{2,2}/([0-9]+([A-Z]?[А-Я]?)*[.\-]?[0-9]?[0-9]?[0-9]?[A-Z]?[А-Я]?)$"))
                {
                    MessageBox.Show("Введите корректно № Акта Ремонта", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_numberActRemont.Select();
                    return;
                }
                if (String.IsNullOrWhiteSpace(сategory))
                {
                    MessageBox.Show("Заполните поле категория ремонта");
                    return;
                }
            }
            string priceRemont = txB_priceRemont.Text;
            string decommission = txB_decommission.Text;
            string month = cmB_month.Text;
            string road = lbL_road.Text;

            if (!String.IsNullOrWhiteSpace(city) && !String.IsNullOrWhiteSpace(poligon) && !String.IsNullOrWhiteSpace(company) 
                && !String.IsNullOrWhiteSpace(location) && !String.IsNullOrWhiteSpace(model) && !String.IsNullOrWhiteSpace(serialNumber) 
                && !String.IsNullOrWhiteSpace(inventoryNumber ) && !String.IsNullOrWhiteSpace(networkNumber) 
                && !String.IsNullOrWhiteSpace(numberAct) && !String.IsNullOrWhiteSpace(dateTO))
            {
                #region проверка ввода РСТ
                if (cmB_model.Text == "Icom IC-F3GT" || cmB_model.Text == "Icom IC-F16" || cmB_model.Text == "Icom IC-F11"
                    || cmB_model.Text == "РН311М")
                {
                    if (!serialNumber.StartsWith("0"))
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"0\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Icom IC-F3GS")
                {
                    if (!serialNumber.StartsWith("54"))
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"54\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Motorola P040" || cmB_model.Text == "Motorola P080")
                {
                    if (!serialNumber.StartsWith("442"))
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"442\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Motorola DP-1400")
                {
                    if (!serialNumber.StartsWith("752"))
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"752\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Motorola DP-2400" || cmB_model.Text == "Motorola DP-2400е")
                {
                    if (!serialNumber.StartsWith("446"))
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"446\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Motorola DP-4400")
                {
                    if (!serialNumber.StartsWith("807"))
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"807\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Motorola GP-300")
                {
                    if (!serialNumber.StartsWith("174"))
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"174\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Motorola GP-320")
                {
                    if (!serialNumber.StartsWith("_что-то"))//TODO узнать зав номер радиостанции Motorola GP-320
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"что-то\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Motorola GP-340")
                {
                    if (!serialNumber.StartsWith("672"))
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"672\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Motorola GP-360")
                {
                    if (!serialNumber.StartsWith("749"))
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"749\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Элодия-351М")
                {
                    if (!serialNumber.StartsWith("1"))
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"1\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Comrade R5")
                {
                    if (!serialNumber.StartsWith("2010R"))
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"2010R\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Комбат T-44")
                {
                    if (!serialNumber.StartsWith("T44.19.10."))
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"T44.19.10.\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }

                    if (!serialNumber.Contains("."))
                    {
                        string MesageRSTProv = $"В заводском номере радиостанции {cmB_model.Text} отстутсвет \".(точка)\". Вы действительно хотите добавить РСТ?";

                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Kenwood ТК-2107")
                {
                    if (!serialNumber.StartsWith("_что-то"))//TODO узнать зав номер радиостанции Kenwood ТК-2107
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"что-то\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "Vertex - 261")
                {
                    if (!serialNumber.StartsWith("_что-то"))//TODO узнать зав номер радиостанции Vertex - 261
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"что-то\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                if (cmB_model.Text == "РА-160")
                {
                    if (!serialNumber.StartsWith("_что-то"))//TODO узнать зав номер радиостанции Kenwood РА-160
                    {
                        string MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"что-то\". Вы действительно хотите добавить РСТ?";
                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                #endregion

                string changeQuery = $"UPDATE radiostantion_сomparison SET poligon = '{poligon}', company = '{company}', " +
                    $"location = '{location}', model = '{model}', inventoryNumber = '{inventoryNumber}', " +
                    $"networkNumber = '{networkNumber}', dateTO = '{dateTO}', numberAct = '{numberAct}', " +
                    $"city = '{city}', price = '{Convert.ToDecimal(price)}', numberActRemont = '{numberActRemont}', " +
                    $"category  = '{сategory}', priceRemont = '{Convert.ToDecimal(priceRemont)}', decommissionSerialNumber = '{decommission}', " +
                    $"month = '{month}' WHERE serialNumber = '{serialNumber}' AND road = '{road}'";

                using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                }

                MessageBox.Show("Радиостанция успешно изменена!");
            }
            else MessageBox.Show("Вы не заполнили нужные поля со (*)!");
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
        }
        #endregion

        #region Показываем календарь у даты ТО
        void TxbDateTOClick(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }
        void MonthCalendar1DateSelected(object sender, DateRangeEventArgs e)
        {
            txB_dateTO.Text = e.End.ToString("dd.MM.yyyy");
            monthCalendar1.Visible = false;
        }
        #endregion

        #region Очищаем дату проведения ТО
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
        void CmbModelSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmB_model.Text == "Icom IC-F3GT" || cmB_model.Text == "Icom IC-F11" || cmB_model.Text == "Icom IC-F16" ||
                cmB_model.Text == "Icom IC-F3GS" || cmB_model.Text == "Motorola P040" || cmB_model.Text == "Motorola P080" ||
                cmB_model.Text == "Motorola GP-300" || cmB_model.Text == "Motorola GP-320" || cmB_model.Text == "Motorola GP-340" ||
                cmB_model.Text == "Motorola GP-360" || cmB_model.Text == "Альтавия-301М" || cmB_model.Text == "Comrade R5" ||
                cmB_model.Text == "Гранит Р33П-1" || cmB_model.Text == "Гранит Р-43" || cmB_model.Text == "Радий-301" ||
                cmB_model.Text == "Kenwood ТК-2107" || cmB_model.Text == "Vertex - 261" || cmB_model.Text == "РА-160")
                txB_price.Text = "1411.18";
            else txB_price.Text = "1919.57";
        }
        void TxbCompanyKeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TxbCompanyKeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-')
                e.Handled = true;
        }
        void TxbLocationKeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TxbLocationKeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
                e.Handled = true;
        }
        void TxbLocationClick(object sender, EventArgs e)
        {
            if (txB_location.Text == String.Empty)
                txB_location.Text = $"ст. {txB_city.Text}";
        }
        void TxbCityKeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TxbCityKeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
                e.Handled = true;
        }
        void TxbSerialNumberKeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TxbSerialNumberClick(object sender, EventArgs e)
        {
            if (cmB_model.Text == "Icom IC-F3GT" || cmB_model.Text == "Icom IC-F11")
            {
                txB_serialNumber.MaxLength = 7;

                if (txB_serialNumber.Text == String.Empty)
                    txB_serialNumber.Text = "0";
            }
            if (cmB_model.Text == "Icom IC-F16" || cmB_model.Text == "Icom IC-F3GS" || cmB_model.Text == "Гранит Р33П-1" ||
                cmB_model.Text == "Гранит Р-43" || cmB_model.Text == "Радий-301")
                txB_serialNumber.MaxLength = 7;
            if (cmB_model.Text == "Motorola P040" || cmB_model.Text == "Motorola P080")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == String.Empty)
                    txB_serialNumber.Text = "442";
            }
            if (cmB_model.Text == "Motorola DP-1400")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == String.Empty)
                    txB_serialNumber.Text = "752";
            }

            if (cmB_model.Text == "Motorola DP-2400" || cmB_model.Text == "Motorola DP-2400е")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == String.Empty)
                    txB_serialNumber.Text = "446";
            }
            if (cmB_model.Text == "Motorola DP-4400")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == String.Empty)
                    txB_serialNumber.Text = "807";
            }
            if (cmB_model.Text == "Motorola GP-300")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == String.Empty)
                    txB_serialNumber.Text = "174";
            }
            if (cmB_model.Text == "Motorola GP-320" || cmB_model.Text == "Kenwood ТК-2107" || cmB_model.Text == "Vertex - 261"
                || cmB_model.Text == "РА-160") //TODO Проверить условия а имеено зав номер GP320 Вертех Кенвуд РА
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == String.Empty)
                    txB_serialNumber.Text = "что-то";
            }
            if (cmB_model.Text == "Motorola GP-340")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == String.Empty)
                    txB_serialNumber.Text = "672";
            }
            if (cmB_model.Text == "Motorola GP-360")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == String.Empty)
                    txB_serialNumber.Text = "749";
            }
            if (cmB_model.Text == "Альтавия-301М" || cmB_model.Text == "Элодия-351М")
                txB_serialNumber.MaxLength = 9;
            if (cmB_model.Text == "РН311М")
                txB_serialNumber.MaxLength = 10;
            if (cmB_model.Text == "Comrade R5")
            {
                txB_serialNumber.MaxLength = 12;

                if (txB_serialNumber.Text == String.Empty)
                    txB_serialNumber.Text = "2010R";
            }
            if (cmB_model.Text == "Комбат T-44")
            {
                txB_serialNumber.MaxLength = 14;

                if (txB_serialNumber.Text == String.Empty)
                    txB_serialNumber.Text = "T44.19.10.";
            }
            if (cmB_model.Text == "РНД-500")
                txB_serialNumber.MaxLength = 4;
            if (cmB_model.Text == "РНД-512")
            {
                txB_serialNumber.MaxLength = 11;

                if (txB_serialNumber.Text == String.Empty)
                    txB_serialNumber.Text = "0";
            }
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
        //Shortcuts для ctrl+c ctrl + x ctrl + V
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
        void TxbNetworkNumberKeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TxbNetworkNumberKeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == ' '
                || e.KeyChar == '/')
            {

            }
            else e.Handled = true;
        }
        void TxbNumberActKeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        #endregion

        #region при выборе модели заполняем цену относительно модели
        private void CmbCategorySelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmB_сategory.Text == "3")
            {
                if (cmB_model.Text == "Icom IC-F3GT"
                || cmB_model.Text == "Icom IC-F11" || cmB_model.Text == "Icom IC-F16" || cmB_model.Text == "Icom IC-F3GS"
                || cmB_model.Text == "Motorola P040" || cmB_model.Text == "Motorola P080" || cmB_model.Text == "Motorola GP-300"
                || cmB_model.Text == "Motorola GP-320" || cmB_model.Text == "Motorola GP-340" || cmB_model.Text == "Motorola GP-360"
                || cmB_model.Text == "Альтавия-301М" || cmB_model.Text == "Comrade R5" || cmB_model.Text == "Гранит Р33П-1"
                || cmB_model.Text == "Гранит Р-43" || cmB_model.Text == "Радий-301" || cmB_model.Text == "Kenwood ТК-2107"
                || cmB_model.Text == "Vertex - 261")
                    txB_priceRemont.Text = "887.94";
                else txB_priceRemont.Text = "895.86";
            }
            if (cmB_сategory.Text == "4")
            {
                if (cmB_сategory.Text == "Icom IC-F3GT"
                || cmB_сategory.Text == "Icom IC-F11" || cmB_сategory.Text == "Icom IC-F16" || cmB_сategory.Text == "Icom IC-F3GS"
                || cmB_сategory.Text == "Motorola P040" || cmB_сategory.Text == "Motorola P080" || cmB_сategory.Text == "Motorola GP-300"
                || cmB_сategory.Text == "Motorola GP-320" || cmB_сategory.Text == "Motorola GP-340" || cmB_сategory.Text == "Motorola GP-360"
                || cmB_сategory.Text == "Альтавия-301М" || cmB_сategory.Text == "Comrade R5" || cmB_сategory.Text == "Гранит Р33П-1"
                || cmB_сategory.Text == "Гранит Р-43" || cmB_сategory.Text == "Радий-301" || cmB_сategory.Text == "Kenwood ТК-2107"
                || cmB_сategory.Text == "Vertex - 261")
                    txB_priceRemont.Text = "1267.49";
                else txB_priceRemont.Text = "1280.37";
            }
            if (cmB_сategory.Text == "5")
            {
                if (cmB_сategory.Text == "Icom IC-F3GT"
                || cmB_сategory.Text == "Icom IC-F11" || cmB_сategory.Text == "Icom IC-F16" || cmB_сategory.Text == "Icom IC-F3GS"
                || cmB_сategory.Text == "Motorola P040" || cmB_сategory.Text == "Motorola P080" || cmB_сategory.Text == "Motorola GP-300"
                || cmB_сategory.Text == "Motorola GP-320" || cmB_сategory.Text == "Motorola GP-340" || cmB_сategory.Text == "Motorola GP-360"
                || cmB_сategory.Text == "Альтавия-301М" || cmB_сategory.Text == "Comrade R5" || cmB_сategory.Text == "Гранит Р33П-1"
                || cmB_сategory.Text == "Гранит Р-43" || cmB_сategory.Text == "Радий-301" || cmB_сategory.Text == "Kenwood ТК-2107"
                || cmB_сategory.Text == "Vertex - 261")
                    txB_priceRemont.Text = "2535.97";
                else txB_priceRemont.Text = "2559.75";
            }
            if (cmB_сategory.Text == "6")
            {
                if (cmB_сategory.Text == "Icom IC-F3GT"
                || cmB_сategory.Text == "Icom IC-F11" || cmB_сategory.Text == "Icom IC-F16" || cmB_сategory.Text == "Icom IC-F3GS"
                || cmB_сategory.Text == "Motorola P040" || cmB_сategory.Text == "Motorola P080" || cmB_сategory.Text == "Motorola GP-300"
                || cmB_сategory.Text == "Motorola GP-320" || cmB_сategory.Text == "Motorola GP-340" || cmB_сategory.Text == "Motorola GP-360"
                || cmB_сategory.Text == "Альтавия-301М" || cmB_сategory.Text == "Comrade R5" || cmB_сategory.Text == "Гранит Р33П-1"
                || cmB_сategory.Text == "Гранит Р-43" || cmB_сategory.Text == "Радий-301" || cmB_сategory.Text == "Kenwood ТК-2107"
                || cmB_сategory.Text == "Vertex - 261")
                    txB_priceRemont.Text = "5071.94";
                else txB_priceRemont.Text = "5119.51";
            }
        }
        #endregion
    }
}
