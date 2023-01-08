﻿using System;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            if(String.IsNullOrEmpty(lbL_AKB.Text) || lbL_AKB.Text == "-")
            {
                lbL_AKB.Visible = false;
                txB_AKB.Visible = false;
            }
            else
            {
                txB_AKB.Size = lbL_AKB.Size;
            }
        }

        void PicB_clear_dataTO_Click(object sender, EventArgs e)
        {
            txB_dateTO.Text = "";
        }

        void TxB_dateTO_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }

        void MonthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            txB_dateTO.Text = e.End.ToString("dd.MM.yyyy");
            monthCalendar1.Visible = false;
        }

        //void TxB_AKB_TextChanged(object sender, EventArgs e)
        //{
        //    if (!Regex.IsMatch(txB_AKB.Text, "^[0-9]{2,2}$"))
        //    {
        //        txB_AKB.Text = txB_AKB.Text.Remove(txB_AKB.Text.Length - 1);
        //    }
        //}
    }
}