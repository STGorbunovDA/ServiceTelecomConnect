using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace ServiceTelecomConnect.Forms
{
    public partial class DirectorForm : Form
    {
        public DirectorForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        void DirectorForm_Load(object sender, System.EventArgs e)
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    string querystring = $"SELECT id, login, is_admin, road FROM users WHERE is_admin = 'Начальник участка'";
                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        DataTable table = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                            if (table.Rows.Count > 0)
                            {
                                cmB_section_foreman_FIO.DataSource = table;
                                cmB_section_foreman_FIO.ValueMember = "id";
                                cmB_section_foreman_FIO.DisplayMember = "login";
                            }
                            else
                            {
                                cmB_section_foreman_FIO.Text = "";
                            }
                        }
                    }

                    string querystring2 = $"SELECT id, login, is_admin, road FROM users WHERE is_admin = 'Инженер'";
                    using (MySqlCommand command = new MySqlCommand(querystring2, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        DataTable table = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(table);
                            if (table.Rows.Count > 0)
                            {
                                cmB_engineers_FIO.DataSource = table;
                                cmB_engineers_FIO.ValueMember = "id";
                                cmB_engineers_FIO.DisplayMember = "login";
                            }
                            else
                            {
                                cmB_section_foreman_FIO.Text = "";
                            }
                        }
                    }

                    if (String.IsNullOrEmpty(cmB_road.Text))
                    {
                        cmB_road.Text = cmB_road.Items[0].ToString();
                    }
                    if(String.IsNullOrEmpty(cmB_engineers_FIO.Text))
                    {
                        MessageBox.Show("Добавьте инженера!");
                    }
                    if (String.IsNullOrEmpty(cmB_section_foreman_FIO.Text))
                    {
                        MessageBox.Show("Добавьте начальника участка!");
                    }

                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка добавления в comboBox данных");
                }
            }
        }
    }
}
