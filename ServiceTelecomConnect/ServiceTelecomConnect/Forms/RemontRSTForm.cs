using MySql.Data.MySqlClient;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    public partial class RemontRSTForm : Form
    {
        private delegate DialogResult ShowOpenFileDialogInvoker(); // делаг для invoke
        public RemontRSTForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            var myCulture = new CultureInfo("ru-RU");
            myCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = myCulture;
        }

        void Button_save_add_rst_remont_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                string Mesage;
                Mesage = "Вы действительно хотите добавить ремонт?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                try
                {
                    var numberActRemont = txB_numberActRemont.Text;
                    var сategory = cmB_сategory.Text;
                    var priceRemont = txB_priceRemont.Text;
                    var сompleted_works_1 = txB_сompleted_works_1.Text;
                    var сompleted_works_2 = txB_сompleted_works_2.Text;
                    var сompleted_works_3 = txB_сompleted_works_3.Text;
                    var сompleted_works_4 = txB_сompleted_works_4.Text;
                    var сompleted_works_5 = txB_сompleted_works_5.Text;
                    var сompleted_works_6 = txB_сompleted_works_6.Text;
                    var сompleted_works_7 = txB_сompleted_works_7.Text;
                    var parts_1 = txB_parts_1.Text;
                    var parts_2 = txB_parts_2.Text;
                    var parts_3 = txB_parts_3.Text;
                    var parts_4 = txB_parts_4.Text;
                    var parts_5 = txB_parts_5.Text;
                    var parts_6 = txB_parts_6.Text;
                    var parts_7 = txB_parts_7.Text;
                    var serialNumber = txB_serialNumber.Text;
                    var mainMeans = txB_MainMeans.Text;
                    var nameProductRepaired = txB_NameProductRepaired.Text;


                    if (!(numberActRemont == "") && !(сategory == "") && !(priceRemont == "") && !(сompleted_works_1 == "") && !(parts_1 == ""))
                    {
                        var changeQuery = $"UPDATE radiostantion SET numberActRemont = '{numberActRemont.Trim()}', category = '{сategory}', " +
                            $"priceRemont = '{priceRemont}', completed_works_1 = '{сompleted_works_1.Trim()}', completed_works_2 = '{сompleted_works_2.Trim()}', " +
                            $"completed_works_3 = '{сompleted_works_3.Trim()}', completed_works_4 = '{сompleted_works_4.Trim()}', " +
                            $"completed_works_5 = '{сompleted_works_5.Trim()}', completed_works_6 = '{сompleted_works_6.Trim()}', " +
                            $"completed_works_7 = '{сompleted_works_7.Trim()}', parts_1 = '{parts_1.Trim()}', parts_2 = '{parts_2.Trim()}', " +
                            $"parts_3 = '{parts_3.Trim()}', parts_4 = '{parts_4.Trim()}', parts_5 = '{parts_5.Trim()}', parts_6 = '{parts_6.Trim()}', parts_7 = '{parts_7.Trim()}'" +
                            $"WHERE serialNumber = '{serialNumber}'";
                        using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            command.ExecuteNonQuery();
                            DB.GetInstance.CloseConnection();
                        }
                        if (CheacSerialNumber.GetInstance.CheacSerialNumber_OC6(serialNumber))
                        {
                            var changeQueryOC = $"UPDATE OC6 SET mainMeans = '{mainMeans}', nameProductRepaired = '{nameProductRepaired}' WHERE serialNumber = '{serialNumber}'";
                            using (MySqlCommand command2 = new MySqlCommand(changeQueryOC, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.OpenConnection();
                                command2.ExecuteNonQuery();
                                DB.GetInstance.CloseConnection();
                            }
                        }
                        else
                        {
                            var addQueryOC = $"INSERT INTO OC6 (serialNumber, mainMeans, nameProductRepaired) " +
                                $"VALUES ('{serialNumber.Trim()}','{mainMeans.Trim()}','{nameProductRepaired.Trim()}')";

                            using (MySqlCommand command3 = new MySqlCommand(addQueryOC, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.OpenConnection();
                                command3.ExecuteNonQuery();
                                DB.GetInstance.CloseConnection();
                            }

                        }

                        MessageBox.Show("Ремонт успешно добавлен!");

                    }
                    else
                    {
                        MessageBox.Show("Вы не заполнили нужные поля со (*)!");
                    }


                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка! Ремонт не добавлен!(Button_save_add_rst_Click)");
                }
            }
        }
        void PictureBox4_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите очистить все введенные вами поля?";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            txB_numberActRemont.Text = "";
            cmB_сategory.Text = "";
            txB_priceRemont.Text = "";
            txB_сompleted_works_1.Text = "";
            txB_сompleted_works_2.Text = "";
            txB_сompleted_works_3.Text = "";
            txB_сompleted_works_4.Text = "";
            txB_сompleted_works_5.Text = "";
            txB_сompleted_works_6.Text = "";
            txB_сompleted_works_7.Text = "";
            txB_parts_1.Text = "";
            txB_parts_2.Text = "";
            txB_parts_3.Text = "";
            txB_parts_4.Text = "";
            txB_parts_5.Text = "";
            txB_parts_6.Text = "";
            txB_parts_7.Text = "";
        }

        void PictureBox3_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Перед вами форма добавление Ремонта!";

            if (MessageBox.Show(Mesage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                return;
            }
        }

        void ComboBox_сategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmB_сategory.Text == "3")
            {
                if (txB_model.Text == "Icom IC-F3GT"
                || txB_model.Text == "Icom IC-F11" || txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F3GS"
                || txB_model.Text == "Motorola P040" || txB_model.Text == "Motorola P080" || txB_model.Text == "Motorola GP-300"
                || txB_model.Text == "Motorola GP-320" || txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-360"
                || txB_model.Text == "Альтавия-301М" || txB_model.Text == "Comrade R5" || txB_model.Text == "Гранит Р33П-1"
                || txB_model.Text == "Гранит Р-43" || txB_model.Text == "Радий-301" || txB_model.Text == "Kenwood ТК-2107"
                || txB_model.Text == "Vertex - 261")
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
                if (txB_model.Text == "Icom IC-F3GT"
                || txB_model.Text == "Icom IC-F11" || txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F3GS"
                || txB_model.Text == "Motorola P040" || txB_model.Text == "Motorola P080" || txB_model.Text == "Motorola GP-300"
                || txB_model.Text == "Motorola GP-320" || txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-360"
                || txB_model.Text == "Альтавия-301М" || txB_model.Text == "Comrade R5" || txB_model.Text == "Гранит Р33П-1"
                || txB_model.Text == "Гранит Р-43" || txB_model.Text == "Радий-301" || txB_model.Text == "Kenwood ТК-2107"
                || txB_model.Text == "Vertex - 261")
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
                if (txB_model.Text == "Icom IC-F3GT"
                || txB_model.Text == "Icom IC-F11" || txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F3GS"
                || txB_model.Text == "Motorola P040" || txB_model.Text == "Motorola P080" || txB_model.Text == "Motorola GP-300"
                || txB_model.Text == "Motorola GP-320" || txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-360"
                || txB_model.Text == "Альтавия-301М" || txB_model.Text == "Comrade R5" || txB_model.Text == "Гранит Р33П-1"
                || txB_model.Text == "Гранит Р-43" || txB_model.Text == "Радий-301" || txB_model.Text == "Kenwood ТК-2107"
                || txB_model.Text == "Vertex - 261")
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
                if (txB_model.Text == "Icom IC-F3GT"
                || txB_model.Text == "Icom IC-F11" || txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F3GS"
                || txB_model.Text == "Motorola P040" || txB_model.Text == "Motorola P080" || txB_model.Text == "Motorola GP-300"
                || txB_model.Text == "Motorola GP-320" || txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-360"
                || txB_model.Text == "Альтавия-301М" || txB_model.Text == "Comrade R5" || txB_model.Text == "Гранит Р33П-1"
                || txB_model.Text == "Гранит Р-43" || txB_model.Text == "Радий-301" || txB_model.Text == "Kenwood ТК-2107"
                || txB_model.Text == "Vertex - 261")
                {
                    txB_priceRemont.Text = "5071.94";
                }
                else
                {
                    txB_priceRemont.Text = "5119.51";
                }
            }
        }
        void TextBox_TextChanged()
        {
            if (txB_numberActRemont.Text.Length > 0 && txB_сompleted_works_1.Text.Length > 0
                && txB_parts_1.Text.Length > 0)
            {
                txB_сompleted_works_2.ReadOnly = false;
                txB_parts_2.ReadOnly = false;
                btn_save_add_rst_remont.Enabled = true;
                if (txB_сompleted_works_2.Text.Length > 0 && txB_parts_2.Text.Length > 0)
                {
                    txB_сompleted_works_3.ReadOnly = false;
                    txB_parts_3.ReadOnly = false;
                    if (txB_сompleted_works_3.Text.Length > 0 && txB_parts_3.Text.Length > 0)
                    {
                        txB_сompleted_works_4.ReadOnly = false;
                        txB_parts_4.ReadOnly = false;
                        if (txB_сompleted_works_4.Text.Length > 0 && txB_parts_4.Text.Length > 0)
                        {
                            txB_сompleted_works_5.ReadOnly = false;
                            txB_parts_5.ReadOnly = false;
                            if (txB_сompleted_works_5.Text.Length > 0 && txB_parts_5.Text.Length > 0)
                            {
                                txB_сompleted_works_6.ReadOnly = false;
                                txB_parts_6.ReadOnly = false;
                                if (txB_сompleted_works_6.Text.Length > 0 && txB_parts_6.Text.Length > 0)
                                {
                                    txB_сompleted_works_7.ReadOnly = false;
                                    txB_parts_7.ReadOnly = false;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                btn_save_add_rst_remont.Enabled = false;
            }
        }

        void RemontRSTForm_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox_TextChanged();
        }
        void Label_company_DoubleClick(object sender, EventArgs e)
        {
            cmb_remont_select.Visible = true;
        }

        #region лайфхак для ремонтов
        void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmB_сategory.Text == "6")
            {
                if (cmb_remont_select.Text == "1")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена батарейных контактов";
                        txB_parts_1.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_2.Text = "Замена уплотнителя О-кольца";
                        txB_parts_2.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена шлейфа";
                        txB_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "LN9820 Заглушка для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "50012013001 Динамик";

                        txB_сompleted_works_2.Text = "Замена накладки РТТ";
                        txB_parts_2.Text = "HN000696A01 Накладка РТТ";

                        txB_сompleted_works_3.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_3.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_4.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_4.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_5.Text = "Замена О кольца";
                        txB_parts_5.Text = "32012111001 О кольцо";

                        txB_сompleted_works_6.Text = "Замена контактов АКБ";
                        txB_parts_6.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_7.Text = "Замена верхнего уплотнителя";
                        txB_parts_7.Text = "32012089001 Верхний уплотнитель";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена антенного разъема";
                        txB_parts_1.Text = "Антенный разъем для F3G (8950005260)";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "K036NA500-66 Динамик для IC-44088 ";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена фильтра";
                        txB_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "Заглушка МР37";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена антенного разъема";
                        txB_parts_1.Text = "Антенный разъем для F3GT";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "K036NA500-66 Динамик для IC-44088 ";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена фильтра";
                        txB_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "Заглушка МР37";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена антенного разъема";
                        txB_parts_1.Text = "Антенный разъем для F16 (6910015910)";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "K036NA500-66 Динамик для IC-44088 ";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена фильтра";
                        txB_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "Заглушка МР37";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "2")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_1.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_2.Text = "Замена войлока GP-340";
                        txB_parts_2.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена шлейфа";
                        txB_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии ";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_6.Text = "Замена батарейных контактов";
                        txB_parts_6.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "50012013001 Динамик";

                        txB_сompleted_works_2.Text = "Замена контактов АКБ";
                        txB_parts_2.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_3.Text = "Замена верхнего уплотнителя";
                        txB_parts_3.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_4.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_4.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_5.Text = "Замена ручки переключения каналов";
                        txB_parts_5.Text = "36012017001 Ручка переключения каналов";

                        txB_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        txB_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена сетки динамика";
                        txB_parts_7.Text = "35012060001 Сетка динамика";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088 ";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена разъёма";
                        txB_parts_5.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_7.Text = "Замена ручки";
                        txB_parts_7.Text = "KNOB N-276 Ручка регулятора громкости";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3GT";

                        txB_сompleted_works_5.Text = "Замена разъёма";
                        txB_parts_5.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "Регулятор громкости для F3GT";

                        txB_сompleted_works_7.Text = "Замена ручки";
                        txB_parts_7.Text = "Ручка регулятора громкости для F3GT";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для IC-F16 (2260002840)";

                        txB_сompleted_works_5.Text = "Замена разъёма";
                        txB_parts_5.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "Регулятор громкости для F16";

                        txB_сompleted_works_7.Text = "Замена ручки";
                        txB_parts_7.Text = "Ручка регулятора громкости для F16";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                }

                if (cmb_remont_select.Text == "3")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена войлока GP-340";
                        txB_parts_1.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        txB_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_6.Text = "Замена шлейфа";
                        txB_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_1.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_2.Text = "Замена верхнего уплотнителя";
                        txB_parts_2.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_3.Text = "Замена контактов АКБ";
                        txB_parts_3.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_4.Text = "Замена регулятора громкости";
                        txB_parts_4.Text = "1875103С04 Регулятор громкости";

                        txB_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        txB_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена кнопки РТТ";
                        txB_parts_6.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_7.Text = "Замена накладки РТТ";
                        txB_parts_7.Text = "HN000696A01 Накладка РТТ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена транзистора";
                        txB_parts_1.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена резины";
                        txB_parts_3.Text = "Резина МР12";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "Замена панели защелки";
                        txB_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_7.Text = "Замена контакта + ";
                        txB_parts_7.Text = "2251 PLUS TERMINAL Контакт  + ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена транзистора";
                        txB_parts_1.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена резины";
                        txB_parts_3.Text = "Резина МР12";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "Замена панели защелки";
                        txB_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_7.Text = "Замена контакта + ";
                        txB_parts_7.Text = "2251 PLUS TERMINAL Контакт  + ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена транзистора";
                        txB_parts_1.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена резины";
                        txB_parts_3.Text = "Резина МР12";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "Замена панели защелки";
                        txB_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_7.Text = "Замена контакта + ";
                        txB_parts_7.Text = "2251 PLUS TERMINAL Контакт  + ";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "4")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена фронтальной наклейки";
                        txB_parts_1.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена ручки регулятора громкости";
                        txB_parts_5.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_6.Text = "Замена шлейфа";
                        txB_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена верхнего уплотнителя";
                        txB_parts_1.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_2.Text = "Замена контактов АКБ";
                        txB_parts_2.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_3.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_3.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_4.Text = "Замена регулятора громкости";
                        txB_parts_4.Text = "1875103С04 Регулятор громкости";

                        txB_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        txB_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена динамика";
                        txB_parts_6.Text = "50012013001 Динамик";

                        txB_сompleted_works_7.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_7.Text = "32012110001 Уплотнитель контактов АКБ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена антенного разъема";
                        txB_parts_2.Text = "Антенный разъем для F3G (8950005260)";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "Регулятор громкости TP76NOON-15F-A103-2251 ";

                        txB_сompleted_works_7.Text = "Замена ручки";
                        txB_parts_7.Text = "KNOB N-276 Ручка регулятора громкости";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена антенного разъема";
                        txB_parts_2.Text = "Антенный разъем для F3G (8950005260)";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "Регулятор громкости TP76NOON-15F-A103-2251 ";

                        txB_сompleted_works_7.Text = "Замена ручки";
                        txB_parts_7.Text = "KNOB N-276 Ручка регулятора громкости";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена антенного разъема";
                        txB_parts_2.Text = "Антенный разъем для F3G (8950005260)";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "Регулятор громкости TP76NOON-15F-A103-2251 ";

                        txB_сompleted_works_7.Text = "Замена ручки";
                        txB_parts_7.Text = "KNOB N-276 Ручка регулятора громкости";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "5")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена герметика верхней панели";
                        txB_parts_1.Text = "3280533Z05 Герметик верхней панели";

                        txB_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        txB_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_5.Text = "Замена батарейных контактов";
                        txB_parts_5.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_6.Text = "Замена шлейфа";
                        txB_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена контактов АКБ";
                        txB_parts_1.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_3.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_3.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "50012013001 Динамик";

                        txB_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        txB_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена кнопки РТТ";
                        txB_parts_7.Text = "4070354A01 Кнопка РТТ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2974";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_6.Text = "Замена контакта -";
                        txB_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_7.Text = "Замена клавиатуры";
                        txB_parts_7.Text = "Клавиатура 2251 MAIN SEAL";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2974";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_6.Text = "Замена контакта -";
                        txB_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_7.Text = "Замена клавиатуры";
                        txB_parts_7.Text = "Клавиатура 2251 MAIN SEAL";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2974";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_6.Text = "Замена контакта -";
                        txB_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_7.Text = "Замена клавиатуры";
                        txB_parts_7.Text = "Клавиатура 2251 MAIN SEAL";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "6")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена липучки интерфейсного разъёма";
                        txB_parts_1.Text = "1386058A01 Липучка интерфейсного разъема";

                        txB_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        txB_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        txB_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_5.Text = "Замена шлейфа";
                        txB_parts_5.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_6.Text = "Замена батарейных контактов";
                        txB_parts_6.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        txB_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_4.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_4.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "50012013001 Динамик";

                        txB_сompleted_works_6.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_6.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_7.Text = "Замена контактов АКБ";
                        txB_parts_7.Text = "0915184H01 Контакты АКБ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена клавиатуры";
                        txB_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        txB_сompleted_works_6.Text = "Замена корпуса";
                        txB_parts_6.Text = "Корпус IC-F3GS";

                        txB_сompleted_works_7.Text = "Замена прокладки";
                        txB_parts_7.Text = "2251 JACK PANEL Прокладка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена клавиатуры";
                        txB_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        txB_сompleted_works_6.Text = "Замена корпуса";
                        txB_parts_6.Text = "Корпус IC-F3GT";

                        txB_сompleted_works_7.Text = "Замена прокладки";
                        txB_parts_7.Text = "2251 JACK PANEL Прокладка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена клавиатуры";
                        txB_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        txB_сompleted_works_6.Text = "Замена корпуса";
                        txB_parts_6.Text = "Корпус для IC-F16 (с вклееным динамиком защелкой АКБ линзой)";

                        txB_сompleted_works_7.Text = "Замена прокладки";
                        txB_parts_7.Text = "2251 JACK PANEL Прокладка";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "7")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена микрофона";
                        txB_parts_3.Text = "5015027H01 Микрофон для GP-340";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена шлейфа";
                        txB_parts_5.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_6.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_6.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена микрофона";
                        txB_parts_2.Text = "5015027H01 Микрофон для DP2400";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена контактов АКБ";
                        txB_parts_4.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_5.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_5.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_6.Text = "Замена О кольца";
                        txB_parts_6.Text = "32012111001 О кольцо";

                        txB_сompleted_works_7.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_7.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена прокладки";
                        txB_parts_1.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_4.Text = "Замена защелки АКБ";
                        txB_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_5.Text = "Замена микросхемы";
                        txB_parts_5.Text = "Микросхема TA31136FN8 EL IC";

                        txB_сompleted_works_6.Text = "Замена кнопки";
                        txB_parts_6.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена прокладки";
                        txB_parts_1.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_4.Text = "Замена защелки АКБ";
                        txB_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_5.Text = "Замена микросхемы";
                        txB_parts_5.Text = "Микросхема TA31136FN8 EL IC";

                        txB_сompleted_works_6.Text = "Замена кнопки";
                        txB_parts_6.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена прокладки";
                        txB_parts_1.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_4.Text = "Замена защелки АКБ";
                        txB_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_5.Text = "Замена микросхемы";
                        txB_parts_5.Text = "Микросхема TA31136FN8 EL IC";

                        txB_сompleted_works_6.Text = "Замена кнопки";
                        txB_parts_6.Text = "Кнопка РТТ для IC-F16 (2260002840)";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "8")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена передней панели";
                        txB_parts_3.Text = "1580666Z03 Передняя панель для GP-340";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена липучки интерфейсного разъёма";
                        txB_parts_5.Text = "1386058A01 Липучка интерфейсного разъема";

                        txB_сompleted_works_6.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_6.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена контактов АКБ";
                        txB_parts_2.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_3.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_3.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_4.Text = "Замена верхнего уплотнителя";
                        txB_parts_4.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_5.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_5.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        txB_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_7.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена фильтра";
                        txB_parts_3.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена прокладки";
                        txB_parts_6.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_7.Text = "Замена заглушки";
                        txB_parts_7.Text = "Заглушка МР37";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена фильтра";
                        txB_parts_3.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена прокладки";
                        txB_parts_6.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_7.Text = "Замена заглушки";
                        txB_parts_7.Text = "Заглушка МР37";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена фильтра";
                        txB_parts_3.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена прокладки";
                        txB_parts_6.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_7.Text = "Замена заглушки";
                        txB_parts_7.Text = "Заглушка МР37";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "9")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки переключателя каналов";
                        txB_parts_1.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_6.Text = "Замена герметика верхней панели";
                        txB_parts_6.Text = "3280533Z05 Герметик верхней панели";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_3.Text = "Замена контактов АКБ";
                        txB_parts_3.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "50012013001 Динамик";

                        txB_сompleted_works_5.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_5.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_6.Text = "Замена кнопки РТТ";
                        txB_parts_6.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_7.Text = "Замена верхнего уплотнителя";
                        txB_parts_7.Text = "32012089001 Верхний уплотнитель";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена фильтра";
                        txB_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_6.Text = "Замена антенного разъема";
                        txB_parts_6.Text = "Антенный разъем для F3G (8950005260)";

                        txB_сompleted_works_7.Text = "Замена заглушки";
                        txB_parts_7.Text = "Заглушка МР37";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена фильтра";
                        txB_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_6.Text = "Замена антенного разъема";
                        txB_parts_6.Text = "Антенный разъем для F3G (8950005260)";

                        txB_сompleted_works_7.Text = "Замена заглушки";
                        txB_parts_7.Text = "Заглушка МР37";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена фильтра";
                        txB_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_6.Text = "Замена антенного разъема";
                        txB_parts_6.Text = "Антенный разъем для F16";

                        txB_сompleted_works_7.Text = "Замена заглушки";
                        txB_parts_7.Text = "Заглушка МР37";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "10")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена войлока GP-340";
                        txB_parts_1.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_2.Text = "Замена микросхемы";
                        txB_parts_2.Text = "5185963A27 Микросхема синтезатора WARIS";

                        txB_сompleted_works_3.Text = "Замена липучки интерфейсного разъёма";
                        txB_parts_3.Text = "1386058A01 Липучка интерфейсного разъема";

                        txB_сompleted_works_4.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_4.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_5.Text = "Замена уплотнителя О-кольца";
                        txB_parts_5.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        txB_сompleted_works_6.Text = "Замена фронтальной наклейки";
                        txB_parts_6.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_2.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "50012013001 Динамик";

                        txB_сompleted_works_4.Text = "Замена  ручки регулятора громкости";
                        txB_parts_4.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена регулятора громкости";
                        txB_parts_5.Text = "1875103С04 Регулятор громкости";

                        txB_сompleted_works_6.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_6.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_7.Text = "Замена контактов АКБ";
                        txB_parts_7.Text = "0915184H01 Контакты АКБ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена разъёма";
                        txB_parts_1.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_2.Text = "Замена регулятора громкости";
                        txB_parts_2.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "Замена ручки";
                        txB_parts_6.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена разъёма";
                        txB_parts_1.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_2.Text = "Замена регулятора громкости";
                        txB_parts_2.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "Замена ручки";
                        txB_parts_6.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена разъёма";
                        txB_parts_1.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_2.Text = "Замена регулятора громкости";
                        txB_parts_2.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F16";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "Замена ручки";
                        txB_parts_6.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "11")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена войлока GP-340";
                        txB_parts_1.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_4.Text = "Замена шлейфа";
                        txB_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_5.Text = "Замена микрофона";
                        txB_parts_5.Text = "5015027H01 Микрофон для GP-340";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "HLN9820 Заглушка для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена О кольца";
                        txB_parts_4.Text = "32012111001 О кольцо";

                        txB_сompleted_works_4.Text = "Замена контактов АКБ";
                        txB_parts_4.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_5.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_5.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_6.Text = "Замена верхнего уплотнителя";
                        txB_parts_6.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_7.Text = "Замена микрофона";
                        txB_parts_7.Text = "5015027H01 Микрофон для DP2400";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена транзистора";
                        txB_parts_3.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_4.Text = "Замена прокладки";
                        txB_parts_4.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_5.Text = "Замена разъёма";
                        txB_parts_5.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_6.Text = "Замена контакта + ";
                        txB_parts_6.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_7.Text = "Замена панели защелки";
                        txB_parts_7.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена транзистора";
                        txB_parts_3.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_4.Text = "Замена прокладки";
                        txB_parts_4.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_5.Text = "Замена разъёма";
                        txB_parts_5.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_6.Text = "Замена контакта + ";
                        txB_parts_6.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_7.Text = "Замена панели защелки";
                        txB_parts_7.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена транзистора";
                        txB_parts_3.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_4.Text = "Замена прокладки";
                        txB_parts_4.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_5.Text = "Замена разъёма";
                        txB_parts_5.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_6.Text = "Замена контакта + ";
                        txB_parts_6.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_7.Text = "Замена панели защелки";
                        txB_parts_7.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                }

                if (cmb_remont_select.Text == "12")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        txB_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        txB_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_5.Text = "Замена батарейных контактов";
                        txB_parts_5.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_6.Text = "Замена шлейфа";
                        txB_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена контактов АКБ";
                        txB_parts_1.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_3.Text = "Замена верхнего уплотнителя";
                        txB_parts_3.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_4.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_4.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        txB_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "Замена О кольца";
                        txB_parts_7.Text = "32012111001 О кольцо";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена регулятора громкости";
                        txB_parts_1.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_7.Text = "Замена антенного разъема";
                        txB_parts_7.Text = "Антенный разъем для F3G (8950005260)";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена регулятора громкости";
                        txB_parts_1.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_7.Text = "Замена антенного разъема";
                        txB_parts_7.Text = "Антенный разъем для F3G (8950005260)";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена регулятора громкости";
                        txB_parts_1.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена кнопки";
                        txB_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "Замена заглушки";
                        txB_parts_6.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_7.Text = "Замена антенного разъема";
                        txB_parts_7.Text = "Антенный разъем для F16";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "13")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена войлока GP-340";
                        txB_parts_1.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_2.Text = "Замена ручки переключателя каналов";
                        txB_parts_2.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_4.Text = "Замена батарейных контактов";
                        txB_parts_4.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_5.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_5.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_6.Text = "Замена шлейфа";
                        txB_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_1.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_4.Text = "Замена контактов АКБ";
                        txB_parts_4.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_6.Text = "Замена верхнего уплотнителя";
                        txB_parts_6.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_7.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_7.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена прокладки";
                        txB_parts_4.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2974";

                        txB_сompleted_works_6.Text = "Замена контакта -";
                        txB_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена прокладки";
                        txB_parts_4.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2974";

                        txB_сompleted_works_6.Text = "Замена контакта -";
                        txB_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена уплотнителя";
                        txB_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_4.Text = "Замена прокладки";
                        txB_parts_4.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2974";

                        txB_сompleted_works_6.Text = "Замена контакта -";
                        txB_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "14")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_1.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_2.Text = "Замена микросхемы";
                        txB_parts_2.Text = "5185963A27 Микросхема синтезатора WARIS";

                        txB_сompleted_works_3.Text = "Замена герметика верхней панели";
                        txB_parts_3.Text = "3280533Z05 Герметик верхней панели";

                        txB_сompleted_works_4.Text = "Замена клавиатуры РТТ";
                        txB_parts_4.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        txB_сompleted_works_5.Text = "Замена уплотнителя О-кольца";
                        txB_parts_5.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        txB_сompleted_works_6.Text = "Замена фронтальной наклейки";
                        txB_parts_6.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена контактов АКБ";
                        txB_parts_1.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        txB_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена ручки переключения каналов";
                        txB_parts_6.Text = "36012017001 Ручка переключения каналов";

                        txB_сompleted_works_7.Text = "Замена переключателя каналов 16 позиций";
                        txB_parts_7.Text = "40012029001 Переключатель каналов 16 позиций";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "Замена корпуса";
                        txB_parts_6.Text = "Корпус IC-F3GS";

                        txB_сompleted_works_7.Text = "Замена кнопки РТТ";
                        txB_parts_7.Text = "JPM1990-2711R Кнопка РТТ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "Замена корпуса";
                        txB_parts_6.Text = "Корпус IC-F3GS";

                        txB_сompleted_works_7.Text = "Замена кнопки РТТ";
                        txB_parts_7.Text = "JPM1990-2711R Кнопка РТТ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "Замена корпуса";
                        txB_parts_6.Text = "Корпус IC-F3GS";

                        txB_сompleted_works_7.Text = "Замена кнопки РТТ";
                        txB_parts_7.Text = "JPM1990-2711R Кнопка РТТ";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "15")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена микрофона";
                        txB_parts_1.Text = "5015027H01 Микрофон для GP-340";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "HLN9820 Заглушка для GP-серии";

                        txB_сompleted_works_6.Text = "Замена шлейфа";
                        txB_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_7.Text = "Замена антенны";
                        txB_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена О кольца";
                        txB_parts_4.Text = "32012111001 О кольцо";

                        txB_сompleted_works_4.Text = "Замена контактов АКБ";
                        txB_parts_4.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_5.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_5.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_6.Text = "Замена кнопки РТТ";
                        txB_parts_6.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_7.Text = "Замена динамика";
                        txB_parts_7.Text = "50012013001 Динамик";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_6.Text = "Замена фильтра";
                        txB_parts_6.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_6.Text = "Замена фильтра";
                        txB_parts_6.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_6.Text = "Замена фильтра";
                        txB_parts_6.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_7.Text = "Замена уплотнителя";
                        txB_parts_7.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "16")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_2.Text = "Замена клавиатуры РТТ";
                        txB_parts_2.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        txB_сompleted_works_3.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_3.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_4.Text = "Замена клавиши РТТ";
                        txB_parts_4.Text = "4080523Z02 Клавиша РТТ";

                        txB_сompleted_works_5.Text = "Замена герметика верхней панели";
                        txB_parts_5.Text = "3280533Z05 Герметик верхней панели ";

                        txB_сompleted_works_6.Text = "Замена фронтальной наклейки";
                        txB_parts_6.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        txB_сompleted_works_7.Text = "Замена батарейных контактов";
                        txB_parts_7.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена кнопки РТТ";
                        txB_parts_1.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        txB_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена О кольца";
                        txB_parts_3.Text = "32012111001 О кольцо";

                        txB_сompleted_works_4.Text = "Замена резиновой клавиши PTT";
                        txB_parts_4.Text = "75012081001 Резиновая клавиша РТТ";

                        txB_сompleted_works_5.Text = "Замена накладки РТТ";
                        txB_parts_5.Text = "38012011001 Накладка РТТ";

                        txB_сompleted_works_6.Text = "Замена контактов АКБ";
                        txB_parts_6.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_5.Text = "Замена фильтра";
                        txB_parts_5.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_5.Text = "Замена фильтра";
                        txB_parts_5.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена транзистора";
                        txB_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_5.Text = "Замена фильтра";
                        txB_parts_5.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "17")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена передней панели";
                        txB_parts_1.Text = "1580666Z03 Передняя панель для GP-340";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_4.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_4.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_5.Text = "Замена клавиатуры РТТ";
                        txB_parts_5.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        txB_сompleted_works_6.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_6.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_7.Text = "Замена клавиши РТТ";
                        txB_parts_7.Text = "4080523Z02 Клавиша РТТ";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена кнопки РТТ";
                        txB_parts_1.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_2.Text = "Замена контактов АКБ";
                        txB_parts_2.Text = "0915184H01 Контакты АКБ";

                        txB_сompleted_works_3.Text = "Замена корпуса";
                        txB_parts_3.Text = "Корпус DP2400(e)";

                        txB_сompleted_works_4.Text = "Замена резиновой клавиши PTT";
                        txB_parts_4.Text = "75012081001 Резиновая клавиша РТТ";

                        txB_сompleted_works_5.Text = "Замена накладки РТТ";
                        txB_parts_5.Text = "38012011001 Накладка РТТ";

                        txB_сompleted_works_6.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_6.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = ")";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = ")";



                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "Замена транзистора";
                        txB_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = ")";


                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

            }

            if (cmB_сategory.Text == "5")
            {
                if (cmb_remont_select.Text == "1")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_2.Text = "Замена уплотнителя О-кольца";
                        txB_parts_2.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена шлейфа";
                        txB_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_5.Text = "Замена микрофона";
                        txB_parts_5.Text = "5015027H01 Микрофон для GP-340";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_1.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена О кольца";
                        txB_parts_3.Text = "32012111001 О кольцо";

                        txB_сompleted_works_4.Text = "Замена накладки РТТ";
                        txB_parts_4.Text = "HN000696A01 Накладка РТТ";

                        txB_сompleted_works_5.Text = "Замена верхнего уплотнителя";
                        txB_parts_5.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "Заглушка МР37";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "2")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена шлейфа";
                        txB_parts_1.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_2.Text = "Замена войлока GP-340";
                        txB_parts_2.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена сетки динамика";
                        txB_parts_1.Text = "35012060001 Сетка динамика";

                        txB_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        txB_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_3.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_4.Text = "Замена ручки переключения каналов";
                        txB_parts_4.Text = "36012017001 Ручка переключения каналов";

                        txB_сompleted_works_5.Text = "Замена верхнего уплотнителя";
                        txB_parts_5.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена клавиатуры";
                        txB_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";
                    }

                }

                if (cmb_remont_select.Text == "3")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена войлока GP-340";
                        txB_parts_1.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        txB_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        txB_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена кнопки РТТ";
                        txB_parts_1.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_2.Text = "Замена регулятора громкости";
                        txB_parts_2.Text = "1875103С04 Регулятор громкости";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена верхнего уплотнителя";
                        txB_parts_5.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена клавиатуры";
                        txB_parts_1.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена контакта -";
                        txB_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена клавиатуры";
                        txB_parts_1.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена контакта -";
                        txB_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена клавиатуры";
                        txB_parts_1.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена контакта -";
                        txB_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "4")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена шлейфа";
                        txB_parts_3.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена ручки регулятора громкости";
                        txB_parts_5.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена верхнего уплотнителя";
                        txB_parts_1.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_2.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_2.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_3.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_3.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "50012013001 Динамик";

                        txB_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        txB_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки";
                        txB_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена кнопки";
                        txB_parts_3.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки";
                        txB_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена кнопки";
                        txB_parts_3.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки";
                        txB_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена кнопки";
                        txB_parts_3.Text = "Кнопка РТТ для F16";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "5")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена шлейфа";
                        txB_parts_1.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        txB_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена батарейных контактов";
                        txB_parts_5.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_1.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_3.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_4.Text = "Замена  ручки регулятора громкости";
                        txB_parts_4.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена контакта + ";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_6.Text = "Замена панели защелки";
                        txB_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена контакта + ";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_6.Text = "Замена панели защелки";
                        txB_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена контакта + ";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_6.Text = "Замена панели защелки";
                        txB_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "6")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_1.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        txB_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_3.Text = "Замена шлейфа";
                        txB_parts_3.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        txB_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        txB_parts_2.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "50012013001 Динамик";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена защелки АКБ";
                        txB_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "7")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_1.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_2.Text = "Замена микрофона";
                        txB_parts_2.Text = "5015027H01 Микрофон для GP-340";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена шлейфа";
                        txB_parts_5.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена О кольца";
                        txB_parts_2.Text = "32012111001 О кольцо";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена микрофона";
                        txB_parts_4.Text = "5015027H01 Микрофон для DP2400";

                        txB_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_6.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_6.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена разъёма";
                        txB_parts_1.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_2.Text = "Замена защелки АКБ";
                        txB_parts_2.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена разъёма";
                        txB_parts_1.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_2.Text = "Замена защелки АКБ";
                        txB_parts_2.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена разъёма";
                        txB_parts_1.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_2.Text = "Замена защелки АКБ";
                        txB_parts_2.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "Заглушка МР37";

                        txB_сompleted_works_4.Text = "Замена ручки";
                        txB_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "8")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_1.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена передней панели";
                        txB_parts_3.Text = "1580666Z03 Передняя панель для GP-340";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена липучки интерфейсного разъёма";
                        txB_parts_5.Text = "1386058A01 Липучка интерфейсного разъема";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_2.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_3.Text = "Замена верхнего уплотнителя";
                        txB_parts_3.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        txB_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена ручки";
                        txB_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "Заглушка МР37";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "9")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена герметика верхней панели";
                        txB_parts_1.Text = "3280533Z05 Герметик верхней панели";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена заглушки";
                        txB_parts_5.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_3.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_4.Text = "Замена верхнего уплотнителя";
                        txB_parts_4.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_5.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_5.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена прокладки";
                        txB_parts_1.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_4.Text = "Замена защелки АКБ";
                        txB_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена прокладки";
                        txB_parts_1.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_4.Text = "Замена защелки АКБ";
                        txB_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена прокладки";
                        txB_parts_1.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена заглушки";
                        txB_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_4.Text = "Замена защелки АКБ";
                        txB_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_5.Text = "Замена динамика";
                        txB_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "10")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки переключателя каналов";
                        txB_parts_1.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        txB_сompleted_works_2.Text = "Замена войлока GP-340";
                        txB_parts_2.Text = "3586057А02 Войлок GP340";

                        txB_сompleted_works_3.Text = "Замена заглушки HLN9820";
                        txB_parts_3.Text = "Заглушка для GP-серии";

                        txB_сompleted_works_4.Text = "Замена уплотнителя О-кольца";
                        txB_parts_4.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        txB_сompleted_works_5.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_5.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_6.Text = "Замена антенны";
                        txB_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        txB_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "Замена регулятора громкости";
                        txB_parts_6.Text = "1875103С04 Регулятор громкости";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена клавиатуры";
                        txB_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена клавиатуры";
                        txB_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена клавиатуры";
                        txB_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_5.Text = "Замена прокладки";
                        txB_parts_5.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "11")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_2.Text = "Замена уплотнителя О-кольца";
                        txB_parts_2.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        txB_сompleted_works_3.Text = "Замена микрофона";
                        txB_parts_3.Text = "5015027H01 Микрофон для GP-340";

                        txB_сompleted_works_4.Text = "Замена шлейфа";
                        txB_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_5.Text = "Замена антенны";
                        txB_parts_5.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        txB_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена верхнего уплотнителя";
                        txB_parts_3.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_5.Text = "Замена О кольца";
                        txB_parts_5.Text = "32012111001 О кольцо";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена контакта -";
                        txB_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_5.Text = "Замена клавиатуры";
                        txB_parts_5.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена контакта -";
                        txB_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_5.Text = "Замена клавиатуры";
                        txB_parts_5.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя";
                        txB_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        txB_сompleted_works_4.Text = "Замена контакта -";
                        txB_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        txB_сompleted_works_5.Text = "Замена клавиатуры";
                        txB_parts_5.Text = "Клавиатура 2251 MAIN SEAL";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "12")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_1.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_2.Text = "Замена батарейных контактов";
                        txB_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        txB_сompleted_works_3.Text = "Замена липучки интерфейсного разъёма";
                        txB_parts_3.Text = "1386058A01 Липучка интерфейсного разъема";

                        txB_сompleted_works_4.Text = "Замена антенны";
                        txB_parts_4.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена кнопки РТТ";
                        txB_parts_1.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_4.Text = "Замена верхнего уплотнителя";
                        txB_parts_4.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_5.Text = "Замена О кольца";
                        txB_parts_5.Text = "32012111001 О кольцо";

                        txB_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        txB_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_3.Text = "Замена кнопки";
                        txB_parts_3.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "Заглушка МР37";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_3.Text = "Замена кнопки";
                        txB_parts_3.Text = "Кнопка РТТ для F3G (22300001070)";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "Заглушка МР37";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_3.Text = "Замена кнопки";
                        txB_parts_3.Text = "Кнопка РТТ для F16";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "Заглушка МР37";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "Ручка регулятора громкости F-16";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "13")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена заглушки";
                        txB_parts_1.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_2.Text = "Замена шлейфа";
                        txB_parts_2.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_3.Text = "Замена ручки регулятора громкости";
                        txB_parts_3.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена антенны";
                        txB_parts_5.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена верхнего уплотнителя";
                        txB_parts_1.Text = "32012089001 Верхний уплотнитель";

                        txB_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "50012013001 Динамик";

                        txB_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        txB_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки";
                        txB_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_2.Text = "Замена резины";
                        txB_parts_2.Text = "Резина МР12";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_4.Text = "Замена панели защелки";
                        txB_parts_4.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_5.Text = "Замена контакта + ";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки";
                        txB_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_2.Text = "Замена резины";
                        txB_parts_2.Text = "Резина МР12";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_4.Text = "Замена панели защелки";
                        txB_parts_4.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_5.Text = "Замена контакта + ";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена ручки";
                        txB_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_2.Text = "Замена резины";
                        txB_parts_2.Text = "Резина МР12";

                        txB_сompleted_works_3.Text = "Замена прокладки";
                        txB_parts_3.Text = "2251 JACK PANEL Прокладка";

                        txB_сompleted_works_4.Text = "Замена панели защелки";
                        txB_parts_4.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        txB_сompleted_works_5.Text = "Замена контакта + ";
                        txB_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "14")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена шлейфа";
                        txB_parts_1.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_2.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_2.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_3.Text = "Замена динамика";
                        txB_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_5.Text = "Замена антенны";
                        txB_parts_5.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_1.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "50012013001 Динамик";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_4.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_5.Text = "Замена ручки переключения каналов";
                        txB_parts_5.Text = "36012017001 Ручка переключения каналов";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена защелки АКБ";
                        txB_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        txB_сompleted_works_4.Text = "Замена разъёма";
                        txB_parts_4.Text = "Разъем антенный корпусной";

                        txB_сompleted_works_5.Text = "Замена ручки";
                        txB_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "15")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена шлейфа";
                        txB_parts_1.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "HLN9820	Заглушка для GP-серии";

                        txB_сompleted_works_3.Text = "Замена ручки регулятора громкости";
                        txB_parts_3.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_5.Text = "Замена антенны";
                        txB_parts_5.Text = "Антенна NAD6502 146-174Mгц";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена антенны";
                        txB_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        txB_сompleted_works_2.Text = "Замена О кольца";
                        txB_parts_2.Text = "32012111001 О кольцо";

                        txB_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        txB_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена динамика";
                        txB_parts_4.Text = "50012013001 Динамик";

                        txB_сompleted_works_5.Text = "Замена уплотнителя контактов АКБ";
                        txB_parts_5.Text = "32012110001 Уплотнитель контактов АКБ";

                        txB_сompleted_works_6.Text = "Замена кнопки РТТ";
                        txB_parts_6.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "Заглушка МР37";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "Заглушка МР37";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена заглушки";
                        txB_parts_4.Text = "Заглушка МР37";

                        txB_сompleted_works_5.Text = "Замена уплотнителя";
                        txB_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "16")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена динамика";
                        txB_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_2.Text = "Замена клавиатуры РТТ";
                        txB_parts_2.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        txB_сompleted_works_3.Text = "Замена держателя боковой клавиатуры";
                        txB_parts_3.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        txB_сompleted_works_4.Text = "Замена клавиши РТТ";
                        txB_parts_4.Text = "4080523Z02 Клавиша РТТ";

                        txB_сompleted_works_5.Text = "Замена герметика верхней панели";
                        txB_parts_5.Text = "3280533Z05 Герметик верхней панели ";

                        txB_сompleted_works_6.Text = "Замена фронтальной наклейки";
                        txB_parts_6.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена кнопки РТТ";
                        txB_parts_1.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        txB_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        txB_сompleted_works_3.Text = "Замена О кольца";
                        txB_parts_3.Text = "32012111001 О кольцо";

                        txB_сompleted_works_4.Text = "Замена резиновой клавиши PTT";
                        txB_parts_4.Text = "75012081001 Резиновая клавиша РТТ";

                        txB_сompleted_works_5.Text = "Замена накладки РТТ";
                        txB_parts_5.Text = "38012011001 Накладка РТТ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена резины";
                        txB_parts_1.Text = "Резина МР12";

                        txB_сompleted_works_2.Text = "Замена уплотнителя";
                        txB_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "Замена кнопки РТТ";
                        txB_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }

                if (cmb_remont_select.Text == "17")
                {
                    if (txB_model.Text == "Motorola GP-340")
                    {
                        txB_сompleted_works_1.Text = "Замена передней панели";
                        txB_parts_1.Text = "1580666Z03 Передняя панель для GP-340";

                        txB_сompleted_works_2.Text = "Замена динамика";
                        txB_parts_2.Text = "5005589U05 Динамик для GP-300/600";

                        txB_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        txB_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        txB_сompleted_works_4.Text = "Замена клавиатуры РТТ";
                        txB_parts_4.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        txB_сompleted_works_5.Text = "Замена клавиши РТТ";
                        txB_parts_5.Text = "4080523Z02 Клавиша РТТ";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-2400е")
                    {
                        txB_сompleted_works_1.Text = "Замена кнопки РТТ";
                        txB_parts_1.Text = "4070354A01 Кнопка РТТ";

                        txB_сompleted_works_2.Text = "Замена корпуса";
                        txB_parts_2.Text = "Корпус DP2400(e)";

                        txB_сompleted_works_3.Text = "Замена резиновой клавиши PTT";
                        txB_parts_3.Text = "75012081001 Резиновая клавиша РТТ";

                        txB_сompleted_works_4.Text = "Замена накладки РТТ";
                        txB_parts_4.Text = "38012011001 Накладка РТТ";

                        txB_сompleted_works_5.Text = "Замена гибкого шлейфа динамика";
                        txB_parts_5.Text = "PF001006A02 Гибкий шлейф динамика";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = "";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GS")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = ")";


                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F3GT")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = ")";

                        btn_save_add_rst_remont.Enabled = true;
                    }

                    else if (txB_model.Text == "Icom IC-F16")
                    {
                        txB_сompleted_works_1.Text = "Замена фильтра";
                        txB_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        txB_сompleted_works_2.Text = "Замена заглушки";
                        txB_parts_2.Text = "Заглушка МР37";

                        txB_сompleted_works_3.Text = "Замена ручки";
                        txB_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        txB_сompleted_works_4.Text = "Замена уплотнителя";
                        txB_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        txB_сompleted_works_5.Text = "";
                        txB_parts_5.Text = "";

                        txB_сompleted_works_6.Text = "";
                        txB_parts_6.Text = "";

                        txB_сompleted_works_7.Text = "";
                        txB_parts_7.Text = ")";

                        btn_save_add_rst_remont.Enabled = true;
                    }
                }
            }

        }

        #endregion

        void RemontRSTForm_Load(object sender, EventArgs e)
        {
            QuerySettingDataBase.LoadingLastNumberActRemont(lbL_last_act_remont);
            txB_MainMeans.Text = QuerySettingDataBase.Loading_OC_6_values(txB_serialNumber.Text).Item1;
            txB_NameProductRepaired.Text = QuerySettingDataBase.Loading_OC_6_values(txB_serialNumber.Text).Item2;
            if(txB_numberActRemont.Text != "53/")
            {
                lbL_AddRemontRST.Text = "Изменение ремонта";
                btn_save_add_rst_remont.Text = "Изменить";
            }
            else
            {
                lbL_AddRemontRST.Text = "Добавление ремонта";
                btn_save_add_rst_remont.Text = "Добавить";
            }
        }
    }
}
