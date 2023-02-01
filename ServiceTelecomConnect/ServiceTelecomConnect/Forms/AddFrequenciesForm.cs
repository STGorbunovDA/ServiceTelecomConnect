using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ServiceTelecomConnect.Forms
{
    public partial class AddFrequenciesForm : Form
    {
        string selectedItemFrequenciesCmb = String.Empty;
        public AddFrequenciesForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        void CmbFrequenciesSelectionChangeCommitted(object sender, EventArgs e)
        {
            selectedItemFrequenciesCmb = cmB_Frequencies.GetItemText(cmB_Frequencies.SelectedItem);
        }
        void AddFrequenciesLoad(object sender, EventArgs e)
        {
            QuerySettingDataBase.CmbGettingFrequenciesRST(cmB_Frequencies);
        }
        void BtnAddFrequenciesClick(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(cmB_Frequencies.Text))
            {
                MessageBox.Show("Нельзя добавить пустую частоту!");
                return;
            }

            if (!Regex.IsMatch(cmB_Frequencies.Text, @"^[1][0-9]{1,1}[0-9]{1,1}[.][0-9]{1,1}[0-9]{1,1}[0-9]{1,1}$"))
            {
                MessageBox.Show("Введите корректно поле: \"Частота\"\nПример: 151.825", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmB_Frequencies.Select();
                return;
            }

            if (Internet_check.CheackSkyNET())
            {
                if (!CheackFrequencies(cmB_Frequencies.Text))
                {
                    string addQuery = $"INSERT INTO frequencies (frequency) VALUES ('{cmB_Frequencies.Text}')";

                    using (MySqlCommand command = new MySqlCommand(addQuery, DB_3.GetInstance.GetConnection()))
                    {
                        DB_3.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Частота успешно добавлена!");
                        DB_3.GetInstance.CloseConnection();
                    }

                    QuerySettingDataBase.CmbGettingFrequenciesRST(cmB_Frequencies);
                }
                else MessageBox.Show("Такая частота присутсвует в БД");

            }
        }
        void BtnChangeFrequenciesClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmB_Frequencies.Text))
                return;

            if (Internet_check.CheackSkyNET())
            {
                string addQuery = $"UPDATE frequencies SET frequency = '{cmB_Frequencies.Text}' WHERE frequency = '{selectedItemFrequenciesCmb}'";

                using (MySqlCommand command = new MySqlCommand(addQuery, DB_3.GetInstance.GetConnection()))
                {
                    DB_3.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    QuerySettingDataBase.CmbGettingFrequenciesRST(cmB_Frequencies);
                    MessageBox.Show("Частота успешно изменена!");
                    DB_3.GetInstance.CloseConnection();
                }
            }
        }
        void BtnDeleteFrequenciesClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmB_Frequencies.Text))
                return;

            string Mesage = $"Вы действительно хотите удалить модель радиостанции?\n Модель: {cmB_Frequencies.GetItemText(cmB_Frequencies.SelectedItem)}";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            string deleteQuery = $"delete from frequencies where frequency = '{cmB_Frequencies.GetItemText(cmB_Frequencies.SelectedItem)}'";

            using (MySqlCommand command = new MySqlCommand(deleteQuery, DB_3.GetInstance.GetConnection()))
            {
                DB_3.GetInstance.OpenConnection();
                command.ExecuteNonQuery(); 
                QuerySettingDataBase.CmbGettingFrequenciesRST(cmB_Frequencies);
                MessageBox.Show("Частота успешно удалена!");
                DB_3.GetInstance.CloseConnection();
            }
        }
        public Boolean CheackFrequencies(string frequency)
        {
            if (Internet_check.CheackSkyNET())
            {
                string querystring = $"SELECT frequency FROM frequencies WHERE frequency = '{frequency}'";
                MySqlCommand command = new MySqlCommand(querystring, DB_3.GetInstance.GetConnection());
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count > 0) return true;
                else return false;
            }
            return true;
        }       
    }
}
