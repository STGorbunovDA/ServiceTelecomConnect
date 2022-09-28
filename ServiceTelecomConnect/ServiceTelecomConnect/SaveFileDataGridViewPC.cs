using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class SaveFileDataGridViewPC
    {
        #region Сохранение БД на PC пользователем

        /// <summary>
        /// сохранение БД на H(S)DD
        /// </summary>
        internal static void UserSaveFilePC(DataGridView dgw)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Unicode))
                    {
                        string note = string.Empty;

                        note += $"Номер\tПолигон\tПредприятие\tМесто нахождения\tМодель\tЗаводской номер\t" +
                            $"Инвентарный номер\tСетевой номер\tДата проведения ТО\tНомер акта\tГород\tЦена ТО\t" +
                            $"Представитель предприятия\tДолжность\tНомер удостоверения\tДата выдачи\tНомер телефона\t" +
                            $"Номер Акта ремонта\tКатегория\tЦена ремонта\tАнтенна\tМанипулятор\tАКБ\tЗУ\tВыполненные работы_1\t" +
                            $"Выполненные работы_2\tВыполненные работы_3\tВыполненные работы_4\tВыполненные работы_5\t" +
                            $"Выполненные работы_6\tВыполненные работы_7\tДеталь_1\tДеталь_2\tДеталь_3\tДеталь_4\tДеталь_5\t" +
                            $"Деталь_6\tДеталь_7\t№ Акта списания\tПримечания";

                        sw.WriteLine(note);

                        for (int i = 0; i < dgw.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgw.ColumnCount; j++)
                            {
                                var re = new Regex(Environment.NewLine);
                                var perem = dgw.Rows[i].Cells[j].Value.ToString();
                                perem = re.Replace(perem, " ");
                                sw.Write(perem + "\t");//todo решить
                            }

                            sw.WriteLine();
                        }

                        MessageBox.Show("Файл успешно сохранен");
                    }
                }
                else
                {
                    MessageBox.Show("Вы не указали путь сохранения!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Файл не сохранен!(UserSaveFilePC)");
            }
        }

        #endregion

        #region Сохранение Бд по счётчику

        internal static void AutoSaveFilePC(DataGridView dgw, string city)
        {
            try
            {
                DateTime today = DateTime.Today;
                
                string fileNamePath = $@"C:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\БазаДанных-{city}-{today.ToString("dd.MM.yyyy")}.csv";

                if (!File.Exists($@"С:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\"))
                {
                    Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\");
                }

                using (StreamWriter sw = new StreamWriter(fileNamePath, false, Encoding.Unicode))
                {
                    string note = string.Empty;

                    note += $"Номер\tПолигон\tПредприятие\tМесто нахождения\tМодель\tЗаводской номер\t" +
                        $"Инвентарный номер\tСетевой номер\tДата проведения ТО\tНомер акта\tГород\tЦена ТО\t" +
                        $"Представитель предприятия\tДолжность\tНомер удостоверения\tДата выдачи\tНомер телефона\t" +
                        $"Номер Акта ремонта\tКатегория\tЦена ремонта\tАнтенна\tМанипулятор\tАКБ\tЗУ\tВыполненные работы_1\t" +
                        $"Выполненные работы_2\tВыполненные работы_3\tВыполненные работы_4\tВыполненные работы_5\t" +
                        $"Выполненные работы_6\tВыполненные работы_7\tДеталь_1\tДеталь_2\tДеталь_3\tДеталь_4\tДеталь_5\t" +
                        $"Деталь_6\tДеталь_7\t№ Акта списания\tПримечания";

                    sw.WriteLine(note);

                    for (int i = 0; i < dgw.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgw.ColumnCount; j++)
                        {
                            sw.Write(dgw.Rows[i].Cells[j].Value.ToString() + "\t");//todo решить
                        }
                        sw.WriteLine();
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Файл не сохранен!(AutoSaveFilePC)");
            }
        }

        #endregion

    }
}
