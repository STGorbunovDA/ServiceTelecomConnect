using MySql.Data.MySqlClient;
using ServiceTelecomConnect.Forms;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    public partial class Menu : Form
    {
        private readonly CheakUser _user;
        public Menu(CheakUser user)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            _user = user;
            IsAdmin();
            lbL_TutorialEngineers.ForeColor = Color.FromArgb(56, 56, 56);
            lbL_sectionForeman.ForeColor = Color.FromArgb(56, 56, 56);
            lbL_сomparison.ForeColor = Color.FromArgb(56, 56, 56);
        }
        void IsAdmin()
        {
            if (_user.IsAdmin == "Admin")
            {
                picB_setting.Visible = true;
                lbL_director.Visible = true;
            }
            if (_user.IsAdmin == "Руководитель")
                lbL_director.Visible = true;
        }
        void MenuLoad(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                DateTime Date = DateTime.Now;
                string inputDate = Date.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime dateTimeInput = QuerySettingDataBase.CheacDateTimeInputLogUserDatabase(_user.Login);
                if (Date.ToString("yyyy-MM-dd") != dateTimeInput.ToString("yyyy-MM-dd"))
                {
                    string addQuery = $"INSERT INTO logUserDB (user, dateTimeInput, dateTimeExit) " +
                        $"VALUES ('{_user.Login}', '{inputDate}', '{inputDate}')";
                    using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        DB.GetInstance.CloseConnection();
                    }
                }
                if (_user.IsAdmin == "Admin" || _user.IsAdmin == "Руководитель")
                {

                }
                else if (_user.IsAdmin == "Начальник участка")
                {
                    string querystring = $"SELECT attorney, numberPrintDocument FROM сharacteristics_вrigade " +
                        $"WHERE section_foreman_FIO = '{_user.Login}'";
                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count >= 1)
                                lbL_сomparison.Enabled = false;
                            else
                            {
                                lbL_сomparison.Enabled = false;
                                lbL_TutorialEngineers.Enabled = false;
                                lbL_sectionForeman.Enabled = false;
                                MessageBox.Show("Сообщи руководителю что-бы сформировал тебя в бригаду");
                            }
                        }
                    }
                }
                else if (_user.IsAdmin == "Инженер")
                {
                    string querystring = $"SELECT attorney, numberPrintDocument FROM сharacteristics_вrigade " +
                        $"WHERE engineers_FIO = '{_user.Login}'";
                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count >= 1)
                                lbL_сomparison.Enabled = false;
                            else
                            {
                                lbL_сomparison.Enabled = false;
                                lbL_TutorialEngineers.Enabled = false;
                                lbL_sectionForeman.Enabled = false;
                                MessageBox.Show("Сообщи руководителю что-бы сформировал тебя в бригаду");
                            }
                        }
                    }
                }
                else if (_user.IsAdmin == "Куратор")
                {
                    string querystring = $"SELECT attorney, numberPrintDocument FROM сharacteristics_вrigade " +
                        $"WHERE curator = '{_user.Login}'";
                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count >= 1)
                                lbL_TutorialEngineers.Enabled = false;
                            else
                            {
                                lbL_сomparison.Enabled = false;
                                lbL_TutorialEngineers.Enabled = false;
                                lbL_sectionForeman.Enabled = false;
                                MessageBox.Show("Сообщи руководителю что-бы сформировал тебя в бригаду");
                            }
                        }
                    }
                }
                else if (_user.IsAdmin == "Дирекция связи")
                {
                    string querystring = $"SELECT attorney, numberPrintDocument FROM сharacteristics_вrigade WHERE departmentCommunications = '{_user.Login}'";
                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            if (table.Rows.Count >= 1)
                            {
                                lbL_сomparison.Enabled = false;
                                lbL_TutorialEngineers.Enabled = false;
                                lbL_sectionForeman.Enabled = true;
                            }
                            else
                            {
                                lbL_сomparison.Enabled = false;
                                lbL_TutorialEngineers.Enabled = false;
                                lbL_sectionForeman.Enabled = false;
                                MessageBox.Show("Сообщи руководителю что-бы добавил Вас в бригаду");
                            }
                        }
                    }
                }
                else
                {
                    lbL_сomparison.Enabled = false;
                    lbL_TutorialEngineers.Enabled = false;
                    lbL_sectionForeman.Enabled = false;
                }
            }
        }
        void LblBazaClick(object sender, EventArgs e)
        {
            using (ST_WorkForm sT_WorkForm = new ST_WorkForm(_user))
            {
                this.Hide();
                sT_WorkForm.ShowDialog();
                this.Show();
            }
        }
        void LblSectionForemanMouseEnter(object sender, EventArgs e)
        {
            lbL_sectionForeman.ForeColor = Color.White;
        }
        void LblSectionForemanMouseLeave(object sender, EventArgs e)
        {
            lbL_sectionForeman.ForeColor = Color.Black;
        }
        void LblTutorialEngineersClick(object sender, EventArgs e)
        {
            using (TutorialForm tutorialForm = new TutorialForm(_user))
            {
                this.Hide();
                tutorialForm.ShowDialog();
                this.Show();
            }
        }
        void LblTutorialEngineersMouseEnter(object sender, EventArgs e)
        {
            lbL_TutorialEngineers.ForeColor = Color.White;
        }
        void LblTutorialEngineersMouseLeave(object sender, EventArgs e)
        {
            lbL_TutorialEngineers.ForeColor = Color.Black;
        }
        void LblComparisonFormClick(object sender, EventArgs e)
        {
            using (ComparisonForm comparisonForm = new ComparisonForm(_user))
            {
                this.Hide();
                comparisonForm.ShowDialog();
                this.Show();
            }
        }
        void LblComparisonMouseEnter(object sender, EventArgs e)
        {
            lbL_сomparison.ForeColor = Color.White;
        }
        void LblComparisonMouseLeave(object sender, EventArgs e)
        {
            lbL_сomparison.ForeColor = Color.Black;
        }

        #region открываем форму управления правами доступа user's
        void SettingClick(object sender, EventArgs e)
        {
            using (Setting_user setting_User = new Setting_user())
            {
                this.Hide();
                setting_User.ShowDialog();
                this.Show();
            }
        }
        #endregion

        void MenuFormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
        void MenuFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = FormClose.GetInstance.FClose(_user.Login);
        }
        void LbLDirectorClick(object sender, EventArgs e)
        {
            using (DirectorForm directorForm = new DirectorForm(_user))
            {
                this.Hide();
                directorForm.ShowDialog();
                this.Show();
            }
        }
        void LbLDirectorMouseEnter(object sender, EventArgs e)
        {
            lbL_director.ForeColor = Color.White;
        }
        void LbLDirectorMouseLeave(object sender, EventArgs e)
        {
            lbL_director.ForeColor = Color.Black;
        }
    }
}
