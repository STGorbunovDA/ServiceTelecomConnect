﻿using MySql.Data.MySqlClient;
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
        void Clear_MouseEnter(object sender, EventArgs e)
        {
            clear.ForeColor = Color.White;
        }
        void Clear_MouseLeave(object sender, EventArgs e)
        {
            clear.ForeColor = Color.Black;
        }
        void Clear_Click(object sender, EventArgs e)
        {
            loginField.Text = "";
            passField.Text = "";
            comboBox_post.Text = "";
        }
        void RegistrationForm_Load(object sender, EventArgs e)
        {
            passField.PasswordChar = '*';
            hidePassword.Visible = false;
            loginField.MaxLength = 100;
            passField.MaxLength = 32;
        }
        void OpenPassword_MouseEnter(object sender, EventArgs e)
        {
            openPassword.ForeColor = Color.White;
        }
        void OpenPassword_MouseLeave(object sender, EventArgs e)
        {
            openPassword.ForeColor = Color.Black;
        }
        void HidePassword_MouseEnter(object sender, EventArgs e)
        {
            hidePassword.ForeColor = Color.White;
        }
        void HidePassword_MouseLeave(object sender, EventArgs e)
        {
            hidePassword.ForeColor = Color.Black;
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
        void EnterButtonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (Internet_check.AvailabilityChanged_bool())
                {
                    var loginUser = loginField.Text;
                    var passUser = md5.hashPassword(passField.Text);

                    if (!CheackUser(loginUser, passUser))
                    {
                        if (comboBox_post.Text == "Инженер" || comboBox_post.Text == "Начальник участка" || 
                            comboBox_post.Text == "Куратор" || comboBox_post.Text == "Руководитель" || comboBox_post.Text == "Дирекция связи")
                        {
                            string querystring = $"INSERT INTO users (login, pass, is_admin) VALUES ('{loginUser}', '{passUser}', '{comboBox_post.Text}')";

                            using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.openConnection();

                                if (command.ExecuteNonQuery() == 1)
                                {
                                    MessageBox.Show("Аккаунт успешно создан!");
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Аккаунт не создан! Ошибка соединения");
                                }
                                DB.GetInstance.closeConnection();
                            }
                        }

                        if (comboBox_post.Text == "")
                        {
                            string Mesage2;
                            Mesage2 = "Вы не указали должность!";

                            if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                            {
                                return;
                            }

                        }
                    }
                    else
                    {
                        string Mesage2;
                        Mesage2 = "Такой пользователь уже существует!";

                        if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            return;
                        }
                    }
                }

            }
            catch (MySqlException)
            {
                string Mesage2;
                Mesage2 = "Ошибка регистрации!";

                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return;
                }
            }
        }
        Boolean CheackUser(string loginUser, string passUser)
        {
            if (Internet_check.AvailabilityChanged_bool())
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
