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
                    txB_loginField.Text = "Admin";
                    txB_passField.Text = "1818";
                }
            });
        }
        void LoginForm_Load(object sender, EventArgs e)
        {
            txB_passField.PasswordChar = '*';
            lbL_hidePassword.Visible = false;
            txB_loginField.MaxLength = 100;
            txB_passField.MaxLength = 32;
            if (txB_loginField.Text == "Admin" || txB_passField.Text == "1818")
                EnterButtonLogin_Click(sender, e);

        }
        void EnterButtonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (Internet_check.CheackSkyNET())
                {
                    var loginUser = txB_loginField.Text;
                    var passUser = md5.hashPassword(txB_passField.Text);

                    string querystring = $"SELECT id, login, pass, is_admin	FROM users WHERE login = '{loginUser}' AND pass = '{passUser}'";

                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count == 1)
                            {
                                var user = new cheakUser(table.Rows[0].ItemArray[1].ToString(), table.Rows[0].ItemArray[3].ToString());
                                using (Menu menu = new Menu(user))
                                {
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
            catch (Exception)
            {
                MessageBox.Show("Системная ошибка авторизации(EnterButtonLogin_Click)");
            }

        }
        void OpenPassword_Click(object sender, EventArgs e)
        {
            txB_passField.UseSystemPasswordChar = true;
            lbL_hidePassword.Visible = true;
            lbL_openPassword.Visible = false;
        }
        void HidePassword_Click(object sender, EventArgs e)
        {
            txB_passField.UseSystemPasswordChar = false;
            lbL_hidePassword.Visible = false;
            lbL_openPassword.Visible = true;
        }
        void Clear_Click(object sender, EventArgs e)
        {
            txB_loginField.Text = "";
            txB_passField.Text = "";
        }
        
        void RegistrationLoginForm_Click(object sender, EventArgs e)
        {
            using (RegistrationForm registrationForm = new RegistrationForm())
            {
                this.Hide();
                registrationForm.ShowDialog();
                this.Show();
            }
        }

        #region Подсветка
       
        void RegistrationLoginForm_MouseEnter(object sender, EventArgs e)
        {
            lbL_registrationLoginForm.ForeColor = Color.White;
        }
        
        void RegistrationLoginForm_MouseLeave(object sender, EventArgs e)
        {
            lbL_registrationLoginForm.ForeColor = Color.Black;
        }
        
        void OpenPassword_MouseEnter(object sender, EventArgs e)
        {
            lbL_openPassword.ForeColor = Color.White;
        }
        
        void OpenPassword_MouseLeave(object sender, EventArgs e)
        {
            lbL_openPassword.ForeColor = Color.Black;
        }
        
        void HidePassword_MouseEnter(object sender, EventArgs e)
        {
            lbL_hidePassword.ForeColor = Color.White;
        }
        
        void HidePassword_MouseLeave(object sender, EventArgs e)
        {
            lbL_hidePassword.ForeColor = Color.Black;
        }

        void Clear_MouseEnter(object sender, EventArgs e)
        {
            lbL_clear.ForeColor = Color.White;
        }

        void clear_MouseLeave(object sender, EventArgs e)
        {
            lbL_clear.ForeColor = Color.Black;
        }

        #endregion

    }
}
