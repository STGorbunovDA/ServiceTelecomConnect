using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ServiceTelecomConnect
{
    enum RowStateTutorial
    {
        Existed,
        New,
        Modifield,
        ModifieldNew,
        Deleted
    }

    public partial class TutorialForm : Form
    {
        DB dB = new DB();

        int selectedRow;

        public TutorialForm()
        {
            InitializeComponent();
            FunctionTextBox();
        }

        private void CreateColums()
        {
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns.Add("author", "Автор текста");
            dataGridView1.Columns.Add("actions", "Что нужно делать");
            dataGridView1.Columns.Add("info", "Информация");
            dataGridView1.Columns.Add("problem", "Неисправность");
            dataGridView1.Columns.Add("modelRST", "Модель радиостанции");
            dataGridView1.Columns.Add("id", "№");

            dataGridView1.Columns[0].Visible = false;           
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[6].Visible = false;
        }

        private void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(RowState.ModifieldNew, record.GetString(5), record.GetString(4), record.GetString(3), record.GetString(2), record.GetString(1), record.GetInt32(0));
        }

        private void RefreshDataGrid(DataGridView dgw)
        {
            try
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                //dataGridView1.Columns[1].DefaultCellStyle.FormatProvider = myCulture;
                Thread.CurrentThread.CurrentCulture = myCulture;

                dgw.Rows.Clear();

                string queryString = $"select * from problem";

                MySqlCommand command = new MySqlCommand(queryString, dB.GetConnection());

                dB.openConnection();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ReedSingleRow(dgw, reader);
                }
                reader.Close();
            }
            catch (MySqlException)
            {
                string Mesage2;
                Mesage2 = "Что-то полшло не так, мы обязательно разберёмся";

                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return;
                }
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                
                textBox_author.Text = row.Cells[1].Value.ToString();
                textBox_actions.Text = row.Cells[2].Value.ToString();
                textBox_info.Text = row.Cells[3].Value.ToString();
                textBox_problem.Text = row.Cells[4].Value.ToString();
                textBox_model.Text = row.Cells[5].Value.ToString();
          
                textBox_author.ReadOnly = true;
                textBox_model.ReadOnly = true;
            }      
        }

        async private void TutorialForm_Load(object sender, EventArgs e)
        {
            CreateColums();
            RefreshDataGrid(dataGridView1);
            UpdateCountProblemRST();

            for (Opacity = 0; Opacity < 1; Opacity += 0.02)
            {
                await Task.Delay(1);
            }
        }

        private void ClearFields()
        {
            textBox_actions.Text = "";
            textBox_problem.Text = "";
            textBox_model.Text = "";
            textBox_info.Text = "";
            textBox_author.Text = "";
        }

        private void pictureBox1_clear_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите очистить все введенные вами поля?";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            ClearFields();    
        }

        private void Search(DataGridView dgw)
        {
            try
            {
                dgw.Rows.Clear();

                string searchString = $"select * from problem where concat (id, modelRST, problem, info, actions) like '%" + textBox_search.Text + "%'";

                MySqlCommand command = new MySqlCommand(searchString, dB.GetConnection());

                dB.openConnection();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ReedSingleRow(dgw, reader);
                }
                reader.Close();
            }
            catch (MySqlException)
            {
                string Mesage2;
                Mesage2 = "Что-то полшло не так, мы обязательно разберёмся";

                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return;
                }
            }
        }

        private void Change()//индекс!!!
        {
            string Mesage;
            Mesage = "Вы действительно хотите изменить выделенную запись";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            var selectedRowBndex = dataGridView1.CurrentCell.RowIndex;
            string id = null;
            string author = textBox_author.Text;
            var actions = textBox_actions.Text;
            var info = textBox_info.Text;
            var problem = textBox_problem.Text;
            var model = textBox_model.Text;
            
            if (dataGridView1.Rows[selectedRowBndex].Cells[1].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRow].SetValues(id, author, info, actions, problem, model);
                dataGridView1.Rows[selectedRow].Cells[0].Value = RowState.Modifield;
            }     
        }

        private void deleteRowСell()
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                //dataGridView1.Rows.RemoveAt(row.Index); //метод удаления от с#
                dataGridView1.Rows[row.Index].Cells[0].Value = RowState.Deleted;
            }
        }

        private void UpdateNew() // индекс
        {
            try
            {
                dB.openConnection();

                for (int index = 0; index < dataGridView1.Rows.Count; index++)
                {
                    var rowState = (RowState)dataGridView1.Rows[index].Cells[0].Value;//проверить индекс

                    if (rowState == RowState.Existed)
                    {
                        continue;
                    }

                    if (rowState == RowState.Deleted)
                    {
                        var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[6].Value);
                        var deleteQuery = $"delete from problem where id = {id}";

                        MySqlCommand command = new MySqlCommand(deleteQuery, dB.GetConnection());

                        command.ExecuteNonQuery();
                    }
                    if (rowState == RowState.Modifield)
                    {
                        //var author = dataGridView1.Rows[index].Cells[1].Value.ToString();
                        var info = dataGridView1.Rows[index].Cells[2].Value.ToString();
                        var actions = dataGridView1.Rows[index].Cells[3].Value.ToString();
                        var problem = dataGridView1.Rows[index].Cells[4].Value.ToString();
                        var model = dataGridView1.Rows[index].Cells[5].Value.ToString();
                        var id = dataGridView1.Rows[index].Cells[6].Value.ToString();

                        var changeQuery = $"update problem set  actions = '{actions}', info = '{info}', problem = '{problem}', modelRST = '{model}'  where id = '{id}'";
                        /// нужно как-то перевести , в точки
                        MySqlCommand command = new MySqlCommand(changeQuery, dB.GetConnection());

                        command.ExecuteNonQuery();
                    }
                }
                dB.closeConnection();
            }
            catch (MySqlException)
            {
                string Mesage2;
                Mesage2 = "Что-то полшло не так, мы обязательно разберёмся";

                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return;
                }
            }
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
            UpdateCountProblemRST();
        }

        private void pictureBox2_update_Click(object sender, EventArgs e)
        {
            Search(dataGridView1);
            ClearFields();
            UpdateCountProblemRST();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = "Вы действительно хотите удалить выделенную запись";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }

            deleteRowСell();
            UpdateNew();
            RefreshDataGrid(dataGridView1);
            UpdateCountProblemRST();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            UpdateNew();
            RefreshDataGrid(dataGridView1);
            UpdateCountProblemRST();
        }

        private void button_new_add_problem_rst_form_Click(object sender, EventArgs e)
        {

            AddToProblemRST addToProblemRST = new AddToProblemRST();
            this.Hide();
            addToProblemRST.ShowDialog();
            this.Show();
            RefreshDataGrid(dataGridView1);           
            UpdateCountProblemRST();

        }

        private void button_change_Click(object sender, EventArgs e)
        {
            Change();
            UpdateNew();
            RefreshDataGrid(dataGridView1);           
        }

        /// <summary>
        /// Для текстбоксов и их отображения прокрутки, размера
        /// </summary>
        private void FunctionTextBox()
        {       
            StartPosition = FormStartPosition.CenterScreen;
            this.textBox_actions.AutoSize = false;
            this.textBox_info.AutoSize = false;
           
            this.textBox_actions.Size = new Size(this.textBox_actions.Size.Width, 120);
            this.textBox_info.Size = new Size(this.textBox_info.Size.Width, 120);
        }

        /// <summary>
        /// Условие при котором картинка появится при нажатии на зелёную кнопку два раза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (textBox_problem.Text == "Выходной ВЧ-транзистор Q3501" && textBox_model.Text == "Motorola GP-340")
            { 
                Form imgFrm = new Form();
                imgFrm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
                imgFrm.MaximizeBox = false;
                imgFrm.MinimizeBox = false;
                pictureBox2.Image = new Bitmap(@"RST_Image\transistor.jpg", true);
                imgFrm.Width = pictureBox2.Image.Width;//questImage - это pictureBox
                imgFrm.Height = pictureBox2.Image.Height;
                imgFrm.BackgroundImage = pictureBox2.Image;
                imgFrm.StartPosition = FormStartPosition.CenterScreen;
                imgFrm.Show();
                pictureBox2.Visible = false;
            }
        }

        /// <summary>
        /// метод убирает пикчурибокс
        /// </summary>

        private void button_info_model_Click(object sender, EventArgs e)
        {
            if(textBox_model.Text == "Motorola GP-340")
            {
                string Mesage;
                Mesage = " Перед началом проверки радиостанции необходимо провести: очистку корпуса, " +
                    "убрать металлическую стружку из динамика, визуально просмотреть корпус на сквозные " +
                    "трещины, сколы, батарейные контакты, уплотнителя батарейного контакта, " +
                    "а также ручку регулятора громкости и переключения каналов";

                if (MessageBox.Show(Mesage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return;
                }
            }
        }
        private void UpdateCountProblemRST()
        {
            int numRows = dataGridView1.Rows.Count;
            label_count.Text = numRows.ToString();
        }
    }
}
