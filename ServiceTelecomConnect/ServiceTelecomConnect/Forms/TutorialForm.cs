using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
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
            cmB_seach.Text = cmB_seach.Items[3].ToString();
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

        void Btn_brief_info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Перед началом проверки радиостанции необходимо визуально " +
                "осмотреть корпус на сквозные трещины, сколы корпуса, батарейные контакты, " +
                "уплотнитель батарейного контакта, а также ручку регулятора громкости и ручку " +
                "переключения каналов.Проивести чистку корпуса радиостанции, убрать металлическую " +
                "стружку из динамика. Чистка внешних поверхностей радиостанции включают фронтальную " +
                "крышку радиостанции, корпус радиостанции и корпус батареи. Чистку проводить неметаллической " +
                "короткошерстной щёткой для удаления грязи с радиостанции. Так же Используйте мягкую, " +
                "абсорбирующую ткань, кубки для мытья посуды или влажные салфетки.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.ReadOnly = false;
                selectedRow = e.RowIndex;

                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[selectedRow];
                    txB_id.Text = row.Cells[0].Value.ToString();
                    txB_model.Text = row.Cells[1].Value.ToString();
                    txB_problem.Text = row.Cells[2].Value.ToString();
                    txB_info.Text = row.Cells[3].Value.ToString();
                    txB_actions.Text = row.Cells[4].Value.ToString();
                    txB_author.Text = row.Cells[5].Value.ToString();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка получения данных в Control-ы(DataGridView1_CellClick)");
            }
        }

        void Btn_change_problem_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    if (!String.IsNullOrEmpty(txB_id.Text))
                    {
                        //ChangeToProblemRST changeToProblem = new ChangeToProblemRST(_user);
                        ChangeToProblemRST changeToProblem = new ChangeToProblemRST(_user);
                        if (Application.OpenForms["ChangeToProblemRST"] == null)
                        {
                            changeToProblem.DoubleBufferedForm(true);
                            changeToProblem.txB_id.Text = txB_id.Text;
                            changeToProblem.cmB_model.Items.Add(txB_model.Text).ToString();
                            changeToProblem.txB_problem.Text = txB_problem.Text;
                            changeToProblem.txB_info.Text = txB_info.Text;
                            changeToProblem.txB_actions.Text = txB_actions.Text;
                            changeToProblem.Show();
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка открытия формы изменения неисправности (Btn_change_problem_Click)");
                }
            }
        }

        void Btn_delete_problem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txB_id.Text))
            {
                MessageBox.Show("Выбери строку которую хочешь удалить!");
                return;
            }
            if (dataGridView1.SelectedRows.Count > 1)
            {
                string Mesage;
                Mesage = $"Вы действительно хотите удалить неисправность у модели: {txB_model.Text}?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }

            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows[row.Index].Cells[6].Value = RowState.Deleted;
                    }

                    for (int index = 0; index < dataGridView1.Rows.Count; index++)
                    {
                        var rowState = (RowState)dataGridView1.Rows[index].Cells[6].Value;

                        if (rowState == RowState.Deleted)
                        {
                            var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                            var deleteQuery = $"DELETE FROM problem_engineer WHERE id = {id}";

                            using (MySqlCommand command = new MySqlCommand(deleteQuery, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.OpenConnection();
                                command.ExecuteNonQuery();
                                DB.GetInstance.CloseConnection();

                            }
                        }
                    }
                    int currRowIndex = dataGridView1.CurrentCell.RowIndex;
                    QuerySettingDataBase.RefreshDataGridEngineer(dataGridView1);
                    dataGridView1.ClearSelection();

                    if (dataGridView1.RowCount - currRowIndex > 0)
                    {
                        dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка DeleteRowСell");
                }
            }
        }

        void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }

        void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (dataGridView1.Rows.Count > 0)
                    {
                        if(!String.IsNullOrEmpty(txB_id.Text))
                        {
                            ContextMenu m1 = new ContextMenu();
                            m1.MenuItems.Add(new MenuItem("Добавить новую неисправность", Btn_new_rst_problem_Click));
                            m1.MenuItems.Add(new MenuItem("Изменить неисправность", Btn_change_problem_Click));
                            m1.MenuItems.Add(new MenuItem("Удалить неисправность", Btn_delete_problem_Click));
                            m1.MenuItems.Add(new MenuItem("Сохранить в excel", Btn_save_excel_Click));
                            m1.MenuItems.Add(new MenuItem("Краткая иформация", Btn_brief_info_Click));
                            m1.Show(dataGridView1, new Point(e.X, e.Y));
                        }
                    }
                    if (dataGridView1.Rows.Count == 0)
                    {
                        ContextMenu m2 = new ContextMenu();
                        m2.MenuItems.Add(new MenuItem("Добавить новую неисправность", Btn_new_rst_problem_Click));
                        m2.MenuItems.Add(new MenuItem("Краткая иформация", Btn_brief_info_Click));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка ContextMenu (DataGridView1_MouseClick)");
            }
        }

        void Btn_save_excel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавь радиостанцию", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                DateTime dateTime = DateTime.Now;
                string dateTimeString = dateTime.ToString("dd.MM.yyyy");
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                sfd.FileName = $"ОБЩАЯ База_Неисправностей_{dateTimeString}";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Unicode))
                    {
                        string note = string.Empty;

                        note += $"Номер\tМодель\tНеисправность\tОписание неисправности\tВиды работ по устраненнию дефекта\tАвтор";

                        sw.WriteLine(note);

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                var re = new Regex(Environment.NewLine);
                                var value = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                value = re.Replace(value, " ");
                                //if (dataGridView1.Columns[j].HeaderText.ToString() == "№")
                                //{

                                //}
                                if (dataGridView1.Columns[j].HeaderText.ToString() == "Автор")
                                {
                                    sw.Write(value);
                                }
                                else if (dataGridView1.Columns[j].HeaderText.ToString() == "RowState")
                                {

                                }
                                else sw.Write(value + "\t");
                            }
                            sw.WriteLine();
                        }

                    }
                    MessageBox.Show("Файл успешно сохранен!");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
