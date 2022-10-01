﻿using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class AddExecutionСurator
    {
        #region добавление в выполнение

        internal static void AddExecutionRowСell(DataGridView dgw, string months)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    //MessageBox.Show(months);
                    var AddExecutionQuery = "";
                    foreach (DataGridViewRow row in dgw.SelectedRows)
                    {
                        dgw.Rows[row.Index].Cells[40].Value = months;
                    }
                    for (int index = 0; index < dgw.Rows.Count; index++)
                    {
                        var rowState = dgw.Rows[index].Cells[40].Value.ToString();//проверить индекс

                        if (rowState == months)
                        {
                            //var id = Convert.ToInt32(dgw.Rows[index].Cells[0].Value);
                            var poligon = dgw.Rows[index].Cells[1].Value.ToString();
                            var company = dgw.Rows[index].Cells[2].Value.ToString();
                            var location = dgw.Rows[index].Cells[3].Value.ToString();
                            var model = dgw.Rows[index].Cells[4].Value.ToString();
                            var serialNumber = dgw.Rows[index].Cells[5].Value.ToString();
                            var inventoryNumber = dgw.Rows[index].Cells[6].Value.ToString();
                            var networkNumber = dgw.Rows[index].Cells[7].Value.ToString();
                            var dateTO = dgw.Rows[index].Cells[8].Value.ToString();
                            var numberAct = dgw.Rows[index].Cells[9].Value.ToString();
                            var city = dgw.Rows[index].Cells[10].Value.ToString();
                            var price = dgw.Rows[index].Cells[11].Value.ToString();
                            var numberActRemont = dgw.Rows[index].Cells[17].Value.ToString();
                            var category = dgw.Rows[index].Cells[18].Value.ToString();
                            var priceRemont = dgw.Rows[index].Cells[19].Value.ToString();
                            var decommissionSerialNumber = dgw.Rows[index].Cells[38].Value.ToString();
                            var comment = dgw.Rows[index].Cells[39].Value.ToString();

                            if (!CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantionCurator(serialNumber))
                            {
                                if (inventoryNumber == "списание" || networkNumber == "списание" || !string.IsNullOrEmpty(decommissionSerialNumber)
                                    || string.IsNullOrEmpty(inventoryNumber) || string.IsNullOrEmpty(networkNumber) || inventoryNumber == "НЕТ"
                                    || networkNumber == "НЕТ")
                                {
                                    string Mesage;
                                    Mesage = $"У радиостанции {serialNumber} предприятия {company} нет подтверждения ОЦОР или она списанна. Вы действительно хотите её добавить в выполнение?";

                                    if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                    {
                                        continue;
                                    }
                                }

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                        $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                        $"category, priceRemont, decommissionSerialNumber, comment, month) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                        $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                        $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                        $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{months.Trim()}')";

                                using (MySqlCommand command = new MySqlCommand(AddExecutionQuery, DB_4.GetInstance.GetConnection()))
                                {
                                    DB_4.GetInstance.OpenConnection();
                                    command.ExecuteNonQuery();
                                    DB_4.GetInstance.CloseConnection();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Ошибка AddExecutionRowСell");
                }
            }
        }

        internal static void AddExecutionRowСellCurator(DataGridView dgw, string months, string cmB_city)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    //MessageBox.Show(months);
                    var AddExecutionQuery = "";
                    foreach (DataGridViewRow row in dgw.SelectedRows)
                    {
                        dgw.Rows[row.Index].Cells[18].Value = months;
                    }
                    for (int index = 0; index < dgw.Rows.Count; index++)
                    {
                        var rowState = dgw.Rows[index].Cells[18].Value.ToString();//проверить индекс

                        if (rowState == months)
                        {
                            //var id = Convert.ToInt32(dgw.Rows[index].Cells[0].Value);
                            var poligon = dgw.Rows[index].Cells[1].Value.ToString();
                            var company = dgw.Rows[index].Cells[2].Value.ToString();
                            var location = dgw.Rows[index].Cells[3].Value.ToString();
                            var model = dgw.Rows[index].Cells[4].Value.ToString();
                            var serialNumber = dgw.Rows[index].Cells[5].Value.ToString();
                            var inventoryNumber = dgw.Rows[index].Cells[6].Value.ToString();
                            var networkNumber = dgw.Rows[index].Cells[7].Value.ToString();
                            var dateTO = dgw.Rows[index].Cells[8].Value.ToString();
                            var numberAct = dgw.Rows[index].Cells[9].Value.ToString();
                            var city = dgw.Rows[index].Cells[10].Value.ToString();
                            var price = dgw.Rows[index].Cells[11].Value.ToString();
                            var numberActRemont = dgw.Rows[index].Cells[12].Value.ToString();
                            var category = dgw.Rows[index].Cells[13].Value.ToString();
                            var priceRemont = dgw.Rows[index].Cells[14].Value.ToString();
                            var decommissionSerialNumber = dgw.Rows[index].Cells[15].Value.ToString();
                            var comment = dgw.Rows[index].Cells[16].Value.ToString();

                            if (inventoryNumber == "списание" || networkNumber == "списание" || !string.IsNullOrEmpty(decommissionSerialNumber)
                                || string.IsNullOrEmpty(inventoryNumber) || string.IsNullOrEmpty(networkNumber) || inventoryNumber == "НЕТ"
                                || networkNumber == "НЕТ")
                            {
                                string Mesage;
                                Mesage = $"У радиостанции {serialNumber} предприятия {company} нет подтверждения ОЦОР или она списанна. Вы действительно хотите её добавить в выполнение?";

                                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                                {
                                    continue;
                                }
                            }

                            AddExecutionQuery = $"UPDATE radiostantion_сomparison SET months = '{months}' WHERE serialNumber = '{serialNumber}'";

                            using (MySqlCommand command = new MySqlCommand(AddExecutionQuery, DB_4.GetInstance.GetConnection()))
                            {
                                DB_4.GetInstance.OpenConnection();
                                command.ExecuteNonQuery();
                                DB_4.GetInstance.CloseConnection();
                            }
                        }
                    }
                    Filling_datagridview.RefreshDataGridСurator(dgw, cmB_city);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show("Ошибка AddExecutionRowСell");
                }
            }
        }

        #endregion
    }
}
