using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

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
            textBox_dateTO.ReadOnly = true;
            textBox_dateTO.Text = DateTime.Now.ToString("dd.MM.yyyy");

            textBox_dateIssue.Text = DateTime.Now.ToString("dd.MM.yyyy");
            button_save_add_rst.Enabled = false;
            comboBox_poligon.Text = comboBox_poligon.Items[0].ToString();
        }

        void AddRSTForm_Load(object sender, EventArgs e)
        {
            if (Internet_check.GetInstance.AvailabilityChanged_bool())
            {
                try
                {
                    DB.GetInstance.openConnection();
                    string querystring = $"SELECT id, model_radiostation_name FROM model_radiostation";
                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        DataTable model_RSR_table = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(model_RSR_table);

                            comboBox_model.DataSource = model_RSR_table;
                            comboBox_model.ValueMember = "id";
                            comboBox_model.DisplayMember = "model_radiostation_name";

                            DB.GetInstance.closeConnection();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка модель не добавленна! в comboBox");
                    MessageBox.Show(ex.ToString());
                }
            }

        }
        #region добавление РСТ
        void Button_save_add_rst_Click(object sender, EventArgs e)
        {
            Add_rst_radiostantion();
        }

        void Add_rst_radiostantion()
        {
            if (Internet_check.GetInstance.AvailabilityChanged_bool())
            {
                string Mesage;
                Mesage = "Вы действительно хотите добавить радиостанцию?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                try
                {
                    var city = textBox_city.Text;
                    var poligon = comboBox_poligon.Text;
                    var company = textBox_company.Text;
                    var location = textBox_location.Text;
                    var model = comboBox_model.Text;
                    var serialNumber = textBox_serialNumber.Text;
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
                    if (dateIssue.Length > 0)
                    {
                        try
                        {
                            DateTime.Parse(dateIssue).ToString("dd.MM.yyyy");
                            if (!(poligon == "") && !(company == "") && !(location == "") && !(model == "")
                            && !(serialNumber == "") && !(dateTO == "") && !(numberAct == "") && !(city == "")
                            && !(representative == "") && !(post == "") && !(numberIdentification == "")
                            && !(phoneNumber == "") && !(antenna == "")
                            && !(manipulator == "") && !(AKB == "") && !(batteryСharger == ""))
                            {
                                #region проверка на ввод зав. номера рст
                                if (comboBox_model.Text == "Icom IC-F3GT" || comboBox_model.Text == "Icom IC-F16" || comboBox_model.Text == "Icom IC-F11"
                                    || comboBox_model.Text == "РН311М")
                                {
                                    if (!serialNumber.StartsWith("0"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"0\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "Icom IC-F3GS")
                                {
                                    if (!serialNumber.StartsWith("54"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"54\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "Motorola P040" || comboBox_model.Text == "Motorola P080")
                                {
                                    if (!serialNumber.StartsWith("442"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"442\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "Motorola DP-1400")
                                {
                                    if (!serialNumber.StartsWith("752"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"752\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "Motorola DP-2400" || comboBox_model.Text == "Motorola DP-2400е")
                                {
                                    if (!serialNumber.StartsWith("446"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"446\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "Motorola DP-4400")
                                {
                                    if (!serialNumber.StartsWith("807"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"807\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "Motorola GP-300")
                                {
                                    if (!serialNumber.StartsWith("174"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"174\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "Motorola GP-320")
                                {
                                    if (!serialNumber.StartsWith("_что-то"))//TODO узнать зав номер радиостанции Motorola GP-320
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"что-то\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "Motorola GP-340")
                                {
                                    if (!serialNumber.StartsWith("672"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"672\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "Motorola GP-360")
                                {
                                    if (!serialNumber.StartsWith("749"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"749\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "Элодия-351М")
                                {
                                    if (!serialNumber.StartsWith("1"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"1\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }


                                if (comboBox_model.Text == "Comrade R5")
                                {
                                    if (!serialNumber.StartsWith("2010R"))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"2010R\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "Комбат T-44")
                                {
                                    if (!serialNumber.StartsWith("T44.19.10."))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"T44.19.10.\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }

                                    if (!serialNumber.Contains("."))
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"В заводском номере радиостанции {comboBox_model.Text} отстутсвет \".(точка)\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }
                                if (comboBox_model.Text == "Kenwood ТК-2107")
                                {
                                    if (!serialNumber.StartsWith("_что-то"))//TODO узнать зав номер радиостанции Kenwood ТК-2107
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"что-то\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "Vertex - 261")
                                {
                                    if (!serialNumber.StartsWith("_что-то"))//TODO узнать зав номер радиостанции Vertex - 261
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"что-то\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (comboBox_model.Text == "РА-160")
                                {
                                    if (!serialNumber.StartsWith("_что-то"))//TODO узнать зав номер радиостанции Kenwood РА-160
                                    {
                                        string MesageRSTProv;
                                        MesageRSTProv = $"Заводской номер радиостанции {comboBox_model.Text} начинается не с \"что-то\". Вы действительно хотите добавить РСТ?";

                                        if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }

                                if (!representative.Contains("."))
                                {
                                    string MesageRSTProv;
                                    MesageRSTProv = $"Вы ввели некоректную запись \"Фамилии И.О.\" представителя!";

                                    if (MessageBox.Show(MesageRSTProv, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    {
                                        return;
                                    }
                                }
                                #endregion
                                if (!CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantion(serialNumber))
                                {
                                    if (!CheacSerialNumber.GetInstance.CheackNumberAct_radiostantion(numberAct))
                                    {

                                        var addQuery = $"INSERT INTO radiostantion (poligon, company, location, model, serialNumber," +
                                            $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, representative, " +
                                            $"post, numberIdentification, dateIssue, phoneNumber, numberActRemont, category, priceRemont, " +
                                            $"antenna, manipulator, AKB, batteryСharger, completed_works_1, completed_works_2, completed_works_3, " +
                                            $"completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1, parts_2, parts_3, parts_4, " +
                                            $"parts_5, parts_6, parts_7, decommissionSerialNumber) VALUES ('{poligon.Trim()}', '{company.Trim()}', '{location.Trim()}'," +
                                            $"'{model.Trim()}','{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}', " +
                                            $"'{dateTO.Trim()}','{numberAct.Trim()}','{city.Trim()}','{price.Trim()}', '{representative.Trim()}', '{post.Trim()}', " +
                                            $"'{numberIdentification.Trim()}', '{dateIssue.Trim()}', '{phoneNumber.Trim()}', '{""}', '{""}', '{0.00}'," +
                                            $"'{antenna.Trim()}', '{manipulator.Trim()}', '{AKB.Trim()}', '{batteryСharger.Trim()}', '{""}', '{""}', " +
                                            $"'{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}')";

                                        using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                                        {
                                            DB.GetInstance.openConnection();
                                            command.ExecuteNonQuery();
                                            DB.GetInstance.closeConnection();
                                            Add_rst_radiostantion_full();
                                            MessageBox.Show("Радиостанция успешно добавлена!");
                                            textBox_serialNumber.Text = "";
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
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка! Радиостнация не добавлена!");
                    MessageBox.Show(ex.ToString());
                }
            }


        }
        void Add_rst_radiostantion_full()
        {
            if (Internet_check.GetInstance.AvailabilityChanged_bool())
            {
                try
                {
                    var city = textBox_city.Text;
                    var poligon = comboBox_poligon.Text;
                    var company = textBox_company.Text;
                    var location = textBox_location.Text;
                    var model = comboBox_model.Text;
                    var serialNumber = textBox_serialNumber.Text;
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

                    DateTime.Parse(dateIssue).ToString("dd.MM.yyyy");
                    if (!(poligon == "") && !(company == "") && !(location == "") && !(model == "")
                    && !(serialNumber == "") && !(dateTO == "") && !(numberAct == "") && !(city == "")
                    && !(representative == "") && !(post == "") && !(numberIdentification == "")
                    && !(phoneNumber == "") && !(antenna == "")
                    && !(manipulator == "") && !(AKB == "") && !(batteryСharger == ""))
                    {
                        if (CheacSerialNumber_radiostantion_full(serialNumber) == false)
                        {
                            var addQuery = $"INSERT INTO radiostantion_full (poligon, company, location, model, serialNumber," +
                                            $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, representative, " +
                                            $"post, numberIdentification, dateIssue, phoneNumber, numberActRemont, category, priceRemont, " +
                                            $"antenna, manipulator, AKB, batteryСharger, completed_works_1, completed_works_2, completed_works_3, " +
                                            $"completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1, parts_2, parts_3, parts_4, " +
                                            $"parts_5, parts_6, parts_7, decommissionSerialNumber) VALUES ('{poligon.Trim()}', '{company.Trim()}', '{location.Trim()}'," +
                                            $"'{model.Trim()}','{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}', " +
                                            $"'{dateTO.Trim()}','{numberAct.Trim()}','{city.Trim()}','{price.Trim()}', '{representative.Trim()}', '{post.Trim()}', " +
                                            $"'{numberIdentification.Trim()}', '{dateIssue.Trim()}', '{phoneNumber.Trim()}', '{""}', '{""}', '{0.00}'," +
                                            $"'{antenna.Trim()}', '{manipulator.Trim()}', '{AKB.Trim()}', '{batteryСharger.Trim()}', '{""}', '{""}', " +
                                            $"'{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}','{""}')";

                            using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.openConnection();
                                command.ExecuteNonQuery();
                                DB.GetInstance.closeConnection();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString()); ;
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

            comboBox_poligon.Text = "";
            textBox_company.Text = "";
            comboBox_model.Text = "";
            textBox_serialNumber.Text = "";
            textBox_inventoryNumber.Text = "";
            textBox_networkNumber.Text = "";
            textBox_location.Text = "";
            textBox_dateTO.Text = "";
            textBox_city.Text = "";
            textBox_price.Text = "";
            textBox_numberAct.Text = "";
            textBox_representative.Text = "";
            textBox_post.Text = "";
            textBox_numberIdentification.Text = "";
            textBox_dateIssue.Text = "";
            textBox_phoneNumber.Text = "";
            textBox_antenna.Text = "-";
            textBox_manipulator.Text = "-";
            textBox_AKB.Text = "-";
            textBox_batteryСharger.Text = "-";
        }
        #endregion

        #region проверка в таблице radiostantion_full и если есть изменение записей
        Boolean CheacSerialNumber_radiostantion_full(string serialNumber)
        {
            if (Internet_check.GetInstance.AvailabilityChanged_bool())
            {
                try
                {
                    string querystring = $"SELECT * FROM radiostantion_full WHERE serialNumber = '{serialNumber}'";

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
                                    DB.GetInstance.openConnection();
                                    var model = comboBox_model.Text;
                                    var inventoryNumber = textBox_inventoryNumber.Text;
                                    var networkNumber = textBox_networkNumber.Text;
                                    var dateTO = textBox_dateTO.Text;
                                    var numberAct = textBox_numberAct.Text;
                                    var representative = textBox_representative.Text;
                                    var numberIdentification = textBox_numberIdentification.Text;
                                    var phoneNumber = textBox_phoneNumber.Text;
                                    var post = textBox_post.Text;
                                    var dateIssue = textBox_dateIssue.Text;

                                    var updateQuery = $"UPDATE radiostantion_full SET model = '{model}', inventoryNumber = '{inventoryNumber}', networkNumber = '{networkNumber}', dateTO = '{dateTO}', numberAct = '{numberAct}', representative = '{representative}', numberIdentification = '{numberIdentification}', phoneNumber = '{phoneNumber}', post = '{post}', dateIssue = '{dateIssue}' WHERE serialNumber = '{serialNumber}'";

                                    using (MySqlCommand command5 = new MySqlCommand(updateQuery, DB.GetInstance.GetConnection()))
                                    {
                                        command5.ExecuteNonQuery();
                                    }
                                    DB.GetInstance.closeConnection();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
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
            textBox_dateTO.Text = e.End.ToString("dd.MM.yyyy");
            monthCalendar1.Visible = false;
        }

        void TextBox_dateIssue_Click(object sender, EventArgs e)
        {
            monthCalendar2.Visible = true;
        }

        void MonthCalendar2_DateSelected(object sender, DateRangeEventArgs e)
        {
            textBox_dateIssue.Text = e.End.ToString("dd.MM.yyyy");
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
            if (ch == decimalSeparatorChar && textBox_price.Text.IndexOf(decimalSeparatorChar) != -1)
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
            if (comboBox_model.Text == "Icom IC-F3GT" || comboBox_model.Text == "Icom IC-F11" || comboBox_model.Text == "Icom IC-F16" ||
                comboBox_model.Text == "Icom IC-F3GS" || comboBox_model.Text == "Motorola P040" || comboBox_model.Text == "Motorola P080" ||
                comboBox_model.Text == "Motorola GP-300" || comboBox_model.Text == "Motorola GP-320" || comboBox_model.Text == "Motorola GP-340" ||
                comboBox_model.Text == "Motorola GP-360" || comboBox_model.Text == "Альтавия-301М" || comboBox_model.Text == "Comrade R5" ||
                comboBox_model.Text == "Гранит Р33П-1" || comboBox_model.Text == "Гранит Р-43" || comboBox_model.Text == "Радий-301" ||
                comboBox_model.Text == "Kenwood ТК-2107" || comboBox_model.Text == "Vertex - 261" || comboBox_model.Text == "РА-160")
            {
                textBox_price.Text = "1411.18";
            }
            else
            {
                textBox_price.Text = "1919.57";
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
            if (textBox_location.Text == "")
            {
                textBox_location.Text = $"ст. {textBox_city.Text}";
                textBox_location.SelectionStart = textBox_location.Text.Length;
                textBox_location.SelectionLength = 0;

            }
        }
        void ComboBox_model_Click(object sender, EventArgs e)
        {
            comboBox_model.MaxLength = 99;
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
            if (comboBox_model.Text == "Icom IC-F3GT" || comboBox_model.Text == "Icom IC-F11")
            {
                textBox_serialNumber.MaxLength = 7;

                if (textBox_serialNumber.Text == "")
                {
                    textBox_serialNumber.Text = "0";
                    textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                    textBox_serialNumber.SelectionLength = 0;
                }
            }

            if (comboBox_model.Text == "Icom IC-F16" || comboBox_model.Text == "Icom IC-F3GS" || comboBox_model.Text == "Гранит Р33П-1" ||
                comboBox_model.Text == "Гранит Р-43" || comboBox_model.Text == "Радий-301")
            {
                textBox_serialNumber.MaxLength = 7;
            }

            if (comboBox_model.Text == "Motorola P040" || comboBox_model.Text == "Motorola P080")
            {
                textBox_serialNumber.MaxLength = 10;

                if (textBox_serialNumber.Text == "")
                {
                    textBox_serialNumber.Text = "442";
                    textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                    textBox_serialNumber.SelectionLength = 0;
                }
            }

            if (comboBox_model.Text == "Motorola DP-1400")
            {
                textBox_serialNumber.MaxLength = 10;

                if (textBox_serialNumber.Text == "")
                {
                    textBox_serialNumber.Text = "752";
                    textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                    textBox_serialNumber.SelectionLength = 0;
                }
            }

            if (comboBox_model.Text == "Motorola DP-2400" || comboBox_model.Text == "Motorola DP-2400е")
            {
                textBox_serialNumber.MaxLength = 10;

                if (textBox_serialNumber.Text == "")
                {
                    textBox_serialNumber.Text = "446";
                    textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                    textBox_serialNumber.SelectionLength = 0;
                }
            }

            if (comboBox_model.Text == "Motorola DP-4400")
            {
                textBox_serialNumber.MaxLength = 10;

                if (textBox_serialNumber.Text == "")
                {
                    textBox_serialNumber.Text = "807";
                    textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                    textBox_serialNumber.SelectionLength = 0;
                }
            }

            if (comboBox_model.Text == "Motorola GP-300")
            {
                textBox_serialNumber.MaxLength = 10;

                if (textBox_serialNumber.Text == "")
                {
                    textBox_serialNumber.Text = "174";
                    textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                    textBox_serialNumber.SelectionLength = 0;
                }
            }

            if (comboBox_model.Text == "Motorola GP-320" || comboBox_model.Text == "Kenwood ТК-2107" || comboBox_model.Text == "Vertex - 261"
                || comboBox_model.Text == "РА-160") //TODO Проверить условия а имеено зав номер GP320 Вертех Кенвуд РА
            {
                textBox_serialNumber.MaxLength = 10;

                if (textBox_serialNumber.Text == "")
                {
                    textBox_serialNumber.Text = "что-то";
                    textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                    textBox_serialNumber.SelectionLength = 0;
                }
            }

            if (comboBox_model.Text == "Motorola GP-340")
            {
                textBox_serialNumber.MaxLength = 10;

                if (textBox_serialNumber.Text == "")
                {
                    textBox_serialNumber.Text = "672";
                    textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                    textBox_serialNumber.SelectionLength = 0;
                }
            }

            if (comboBox_model.Text == "Motorola GP-360")
            {
                textBox_serialNumber.MaxLength = 10;

                if (textBox_serialNumber.Text == "")
                {
                    textBox_serialNumber.Text = "749";
                    textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                    textBox_serialNumber.SelectionLength = 0;
                }
            }

            if (comboBox_model.Text == "Альтавия-301М" || comboBox_model.Text == "Элодия-351М")
            {
                textBox_serialNumber.MaxLength = 9;
            }

            if (comboBox_model.Text == "РН311М")
            {
                textBox_serialNumber.MaxLength = 10;

            }

            if (comboBox_model.Text == "Comrade R5")
            {
                textBox_serialNumber.MaxLength = 12;

                if (textBox_serialNumber.Text == "")
                {
                    textBox_serialNumber.Text = "2010R";
                    textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                    textBox_serialNumber.SelectionLength = 0;
                }
            }

            if (comboBox_model.Text == "Комбат T-44")
            {
                textBox_serialNumber.MaxLength = 14;

                if (textBox_serialNumber.Text == "")
                {
                    textBox_serialNumber.Text = "T44.19.10.";
                    textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                    textBox_serialNumber.SelectionLength = 0;
                }
            }

            if (comboBox_model.Text == "РНД-500")
            {
                textBox_serialNumber.MaxLength = 4;
                textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                textBox_serialNumber.SelectionLength = 0;
            }

            if (comboBox_model.Text == "РНД-512")
            {
                textBox_serialNumber.MaxLength = 11;

                if (textBox_serialNumber.Text == "")
                {
                    textBox_serialNumber.Text = "0";
                    textBox_serialNumber.SelectionStart = textBox_serialNumber.Text.Length;
                    textBox_serialNumber.SelectionLength = 0;
                }

            }
        }

        void TextBox_serialNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
            {
                if (textBox_serialNumber.Text != "")
                {
                    var serialNumber = textBox_serialNumber.Text;

                    string querystring = $"SELECT * FROM radiostantion_full WHERE serialNumber = '{serialNumber}'";

                    MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        comboBox_poligon.Text = table.Rows[0].ItemArray[1].ToString();
                        textBox_company.Text = table.Rows[0].ItemArray[2].ToString();
                        textBox_location.Text = table.Rows[0].ItemArray[3].ToString();
                        comboBox_model.Text = table.Rows[0].ItemArray[4].ToString();
                        textBox_inventoryNumber.Text = table.Rows[0].ItemArray[6].ToString();
                        textBox_networkNumber.Text = table.Rows[0].ItemArray[7].ToString();
                        textBox_numberAct.Text = table.Rows[0].ItemArray[9].ToString();
                        textBox_city.Text = table.Rows[0].ItemArray[10].ToString();
                        textBox_representative.Text = table.Rows[0].ItemArray[12].ToString();
                        textBox_post.Text = table.Rows[0].ItemArray[13].ToString();
                        textBox_numberIdentification.Text = table.Rows[0].ItemArray[14].ToString();
                        textBox_dateIssue.Text = table.Rows[0].ItemArray[15].ToString();
                        textBox_phoneNumber.Text = table.Rows[0].ItemArray[16].ToString();
                    }
                }
            }
            else if (e.KeyCode == Keys.Return)
            {
                if (textBox_serialNumber.Text != "")
                {
                    var serialNumber = textBox_serialNumber.Text;

                    string querystring = $"SELECT * FROM radiostantion_full WHERE serialNumber = '{serialNumber}'";

                    MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());

                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);

                    DataTable table = new DataTable();

                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        comboBox_poligon.Text = table.Rows[0].ItemArray[1].ToString();
                        textBox_company.Text = table.Rows[0].ItemArray[2].ToString();
                        textBox_location.Text = table.Rows[0].ItemArray[3].ToString();
                        comboBox_model.Text = table.Rows[0].ItemArray[4].ToString();
                        textBox_inventoryNumber.Text = table.Rows[0].ItemArray[6].ToString();
                        textBox_networkNumber.Text = table.Rows[0].ItemArray[7].ToString();
                        //textBox_numberAct.Text = table.Rows[0].ItemArray[9].ToString();
                        textBox_city.Text = table.Rows[0].ItemArray[10].ToString();
                    }
                    else
                    {
                        textBox_inventoryNumber.Text = "";
                        textBox_networkNumber.Text = "";
                    }
                }
            }
        }

        void TextBox_serialNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region проверка ввода
            if (comboBox_model.Text == "Icom IC-F3GT" || comboBox_model.Text == "Icom IC-F11" || comboBox_model.Text == "Icom IC-F16"
                || comboBox_model.Text == "Icom IC-F3GS" || comboBox_model.Text == "Альтавия-301М" || comboBox_model.Text == "Элодия-351М"
                || comboBox_model.Text == "Гранит Р33П-1" || comboBox_model.Text == "Гранит Р-43" || comboBox_model.Text == "Радий-301"
                || comboBox_model.Text == "РНД-500")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
                {

                }
                else
                {
                    e.Handled = true;
                }
            }

            if (comboBox_model.Text == "Motorola P040" || comboBox_model.Text == "Motorola P080" || comboBox_model.Text == "Motorola DP-1400" ||
                comboBox_model.Text == "Motorola DP-2400" || comboBox_model.Text == "Motorola DP-2400е" || comboBox_model.Text == "Motorola DP-4400" ||
                comboBox_model.Text == "Motorola GP-300" || comboBox_model.Text == "Motorola GP-320" || comboBox_model.Text == "Motorola GP-340" ||
                comboBox_model.Text == "Motorola GP-360" || comboBox_model.Text == "Comrade R5")
            {
                if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
                {

                }
                else
                {
                    e.Handled = true;
                }

            }

            if (comboBox_model.Text == "РН311М" || comboBox_model.Text == "РНД-512")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == ' ')
                {

                }
                else
                {
                    e.Handled = true;
                }
            }

            if (comboBox_model.Text == "Комбат T-44")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == 84)
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
            if (textBox_numberIdentification.Text == "")
            {
                textBox_numberIdentification.Text = "V ";
                textBox_numberIdentification.SelectionStart = textBox_numberIdentification.Text.Length;
                textBox_numberIdentification.SelectionLength = 0;
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

            if (textBox_phoneNumber.Text == "")
            {
                textBox_phoneNumber.Text = "+7-";
                textBox_phoneNumber.SelectionStart = textBox_phoneNumber.Text.Length;
                textBox_phoneNumber.SelectionLength = 0;
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
            textBox_antenna.Text = "";
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
            if (textBox_antenna.Text == "")
            {
                textBox_antenna.Text = "-";
            }
        }
        void TextBox_manipulator_Click(object sender, EventArgs e)
        {
            textBox_manipulator.Text = "";
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
            if (textBox_manipulator.Text == "")
            {
                textBox_manipulator.Text = "-";
            }
        }
        void TextBox_AKB_Click(object sender, EventArgs e)
        {
            textBox_AKB.Text = "";
        }
        void TextBox_AKB_Leave(object sender, EventArgs e)
        {
            if (textBox_AKB.Text == "")
            {
                textBox_AKB.Text = "-";
            }
        }
        void TextBox_batteryСharger_Click(object sender, EventArgs e)
        {
            textBox_batteryСharger.Text = "";
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
            if (textBox_batteryСharger.Text == "")
            {
                textBox_batteryСharger.Text = "-";
            }
        }
        void TextBox_TextChanged()
        {
            if (comboBox_poligon.Text.Length > 0 && textBox_company.Text.Length > 0
                && textBox_location.Text.Length > 0
                && comboBox_model.Text.Length > 0 && textBox_serialNumber.Text.Length > 0
                && textBox_inventoryNumber.Text.Length > 0 && textBox_networkNumber.Text.Length > 0
                && textBox_dateTO.Text.Length > 0 && textBox_price.Text.Length > 0
                && textBox_numberAct.Text.Length > 0 && textBox_representative.Text.Length > 0
                && textBox_numberIdentification.Text.Length > 0 && textBox_phoneNumber.Text.Length > 0
                && textBox_post.Text.Length > 0 && textBox_dateIssue.Text.Length > 0)
            {
                button_save_add_rst.Enabled = true;
            }
            else
            {
                button_save_add_rst.Enabled = false;
            }
        }
        void AddRSTForm_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox_TextChanged();
        }
        #endregion

        #region очистка дат
        void PictureBox5_Click(object sender, EventArgs e)
        {
            textBox_dateTO.Text = "";
        }

        void PictureBox6_Click(object sender, EventArgs e)
        {
            textBox_dateIssue.Text = "";
        }
        #endregion

        #region возможность редактирования comBox_model
        void Button_Enable_editor_comBox_model_Click(object sender, EventArgs e)
        {
            if (comboBox_model.Text != "" && comboBox_model.DropDownStyle != ComboBoxStyle.DropDown)
            {
                comboBox_model.DropDownStyle = ComboBoxStyle.DropDown;
                button_model_radiostation_name.Enabled = true;
            }
        }
        #endregion

        #region добавление модели радиостанции в БД
        void Button_model_radiostation_name_MouseClick(object sender, MouseEventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите добавить модель радиостанции?";

            if (Internet_check.GetInstance.AvailabilityChanged_bool())
            {
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
                try
                {
                    DB.GetInstance.openConnection();
                    var addQuery = $"insert into model_radiostation (model_radiostation_name) VALUES ('{comboBox_model.Text}')";

                    MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection());
                    command.ExecuteNonQuery();

                    MessageBox.Show("Модель радиостанции успешно добавлена!");
                    DB.GetInstance.closeConnection();

                    comboBox_model.DropDownStyle = ComboBoxStyle.DropDownList;
                    button_model_radiostation_name.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка модель не добавленна!");
                    MessageBox.Show(ex.ToString());
                }
            }

        }


        #endregion

    }
}
