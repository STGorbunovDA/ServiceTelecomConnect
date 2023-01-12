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


        internal static void DirectorateSaveFilePC(DataGridView dgw, string cmB_city)
        {
            DateTime dateTime = DateTime.Now;
            string dateTimeString = dateTime.ToString("dd.MM.yyyy");
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            sfd.FileName = $"База_{cmB_city}_{dateTimeString}";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Unicode))
                {
                    string note = string.Empty;

                    note += $"РЦС\tПредприятие(балансодержатель)\tМесто нахождения\tМодель\tЗаводской номер\tИнвентарный\t" +
                        $"Сетевой\tДата проведения ТО\t№ акта\t№ накладной\t№ ведомости\t№ акта ремонта\tКатегория\t" +
                        $"№ акта списания\tЦенаТО(без НДС)\tЦена ремонта(без НДС)\tГород\tПримечание";

                    sw.WriteLine(note);

                    var poligon = ""; var company = ""; var location = ""; var model = ""; var serialNumber = "";
                    var inventoryNumber = ""; var networkNumber = ""; var dateTO = ""; var numberAct = "";
                    var numberActRemont = ""; var category = ""; var decommissionSerialNumber = "";
                    var price = ""; var priceRemont = ""; var city = ""; var comment = "";

                    for (int i = 0; i < dgw.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgw.ColumnCount; j++)
                        {
                            if (j < dgw.ColumnCount)
                            {
                                if (dgw.Columns[j].HeaderText.ToString() == "Полигон")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    poligon = dgw.Rows[i].Cells[j].Value.ToString();
                                    poligon = re.Replace(poligon, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Предприятие")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    company = dgw.Rows[i].Cells[j].Value.ToString();
                                    company = re.Replace(company, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Место нахождения")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    location = dgw.Rows[i].Cells[j].Value.ToString();
                                    location = re.Replace(location, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Модель радиостанции")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    model = dgw.Rows[i].Cells[j].Value.ToString();
                                    model = re.Replace(model, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Заводской номер")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    serialNumber = dgw.Rows[i].Cells[j].Value.ToString();
                                    serialNumber = re.Replace(serialNumber, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Инвентарный номер")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    inventoryNumber = dgw.Rows[i].Cells[j].Value.ToString();
                                    inventoryNumber = re.Replace(inventoryNumber, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Сетевой номер")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    networkNumber = dgw.Rows[i].Cells[j].Value.ToString();
                                    networkNumber = re.Replace(networkNumber, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Дата ТО")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    dateTO = Convert.ToDateTime(dgw.Rows[i].Cells[j].Value.ToString()).ToString("dd.MM.yyyy");
                                    dateTO = re.Replace(dateTO, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "№ акта ТО")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    numberAct = dgw.Rows[i].Cells[j].Value.ToString();
                                    numberAct = re.Replace(numberAct, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "№ акта ремонта")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    numberActRemont = dgw.Rows[i].Cells[j].Value.ToString();
                                    numberActRemont = re.Replace(numberActRemont, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Категория")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    category = dgw.Rows[i].Cells[j].Value.ToString();
                                    category = re.Replace(category, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "№ акта списания")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    decommissionSerialNumber = dgw.Rows[i].Cells[j].Value.ToString();
                                    decommissionSerialNumber = re.Replace(decommissionSerialNumber, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Цена ТО")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    price = dgw.Rows[i].Cells[j].Value.ToString();
                                    price = re.Replace(price, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Цена ремонта")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    priceRemont = dgw.Rows[i].Cells[j].Value.ToString();
                                    priceRemont = re.Replace(priceRemont, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Город")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    city = dgw.Rows[i].Cells[j].Value.ToString();
                                    city = re.Replace(city, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Примечание")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    comment = dgw.Rows[i].Cells[j].Value.ToString();
                                    comment = re.Replace(comment, " ");
                                }
                            }
                            if (j == dgw.ColumnCount - 1)
                            {
                                if (priceRemont == "0.00")
                                    priceRemont = "";

                                if (!String.IsNullOrEmpty(decommissionSerialNumber))
                                {
                                    numberAct = "списание"; dateTO = "списание";
                                    if (!String.IsNullOrEmpty(numberActRemont))
                                    {
                                        numberActRemont = "";
                                        category = "";
                                        priceRemont = "";
                                    }
                                }
                                sw.Write(poligon + "\t" + company + "\t" + location + "\t" + model + "\t" + serialNumber + "\t" + inventoryNumber + "\t"
                                    + networkNumber + "\t" + dateTO + "\t" + numberAct + "\t" + numberAct + "\t" + numberAct + "\t" + numberActRemont + "\t"
                                    + category + "\t" + decommissionSerialNumber + "\t" + price + "\t" + priceRemont + "\t" + city + "\t" + comment);
                            }
                        }
                        sw.WriteLine();
                    }
                    MessageBox.Show("Файл успешно сохранен");
                }
            }
            else MessageBox.Show("Вы не указали путь сохранения!");
        }

        internal static void UserSaveFileCuratorPC(DataGridView dgw, string cmb_road)
        {
            DateTime dateTime = DateTime.Now;
            string dateTimeString = dateTime.ToString("dd.MM.yyyy");
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            sfd.FileName = $"База_{cmb_road}_{dateTimeString}";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Unicode))
                {
                    string note = string.Empty;

                    note += $"РЦС\tПредприятие(балансодержатель)\tМесто нахождения\tМодель\tЗаводской номер\tИнвентарный\t" +
                        $"Сетевой\tДата проведения ТО\t№ акта\t№ накладной\t№ ведомости\t№ акта ремонта\tКатегория\t" +
                        $"№ акта списания\tЦенаТО(без НДС)\tЦена ремонта(без НДС)\tГород\tПримечание\tМесяц выполнения\tДорога";

                    sw.WriteLine(note);

                    var poligon = ""; var company = ""; var location = ""; var model = ""; var serialNumber = "";
                    var inventoryNumber = ""; var networkNumber = ""; var dateTO = ""; var numberAct = "";
                    var numberActRemont = ""; var category = ""; var decommissionSerialNumber = "";
                    var price = ""; var priceRemont = ""; var city = ""; var comment = ""; var month = "";
                    var road = "";

                    for (int i = 0; i < dgw.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgw.ColumnCount; j++)
                        {
                            if (j < dgw.ColumnCount)
                            {
                                if (dgw.Columns[j].HeaderText.ToString() == "Полигон")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    poligon = dgw.Rows[i].Cells[j].Value.ToString();
                                    poligon = re.Replace(poligon, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Предприятие")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    company = dgw.Rows[i].Cells[j].Value.ToString();
                                    company = re.Replace(company, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Место нахождения")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    location = dgw.Rows[i].Cells[j].Value.ToString();
                                    location = re.Replace(location, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Модель радиостанции")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    model = dgw.Rows[i].Cells[j].Value.ToString();
                                    model = re.Replace(model, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Заводской номер")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    serialNumber = dgw.Rows[i].Cells[j].Value.ToString();
                                    serialNumber = re.Replace(serialNumber, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Инвентарный номер")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    inventoryNumber = dgw.Rows[i].Cells[j].Value.ToString();
                                    inventoryNumber = re.Replace(inventoryNumber, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Сетевой номер")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    networkNumber = dgw.Rows[i].Cells[j].Value.ToString();
                                    networkNumber = re.Replace(networkNumber, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Дата ТО")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    dateTO = Convert.ToDateTime(dgw.Rows[i].Cells[j].Value.ToString()).ToString("dd.MM.yyyy");
                                    dateTO = re.Replace(dateTO, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "№ акта ТО")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    numberAct = dgw.Rows[i].Cells[j].Value.ToString();
                                    numberAct = re.Replace(numberAct, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "№ акта ремонта")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    numberActRemont = dgw.Rows[i].Cells[j].Value.ToString();
                                    numberActRemont = re.Replace(numberActRemont, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Категория")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    category = dgw.Rows[i].Cells[j].Value.ToString();
                                    category = re.Replace(category, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "№ акта списания")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    decommissionSerialNumber = dgw.Rows[i].Cells[j].Value.ToString();
                                    decommissionSerialNumber = re.Replace(decommissionSerialNumber, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Цена ТО")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    price = dgw.Rows[i].Cells[j].Value.ToString();
                                    price = re.Replace(price, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Цена ремонта")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    priceRemont = dgw.Rows[i].Cells[j].Value.ToString();
                                    priceRemont = re.Replace(priceRemont, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Город")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    city = dgw.Rows[i].Cells[j].Value.ToString();
                                    city = re.Replace(city, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Примечание")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    comment = dgw.Rows[i].Cells[j].Value.ToString();
                                    comment = re.Replace(comment, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Месяц выполнения")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    month = dgw.Rows[i].Cells[j].Value.ToString();
                                    month = re.Replace(month, " ");
                                }
                                else if (dgw.Columns[j].HeaderText.ToString() == "Дорога")
                                {
                                    var re = new Regex(Environment.NewLine);
                                    road = dgw.Rows[i].Cells[j].Value.ToString();
                                    road = re.Replace(road, " ");
                                }
                            }
                            if (j == dgw.ColumnCount - 1)
                            {
                                if (priceRemont == "0.00")
                                    priceRemont = "";

                                if (!String.IsNullOrEmpty(decommissionSerialNumber))
                                {
                                    numberAct = "списание"; dateTO = "списание"; month = "списание";
                                    if (!String.IsNullOrEmpty(numberActRemont))
                                    {
                                        numberActRemont = "";
                                        category = "";
                                        priceRemont = "";
                                    }
                                }
                                sw.Write(poligon + "\t" + company + "\t" + location + "\t" + model + "\t" + serialNumber + "\t" + inventoryNumber + "\t"
                                    + networkNumber + "\t" + dateTO + "\t" + numberAct + "\t" + numberAct + "\t" + numberAct + "\t" + numberActRemont + "\t"
                                    + category + "\t" + decommissionSerialNumber + "\t" + price + "\t" + priceRemont + "\t" + city + "\t" + comment + "\t"
                                    + month + "\t" + road);
                            }
                        }
                        sw.WriteLine();
                    }
                    MessageBox.Show("Файл успешно сохранен");
                }
            }
            else MessageBox.Show("Вы не указали путь сохранения!");

        }

        internal static void SaveFullBasePC(DataGridView dgw, string cmB_city)
        {
            DateTime dateTime = DateTime.Now;
            string dateTimeString = dateTime.ToString("dd.MM.yyyy");
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            sfd.FileName = $"ОБЩАЯ База_{cmB_city}_{dateTimeString}";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Unicode))
                {
                    string note = string.Empty;

                    note += $"Полигон\tПредприятие\tМесто нахождения\tМодель\tЗаводской номер\t" +
                        $"Инвентарный номер\tСетевой номер\tДата проведения ТО\tНомер акта\tГород\tЦена ТО\t" +
                        $"Представитель предприятия\tДолжность\tНомер удостоверения\tДата выдачи\tНомер телефона\t" +
                        $"Номер Акта ремонта\tКатегория\tЦена ремонта\tАнтенна\tМанипулятор\tАКБ\tЗУ\tВыполненные работы_1\t" +
                        $"Выполненные работы_2\tВыполненные работы_3\tВыполненные работы_4\tВыполненные работы_5\t" +
                        $"Выполненные работы_6\tВыполненные работы_7\tДеталь_1\tДеталь_2\tДеталь_3\tДеталь_4\tДеталь_5\t" +
                        $"Деталь_6\tДеталь_7\t№ Акта списания\tПримечания\tДорога";

                    sw.WriteLine(note);

                    for (int i = 0; i < dgw.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgw.ColumnCount; j++)
                        {
                            var re = new Regex(Environment.NewLine);
                            var value = dgw.Rows[i].Cells[j].Value.ToString();
                            value = re.Replace(value, " ");
                            if (dgw.Columns[j].HeaderText.ToString() == "№")
                            {

                            }
                            else if (dgw.Columns[j].HeaderText.ToString() == "Дата ТО")
                                sw.Write(Convert.ToDateTime(value).ToString("dd.MM.yyyy") + "\t");
                            else if (dgw.Columns[j].HeaderText.ToString() == "Дорога")
                                sw.Write(value);
                            else if (dgw.Columns[j].HeaderText.ToString() == "RowState")
                            {

                            }
                            else sw.Write(value + "\t");
                        }
                        sw.WriteLine();
                    }

                }
                MessageBox.Show("Файл успешно сохранен!");
            }
        }
        #endregion

        #region Сохранение Бд по Таймеру 

        internal static void AutoSaveFilePC(DataGridView dgw, string city)
        {

            DateTime today = DateTime.Today;

            if (File.Exists($@"C:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\БазаДанных-{city}-{today.ToString("dd.MM.yyyy")}.csv"))
                File.Delete($@"C:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\БазаДанных-{city}-{today.ToString("dd.MM.yyyy")}.csv");

            string fileNamePath = $@"C:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\БазаДанных-{city}-{today.ToString("dd.MM.yyyy")}.csv";

            if (!File.Exists($@"С:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\"))
                Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\");

            using (StreamWriter sw = new StreamWriter(fileNamePath, false, Encoding.Unicode))
            {
                string note = string.Empty;

                note += $"Полигон\tПредприятие\tМесто нахождения\tМодель\tЗаводской номер\t" +
                    $"Инвентарный номер\tСетевой номер\tДата проведения ТО\tНомер акта\tГород\tЦена ТО\t" +
                    $"Представитель предприятия\tДолжность\tНомер удостоверения\tДата выдачи\tНомер телефона\t" +
                    $"Номер Акта ремонта\tКатегория\tЦена ремонта\tАнтенна\tМанипулятор\tАКБ\tЗУ\tВыполненные работы_1\t" +
                    $"Выполненные работы_2\tВыполненные работы_3\tВыполненные работы_4\tВыполненные работы_5\t" +
                    $"Выполненные работы_6\tВыполненные работы_7\tДеталь_1\tДеталь_2\tДеталь_3\tДеталь_4\tДеталь_5\t" +
                    $"Деталь_6\tДеталь_7\t№ Акта списания\tПримечания\tДорога";

                sw.WriteLine(note);

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    for (int j = 0; j < dgw.ColumnCount; j++)
                    {
                        var re = new Regex(Environment.NewLine);
                        var value = dgw.Rows[i].Cells[j].Value.ToString();
                        value = re.Replace(value, " ");
                        if (dgw.Columns[j].HeaderText.ToString() == "№")
                        {

                        }
                        else if (dgw.Columns[j].HeaderText.ToString() == "Дата ТО")
                            sw.Write(Convert.ToDateTime(value).ToString("dd.MM.yyyy") + "\t");
                        else if (dgw.Columns[j].HeaderText.ToString() == "Дорога")
                            sw.Write(value);
                        else if (dgw.Columns[j].HeaderText.ToString() == "RowState")
                        {

                        }
                        else sw.Write(value + "\t");
                    }
                    sw.WriteLine();
                }
            }
        }

        internal static void AutoSaveFileCurator(DataGridView dgw, string road)
        {
            DateTime today = DateTime.Today;

            if (File.Exists($@"C:\Documents_ServiceTelekom\Куратор\БазаДанныхExcel\{road}\БазаДанных-{road}-{today.ToString("dd.MM.yyyy")}.csv"))
                File.Delete($@"C:\Documents_ServiceTelekom\Куратор\БазаДанныхExcel\{road}\БазаДанных-{road}-{today.ToString("dd.MM.yyyy")}.csv");

            string fileNamePath = $@"C:\Documents_ServiceTelekom\Куратор\БазаДанныхExcel\{road}\БазаДанных-{road}-{today.ToString("dd.MM.yyyy")}.csv";

            if (!File.Exists($@"С:\Documents_ServiceTelekom\Куратор\БазаДанныхExcel\{road}\"))
                Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\Куратор\БазаДанныхExcel\{road}\");

            using (StreamWriter sw = new StreamWriter(fileNamePath, false, Encoding.Unicode))
            {
                string note = string.Empty;

                note += $"Номер\tПолигон\tПредприятие\tМесто нахождения\tМодель\tЗаводской номер\t" +
                    $"Инвентарный номер\tСетевой номер\tДата проведения ТО\tНомер акта\tГород\tЦена ТО\t" +
                    $"Номер Акта ремонта\tКатегория\tЦена ремонта\t№ Акта списания\tПримечания\tМесяц выполнения\tДорога";

                sw.WriteLine(note);

                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    for (int j = 0; j < dgw.ColumnCount; j++)
                    {
                        var re = new Regex(Environment.NewLine);
                        var value = dgw.Rows[i].Cells[j].Value.ToString();
                        value = re.Replace(value, " ");
                        if (dgw.Columns[j].HeaderText.ToString() == "№")
                        {

                        }
                        else if (dgw.Columns[j].HeaderText.ToString() == "Дата ТО")
                            sw.Write(Convert.ToDateTime(value).ToString("dd.MM.yyyy") + "\t");
                        else if (dgw.Columns[j].HeaderText.ToString() == "Дорога")
                            sw.Write(value);
                        else if (dgw.Columns[j].HeaderText.ToString() == "RowState")
                        {

                        }
                        else sw.Write(value + "\t");
                    }
                    sw.WriteLine();
                }
            }
        }

        #endregion

    }
}
