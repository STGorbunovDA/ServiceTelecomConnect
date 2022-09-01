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
    public partial class ComparisonForm : Form
    {
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
        public ComparisonForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            textBox_dateTOPlan.ReadOnly = true;
            textBox_price.ReadOnly = true;
            textBox_id.Visible = false;
            monthCalendar1.Visible = false;
        }

        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        DB dB = new DB();

        private delegate DialogResult ShowOpenFileDialogInvoker(); // делаг для invoke

        /// <summary>
        /// переменная для индекса dataGridView1 
        /// </summary>
        int selectedRow;

        // <summary>
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
            #region дата грид заполняет с права на лево
            //dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3), record.GetString(4), record.GetDateTime(5), record.GetString(6), RowState.ModifieldNew);
            //dgw.Rows.Add(RowState.ModifieldNew, record.GetString(6), record.GetString(5), record.GetString(4), record.GetString(3), record.GetString(2), record.GetString(1), record.GetInt32(0));
            #endregion
            dgw.Rows.Add(RowState.ModifieldNew, Convert.ToDecimal(record.GetString(9)),
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

                string queryString = $"select * from generallist";

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

        async private void ComparisonForm_Load(object sender, EventArgs e)
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                textBox_price.Text = row.Cells[1].Value.ToString();
                textBox_price.Text = string.Format("{0:#,##0.00}", decimal.Parse(textBox_price.Text));
                textBox_dateTOPlan.Text = row.Cells[2].Value.ToString();
                textBox_networkNumber.Text = row.Cells[3].Value.ToString();
                textBox_inventoryNumber.Text = row.Cells[4].Value.ToString();
                textBox_serialNumber.Text = row.Cells[5].Value.ToString();
                comboBox_model.Text = row.Cells[6].Value.ToString();
                textBox_location.Text = row.Cells[7].Value.ToString();
                textBox_company.Text = row.Cells[8].Value.ToString();
                comboBox_poligon.Text = row.Cells[9].Value.ToString();
                textBox_id.Text = row.Cells[10].Value.ToString();
            }
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
            textBox_id.Text = "";
            comboBox_poligon.Text = "";
            textBox_company.Text = "";
            textBox_location.Text = "";
            comboBox_model.Text = "";
            textBox_serialNumber.Text = "";
            textBox_inventoryNumber.Text = "";
            textBox_networkNumber.Text = "";
            textBox_dateTOPlan.Text = "";
            textBox_price.Text = "";
            textBox_search.Text = "";
        }

        /// <summary>
        /// метод поиска по базе данных, подключение к базе, выполнение запроса так-же внутри  вызываем метод ReedSingleRow для вывода данных из базы
        /// </summary>
        /// <param name="dgw"></param>
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

        private void Change()
        {
            string Mesage;
            Mesage = "Вы действительно хотите изменить выделенную запись";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            var selectedRowBndex = dataGridView1.CurrentCell.RowIndex;
            var id = textBox_id.Text;
            var poligon = comboBox_poligon.Text;
            var company = textBox_company.Text;
            var location = textBox_location.Text;
            var model = comboBox_model.Text;
            var serialNumber = textBox_serialNumber.Text;
            var inventoryNumber = textBox_inventoryNumber.Text;
            var networkNumber = textBox_networkNumber.Text;
            var dateTOPlan = textBox_dateTOPlan.Text;
            var price = textBox_price.Text;

            if (dataGridView1.Rows[selectedRowBndex].Cells[10].Value.ToString() != string.Empty)
            {
                dataGridView1.Rows[selectedRow].SetValues(id, price, dateTOPlan, networkNumber, inventoryNumber, serialNumber, model, location, company, poligon);
                dataGridView1.Rows[selectedRow].Cells[0].Value = RowState.Modifield;
            }
            else
            {
                string Mesage2;
                Mesage2 = "Не получилось изменить выделенную запись";

                if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
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

        private void button_change_Click(object sender, EventArgs e)
        {
            Change();
            UpdateNew();
            RefreshDataGrid(dataGridView1);
        }

        private void deleteRowСell()
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                //dataGridView1.Rows.RemoveAt(row.Index); //метод удаления от с#
                dataGridView1.Rows[row.Index].Cells[0].Value = RowState.Deleted;
            }
        }

        /// <summary>
        /// метод обновления базы данных исходя из статуса присваивания колонке RowState значений или удаление или редактирования
        /// </summary>
        private void UpdateNew() /// гребанный децимал
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
                        var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[10].Value);
                        var deleteQuery = $"delete from generallist where id = {id}";

                        MySqlCommand command = new MySqlCommand(deleteQuery, dB.GetConnection());

                        command.ExecuteNonQuery();
                    }
                    if (rowState == RowState.Modifield)
                    {
                        var price = dataGridView1.Rows[index].Cells[1].Value.ToString();
                        var dateTOPlan = dataGridView1.Rows[index].Cells[2].Value.ToString();
                        var networkNumber = dataGridView1.Rows[index].Cells[3].Value.ToString();
                        var inventoryNumber = dataGridView1.Rows[index].Cells[4].Value.ToString();
                        var serialNumber = dataGridView1.Rows[index].Cells[5].Value.ToString();
                        var model = dataGridView1.Rows[index].Cells[6].Value.ToString();
                        var location = dataGridView1.Rows[index].Cells[7].Value.ToString();
                        var company = dataGridView1.Rows[index].Cells[8].Value.ToString();
                        var poligon = dataGridView1.Rows[index].Cells[9].Value.ToString();
                        var id = dataGridView1.Rows[index].Cells[10].Value.ToString();

                        var changeQuery = $"update generallist set poligon = '{poligon}', company = '{company}', location = '{location}',model = '{model}', serialNumber = '{serialNumber}', inventoryNumber = '{inventoryNumber}'," +
                            $"networkNumber = '{networkNumber}', dateTOPlan = '{dateTOPlan}', price = '{Convert.ToDecimal(price)}'  where id = '{id}'";
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
            UpdateCountRST();
            UpdateSumTOrst();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            UpdateNew();
            RefreshDataGrid(dataGridView1);
            UpdateCountRST();
            UpdateSumTOrst();
        }

        async void button_adding_insert_Click_Async(object sender, EventArgs e)
        {
            await Task.Run(() => AddFileRST());
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            textBox_dateTOPlan.Text = e.End.ToString("dd/MM/yyyy");
            monthCalendar1.Visible = false;
        }

        private void textBox_dateTOPlan_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = true;
        }

        private void textBox_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            char decimalSeparatorChar = Convert.ToChar(Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator);
            if (ch == decimalSeparatorChar && textBox_price.Text.IndexOf(decimalSeparatorChar) != -1)
            {
                e.Handled = true;
                return;
            }

            if (!Char.IsDigit(ch) && ch != 8 && ch != decimalSeparatorChar)
            {
                e.Handled = true;
            }
        }

        private void comboBox_model_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_model.Text == "Icom IC-F3GT" || comboBox_model.Text == "Icom IC-F11" || comboBox_model.Text == "Icom IC-F16" ||
                comboBox_model.Text == "Icom IC-F3GS" || comboBox_model.Text == "Motorola P040" || comboBox_model.Text == "Motorola P080" ||
                comboBox_model.Text == "Motorola GP-300" || comboBox_model.Text == "Motorola GP-320" || comboBox_model.Text == "Motorola GP-340" ||
                comboBox_model.Text == "Motorola GP-360" || comboBox_model.Text == "Альтавия-301М" || comboBox_model.Text == "Comrade R5" ||
                comboBox_model.Text == "Гранит Р33П-1" || comboBox_model.Text == "Гранит Р-43" || comboBox_model.Text == "Радий-301" ||
                comboBox_model.Text == "Kenwood ТК-2107" || comboBox_model.Text == "Vertex - 261")
            {
                textBox_price.Text = "1411.18";
            }
            else
            {
                textBox_price.Text = "1919.57";
            }
        }

        private void AddFileRST()
        {
            OpenFileDialog openFile = new OpenFileDialog();

            ShowOpenFileDialogInvoker invoker = new ShowOpenFileDialogInvoker(openFile.ShowDialog);

            this.Invoke(invoker);

            if (openFile.FileName != "")
            {
                string filename = openFile.FileName;
                string text = File.ReadAllText(filename);

                var lineNumber = 0;

                using (var command = new MySqlConnection($"server = localhost; port = 3306; username = root; password = root; database = userlogin"))
                {
                    command.Open();
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();

                            if (lineNumber != 0)
                            {
                                var values = line.Split(';');

                                if (cheacSerialNumber(values[4]) == false)
                                {                                   
                                    var mySql = $"insert into generallist (poligon, company, location, model, serialNumber, inventoryNumber, networkNumber, dateTOPlan, price) " +
                                           $"values ('{values[0]}', '{values[1]}', '{values[2]}', '{values[3]}', '{values[4]}', '{values[5]}', '{values[6]}', '{values[7]}', '{values[8]}')";

                                    var cmd = new MySqlCommand();
                                    cmd.CommandText = mySql;
                                    cmd.CommandType = System.Data.CommandType.Text;
                                    cmd.Connection = command;
                                    cmd.ExecuteNonQuery();

                                }
                                else
                                {
                                    string Mesage;

                                    Mesage = $"Радиостанции были добавлены до найденой с номером: {values[4]}";

                                    if (MessageBox.Show(Mesage, "Радиостанции добавленны не полностью!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                                    {
                                        return;
                                    }
                                }

                            }
                            lineNumber++;
                        }
                        MessageBox.Show("Радиостанции успешно добавлены!");
                    }
                    command.Close();
                }
            }
            else
            {
                string Mesage;
                Mesage = "Вы не выбрали файл .csv который нужно добавить";

                if (MessageBox.Show(Mesage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return;
                }
            }
        }

        private Boolean cheacSerialNumber(string serialNumber)
        {
            string querystring = $"SELECT * FROM generallist WHERE serialNumber = '{serialNumber}'";

            MySqlCommand command = new MySqlCommand(querystring, dB.GetConnection());

            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

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

        private void button_overlap_Click(object sender, EventArgs e)
        {
            
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

        private void processKbdCtrlShortcuts(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (e.KeyData == (Keys.C | Keys.Control))
            {
                t.Copy();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.X | Keys.Control))
            {
                t.Cut();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.V | Keys.Control))
            {
                t.Paste();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.A | Keys.Control))
            {
                t.SelectAll();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Z | Keys.Control))
            {
                t.Undo();
                e.Handled = true;
            }
        }

        private void textBox_company_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        private void textBox_company_Click(object sender, EventArgs e)
        {
            textBox_company.MaxLength = 25;
        }

        private void textBox_company_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);

            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-')
            {
                e.Handled = true;
            }
        }

        private void textBox_location_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        private void textBox_location_Click(object sender, EventArgs e)
        {
            textBox_location.MaxLength = 25;
        }

        private void textBox_location_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if ((ch < 'А' || ch > 'Я') && (ch < 'а' || ch > 'я') && (ch <= 47 || ch >= 58) && ch != '\b' && ch != '-' && ch != '.' && ch != ' ')
            {
                e.Handled = true;
            }
        }

        private void textBox_inventoryNumber_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        private void textBox_inventoryNumber_Click(object sender, EventArgs e)
        {
            textBox_inventoryNumber.MaxLength = 40;
        }

        private void textBox_networkNumber_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        private void textBox_networkNumber_Click(object sender, EventArgs e)
        {
            textBox_networkNumber.MaxLength = 40;
        }

        private void textBox_networkNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == ' '
                || e.KeyChar == '/')
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox_serialNumber_KeyUp(object sender, KeyEventArgs e)
        {
            processKbdCtrlShortcuts(sender, e);
        }

        private void textBox_serialNumber_Click(object sender, EventArgs e)
        {
            if (comboBox_model.Text == "Icom IC-F3GT" || comboBox_model.Text == "Icom IC-F11")
            {
                textBox_serialNumber.MaxLength = 7;
            }

            if (comboBox_model.Text == "Icom IC-F16" || comboBox_model.Text == "Icom IC-F3GS" || comboBox_model.Text == "Гранит Р33П-1" ||
                comboBox_model.Text == "Гранит Р-43" || comboBox_model.Text == "Радий-301")
            {
                textBox_serialNumber.MaxLength = 7;
            }

            if (comboBox_model.Text == "Motorola P040" || comboBox_model.Text == "Motorola P080")
            {
                textBox_serialNumber.MaxLength = 10;
            }

            if (comboBox_model.Text == "Motorola DP-1400")
            {
                textBox_serialNumber.MaxLength = 10;
            }

            if (comboBox_model.Text == "Motorola DP-2400" || comboBox_model.Text == "Motorola DP-2400е")
            {
                textBox_serialNumber.MaxLength = 10;
            }

            if (comboBox_model.Text == "Motorola DP-4400")
            {
                textBox_serialNumber.MaxLength = 10;
            }

            if (comboBox_model.Text == "Motorola GP-300")
            {
                textBox_serialNumber.MaxLength = 10;
            }

            if (comboBox_model.Text == "Motorola GP-320" || comboBox_model.Text == "Kenwood ТК-2107" || comboBox_model.Text == "Vertex - 261"
                || comboBox_model.Text == "РА-160") //TODO Проверить условия а имеено зав номер GP320 Вертех Кенвуд РА
            {
                textBox_serialNumber.MaxLength = 10;
            }

            if (comboBox_model.Text == "Motorola GP-340")
            {
                textBox_serialNumber.MaxLength = 10;
            }

            if (comboBox_model.Text == "Motorola GP-360")
            {
                textBox_serialNumber.MaxLength = 10;
            }

            if (comboBox_model.Text == "Альтавия-301М" || comboBox_model.Text == "Элодия-351М")
            {
                textBox_serialNumber.MaxLength = 9;
            }

            if (comboBox_model.Text == "РН311М")
            {
                textBox_serialNumber.MaxLength = 10;
            }

            if (comboBox_model.Text == "Comrade R5")
            {
                textBox_serialNumber.MaxLength = 12;
            }

            if (comboBox_model.Text == "Комбат T-44")
            {
                textBox_serialNumber.MaxLength = 14;
            }

            if (comboBox_model.Text == "РНД-500")
            {
                textBox_serialNumber.MaxLength = 4;
            }

            if (comboBox_model.Text == "РНД-512")
            {
                textBox_serialNumber.MaxLength = 11;

            }
        }

        private void textBox_serialNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox_model.Text == "Icom IC-F3GT" || comboBox_model.Text == "Icom IC-F11" || comboBox_model.Text == "Icom IC-F16"
                || comboBox_model.Text == "Icom IC-F3GS" || comboBox_model.Text == "Альтавия-301М" || comboBox_model.Text == "Элодия-351М"
                || comboBox_model.Text == "Гранит Р33П-1" || comboBox_model.Text == "Гранит Р-43" || comboBox_model.Text == "Радий-301"
                || comboBox_model.Text == "РНД-500")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
                {

                }
                else
                {
                    e.Handled = true;
                }
            }

            if (comboBox_model.Text == "Motorola P040" || comboBox_model.Text == "Motorola P080" || comboBox_model.Text == "Motorola DP-1400" ||
                comboBox_model.Text == "Motorola DP-2400" || comboBox_model.Text == "Motorola DP-2400е" || comboBox_model.Text == "Motorola DP-4400" ||
                comboBox_model.Text == "Motorola GP-300" || comboBox_model.Text == "Motorola GP-320" || comboBox_model.Text == "Motorola GP-340" ||
                comboBox_model.Text == "Motorola GP-360" || comboBox_model.Text == "Comrade R5")
            {
                if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back)
                {

                }
                else
                {
                    e.Handled = true;
                }

            }

            if (comboBox_model.Text == "РН311М" || comboBox_model.Text == "РНД-512")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == ' ')
                {

                }
                else
                {
                    e.Handled = true;
                }
            }

            if (comboBox_model.Text == "Комбат T-44")
            {
                if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == 84)
                {

                }
                else
                {
                    e.Handled = true;
                }
            }
        }
    }
}
