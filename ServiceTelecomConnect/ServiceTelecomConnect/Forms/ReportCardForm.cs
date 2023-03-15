using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ServiceTelecomConnect.Forms
{
    public partial class ReportCardForm : Form
    {
        #region состояние Rows

        enum RowState
        {
            Existed,
            New,
            Modifield,
            ModifieldNew,
            Deleted
        }
        #endregion
        int selectedRow;
        public ReportCardForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }
        private void CreateColums()
        {
            dataGridView1.Columns.Add("id", "№");
            dataGridView1.Columns.Add("user", "Работник");
            dataGridView1.Columns.Add("dateTimeInput", "Дата входа");
            dataGridView1.Columns.Add("dateTimeExit", "Дата выхода");
            dataGridView1.Columns.Add("TimeCount", "Время нахождения");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }
        void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            dataGridView1.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1),
                record.GetDateTime(2), record.GetDateTime(3), record.GetDateTime(3).Subtract(record.GetDateTime(2)), RowState.ModifieldNew)));
        }
        void RefreshDataGrid(DataGridView dgw)
        {
            if (InternetCheck.CheackSkyNET())
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = myCulture;
                dgw.Rows.Clear();
                string queryString = $"SELECT id, user, dateTimeInput, dateTimeExit FROM logUserDB";
                using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                ReedSingleRow(dgw, reader);
                            reader.Close();
                        }
                    }
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                }
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            }
        }
        void ReportCardFormLoad(object sender, EventArgs e)
        {
            CreateColums();
            RefreshDataGrid(dataGridView1);
            string querystring2 = $"SELECT DISTINCT DATE(dateTimeInput) FROM logUserDB ORDER BY dateTimeInput";
            using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable table = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);
                    cmB_dateTimeInput.DataSource = table;
                    cmB_dateTimeInput.DisplayMember = "DATE(dateTimeInput)";
                    cmB_dateTimeInput.ValueMember = "DATE(dateTimeInput)";
                    DB.GetInstance.CloseConnection();
                }
            }
            if (cmB_dateTimeInput.Items.Count > 0)
            {
                cmB_dateTimeInput.SelectedIndex = cmB_dateTimeInput.Items.Count - 1;
                CmbDateTimeInputSelectionChangeCommitted(sender, e);
            }
            this.dataGridView1.Sort(this.dataGridView1.Columns["dateTimeInput"], ListSortDirection.Ascending);
        }
        void DataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = false;
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                txB_id.Text = row.Cells[0].Value.ToString();
                txB_user.Text = row.Cells[1].Value.ToString();
                txB_dateTimeInput.Text = row.Cells[2].Value.ToString();
                txB_dateTimeExit.Text = row.Cells[3].Value.ToString();
                txB_timeCount.Text = row.Cells[4].Value.ToString();
            }
        }
        void DataGridView1CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
                e.Cancel = true;
        }
        void PicB_Update_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }
        void BtnSaveExcelClick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            string dateTimeString = dateTime.ToString("dd.MM.yyyy");
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            sfd.FileName = $"Табель сотрудников_{dateTimeString}";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Unicode))
                {
                    string note = string.Empty;
                    note += $"Работник\tДата входа\tДата выхода\tВремя нахождения";
                    sw.WriteLine(note);
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            Regex re = new Regex(Environment.NewLine);
                            string value = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            value = re.Replace(value, " ");
                            if (dataGridView1.Columns[j].HeaderText.ToString() == "№")
                            {

                            }
                            else if (dataGridView1.Columns[j].HeaderText.ToString() == "Время нахождения")
                            {
                                sw.Write(value);
                            }
                            else sw.Write(value + "\t");
                        }
                        sw.WriteLine();
                    }
                }
                MessageBox.Show("Файл успешно сохранен!");
            }
        }
        void CmbDateTimeInputSelectionChangeCommitted(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (cmB_dateTimeInput.Items.Count == 0)
                return;
            string date = Convert.ToDateTime(cmB_dateTimeInput.Text).ToString("yyyy-MM-dd");
            string searchString = $"SELECT id, user, dateTimeInput, dateTimeExit FROM logUserDB WHERE dateTimeInput LIKE '%" + date + "%'";
            using (MySqlCommand command = new MySqlCommand(searchString, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            ReedSingleRow(dataGridView1, reader);
                        reader.Close();
                    }
                }
                DB.GetInstance.CloseConnection();
            }
        }
        void PicbDeleteClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    dataGridView1.Rows[row.Index].Cells[5].Value = RowState.Deleted;
                DB.GetInstance.OpenConnection();
                for (int index = 0; index < dataGridView1.Rows.Count; index++)
                {
                    var rowState = (RowState)dataGridView1.Rows[index].Cells[5].Value;
                    if (rowState == RowState.Deleted)
                    {
                        int id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                        string deleteQuery = $"delete from logUserDB where id = {id}";
                        using (MySqlCommand command = new MySqlCommand(deleteQuery, DB.GetInstance.GetConnection()))
                            command.ExecuteNonQuery();
                    }
                }
                DB.GetInstance.CloseConnection();
                RefreshDataGrid(dataGridView1);
            }
        }
    }
}
