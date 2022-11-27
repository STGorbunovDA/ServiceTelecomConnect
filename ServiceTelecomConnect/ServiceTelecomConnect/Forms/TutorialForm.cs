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
            QuerySettingDataBase.ModelGetEngineer(cmB_model);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12f, FontStyle.Bold); //жирный курсив размера 16
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; //цвет текста
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; //цвет ячейки

            QuerySettingDataBase.CreateColumsEngineer(dataGridView1);
            QuerySettingDataBase.RefreshDataGridEngineer(dataGridView1);

            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
        }

        void CmB_model_SelectionChangeCommitted(object sender, EventArgs e)
        {
            QuerySettingDataBase.RefreshDataGridEngineer(dataGridView1);
        }
    }
}
