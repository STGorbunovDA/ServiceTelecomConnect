using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceTelecomConnect.Forms
{
    public partial class AddRadioStationParametersForm : Form
    {
        public AddRadioStationParametersForm()
        {
            InitializeComponent();
        }

        void AddRadioStationParametersForm_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            monthCalendar1.Visible = false;
            var myCulture = new CultureInfo("ru-RU");
            myCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = myCulture;
            txB_dateTO.ReadOnly = true;
            txB_dateTO.Text = DateTime.Now.ToString("dd.MM.yyyy");
            if (String.IsNullOrEmpty(lbL_AKB.Text) || lbL_AKB.Text == "-")
                txB_AKB.Enabled = false;
            else txB_AKB.Size = lbL_AKB.Size;
            if (String.IsNullOrEmpty(lbL_BatteryChargerAccessories.Text) || lbL_BatteryChargerAccessories.Text == "-")
                cmB_BatteryChargerAccessories.Enabled = false;
            if (String.IsNullOrEmpty(lbL_ManipulatorAccessories.Text) || lbL_ManipulatorAccessories.Text == "-")
                cmB_ManipulatorAccessories.Enabled = false;

            QuerySettingDataBase.GettingFrequenciesRST_CMB(cmB_frequency);
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
            if ((ch <= 47 || ch >= 58) && ch != (char)Keys.Enter && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TxB_ReceiverFrequencies_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != (char)Keys.Enter && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void CmB_frequency_MouseLeave(object sender, EventArgs e)
        {
            cmB_frequency.Visible = false;
        }

        void TxB_TransmitterFrequencies_Click(object sender, EventArgs e)
        {
            cmB_frequency.Visible = true;
        }

        void CmB_frequency_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txB_TransmitterFrequencies.Text += cmB_frequency.Text + Environment.NewLine;

            txB_ReceiverFrequencies.Text += cmB_frequency.Text + Environment.NewLine;
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
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        void TxB_SelectivityReceiver_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
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

        #region АКБ KeyPress

        void TxB_AKB_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != '\b' && ch != '.')
                e.Handled = true;
        }

        #endregion

        #region Добавляем параметры в БД
        void Btn_save_add_rst_remont_Click(object sender, EventArgs e)
        {
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
                    var re = new Regex(Environment.NewLine);
                    control.Text = re.Replace(control.Text, " ");
                    control.Text.Trim();
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
                    var re = new Regex(Environment.NewLine);
                    control.Text = re.Replace(control.Text, " ");
                    control.Text.Trim();
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
                    var re = new Regex(Environment.NewLine);
                    control.Text = re.Replace(control.Text, " ");
                    control.Text.Trim();
                }
            }
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
            if (cmB_BatteryChargerAccessories.Enabled || cmB_ManipulatorAccessories.Enabled)
            {
                foreach (Control control in pnl_Accessories.Controls)
                {
                    if (control is ComboBox)
                    {
                        if (String.IsNullOrEmpty(control.Text))
                        {
                            MessageBox.Show("Заполните параметры \"Аксессуары\"");
                            control.Select();
                            return;
                        }
                    }
                }
            }
            if (txB_AKB.Enabled)
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
                        var re = new Regex(Environment.NewLine);
                        control.Text = re.Replace(control.Text, " ");
                        control.Text.Trim();
                    }
                }
            }

            if (!String.IsNullOrEmpty(txB_NoteRadioStationParameters.Text))
            {
                foreach (Control control in pnl_NoteRadioStationParameters.Controls)
                {
                    if (control is TextBox)
                    {
                        var re = new Regex(Environment.NewLine);
                        control.Text = re.Replace(control.Text, " ");
                        control.Text.Trim();
                    }
                }
            }

            if (!Regex.IsMatch(txB_LowPowerLevelTransmitter.Text, @"^[2-2]{1,1}[.][0-9]{1,2}$"))
            {
                MessageBox.Show("Введите корректно поле: \"Низкий, Вт\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_LowPowerLevelTransmitter.Select();
                return;
            }
            if (!Regex.IsMatch(txB_HighPowerLevelTransmitter.Text, @"^[2-5]{1,1}[.][0-9]{1,2}$"))
            {
                MessageBox.Show("Введите корректно поле: \"Высокий, Вт\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_LowPowerLevelTransmitter.Select();
                return;
            }
            if (!Regex.IsMatch(txB_FrequencyDeviationTransmitter.Text, @"^[+?-][0-9]{1,3}$"))
            {
                MessageBox.Show("Введите корректно поле: \"Отклоние, Гц\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_FrequencyDeviationTransmitter.Select();
                return;
            }
            else
            {
                Regex re = new Regex(@"^[+?-][0-9]{1,3}$");
                Match result = re.Match(txB_FrequencyDeviationTransmitter.Text);

                var x1 = Convert.ToInt32(result.Groups[1].Value);

            }

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
                    QuerySettingDataBase.GettingFrequenciesRST_CMB(cmB_frequency);
                }
            }
        }

        #endregion










        //void TxB_AKB_TextChanged(object sender, EventArgs e)
        //{
        //    if (!Regex.IsMatch(txB_AKB.Text, "^[0-9]{2,2}$"))
        //    {
        //        txB_AKB.Text = txB_AKB.Text.Remove(txB_AKB.Text.Length - 1);
        //    }
        //}
    }
}
