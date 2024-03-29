﻿using MySql.Data.MySqlClient;
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
        private readonly CheakUser _user;
        int selectedRow;
        public TutorialForm(CheakUser user)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            dataGridView1.DoubleBuffered(true);
            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            _user = user;
            cmB_seach.Text = cmB_seach.Items[3].ToString();
        }
        void TutorialFormLoad(object sender, EventArgs e)
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
        void Update_Click(object sender, EventArgs e)
        {
            QuerySettingDataBase.RefreshDataGridEngineer(dataGridView1);
        }
        void BtnBriefInfoClick(object sender, EventArgs e)
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
        void DataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
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
        void DataGridView1CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
                e.Cancel = true;
        }
        void BtnSaveExcelClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавь радиостанцию", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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
                            Regex re = new Regex(Environment.NewLine);
                            string value = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            value = re.Replace(value, " ");
                            if (dataGridView1.Columns[j].HeaderText.ToString() == "Автор")
                                sw.Write(value);
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

        void CmbSeachSelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmB_seach.SelectedIndex == 0)
            {
                cmb_unique.Visible = true;
                txB_search.Visible = false;
                txB_search.Clear();
                QuerySettingDataBase.CmbUniqueModelEngineer(cmb_unique);
            }
            if (cmB_seach.SelectedIndex == 1)
            {
                cmb_unique.Visible = true;
                txB_search.Visible = false;
                txB_search.Clear();
                QuerySettingDataBase.CmbUniqueProblemEngineer(cmb_unique);
            }
            if (cmB_seach.SelectedIndex == 2)
            {
                cmb_unique.Visible = true;
                txB_search.Visible = false;
                txB_search.Clear();
                QuerySettingDataBase.CmbUniqueAuthorEngineer(cmb_unique);
            }
            if (cmB_seach.SelectedIndex == 3)
            {
                txB_search.Visible = true;
                cmb_unique.Visible = false;
            }
        }
        void CmbUniqueSelectedIndexChanged(object sender, EventArgs e)
        {
            QuerySettingDataBase.SearchEngineer(dataGridView1, cmB_seach.Text, txB_search.Text, cmb_unique.Text);
        }
        void TxbSearchDoubleClick(object sender, EventArgs e)
        {
            QuerySettingDataBase.SearchEngineer(dataGridView1, cmB_seach.Text, txB_search.Text, cmb_unique.Text);
        }
        void TxbSearchKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                QuerySettingDataBase.SearchEngineer(dataGridView1, cmB_seach.Text, txB_search.Text, cmb_unique.Text);
        }

        void BtnNewRadiostantionProblemClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                AddToProblemRST addProblemRST = new AddToProblemRST(_user);
                if (Application.OpenForms["AddToProblemRST"] == null)
                {
                    addProblemRST.DoubleBufferedForm(true);
                    addProblemRST.Show();
                }
            }
        }     
        void BtnChangeProblemClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                if (!String.IsNullOrWhiteSpace(txB_id.Text))
                {
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
        }
        void BtnDeleteProblemClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txB_id.Text))
            {
                MessageBox.Show("Выбери строку которую хочешь удалить!");
                return;
            }
            if (dataGridView1.SelectedRows.Count > 1)
            {
                string Mesage = $"Вы действительно хотите удалить неисправность у модели: {txB_model.Text}?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }

            if (InternetCheck.CheackSkyNET())
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    dataGridView1.Rows[row.Index].Cells[6].Value = RowState.Deleted;
                for (int index = 0; index < dataGridView1.Rows.Count; index++)
                {
                    var rowState = (RowState)dataGridView1.Rows[index].Cells[6].Value;
                    if (rowState == RowState.Deleted)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                        string deleteQuery = $"DELETE FROM problem_engineer WHERE id = {id}";
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
                    dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
            }
        }

        void DataGridView1MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    if (!String.IsNullOrWhiteSpace(txB_id.Text))
                    {
                        ContextMenu m1 = new ContextMenu();
                        m1.MenuItems.Add(new MenuItem("Добавить новую неисправность", BtnNewRadiostantionProblemClick));
                        m1.MenuItems.Add(new MenuItem("Изменить неисправность", BtnChangeProblemClick));
                        m1.MenuItems.Add(new MenuItem("Удалить неисправность", BtnDeleteProblemClick));
                        m1.MenuItems.Add(new MenuItem("Сохранить в excel", BtnSaveExcelClick));
                        m1.MenuItems.Add(new MenuItem("Краткая иформация", BtnBriefInfoClick));
                        m1.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                }
                if (dataGridView1.Rows.Count == 0)
                {
                    ContextMenu m2 = new ContextMenu();
                    m2.MenuItems.Add(new MenuItem("Добавить новую неисправность", BtnNewRadiostantionProblemClick));
                    m2.MenuItems.Add(new MenuItem("Краткая иформация", BtnBriefInfoClick));
                }
            }
        }
    }
}
