using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
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
    }
}
