﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    public partial class Setting_user : Form
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

        public Setting_user()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColums()
        {
            try
            {
                dataGridView1.Columns.Add("id", "№");
                dataGridView1.Columns.Add("login", "Логин");
                dataGridView1.Columns.Add("pass", "Пароль");
                dataGridView1.Columns.Add("is_admin", "Должность");
                dataGridView1.Columns.Add("IsNew", String.Empty);
                dataGridView1.Columns[4].Visible = false;
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
                dataGridView1.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), RowState.ModifieldNew)));
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! Не загруженны данные в Datagridview(ReedSingleRow)");
            }
        }

        void RefreshDataGrid(DataGridView dgw)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    var myCulture = new CultureInfo("ru-RU");
                    myCulture.NumberFormat.NumberDecimalSeparator = ".";
                    Thread.CurrentThread.CurrentCulture = myCulture;
                    dgw.Rows.Clear();

                    string queryString = $"select * from users";

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
        void Setting_user_Load(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                CreateColums();
                RefreshDataGrid(dataGridView1);
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
                    textBox_id.Text = row.Cells[0].Value.ToString();
                    textBox_login.Text = row.Cells[1].Value.ToString();
                    textBox_pass.Text = row.Cells[2].Value.ToString();
                    comboBox_is_admin.Text = row.Cells[3].Value.ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка! (DataGridView1_CellClick)");
            }
        }

        void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.Cancel = true;
            }
        }

        private void Button_update_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    RefreshDataGrid(dataGridView1);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка! Не загруженны данные в Datagridview(RefreshDataGrid)");
                }
            }
        }

        void Button_delete_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        dataGridView1.Rows[row.Index].Cells[4].Value = RowState.Deleted;
                    }
                    if (Internet_check.AvailabilityChanged_bool())
                    {

                        DB.GetInstance.OpenConnection();

                        for (int index = 0; index < dataGridView1.Rows.Count; index++)
                        {
                            var rowState = (RowState)dataGridView1.Rows[index].Cells[4].Value;

                            if (rowState == RowState.Deleted)
                            {
                                var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                                var deleteQuery = $"delete from users where id = {id}";

                                using (MySqlCommand command = new MySqlCommand(deleteQuery, DB.GetInstance.GetConnection()))
                                {
                                    command.ExecuteNonQuery();
                                }
                            }
                            if (rowState == RowState.Modifield)
                            {
                                var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                                var login = dataGridView1.Rows[index].Cells[1].Value.ToString();
                                var pass = dataGridView1.Rows[index].Cells[2].Value.ToString();
                                var is_admin = dataGridView1.Rows[index].Cells[3].Value.ToString();


                                var changeQuery = $"update users set login = '{login}', pass = '{pass}', is_Admin = '{is_admin}' where id = '{id}'";

                                using (MySqlCommand command = new MySqlCommand(changeQuery, DB.GetInstance.GetConnection()))
                                {
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        DB.GetInstance.CloseConnection();
                    }
                    RefreshDataGrid(dataGridView1);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка! Не возможно удалить данные (Button_delete_Click)");
                }
            }
        }
        void Button_change_Click(object sender, EventArgs e)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    var id = textBox_id.Text;
                    var login = textBox_login.Text;
                    var pass = textBox_pass.Text;
                    var is_admin = comboBox_is_admin.Text;

                    var changeQuery = $"update users set login = '{login.Trim()}', pass = '{pass.Trim()}', is_Admin = '{is_admin.Trim()}' where id = '{id.Trim()}'";

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
