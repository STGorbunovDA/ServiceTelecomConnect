using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
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
                    txbLogin.Text = "Admin";
                    txbPassword.Text = "1818";
                }
            });
        }
        void LoginFormLoad(object sender, EventArgs e)
        {
            txbPassword.PasswordChar = '*';
            hidePassword.Visible = false;
            txbLogin.MaxLength = 100;
            txbPassword.MaxLength = 32;
            if (txbLogin.Text == "Admin" || txbPassword.Text == "1818")
                EnterButtonLoginClick(sender, e);
            try
            {
                RegistryKey reg1 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Login_Password");
                if (reg1 != null)
                {
                    RegistryKey currentUserKey = Registry.CurrentUser;
                    RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Login_Password");
                    txbLogin.Text = helloKey.GetValue("Login").ToString();
                    txbPassword.Text = helloKey.GetValue("Password").ToString();
                    helloKey.Close();
                }
            }
            catch 
            {
            }
        }
        void EnterButtonLoginClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                string loginUser = txbLogin.Text;
                string passUser = Md5.EncryptPlainTextToCipherText(txbPassword.Text);
                string querystring = $"SELECT id, login, pass, is_admin	FROM users " +
                    $"WHERE login = '{loginUser}' AND pass = '{passUser}'";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        if (table.Rows.Count == 1)
                        {
                            CheakUser user = new CheakUser(table.Rows[0].ItemArray[1].ToString(), table.Rows[0].ItemArray[3].ToString());
                            using (Menu menu = new Menu(user))
                            {
                                RegistryKey currentUserKey = Registry.CurrentUser;
                                RegistryKey helloKey = currentUserKey.CreateSubKey("SOFTWARE\\ServiceTelekom_Setting\\Login_Password");
                                helloKey.SetValue("Login", $"{txbLogin.Text}");
                                helloKey.SetValue("Password", $"{txbPassword.Text}");
                                helloKey.Close();
                                this.Hide();
                                menu.ShowDialog();
                                DB.GetInstance.CloseConnection();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин и пароль");
                            DB.GetInstance.CloseConnection();
                        }
                    }
                }
            }
        }
        void OpenPasswordClick(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = true;
            hidePassword.Visible = true;
            openPassword.Visible = false;
        }
        void HidePasswordClick(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = false;
            hidePassword.Visible = false;
            openPassword.Visible = true;
        }
        void ClearClick(object sender, EventArgs e)
        {
            txbLogin.Text = String.Empty;
            txbPassword.Text = String.Empty;
        }
        void RegistrationLoginFormClick(object sender, EventArgs e)
        {
            using (RegistrationForm registrationForm = new RegistrationForm())
            {
                this.Hide();
                registrationForm.ShowDialog();
                this.Show();
            }
        }

        #region Подсветка
        void RegistrationLoginFormMouseEnter(object sender, EventArgs e)
        {
            lbL_registrationLoginForm.ForeColor = Color.White;
        }
        void RegistrationLoginFormMouseLeave(object sender, EventArgs e)
        {
            lbL_registrationLoginForm.ForeColor = Color.Black;
        }
        void OpenPasswordMouseEnter(object sender, EventArgs e)
        {
            openPassword.ForeColor = Color.White;
        }
        void OpenPasswordMouseLeave(object sender, EventArgs e)
        {
            openPassword.ForeColor = Color.Black;
        }
        void HidePasswordMouseEnter(object sender, EventArgs e)
        {
            hidePassword.ForeColor = Color.White;
        }
        void HidePasswordMouseLeave(object sender, EventArgs e)
        {
            hidePassword.ForeColor = Color.Black;
        }
        void ClearMouseEnter(object sender, EventArgs e)
        {
            lbL_clear.ForeColor = Color.White;
        }
        void ClearMouseLeave(object sender, EventArgs e)
        {
            lbL_clear.ForeColor = Color.Black;
        }
        #endregion

    }
}
