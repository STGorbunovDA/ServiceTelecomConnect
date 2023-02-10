using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ServiceTelecomConnect.Forms
{
    public partial class DirectorForm : Form
    {
        int selectedRow;
        private readonly CheakUser _user;

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
        public DirectorForm(CheakUser user)
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            _user = user;
        }

        private void CreateColums()
        {
            dataGridView1.Columns.Add("id", "№");
            dataGridView1.Columns.Add("section_foreman_FIO", "Начальник участка");
            dataGridView1.Columns.Add("engineers_FIO", "Инженер");
            dataGridView1.Columns.Add("attorney", "Доверенность");
            dataGridView1.Columns.Add("road", "Дорога");
            dataGridView1.Columns.Add("numberPrintDocument", "№ печати");
            dataGridView1.Columns.Add("curator", "Куратор");
            dataGridView1.Columns.Add("departmentCommunications", "Представитель дирекции");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[0].Width = 45;
            dataGridView1.Columns[8].Width = 80;
        }
        void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            dataGridView1.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1),
                record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6),
                record.GetString(7), RowState.ModifieldNew)));
        }
        void DataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = false;
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                txB_id.Text = row.Cells[0].Value.ToString();
                cmB_sectionForemanFIO.Text = row.Cells[1].Value.ToString();
                cmB_EngineersFIO.Text = row.Cells[2].Value.ToString();
                txB_attorney.Text = row.Cells[3].Value.ToString();
                cmB_road.Text = row.Cells[4].Value.ToString();
                txB_numberPrintDocument.Text = row.Cells[5].Value.ToString();
                cmB_curator.Text = row.Cells[6].Value.ToString();
                cmB_departmentCommunications.Text = row.Cells[7].Value.ToString();
            }
        }
        void RefreshDataGrid(DataGridView dgw)
        {
            if (InternetCheck.CheackSkyNET())
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = myCulture;
                dgw.Rows.Clear();
                string queryString = $"SELECT id, section_foreman_FIO, engineers_FIO, attorney, road, numberPrintDocument, " +
                    $"curator, departmentCommunications FROM сharacteristics_вrigade";
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
        void DirectorFormLoad(object sender, System.EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                string querystring = $"SELECT id, login, is_admin FROM users WHERE is_admin = 'Начальник участка'";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable table = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            cmB_sectionForemanFIO.DataSource = table;
                            cmB_sectionForemanFIO.ValueMember = "id";
                            cmB_sectionForemanFIO.DisplayMember = "login";
                        }
                        else cmB_sectionForemanFIO.Text = String.Empty;
                    }
                }

                string querystring2 = $"SELECT id, login, is_admin FROM users WHERE is_admin = 'Инженер'";
                using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable table = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            cmB_EngineersFIO.DataSource = table;
                            cmB_EngineersFIO.ValueMember = "id";
                            cmB_EngineersFIO.DisplayMember = "login";
                        }
                        else cmB_EngineersFIO.Text = "";
                    }
                }
                string querystring3 = $"SELECT id, login, is_admin FROM users WHERE is_admin = 'Куратор'";
                using (MySqlCommand command = new MySqlCommand(querystring3, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable table = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            cmB_curator.DataSource = table;
                            cmB_curator.ValueMember = "id";
                            cmB_curator.DisplayMember = "login";
                        }
                        else cmB_curator.Text = "";

                    }
                }
                string querystring4 = $"SELECT id, login, is_admin FROM users WHERE is_admin = 'Дирекция связи'";
                using (MySqlCommand command = new MySqlCommand(querystring4, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable table = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            cmB_departmentCommunications.DataSource = table;
                            cmB_departmentCommunications.ValueMember = "id";
                            cmB_departmentCommunications.DisplayMember = "login";
                        }
                        else cmB_departmentCommunications.Text = "";

                    }
                }
                if (String.IsNullOrWhiteSpace(cmB_departmentCommunications.Text))
                    MessageBox.Show("Добавьте представителя дирекции связи!");
                if (String.IsNullOrWhiteSpace(cmB_curator.Text))
                    MessageBox.Show("Добавьте куратора!");
                if (String.IsNullOrWhiteSpace(cmB_road.Text))
                    cmB_road.Text = cmB_road.Items[0].ToString();
                if (String.IsNullOrWhiteSpace(cmB_EngineersFIO.Text))
                    MessageBox.Show("Добавьте инженера!");
                if (String.IsNullOrWhiteSpace(cmB_sectionForemanFIO.Text))
                    MessageBox.Show("Добавьте начальника участка!");

                CreateColums();
                RefreshDataGrid(dataGridView1);
            }
        }
        void BtnAddRegistrationEmployeessClick(object sender, EventArgs e)
        {
            var re = new Regex(Environment.NewLine);
            txB_attorney.Text = re.Replace(txB_attorney.Text, " ");
            txB_attorney.Text.Trim();
            if (String.IsNullOrWhiteSpace(cmB_sectionForemanFIO.Text))
            {
                MessageBox.Show("Поле \"Начальник\" не должен быть пустым, добавьте начальника участка", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmB_EngineersFIO.Text))
            {
                MessageBox.Show("Поле \"Инженер\" не должен быть пустым, добавьте инженера", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmB_road.Text))
            {
                MessageBox.Show("Поле \"Дорога\" не должна быть пустым, добавьте дорогу", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmB_curator.Text))
            {
                MessageBox.Show("Поле \"Куратор\" не должно быть пустым, добавьте куратора", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmB_departmentCommunications.Text))
            {
                MessageBox.Show("Поле \"Представитель дирекции связи\" не должно быть пустым, добавьте представителя дирекции связи", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!Regex.IsMatch(txB_attorney.Text, @"^[0-9]{1,}[\/][0-9]{1,}[\s][о][т][\s][0-9]{2,2}[\.][0-9]{2,2}[\.][2][0][0-9]{2,2}[\s][г][о][д][а]$"))
            {
                MessageBox.Show("Введите корректно \"Доверенность\"\n P.s. Пример: 53/53 от 10.01.2023 года", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_attorney.Select();
                return;
            }
            if (!Regex.IsMatch(txB_numberPrintDocument.Text, @"^[0-9]{2,}$"))
            {
                MessageBox.Show("Введите корректно \"№ печати\"\n P.s. Пример: 53", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_numberPrintDocument.Select();
                return;
            }
            if (InternetCheck.CheackSkyNET())
            {
                var addQuery = $"INSERT INTO сharacteristics_вrigade (section_foreman_FIO, engineers_FIO, attorney, " +
                    $"road, numberPrintDocument, curator, departmentCommunications) VALUES ('{cmB_sectionForemanFIO.Text}', " +
                    $"'{cmB_EngineersFIO.Text}', '{txB_attorney.Text}', '{cmB_road.Text}', '{txB_numberPrintDocument.Text}', " +
                    $"'{cmB_curator.Text}', '{cmB_departmentCommunications.Text}')";

                using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                    MessageBox.Show("Бригада сформирована", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            RefreshDataGrid(dataGridView1);
        }
        void DataGridView1CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
                e.Cancel = true;
        }
        void BtnChangeRegistrationEmployeesClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                var id = txB_id.Text.Trim();
                var re = new Regex(Environment.NewLine);
                txB_attorney.Text = re.Replace(txB_attorney.Text, " ");
                txB_attorney.Text.Trim();
                var re2 = new Regex(Environment.NewLine);
                txB_numberPrintDocument.Text = re2.Replace(txB_numberPrintDocument.Text, " ");
                txB_numberPrintDocument.Text.Trim();
                if (String.IsNullOrWhiteSpace(cmB_sectionForemanFIO.Text))
                {
                    MessageBox.Show("Поле \"Начальник\" не должен быть пустым, добавьте начальника участка", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (String.IsNullOrWhiteSpace(cmB_EngineersFIO.Text))
                {
                    MessageBox.Show("Поле \"Инженер\" не должен быть пустым, добавьте инженера", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (String.IsNullOrWhiteSpace(cmB_road.Text))
                {
                    MessageBox.Show("Поле \"Дорога\" не должна быть пустым, добавьте дорогу", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (String.IsNullOrWhiteSpace(cmB_curator.Text))
                {
                    MessageBox.Show("Поле \"Куратор\" не должно быть пустым, добавьте куратора", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (String.IsNullOrWhiteSpace(cmB_departmentCommunications.Text))
                {
                    MessageBox.Show("Поле \"Представитель дирекции связи\" не должно быть пустым, добавьте представителя дирекции связи", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!Regex.IsMatch(txB_attorney.Text, @"^[0-9]{1,}[\/][0-9]{1,}[\s][о][т][\s][0-9]{2,2}[\.][0-9]{2,2}[\.][2][0][0-9]{2,2}[\s][г][о][д][а]$"))
                {
                    MessageBox.Show("Введите корректно \"Доверенность\"\n P.s. Пример: 53/53 от 10.01.2023 года", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_attorney.Select();
                    return;
                }
                if (!Regex.IsMatch(txB_numberPrintDocument.Text, @"^[0-9]{2,}$"))
                {
                    MessageBox.Show("Введите корректно \"№ печати\"\n P.s. Пример: 53", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_numberPrintDocument.Select();
                    return;
                }

                var changeQuery = $"UPDATE сharacteristics_вrigade SET section_foreman_FIO = '{cmB_sectionForemanFIO.Text}', " +
                    $"engineers_FIO = '{cmB_EngineersFIO.Text}', attorney = '{txB_attorney.Text}', road = '{cmB_road.Text}'," +
                    $"numberPrintDocument = '{txB_numberPrintDocument.Text}', curator = '{cmB_curator.Text}', " +
                    $"departmentCommunications = '{cmB_departmentCommunications.Text}' WHERE id = '{id}'";

                using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                    MessageBox.Show("Запись успешно изменена!");
                }
                RefreshDataGrid(dataGridView1);
            }
        }
        void BtnDeleteRegistrationEmployeesClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    dataGridView1.Rows[row.Index].Cells[8].Value = RowState.Deleted;
                DB.GetInstance.OpenConnection();
                for (int index = 0; index < dataGridView1.Rows.Count; index++)
                {
                    var rowState = (RowState)dataGridView1.Rows[index].Cells[8].Value;
                    if (rowState == RowState.Deleted)
                    {
                        var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                        var deleteQuery = $"delete from сharacteristics_вrigade where id = {id}";
                        using (MySqlCommand command = new MySqlCommand(deleteQuery, DB.GetInstance.GetConnection()))
                            command.ExecuteNonQuery();
                    }
                }
                DB.GetInstance.CloseConnection();
                RefreshDataGrid(dataGridView1);
            }
        }
        void UpdateClick(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }
        void ClearControlFormClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cmB_sectionForemanFIO.Text))
            {
                MessageBox.Show("Поле \"Начальник\" не должен быть пустым, добавьте начальника участка", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmB_EngineersFIO.Text))
            {
                MessageBox.Show("Поле \"Инженер\" не должен быть пустым, добавьте инженера", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cmB_sectionForemanFIO.Text = cmB_sectionForemanFIO.Items[0].ToString();
            cmB_EngineersFIO.Text = cmB_EngineersFIO.Items[0].ToString();
            cmB_road.Text = cmB_road.Items[0].ToString();
            cmB_curator.Text = cmB_road.Items[0].ToString();
            cmB_departmentCommunications.Text = cmB_road.Items[0].ToString();
            txB_attorney.Clear();
            txB_numberPrintDocument.Clear();
        }
        void DirectorFormFormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(1);
        }
        void DirectorFormFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = FormClose.GetInstance.FClose(_user.Login);
        }
        void BtnReportCardClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Нет сформированных бригад");
                return;
            }
            using (ReportCardForm reportCard = new ReportCardForm())
            {
                this.Hide();
                reportCard.ShowDialog();
                this.Show();
            }
        }
    }
}
