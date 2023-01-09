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
            else
            {
                txB_AKB.Size = lbL_AKB.Size;
            }
            if (String.IsNullOrEmpty(lbL_BatteryChargerAccessories.Text) || lbL_BatteryChargerAccessories.Text == "-")
                cmB_BatteryChargerAccessories.Enabled = false;
            if (String.IsNullOrEmpty(lbL_ManipulatorAccessories.Text) || lbL_ManipulatorAccessories.Text == "-")
                cmB_ManipulatorAccessories.Enabled = false;
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

        #region Частоты
        void TxB_TransmitterFrequencies_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != (char)Keys.Enter && ch != '\b' && ch != '.')
            {
                e.Handled = true;
            }
        }

        void TxB_ReceiverFrequencies_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch <= 47 || ch >= 58) && ch != (char)Keys.Enter && ch != '\b' && ch != '.')
            {
                e.Handled = true;
            }
        }

        void CmB_frequency_MouseLeave(object sender, EventArgs e)
        {
            cmB_frequency.Visible = false;
        }

        void TxB_TransmitterFrequencies_Click(object sender, EventArgs e)
        {
            cmB_frequency.Visible = true;
        }

        void CmB_frequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            txB_TransmitterFrequencies.Text += cmB_frequency.Text + Environment.NewLine;

            txB_ReceiverFrequencies.Text += cmB_frequency.Text + Environment.NewLine;
        }

        void TxB_ReceiverFrequencies_Click(object sender, EventArgs e)
        {
            cmB_frequency.Visible = true;
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
                }
            }
            if(cmB_BatteryChargerAccessories.Enabled || cmB_ManipulatorAccessories.Enabled)
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
                    }
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
