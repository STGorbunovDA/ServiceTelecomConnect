using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ServiceTelecomConnect.Forms
{
    public partial class AddRadioStationParametersForm : Form
    {
        public AddRadioStationParametersForm()
        {
            InitializeComponent();
        }

        void AddRadioStationParametersFormLoad(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            monthCalendar1.Visible = false;
            var myCulture = new CultureInfo("ru-RU");
            myCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = myCulture;
            txB_dateTO.ReadOnly = true;
            QuerySettingDataBase.CmbGettingFrequenciesRST(cmB_frequency);

            if (CheacSerialNumber.GetInstance.CheackSerialNumberRadiostationParameters(lbL_road.Text, lbL_city.Text, txB_serialNumber.Text))
            {
                var queryRadiostantionParameters = $"SELECT dateTO, lowPowerLevelTransmitter, highPowerLevelTransmitter, frequencyDeviationTransmitter," +
               $"sensitivityTransmitter, kniTransmitter, deviationTransmitter, outputPowerVoltReceiver, outputPowerWattReceiver, selectivityReceiver," +
               $"sensitivityReceiver, kniReceiver, suppressorReceiver, standbyModeCurrentConsumption, receptionModeCurrentConsumption, " +
               $"transmissionModeCurrentConsumption, batteryDischargeAlarmCurrentConsumption, transmitterFrequencies, receiverFrequencies, " +
               $"batteryChargerAccessories, manipulatorAccessories, nameAKB, percentAKB, noteRadioStationParameters, verifiedRST " +
               $"FROM radiostation_parameters WHERE road = '{lbL_road.Text}' AND city = '{lbL_city.Text}' " +
               $"AND serialNumber = '{txB_serialNumber.Text}'";
                using (MySqlCommand command = new MySqlCommand(queryRadiostantionParameters, DB_3.GetInstance.GetConnection()))
                {
                    DB_3.GetInstance.OpenConnection();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            txB_dateTO.Text = Convert.ToDateTime(reader[0].ToString()).ToString("dd.MM.yyyy");
                            txB_LowPowerLevelTransmitter.Text = reader[1].ToString();
                            txB_HighPowerLevelTransmitter.Text = reader[2].ToString();
                            txB_FrequencyDeviationTransmitter.Text = reader[3].ToString();
                            txB_SensitivityTransmitter.Text = reader[4].ToString();
                            txB_KNITransmitter.Text = reader[5].ToString();
                            txB_DeviationTransmitter.Text = reader[6].ToString();
                            txB_OutputPowerVoltReceiver.Text = reader[7].ToString();
                            txB_OutputPowerWattReceiver.Text = reader[8].ToString();
                            txB_SelectivityReceiver.Text = reader[9].ToString();
                            txB_SensitivityReceiver.Text = reader[10].ToString();
                            txB_KNIReceiver.Text = reader[11].ToString();
                            txB_SuppressorReceiver.Text = reader[12].ToString();
                            txB_StandbyModeCurrentConsumption.Text = reader[13].ToString();
                            txB_ReceptionModeCurrentConsumption.Text = reader[14].ToString();
                            txB_TransmissionModeCurrentConsumption.Text = reader[15].ToString();
                            txB_BatteryDischargeAlarmCurrentConsumption.Text = reader[16].ToString();
                            txB_TransmitterFrequencies.Text = reader[17].ToString();
                            txB_ReceiverFrequencies.Text = reader[18].ToString();
                            cmB_BatteryChargerAccessories.Text = reader[19].ToString();
                            cmB_ManipulatorAccessories.Text = reader[20].ToString();
                            lbL_nameAKB.Text = reader[21].ToString();
                            txB_percentAKB.Text = reader[22].ToString();
                            txB_NoteRadioStationParameters.Text = reader[23].ToString();

                            if (reader[24].ToString() == "+")
                            {
                                lbl_verifiedRST.Visible = true;
                            }
                            else if (reader[24].ToString() == "?")
                            {
                                chB_InRepair.Checked = true;
                                lbl_verifiedRST.Text = "В ремонте";
                                lbl_verifiedRST.ForeColor = Color.Red;
                                lbl_verifiedRST.Visible = true;
                            }

                        }
                        reader.Close();
                    }
                    DB_3.GetInstance.CloseConnection();

                    if (String.IsNullOrEmpty(cmB_BatteryChargerAccessories.Text) || cmB_BatteryChargerAccessories.Text == "-")
                        cmB_BatteryChargerAccessories.Enabled = false;
                    if (String.IsNullOrEmpty(cmB_ManipulatorAccessories.Text) || cmB_ManipulatorAccessories.Text == "-")
                        cmB_ManipulatorAccessories.Enabled = false;
                }

                if (txB_percentAKB.Text == "неиспр.")
                {
                    chB_Faulty.Checked = true;
                    txB_percentAKB.Enabled = false;
                }
                else
                {
                    txB_percentAKB.Enabled = true;
                    chB_Faulty.Checked = false;
                }
            }
            else
            {
                #region заполнение парметров РСТ

                if (txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-320")
                {
                    // Передатчик
                    txB_LowPowerLevelTransmitter.Text = "2.15";
                    txB_HighPowerLevelTransmitter.Text = "4.88";
                    txB_FrequencyDeviationTransmitter.Text = "+55";
                    txB_SensitivityTransmitter.Text = "9.5";
                    txB_KNITransmitter.Text = "0.87";
                    txB_DeviationTransmitter.Text = "4.55";
                    //Приёмник
                    txB_OutputPowerVoltReceiver.Text = "5.10";
                    txB_OutputPowerWattReceiver.Text = ">0.5";
                    txB_SelectivityReceiver.Text = "71";
                    txB_SensitivityReceiver.Text = "0.21";
                    txB_KNIReceiver.Text = "0.87";
                    txB_SuppressorReceiver.Text = "0.15";
                    //Потребляемый ток
                    txB_StandbyModeCurrentConsumption.Text = "50";
                    txB_ReceptionModeCurrentConsumption.Text = "340";
                    txB_TransmissionModeCurrentConsumption.Text = "1.55";
                    txB_BatteryDischargeAlarmCurrentConsumption.Text = "6.0";
                }
                else if (txB_model.Text == "Motorola GP-360")
                {
                    // Передатчик
                    txB_LowPowerLevelTransmitter.Text = "2.15";
                    txB_HighPowerLevelTransmitter.Text = "4.88";
                    txB_FrequencyDeviationTransmitter.Text = "+55";
                    txB_SensitivityTransmitter.Text = "9.5";
                    txB_KNITransmitter.Text = "0.87";
                    txB_DeviationTransmitter.Text = "4.55";
                    //Приёмник
                    txB_OutputPowerVoltReceiver.Text = "5.10";
                    txB_OutputPowerWattReceiver.Text = ">0.5";
                    txB_SelectivityReceiver.Text = "71";
                    txB_SensitivityReceiver.Text = "0.21";
                    txB_KNIReceiver.Text = "0.87";
                    txB_SuppressorReceiver.Text = "0.15";
                    //Потребляемый ток
                    txB_StandbyModeCurrentConsumption.Text = "70";
                    txB_ReceptionModeCurrentConsumption.Text = "340";
                    txB_TransmissionModeCurrentConsumption.Text = "1.55";
                    txB_BatteryDischargeAlarmCurrentConsumption.Text = "6.0";
                }
                else if (txB_model.Text == "Icom IC-F3GS" || txB_model.Text == "Icom IC-F3GT" ||
                    txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F11")
                {
                    // Передатчик
                    txB_LowPowerLevelTransmitter.Text = "2.12";
                    txB_HighPowerLevelTransmitter.Text = "4.75";
                    txB_FrequencyDeviationTransmitter.Text = "+120";
                    txB_SensitivityTransmitter.Text = "15.5";
                    txB_KNITransmitter.Text = "1.50";
                    txB_DeviationTransmitter.Text = "4.65";
                    //Приёмник
                    txB_OutputPowerVoltReceiver.Text = "2.71";
                    txB_OutputPowerWattReceiver.Text = ">0.5";
                    txB_SelectivityReceiver.Text = "71";
                    txB_SensitivityReceiver.Text = "0.23";
                    txB_KNIReceiver.Text = "1.62";
                    txB_SuppressorReceiver.Text = "0.15";
                    //Потребляемый ток
                    txB_StandbyModeCurrentConsumption.Text = "70";
                    txB_ReceptionModeCurrentConsumption.Text = "180";
                    txB_TransmissionModeCurrentConsumption.Text = "1.45";
                    txB_BatteryDischargeAlarmCurrentConsumption.Text = "6.0";
                }
                else if (txB_model.Text == "Motorola DP-2400е" || txB_model.Text == "Motorola DP-2400" ||
                    txB_model.Text == "Motorola DP-1400" || txB_model.Text == "Motorola DP-4400")
                {
                    // Передатчик
                    txB_LowPowerLevelTransmitter.Text = "2.15";
                    txB_HighPowerLevelTransmitter.Text = "4.88";
                    txB_FrequencyDeviationTransmitter.Text = "+55";
                    txB_SensitivityTransmitter.Text = "9.5";
                    txB_KNITransmitter.Text = "1.35";
                    txB_DeviationTransmitter.Text = "4.55";
                    //Приёмник
                    txB_OutputPowerVoltReceiver.Text = "5.10";
                    txB_OutputPowerWattReceiver.Text = ">0.5";
                    txB_SelectivityReceiver.Text = "-";
                    txB_SensitivityReceiver.Text = "0.21";
                    txB_KNIReceiver.Text = "1.55";
                    txB_SuppressorReceiver.Text = "0.15";
                    //Потребляемый ток
                    txB_StandbyModeCurrentConsumption.Text = "70";
                    txB_ReceptionModeCurrentConsumption.Text = "400";
                    txB_TransmissionModeCurrentConsumption.Text = "1.55";
                    txB_BatteryDischargeAlarmCurrentConsumption.Text = "6.0";
                }
                else if (txB_model.Text == "Альтавия-301М" || txB_model.Text == "Элодия-351М")
                {
                    // Передатчик
                    txB_LowPowerLevelTransmitter.Text = "2.15";
                    txB_HighPowerLevelTransmitter.Text = "4.88";
                    txB_FrequencyDeviationTransmitter.Text = "+55";
                    txB_SensitivityTransmitter.Text = "15.5";
                    txB_KNITransmitter.Text = "1.35";
                    txB_DeviationTransmitter.Text = "4.55";
                    //Приёмник
                    txB_OutputPowerVoltReceiver.Text = "5.10";
                    txB_OutputPowerWattReceiver.Text = ">0.5";
                    txB_SelectivityReceiver.Text = "76";
                    txB_SensitivityReceiver.Text = "0.21";
                    txB_KNIReceiver.Text = "1.55";
                    txB_SuppressorReceiver.Text = "0.15";
                    //Потребляемый ток
                    txB_StandbyModeCurrentConsumption.Text = "40";
                    txB_ReceptionModeCurrentConsumption.Text = "190";
                    txB_TransmissionModeCurrentConsumption.Text = "1.55";
                    txB_BatteryDischargeAlarmCurrentConsumption.Text = "6.0";
                }
                else if (txB_model.Text == "Comrade R5")
                {
                    // Передатчик
                    txB_LowPowerLevelTransmitter.Text = "2.15";
                    txB_HighPowerLevelTransmitter.Text = "4.88";
                    txB_FrequencyDeviationTransmitter.Text = "+55";
                    txB_SensitivityTransmitter.Text = "7.5";
                    txB_KNITransmitter.Text = "1.35";
                    txB_DeviationTransmitter.Text = "4.55";
                    //Приёмник
                    txB_OutputPowerVoltReceiver.Text = "5.10";
                    txB_OutputPowerWattReceiver.Text = ">=0.4";
                    txB_SelectivityReceiver.Text = "71";
                    txB_SensitivityReceiver.Text = "0.21";
                    txB_KNIReceiver.Text = "1.55";
                    txB_SuppressorReceiver.Text = "0.15";
                    //Потребляемый ток
                    txB_StandbyModeCurrentConsumption.Text = "70";
                    txB_ReceptionModeCurrentConsumption.Text = "350";
                    txB_TransmissionModeCurrentConsumption.Text = "1.55";
                    txB_BatteryDischargeAlarmCurrentConsumption.Text = "6.0";
                }
                else
                {
                    // Передатчик
                    txB_LowPowerLevelTransmitter.Text = "2.15";
                    txB_HighPowerLevelTransmitter.Text = "4.75";
                    txB_FrequencyDeviationTransmitter.Text = "+105";
                    txB_SensitivityTransmitter.Text = "9.5";
                    txB_KNITransmitter.Text = "1.35";
                    txB_DeviationTransmitter.Text = "4.55";
                    //Приёмник
                    txB_OutputPowerVoltReceiver.Text = "4.5";
                    txB_OutputPowerWattReceiver.Text = ">0.5";
                    txB_SelectivityReceiver.Text = "71";
                    txB_SensitivityReceiver.Text = "0.24";
                    txB_KNIReceiver.Text = "1.55";
                    txB_SuppressorReceiver.Text = "0.17";
                    //Потребляемый ток
                    txB_StandbyModeCurrentConsumption.Text = "50";
                    txB_ReceptionModeCurrentConsumption.Text = "350";
                    txB_TransmissionModeCurrentConsumption.Text = "1.60";
                    txB_BatteryDischargeAlarmCurrentConsumption.Text = "6.0";
                }

                #endregion

                btn_DecommissionRadiostantion.Enabled = false;

                txB_dateTO.Text = DateTime.Now.ToString("dd.MM.yyyy");
                if (String.IsNullOrEmpty(lbL_BatteryChargerAccessories.Text) || lbL_BatteryChargerAccessories.Text == "-")
                    cmB_BatteryChargerAccessories.Enabled = false;
                if (String.IsNullOrEmpty(lbL_ManipulatorAccessories.Text) || lbL_ManipulatorAccessories.Text == "-")
                    cmB_ManipulatorAccessories.Enabled = false;
            }

            if (String.IsNullOrEmpty(lbL_nameAKB.Text) || lbL_nameAKB.Text == "-")
            {
                lbL_nameAKB.Visible = false;
                txB_percentAKB.Enabled = false;
            }
        }

        #region Дата проверки
        void TxB_dateTO_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }

        void MonthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            txB_dateTO.Text = e.End.ToString("dd.MM.yyyy");
            monthCalendar1.Visible = false;
        }
        #endregion

        #region Частоты KeyPress
        void TxB_TransmitterFrequencies_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != (char)Keys.Enter && ch != '\b' && ch != '.' && ch != '/')
                e.Handled = true;
        }

        void TxB_ReceiverFrequencies_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != (char)Keys.Enter && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void CmB_frequency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(!chb_repeater.Checked)
            {
                txB_TransmitterFrequencies.Text += cmB_frequency.Text + Environment.NewLine;
                txB_ReceiverFrequencies.Text += cmB_frequency.Text + Environment.NewLine;
            }
            else txB_TransmitterFrequencies.Text += cmB_frequency.Text + "/" + cmB_frequency.Text + Environment.NewLine;
        }


        void TxB_ReceiverFrequencies_Click(object sender, EventArgs e)
        {
            cmB_frequency.Visible = true;
        }

        #endregion

        #region Передатчик KeyPress
        void TxB_LowPowerLevelTransmitter_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TxB_HighPowerLevelTransmitter_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TxB_FrequencyDeviationTransmitter_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.' && ch != '+' && ch != '-')
                e.Handled = true;
        }

        void TxB_SensitivityTransmitter_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TxB_KNITransmitter_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TxB_DeviationTransmitter_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }
        #endregion

        #region Приёмник KeyPress

        void TxB_OutputPowerVoltReceiver_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TxB_OutputPowerWattReceiver_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.' && ch != '>')
                e.Handled = true;
        }

        void TxB_SelectivityReceiver_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.' && ch != '-')
                e.Handled = true;
        }

        void TxB_SensitivityReceiver_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TxB_KNIReceiver_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TxB_SuppressorReceiver_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        #endregion

        #region Потребляемый ток KeyPress

        void TxB_StandbyModeCurrentConsumption_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TxB_ReceptionModeCurrentConsumption_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TxB_TransmissionModeCurrentConsumption_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TxB_BatteryDischargeAlarmCurrentConsumption_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        #endregion

        #region АКБ KeyPress and click cheackbox 

        void ChB_Faulty_Click(object sender, EventArgs e)
        {
            if (lbL_nameAKB.Visible)
            {
                if (chB_Faulty.Checked)
                {
                    txB_percentAKB.Enabled = false;
                    txB_percentAKB.Text = "неиспр.";
                }
                else
                {
                    txB_percentAKB.Enabled = true;
                    txB_percentAKB.Text = String.Empty;
                }
            }
        }

        void TxB_AKB_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        #endregion

        #region добавляем частоту

        void Btn_Frequencies_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                AddFrequenciesForm addFrequencies = new AddFrequenciesForm();
                if (Application.OpenForms["AddFrequencies"] == null)
                {
                    string Mesage = "Вы действительно хотите добавить частоту?";

                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;
                    addFrequencies.ShowDialog();
                    QuerySettingDataBase.CmbGettingFrequenciesRST(cmB_frequency);
                }
            }
        }

        #endregion

        #region Добавляем параметры в БД
        void Btn_save_add_rst_remont_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                if(!chb_repeater.Checked)
                {
                    if (txB_TransmitterFrequencies.TextLength != txB_ReceiverFrequencies.TextLength)
                    {
                        MessageBox.Show($"Пропущена частота.\nP.s. приём и передача не могут существовать без друг друга\n как \"Инь Ян\"");
                        return;
                    }
                }

                #region проверка на пустные control-ы
                foreach (Control control in pnl_transmitter.Controls)
                {
                    if (control is TextBox)
                    {
                        if (String.IsNullOrEmpty(control.Text))
                        {
                            MessageBox.Show("Заполните параметры \"Передатчика\"");
                            control.Select();
                            return;
                        }
                    }
                }
                foreach (Control control in pnl_Receiver.Controls)
                {
                    if (control is TextBox)
                    {
                        if (String.IsNullOrEmpty(control.Text))
                        {
                            MessageBox.Show("Заполните параметры \"Приёмника\"");
                            control.Select();
                            return;
                        }
                    }
                }
                foreach (Control control in pnl_CurrentConsumption.Controls)
                {
                    if (control is TextBox)
                    {
                        if (String.IsNullOrEmpty(control.Text))
                        {
                            MessageBox.Show("Заполните параметры \"Потребляемый ток\"");
                            control.Select();
                            return;
                        }
                    }
                }
                if(!chb_repeater.Checked)
                {
                    foreach (Control control in pnl_frequencies.Controls)
                    {
                        if (control is TextBox)
                        {
                            if (String.IsNullOrEmpty(control.Text))
                            {
                                MessageBox.Show("Заполните параметры \"Частоты\"");
                                control.Select();
                                return;
                            }
                            control.Text.Trim();
                        }
                    }
                }
                else
                {
                    if(String.IsNullOrEmpty(txB_TransmitterFrequencies.Text))
                    {
                        MessageBox.Show("Заполните параметры \"Частоты (передатчик).\"");
                        return;
                    }
                }

                if (cmB_BatteryChargerAccessories.Enabled)
                {
                    if (String.IsNullOrEmpty(cmB_BatteryChargerAccessories.Text))
                    {
                        MessageBox.Show("Заполните параметры \"Аксессуары\"\n\"Зарядное устройство\"");
                        return;
                    }
                }
                if (cmB_ManipulatorAccessories.Enabled)
                {
                    if (String.IsNullOrEmpty(cmB_ManipulatorAccessories.Text))
                    {
                        MessageBox.Show("Заполните параметры \"Аксессуары\"\n\"Манипулятор\"");
                        return;
                    }
                }
                if (txB_percentAKB.Enabled)
                {
                    foreach (Control control in pnl_AKB.Controls)
                    {
                        if (control is TextBox)
                        {
                            if (String.IsNullOrEmpty(control.Text))
                            {
                                MessageBox.Show("Заполните параметры \"АКБ\"");
                                control.Select();
                                return;
                            }
                        }
                    }
                }
                #endregion

                #region передатчик
                if (!Regex.IsMatch(txB_LowPowerLevelTransmitter.Text, @"^[2-2]{1,1}[.][0-2]{1,1}[0-9]$"))
                {
                    MessageBox.Show("Введите корректно поле: \"Низкий, Вт\"\nПример: от 2.0 Вт. до 2.2 Вт.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_LowPowerLevelTransmitter.Select();
                    return;
                }
                if (!Regex.IsMatch(txB_HighPowerLevelTransmitter.Text, @"^[2-5]{1,1}[.][0-9]{1,2}$"))
                {
                    MessageBox.Show("Введите корректно поле: \"Высокий, Вт\"Пример: от 2.0 Вт. до 5.9 Вт.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_LowPowerLevelTransmitter.Select();
                    return;
                }
                if (!Regex.IsMatch(txB_FrequencyDeviationTransmitter.Text, @"^[+?-][0-9]{1,3}$"))
                {
                    MessageBox.Show("Введите корректно поле: \"Отклоние, Гц\"\nПример: от -350 Гц. до +350 Гц.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_FrequencyDeviationTransmitter.Select();
                    return;
                }
                else
                {
                    Regex re = new Regex(@"^[+?-]([0-9]{1,3})$");
                    Match result = re.Match(txB_FrequencyDeviationTransmitter.Text);

                    var intFrequency = Convert.ToInt32(result.Groups[1].Value);

                    if (intFrequency > 350 || intFrequency < -350)
                    {
                        MessageBox.Show("Введите парметры отклонения частоты корректно\nПример: от -350 Гц. до 350 Гц.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_FrequencyDeviationTransmitter.Select();
                        return;
                    }
                }
                if (!Regex.IsMatch(txB_SensitivityTransmitter.Text, @"^[0-9]{1,2}[.][0-9]{1,1}$"))
                {
                    MessageBox.Show("Введите корректно поле: \"Чувствительность, мВ\"\nПример для Motorola серии GP: от 9.0 мВ. до 10.0 мВ.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_SensitivityTransmitter.Select();
                    return;
                }
                else
                {
                    if (txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-320" || txB_model.Text == "Motorola GP-360")
                    {

                        Regex re = new Regex(@"^([0-9]{1,2}[.][0-9]{1,1}$)");
                        Match result = re.Match(txB_SensitivityTransmitter.Text);

                        var doubleSensitivityTransmitter = Convert.ToDouble(result.Groups[1].Value);
                        if (doubleSensitivityTransmitter > 10.1 || doubleSensitivityTransmitter < 8.9)
                        {
                            MessageBox.Show($"Введите корректно параметры чувствительности модуляционного входа передатчика, модели {txB_model.Text}\nПример: от 9.0 мВ. до 10.0 мВ.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_SensitivityTransmitter.Select();
                            return;
                        }

                    }
                    else if (txB_model.Text == "Icom IC-F3GS" || txB_model.Text == "Icom IC-F3GT" || txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F11" ||
                    txB_model.Text == "Альтавия-301М" || txB_model.Text == "Элодия-351М")
                    {

                        Regex re = new Regex(@"^([0-9]{2,2}[.][0-9]{1,1})$");
                        Match result = re.Match(txB_SensitivityTransmitter.Text);

                        var doubleSensitivityTransmitter = Convert.ToDouble(result.Groups[1].Value);
                        if (doubleSensitivityTransmitter > 18.1 || doubleSensitivityTransmitter < 14.9)
                        {
                            MessageBox.Show($"Введите корректно параметры чувствительности модуляционного входа передатчика, модели {txB_model.Text}\nПример: от 15.0 мВ. до 18.0 мВ.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_SensitivityTransmitter.Select();
                            return;
                        }

                    }
                    else if (txB_model.Text == "Comrade R5")
                    {

                        Regex re = new Regex(@"^([0-9]{1,1}[.][0-9]{1,1})$");
                        Match result = re.Match(txB_SensitivityTransmitter.Text);

                        var doubleSensitivityTransmitter = Convert.ToDouble(result.Groups[1].Value);
                        if (doubleSensitivityTransmitter > 8.1 || doubleSensitivityTransmitter < 6.9)
                        {
                            MessageBox.Show($"Введите корректно параметры чувствительности модуляционного входа передатчика, модели {txB_model.Text}\nПример: от 7.0 мВ. до 8.0 мВ.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_SensitivityTransmitter.Select();
                            return;
                        }

                    }
                    else if (txB_model.Text == "Motorola DP-2400е" || txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-4400" || txB_model.Text == "Motorola DP-1400")
                    {

                        Regex re = new Regex(@"^([0-9]{1,2}[.][0-9]{1,1})$");
                        Match result = re.Match(txB_SensitivityTransmitter.Text);

                        var doubleSensitivityTransmitter = Convert.ToDouble(result.Groups[1].Value);
                        if (doubleSensitivityTransmitter > 10.1 || doubleSensitivityTransmitter < 4.4)
                        {
                            MessageBox.Show($"Введите корректно параметры чувствительности модуляционного входа передатчика, модели {txB_model.Text}\nПример: от 6.0 мВ. до 10.0 мВ.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_SensitivityTransmitter.Select();
                            return;
                        }
                    }
                    else
                    {
                        Regex re = new Regex(@"^([0-9]{1,1}[.][0-9]{1,1})$");
                        Match result = re.Match(txB_SensitivityTransmitter.Text);

                        var doubleSensitivityTransmitter = Convert.ToDouble(result.Groups[1].Value);
                        if (doubleSensitivityTransmitter > 18.1 || doubleSensitivityTransmitter < 5.9)
                        {
                            MessageBox.Show($"Введите корректно параметры чувствительности модуляционного входа передатчика, модели {txB_model.Text}\nПример: от 6.0 мВ. до 18.0 мВ.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_SensitivityTransmitter.Select();
                            return;
                        }
                    }
                }
                if (!Regex.IsMatch(txB_KNITransmitter.Text, @"^[0-4]{1,1}[.][0-9]{1,2}$"))
                {
                    MessageBox.Show("Введите корректно поле: \"КНИ, %\"\nПример: от 0.30 % до 4.99 %", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_KNITransmitter.Select();
                    return;
                }
                else
                {
                    Regex re = new Regex(@"^([0-4]{1,1}[.][0-9]{1,2}$)");
                    Match result = re.Match(txB_KNITransmitter.Text);

                    var doubleKNITransmitter = Convert.ToDouble(result.Groups[1].Value);

                    if (doubleKNITransmitter > 5.00 || doubleKNITransmitter < 0.30)
                    {
                        MessageBox.Show("Введите параметры КНИ передатчика корректно\nПример: от 0.30 % до 4.99 %", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_KNITransmitter.Select();
                        return;
                    }
                }
                if (!Regex.IsMatch(txB_DeviationTransmitter.Text, @"^[4]{1,1}[.][0-9]{1,2}$"))
                {
                    MessageBox.Show("Введите корректно поле: \"Девиация, кГЦ\"\nПример: от 4.00 кГЦ. до 5.00 кГЦ.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_DeviationTransmitter.Select();
                    return;
                }
                else
                {
                    Regex re = new Regex(@"^([4]{1,1}[.][0-9]{1,2}$)");
                    Match result = re.Match(txB_DeviationTransmitter.Text);

                    var doubleDeviationTransmitter = Convert.ToDouble(result.Groups[1].Value);

                    if (doubleDeviationTransmitter > 5.01 || doubleDeviationTransmitter < 3.99)
                    {
                        MessageBox.Show("Введите параметры Девиации передатчика корректно\nПример: от 4.00 кГЦ. до 5.00 кГЦ.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_DeviationTransmitter.Select();
                        return;
                    }
                }
                #endregion

                #region приёмник

                if (!Regex.IsMatch(txB_OutputPowerVoltReceiver.Text, @"^[0-9]{1,1}[.][0-9]{1,2}$"))
                {
                    MessageBox.Show("Введите корректно поле: \"Вых. мощность, В\"\nПример для Motorola серии GP: от 4.00 В. до 5.50 В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_OutputPowerVoltReceiver.Select();
                    return;
                }
                else
                {
                    if (txB_model.Text == "Motorola GP-340" || txB_model.Text == "Motorola GP-320" || txB_model.Text == "Motorola GP-360" ||
                        txB_model.Text == "Motorola DP-2400е" || txB_model.Text == "Motorola DP-2400" || txB_model.Text == "Motorola DP-4400" ||
                        txB_model.Text == "Motorola DP-1400" || txB_model.Text == "Комбат T-44" || txB_model.Text == "Комбат T-54")
                    {

                        Regex re = new Regex(@"^([0-9]{1,1}[.][0-9]{1,2})$");
                        Match result = re.Match(txB_OutputPowerVoltReceiver.Text);

                        var doubleOutputPowerVoltReceiver = Convert.ToDouble(result.Groups[1].Value);
                        if (doubleOutputPowerVoltReceiver > 5.51 || doubleOutputPowerVoltReceiver < 3.99)
                        {
                            MessageBox.Show($"Введите корректно параметры выходной мощности приёмника, модели {txB_model.Text}\nПример: от 4.00 В. до 5.50 В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_OutputPowerVoltReceiver.Select();
                            return;
                        }
                    }
                    else if (txB_model.Text == "Icom IC-F3GS" || txB_model.Text == "Icom IC-F3GT" || txB_model.Text == "Icom IC-F16" || txB_model.Text == "Icom IC-F11" ||
                        txB_model.Text == "Альтавия-301М" || txB_model.Text == "Элодия-351М" || txB_model.Text == "Comrade R5")
                    {

                        Regex re = new Regex(@"^([0-9]{1,1}[.][0-9]{1,2})$");
                        Match result = re.Match(txB_OutputPowerVoltReceiver.Text);

                        var doubleOutputPowerVoltReceiver = Convert.ToDouble(result.Groups[1].Value);
                        if (doubleOutputPowerVoltReceiver > 3.01 || doubleOutputPowerVoltReceiver < 2.19)
                        {
                            MessageBox.Show($"Введите корректно параметры выходной мощности приёмника, модели {txB_model.Text}\nПример: от 2.60 В. до 3.50 В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_OutputPowerVoltReceiver.Select();
                            return;
                        }
                    }
                    else
                    {
                        Regex re = new Regex(@"^([0-9]{1,1}[.][0-9]{1,2})$");
                        Match result = re.Match(txB_OutputPowerVoltReceiver.Text);

                        var doubleOutputPowerVoltReceiver = Convert.ToDouble(result.Groups[1].Value);
                        if (doubleOutputPowerVoltReceiver > 5.51 || doubleOutputPowerVoltReceiver < 2.59)
                        {
                            MessageBox.Show($"Введите корректно параметры выходной мощности приёмника В., модели {txB_model.Text}\nПример: от 2.60 В. до 5.50 В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_OutputPowerVoltReceiver.Select();
                            return;
                        }
                    }
                }

                if (txB_model.Text == "Comrade R5")
                {
                    try
                    {
                        Regex re = new Regex(@"[>][=]([0][.][4])$");
                        Match result = re.Match(txB_OutputPowerWattReceiver.Text);

                        var doubleOutputPowerWattReceiver = Convert.ToDouble(result.Groups[1].Value);
                        if (doubleOutputPowerWattReceiver != 0.4)
                        {
                            MessageBox.Show($"Введите корректно параметры выходной мощности приёмника Вт., модели {txB_model.Text}\nПример: >=0.4", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_OutputPowerWattReceiver.Select();
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Введите корректно поле: \"Вых. мощность, Вт.\"\nПример: от >=0.4 Вт.(для {txB_model.Text})", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_OutputPowerWattReceiver.Select();
                        return;
                    }
                }
                else
                {
                    if (!Regex.IsMatch(txB_OutputPowerWattReceiver.Text, @"^[>][0][.][5]{1,1}$"))
                    {
                        MessageBox.Show($"Введите корректно поле: \"Вых. мощность, Вт.\"\nПример: >0.5 Вт. ({txB_model.Text})", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_OutputPowerWattReceiver.Select();
                        return;
                    }
                    else
                    {
                        Regex re = new Regex(@"[>]([0][.][5])$");
                        Match result = re.Match(txB_OutputPowerWattReceiver.Text);

                        var doubleOutputPowerWattReceiver = Convert.ToDouble(result.Groups[1].Value);
                        if (doubleOutputPowerWattReceiver != 0.5)
                        {
                            MessageBox.Show($"Введите корректно параметры выходной мощности приёмника Вт., модели {txB_model.Text}\nПример: >0.5", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_OutputPowerWattReceiver.Select();
                            return;
                        }
                    }
                }


                if (txB_model.Text == "Motorola GP-340" || txB_model.Text == "Icom IC-F3GS" || txB_model.Text == "Icom IC-F3GT" || txB_model.Text == "Icom IC-F16" ||
                     txB_model.Text == "Icom IC-F11" || txB_model.Text == "Motorola GP-360" || txB_model.Text == "Motorola GP-360" || txB_model.Text == "Motorola GP-320" ||
                     txB_model.Text == "Motorola P080" || txB_model.Text == "Motorola P040" || txB_model.Text == "Гранит Р33П-1" || txB_model.Text == "Гранит Р-43" ||
                     txB_model.Text == "Радий-301")
                {
                    try
                    {
                        Regex re = new Regex(@"^([7][1])$");
                        Match result = re.Match(txB_SelectivityReceiver.Text);

                        var intOutpuSelectivityReceiver = Convert.ToInt32(result.Groups[1].Value);
                        if (intOutpuSelectivityReceiver != 71)
                        {
                            MessageBox.Show($"Введите корректно параметры избирательности приёмника дБ., модели {txB_model.Text}\nПример: 71", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_SelectivityReceiver.Select();
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Введите корректно поле: \"Избирательн., дБ.\"\nПример: 71 для {txB_model.Text}", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_SelectivityReceiver.Select();
                        return;
                    }
                }
                else if (txB_model.Text == "Альтавия-301М" || txB_model.Text == "Элодия-351М")
                {
                    try
                    {
                        Regex re = new Regex(@"^([7][6])$");
                        Match result = re.Match(txB_SelectivityReceiver.Text);

                        var intOutpuSelectivityReceiver = Convert.ToInt32(result.Groups[1].Value);
                        if (intOutpuSelectivityReceiver != 76)
                        {
                            MessageBox.Show($"Введите корректно параметры избирательности приёмника дБ., модели {txB_model.Text}\nПример: 76", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_SelectivityReceiver.Select();
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Введите корректно поле: \"Избирательн., дБ.\"\nПример: 76 для {txB_model.Text}", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_SelectivityReceiver.Select();
                        return;
                    }
                }
                else
                {
                    if (!Regex.IsMatch(txB_SelectivityReceiver.Text, @"^[-]$"))
                    {
                        MessageBox.Show("Введите корректно поле: \"Избирательн., дБ.\"\nДля цифровых радиостанций\nПример: \"-\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_SelectivityReceiver.Select();
                        return;
                    }
                }

                if (!Regex.IsMatch(txB_KNIReceiver.Text, @"^[0-4]{1,1}[.][0-9]{1,2}$"))
                {
                    MessageBox.Show("Введите корректно поле: \"КНИ, %\"\nПример: от 0.30 % до 4.99 %", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_KNIReceiver.Select();
                    return;
                }
                else
                {
                    Regex re = new Regex(@"^([0-4]{1,1}[.][0-9]{1,2}$)");
                    Match result = re.Match(txB_KNIReceiver.Text);

                    var doubleKNIReceiver = Convert.ToDouble(result.Groups[1].Value);

                    if (doubleKNIReceiver > 5.00 || doubleKNIReceiver < 0.30)
                    {
                        MessageBox.Show("Введите параметры КНИ приёмника корректно\nПример: от 0.30 % до 4.99 %", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_KNIReceiver.Select();
                        return;
                    }
                }

                if (!Regex.IsMatch(txB_SensitivityReceiver.Text, @"^[0][.][1-2]{1,1}[0-9]{1,1}$"))
                {
                    MessageBox.Show("Введите корректно поле: \"Чувствительн., мкВ.\"\nПример: от 0.11 мкВ. до 0.27 мкВ.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_SensitivityReceiver.Select();
                    return;
                }
                else
                {
                    Regex re = new Regex(@"^([0][.][1-2]{1,1}[0-9]{1,1})$");
                    Match result = re.Match(txB_SensitivityReceiver.Text);

                    var doubleSensitivityReceiver = Convert.ToDouble(result.Groups[1].Value);

                    if (doubleSensitivityReceiver > 0.28 || doubleSensitivityReceiver < 0.11)
                    {
                        MessageBox.Show("Введите параметры чувствительности приёмника корректно\nПример: от 0.11 мкВ. до 0.27 мкВ.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_SensitivityReceiver.Select();
                        return;
                    }
                }

                if (!Regex.IsMatch(txB_SuppressorReceiver.Text, @"^[0][.][1-2]{1,1}[0-9]{1,1}$"))
                {
                    MessageBox.Show("Введите корректно поле: \"ШУМ, мкВ.\"\nПример: от 0.11 мкВ. до 0.22 мкВ.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_SuppressorReceiver.Select();
                    return;
                }
                else
                {
                    Regex re = new Regex(@"^([0][.][1-2]{1,1}[0-9]{1,1})$");
                    Match result = re.Match(txB_SuppressorReceiver.Text);

                    var doubleSuppressorReceiver = Convert.ToDouble(result.Groups[1].Value);

                    if (doubleSuppressorReceiver > 0.22 || doubleSuppressorReceiver < 0.11)
                    {
                        MessageBox.Show("Поправь параметры шумоподавителя приёмника в программе Tuner\nПример: от 0.11 мкВ. до 0.22 мкВ.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_SuppressorReceiver.Select();
                        return;
                    }
                }


                #endregion

                #region Потребляемый ток

                if (!Regex.IsMatch(txB_BatteryDischargeAlarmCurrentConsumption.Text, @"^[6][.][0]$"))
                {
                    MessageBox.Show("Введите корректно поле: \"Сигнализация разряда АКБ, В.\"\nПример: 6.0 В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_BatteryDischargeAlarmCurrentConsumption.Select();
                    return;
                }

                if (!Regex.IsMatch(txB_TransmissionModeCurrentConsumption.Text, @"^[1][.][1-9]{1,1}[0-9]{1,1}$"))
                {
                    MessageBox.Show("Введите корректно поле: \"Режим передачи (высокая мощность), А.\"\nПример: 1.64 A.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_TransmissionModeCurrentConsumption.Select();
                    return;
                }

                if (txB_model.Text == "РН311М")
                {
                    if (!Regex.IsMatch(txB_StandbyModeCurrentConsumption.Text, @"^[1][1-5]{1,1}[0]$"))
                    {
                        MessageBox.Show($"Введите корректно поле: \"Дежурный режим, мА.\" для {txB_model.Text}\nПример: 110 мA.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_StandbyModeCurrentConsumption.Select();
                        return;
                    }
                }
                else
                {
                    if (!Regex.IsMatch(txB_StandbyModeCurrentConsumption.Text, @"^[4-8]{1,1}[0]$"))
                    {
                        MessageBox.Show($"Введите корректно поле: \"Дежурный режим, мА.\" для {txB_model.Text}\nПример: от 40 мA. до 80 мA.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_StandbyModeCurrentConsumption.Select();
                        return;
                    }
                }

                #endregion

                #region AKB

                if (txB_percentAKB.Enabled)
                {
                    if (!chB_Faulty.Checked)
                    {
                        if (!Regex.IsMatch(txB_percentAKB.Text, @"^[5-9]{1,1}[0-9]{1,1}$"))
                        {
                            MessageBox.Show($"Введите корректно поле: \"АКБ, %\" для {lbL_nameAKB.Text}\nПример: 75", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_percentAKB.Select();
                            return;
                        }
                    }
                    else
                    {
                        if (!Regex.IsMatch(txB_percentAKB.Text, @"^[н][е][и][с][п][р][.]$"))
                        {
                            MessageBox.Show($"Введите корректно поле: \"АКБ, %\" для {lbL_nameAKB.Text}\nПример: 75", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txB_percentAKB.Select();
                            return;
                        }
                    }
                }




                #endregion

                string road = lbL_road.Text;
                string city = lbL_city.Text;
                string numberAct = txB_numberAct.Text;
                string serialNumber = txB_serialNumber.Text;
                string dateTO = Convert.ToDateTime(txB_dateTO.Text).ToString("yyyy-MM-dd");
                string model = txB_model.Text;
                string lowPowerLevelTransmitter = txB_LowPowerLevelTransmitter.Text;
                string highPowerLevelTransmitter = txB_HighPowerLevelTransmitter.Text;
                string frequencyDeviationTransmitter = txB_FrequencyDeviationTransmitter.Text;
                string sensitivityTransmitter = txB_SensitivityTransmitter.Text;
                string kniTransmitter = txB_KNITransmitter.Text;
                string deviationTransmitter = txB_DeviationTransmitter.Text;
                string outputPowerVoltReceiver = txB_OutputPowerVoltReceiver.Text;
                string outputPowerWattReceiver = txB_OutputPowerWattReceiver.Text;
                string selectivityReceiver = txB_SelectivityReceiver.Text;
                string sensitivityReceiver = txB_SensitivityReceiver.Text;
                string kniReceiver = txB_KNIReceiver.Text;
                string suppressorReceiver = txB_SuppressorReceiver.Text;
                string standbyModeCurrentConsumption = txB_StandbyModeCurrentConsumption.Text;
                string receptionModeCurrentConsumption = txB_ReceptionModeCurrentConsumption.Text;
                string transmissionModeCurrentConsumption = txB_TransmissionModeCurrentConsumption.Text;
                string batteryDischargeAlarmCurrentConsumption = txB_BatteryDischargeAlarmCurrentConsumption.Text;

                string transmitterFrequencies = txB_TransmitterFrequencies.Text;
                //var regex = new Regex(Environment.NewLine);
                //transmitterFrequencies = regex.Replace(transmitterFrequencies, " ");
                transmitterFrequencies.Trim();


                string receiverFrequencies = txB_ReceiverFrequencies.Text;
                //var regex2 = new Regex(Environment.NewLine);
                //receiverFrequencies = regex2.Replace(receiverFrequencies, " ");
                receiverFrequencies.Trim();

                string batteryChargerAccessories = String.Empty;
                if (cmB_BatteryChargerAccessories.Enabled)
                    batteryChargerAccessories = cmB_BatteryChargerAccessories.Text;
                else batteryChargerAccessories = "-";

                string manipulatorAccessories = String.Empty;
                if (cmB_ManipulatorAccessories.Enabled)
                    manipulatorAccessories = cmB_ManipulatorAccessories.Text;
                else manipulatorAccessories = "-";

                string nameAKB = String.Empty;
                if (lbL_nameAKB.Visible)
                    nameAKB = lbL_nameAKB.Text;
                else nameAKB = "-";

                string percentAKB = String.Empty;

                if (txB_percentAKB.Enabled)
                    percentAKB = txB_percentAKB.Text;
                else if (!String.IsNullOrEmpty(txB_percentAKB.Text))
                    percentAKB = txB_percentAKB.Text;
                else percentAKB = "-";

                string noteRadioStationParameters = txB_NoteRadioStationParameters.Text;
                var regex3 = new Regex(Environment.NewLine);
                noteRadioStationParameters = regex3.Replace(noteRadioStationParameters, " ");

                string inRepairBool = String.Empty;

                if (chB_InRepair.Checked)
                {
                    chB_InRepair.Checked = true;
                    lbl_verifiedRST.Text = "В ремонте";
                    lbl_verifiedRST.ForeColor = Color.Red;
                    inRepairBool = "?";
                }
                else
                {
                    chB_InRepair.Checked = false;
                    lbl_verifiedRST.Text = "Проверена";
                    lbl_verifiedRST.ForeColor = Color.ForestGreen;
                    inRepairBool = "+";
                }

                lbl_verifiedRST.Visible = true;
                if (CheacSerialNumber.GetInstance.CheackSerialNumberRadiostationParameters(lbL_road.Text, lbL_city.Text, txB_serialNumber.Text))
                {
                    string changeQueryParameters = $"UPDATE radiostation_parameters SET numberAct = '{numberAct}', dateTO = '{dateTO}', model = '{model}', " +
                        $"lowPowerLevelTransmitter = '{lowPowerLevelTransmitter}', " +
                        $"highPowerLevelTransmitter = '{highPowerLevelTransmitter}', frequencyDeviationTransmitter = '{frequencyDeviationTransmitter}', " +
                        $"sensitivityTransmitter = '{sensitivityTransmitter}', kniTransmitter = '{kniTransmitter}', deviationTransmitter = '{deviationTransmitter}', " +
                        $"outputPowerVoltReceiver = '{outputPowerVoltReceiver}', outputPowerWattReceiver = '{outputPowerWattReceiver}', " +
                        $"selectivityReceiver = '{selectivityReceiver}', sensitivityReceiver = '{sensitivityReceiver}', kniReceiver = '{kniReceiver}', " +
                        $"suppressorReceiver = '{suppressorReceiver}', standbyModeCurrentConsumption ='{standbyModeCurrentConsumption}', " +
                        $"receptionModeCurrentConsumption = '{receptionModeCurrentConsumption}', transmissionModeCurrentConsumption = '{transmissionModeCurrentConsumption}', " +
                        $"batteryDischargeAlarmCurrentConsumption = '{batteryDischargeAlarmCurrentConsumption}', transmitterFrequencies = '{transmitterFrequencies}', " +
                        $"receiverFrequencies = '{receiverFrequencies}', batteryChargerAccessories = '{batteryChargerAccessories}', manipulatorAccessories = '{manipulatorAccessories}', " +
                        $"nameAKB = '{nameAKB}', percentAKB = '{percentAKB}', noteRadioStationParameters = '{noteRadioStationParameters}', " +
                        $"verifiedRST = '{inRepairBool}' WHERE road = '{road}' AND city = '{city}' AND serialNumber = '{serialNumber}'";

                    using (MySqlCommand command = new MySqlCommand(changeQueryParameters, DB_3.GetInstance.GetConnection()))
                    {
                        DB_3.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        DB_3.GetInstance.CloseConnection();
                    }
                }
                else
                {
                    string addQueryParameters = $"INSERT INTO radiostation_parameters (road, city, numberAct, serialNumber, dateTO," +
                               $"model, lowPowerLevelTransmitter, highPowerLevelTransmitter, frequencyDeviationTransmitter, " +
                               $"sensitivityTransmitter, kniTransmitter, deviationTransmitter, outputPowerVoltReceiver, outputPowerWattReceiver, " +
                               $"selectivityReceiver, sensitivityReceiver, kniReceiver, suppressorReceiver, standbyModeCurrentConsumption, " +
                               $"receptionModeCurrentConsumption, transmissionModeCurrentConsumption, batteryDischargeAlarmCurrentConsumption, " +
                               $"transmitterFrequencies, receiverFrequencies, batteryChargerAccessories, manipulatorAccessories, " +
                               $"nameAKB, percentAKB, noteRadioStationParameters, verifiedRST) VALUES ('{road}', '{city}', '{numberAct}'," +
                               $"'{serialNumber}','{dateTO}', '{model}', '{lowPowerLevelTransmitter}', " +
                               $"'{highPowerLevelTransmitter}','{frequencyDeviationTransmitter}','{sensitivityTransmitter}', " +
                               $"'{kniTransmitter}', '{deviationTransmitter}', '{outputPowerVoltReceiver}', " +
                               $"'{outputPowerWattReceiver}', '{selectivityReceiver}', '{sensitivityReceiver}', '{kniReceiver}', " +
                               $"'{suppressorReceiver}', '{standbyModeCurrentConsumption}', '{receptionModeCurrentConsumption}', " +
                               $"'{transmissionModeCurrentConsumption}', '{batteryDischargeAlarmCurrentConsumption}', " +
                               $"'{transmitterFrequencies}', '{receiverFrequencies}', '{batteryChargerAccessories}', " +
                               $"'{manipulatorAccessories}', '{nameAKB}', '{percentAKB}', '{noteRadioStationParameters}', '{inRepairBool}')";

                    using (MySqlCommand command = new MySqlCommand(addQueryParameters, DB_3.GetInstance.GetConnection()))
                    {
                        DB_3.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        DB_3.GetInstance.CloseConnection();
                    }
                }

                string changeQueryRadiostantion = $"UPDATE radiostantion SET verifiedRST = '{inRepairBool}' " +
                   $"WHERE road = '{road}' AND city = '{city}' AND serialNumber = '{serialNumber}'";

                using (MySqlCommand command = new MySqlCommand(changeQueryRadiostantion, DB_3.GetInstance.GetConnection()))
                {
                    DB_3.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB_3.GetInstance.CloseConnection();
                }

            }
            MessageBox.Show("Готовченко");
        }



        #endregion

        void Btn_DecommissionRadiostantion_Click(object sender, EventArgs e)
        {
            string road = lbL_road.Text;
            string city = lbL_city.Text;
            string serialNumber = txB_serialNumber.Text;

            string changeQueryRadiostantion = $"UPDATE radiostantion SET verifiedRST = '0' " +
                   $"WHERE road = '{road}' AND city = '{city}' AND serialNumber = '{serialNumber}'";

            using (MySqlCommand command = new MySqlCommand(changeQueryRadiostantion, DB_3.GetInstance.GetConnection()))
            {
                DB_3.GetInstance.OpenConnection();
                command.ExecuteNonQuery();
                DB_3.GetInstance.CloseConnection();
            }

            string deleteQueryParameters = $"DELETE FROM radiostation_parameters WHERE serialNumber = '{serialNumber}'";

            using (MySqlCommand command = new MySqlCommand(deleteQueryParameters, DB_3.GetInstance.GetConnection()))
            {
                DB_3.GetInstance.OpenConnection();
                command.ExecuteNonQuery();
                DB_3.GetInstance.CloseConnection();
            }
            MessageBox.Show("Радиостанция списана (удалена)!");
        }
    }
}
