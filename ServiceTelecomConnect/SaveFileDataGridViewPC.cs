using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ServiceTelecomConnect
{
    class SaveFileDataGridViewPC
    {
        #region Сохранение БД на PC

        /// <summary>
        /// сохранение БД на H(S)DD
        /// </summary>
       internal static void SaveFilePC(DataGridView dgw)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    string filename = sfd.FileName;

                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Unicode))
                    {
                        string note = string.Empty;

                        note += $"Номер\tПолигон\tПредприятие\tМесто нахождения\tМодель\tЗаводской номер\t" +
                            $"Инвентарный номер\tСетевой номер\tДата проведения ТО\tНомер акта\tГород\tЦена ТО\t" +
                            $"Представитель предприятия\tДолжность\tНомер удостоверения\tДата выдачи\tНомер телефона\t" +
                            $"Номер Акта ремонта\tКатегория\tЦена ремонта\tАнтенна\tМанипулятор\tАКБ\tЗУ\tВыполненные работы_1\t" +
                            $"Выполненные работы_2\tВыполненные работы_3\tВыполненные работы_4\tВыполненные работы_5\t" +
                            $"Выполненные работы_6\tВыполненные работы_7\tДеталь_1\tДеталь_2\tДеталь_3\tДеталь_4\tДеталь_5\t" +
                            $"Деталь_6\tДеталь_7\t";

                        sw.WriteLine(note);

                        for (int i = 0; i < dgw.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgw.ColumnCount; j++)
                            {
                                sw.Write((dgw.Rows[i].Cells[j].Value + "\t").ToString());
                            }

                            sw.WriteLine();
                        }

                        MessageBox.Show("Файл успешно сохранен");
                    }
                }
                else
                {
                    string Mesage;
                    Mesage = "Файл не сохранён";

                    if (MessageBox.Show(Mesage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                string Mesage;
                Mesage = "Файл не сохранён!";

                if (MessageBox.Show(Mesage, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    return;
                }
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion
    }
}
