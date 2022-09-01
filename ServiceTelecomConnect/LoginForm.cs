using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;


namespace ServiceTelecomConnect
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            if (!InstanceChecker.TakeMemory())
            
            StartPosition = FormStartPosition.CenterScreen;
            Environment.GetCommandLineArgs().ToList().ForEach(x =>
            {
                if (x.EndsWith("/Admin"))
                {
                    loginField.Text = "Admin";
                    passField.Text = "1818";
                }
            });
        }
        void LoginForm_Load(object sender, EventArgs e)
        {
            passField.PasswordChar = '*';
            hidePassword.Visible = false;
            loginField.MaxLength = 100;
            passField.MaxLength = 32;
            if(loginField.Text == "Admin" || passField.Text == "1818")
                EnterButtonLogin_Click(sender, e);

        }
        void EnterButtonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if(AvailabilityChanged_bool())
                {
                    var loginUser = loginField.Text;
                    var passUser = md5.hashPassword(passField.Text);

                    string querystring = $"SELECT id, login, pass, is_admin	FROM users WHERE login = '{loginUser}' AND pass = '{passUser}'";

                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.openConnection();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count == 1)
                            {
                                var user = new cheakUser(table.Rows[0].ItemArray[1].ToString(), table.Rows[0].ItemArray[3].ToString());

                                //MessageBox.Show("Вы успешно авторизировались!");
                                Menu menu = new Menu(user);
                                this.Hide();
                                menu.ShowDialog();
                                DB.GetInstance.closeConnection();
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин и пароль");
                                DB.GetInstance.closeConnection();
                            }
                        }
                    }
                }
                                       
            }
            catch (MySqlException)
            {
                DB.GetInstance.closeConnection();
                string Mesage2;
                Mesage2 = "Системная ошибка авторизации";

                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return;
                }
            }

        }
        void OpenPassword_Click(object sender, EventArgs e)
        {
            passField.UseSystemPasswordChar = true;
            hidePassword.Visible = true;
            openPassword.Visible = false;
        }
        void HidePassword_Click(object sender, EventArgs e)
        {
            passField.UseSystemPasswordChar = false;
            hidePassword.Visible = false;
            openPassword.Visible = true;
        }
        void Clear_Click(object sender, EventArgs e)
        {
            loginField.Text = "";
            passField.Text = "";
        }
        /// <summary>
        /// при нажатии на регистрацию вывод формы регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RegistrationLoginForm_Click(object sender, EventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            this.Hide();
            registrationForm.ShowDialog();
            this.Show();
        }
        #region Подсветка
        /// <summary>
        /// подсвечивании лебла регистрации в белый цвет при наведении мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RegistrationLoginForm_MouseEnter(object sender, EventArgs e)
        {
            registrationLoginForm.ForeColor = Color.White;
        }
        /// <summary>
        /// подсвечивании лебла регистрации в черный цвет при убирании мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RegistrationLoginForm_MouseLeave(object sender, EventArgs e)
        {
            registrationLoginForm.ForeColor = Color.Black;
        }
        /// <summary>
        /// Подсвечивание лебла показать пароль в белый
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OpenPassword_MouseEnter(object sender, EventArgs e)
        {
            openPassword.ForeColor = Color.White;
        }
        /// <summary>
        /// Подсвечивания лебла показать пароль в черный
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OpenPassword_MouseLeave(object sender, EventArgs e)
        {
            openPassword.ForeColor = Color.Black;
        }
        /// <summary>
        /// Подсвечивания лебла скрыть пароль в черный
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HidePassword_MouseEnter(object sender, EventArgs e)
        {
            hidePassword.ForeColor = Color.White;
        }
        /// <summary>
        /// Подсвечивания лебла скрыть пароль в белый
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void HidePassword_MouseLeave(object sender, EventArgs e)
        {
            hidePassword.ForeColor = Color.Black;
        }
        void Clear_MouseEnter(object sender, EventArgs e)
        {
            clear.ForeColor = Color.White;
        }

        void clear_MouseLeave(object sender, EventArgs e)
        {
            clear.ForeColor = Color.Black;
        }
        #endregion

        #region функция проверки интернета

        bool AvailabilityChanged_bool()
        {
            try
            {
                if (new Ping().Send("yandex.ru").Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(@"1.Отсутствует подключение к Интернету. Проверьте настройки сети и повторите попытку",
                        "Сеть недоступна");
            }
            return false;
        }
        #endregion
    }
}
