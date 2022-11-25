using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows.Forms;

namespace ServiceTelecomConnect.Classes.Other
{
    class FunctionalPanel
    {
        internal static void Loading_file_current_BD()
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    OpenFileDialog openFile = new OpenFileDialog
                    {
                        Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
                    };

                    openFile.ShowDialog();

                    if (!String.IsNullOrEmpty(openFile.FileName))
                    {
                        string filename = openFile.FileName;
                        string text = File.ReadAllText(filename);

                        var lineNumber = 0;

                        using (StreamReader reader = new StreamReader(filename))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();

                                if (lineNumber != 0)
                                {
                                    var values = line.Split('\t');

                                    if (!CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantion(values[4]))
                                    {
                                        var mySql = $"INSERT INTO radiostantion (poligon, company, location, model, serialNumber," +
                                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, representative, " +
                                        $"post, numberIdentification, dateIssue, phoneNumber, numberActRemont, category, priceRemont, " +
                                        $"antenna, manipulator, AKB, batteryСharger, completed_works_1, completed_works_2, completed_works_3, " +
                                        $"completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1, parts_2, parts_3, parts_4, " +
                                        $"parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road) VALUES " +
                                        $"('{values[0]}', '{values[1]}', '{values[2]}', '{values[3]}','{values[4]}', '{values[5]}', '{values[6]}', " +
                                        $"'{values[7]}','{values[8]}','{values[9]}','{values[10]}', '{values[11]}', '{values[12]}', " +
                                        $"'{values[13]}', '{values[14]}', '{values[15]}', '{values[16]}', '{values[17]}', '{values[18]}'," +
                                        $"'{values[19]}', '{values[20]}', '{values[21]}', '{values[22]}', '{values[23]}', '{values[24]}', " +
                                        $"'{values[25]}', '{values[26]}', '{values[27]}', '{values[28]}', '{values[29]}', '{values[30]}', " +
                                        $"'{values[31]}', '{values[32]}', '{values[33]}', '{values[34]}', '{values[35]}', '{values[36]}', " +
                                        $"'{values[37]}', '{values[38]}', '{values[39]}')";

                                        using (MySqlCommand command = new MySqlCommand(mySql, DB.GetInstance.GetConnection()))
                                        {
                                            DB.GetInstance.OpenConnection();
                                            command.ExecuteNonQuery();
                                            DB.GetInstance.CloseConnection();
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                lineNumber++;
                            }
                            if (reader.EndOfStream)
                            {
                                MessageBox.Show("Радиостанции успешно добавлены!");
                            }
                            else
                            {
                                MessageBox.Show("Радиостанции не добавленны.Системная ошибка ");
                            }
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
                catch (Exception)
                {
                    string Mesage = $"Ошибка загрузки данных для текущей БД! Радиостанции не добавленны!(Loading_file_current_BD)";

                    if (MessageBox.Show(Mesage, "Обратите внимание на содержимое файла", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
            }
        }

        internal static void Loading_file_last_year()
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    OpenFileDialog openFile = new OpenFileDialog
                    {
                        Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
                    };

                    openFile.ShowDialog();

                    if (!String.IsNullOrEmpty(openFile.FileName))
                    {
                        string filename = openFile.FileName;
                        string text = File.ReadAllText(filename);

                        var lineNumber = 0;

                        using (StreamReader reader = new StreamReader(filename))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();

                                if (lineNumber != 0)
                                {
                                    var values = line.Split('\t');

                                    if (!CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantion_last_year(values[4]))
                                    {
                                        var mySql = $"INSERT INTO radiostantion_last_year (poligon, company, location, model, serialNumber," +
                                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, representative, " +
                                        $"post, numberIdentification, dateIssue, phoneNumber, numberActRemont, category, priceRemont, " +
                                        $"antenna, manipulator, AKB, batteryСharger, completed_works_1, completed_works_2, completed_works_3, " +
                                        $"completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1, parts_2, parts_3, parts_4, " +
                                        $"parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road) VALUES " +
                                        $"('{values[0]}', '{values[1]}', '{values[2]}', '{values[3]}','{values[4]}', '{values[5]}', '{values[6]}', " +
                                        $"'{values[7]}','{values[8]}','{values[9]}','{values[10]}', '{values[11]}', '{values[12]}', " +
                                        $"'{values[13]}', '{values[14]}', '{values[15]}', '{values[16]}', '{values[17]}', '{values[18]}'," +
                                        $"'{values[19]}', '{values[20]}', '{values[21]}', '{values[22]}', '{values[23]}', '{values[24]}', " +
                                        $"'{values[25]}', '{values[26]}', '{values[27]}', '{values[28]}', '{values[29]}', '{values[30]}', " +
                                        $"'{values[31]}', '{values[32]}', '{values[33]}', '{values[34]}', '{values[35]}', '{values[36]}', " +
                                        $"'{values[37]}', '{values[38]}', '{values[39]}')";

                                        using (MySqlCommand command = new MySqlCommand(mySql, DB.GetInstance.GetConnection()))
                                        {
                                            DB.GetInstance.OpenConnection();
                                            command.ExecuteNonQuery();
                                            DB.GetInstance.CloseConnection();
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                lineNumber++;
                            }
                            if (reader.EndOfStream)
                            {
                                MessageBox.Show("Радиостанции успешно добавлены!");
                            }
                            else
                            {
                                MessageBox.Show("Радиостанции не добавленны.Системная ошибка ");
                            }
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
                catch (Exception)
                {
                    string Mesage = $"Ошибка загрузки данных для текущей БД! Радиостанции не добавленны!(Loading_file_last_year)";

                    if (MessageBox.Show(Mesage, "Обратите внимание на содержимое файла", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
            }
        }

        internal static void Loading_file_full_BD()
        {
            if (Internet_check.CheackSkyNET())
            {
                try
                {
                    OpenFileDialog openFile = new OpenFileDialog
                    {
                        Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
                    };

                    openFile.ShowDialog();

                    if (!String.IsNullOrEmpty(openFile.FileName))
                    {
                        string filename = openFile.FileName;
                        string text = File.ReadAllText(filename);

                        var lineNumber = 0;

                        using (StreamReader reader = new StreamReader(filename))
                        {
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();

                                if (lineNumber != 0)
                                {
                                    var values = line.Split('\t');

                                    if (!CheacSerialNumber.GetInstance.CheacSerialNumber_radiostantion_last_year(values[4]))
                                    {
                                        var mySql = $"INSERT INTO radiostantion_full (poligon, company, location, model, serialNumber," +
                                        $"inventoryNumber, networkNumber, dateTO, numberAct, city, price, representative, " +
                                        $"post, numberIdentification, dateIssue, phoneNumber, numberActRemont, category, priceRemont, " +
                                        $"antenna, manipulator, AKB, batteryСharger, completed_works_1, completed_works_2, completed_works_3, " +
                                        $"completed_works_4, completed_works_5, completed_works_6, completed_works_7, parts_1, parts_2, parts_3, parts_4, " +
                                        $"parts_5, parts_6, parts_7, decommissionSerialNumber, comment, road) VALUES " +
                                        $"('{values[0]}', '{values[1]}', '{values[2]}', '{values[3]}','{values[4]}', '{values[5]}', '{values[6]}', " +
                                        $"'{values[7]}','{values[8]}','{values[9]}','{values[10]}', '{values[11]}', '{values[12]}', " +
                                        $"'{values[13]}', '{values[14]}', '{values[15]}', '{values[16]}', '{values[17]}', '{values[18]}'," +
                                        $"'{values[19]}', '{values[20]}', '{values[21]}', '{values[22]}', '{values[23]}', '{values[24]}', " +
                                        $"'{values[25]}', '{values[26]}', '{values[27]}', '{values[28]}', '{values[29]}', '{values[30]}', " +
                                        $"'{values[31]}', '{values[32]}', '{values[33]}', '{values[34]}', '{values[35]}', '{values[36]}', " +
                                        $"'{values[37]}', '{values[38]}', '{values[39]}')";

                                        using (MySqlCommand command = new MySqlCommand(mySql, DB.GetInstance.GetConnection()))
                                        {
                                            DB.GetInstance.OpenConnection();
                                            command.ExecuteNonQuery();
                                            DB.GetInstance.CloseConnection();
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                lineNumber++;
                            }
                            if (reader.EndOfStream)
                            {
                                MessageBox.Show("Радиостанции успешно добавлены!");
                            }
                            else
                            {
                                MessageBox.Show("Радиостанции не добавленны.Системная ошибка ");
                            }
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
                catch (Exception)
                {
                    string Mesage = $"Ошибка загрузки данных для текущей БД! Радиостанции не добавленны!(Loading_file_full_BD)";

                    if (MessageBox.Show(Mesage, "Обратите внимание на содержимое файла", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
            }
        }
    }
}
