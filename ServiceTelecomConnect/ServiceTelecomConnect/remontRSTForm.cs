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

        void Button_save_add_rst_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                string Mesage;
                Mesage = "Вы действительно хотите добавить ремонт?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                try
                {
                    var numberActRemont = textBox_numberActRemont.Text;
                    var сategory = comboBox_сategory.Text;
                    var priceRemont = textBox_priceRemont.Text;
                    var сompleted_works_1 = textBox_сompleted_works_1.Text;
                    var сompleted_works_2 = textBox_сompleted_works_2.Text;
                    var сompleted_works_3 = textBox_сompleted_works_3.Text;
                    var сompleted_works_4 = textBox_сompleted_works_4.Text;
                    var сompleted_works_5 = textBox_сompleted_works_5.Text;
                    var сompleted_works_6 = textBox_сompleted_works_6.Text;
                    var сompleted_works_7 = textBox_сompleted_works_7.Text;
                    var parts_1 = textBox_parts_1.Text;
                    var parts_2 = textBox_parts_2.Text;
                    var parts_3 = textBox_parts_3.Text;
                    var parts_4 = textBox_parts_4.Text;
                    var parts_5 = textBox_parts_5.Text;
                    var parts_6 = textBox_parts_6.Text;
                    var parts_7 = textBox_parts_7.Text;
                    var serialNumber = textBox_serialNumber.Text;


                    if (!(numberActRemont == "") && !(сategory == "") && !(priceRemont == "") && !(сompleted_works_1 == "") && !(parts_1 == ""))
                    {
                        var changeQuery = $"UPDATE radiostantion SET numberActRemont = '{numberActRemont.Trim()}', category = '{сategory}', " +
                            $"priceRemont = '{priceRemont}', completed_works_1 = '{сompleted_works_1.Trim()}', completed_works_2 = '{сompleted_works_2.Trim()}', " +
                            $"completed_works_3 = '{сompleted_works_3.Trim()}', completed_works_4 = '{сompleted_works_4.Trim()}', " +
                            $"completed_works_5 = '{сompleted_works_5.Trim()}', completed_works_6 = '{сompleted_works_6.Trim()}', " +
                            $"completed_works_7 = '{сompleted_works_7.Trim()}', parts_1 = '{parts_1.Trim()}', parts_2 = '{parts_2.Trim()}', " +
                            $"parts_3 = '{parts_3.Trim()}', parts_4 = '{parts_4.Trim()}', parts_5 = '{parts_5.Trim()}', parts_6 = '{parts_6.Trim()}', parts_7 = '{parts_7.Trim()}'" +
                            $"WHERE serialNumber = '{serialNumber}' ";

                        using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            command.ExecuteNonQuery();
                            DB.GetInstance.CloseConnection();
                        }


                        MessageBox.Show("Ремонт успешно добавлен!");

                    }
                    else
                    {
                        MessageBox.Show("Вы не заполнили нужные поля со (*)!");
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка! Ремонт не добавлен!");
                    MessageBox.Show(ex.ToString());
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

            textBox_numberActRemont.Text = "";
            comboBox_сategory.Text = "";
            textBox_priceRemont.Text = "";
            textBox_сompleted_works_1.Text = "";
            textBox_сompleted_works_2.Text = "";
            textBox_сompleted_works_3.Text = "";
            textBox_сompleted_works_4.Text = "";
            textBox_сompleted_works_5.Text = "";
            textBox_сompleted_works_6.Text = "";
            textBox_сompleted_works_7.Text = "";
            textBox_parts_1.Text = "";
            textBox_parts_2.Text = "";
            textBox_parts_3.Text = "";
            textBox_parts_4.Text = "";
            textBox_parts_5.Text = "";
            textBox_parts_6.Text = "";
            textBox_parts_7.Text = "";
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
            if (comboBox_сategory.Text == "3")
            {
                if (textBox_model.Text == "Icom IC-F3GT"
                || textBox_model.Text == "Icom IC-F11" || textBox_model.Text == "Icom IC-F16" || textBox_model.Text == "Icom IC-F3GS"
                || textBox_model.Text == "Motorola P040" || textBox_model.Text == "Motorola P080" || textBox_model.Text == "Motorola GP-300"
                || textBox_model.Text == "Motorola GP-320" || textBox_model.Text == "Motorola GP-340" || textBox_model.Text == "Motorola GP-360"
                || textBox_model.Text == "Альтавия-301М" || textBox_model.Text == "Comrade R5" || textBox_model.Text == "Гранит Р33П-1"
                || textBox_model.Text == "Гранит Р-43" || textBox_model.Text == "Радий-301" || textBox_model.Text == "Kenwood ТК-2107"
                || textBox_model.Text == "Vertex - 261")
                {
                    textBox_priceRemont.Text = "887.94";
                }
                else
                {
                    textBox_priceRemont.Text = "895.86";
                }
            }

            if (comboBox_сategory.Text == "4")
            {
                if (textBox_model.Text == "Icom IC-F3GT"
                || textBox_model.Text == "Icom IC-F11" || textBox_model.Text == "Icom IC-F16" || textBox_model.Text == "Icom IC-F3GS"
                || textBox_model.Text == "Motorola P040" || textBox_model.Text == "Motorola P080" || textBox_model.Text == "Motorola GP-300"
                || textBox_model.Text == "Motorola GP-320" || textBox_model.Text == "Motorola GP-340" || textBox_model.Text == "Motorola GP-360"
                || textBox_model.Text == "Альтавия-301М" || textBox_model.Text == "Comrade R5" || textBox_model.Text == "Гранит Р33П-1"
                || textBox_model.Text == "Гранит Р-43" || textBox_model.Text == "Радий-301" || textBox_model.Text == "Kenwood ТК-2107"
                || textBox_model.Text == "Vertex - 261")
                {
                    textBox_priceRemont.Text = "1267.49";
                }
                else
                {
                    textBox_priceRemont.Text = "1280.37";
                }
            }
            if (comboBox_сategory.Text == "5")
            {
                if (textBox_model.Text == "Icom IC-F3GT"
                || textBox_model.Text == "Icom IC-F11" || textBox_model.Text == "Icom IC-F16" || textBox_model.Text == "Icom IC-F3GS"
                || textBox_model.Text == "Motorola P040" || textBox_model.Text == "Motorola P080" || textBox_model.Text == "Motorola GP-300"
                || textBox_model.Text == "Motorola GP-320" || textBox_model.Text == "Motorola GP-340" || textBox_model.Text == "Motorola GP-360"
                || textBox_model.Text == "Альтавия-301М" || textBox_model.Text == "Comrade R5" || textBox_model.Text == "Гранит Р33П-1"
                || textBox_model.Text == "Гранит Р-43" || textBox_model.Text == "Радий-301" || textBox_model.Text == "Kenwood ТК-2107"
                || textBox_model.Text == "Vertex - 261")
                {
                    textBox_priceRemont.Text = "2535.97";
                }
                else
                {
                    textBox_priceRemont.Text = "2559.75";
                }
            }

            if (comboBox_сategory.Text == "6")
            {
                if (textBox_model.Text == "Icom IC-F3GT"
                || textBox_model.Text == "Icom IC-F11" || textBox_model.Text == "Icom IC-F16" || textBox_model.Text == "Icom IC-F3GS"
                || textBox_model.Text == "Motorola P040" || textBox_model.Text == "Motorola P080" || textBox_model.Text == "Motorola GP-300"
                || textBox_model.Text == "Motorola GP-320" || textBox_model.Text == "Motorola GP-340" || textBox_model.Text == "Motorola GP-360"
                || textBox_model.Text == "Альтавия-301М" || textBox_model.Text == "Comrade R5" || textBox_model.Text == "Гранит Р33П-1"
                || textBox_model.Text == "Гранит Р-43" || textBox_model.Text == "Радий-301" || textBox_model.Text == "Kenwood ТК-2107"
                || textBox_model.Text == "Vertex - 261")
                {
                    textBox_priceRemont.Text = "5071.94";
                }
                else
                {
                    textBox_priceRemont.Text = "5119.51";
                }
            }
        }
        void TextBox_TextChanged()
        {
            if (textBox_numberActRemont.Text.Length > 0 && textBox_сompleted_works_1.Text.Length > 0
                && textBox_parts_1.Text.Length > 0)
            {
                textBox_сompleted_works_2.ReadOnly = false;
                textBox_parts_2.ReadOnly = false;
                button_save_add_rst_remont.Enabled = true;
                if (textBox_сompleted_works_2.Text.Length > 0 && textBox_parts_2.Text.Length > 0)
                {
                    textBox_сompleted_works_3.ReadOnly = false;
                    textBox_parts_3.ReadOnly = false;
                    if (textBox_сompleted_works_3.Text.Length > 0 && textBox_parts_3.Text.Length > 0)
                    {
                        textBox_сompleted_works_4.ReadOnly = false;
                        textBox_parts_4.ReadOnly = false;
                        if (textBox_сompleted_works_4.Text.Length > 0 && textBox_parts_4.Text.Length > 0)
                        {
                            textBox_сompleted_works_5.ReadOnly = false;
                            textBox_parts_5.ReadOnly = false;
                            if (textBox_сompleted_works_5.Text.Length > 0 && textBox_parts_5.Text.Length > 0)
                            {
                                textBox_сompleted_works_6.ReadOnly = false;
                                textBox_parts_6.ReadOnly = false;
                                if (textBox_сompleted_works_6.Text.Length > 0 && textBox_parts_6.Text.Length > 0)
                                {
                                    textBox_сompleted_works_7.ReadOnly = false;
                                    textBox_parts_7.ReadOnly = false;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                button_save_add_rst_remont.Enabled = false;
            }
        }

        void RemontRSTForm_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox_TextChanged();
        }
        void Label_company_DoubleClick(object sender, EventArgs e)
        {
            comboBox_remont_select.Visible = true;
        }

        #region лайфхак для ремонтов
        void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_сategory.Text == "6")
            {
                if (comboBox_remont_select.Text == "1")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена батарейных контактов";
                        textBox_parts_1.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя О-кольца";
                        textBox_parts_2.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_4.Text = "Замена шлейфа";
                        textBox_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_6.Text = "Замена заглушки";
                        textBox_parts_6.Text = "LN9820 Заглушка для GP-серии";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "50012013001 Динамик";

                        textBox_сompleted_works_2.Text = "Замена накладки РТТ";
                        textBox_parts_2.Text = "HN000696A01 Накладка РТТ";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        textBox_parts_3.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        textBox_сompleted_works_4.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_4.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_5.Text = "Замена О кольца";
                        textBox_parts_5.Text = "32012111001 О кольцо";

                        textBox_сompleted_works_6.Text = "Замена контактов АКБ";
                        textBox_parts_6.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_7.Text = "Замена верхнего уплотнителя";
                        textBox_parts_7.Text = "32012089001 Верхний уплотнитель";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенного разъема";
                        textBox_parts_1.Text = "Антенный разъем для F3G (8950005260)";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "K036NA500-66 Динамик для IC-44088 ";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя";
                        textBox_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_4.Text = "Замена фильтра";
                        textBox_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_5.Text = "Замена ручки";
                        textBox_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "Замена заглушки";
                        textBox_parts_6.Text = "Заглушка МР37";

                        textBox_сompleted_works_7.Text = "Замена уплотнителя";
                        textBox_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенного разъема";
                        textBox_parts_1.Text = "Антенный разъем для F3GT";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "K036NA500-66 Динамик для IC-44088 ";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя";
                        textBox_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_4.Text = "Замена фильтра";
                        textBox_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_5.Text = "Замена ручки";
                        textBox_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "Замена заглушки";
                        textBox_parts_6.Text = "Заглушка МР37";

                        textBox_сompleted_works_7.Text = "Замена уплотнителя";
                        textBox_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенного разъема";
                        textBox_parts_1.Text = "Антенный разъем для F16 (6910015910)";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "K036NA500-66 Динамик для IC-44088 ";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя";
                        textBox_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_4.Text = "Замена фильтра";
                        textBox_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_5.Text = "Замена ручки";
                        textBox_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "Замена заглушки";
                        textBox_parts_6.Text = "Заглушка МР37";

                        textBox_сompleted_works_7.Text = "Замена уплотнителя";
                        textBox_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "2")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена держателя боковой клавиатуры";
                        textBox_parts_1.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        textBox_сompleted_works_2.Text = "Замена войлока GP-340";
                        textBox_parts_2.Text = "3586057А02 Войлок GP340";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_4.Text = "Замена шлейфа";
                        textBox_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии ";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_6.Text = "Замена батарейных контактов";
                        textBox_parts_6.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "50012013001 Динамик";

                        textBox_сompleted_works_2.Text = "Замена контактов АКБ";
                        textBox_parts_2.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_3.Text = "Замена верхнего уплотнителя";
                        textBox_parts_3.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        textBox_parts_4.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        textBox_сompleted_works_5.Text = "Замена ручки переключения каналов";
                        textBox_parts_5.Text = "36012017001 Ручка переключения каналов";

                        textBox_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_7.Text = "Замена сетки динамика";
                        textBox_parts_7.Text = "35012060001 Сетка динамика";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "K036NA500-66 Динамик для IC-44088 ";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена кнопки";
                        textBox_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_5.Text = "Замена разъёма";
                        textBox_parts_5.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_6.Text = "Замена регулятора громкости";
                        textBox_parts_6.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        textBox_сompleted_works_7.Text = "Замена ручки";
                        textBox_parts_7.Text = "KNOB N-276 Ручка регулятора громкости";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена кнопки";
                        textBox_parts_4.Text = "Кнопка РТТ для F3GT";

                        textBox_сompleted_works_5.Text = "Замена разъёма";
                        textBox_parts_5.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_6.Text = "Замена регулятора громкости";
                        textBox_parts_6.Text = "Регулятор громкости для F3GT";

                        textBox_сompleted_works_7.Text = "Замена ручки";
                        textBox_parts_7.Text = "Ручка регулятора громкости для F3GT";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена кнопки";
                        textBox_parts_4.Text = "Кнопка РТТ для IC-F16 (2260002840)";

                        textBox_сompleted_works_5.Text = "Замена разъёма";
                        textBox_parts_5.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_6.Text = "Замена регулятора громкости";
                        textBox_parts_6.Text = "Регулятор громкости для F16";

                        textBox_сompleted_works_7.Text = "Замена ручки";
                        textBox_parts_7.Text = "Ручка регулятора громкости для F16";

                        button_save_add_rst_remont.Enabled = true;
                    }

                }

                if (comboBox_remont_select.Text == "3")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена войлока GP-340";
                        textBox_parts_1.Text = "3586057А02 Войлок GP340";

                        textBox_сompleted_works_2.Text = "Замена батарейных контактов";
                        textBox_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        textBox_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_6.Text = "Замена шлейфа";
                        textBox_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        textBox_parts_1.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        textBox_сompleted_works_2.Text = "Замена верхнего уплотнителя";
                        textBox_parts_2.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_3.Text = "Замена контактов АКБ";
                        textBox_parts_3.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_4.Text = "Замена регулятора громкости";
                        textBox_parts_4.Text = "1875103С04 Регулятор громкости";

                        textBox_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "Замена кнопки РТТ";
                        textBox_parts_6.Text = "4070354A01 Кнопка РТТ";

                        textBox_сompleted_works_7.Text = "Замена накладки РТТ";
                        textBox_parts_7.Text = "HN000696A01 Накладка РТТ";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена транзистора";
                        textBox_parts_1.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена резины";
                        textBox_parts_3.Text = "Резина МР12";

                        textBox_сompleted_works_4.Text = "Замена разъёма";
                        textBox_parts_4.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_6.Text = "Замена панели защелки";
                        textBox_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        textBox_сompleted_works_7.Text = "Замена контакта + ";
                        textBox_parts_7.Text = "2251 PLUS TERMINAL Контакт  + ";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена транзистора";
                        textBox_parts_1.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена резины";
                        textBox_parts_3.Text = "Резина МР12";

                        textBox_сompleted_works_4.Text = "Замена разъёма";
                        textBox_parts_4.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_6.Text = "Замена панели защелки";
                        textBox_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        textBox_сompleted_works_7.Text = "Замена контакта + ";
                        textBox_parts_7.Text = "2251 PLUS TERMINAL Контакт  + ";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена транзистора";
                        textBox_parts_1.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена резины";
                        textBox_parts_3.Text = "Резина МР12";

                        textBox_сompleted_works_4.Text = "Замена разъёма";
                        textBox_parts_4.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_6.Text = "Замена панели защелки";
                        textBox_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        textBox_сompleted_works_7.Text = "Замена контакта + ";
                        textBox_parts_7.Text = "2251 PLUS TERMINAL Контакт  + ";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "4")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена фронтальной наклейки";
                        textBox_parts_1.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        textBox_сompleted_works_2.Text = "Замена батарейных контактов";
                        textBox_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_5.Text = "Замена ручки регулятора громкости";
                        textBox_parts_5.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        textBox_сompleted_works_6.Text = "Замена шлейфа";
                        textBox_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена верхнего уплотнителя";
                        textBox_parts_1.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_2.Text = "Замена контактов АКБ";
                        textBox_parts_2.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_3.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        textBox_parts_3.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        textBox_сompleted_works_4.Text = "Замена регулятора громкости";
                        textBox_parts_4.Text = "1875103С04 Регулятор громкости";

                        textBox_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "Замена динамика";
                        textBox_parts_6.Text = "50012013001 Динамик";

                        textBox_сompleted_works_7.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_7.Text = "32012110001 Уплотнитель контактов АКБ";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_2.Text = "Замена антенного разъема";
                        textBox_parts_2.Text = "Антенный разъем для F3G (8950005260)";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_4.Text = "Замена кнопки";
                        textBox_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "Заглушка МР37";

                        textBox_сompleted_works_6.Text = "Замена регулятора громкости";
                        textBox_parts_6.Text = "Регулятор громкости TP76NOON-15F-A103-2251 ";

                        textBox_сompleted_works_7.Text = "Замена ручки";
                        textBox_parts_7.Text = "KNOB N-276 Ручка регулятора громкости";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_2.Text = "Замена антенного разъема";
                        textBox_parts_2.Text = "Антенный разъем для F3G (8950005260)";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_4.Text = "Замена кнопки";
                        textBox_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "Заглушка МР37";

                        textBox_сompleted_works_6.Text = "Замена регулятора громкости";
                        textBox_parts_6.Text = "Регулятор громкости TP76NOON-15F-A103-2251 ";

                        textBox_сompleted_works_7.Text = "Замена ручки";
                        textBox_parts_7.Text = "KNOB N-276 Ручка регулятора громкости";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_2.Text = "Замена антенного разъема";
                        textBox_parts_2.Text = "Антенный разъем для F3G (8950005260)";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_4.Text = "Замена кнопки";
                        textBox_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "Заглушка МР37";

                        textBox_сompleted_works_6.Text = "Замена регулятора громкости";
                        textBox_parts_6.Text = "Регулятор громкости TP76NOON-15F-A103-2251 ";

                        textBox_сompleted_works_7.Text = "Замена ручки";
                        textBox_parts_7.Text = "KNOB N-276 Ручка регулятора громкости";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "5")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена герметика верхней панели";
                        textBox_parts_1.Text = "3280533Z05 Герметик верхней панели";

                        textBox_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        textBox_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_5.Text = "Замена батарейных контактов";
                        textBox_parts_5.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_6.Text = "Замена шлейфа";
                        textBox_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена контактов АКБ";
                        textBox_parts_1.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        textBox_parts_3.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "50012013001 Динамик";

                        textBox_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        textBox_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        textBox_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_7.Text = "Замена кнопки РТТ";
                        textBox_parts_7.Text = "4070354A01 Кнопка РТТ";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена фильтра";
                        textBox_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя";
                        textBox_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_4.Text = "Замена транзистора";
                        textBox_parts_4.Text = "Транзистор 2SK2974";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_6.Text = "Замена контакта -";
                        textBox_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        textBox_сompleted_works_7.Text = "Замена клавиатуры";
                        textBox_parts_7.Text = "Клавиатура 2251 MAIN SEAL";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена фильтра";
                        textBox_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя";
                        textBox_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_4.Text = "Замена транзистора";
                        textBox_parts_4.Text = "Транзистор 2SK2974";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_6.Text = "Замена контакта -";
                        textBox_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        textBox_сompleted_works_7.Text = "Замена клавиатуры";
                        textBox_parts_7.Text = "Клавиатура 2251 MAIN SEAL";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена фильтра";
                        textBox_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя";
                        textBox_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_4.Text = "Замена транзистора";
                        textBox_parts_4.Text = "Транзистор 2SK2974";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_6.Text = "Замена контакта -";
                        textBox_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        textBox_сompleted_works_7.Text = "Замена клавиатуры";
                        textBox_parts_7.Text = "Клавиатура 2251 MAIN SEAL";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "6")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена липучки интерфейсного разъёма";
                        textBox_parts_1.Text = "1386058A01 Липучка интерфейсного разъема";

                        textBox_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        textBox_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        textBox_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        textBox_сompleted_works_5.Text = "Замена шлейфа";
                        textBox_parts_5.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_6.Text = "Замена батарейных контактов";
                        textBox_parts_6.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        textBox_parts_4.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "50012013001 Динамик";

                        textBox_сompleted_works_6.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_6.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_7.Text = "Замена контактов АКБ";
                        textBox_parts_7.Text = "0915184H01 Контакты АКБ";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена клавиатуры";
                        textBox_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_5.Text = "Замена кнопки РТТ";
                        textBox_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        textBox_сompleted_works_6.Text = "Замена корпуса";
                        textBox_parts_6.Text = "Корпус IC-F3GS";

                        textBox_сompleted_works_7.Text = "Замена прокладки";
                        textBox_parts_7.Text = "2251 JACK PANEL Прокладка";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена клавиатуры";
                        textBox_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_5.Text = "Замена кнопки РТТ";
                        textBox_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        textBox_сompleted_works_6.Text = "Замена корпуса";
                        textBox_parts_6.Text = "Корпус IC-F3GT";

                        textBox_сompleted_works_7.Text = "Замена прокладки";
                        textBox_parts_7.Text = "2251 JACK PANEL Прокладка";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена клавиатуры";
                        textBox_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_5.Text = "Замена кнопки РТТ";
                        textBox_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        textBox_сompleted_works_6.Text = "Замена корпуса";
                        textBox_parts_6.Text = "Корпус для IC-F16 (с вклееным динамиком защелкой АКБ линзой)";

                        textBox_сompleted_works_7.Text = "Замена прокладки";
                        textBox_parts_7.Text = "2251 JACK PANEL Прокладка";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "7")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_2.Text = "Замена батарейных контактов";
                        textBox_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_3.Text = "Замена микрофона";
                        textBox_parts_3.Text = "5015027H01 Микрофон для GP-340";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_5.Text = "Замена шлейфа";
                        textBox_parts_5.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_6.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_6.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_2.Text = "Замена микрофона";
                        textBox_parts_2.Text = "5015027H01 Микрофон для DP2400";

                        textBox_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена контактов АКБ";
                        textBox_parts_4.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_5.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_6.Text = "Замена О кольца";
                        textBox_parts_6.Text = "32012111001 О кольцо";

                        textBox_сompleted_works_7.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        textBox_parts_7.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена прокладки";
                        textBox_parts_1.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_4.Text = "Замена защелки АКБ";
                        textBox_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_5.Text = "Замена микросхемы";
                        textBox_parts_5.Text = "Микросхема TA31136FN8 EL IC";

                        textBox_сompleted_works_6.Text = "Замена кнопки";
                        textBox_parts_6.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_7.Text = "Замена динамика";
                        textBox_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена прокладки";
                        textBox_parts_1.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_4.Text = "Замена защелки АКБ";
                        textBox_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_5.Text = "Замена микросхемы";
                        textBox_parts_5.Text = "Микросхема TA31136FN8 EL IC";

                        textBox_сompleted_works_6.Text = "Замена кнопки";
                        textBox_parts_6.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_7.Text = "Замена динамика";
                        textBox_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена прокладки";
                        textBox_parts_1.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_4.Text = "Замена защелки АКБ";
                        textBox_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_5.Text = "Замена микросхемы";
                        textBox_parts_5.Text = "Микросхема TA31136FN8 EL IC";

                        textBox_сompleted_works_6.Text = "Замена кнопки";
                        textBox_parts_6.Text = "Кнопка РТТ для IC-F16 (2260002840)";

                        textBox_сompleted_works_7.Text = "Замена динамика";
                        textBox_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "8")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_2.Text = "Замена батарейных контактов";
                        textBox_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_3.Text = "Замена передней панели";
                        textBox_parts_3.Text = "1580666Z03 Передняя панель для GP-340";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_5.Text = "Замена липучки интерфейсного разъёма";
                        textBox_parts_5.Text = "1386058A01 Липучка интерфейсного разъема";

                        textBox_сompleted_works_6.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_6.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_2.Text = "Замена контактов АКБ";
                        textBox_parts_2.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_3.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_4.Text = "Замена верхнего уплотнителя";
                        textBox_parts_4.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_5.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_5.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_7.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        textBox_parts_7.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена фильтра";
                        textBox_parts_3.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_4.Text = "Замена транзистора";
                        textBox_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_5.Text = "Замена ручки";
                        textBox_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "Замена прокладки";
                        textBox_parts_6.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_7.Text = "Замена заглушки";
                        textBox_parts_7.Text = "Заглушка МР37";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена фильтра";
                        textBox_parts_3.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_4.Text = "Замена транзистора";
                        textBox_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_5.Text = "Замена ручки";
                        textBox_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "Замена прокладки";
                        textBox_parts_6.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_7.Text = "Замена заглушки";
                        textBox_parts_7.Text = "Заглушка МР37";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена фильтра";
                        textBox_parts_3.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_4.Text = "Замена транзистора";
                        textBox_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_5.Text = "Замена ручки";
                        textBox_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "Замена прокладки";
                        textBox_parts_6.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_7.Text = "Замена заглушки";
                        textBox_parts_7.Text = "Заглушка МР37";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "9")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена ручки переключателя каналов";
                        textBox_parts_1.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        textBox_сompleted_works_2.Text = "Замена батарейных контактов";
                        textBox_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_6.Text = "Замена герметика верхней панели";
                        textBox_parts_6.Text = "3280533Z05 Герметик верхней панели";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_3.Text = "Замена контактов АКБ";
                        textBox_parts_3.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "50012013001 Динамик";

                        textBox_сompleted_works_5.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_5.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_6.Text = "Замена кнопки РТТ";
                        textBox_parts_6.Text = "4070354A01 Кнопка РТТ";

                        textBox_сompleted_works_7.Text = "Замена верхнего уплотнителя";
                        textBox_parts_7.Text = "32012089001 Верхний уплотнитель";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_4.Text = "Замена фильтра";
                        textBox_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя";
                        textBox_parts_5.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_6.Text = "Замена антенного разъема";
                        textBox_parts_6.Text = "Антенный разъем для F3G (8950005260)";

                        textBox_сompleted_works_7.Text = "Замена заглушки";
                        textBox_parts_7.Text = "Заглушка МР37";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_4.Text = "Замена фильтра";
                        textBox_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя";
                        textBox_parts_5.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_6.Text = "Замена антенного разъема";
                        textBox_parts_6.Text = "Антенный разъем для F3G (8950005260)";

                        textBox_сompleted_works_7.Text = "Замена заглушки";
                        textBox_parts_7.Text = "Заглушка МР37";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_4.Text = "Замена фильтра";
                        textBox_parts_4.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя";
                        textBox_parts_5.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_6.Text = "Замена антенного разъема";
                        textBox_parts_6.Text = "Антенный разъем для F16";

                        textBox_сompleted_works_7.Text = "Замена заглушки";
                        textBox_parts_7.Text = "Заглушка МР37";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "10")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена войлока GP-340";
                        textBox_parts_1.Text = "3586057А02 Войлок GP340";

                        textBox_сompleted_works_2.Text = "Замена микросхемы";
                        textBox_parts_2.Text = "5185963A27 Микросхема синтезатора WARIS";

                        textBox_сompleted_works_3.Text = "Замена липучки интерфейсного разъёма";
                        textBox_parts_3.Text = "1386058A01 Липучка интерфейсного разъема";

                        textBox_сompleted_works_4.Text = "Замена держателя боковой клавиатуры";
                        textBox_parts_4.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя О-кольца";
                        textBox_parts_5.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        textBox_сompleted_works_6.Text = "Замена фронтальной наклейки";
                        textBox_parts_6.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_2.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_2.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "50012013001 Динамик";

                        textBox_сompleted_works_4.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_4.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_5.Text = "Замена регулятора громкости";
                        textBox_parts_5.Text = "1875103С04 Регулятор громкости";

                        textBox_сompleted_works_6.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_6.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_7.Text = "Замена контактов АКБ";
                        textBox_parts_7.Text = "0915184H01 Контакты АКБ";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена разъёма";
                        textBox_parts_1.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_2.Text = "Замена регулятора громкости";
                        textBox_parts_2.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена кнопки";
                        textBox_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "Заглушка МР37";

                        textBox_сompleted_works_6.Text = "Замена ручки";
                        textBox_parts_6.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_7.Text = "Замена динамика";
                        textBox_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена разъёма";
                        textBox_parts_1.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_2.Text = "Замена регулятора громкости";
                        textBox_parts_2.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена кнопки";
                        textBox_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "Заглушка МР37";

                        textBox_сompleted_works_6.Text = "Замена ручки";
                        textBox_parts_6.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_7.Text = "Замена динамика";
                        textBox_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена разъёма";
                        textBox_parts_1.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_2.Text = "Замена регулятора громкости";
                        textBox_parts_2.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена кнопки";
                        textBox_parts_4.Text = "Кнопка РТТ для F16";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "Заглушка МР37";

                        textBox_сompleted_works_6.Text = "Замена ручки";
                        textBox_parts_6.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_7.Text = "Замена динамика";
                        textBox_parts_7.Text = "K036NA500-66 Динамик для IC-44088";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "11")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена войлока GP-340";
                        textBox_parts_1.Text = "3586057А02 Войлок GP340";

                        textBox_сompleted_works_2.Text = "Замена батарейных контактов";
                        textBox_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_4.Text = "Замена шлейфа";
                        textBox_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_5.Text = "Замена микрофона";
                        textBox_parts_5.Text = "5015027H01 Микрофон для GP-340";

                        textBox_сompleted_works_6.Text = "Замена заглушки";
                        textBox_parts_6.Text = "HLN9820 Заглушка для GP-серии";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена О кольца";
                        textBox_parts_4.Text = "32012111001 О кольцо";

                        textBox_сompleted_works_4.Text = "Замена контактов АКБ";
                        textBox_parts_4.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_5.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_6.Text = "Замена верхнего уплотнителя";
                        textBox_parts_6.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_7.Text = "Замена микрофона";
                        textBox_parts_7.Text = "5015027H01 Микрофон для DP2400";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена резины";
                        textBox_parts_1.Text = "Резина МР12";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена транзистора";
                        textBox_parts_3.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_4.Text = "Замена прокладки";
                        textBox_parts_4.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_5.Text = "Замена разъёма";
                        textBox_parts_5.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_6.Text = "Замена контакта + ";
                        textBox_parts_6.Text = "2251 PLUS TERMINAL Контакт  + ";

                        textBox_сompleted_works_7.Text = "Замена панели защелки";
                        textBox_parts_7.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена резины";
                        textBox_parts_1.Text = "Резина МР12";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена транзистора";
                        textBox_parts_3.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_4.Text = "Замена прокладки";
                        textBox_parts_4.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_5.Text = "Замена разъёма";
                        textBox_parts_5.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_6.Text = "Замена контакта + ";
                        textBox_parts_6.Text = "2251 PLUS TERMINAL Контакт  + ";

                        textBox_сompleted_works_7.Text = "Замена панели защелки";
                        textBox_parts_7.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена резины";
                        textBox_parts_1.Text = "Резина МР12";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена транзистора";
                        textBox_parts_3.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_4.Text = "Замена прокладки";
                        textBox_parts_4.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_5.Text = "Замена разъёма";
                        textBox_parts_5.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_6.Text = "Замена контакта + ";
                        textBox_parts_6.Text = "2251 PLUS TERMINAL Контакт  + ";

                        textBox_сompleted_works_7.Text = "Замена панели защелки";
                        textBox_parts_7.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        button_save_add_rst_remont.Enabled = true;
                    }

                }

                if (comboBox_remont_select.Text == "12")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        textBox_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        textBox_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        textBox_сompleted_works_5.Text = "Замена батарейных контактов";
                        textBox_parts_5.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_6.Text = "Замена шлейфа";
                        textBox_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена контактов АКБ";
                        textBox_parts_1.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_3.Text = "Замена верхнего уплотнителя";
                        textBox_parts_3.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_4.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_4.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_5.Text = "Замена кнопки РТТ";
                        textBox_parts_5.Text = "4070354A01 Кнопка РТТ";

                        textBox_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_7.Text = "Замена О кольца";
                        textBox_parts_7.Text = "32012111001 О кольцо";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена регулятора громкости";
                        textBox_parts_1.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "Заглушка МР37";

                        textBox_сompleted_works_4.Text = "Замена кнопки";
                        textBox_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_6.Text = "Замена заглушки";
                        textBox_parts_6.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_7.Text = "Замена антенного разъема";
                        textBox_parts_7.Text = "Антенный разъем для F3G (8950005260)";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена регулятора громкости";
                        textBox_parts_1.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "Заглушка МР37";

                        textBox_сompleted_works_4.Text = "Замена кнопки";
                        textBox_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_6.Text = "Замена заглушки";
                        textBox_parts_6.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_7.Text = "Замена антенного разъема";
                        textBox_parts_7.Text = "Антенный разъем для F3G (8950005260)";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена регулятора громкости";
                        textBox_parts_1.Text = "Регулятор громкости TP76NOON-15F-A103-2251";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "Заглушка МР37";

                        textBox_сompleted_works_4.Text = "Замена кнопки";
                        textBox_parts_4.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_6.Text = "Замена заглушки";
                        textBox_parts_6.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_7.Text = "Замена антенного разъема";
                        textBox_parts_7.Text = "Антенный разъем для F16";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "13")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена войлока GP-340";
                        textBox_parts_1.Text = "3586057А02 Войлок GP340";

                        textBox_сompleted_works_2.Text = "Замена ручки переключателя каналов";
                        textBox_parts_2.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_4.Text = "Замена батарейных контактов";
                        textBox_parts_4.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_5.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_6.Text = "Замена шлейфа";
                        textBox_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_1.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "50012013001 Динамик";

                        textBox_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_4.Text = "Замена контактов АКБ";
                        textBox_parts_4.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_5.Text = "Замена кнопки РТТ";
                        textBox_parts_5.Text = "4070354A01 Кнопка РТТ";

                        textBox_сompleted_works_6.Text = "Замена верхнего уплотнителя";
                        textBox_parts_6.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_7.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        textBox_parts_7.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена фильтра";
                        textBox_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_2.Text = "Замена клавиатуры";
                        textBox_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя";
                        textBox_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_4.Text = "Замена прокладки";
                        textBox_parts_4.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_5.Text = "Замена транзистора";
                        textBox_parts_5.Text = "Транзистор 2SK2974";

                        textBox_сompleted_works_6.Text = "Замена контакта -";
                        textBox_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        textBox_сompleted_works_7.Text = "Замена уплотнителя";
                        textBox_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена фильтра";
                        textBox_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_2.Text = "Замена клавиатуры";
                        textBox_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя";
                        textBox_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_4.Text = "Замена прокладки";
                        textBox_parts_4.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_5.Text = "Замена транзистора";
                        textBox_parts_5.Text = "Транзистор 2SK2974";

                        textBox_сompleted_works_6.Text = "Замена контакта -";
                        textBox_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        textBox_сompleted_works_7.Text = "Замена уплотнителя";
                        textBox_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена фильтра";
                        textBox_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_2.Text = "Замена клавиатуры";
                        textBox_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя";
                        textBox_parts_3.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_4.Text = "Замена прокладки";
                        textBox_parts_4.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_5.Text = "Замена транзистора";
                        textBox_parts_5.Text = "Транзистор 2SK2974";

                        textBox_сompleted_works_6.Text = "Замена контакта -";
                        textBox_parts_6.Text = "2251 MINUS TERMINAL Контакт -";

                        textBox_сompleted_works_7.Text = "Замена уплотнителя";
                        textBox_parts_7.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "14")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена держателя боковой клавиатуры";
                        textBox_parts_1.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        textBox_сompleted_works_2.Text = "Замена микросхемы";
                        textBox_parts_2.Text = "5185963A27 Микросхема синтезатора WARIS";

                        textBox_сompleted_works_3.Text = "Замена герметика верхней панели";
                        textBox_parts_3.Text = "3280533Z05 Герметик верхней панели";

                        textBox_сompleted_works_4.Text = "Замена клавиатуры РТТ";
                        textBox_parts_4.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя О-кольца";
                        textBox_parts_5.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        textBox_сompleted_works_6.Text = "Замена фронтальной наклейки";
                        textBox_parts_6.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена контактов АКБ";
                        textBox_parts_1.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "50012013001 Динамик";

                        textBox_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "Замена ручки переключения каналов";
                        textBox_parts_6.Text = "36012017001 Ручка переключения каналов";

                        textBox_сompleted_works_7.Text = "Замена переключателя каналов 16 позиций";
                        textBox_parts_7.Text = "40012029001 Переключатель каналов 16 позиций";


                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена защелки АКБ";
                        textBox_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_2.Text = "Замена клавиатуры";
                        textBox_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "Заглушка МР37";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_6.Text = "Замена корпуса";
                        textBox_parts_6.Text = "Корпус IC-F3GS";

                        textBox_сompleted_works_7.Text = "Замена кнопки РТТ";
                        textBox_parts_7.Text = "JPM1990-2711R Кнопка РТТ";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена защелки АКБ";
                        textBox_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_2.Text = "Замена клавиатуры";
                        textBox_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "Заглушка МР37";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_6.Text = "Замена корпуса";
                        textBox_parts_6.Text = "Корпус IC-F3GS";

                        textBox_сompleted_works_7.Text = "Замена кнопки РТТ";
                        textBox_parts_7.Text = "JPM1990-2711R Кнопка РТТ";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена защелки АКБ";
                        textBox_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_2.Text = "Замена клавиатуры";
                        textBox_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "Заглушка МР37";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_6.Text = "Замена корпуса";
                        textBox_parts_6.Text = "Корпус IC-F3GS";

                        textBox_сompleted_works_7.Text = "Замена кнопки РТТ";
                        textBox_parts_7.Text = "JPM1990-2711R Кнопка РТТ";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "15")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена микрофона";
                        textBox_parts_1.Text = "5015027H01 Микрофон для GP-340";

                        textBox_сompleted_works_2.Text = "Замена батарейных контактов";
                        textBox_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "HLN9820 Заглушка для GP-серии";

                        textBox_сompleted_works_6.Text = "Замена шлейфа";
                        textBox_parts_6.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_7.Text = "Замена антенны";
                        textBox_parts_7.Text = "Антенна NAD6502 146-174Mгц";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена О кольца";
                        textBox_parts_4.Text = "32012111001 О кольцо";

                        textBox_сompleted_works_4.Text = "Замена контактов АКБ";
                        textBox_parts_4.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_5.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_6.Text = "Замена кнопки РТТ";
                        textBox_parts_6.Text = "4070354A01 Кнопка РТТ";

                        textBox_сompleted_works_7.Text = "Замена динамика";
                        textBox_parts_7.Text = "50012013001 Динамик";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "Заглушка МР37";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена ручки";
                        textBox_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_5.Text = "Замена транзистора";
                        textBox_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_6.Text = "Замена фильтра";
                        textBox_parts_6.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_7.Text = "Замена уплотнителя";
                        textBox_parts_7.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "Заглушка МР37";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена ручки";
                        textBox_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_5.Text = "Замена транзистора";
                        textBox_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_6.Text = "Замена фильтра";
                        textBox_parts_6.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_7.Text = "Замена уплотнителя";
                        textBox_parts_7.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "Заглушка МР37";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена ручки";
                        textBox_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_5.Text = "Замена транзистора";
                        textBox_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_6.Text = "Замена фильтра";
                        textBox_parts_6.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_7.Text = "Замена уплотнителя";
                        textBox_parts_7.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "16")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_2.Text = "Замена клавиатуры РТТ";
                        textBox_parts_2.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        textBox_сompleted_works_3.Text = "Замена держателя боковой клавиатуры";
                        textBox_parts_3.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        textBox_сompleted_works_4.Text = "Замена клавиши РТТ";
                        textBox_parts_4.Text = "4080523Z02 Клавиша РТТ";

                        textBox_сompleted_works_5.Text = "Замена герметика верхней панели";
                        textBox_parts_5.Text = "3280533Z05 Герметик верхней панели ";

                        textBox_сompleted_works_6.Text = "Замена фронтальной наклейки";
                        textBox_parts_6.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        textBox_сompleted_works_7.Text = "Замена батарейных контактов";
                        textBox_parts_7.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена кнопки РТТ";
                        textBox_parts_1.Text = "4070354A01 Кнопка РТТ";

                        textBox_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена О кольца";
                        textBox_parts_3.Text = "32012111001 О кольцо";

                        textBox_сompleted_works_4.Text = "Замена резиновой клавиши PTT";
                        textBox_parts_4.Text = "75012081001 Резиновая клавиша РТТ";

                        textBox_сompleted_works_5.Text = "Замена накладки РТТ";
                        textBox_parts_5.Text = "38012011001 Накладка РТТ";

                        textBox_сompleted_works_6.Text = "Замена контактов АКБ";
                        textBox_parts_6.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "Заглушка МР37";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена транзистора";
                        textBox_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_5.Text = "Замена фильтра";
                        textBox_parts_5.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "Заглушка МР37";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена транзистора";
                        textBox_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_5.Text = "Замена фильтра";
                        textBox_parts_5.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "Заглушка МР37";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена транзистора";
                        textBox_parts_4.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_5.Text = "Замена фильтра";
                        textBox_parts_5.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "17")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена передней панели";
                        textBox_parts_1.Text = "1580666Z03 Передняя панель для GP-340";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_4.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_5.Text = "Замена клавиатуры РТТ";
                        textBox_parts_5.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        textBox_сompleted_works_6.Text = "Замена держателя боковой клавиатуры";
                        textBox_parts_6.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        textBox_сompleted_works_7.Text = "Замена клавиши РТТ";
                        textBox_parts_7.Text = "4080523Z02 Клавиша РТТ";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена кнопки РТТ";
                        textBox_parts_1.Text = "4070354A01 Кнопка РТТ";

                        textBox_сompleted_works_2.Text = "Замена контактов АКБ";
                        textBox_parts_2.Text = "0915184H01 Контакты АКБ";

                        textBox_сompleted_works_3.Text = "Замена корпуса";
                        textBox_parts_3.Text = "Корпус DP2400(e)";

                        textBox_сompleted_works_4.Text = "Замена резиновой клавиши PTT";
                        textBox_parts_4.Text = "75012081001 Резиновая клавиша РТТ";

                        textBox_сompleted_works_5.Text = "Замена накладки РТТ";
                        textBox_parts_5.Text = "38012011001 Накладка РТТ";

                        textBox_сompleted_works_6.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_6.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена фильтра";
                        textBox_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя";
                        textBox_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_5.Text = "Замена транзистора";
                        textBox_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = ")";


                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена фильтра";
                        textBox_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя";
                        textBox_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_5.Text = "Замена транзистора";
                        textBox_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = ")";



                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена фильтра";
                        textBox_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя";
                        textBox_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_5.Text = "Замена транзистора";
                        textBox_parts_5.Text = "Транзистор 2SK2973 (MTS101P)";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = ")";


                        button_save_add_rst_remont.Enabled = true;
                    }
                }

            }

            if (comboBox_сategory.Text == "5")
            {
                if (comboBox_remont_select.Text == "1")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя О-кольца";
                        textBox_parts_2.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_4.Text = "Замена шлейфа";
                        textBox_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_5.Text = "Замена микрофона";
                        textBox_parts_5.Text = "5015027H01 Микрофон для GP-340";

                        textBox_сompleted_works_6.Text = "Замена антенны";
                        textBox_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        textBox_parts_1.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "50012013001 Динамик";

                        textBox_сompleted_works_3.Text = "Замена О кольца";
                        textBox_parts_3.Text = "32012111001 О кольцо";

                        textBox_сompleted_works_4.Text = "Замена накладки РТТ";
                        textBox_parts_4.Text = "HN000696A01 Накладка РТТ";

                        textBox_сompleted_works_5.Text = "Замена верхнего уплотнителя";
                        textBox_parts_5.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "Заглушка МР37";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена ручки";
                        textBox_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_5.Text = "";
                        textBox_parts_5.Text = "";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "Заглушка МР37";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена ручки";
                        textBox_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_5.Text = "";
                        textBox_parts_5.Text = "";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "Заглушка МР37";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена ручки";
                        textBox_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_5.Text = "";
                        textBox_parts_5.Text = "";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "2")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена шлейфа";
                        textBox_parts_1.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_2.Text = "Замена войлока GP-340";
                        textBox_parts_2.Text = "3586057А02 Войлок GP340";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_6.Text = "Замена антенны";
                        textBox_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена сетки динамика";
                        textBox_parts_1.Text = "35012060001 Сетка динамика";

                        textBox_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        textBox_parts_3.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        textBox_сompleted_works_4.Text = "Замена ручки переключения каналов";
                        textBox_parts_4.Text = "36012017001 Ручка переключения каналов";

                        textBox_сompleted_works_5.Text = "Замена верхнего уплотнителя";
                        textBox_parts_5.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";


                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена защелки АКБ";
                        textBox_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_2.Text = "Замена клавиатуры";
                        textBox_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "Заглушка МР37";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена защелки АКБ";
                        textBox_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_2.Text = "Замена клавиатуры";
                        textBox_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "Заглушка МР37";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена защелки АКБ";
                        textBox_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_2.Text = "Замена клавиатуры";
                        textBox_parts_2.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "Заглушка МР37";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";
                    }

                }

                if (comboBox_remont_select.Text == "3")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена войлока GP-340";
                        textBox_parts_1.Text = "3586057А02 Войлок GP340";

                        textBox_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        textBox_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        textBox_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_6.Text = "Замена антенны";
                        textBox_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена кнопки РТТ";
                        textBox_parts_1.Text = "4070354A01 Кнопка РТТ";

                        textBox_сompleted_works_2.Text = "Замена регулятора громкости";
                        textBox_parts_2.Text = "1875103С04 Регулятор громкости";

                        textBox_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена верхнего уплотнителя";
                        textBox_parts_5.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        textBox_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";


                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена клавиатуры";
                        textBox_parts_1.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена контакта -";
                        textBox_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя";
                        textBox_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена клавиатуры";
                        textBox_parts_1.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена контакта -";
                        textBox_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя";
                        textBox_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена клавиатуры";
                        textBox_parts_1.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена контакта -";
                        textBox_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя";
                        textBox_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "4")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_2.Text = "Замена батарейных контактов";
                        textBox_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_3.Text = "Замена шлейфа";
                        textBox_parts_3.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_5.Text = "Замена ручки регулятора громкости";
                        textBox_parts_5.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        textBox_сompleted_works_6.Text = "Замена антенны";
                        textBox_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена верхнего уплотнителя";
                        textBox_parts_1.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_2.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        textBox_parts_2.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_3.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "50012013001 Динамик";

                        textBox_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";


                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена ручки";
                        textBox_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена кнопки";
                        textBox_parts_3.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена ручки";
                        textBox_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена кнопки";
                        textBox_parts_3.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена ручки";
                        textBox_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена кнопки";
                        textBox_parts_3.Text = "Кнопка РТТ для F16";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "5")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена шлейфа";
                        textBox_parts_1.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        textBox_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_5.Text = "Замена батарейных контактов";
                        textBox_parts_5.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_6.Text = "Замена антенны";
                        textBox_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_1.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "50012013001 Динамик";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        textBox_parts_3.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        textBox_сompleted_works_4.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_4.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        textBox_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";


                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена резины";
                        textBox_parts_1.Text = "Резина МР12";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена разъёма";
                        textBox_parts_4.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_5.Text = "Замена контакта + ";
                        textBox_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        textBox_сompleted_works_6.Text = "Замена панели защелки";
                        textBox_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена резины";
                        textBox_parts_1.Text = "Резина МР12";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена разъёма";
                        textBox_parts_4.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_5.Text = "Замена контакта + ";
                        textBox_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        textBox_сompleted_works_6.Text = "Замена панели защелки";
                        textBox_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена резины";
                        textBox_parts_1.Text = "Резина МР12";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена разъёма";
                        textBox_parts_4.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_5.Text = "Замена контакта + ";
                        textBox_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        textBox_сompleted_works_6.Text = "Замена панели защелки";
                        textBox_parts_6.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "6")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_1.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_2.Text = "Замена ручки регулятора громкости";
                        textBox_parts_2.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        textBox_сompleted_works_3.Text = "Замена шлейфа";
                        textBox_parts_3.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_4.Text = "Замена ручки переключателя каналов";
                        textBox_parts_4.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_6.Text = "Замена антенны";
                        textBox_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя регулятора громкости и перключателя каналов";
                        textBox_parts_2.Text = "32012269001 Уплотнитель регулятора громкости и перключателя каналов";

                        textBox_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "50012013001 Динамик";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";


                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена защелки АКБ";
                        textBox_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_5.Text = "";
                        textBox_parts_5.Text = "";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена защелки АКБ";
                        textBox_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_5.Text = "";
                        textBox_parts_5.Text = "";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена защелки АКБ";
                        textBox_parts_1.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_5.Text = "";
                        textBox_parts_5.Text = "";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "7")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_1.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_2.Text = "Замена микрофона";
                        textBox_parts_2.Text = "5015027H01 Микрофон для GP-340";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_5.Text = "Замена шлейфа";
                        textBox_parts_5.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_6.Text = "Замена антенны";
                        textBox_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_2.Text = "Замена О кольца";
                        textBox_parts_2.Text = "32012111001 О кольцо";

                        textBox_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена микрофона";
                        textBox_parts_4.Text = "5015027H01 Микрофон для DP2400";

                        textBox_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        textBox_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        textBox_сompleted_works_6.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_6.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена разъёма";
                        textBox_parts_1.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_2.Text = "Замена защелки АКБ";
                        textBox_parts_2.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "Заглушка МР37";

                        textBox_сompleted_works_4.Text = "Замена ручки";
                        textBox_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена разъёма";
                        textBox_parts_1.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_2.Text = "Замена защелки АКБ";
                        textBox_parts_2.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "Заглушка МР37";

                        textBox_сompleted_works_4.Text = "Замена ручки";
                        textBox_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена разъёма";
                        textBox_parts_1.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_2.Text = "Замена защелки АКБ";
                        textBox_parts_2.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "Заглушка МР37";

                        textBox_сompleted_works_4.Text = "Замена ручки";
                        textBox_parts_4.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "8")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_1.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_2.Text = "Замена батарейных контактов";
                        textBox_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_3.Text = "Замена передней панели";
                        textBox_parts_3.Text = "1580666Z03 Передняя панель для GP-340";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_5.Text = "Замена липучки интерфейсного разъёма";
                        textBox_parts_5.Text = "1386058A01 Липучка интерфейсного разъема";

                        textBox_сompleted_works_6.Text = "Замена антенны";
                        textBox_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_2.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        textBox_parts_2.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        textBox_сompleted_works_3.Text = "Замена верхнего уплотнителя";
                        textBox_parts_3.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";


                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя";
                        textBox_parts_4.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "Заглушка МР37";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя";
                        textBox_parts_4.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "Заглушка МР37";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_2.Text = "Замена ручки";
                        textBox_parts_2.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя";
                        textBox_parts_4.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "Заглушка МР37";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "9")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена герметика верхней панели";
                        textBox_parts_1.Text = "3280533Z05 Герметик верхней панели";

                        textBox_сompleted_works_2.Text = "Замена батарейных контактов";
                        textBox_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_5.Text = "Замена заглушки";
                        textBox_parts_5.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_6.Text = "Замена антенны";
                        textBox_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "50012013001 Динамик";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_3.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_4.Text = "Замена верхнего уплотнителя";
                        textBox_parts_4.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_5.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_5.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена прокладки";
                        textBox_parts_1.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_4.Text = "Замена защелки АКБ";
                        textBox_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена прокладки";
                        textBox_parts_1.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_4.Text = "Замена защелки АКБ";
                        textBox_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена прокладки";
                        textBox_parts_1.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена заглушки";
                        textBox_parts_3.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_4.Text = "Замена защелки АКБ";
                        textBox_parts_4.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_5.Text = "Замена динамика";
                        textBox_parts_5.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "10")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена ручки переключателя каналов";
                        textBox_parts_1.Text = "3680530Z02 Ручка переключения каналов GP-340";

                        textBox_сompleted_works_2.Text = "Замена войлока GP-340";
                        textBox_parts_2.Text = "3586057А02 Войлок GP340";

                        textBox_сompleted_works_3.Text = "Замена заглушки HLN9820";
                        textBox_parts_3.Text = "Заглушка для GP-серии";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя О-кольца";
                        textBox_parts_4.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        textBox_сompleted_works_5.Text = "Замена держателя боковой клавиатуры";
                        textBox_parts_5.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        textBox_сompleted_works_6.Text = "Замена антенны";
                        textBox_parts_6.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "50012013001 Динамик";

                        textBox_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_5.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_5.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "Замена регулятора громкости";
                        textBox_parts_6.Text = "1875103С04 Регулятор громкости";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";


                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена клавиатуры";
                        textBox_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена клавиатуры";
                        textBox_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена клавиатуры";
                        textBox_parts_4.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_5.Text = "Замена прокладки";
                        textBox_parts_5.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "11")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя О-кольца";
                        textBox_parts_2.Text = "3280536Z01 Уплотнитель О-кольца GP340";

                        textBox_сompleted_works_3.Text = "Замена микрофона";
                        textBox_parts_3.Text = "5015027H01 Микрофон для GP-340";

                        textBox_сompleted_works_4.Text = "Замена шлейфа";
                        textBox_parts_4.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_5.Text = "Замена антенны";
                        textBox_parts_5.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена верхнего уплотнителя";
                        textBox_parts_3.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_4.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_5.Text = "Замена О кольца";
                        textBox_parts_5.Text = "32012111001 О кольцо";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена контакта -";
                        textBox_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        textBox_сompleted_works_5.Text = "Замена клавиатуры";
                        textBox_parts_5.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена контакта -";
                        textBox_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        textBox_сompleted_works_5.Text = "Замена клавиатуры";
                        textBox_parts_5.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя";
                        textBox_parts_1.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 PLUS TERMINAL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена контакта -";
                        textBox_parts_4.Text = "2251 MINUS TERMINAL Контакт -";

                        textBox_сompleted_works_5.Text = "Замена клавиатуры";
                        textBox_parts_5.Text = "Клавиатура 2251 MAIN SEAL";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "12")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_1.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_2.Text = "Замена батарейных контактов";
                        textBox_parts_2.Text = "0986237А02 Контакты для подсоед.аккум.в серии р/ст WARIS";

                        textBox_сompleted_works_3.Text = "Замена липучки интерфейсного разъёма";
                        textBox_parts_3.Text = "1386058A01 Липучка интерфейсного разъема";

                        textBox_сompleted_works_4.Text = "Замена антенны";
                        textBox_parts_4.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_5.Text = "";
                        textBox_parts_5.Text = "";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена кнопки РТТ";
                        textBox_parts_1.Text = "4070354A01 Кнопка РТТ";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_4.Text = "Замена верхнего уплотнителя";
                        textBox_parts_4.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_5.Text = "Замена О кольца";
                        textBox_parts_5.Text = "32012111001 О кольцо";

                        textBox_сompleted_works_6.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_6.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";


                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_3.Text = "Замена кнопки";
                        textBox_parts_3.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "Заглушка МР37";

                        textBox_сompleted_works_5.Text = "Замена ручки";
                        textBox_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_3.Text = "Замена кнопки";
                        textBox_parts_3.Text = "Кнопка РТТ для F3G (22300001070)";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "Заглушка МР37";

                        textBox_сompleted_works_5.Text = "Замена ручки";
                        textBox_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "2251 JACK CAP Резиновая заглушка";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_3.Text = "Замена кнопки";
                        textBox_parts_3.Text = "Кнопка РТТ для F16";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "Заглушка МР37";

                        textBox_сompleted_works_5.Text = "Замена ручки";
                        textBox_parts_5.Text = "Ручка регулятора громкости F-16";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "13")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена заглушки";
                        textBox_parts_1.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_2.Text = "Замена шлейфа";
                        textBox_parts_2.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_3.Text = "Замена ручки регулятора громкости";
                        textBox_parts_3.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_5.Text = "Замена антенны";
                        textBox_parts_5.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена верхнего уплотнителя";
                        textBox_parts_1.Text = "32012089001 Верхний уплотнитель";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_2.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_3.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_3.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "50012013001 Динамик";

                        textBox_сompleted_works_5.Text = "Замена держателя РТТ (Клавиатура программирования)";
                        textBox_parts_5.Text = "42012035001 Держатель РТТ (Клавиатура программирования)";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена ручки";
                        textBox_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_2.Text = "Замена резины";
                        textBox_parts_2.Text = "Резина МР12";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена панели защелки";
                        textBox_parts_4.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        textBox_сompleted_works_5.Text = "Замена контакта + ";
                        textBox_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена ручки";
                        textBox_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_2.Text = "Замена резины";
                        textBox_parts_2.Text = "Резина МР12";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена панели защелки";
                        textBox_parts_4.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        textBox_сompleted_works_5.Text = "Замена контакта + ";
                        textBox_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена ручки";
                        textBox_parts_1.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_2.Text = "Замена резины";
                        textBox_parts_2.Text = "Резина МР12";

                        textBox_сompleted_works_3.Text = "Замена прокладки";
                        textBox_parts_3.Text = "2251 JACK PANEL Прокладка";

                        textBox_сompleted_works_4.Text = "Замена панели защелки";
                        textBox_parts_4.Text = "Панель для защелки АКБ 2251 REAL PANEL";

                        textBox_сompleted_works_5.Text = "Замена контакта + ";
                        textBox_parts_5.Text = "2251 PLUS TERMINAL Контакт  + ";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "14")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена шлейфа";
                        textBox_parts_1.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_2.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_3.Text = "Замена динамика";
                        textBox_parts_3.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_5.Text = "Замена антенны";
                        textBox_parts_5.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_1.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "50012013001 Динамик";

                        textBox_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_4.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_5.Text = "Замена ручки переключения каналов";
                        textBox_parts_5.Text = "36012017001 Ручка переключения каналов";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена разъёма";
                        textBox_parts_4.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_5.Text = "Замена ручки";
                        textBox_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена разъёма";
                        textBox_parts_4.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_5.Text = "Замена ручки";
                        textBox_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена защелки АКБ";
                        textBox_parts_3.Text = "2251 RELESE BUTTON Защелка для АКБ";

                        textBox_сompleted_works_4.Text = "Замена разъёма";
                        textBox_parts_4.Text = "Разъем антенный корпусной";

                        textBox_сompleted_works_5.Text = "Замена ручки";
                        textBox_parts_5.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "15")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена шлейфа";
                        textBox_parts_1.Text = "8415169Н01 Шлейф динамика и микрофона для GP-серии";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "HLN9820	Заглушка для GP-серии";

                        textBox_сompleted_works_3.Text = "Замена ручки регулятора громкости";
                        textBox_parts_3.Text = "3680529Z01 Ручка регулятора громкости GP-340";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_5.Text = "Замена антенны";
                        textBox_parts_5.Text = "Антенна NAD6502 146-174Mгц";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена антенны";
                        textBox_parts_1.Text = "PMAD4120 Антенна PMAD4120 (146-160мГц)";

                        textBox_сompleted_works_2.Text = "Замена О кольца";
                        textBox_parts_2.Text = "32012111001 О кольцо";

                        textBox_сompleted_works_3.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_3.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена динамика";
                        textBox_parts_4.Text = "50012013001 Динамик";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя контактов АКБ";
                        textBox_parts_5.Text = "32012110001 Уплотнитель контактов АКБ";

                        textBox_сompleted_works_6.Text = "Замена кнопки РТТ";
                        textBox_parts_6.Text = "4070354A01 Кнопка РТТ";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";


                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "Заглушка МР37";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя";
                        textBox_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "Заглушка МР37";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя";
                        textBox_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "K036NA500-66 Динамик для IC-44088";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 JACK RUBBER Резин.уплотнитель разъема гарнитуры IC-F3/4/GT/GS";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена заглушки";
                        textBox_parts_4.Text = "Заглушка МР37";

                        textBox_сompleted_works_5.Text = "Замена уплотнителя";
                        textBox_parts_5.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "16")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена динамика";
                        textBox_parts_1.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_2.Text = "Замена клавиатуры РТТ";
                        textBox_parts_2.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        textBox_сompleted_works_3.Text = "Замена держателя боковой клавиатуры";
                        textBox_parts_3.Text = "1380528Z01 Держатель боковой клавиатуры для GP-340";

                        textBox_сompleted_works_4.Text = "Замена клавиши РТТ";
                        textBox_parts_4.Text = "4080523Z02 Клавиша РТТ";

                        textBox_сompleted_works_5.Text = "Замена герметика верхней панели";
                        textBox_parts_5.Text = "3280533Z05 Герметик верхней панели ";

                        textBox_сompleted_works_6.Text = "Замена фронтальной наклейки";
                        textBox_parts_6.Text = "1364279В03 Фронтальная наклейка для GP-340";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена кнопки РТТ";
                        textBox_parts_1.Text = "4070354A01 Кнопка РТТ";

                        textBox_сompleted_works_2.Text = "Замена  ручки регулятора громкости";
                        textBox_parts_2.Text = "36012016001 Ручка регулятора громкости";

                        textBox_сompleted_works_3.Text = "Замена О кольца";
                        textBox_parts_3.Text = "32012111001 О кольцо";

                        textBox_сompleted_works_4.Text = "Замена резиновой клавиши PTT";
                        textBox_parts_4.Text = "75012081001 Резиновая клавиша РТТ";

                        textBox_сompleted_works_5.Text = "Замена накладки РТТ";
                        textBox_parts_5.Text = "38012011001 Накладка РТТ";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена резины";
                        textBox_parts_1.Text = "Резина МР12";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя";
                        textBox_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_5.Text = "Замена кнопки РТТ";
                        textBox_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена резины";
                        textBox_parts_1.Text = "Резина МР12";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя";
                        textBox_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_5.Text = "Замена кнопки РТТ";
                        textBox_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена резины";
                        textBox_parts_1.Text = "Резина МР12";

                        textBox_сompleted_works_2.Text = "Замена уплотнителя";
                        textBox_parts_2.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя";
                        textBox_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_5.Text = "Замена кнопки РТТ";
                        textBox_parts_5.Text = "JPM1990-2711R Кнопка РТТ";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }

                if (comboBox_remont_select.Text == "17")
                {
                    if (textBox_model.Text == "Motorola GP-340")
                    {
                        textBox_сompleted_works_1.Text = "Замена передней панели";
                        textBox_parts_1.Text = "1580666Z03 Передняя панель для GP-340";

                        textBox_сompleted_works_2.Text = "Замена динамика";
                        textBox_parts_2.Text = "5005589U05 Динамик для GP-300/600";

                        textBox_сompleted_works_3.Text = "Замена уплотнителя бат. контактов";
                        textBox_parts_3.Text = "3280534Z01 Уплотнит.резин.батар.контактов Karizma";

                        textBox_сompleted_works_4.Text = "Замена клавиатуры РТТ";
                        textBox_parts_4.Text = "7580532Z01 Клавиатура РТТ для GP-340/640";

                        textBox_сompleted_works_5.Text = "Замена клавиши РТТ";
                        textBox_parts_5.Text = "4080523Z02 Клавиша РТТ";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Motorola DP-2400" || textBox_model.Text == "Motorola DP-2400е")
                    {
                        textBox_сompleted_works_1.Text = "Замена кнопки РТТ";
                        textBox_parts_1.Text = "4070354A01 Кнопка РТТ";

                        textBox_сompleted_works_2.Text = "Замена корпуса";
                        textBox_parts_2.Text = "Корпус DP2400(e)";

                        textBox_сompleted_works_3.Text = "Замена резиновой клавиши PTT";
                        textBox_parts_3.Text = "75012081001 Резиновая клавиша РТТ";

                        textBox_сompleted_works_4.Text = "Замена накладки РТТ";
                        textBox_parts_4.Text = "38012011001 Накладка РТТ";

                        textBox_сompleted_works_5.Text = "Замена гибкого шлейфа динамика";
                        textBox_parts_5.Text = "PF001006A02 Гибкий шлейф динамика";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = "";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GS")
                    {
                        textBox_сompleted_works_1.Text = "Замена фильтра";
                        textBox_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя";
                        textBox_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_5.Text = "";
                        textBox_parts_5.Text = "";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = ")";


                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F3GT")
                    {
                        textBox_сompleted_works_1.Text = "Замена фильтра";
                        textBox_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя";
                        textBox_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_5.Text = "";
                        textBox_parts_5.Text = "";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = ")";

                        button_save_add_rst_remont.Enabled = true;
                    }

                    else if (textBox_model.Text == "Icom IC-F16")
                    {
                        textBox_сompleted_works_1.Text = "Замена фильтра";
                        textBox_parts_1.Text = "Фильтр S.XTRAL CR-664A 15.300 MHz";

                        textBox_сompleted_works_2.Text = "Замена заглушки";
                        textBox_parts_2.Text = "Заглушка МР37";

                        textBox_сompleted_works_3.Text = "Замена ручки";
                        textBox_parts_3.Text = "KNOB N-276 Ручка регулятора громкости";

                        textBox_сompleted_works_4.Text = "Замена уплотнителя";
                        textBox_parts_4.Text = "2251 MAIN SEAL Уплотнительная резинка";

                        textBox_сompleted_works_5.Text = "";
                        textBox_parts_5.Text = "";

                        textBox_сompleted_works_6.Text = "";
                        textBox_parts_6.Text = "";

                        textBox_сompleted_works_7.Text = "";
                        textBox_parts_7.Text = ")";

                        button_save_add_rst_remont.Enabled = true;
                    }
                }
            }

        }

        #endregion
    }
}
