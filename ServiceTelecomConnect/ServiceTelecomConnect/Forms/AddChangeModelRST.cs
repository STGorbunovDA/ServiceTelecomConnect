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

namespace ServiceTelecomConnect.Forms
{
    public partial class AddChangeModelRST : Form
    {
        public AddChangeModelRST()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        void AddChangeModelRST_Load(object sender, EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                DB.GetInstance.OpenConnection();
                string querystring = $"SELECT id, model_radiostation_name FROM model_radiostation";
                using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DataTable model_RSR_table = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(model_RSR_table);
                        cmB_model.DataSource = model_RSR_table;
                        cmB_model.ValueMember = "id";
                        cmB_model.DisplayMember = "model_radiostation_name";
                    }
                }
                DB.GetInstance.CloseConnection();
            }
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

                string querystring = $"SELECT id, model_radiostation_name FROM model_radiostation";
                using (MySqlCommand command2 = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    DataTable table = new DataTable();

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command2))
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            cmB_model.DataSource = table;
                            cmB_model.ValueMember = "id";
                            cmB_model.DisplayMember = "model_radiostation_name";
                        }
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }

        void Btn_change_modelRST_Click(object sender, EventArgs e)
        {

        }
    }
}
