using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    internal class RegistryClass
    {
        internal static void SelectCityGropBy(ComboBox cmB_city)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    string querystring = $"SELECT city FROM radiostantion GROUP BY city";
                    using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
                    {
                        DB.GetInstance.OpenConnection();
                        DataTable city_table = new DataTable();

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(city_table);

                            cmB_city.DataSource = city_table;
                            cmB_city.DisplayMember = "city";
                            DB.GetInstance.CloseConnection();
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка! Города не добавленны в comboBox!ST_WorkForm_Load");
                }
            }
        }
    }
}
