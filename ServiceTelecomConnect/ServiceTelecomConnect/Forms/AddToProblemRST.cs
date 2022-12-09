using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    public partial class AddToProblemRST : Form
    {
        private readonly cheakUser _user;

        public AddToProblemRST(cheakUser user)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            _user = user;
            cmB_problem.Text = cmB_problem.Items[0].ToString();
        }

        void AddToProblemRST_Load(object sender, EventArgs e)
        {
            lbL_Author.Text = _user.Login;
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT id, model_radiostation_name FROM model_radiostation";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable model_RSR_table = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(model_RSR_table);

                        cmB_model.DataSource = model_RSR_table;
                        cmB_model.ValueMember = "id";
                        cmB_model.DisplayMember = "model_radiostation_name";

                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }

        void Btn_save_add_rst_problem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmB_model.Text))
            {
                MessageBox.Show("Модель не может быть пустой", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmB_model.Select();
                return;
            }
            if (chB_problem_Enable.Checked)
            {
                if (String.IsNullOrEmpty(txB_problem.Text))
                {
                    MessageBox.Show("Опиши неисправность", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_problem.Select();
                    return;
                }
            }
            if (String.IsNullOrEmpty(txB_info.Text))
            {
                MessageBox.Show("Не заполнено поле \"Описание дефекта\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_info.Select();
                return;
            }
            if (String.IsNullOrEmpty(txB_actions.Text))
            {
                MessageBox.Show("Не заполнено поле \"Виды работ по устраненнию дефекта\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_actions.Select();
                return;
            }

            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    var re = new Regex(Environment.NewLine);
                    control.Text = re.Replace(control.Text, " ");
                    control.Text.Trim();
                }
            }
            if (Internet_check.CheackSkyNET())
            {
                var problem = String.Empty;
                var model = cmB_model.Text;
                if (chB_problem_Enable.Checked)
                    problem = txB_problem.Text;
                else problem = cmB_problem.Text;

                var info = txB_info.Text;
                var actions = txB_actions.Text;
                var author = lbL_Author.Text;

                var addQuery = $"INSERT INTO problem_engineer (model, problem, info, actions, author) " +
                    $"VALUES ('{model}', '{problem}', '{info}', '{actions}', '{author}')";

                using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                    MessageBox.Show("Неисправность успешно добавлена!");
                }
            }


        }

        void PictureBox4_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите очистить все введенные вами поля?";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    control.Text = "";
                }
            }
        }

        void ChB_problem_Enable_Click(object sender, EventArgs e)
        {
            if (chB_problem_Enable.Checked)
            {
                cmB_problem.Enabled = false;
                txB_problem.Enabled = true;
                txB_problem.Select();
            }
            else if (!chB_problem_Enable.Checked)
            {
                cmB_problem.Enabled = true;
                txB_problem.Enabled = false;
                txB_problem.Clear();
            }
        }

    }
}
