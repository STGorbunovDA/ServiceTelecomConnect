using System;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceTelecomConnect.Forms
{
    public partial class TutorialForm : Form
    {
        private readonly cheakUser _user;

        int selectedRow;

        public TutorialForm(cheakUser user)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            dataGridView1.DoubleBuffered(true);
            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            _user = user;
            cmB_seach.Text = cmB_seach.Items[0].ToString();
        }

        void TutorialForm_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12f, FontStyle.Bold); //жирный курсив размера 16
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; //цвет текста
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; //цвет ячейки

            QuerySettingDataBase.CreateColumsEngineer(dataGridView1);
            QuerySettingDataBase.RefreshDataGridEngineer(dataGridView1);

            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
        }


        void PicB_update_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавь неисправность", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            QuerySettingDataBase.RefreshDataGridEngineer(dataGridView1);
        }

        void Btn_new_rst_problem_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    AddToProblemRST addProblemRST = new AddToProblemRST(_user);
                    if (Application.OpenForms["AddToProblemRST"] == null)
                    {
                        addProblemRST.DoubleBufferedForm(true);
                        addProblemRST.Show();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка создания формы AddRSTForm(Btn_new_rst_problem_Click)");
                }
            }    
        }

        void Cmb_seach_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmB_seach.SelectedIndex == 0)
            {
                try
                {
                    cmb_unique.Visible = true;
                    txB_search.Visible = false;
                    txB_search.Clear();
                    QuerySettingDataBase.Cmb_unique_model_engineer(cmb_unique);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка! Модели не добавлены в comboBox!");
                    MessageBox.Show(ex.ToString());
                }
            }
            if (cmB_seach.SelectedIndex == 1)
            {
                try
                {
                    cmb_unique.Visible = true;
                    txB_search.Visible = false;
                    txB_search.Clear();
                    QuerySettingDataBase.Cmb_unique_problem_engineer(cmb_unique);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка! неисправности не добавлены в comboBox!");
                    MessageBox.Show(ex.ToString());
                }
            }
            if (cmB_seach.SelectedIndex == 2)
            {
                try
                {
                    cmb_unique.Visible = true;
                    txB_search.Visible = false;
                    txB_search.Clear();
                    QuerySettingDataBase.Cmb_unique_author_engineer(cmb_unique);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка! Авторы не добавлены в comboBox!");
                    MessageBox.Show(ex.ToString());
                }
            }
            if (cmB_seach.SelectedIndex == 3)
            {
                try
                {
                    txB_search.Visible = true;
                    cmb_unique.Visible = false;
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка! Авторы не добавлены в comboBox!");
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        void Cmb_unique_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuerySettingDataBase.SearchEngineer(dataGridView1, cmB_seach.Text, txB_search.Text, cmb_unique.Text);
        }

        void Btn_search_Click(object sender, EventArgs e)
        {
            QuerySettingDataBase.SearchEngineer(dataGridView1, cmB_seach.Text, txB_search.Text, cmb_unique.Text);
        }

        void TxB_search_DoubleClick(object sender, EventArgs e)
        {
            QuerySettingDataBase.SearchEngineer(dataGridView1, cmB_seach.Text, txB_search.Text, cmb_unique.Text);
        }

        private void TxB_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                QuerySettingDataBase.SearchEngineer(dataGridView1, cmB_seach.Text, txB_search.Text, cmb_unique.Text);
            }
        }
    }
}
