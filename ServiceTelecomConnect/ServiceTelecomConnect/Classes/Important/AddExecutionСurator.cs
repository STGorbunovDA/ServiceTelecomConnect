using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class AddExecutionСurator
    {
        #region добавление в выполнение

        internal static void AddExecutionRowСell(DataGridView dgw, string months)
        {
            if (Internet_check.CheackSkyNET())
            {
                string AddExecutionQuery = String.Empty;
                foreach (DataGridViewRow row in dgw.SelectedRows)
                    dgw.Rows[row.Index].Cells[41].Value = months;

                for (int index = 0; index < dgw.Rows.Count; index++)
                {
                    string rowState = dgw.Rows[index].Cells[41].Value.ToString();//проверить индекс

                    if (rowState == months)
                    {
                        string poligon = dgw.Rows[index].Cells[1].Value.ToString();
                        string company = dgw.Rows[index].Cells[2].Value.ToString();
                        string location = dgw.Rows[index].Cells[3].Value.ToString();
                        string model = dgw.Rows[index].Cells[4].Value.ToString();
                        string serialNumber = dgw.Rows[index].Cells[5].Value.ToString();
                        string inventoryNumber = dgw.Rows[index].Cells[6].Value.ToString();
                        string networkNumber = dgw.Rows[index].Cells[7].Value.ToString();
                        DateTime _dateTO = (DateTime)dgw.Rows[index].Cells[8].Value;
                        string dateTO = Convert.ToDateTime(_dateTO).ToString("yyyy-MM-dd");
                        string numberAct = dgw.Rows[index].Cells[9].Value.ToString();
                        string city = dgw.Rows[index].Cells[10].Value.ToString();
                        string price = dgw.Rows[index].Cells[11].Value.ToString();
                        string numberActRemont = dgw.Rows[index].Cells[17].Value.ToString();
                        string category = dgw.Rows[index].Cells[18].Value.ToString();
                        string priceRemont = dgw.Rows[index].Cells[19].Value.ToString();
                        string decommissionSerialNumber = dgw.Rows[index].Cells[38].Value.ToString();
                        string comment = dgw.Rows[index].Cells[39].Value.ToString();
                        string road = dgw.Rows[index].Cells[40].Value.ToString();

                        if (!CheacSerialNumber.GetInstance.CheacSerialNumberRadiostantionCurator(road, city, serialNumber))
                        {
                            if (inventoryNumber == "списание" || networkNumber == "списание" || !string.IsNullOrEmpty(decommissionSerialNumber)
                                || string.IsNullOrEmpty(inventoryNumber) || string.IsNullOrEmpty(networkNumber) || inventoryNumber == "НЕТ"
                                || networkNumber == "НЕТ")
                            {
                                string Mesage = $"У радиостанции {serialNumber} предприятия {company} нет подтверждения ОЦОР или она списанна. Вы действительно хотите её добавить в выполнение?";

                                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    continue;
                            }

                            AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, month, road) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{months.Trim()}', '{road.Trim()}')";

                            using (MySqlCommand command = new MySqlCommand(AddExecutionQuery, DB_4.GetInstance.GetConnection()))
                            {
                                DB_4.GetInstance.OpenConnection();
                                command.ExecuteNonQuery();
                                DB_4.GetInstance.CloseConnection();
                            }
                        }
                    }
                }
                MessageBox.Show("Успешно!");
            }
        }

        internal static void AddExecutionRowСellCurator(DataGridView dgw, string month, ComboBox road, ComboBox cmB_month)
        {
            if (Internet_check.CheackSkyNET())
            {
                foreach (DataGridViewRow row in dgw.SelectedRows)
                    dgw.Rows[row.Index].Cells[19].Value = month;

                for (int index = 0; index < dgw.Rows.Count; index++)
                {
                    string rowState = dgw.Rows[index].Cells[19].Value.ToString();//проверить индекс

                    if (rowState == month)
                    {
                        string company = dgw.Rows[index].Cells[2].Value.ToString();
                        string serialNumber = dgw.Rows[index].Cells[5].Value.ToString();
                        string inventoryNumber = dgw.Rows[index].Cells[6].Value.ToString();
                        string networkNumber = dgw.Rows[index].Cells[7].Value.ToString();
                        string decommissionSerialNumber = dgw.Rows[index].Cells[15].Value.ToString();

                        if (inventoryNumber == "списание" || networkNumber == "списание" || !string.IsNullOrEmpty(decommissionSerialNumber)
                            || string.IsNullOrEmpty(inventoryNumber) || string.IsNullOrEmpty(networkNumber) || inventoryNumber == "НЕТ"
                            || networkNumber == "НЕТ")
                        {
                            string Mesage = $"У радиостанции {serialNumber} предприятия {company} нет подтверждения ОЦОР или она списанна. Вы действительно хотите её добавить в выполнение?";

                            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                continue;
                        }

                        string AddExecutionQuery = $"UPDATE radiostantion_сomparison SET month = '{month}' WHERE serialNumber = '{serialNumber}' AND road = '{road.Text}'";

                        using (MySqlCommand command = new MySqlCommand(AddExecutionQuery, DB_4.GetInstance.GetConnection()))
                        {
                            DB_4.GetInstance.OpenConnection();
                            command.ExecuteNonQuery();
                            DB_4.GetInstance.CloseConnection();
                        }
                    }
                }
                MessageBox.Show("Успешно!");
                int currRowIndex = dgw.CurrentCell.RowIndex;
                QuerySettingDataBase.RefreshDataGridСurator(dgw, road.Text);
                QuerySettingDataBase.SelectCityGropByMonthRoad(road, cmB_month);
                dgw.ClearSelection();

                if (dgw.RowCount - currRowIndex > 0)
                    dgw.CurrentCell = dgw[0, currRowIndex];
            }
        }

        #endregion
    }
}
