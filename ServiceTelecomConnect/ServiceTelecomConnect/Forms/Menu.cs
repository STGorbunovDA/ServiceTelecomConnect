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
        private readonly cheakUser _user;

        public Menu(cheakUser user)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            _user = user;
            IsAdmin();

            lbL_TutorialEngineers.ForeColor = Color.FromArgb(56, 56, 56);
            lbL_section_foreman.ForeColor = Color.FromArgb(56, 56, 56);
            lbL_сomparison.ForeColor = Color.FromArgb(56, 56, 56);
        }
        void IsAdmin()
        {
            if (_user.IsAdmin == "Дирекция связи")
            {
                lbL_сomparison.Enabled = false;
                lbL_TutorialEngineers.Enabled = false;
                lbL_section_foreman.Enabled = true;
            }

            if (_user.IsAdmin == "Admin")
            {
                picB_setting.Visible = true;
                lbL_director.Visible = true;
            }
            if (_user.IsAdmin == "Руководитель")
            {
                lbL_director.Visible = true;
            }
        }

        void Label_baza_Click(object sender, EventArgs e)
        {
            using (ST_WorkForm sT_WorkForm = new ST_WorkForm(_user))
            {
                this.Hide();
                sT_WorkForm.ShowDialog();
            }
        }

        void Label_section_foreman_MouseEnter(object sender, EventArgs e)
        {

            lbL_section_foreman.ForeColor = Color.White;
        }
        void Label_section_foreman_MouseLeave(object sender, EventArgs e)
        {
            lbL_section_foreman.ForeColor = Color.Black;
        }
        void Label_TutorialEngineers_Click(object sender, EventArgs e)
        {
            using (TutorialForm tutorialForm = new TutorialForm())
            {
                this.Hide();
                tutorialForm.ShowDialog();
                this.Show();
            }
        }
        void Label_TutorialEngineers_MouseEnter(object sender, EventArgs e)
        {
            lbL_TutorialEngineers.ForeColor = Color.White;
        }
        void Label_TutorialEngineers_MouseLeave(object sender, EventArgs e)
        {
            lbL_TutorialEngineers.ForeColor = Color.Black;
        }
        void Label1_Click(object sender, EventArgs e)
        {
            using (ComparisonForm comparisonForm = new ComparisonForm())
            {
                this.Hide();
                comparisonForm.ShowDialog();
                this.Show();
            }
        }
        void Label_сomparison_MouseEnter(object sender, EventArgs e)
        {
            lbL_сomparison.ForeColor = Color.White;
        }
        void Label_сomparison_MouseLeave(object sender, EventArgs e)
        {
            lbL_сomparison.ForeColor = Color.Black;
        }

        #region открываем форму управления правами доступа user's
        void PictureBox1_setting_Click(object sender, EventArgs e)
        {
            using (Setting_user setting_User = new Setting_user())
            {
                this.Hide();
                setting_User.ShowDialog();
                this.Show();
            }
        }
        #endregion

        void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(1);
        }
        void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = FormClose.GetInstance.FClose();
        }

        void LbL_director_Click(object sender, EventArgs e)
        {
            using (DirectorForm directorForm = new DirectorForm())
            {
                this.Hide();
                directorForm.ShowDialog();
                this.Show();
            }
        }

        void LbL_director_MouseEnter(object sender, EventArgs e)
        {
            lbL_director.ForeColor = Color.White;
        }

        void LbL_director_MouseLeave(object sender, EventArgs e)
        {
            lbL_director.ForeColor = Color.Black;
        }

        void Menu_Load(object sender, EventArgs e)
        {
            if(_user.IsAdmin != "Admin")
            {
                string querystring = $"SELECT attorney FROM сharacteristics_вrigade WHERE section_foreman_FIO = '{_user.Login}'";

                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        if (table.Rows.Count == 1)
                        {
                            lbL_сomparison.Enabled = false;
                        }
                        else
                        {
                            string querystring2 = $"SELECT attorney FROM сharacteristics_вrigade WHERE engineers_FIO = '{_user.Login}'";
                            using (MySqlCommand command2 = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.OpenConnection();
                                using (MySqlDataAdapter adapter2 = new MySqlDataAdapter(command2))
                                {
                                    DataTable table2 = new DataTable();

                                    adapter2.Fill(table2);

                                    if (table2.Rows.Count == 1)
                                    {
                                        lbL_сomparison.Enabled = false;
                                    }
                                    else
                                    {
                                        lbL_TutorialEngineers.Enabled = false;
                                        lbL_section_foreman.Enabled = false;
                                        lbL_сomparison.Enabled = false;
                                        MessageBox.Show("Сообщи руководителю что-бы он выдал тебе доверенность!");
                                    }
                                }
                            }
                        }
                    }
                }
            }   
        }
    }
}
