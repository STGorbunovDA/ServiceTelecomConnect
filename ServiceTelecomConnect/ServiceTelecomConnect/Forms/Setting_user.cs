using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
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
            dataGridView1.Columns.Add("id", "№");
            dataGridView1.Columns.Add("login", "Логин");
            dataGridView1.Columns.Add("pass", "Пароль");
            dataGridView1.Columns.Add("is_admin", "Должность");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[4].Visible = false;
        }

        void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            dataGridView1.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1),
                record.GetString(2), record.GetString(3), RowState.ModifieldNew)));
        }

        void RefreshDataGrid(DataGridView dgw)
        {
            if (Internet_check.CheackSkyNET())
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = myCulture;
                dgw.Rows.Clear();

                string queryString = $"select id, login, pass, is_admin from users";

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
        }
        void Setting_user_Load(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                CreateColums();
                RefreshDataGrid(dataGridView1);
            }
        }

        void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = false;

            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                txB_id.Text = row.Cells[0].Value.ToString();
                txB_login.Text = row.Cells[1].Value.ToString();
                //txB_pass.Text = row.Cells[2].Value.ToString();
                txB_pass.Text = Md5.DecryptCipherTextToPlainText(row.Cells[2].Value.ToString());
                cmB_is_admin_post.Text = row.Cells[3].Value.ToString();
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
            if (Internet_check.CheackSkyNET())
            {
                RefreshDataGrid(dataGridView1);
            }
        }

        void Button_delete_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows[row.Index].Cells[4].Value = RowState.Deleted;
                }
                if (Internet_check.CheackSkyNET())
                {

                    DB.GetInstance.OpenConnection();

                    for (int index = 0; index < dataGridView1.Rows.Count; index++)
                    {
                        var rowState = (RowState)dataGridView1.Rows[index].Cells[4].Value;

                        if (rowState == RowState.Deleted)
                        {
                            var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                            var login = dataGridView1.Rows[index].Cells[1].Value;
                            var deleteQuery = $"delete from users where id = {id}";

                            var update_сharacteristics_вrigade = $"UPDATE сharacteristics_вrigade SET section_foreman_FIO = '' " +
                                $"OR engineers_FIO = '' OR curator = '' OR departmentCommunications = '' " +
                                $"WHERE section_foreman_FIO = '{login}' OR engineers_FIO = '{login}' OR curator = '{login}' " +
                                $"OR departmentCommunications = '{login}'";

                            using (MySqlCommand command = new MySqlCommand(deleteQuery, DB.GetInstance.GetConnection()))
                            {
                                command.ExecuteNonQuery();
                            }
                            using (MySqlCommand command2 = new MySqlCommand(update_сharacteristics_вrigade, DB.GetInstance.GetConnection()))
                            {
                                command2.ExecuteNonQuery();
                            }

                        }
                    }
                    DB.GetInstance.CloseConnection();
                }
                RefreshDataGrid(dataGridView1);
            }
        }
        void Button_change_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                var id = txB_id.Text;
                var login = txB_login.Text;
                var pass = Md5.EncryptPlainTextToCipherText(txB_pass.Text);
                var is_admin = cmB_is_admin_post.Text;

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
        }

        void PicB_clear_Click(object sender, EventArgs e)
        {
            foreach (Control control in panel2.Controls)
            {
                if (control is TextBox)
                {
                    control.Text = "";
                }
            }
        }

        void Btn_add_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                var loginUser = txB_login.Text;
                if (!loginUser.Contains("-"))
                {
                    if (!Regex.IsMatch(loginUser, @"^[А-ЯЁ][а-яё]*(([\s]+[А-Я][\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"Логин\"\nP.s. пример: Иванов В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_login.Select();
                        return;
                    }
                }
                if (loginUser.Contains("-"))
                {
                    if (!Regex.IsMatch(loginUser, @"^[А-ЯЁ][а-яё]*(([\-][А-Я][а-яё]*[\s]+[А-Я]+[\.]+[А-Я]+[\.])$)"))
                    {
                        MessageBox.Show("Введите корректно поле \"Логин\"\nP.s. пример: Иванов-Петров В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txB_login.Select();
                        return;
                    }
                }


                var passUser = Md5.EncryptPlainTextToCipherText(txB_pass.Text);
                if (!CheackUser(loginUser, passUser))
                {
                    if (!String.IsNullOrEmpty(cmB_is_admin_post.Text))
                    {

                        string querystring = $"INSERT INTO users (login, pass, is_admin) VALUES ('{loginUser}', '{passUser}', '{cmB_is_admin_post.Text}')";

                        using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();

                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Аккаунт успешно создан!");
                                RefreshDataGrid(dataGridView1);
                            }
                            else
                            {
                                MessageBox.Show("Аккаунт не создан! Ошибка соединения");
                            }
                            DB.GetInstance.CloseConnection();
                        }
                    }
                    else { MessageBox.Show("Должность и Дорога не должны быть пустыми!"); }
                }
                else
                {
                    MessageBox.Show("Такой пользователь уже существует!");
                }
            }
        }

        Boolean CheackUser(string loginUser, string passUser)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT * FROM users WHERE login = '{loginUser}' AND pass = '{passUser}'";

                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            return true;
                        }

                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
