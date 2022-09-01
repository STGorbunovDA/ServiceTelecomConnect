using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    public partial class AddToProblemRST : Form
    {
        public AddToProblemRST()
        {
            InitializeComponent();
            FunctionTextBox();
        }

        DB dB = new DB();
        private void FunctionTextBox()
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.textBox_actions.AutoSize = false;
            this.textBox_info.AutoSize = false;
            this.textBox_actions.Size = new Size(this.textBox_actions.Size.Width, 150);
            this.textBox_info.Size = new Size(this.textBox_info.Size.Width, 120);
        }

        private void button_new_add_problem_rst_form_Click(object sender, EventArgs e)
        {
            dB.openConnection();
            try
            {
                string Mesage;
                Mesage = "Вы действительно хотите добавить неисправность соответсвующей модели радиостанции?";

                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                var author = textBox_author.Text;

                if (author == "")
                {
                    string Mesage2;

                    Mesage2 = "Вы не заполнили поле \"Автор записи\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                var model = comboBox_model.Text;

                if (model == "")
                {
                    string Mesage2;

                    Mesage2 = "Вы не добавили модель!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                var problem = textBox_problem.Text;

                if (problem == "")
                {
                    string Mesage2;

                    Mesage2 = "Вы не заполнили поле \"Неисправность:\"!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                var info = textBox_info.Text;

                if (info == "")
                {
                    string Mesage2;

                    Mesage2 = "Вы не добавили описание неисправности!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }

                var actions = textBox_actions.Text;

                if (actions == "")
                {
                    string Mesage2;

                    Mesage2 = "Вы не заполнили поле \"Что нужно делать\"";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }           

                if (!(model == "") && !(problem == "") && !(info == "") && !(model == "") && !(actions == "") && !(author == ""))
                {
                    var addQuery = $"insert into problem (modelRST, problem, info, actions, author ) values ('{model}', '{problem}','{info}', '{actions}', '{author}')";

                    MySqlCommand command = new MySqlCommand(addQuery, dB.GetConnection());
                    command.ExecuteNonQuery();

                    MessageBox.Show("Новая запись успешно добавлена!");
                }
                else
                {
                    MessageBox.Show("Вы не заполнили нужные поля со (*)!");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка! Новая запись не добавлена!");
                MessageBox.Show(ex.ToString());
;            }

            dB.closeConnection();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите очистить все введенные вами поля?";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            textBox_author.Text = "";
            comboBox_model.Text = "";
            textBox_problem.Text = "";
            textBox_info.Text = "";
            textBox_actions.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Перед вами форма добавление неисправности радиостанций! Ввведите в поля соответсвующие неисправности";

            if (MessageBox.Show(Mesage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                return;
            }
        }
    }
}
