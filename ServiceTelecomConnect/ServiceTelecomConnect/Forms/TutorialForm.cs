using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceTelecomConnect.Forms
{
    public partial class TutorialForm : Form
    {
        private readonly cheakUser _user;

        DB dB = new DB();

        int selectedRow;

        public TutorialForm(cheakUser user)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            dataGridView1.DoubleBuffered(true);
            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            _user = user;
        }

        void TutorialForm_Load(object sender, EventArgs e)
        {
            QuerySettingDataBase.modelGetEngineer(cmB_model);
        }
    }
}
