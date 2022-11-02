using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
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
            txB_dateTO.ReadOnly = true;
            txB_dateTO.Text = DateTime.Now.ToString("dd.MM.yyyy");

            txB_dateIssue.Text = DateTime.Now.ToString("dd.MM.yyyy");
            btn_save_add_rst.Enabled = false;
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
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    var re = new Regex(Environment.NewLine);
                    control.Text = re.Replace(control.Text, " ");
                }
            }
            Add_rst_radiostantion();
        }

        void Add_rst_radiostantion()
        {
            if(!String.IsNullOrEmpty(txB_numberAct.Text))
            {
                if (Internet_check.CheackSkyNET())
                {
                    string Mesage;
                    Mesage = "Вы действительно хотите добавить радиостанцию?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                    try
                    {
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

                            DateTime.Parse(dateIssue).ToString("dd.MM.yyyy");
                            if (!(poligon == "") && !(company == "") && !(location == "") && !(model == "")
                            && !(serialNumber == "") && !(dateTO == "") && !(numberAct == "") && !(city == "")
                            && !(representative == "") && !(post == "") && !(numberIdentification == "")
                            && !(phoneNumber == "") && !(antenna == "")
                            && !(manipulator == "") && !(AKB == "") && !(batteryСharger == ""))
                            {
                                #region проверка на ввод зав. номера рст
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
                                            $"parts_5, parts_6, parts_7, decommissionSerialNumber, comment) VALUES ('{poligon.Trim()}', '{company.Trim()}', '{location.Trim()}'," +
                                            $"'{model.Trim()}','{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}', " +
                                            $"'{dateTO.Trim()}','{numberAct.Trim()}','{city.Trim()}','{price.Trim()}', '{representative.Trim()}', '{post.Trim()}', " +
                                            $"'{numberIdentification.Trim()}', '{dateIssue.Trim()}', '{phoneNumber.Trim()}', '{""}', '{""}', '{0.00}'," +
                                            $"'{antenna.Trim()}', '{manipulator.Trim()}', '{AKB.Trim()}', '{batteryСharger.Trim()}', '{""}', '{""}', " +
                                            $"'{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{comment.Trim()}')";


                                        using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                                        {
                                            DB.GetInstance.OpenConnection();
                                            command.ExecuteNonQuery();
                                            DB.GetInstance.CloseConnection();
                                            Add_rst_radiostantion_full();
                                            MessageBox.Show("Радиостанция успешно добавлена!");
                                            txB_serialNumber.Text = "";
                                            txB_inventoryNumber.Text = "";
                                            txB_networkNumber.Text = "";
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
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ошибка! Радиостнация не добавлена!(Add_rst_radiostantion)");
                    }
                }
            }
            else MessageBox.Show("Заполни номер акта");
        }
        void Add_rst_radiostantion_full()
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
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
                                            $"parts_5, parts_6, parts_7, decommissionSerialNumber, comment) VALUES ('{poligon.Trim()}', '{company.Trim()}', '{location.Trim()}'," +
                                            $"'{model.Trim()}','{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}', " +
                                            $"'{dateTO.Trim()}','{numberAct.Trim()}','{city.Trim()}','{price.Trim()}', '{representative.Trim()}', '{post.Trim()}', " +
                                            $"'{numberIdentification.Trim()}', '{dateIssue.Trim()}', '{phoneNumber.Trim()}', '{""}', '{""}', '{0.00}'," +
                                            $"'{antenna.Trim()}', '{manipulator.Trim()}', '{AKB.Trim()}', '{batteryСharger.Trim()}', '{""}', '{""}', " +
                                            $"'{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{""}', '{comment}')";

                            using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.OpenConnection();
                                command.ExecuteNonQuery();
                                DB.GetInstance.CloseConnection();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка! Радиостнация не добавлена в общую БД!(CheacSerialNumber_radiostantion_full)");
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

        #region проверка в таблице radiostantion_full и если есть изменение записей
        Boolean CheacSerialNumber_radiostantion_full(string serialNumber)
        {
            if (Internet_check.CheackSkyNET())
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
                                    DB.GetInstance.OpenConnection();
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

                                    var updateQuery = $"UPDATE radiostantion_full SET model = '{model}', inventoryNumber = '{inventoryNumber}', networkNumber = '{networkNumber}', dateTO = '{dateTO}', numberAct = '{numberAct}', representative = '{representative}', numberIdentification = '{numberIdentification}', phoneNumber = '{phoneNumber}', post = '{post}', dateIssue = '{dateIssue}' WHERE serialNumber = '{serialNumber}'";

                                    using (MySqlCommand command5 = new MySqlCommand(updateQuery, DB.GetInstance.GetConnection()))
                                    {
                                        command5.ExecuteNonQuery();
                                    }
                                    DB.GetInstance.CloseConnection();
                                }
                                catch (Exception )
                                {
                                    MessageBox.Show("Ошибка! При добавлении в текущую БД найденная радиостанция в общей БД не изменена!(CheacSerialNumber_radiostantion_full)");
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
        void TextBox_company_KeyUp(object sender, KeyEventArgs e)
        {
            ProcessKbdCtrlShortcuts(sender, e);
        }
        void TextBox_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != ' ')
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
                txB_location.SelectionStart = txB_location.Text.Length;
                txB_location.SelectionLength = 0;

            }
        }
        void ComboBox_model_Click(object sender, EventArgs e)
        {
            cmB_model.MaxLength = 99;
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
                    txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                    txB_serialNumber.SelectionLength = 0;
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
                    txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                    txB_serialNumber.SelectionLength = 0;
                }
            }

            if (cmB_model.Text == "Motorola DP-1400")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "752";
                    txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                    txB_serialNumber.SelectionLength = 0;
                }
            }

            if (cmB_model.Text == "Motorola DP-2400" || cmB_model.Text == "Motorola DP-2400е")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "446";
                    txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                    txB_serialNumber.SelectionLength = 0;
                }
            }

            if (cmB_model.Text == "Motorola DP-4400")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "807";
                    txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                    txB_serialNumber.SelectionLength = 0;
                }
            }

            if (cmB_model.Text == "Motorola GP-300")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "174";
                    txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                    txB_serialNumber.SelectionLength = 0;
                }
            }

            if (cmB_model.Text == "Motorola GP-320" || cmB_model.Text == "Kenwood ТК-2107" || cmB_model.Text == "Vertex - 261"
                || cmB_model.Text == "РА-160") //TODO Проверить условия а имеено зав номер GP320 Вертех Кенвуд РА
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "что-то";
                    txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                    txB_serialNumber.SelectionLength = 0;
                }
            }

            if (cmB_model.Text == "Motorola GP-340")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "672";
                    txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                    txB_serialNumber.SelectionLength = 0;
                }
            }

            if (cmB_model.Text == "Motorola GP-360")
            {
                txB_serialNumber.MaxLength = 10;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "749";
                    txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                    txB_serialNumber.SelectionLength = 0;
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
                    txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                    txB_serialNumber.SelectionLength = 0;
                }
            }

            if (cmB_model.Text == "Комбат T-44")
            {
                txB_serialNumber.MaxLength = 14;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "T44.19.10.";
                    txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                    txB_serialNumber.SelectionLength = 0;
                }
            }

            if (cmB_model.Text == "РНД-500")
            {
                txB_serialNumber.MaxLength = 4;
                txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                txB_serialNumber.SelectionLength = 0;
            }

            if (cmB_model.Text == "РНД-512")
            {
                txB_serialNumber.MaxLength = 11;

                if (txB_serialNumber.Text == "")
                {
                    txB_serialNumber.Text = "0";
                    txB_serialNumber.SelectionStart = txB_serialNumber.Text.Length;
                    txB_serialNumber.SelectionLength = 0;
                }

            }
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
                txB_numberIdentification.SelectionStart = txB_numberIdentification.Text.Length;
                txB_numberIdentification.SelectionLength = 0;
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

            if (txB_phoneNumber.Text == "")
            {
                txB_phoneNumber.Text = "+7-";
                txB_phoneNumber.SelectionStart = txB_phoneNumber.Text.Length;
                txB_phoneNumber.SelectionLength = 0;
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
            if(txB_AKB.Text == "-")
            {
                txB_AKB.Text = "";
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
        void AddRSTForm_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox_TextChanged();
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
