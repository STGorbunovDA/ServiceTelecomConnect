using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    public partial class ChangeToProblemRST : Form
    {
        private readonly CheakUser _user;
        public ChangeToProblemRST(CheakUser user)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            _user = user;
        }
        void AddToProblemRadiostantionLoad(object sender, EventArgs e)
        {
            lbL_Author.Text = _user.Login;
            cmB_model.Text = cmB_model.Items[0].ToString();
        }
        void BtnChangeRadiostantionProblemClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cmB_model.Text))
            {
                MessageBox.Show("Модель не может быть пустой", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmB_model.Select();
                return;
            }
            if (!chB_problem_Enable.Checked)
            {
                if (String.IsNullOrWhiteSpace(txB_problem.Text))
                {
                    MessageBox.Show("Опиши неисправность", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txB_problem.Select();
                    return;
                }
            }
            if (String.IsNullOrWhiteSpace(txB_info.Text))
            {
                MessageBox.Show("Не заполнено поле \"Описание дефекта\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_info.Select();
                return;
            }
            if (String.IsNullOrWhiteSpace(txB_actions.Text))
            {
                MessageBox.Show("Не заполнено поле \"Виды работ по устраненнию дефекта\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txB_actions.Select();
                return;
            }

            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    Regex re = new Regex(Environment.NewLine);
                    control.Text = re.Replace(control.Text, " ");
                    control.Text.Trim();
                }
            }
            if (InternetCheck.CheackSkyNET())
            {
                string problem = String.Empty;
                string model = cmB_model.Text;
                if (chB_problem_Enable.Checked)
                    problem = cmB_problem.Text;
                else problem = txB_problem.Text;

                string info = txB_info.Text;
                string actions = txB_actions.Text;
                string author = lbL_Author.Text;
                string id = txB_id.Text;

                string addQuery = $"UPDATE problem_engineer SET model = '{model}', problem = '{problem}', " +
                    $"info = '{info}', actions = '{actions}', author = '{author}' WHERE id = '{id}'";

                using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                    MessageBox.Show("Неисправность успешно добавлена!");
                }
            }
        }
        void ClearControlForm(object sender, EventArgs e)
        {
            string Mesage = "Вы действительно хотите очистить все введенные вами поля?";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                    control.Text = String.Empty;
            }
        }
        void ChbProblemEnableClick(object sender, EventArgs e)
        {
            if (chB_problem_Enable.Checked)
            {
                cmB_problem.Enabled = true;
                txB_problem.Enabled = false;
                txB_problem.Select();
            }
            else if (!chB_problem_Enable.Checked)
            {
                cmB_problem.Enabled = false;
                txB_problem.Enabled = true;
                txB_problem.Select();
            }
        }
        void CmbModelClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                string querystring = $"SELECT id, model_radiostation_name FROM model_radiostation";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable modelRadiostantionTable = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(modelRadiostantionTable);
                        cmB_model.DataSource = modelRadiostantionTable;
                        cmB_model.ValueMember = "id";
                        cmB_model.DisplayMember = "model_radiostation_name";
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }
    }
}
