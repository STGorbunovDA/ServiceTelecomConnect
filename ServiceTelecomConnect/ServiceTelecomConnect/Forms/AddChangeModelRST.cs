using MySql.Data.MySqlClient;
using System;
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
        private void CmB_model_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selectedItem_cmB_model = cmB_model.GetItemText(cmB_model.SelectedItem);
        }

        void AddChangeModelRST_Load(object sender, EventArgs e)
        {
            QuerySettingDataBase.GettingModelRST_CMB(cmB_model);
        }

        void Btn_add_modelRST_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                var addQuery = $"insert into model_radiostation (model_radiostation_name) VALUES ('{cmB_model.Text}')";

                using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Модель радиостанции успешно добавлена!");
                    DB.GetInstance.CloseConnection();
                }

                QuerySettingDataBase.GettingModelRST_CMB(cmB_model);
            }
        }

        void Btn_change_modelRST_Click(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                var addQuery = $"UPDATE model_radiostation SET model_radiostation_name = '{cmB_model.Text}' WHERE model_radiostation_name = '{selectedItem_cmB_model}'";

                using (MySqlCommand command = new MySqlCommand(addQuery, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    QuerySettingDataBase.GettingModelRST_CMB(cmB_model);
                    MessageBox.Show("Модель радиостанции успешно изменена!");
                    DB.GetInstance.CloseConnection();
                }
            }
        }

        void Btn_delete_modelRST_Click(object sender, EventArgs e)
        {
            string Mesage;
            Mesage = $"Вы действительно хотите удалить модель радиостанции?\n Модель: {cmB_model.GetItemText(cmB_model.SelectedItem)}";

            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            var deleteQuery = $"delete from model_radiostation where model_radiostation_name = '{cmB_model.GetItemText(cmB_model.SelectedItem)}'";
            using (MySqlCommand command = new MySqlCommand(deleteQuery, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                command.ExecuteNonQuery();
                QuerySettingDataBase.GettingModelRST_CMB(cmB_model);
                MessageBox.Show("Модель радиостанции успешно удалена!");
                DB.GetInstance.CloseConnection();
            }
        }
    }
}
