using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    public partial class OverlapForm : Form
    {
        public OverlapForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// для значений к базе данных, по данному статусу будем или удалять или редактировать
        /// </summary>
        enum RowState
        {
            Existed,
            New,
            Modifield,
            ModifieldNew,
            Deleted
        }
        private delegate DialogResult ShowOpenFileDialogInvoker(); // делаг для invoke
        DB dB = new DB();

        /// <summary>
        /// переменная для индекса dataGridView1 
        /// </summary>
        //int selectedRow;

        /// <summary>
        /// заполняем dataGridView1 колонки
        /// </summary>
        private void CreateColums()
        {
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns.Add("price", "Цена технического обслуживания");
            dataGridView1.Columns.Add("dateTOPlan", "Дата закрытия");
            dataGridView1.Columns.Add("networkNumber", "Сетевой номер");
            dataGridView1.Columns.Add("inventoryNumber", "Инвентарный номер");
            dataGridView1.Columns.Add("serialNumber", "Заводской номер");
            dataGridView1.Columns.Add("model", "Модель радиостанции");
            dataGridView1.Columns.Add("location", "Место нахождения");
            dataGridView1.Columns.Add("company", "Предприятие");
            dataGridView1.Columns.Add("poligon", "Полигон");
            dataGridView1.Columns.Add("id", "№");

            dataGridView1.Columns[0].Visible = false;
        }

        /// <summary>
        /// Заполняем колонки значениями из базы данных из RefreshDataGrid
        /// </summary>
        /// <param name="dgw"></param>
        /// <param name="record"></param>
        private void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(RowState.ModifieldNew, Convert.ToDecimal(record.GetString(10)),
                Convert.ToDateTime(record.GetString(8)).ToString("Y"), record.GetString(7), record.GetString(6),
                record.GetString(5), record.GetString(4), record.GetString(3), record.GetString(2), record.GetString(1), record.GetInt32(0));
        }

        /// <summary>
        /// выполняем подключение к базе данных, выполняем команду запроса и передаём данные ReedSingleRow
        /// </summary>
        /// <param name="dgw"></param>
        private void RefreshDataGrid(DataGridView dgw)
        {
            try
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                //dataGridView1.Columns[1].DefaultCellStyle.FormatProvider = myCulture;
                Thread.CurrentThread.CurrentCulture = myCulture;

                dgw.Rows.Clear();

                string queryString = $"SELECT radiostantion. * FROM radiostantion INNER JOIN generallist ON radiostantion.serialNumber=generallist.serialNumber";

                //string queryString = $"select * from radiostantion where (serialNumber) not in (serialNumber from generallist)";

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

        async private void OverlapForm_Load(object sender, EventArgs e)
        {
            for (Opacity = 0; Opacity < 1; Opacity += 0.02)
            {
                await Task.Delay(1);
            }

            CreateColums();
            RefreshDataGrid(dataGridView1);
            UpdateCountRST();
            UpdateSumTOrst();
        }

        private void UpdateCountRST()
        {
            int numRows = dataGridView1.Rows.Count;
            label_count.Text = numRows.ToString();
        }

        private void UpdateSumTOrst()
        {
            decimal sum = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells["price"].Value);
            }
            label_summ.Text = sum.ToString();
        }

        private void pictureBox2_update_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            UpdateCountRST();
            UpdateSumTOrst();
            ClearFields();
        }

        private void ClearFields()
        {
            textBox_search.Text = "";
        }

        private void Search(DataGridView dgw)
        {
            try
            {
                dgw.Rows.Clear();

                string searchString = $"select * from generallist where concat (id, poligon, company, location, model, serialNumber, inventoryNumber, dateTOPlan, price) like '%" + textBox_search.Text + "%'";

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

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            Search(dataGridView1);
            UpdateCountRST();
            UpdateSumTOrst();
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

        private void button_save_in_file_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

            ShowOpenFileDialogInvoker invoker = new ShowOpenFileDialogInvoker(sfd.ShowDialog);

            this.Invoke(invoker);

            if (sfd.FileName != "")
            {
                string filename = sfd.FileName;
                //string text = File.ReadAllText(filename);

                using (StreamWriter sw = new StreamWriter(filename, false, Encoding.Unicode))
                {
                    string note = string.Empty;

                    note += $"Номер\tПолигон\tПредприятие\tМесто нахождения\tМодель\tЗаводской номер\tИнвентарный номер\tСетевой номер\tДата включения\tЦена ТО";
                    sw.WriteLine(note);

                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        for (int i = dataGridView1.Rows[j].Cells.Count - 1; i > 0; i--)
                        {
                            sw.Write(dataGridView1.Rows[j].Cells[i].Value + "\t");
                        }

                        sw.WriteLine();
                    }

                    MessageBox.Show("Файл успешно сохранен");
                }
            }

            else
            {
                string Mesage;
                Mesage = "Файл не сохранён";

                if (MessageBox.Show(Mesage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return;
                }
            }
        }
    }
}
