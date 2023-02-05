using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ServiceTelecomConnect.Forms
{
    public partial class AddChangeModelRST : Form
    {
        string selectedItem_cmB_model = String.Empty;

        public AddChangeModelRST()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void CmbModelSelectionChangeCommitted(object sender, EventArgs e)
        {
            selectedItem_cmB_model = cmB_model.GetItemText(cmB_model.SelectedItem);
        }

        void AddChangeModelRSTLoad(object sender, EventArgs e)
        {
            QuerySettingDataBase.CmbGettingModelRST(cmB_model);
        }

        void BtnAddModelRSTClick(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
            {
                if(!CheacModelRST(cmB_model.Text))
                {
                    string addQuery = $"INSERT INTO model_radiostation (model_radiostation_name) VALUES ('{cmB_model.Text}')";

                    using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Модель радиостанции успешно добавлена!");
                        DB.GetInstance.CloseConnection();
                    }

                    QuerySettingDataBase.CmbGettingModelRST(cmB_model);
                }
                else MessageBox.Show("Такая модель присутсвует в БД");

            }
        }

        void BtnChangeModelRSTClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmB_model.Text))
                return;

            if (InternetCheck.CheackSkyNET())
            {
                string addQuery = $"UPDATE model_radiostation SET model_radiostation_name = '{cmB_model.Text}' WHERE model_radiostation_name = '{selectedItem_cmB_model}'";

                using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    QuerySettingDataBase.CmbGettingModelRST(cmB_model);
                    MessageBox.Show("Модель радиостанции успешно изменена!");
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        void BtnDeleteModelRSTClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cmB_model.Text))
                return;

            string Mesage = $"Вы действительно хотите удалить модель радиостанции?\n Модель: {cmB_model.GetItemText(cmB_model.SelectedItem)}";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            string deleteQuery = $"DELETE FROM model_radiostation WHERE model_radiostation_name = '{cmB_model.GetItemText(cmB_model.SelectedItem)}'";
            using (MySqlCommand command = new MySqlCommand(deleteQuery, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                command.ExecuteNonQuery();
                QuerySettingDataBase.CmbGettingModelRST(cmB_model);
                MessageBox.Show("Модель радиостанции успешно удалена!");
                DB.GetInstance.CloseConnection();
            }
        }

        public Boolean CheacModelRST(string model)
        {
            if (InternetCheck.CheackSkyNET())
            {
                string querystring = $"SELECT model_radiostation_name FROM model_radiostation WHERE model_radiostation_name = '{model}'";
                MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection());
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
