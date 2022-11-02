using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
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

        void ChangeRSTForm_Load(object sender, EventArgs e)
        {
            cmB_model.Text = cmB_model.Items[0].ToString();
        }

        void ComboBox_model_Click(object sender, EventArgs e)
        {
            try
            {
                if (Internet_check.CheackSkyNET())
                {
                    DB.GetInstance.OpenConnection();
                    string querystring = $"SELECT id, model_radiostation_name FROM model_radiostation";
                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        DataTable model_RSR_table = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(model_RSR_table);
                            cmB_model.DataSource = model_RSR_table;
                            cmB_model.ValueMember = "id";
                            cmB_model.DisplayMember = "model_radiostation_name";
                        }
                    }
                    DB.GetInstance.CloseConnection();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка модель радиостанций из БД не добавлены в comboBox_model(ComboBox_model_Click)");
            }
        }

        #region изменяем рст
        void Button_сhange_rst_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txB_numberAct.Text))
            {
                string Mesage;
                Mesage = "Вы действительно хотите изменить радиостанцию?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                try
                {
                    foreach (Control control in this.Controls)
                    {
                        if (control is TextBox)
                        {
                            var re = new Regex(Environment.NewLine);
                            control.Text = re.Replace(control.Text, " ");
                        }
                    }

                    var city = txB_city.Text;
                    var poligon = cmB_poligon.Text;
                    var company = txB_company.Text;
                    var location = txB_location.Text;
                    var model = cmB_model.Text;
                    var serialNumber = txB_serialNumber.Text;
                    var inventoryNumber = txB_inventoryNumber.Text;
                    var networkNumber = txB_networkNumber.Text;
                    var numberAct = txB_numberAct.Text;
                    var dateTO = txB_dateTO.Text;
                    var price = txB_price.Text;
                    var representative = txB_representative.Text;
                    var post = txB_post.Text;
                    var numberIdentification = txB_numberIdentification.Text;
                    var dateIssue = txB_dateIssue.Text;
                    var phoneNumber = txB_phoneNumber.Text;
                    var antenna = txB_antenna.Text;
                    var manipulator = txB_manipulator.Text;
                    var AKB = txB_AKB.Text;
                    var batteryСharger = txB_batteryСharger.Text;
                    var comment = txB_comment.Text;

                    if (dateIssue.Length > 0)
                    {
                        try
                        {
                            DateTime.Parse(dateIssue).ToString("dd.MM.yyyy");
                            if (!(poligon == "") && !(company == "") && !(location == "") && !(model == "")
                            && !(serialNumber == "") && !(dateTO == "") && !(numberAct == "") && !(city == "")
                            && !(representative == "") && !(post == "") && !(numberIdentification == "")
                            && !(dateIssue == "") && !(phoneNumber == "") && !(antenna == "")
                            && !(manipulator == "") && !(AKB == "") && !(batteryСharger == ""))
                            {
                                #region проверка ввода РСТ
                                if (cmB_model.Text == "Icom IC-F3GT" || cmB_model.Text == "Icom IC-F16" || cmB_model.Text == "Icom IC-F11"
                                    || cmB_model.Text == "РН311М")
                                {
                                    if (!serialNumber.StartsWith("0"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"0\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Icom IC-F3GS")
                                {
                                    if (!serialNumber.StartsWith("54"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"54\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Motorola P040" || cmB_model.Text == "Motorola P080")
                                {
                                    if (!serialNumber.StartsWith("442"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"442\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Motorola DP-1400")
                                {
                                    if (!serialNumber.StartsWith("752"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"752\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Motorola DP-2400" || cmB_model.Text == "Motorola DP-2400е")
                                {
                                    if (!serialNumber.StartsWith("446"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"446\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Motorola DP-4400")
                                {
                                    if (!serialNumber.StartsWith("807"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"807\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Motorola GP-300")
                                {
                                    if (!serialNumber.StartsWith("174"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"174\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Motorola GP-320")
                                {
                                    if (!serialNumber.StartsWith("_что-то"))//TODO узнать зав номер радиостанции Motorola GP-320
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"что-то\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Motorola GP-340")
                                {
                                    if (!serialNumber.StartsWith("672"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"672\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Motorola GP-360")
                                {
                                    if (!serialNumber.StartsWith("749"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"749\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Элодия-351М")
                                {
                                    if (!serialNumber.StartsWith("1"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"1\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Comrade R5")
                                {
                                    if (!serialNumber.StartsWith("2010R"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"2010R\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Комбат T-44")
                                {
                                    if (!serialNumber.StartsWith("T44.19.10."))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"T44.19.10.\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }

                                    if (!serialNumber.Contains("."))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"В заводском номере радиостанции {cmB_model.Text} отстутсвет \".(точка)\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }
                                if (cmB_model.Text == "Kenwood ТК-2107")
                                {
                                    if (!serialNumber.StartsWith("_что-то"))//TODO узнать зав номер радиостанции Kenwood ТК-2107
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"что-то\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "Vertex - 261")
                                {
                                    if (!serialNumber.StartsWith("_что-то"))//TODO узнать зав номер радиостанции Vertex - 261
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"что-то\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (cmB_model.Text == "РА-160")
                                {
                                    if (!serialNumber.StartsWith("_что-то"))//TODO узнать зав номер радиостанции Kenwood РА-160
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {cmB_model.Text} начинается не с \"что-то\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (!representative.Contains("."))
                                {
                                    string MesageRSTProv;
                                    MesageRSTProv = $"В графе \"Представитель ФИО\" отстутсвуют в имени или отчестве \".(точки)\". Вы действительно хотите добавить РСТ?";

                                    if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    {
                                        return;
                                    }
                                }
                                #endregion

                                var changeQuery = $"UPDATE radiostantion SET city = '{city}', poligon = '{poligon}', company = '{company}', " +
                                    $"location = '{location}', model = '{model}', inventoryNumber = '{inventoryNumber}', " +
                                    $"networkNumber = '{networkNumber}', dateTO = '{dateTO}', numberAct = '{numberAct}', " +
                                    $"price = '{Convert.ToDecimal(price)}', representative = '{representative}', " +
                                    $"numberIdentification = '{numberIdentification}', dateIssue = '{dateIssue}', " +
                                    $"phoneNumber = '{phoneNumber}', post = '{post}', antenna = '{antenna}', manipulator = '{manipulator}', AKB = '{AKB}', " +
                                    $"batteryСharger = '{batteryСharger}', comment = '{comment}' WHERE serialNumber = '{serialNumber}'";

                                var changeQuery2 = $"UPDATE radiostantion_full SET city = '{city}', poligon = '{poligon}', company = '{company}', " +
                                    $"location = '{location}', model = '{model}', inventoryNumber = '{inventoryNumber}', " +
                                    $"networkNumber = '{networkNumber}', dateTO = '{dateTO}', numberAct = '{numberAct}', " +
                                    $"price = '{Convert.ToDecimal(price)}', representative = '{representative}', " +
                                    $"numberIdentification = '{numberIdentification}', dateIssue = '{dateIssue}', " +
                                    $"phoneNumber = '{phoneNumber}', post = '{post}', antenna = '{antenna}', manipulator = '{manipulator}', AKB = '{AKB}', " +
                                    $"batteryСharger = '{batteryСharger}', comment = '{comment}' WHERE serialNumber = '{serialNumber}'";

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
                            else
                            {
                                MessageBox.Show("Вы не заполнили нужные поля со (*)!");
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Дата выдачи удостоверения введена неверно!");
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка! Радиостнация не изменена!(Button_сhange_rst_Click)");
                }
            }
            else MessageBox.Show("Заполни номер акта");
        }
        #endregion

        #region Очищаем Conrol-ы
        void PictureBox4_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите очистить все введенные вами поля?";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            cmB_poligon.Text = "";
            txB_company.Text = "";
            cmB_model.Text = "";
            txB_serialNumber.Text = "";
            txB_inventoryNumber.Text = "";
            txB_networkNumber.Text = "";
            txB_location.Text = "";
            txB_dateTO.Text = "";
            txB_city.Text = "";
            txB_price.Text = "";
            txB_numberAct.Text = "";
            txB_representative.Text = "";
            txB_post.Text = "";
            txB_numberIdentification.Text = "";
            txB_dateIssue.Text = "";
            txB_phoneNumber.Text = "";
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

        #region Очищаем дату проведения ТО
        void PictureBox6_Click(object sender, EventArgs e)
        {
            txB_dateIssue.Text = "";
        }
        #endregion

        #region KeyUp KeyPress для Control-ов

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

        void PictureBox5_Click(object sender, EventArgs e)
        {
            txB_dateTO.Text = "";
        }

        void TextBox_company_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-')
            {
                e.Handled = true;
            }
        }

        void TextBox_location_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_location_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }
        void TextBox_location_Click(object sender, EventArgs e)
        {
            if (txB_location.Text == "")
            {
                txB_location.Text = $"ст. {txB_city.Text}";
            }
        }

        void TextBox_city_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_city_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        void TextBox_serialNumber_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_serialNumber_Click(object sender, EventArgs e)
        {
            if (cmB_model.Text == "Icom IC-F3GT" || cmB_model.Text == "Icom IC-F11")
            {
                txB_serialNumber.MaxLength = 7;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "0";
                }
            }

            if (cmB_model.Text == "Icom IC-F16" || cmB_model.Text == "Icom IC-F3GS" || cmB_model.Text == "Гранит Р33П-1" ||
                cmB_model.Text == "Гранит Р-43" || cmB_model.Text == "Радий-301")
            {
                txB_serialNumber.MaxLength = 7;
            }

            if (cmB_model.Text == "Motorola P040" || cmB_model.Text == "Motorola P080")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "442";
                }
            }

            if (cmB_model.Text == "Motorola DP-1400")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "752";
                }
            }

            if (cmB_model.Text == "Motorola DP-2400" || cmB_model.Text == "Motorola DP-2400е")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "446";
                }
            }

            if (cmB_model.Text == "Motorola DP-4400")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "807";
                }
            }

            if (cmB_model.Text == "Motorola GP-300")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "174";
                }
            }

            if (cmB_model.Text == "Motorola GP-320" || cmB_model.Text == "Kenwood ТК-2107" || cmB_model.Text == "Vertex - 261"
                || cmB_model.Text == "РА-160") //TODO Проверить условия а имеено зав номер GP320 Вертех Кенвуд РА
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "что-то";
                }
            }

            if (cmB_model.Text == "Motorola GP-340")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "672";
                }
            }

            if (cmB_model.Text == "Motorola GP-360")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "749";
                }
            }

            if (cmB_model.Text == "Альтавия-301М" || cmB_model.Text == "Элодия-351М")
            {
                txB_serialNumber.MaxLength = 9;
            }

            if (cmB_model.Text == "РН311М")
            {
                txB_serialNumber.MaxLength = 10;
            }

            if (cmB_model.Text == "Comrade R5")
            {
                txB_serialNumber.MaxLength = 12;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "2010R";
                }
            }

            if (cmB_model.Text == "Комбат T-44")
            {
                txB_serialNumber.MaxLength = 14;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "T44.19.10.";
                }
            }

            if (cmB_model.Text == "РНД-500")
            {
                txB_serialNumber.MaxLength = 4;
            }

            if (cmB_model.Text == "РНД-512")
            {
                txB_serialNumber.MaxLength = 11;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "0";
                }
            }
        }

        void TextBox_serialNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cmB_model.Text == "Icom IC-F3GT" || cmB_model.Text == "Icom IC-F11" || cmB_model.Text == "Icom IC-F16"
                || cmB_model.Text == "Icom IC-F3GS" || cmB_model.Text == "Альтавия-301М" || cmB_model.Text == "Элодия-351М"
                || cmB_model.Text == "Гранит Р33П-1" || cmB_model.Text == "Гранит Р-43" || cmB_model.Text == "Радий-301"
                || cmB_model.Text == "РНД-500")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
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
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == 84)
                {

                }
                else
                {
                    e.Handled = true;
                }
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

        void TextBox_networkNumber_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_networkNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == ' '
                || e.KeyChar == '/')
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        void TextBox_numberAct_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_representative_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_representative_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        void TextBox_numberIdentification_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_numberIdentification_Click(object sender, EventArgs e)
        {

            if (txB_numberIdentification.Text == "")
            {
                txB_numberIdentification.Text = "V ";
            }
        }

        void TextBox_numberIdentification_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '№' || e.KeyChar == ' ' || e.KeyChar == 'V')
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        void TextBox_phoneNumber_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_phoneNumber_Click(object sender, EventArgs e)
        {
            txB_phoneNumber.MaxLength = 16;

            if (txB_phoneNumber.Text == "")
            {
                txB_phoneNumber.Text = "+7-";
            }
        }

        void TextBox_phoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '+' || e.KeyChar == ' ' || e.KeyChar == '-')
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        void TextBox_post_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }

        void TextBox_post_Click(object sender, EventArgs e)
        {
            txB_post.MaxLength = 150;

        }

        void TextBox_post_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b'
                && ch != '-' && ch != '.' && ch != ' ' && ch != '=' && ch != '!' && ch != '*')
            {
                e.Handled = true;
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

        void TextBox_TextChanged()
        {
            if (cmB_poligon.Text.Length > 0 && txB_company.Text.Length > 0
                && txB_location.Text.Length > 0
                && cmB_model.Text.Length > 0 && txB_serialNumber.Text.Length > 0
                && txB_inventoryNumber.Text.Length > 0 && txB_networkNumber.Text.Length > 0
                && txB_dateTO.Text.Length > 0 && txB_price.Text.Length > 0
                && txB_numberAct.Text.Length > 0 && txB_representative.Text.Length > 0
                && txB_numberIdentification.Text.Length > 0 && txB_phoneNumber.Text.Length > 0
                && txB_post.Text.Length > 0 && txB_dateIssue.Text.Length > 0)
            {
                btn_save_add_rst.Enabled = true;
            }
            else
            {
                btn_save_add_rst.Enabled = false;
            }
        }

        void ChangeRSTForm_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox_TextChanged();

            if (e.KeyCode == Keys.F1)
            {
                toolTip1.Active = toolTip1.Active ? false : true;
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

        #region toolTip для Control-ов формы
        void PictureBox4_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(picB_clear, $"Очистить все поля");
        }

        void PictureBox5_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(pictureBox5, $"Очистить поле Дата ТО:");
        }

        void PictureBox6_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.OwnerDraw = true;
            toolTip1.Draw += new DrawToolTipEventHandler(ToolTip1_Draw);
            toolTip1.Popup += new PopupEventHandler(ToolTip1_Popup);
            toolTip1.SetToolTip(pictureBox6, $"Очистить поле Дата Выдачи удостоверения:");
        }





        #endregion


        #region смена удостоврения сразу у всех рст по номеру акта или по пп

        void Change_numberIdentification_numberAct_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string Mesage;
                Mesage = "Вы действительно хотите сменить удостоверение представителя у всего акта?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                var queryUpdateClient = $"UPDATE radiostantion SET representative = '{txB_representative.Text}', post = '{txB_post.Text}', " +
                    $"numberIdentification = '{txB_numberIdentification.Text}', dateIssue = '{txB_dateIssue.Text}',  phoneNumber = '{txB_phoneNumber.Text}' WHERE numberAct = '{txB_numberAct.Text}'";

                using (MySqlCommand command = new MySqlCommand(queryUpdateClient, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                    MessageBox.Show($"Всё данные удостоверния по номеру акта изменены ", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка общего изменения юридических характеристик представителя предприятия по номеру акта (LbL_client_FIO_company_DoubleClick)");
            }
        }

        void Change_numberIdentification_company_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string Mesage;
                Mesage = "Вы действительно хотите сменить удостоверение представителя у всего предприятия?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                var queryUpdateClient = $"UPDATE radiostantion SET representative = '{txB_representative.Text}', post = '{txB_post.Text}', " +
                    $"numberIdentification = '{txB_numberIdentification.Text}', dateIssue = '{txB_dateIssue.Text}',  phoneNumber = '{txB_phoneNumber.Text}' WHERE company = '{txB_company.Text}'";

                using (MySqlCommand command = new MySqlCommand(queryUpdateClient, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                    MessageBox.Show($"Всё данные удостоверния по предприятию изменены ", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка общего изменения юридических характеристик представителя предприятия по номеру акта (LbL_client_FIO_company_DoubleClick)");
            }
        }
    }
    #endregion
}

