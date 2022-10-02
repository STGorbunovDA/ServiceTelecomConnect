using System;
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

            if (_user.IsAdmin == "Инженер")
            {
                //label_section_foreman.Enabled = false;
                lbL_сomparison.Enabled = false;
            }
            if (_user.IsAdmin == "Начальник участка")
            {
                lbL_сomparison.Enabled = false;
            }

            if (_user.IsAdmin == "Куратор")
            {
                lbL_TutorialEngineers.Enabled = false;
            }

            if (_user.IsAdmin == "Admin")
            {
                picB_setting.Visible = true;
            }

            if (!(_user.IsAdmin == "Admin" || _user.IsAdmin == "Руководитель" || _user.IsAdmin == "Куратор" || _user.IsAdmin == "Начальник участка" || _user.IsAdmin == "Инженер" || _user.IsAdmin == "Дирекция связи"))
            {
                lbL_TutorialEngineers.Enabled = false;
                lbL_section_foreman.Enabled = false;
                lbL_сomparison.Enabled = false;
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
    }
}
