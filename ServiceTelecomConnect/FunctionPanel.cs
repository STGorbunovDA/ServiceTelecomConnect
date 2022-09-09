using MySql.Data.MySqlClient;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class FunctionPanel
    {
        internal static void Show_DB_radiostantion_full(DataGridView dgw, string city)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {  
                    if (city != "")
                    {
                        var myCulture = new CultureInfo("ru-RU");
                        myCulture.NumberFormat.NumberDecimalSeparator = ".";
                        Thread.CurrentThread.CurrentCulture = myCulture;
                        dgw.Rows.Clear();
                        string queryString = $"SELECT * FROM radiostantion_full";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB_2.GetInstance.GetConnection()))
                        {
                            DB_2.GetInstance.openConnection();

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        Filling_datagridview.ReedSingleRow(dgw, reader);
                                    }
                                    reader.Close();
                                }
                            }
                            command.ExecuteNonQuery();
                            DB_2.GetInstance.closeConnection();
                        }
                    }

                    dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                    dgw.Columns[0].Width = 45;
                    dgw.Columns[3].Width = 170;
                    dgw.Columns[4].Width = 180;
                    dgw.Columns[5].Width = 150;
                    dgw.Columns[6].Width = 178;
                    dgw.Columns[7].Width = 178;
                    dgw.Columns[8].Width = 100;
                    dgw.Columns[9].Width = 110;
                    dgw.Columns[10].Width = 100;
                    dgw.Columns[11].Width = 100;
                    dgw.Columns[17].Width = 120;
                    dgw.Columns[39].Width = 300;
                }
                catch (Exception ex)
                {
                    string Mesage2;
                    Mesage2 = "Невозможно загрузить общую базу данных!(radiostantion_full)";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        internal static void Show_DB_radiostantion_last_year(DataGridView dgw, string city)
        {
            if (Internet_check.AvailabilityChanged_bool())
            {
                try
                {
                    
                    if (city != "")
                    {
                        var myCulture = new CultureInfo("ru-RU");
                        myCulture.NumberFormat.NumberDecimalSeparator = ".";
                        Thread.CurrentThread.CurrentCulture = myCulture;
                        dgw.Rows.Clear();
                        string queryString = $"SELECT * FROM radiostantion_last_year WHERE city LIKE N'%{city}%'";

                        using (MySqlCommand command = new MySqlCommand(queryString, DB_2.GetInstance.GetConnection()))
                        {
                            DB_2.GetInstance.openConnection();

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        Filling_datagridview.ReedSingleRow(dgw, reader);
                                    }
                                    reader.Close();
                                }
                            }
                            command.ExecuteNonQuery();
                            DB_2.GetInstance.closeConnection();
                        }
                    }

                    dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                    dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

                    dgw.Columns[0].Width = 45;
                    dgw.Columns[3].Width = 170;
                    dgw.Columns[4].Width = 180;
                    dgw.Columns[5].Width = 150;
                    dgw.Columns[6].Width = 178;
                    dgw.Columns[7].Width = 178;
                    dgw.Columns[8].Width = 100;
                    dgw.Columns[9].Width = 110;
                    dgw.Columns[10].Width = 100;
                    dgw.Columns[11].Width = 100;
                    dgw.Columns[17].Width = 120;
                    dgw.Columns[39].Width = 300;
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
                finally
                {
                    DB.GetInstance.closeConnection();
                }
            }
        }
    }
}
