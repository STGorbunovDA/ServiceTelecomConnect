using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Xamarin.Forms.Shapes;

namespace ServiceTelecomConnect.Forms
{
    public partial class DirectorForm : Form
    {
        int selectedRow;

        #region состояние Rows
        /// <summary>
        /// для значений к базе данных, по данному статусу будем или удалять или редактировать
        /// </summary>
        enum RowState
        {
            Existed,
            New,
            Modifield,
            ModifieldNew,
            Deleted
        }
        #endregion
        public DirectorForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void CreateColums()
        {
            try
            {
                dataGridView1.Columns.Add("id", "№");
                dataGridView1.Columns.Add("section_foreman_FIO", "Начальник участка");
                dataGridView1.Columns.Add("engineers_FIO", "Инженер");
                dataGridView1.Columns.Add("attorney", "Доверенность");
                dataGridView1.Columns.Add("road", "Дорога");
                dataGridView1.Columns.Add("IsNew", String.Empty);
                dataGridView1.Columns[5].Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Не сформированы столбцы Datagrid(CreateColums)");
            }
        }
        void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            try
            {
                dataGridView1.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), RowState.ModifieldNew)));
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Не загруженны данные в Datagridview(ReedSingleRow)");
            }
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
                    cmB_section_foreman_FIO.Text = row.Cells[1].Value.ToString();
                    cmB_engineers_FIO.Text = row.Cells[2].Value.ToString();
                    txB_attorney.Text = row.Cells[3].Value.ToString();
                    cmB_road.Text = row.Cells[4].Value.ToString();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! (DataGridView1_CellClick)");
            }
        }

        void RefreshDataGrid(DataGridView dgw)
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();

                    string queryString = $"SELECT id, section_foreman_FIO, engineers_FIO, attorney, road FROM сharacteristics_вrigade";

                    using (MySqlCommand command = new MySqlCommand(queryString, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ReedSingleRow(dgw, reader);
                                }
                                reader.Close();
                            }
                        }
                        command.ExecuteNonQuery();
                        DB.GetInstance.CloseConnection();
                    }
                    dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка! Не загруженны данные в Datagridview(RefreshDataGrid)");
                }
            }
        }

        void DirectorForm_Load(object sender, System.EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                try
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
                                cmB_section_foreman_FIO.DataSource = table;
                                cmB_section_foreman_FIO.ValueMember = "id";
                                cmB_section_foreman_FIO.DisplayMember = "login";
                            }
                            else
                            {
                                cmB_section_foreman_FIO.Text = "";
                            }
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
                                cmB_engineers_FIO.DataSource = table;
                                cmB_engineers_FIO.ValueMember = "id";
                                cmB_engineers_FIO.DisplayMember = "login";
                            }
                            else
                            {
                                cmB_section_foreman_FIO.Text = "";
                            }
                        }
                    }

                    if (String.IsNullOrEmpty(cmB_road.Text))
                    {
                        cmB_road.Text = cmB_road.Items[0].ToString();
                    }
                    if (String.IsNullOrEmpty(cmB_engineers_FIO.Text))
                    {
                        MessageBox.Show("Добавьте инженера!");
                    }
                    if (String.IsNullOrEmpty(cmB_section_foreman_FIO.Text))
                    {
                        MessageBox.Show("Добавьте начальника участка!");
                    }

                    CreateColums();
                    RefreshDataGrid(dataGridView1);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка добавления в comboBox данных");
                }
            }
        }

        void Btn_add_registrationEmployeess_Click(object sender, EventArgs e)
        {
            try
            {
                var re = new Regex(Environment.NewLine);
                txB_attorney.Text = re.Replace(txB_attorney.Text, " ");
                txB_attorney.Text.Trim();

                if (String.IsNullOrEmpty(cmB_section_foreman_FIO.Text))
                {
                    MessageBox.Show("Поле \"Начальник\" не должен быть пустым, добавьте начальника участка", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (String.IsNullOrEmpty(cmB_engineers_FIO.Text))
                {
                    MessageBox.Show("Поле \"Инженер\" не должен быть пустым, добавьте инженера", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (String.IsNullOrEmpty(cmB_road.Text))
                {
                    MessageBox.Show("Поле \"Дорога\" не должна быть пустым, добавьте дорогу", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!Regex.IsMatch(txB_attorney.Text, @"[0-9]{1,}[\/][0-9]{1,}[\s][о][т][\s][0-9]{2,2}[\.][0-9]{2,2}[\.][2][0][0-9]{2,2}[\s][г][о][д][а]$"))
                {
                    MessageBox.Show("Введите корректно \"Доверенность\"\n P.s. Пример: 53/53 от 10.01.2023 года", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_attorney.Select();
                    return;
                }
                if (Internet_check.CheackSkyNET())
                {
                    var addQuery = $"INSERT INTO сharacteristics_вrigade (section_foreman_FIO, engineers_FIO, attorney, road) " +
                        $"VALUES ('{cmB_section_foreman_FIO.Text}', '{cmB_engineers_FIO.Text}', '{txB_attorney.Text}','{cmB_road.Text}')";

                    using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        DB.GetInstance.CloseConnection();
                        MessageBox.Show("Бригада сформирована", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }

        void Btn_change_registrationEmployees_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    var id = txB_id.Text.Trim();
                    var re = new Regex(Environment.NewLine);
                    txB_attorney.Text = re.Replace(txB_attorney.Text, " ");
                    txB_attorney.Text.Trim();

                    var changeQuery = $"update сharacteristics_вrigade set section_foreman_FIO = '{cmB_section_foreman_FIO.Text}', " +
                        $"engineers_FIO = '{cmB_engineers_FIO.Text}', attorney = '{txB_attorney.Text}', road = '{cmB_road.Text}' where id = '{id}'";

                    using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        DB.GetInstance.CloseConnection();
                        MessageBox.Show("Запись успешно изменена!");
                    }
                    RefreshDataGrid(dataGridView1);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка! Не возможно изменить данные (Button_change_Click)");
                }
            }
        }
    }
}
