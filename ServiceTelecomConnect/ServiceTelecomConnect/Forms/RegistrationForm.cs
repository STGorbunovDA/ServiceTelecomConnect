using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        void ClearMouseEnter(object sender, EventArgs e)
        {
            lbL_clear.ForeColor = Color.White;
        }
        void ClearMouseLeave(object sender, EventArgs e)
        {
            lbL_clear.ForeColor = Color.Black;
        }
        void ClearClick(object sender, EventArgs e)
        {
            txB_loginField.Text = String.Empty;
            txB_passField.Text = String.Empty;
            cmB_post.Text = String.Empty;
        }
        void RegistrationFormLoad(object sender, EventArgs e)
        {
            txB_passField.PasswordChar = '*';
            lbL_hidePassword.Visible = false;
            txB_loginField.MaxLength = 100;
            txB_passField.MaxLength = 32;
        }
        void OpenPasswordMouseEnter(object sender, EventArgs e)
        {
            lbL_openPassword.ForeColor = Color.White;
        }
        void OpenPasswordMouseLeave(object sender, EventArgs e)
        {
            lbL_openPassword.ForeColor = Color.Black;
        }
        void HidePasswordMouseEnter(object sender, EventArgs e)
        {
            lbL_hidePassword.ForeColor = Color.White;
        }
        void HidePasswordMouseLeave(object sender, EventArgs e)
        {
            lbL_hidePassword.ForeColor = Color.Black;
        }
        void OpenPasswordClick(object sender, EventArgs e)
        {
            txB_passField.UseSystemPasswordChar = true;
            lbL_hidePassword.Visible = true;
            lbL_openPassword.Visible = false;
        }
        void HidePasswordClick(object sender, EventArgs e)
        {
            txB_passField.UseSystemPasswordChar = false;
            lbL_hidePassword.Visible = false;
            lbL_openPassword.Visible = true;
        }
        void EnterBtnLoginClick(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                string loginUser = txB_loginField.Text;
                string passUser = Md5.EncryptPlainTextToCipherText(txB_passField.Text);

                if (!CheackUser(loginUser, passUser))
                {
                    if (cmB_post.Text == "Инженер" || cmB_post.Text == "Начальник участка" ||
                        cmB_post.Text == "Куратор" || cmB_post.Text == "Руководитель" || cmB_post.Text == "Дирекция связи")
                    {
                        string querystring = $"INSERT INTO users (login, pass, is_admin) " +
                            $"VALUES ('{loginUser}', '{passUser}', '{cmB_post.Text}')";
                        using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                        {
                            DB.GetInstance.OpenConnection();
                            if (command.ExecuteNonQuery() == 1)
                            {
                                MessageBox.Show("Аккаунт успешно создан!");
                                this.Close();
                            }
                            else MessageBox.Show("Аккаунт не создан! Ошибка соединения");
                            DB.GetInstance.CloseConnection();
                        }
                    }

                    if (!String.IsNullOrEmpty(cmB_post.Text)) MessageBox.Show("Вы не указали должность!");
                }
                else MessageBox.Show("Такой пользователь уже существует!");
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
                        if (table.Rows.Count > 0) return true;
                        else return false;
                    }
                }
            }
            return true;
        }
    }
}
