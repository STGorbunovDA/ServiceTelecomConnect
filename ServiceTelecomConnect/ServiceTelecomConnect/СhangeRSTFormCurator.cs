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

        void ChangeRSTForm_Load(object sender, EventArgs e)
        {
            cmB_model.Text = cmB_model.Items[0].ToString();
        }

        void ComboBox_model_Click(object sender, EventArgs e)
        {
            try
            {
                if (Internet_check.AvailabilityChanged_bool())
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
                var numberActRemont = txB_numberActRemont.Text;
                var сategory = cmB_сategory.Text;
                var priceRemont = txB_priceRemont.Text;
                var decommission = txB_decommission.Text;
                var january = txB_january.Text;
                var february = txB_february.Text;
                var march = txB_march.Text;
                var april = txB_april.Text;
                var may = txB_may.Text;
                var june = txB_june.Text;
                var july = txB_july.Text;
                var august = txB_august.Text;
                var september = txB_september.Text;
                var october = txB_october.Text;
                var november = txB_november.Text;
                var december = txB_december.Text;
                var comment = txB_comment.Text;

                try
                {
                    DateTime.Parse(dateTO).ToString("dd.MM.yyyy");
                    if ((city != "") && (poligon != "") && (company != "") && (location != "")
                    && (model != "") && (serialNumber != "") && (inventoryNumber != "") && (networkNumber != "")
                    && (numberAct != "") && (dateTO != ""))
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

                        #endregion

                        var changeQuery = $"UPDATE radiostantion_сomparison SET poligon = '{poligon}', company = '{company}', " +
                            $"location = '{location}', model = '{model}', inventoryNumber = '{inventoryNumber}', " +
                            $"networkNumber = '{networkNumber}', dateTO = '{dateTO}', numberAct = '{numberAct}', " +
                            $"city = '{city}', price = '{Convert.ToDecimal(price)}', numberActRemont = '{numberActRemont}', " +
                            $"category  = '{сategory}', priceRemont = '{Convert.ToDecimal(priceRemont)}', decommissionSerialNumber = '{decommission}', " +
                            $"february = '{february}', march = '{march}', april = '{april}', may = '{may}', june = '{june}', " +
                            $"january = '{january}', july = '{july}', august = '{august}', september = '{september}', october = '{october}', " +
                            $"november = '{november}', december = '{december}', comment = '{comment}' WHERE serialNumber = '{serialNumber}'";

                        using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            command.ExecuteNonQuery();
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
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Радиостнация не изменена!(Button_сhange_rst_Click)");
            }
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
            txB_city.Text = "";
            cmB_poligon.Text = "";
            txB_company.Text = "";
            txB_location.Text = "";
            cmB_model.Text = "";
            txB_serialNumber.Text = "";
            txB_inventoryNumber.Text = "";
            txB_networkNumber.Text = "";
            txB_numberAct.Text = "";
            txB_dateTO.Text = "";
            txB_price.Text = "";
            txB_numberActRemont.Text = "";
            cmB_сategory.Text = "";
            txB_priceRemont.Text = "";
            txB_decommission.Text = "";
            txB_january.Text = "";
            txB_february.Text = "";
            txB_march.Text = "";
            txB_april.Text = "";
            txB_may.Text = "";
            txB_june.Text = "";
            txB_july.Text = "";
            txB_august.Text = "";
            txB_september.Text = "";
            txB_october.Text = "";
            txB_november.Text = "";
            txB_december.Text = "";
            txB_comment.Text = "";
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
        #endregion

        #region Очищаем дату проведения ТО
        void PictureBox5_Click(object sender, EventArgs e)
        {
            txB_dateTO.Text = "";
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

        void TextBox_TextChanged()
        {
            if (txB_city.Text.Length > 0 && txB_company.Text.Length > 0
                && txB_location.Text.Length > 0 && txB_serialNumber.Text.Length > 0
                && txB_inventoryNumber.Text.Length > 0 && txB_networkNumber.Text.Length > 0
                && txB_dateTO.Text.Length > 0 && txB_numberAct.Text.Length > 0)
            {
                button_save_add_rst.Enabled = true;
            }
            else
            {
                button_save_add_rst.Enabled = false;
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
            toolTip1.SetToolTip(pictureBox4, $"Очистить все поля");
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
            //toolTip1.SetToolTip(pictureBox6, $"Очистить поле Дата Выдачи удостоверения:");
        }



        #endregion

        private void CmB_сategory_SelectionChangeCommitted(object sender, EventArgs e)
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
                {
                    txB_priceRemont.Text = "887.94";
                }
                else
                {
                    txB_priceRemont.Text = "895.86";
                }
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
                {
                    txB_priceRemont.Text = "1267.49";
                }
                else
                {
                    txB_priceRemont.Text = "1280.37";
                }
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
                {
                    txB_priceRemont.Text = "2535.97";
                }
                else
                {
                    txB_priceRemont.Text = "2559.75";
                }
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
                {
                    txB_priceRemont.Text = "5071.94";
                }
                else
                {
                    txB_priceRemont.Text = "5119.51";
                }
            }
        }
    }
}
