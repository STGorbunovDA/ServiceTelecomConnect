﻿using System;
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

            label_TutorialEngineers.ForeColor = Color.FromArgb(56, 56, 56);
            label_section_foreman.ForeColor = Color.FromArgb(56, 56, 56);
            label_сomparison.ForeColor = Color.FromArgb(56, 56, 56);
        }
        void IsAdmin()
        {
            if (_user.IsAdmin == "Дирекция связи")
            {
                label_сomparison.Enabled = false;
                label_TutorialEngineers.Enabled = false;
                label_section_foreman.Enabled = true;
            }

            if (_user.IsAdmin == "Инженер")
            {
                //label_section_foreman.Enabled = false;
                label_сomparison.Enabled = false;
            }
            if (_user.IsAdmin == "Начальник участка")
            {
                label_сomparison.Enabled = false;
            }

            if (_user.IsAdmin == "Куратор")
            {
                label_TutorialEngineers.Enabled = false;
            }

            if (_user.IsAdmin == "Руководитель")
            {
                string Mesage2;
                Mesage2 = "Вы руководитель, а значит у Вас полный доступ к конфигурации!";

                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return;
                }
            }

            if (_user.IsAdmin == "Admin")
            {
                pictureBox1_setting.Visible = true;
            }

            if (!(_user.IsAdmin == "Admin" || _user.IsAdmin == "Руководитель" || _user.IsAdmin == "Куратор" || _user.IsAdmin == "Начальник участка" || _user.IsAdmin == "Инженер" || _user.IsAdmin == "Дирекция связи"))
            {
                label_TutorialEngineers.Enabled = false;
                label_section_foreman.Enabled = false;
                label_сomparison.Enabled = false;
            }

        }

        void label_baza_Click(object sender, EventArgs e)
        {
            ST_WorkForm sT_WorkForm = new ST_WorkForm(_user);
            this.Hide();   
            sT_WorkForm.ShowDialog();
        }

        void label_section_foreman_MouseEnter(object sender, EventArgs e)
        {

            label_section_foreman.ForeColor = Color.White;
        }
        void label_section_foreman_MouseLeave(object sender, EventArgs e)
        {
            label_section_foreman.ForeColor = Color.Black;
        }
        void label_TutorialEngineers_Click(object sender, EventArgs e)
        {
            TutorialForm tutorialForm = new TutorialForm();
            this.Hide();
            tutorialForm.ShowDialog();
            this.Show();
        }
        void label_TutorialEngineers_MouseEnter(object sender, EventArgs e)
        {       
            label_TutorialEngineers.ForeColor = Color.White;
        }
        void label_TutorialEngineers_MouseLeave(object sender, EventArgs e)
        {
            label_TutorialEngineers.ForeColor = Color.Black;
        }
        void label1_Click(object sender, EventArgs e)
        {
            ComparisonForm comparisonForm = new ComparisonForm();
            this.Hide();
            comparisonForm.ShowDialog();
            this.Show();
        }
        void label_сomparison_MouseEnter(object sender, EventArgs e)
        {
            label_сomparison.ForeColor = Color.White;
        }
        void label_сomparison_MouseLeave(object sender, EventArgs e)
        {
            label_сomparison.ForeColor = Color.Black;
        }

        #region открываем форму управления правами доступа user's
        void pictureBox1_setting_Click(object sender, EventArgs e)
        {
            Setting_user setting_User = new Setting_user();
            this.Hide();
            setting_User.ShowDialog();
            this.Show();
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
