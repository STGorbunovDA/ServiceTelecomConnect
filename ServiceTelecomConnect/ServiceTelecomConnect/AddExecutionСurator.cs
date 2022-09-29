using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                            if(inventoryNumber == "списание" || networkNumber == "списание" || string.IsNullOrEmpty(decommissionSerialNumber) 
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

                            if (months == "Январь")
                            {
                                var january = "Январь";
                                var february = "";
                                var march = "";
                                var april = "";
                                var may = "";
                                var june = "";
                                var july = "";
                                var august = "";
                                var september = "";
                                var october = "";
                                var november = "";
                                var december = "";

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, january, february, march, april, may," +
                                    $"june, july, august, september, october, november, december) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{january.Trim()}', '{february.Trim()}', '{march.Trim()}', '{april.Trim()}'," +
                                    $"'{may.Trim()}', '{june.Trim()}', '{july.Trim()}', '{august.Trim()}', '{september.Trim()}', '{october.Trim()}', '{november.Trim()}'," +
                                    $"'{december.Trim()}')";
                            }
                            if (months == "Февраль")
                            {
                                var january = "";
                                var february = "Февраль";
                                var march = "";
                                var april = "";
                                var may = "";
                                var june = "";
                                var july = "";
                                var august = "";
                                var september = "";
                                var october = "";
                                var november = "";
                                var december = "";

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, january, february, march, april, may," +
                                    $"june, july, august, september, october, november, december) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{january.Trim()}', '{february.Trim()}', '{march.Trim()}', '{april.Trim()}'," +
                                    $"'{may.Trim()}', '{june.Trim()}', '{july.Trim()}', '{august.Trim()}', '{september.Trim()}', '{october.Trim()}', '{november.Trim()}'," +
                                    $"'{december.Trim()}')";
                            }
                            if (months == "Март")
                            {
                                var january = "";
                                var february = "";
                                var march = "Март";
                                var april = "";
                                var may = "";
                                var june = "";
                                var july = "";
                                var august = "";
                                var september = "";
                                var october = "";
                                var november = "";
                                var december = "";

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, january, february, march, april, may," +
                                    $"june, july, august, september, october, november, december) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{january.Trim()}', '{february.Trim()}', '{march.Trim()}', '{april.Trim()}'," +
                                    $"'{may.Trim()}', '{june.Trim()}', '{july.Trim()}', '{august.Trim()}', '{september.Trim()}', '{october.Trim()}', '{november.Trim()}'," +
                                    $"'{december.Trim()}')";
                            }
                            if (months == "Апрель")
                            {
                                var january = "";
                                var february = "";
                                var march = "";
                                var april = "Апрель";
                                var may = "";
                                var june = "";
                                var july = "";
                                var august = "";
                                var september = "";
                                var october = "";
                                var november = "";
                                var december = "";

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, january, february, march, april, may," +
                                    $"june, july, august, september, october, november, december) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{january.Trim()}', '{february.Trim()}', '{march.Trim()}', '{april.Trim()}'," +
                                    $"'{may.Trim()}', '{june.Trim()}', '{july.Trim()}', '{august.Trim()}', '{september.Trim()}', '{october.Trim()}', '{november.Trim()}'," +
                                    $"'{december.Trim()}')";
                            }
                            if (months == "Май")
                            {
                                var january = "";
                                var february = "";
                                var march = "";
                                var april = "";
                                var may = "Май";
                                var june = "";
                                var july = "";
                                var august = "";
                                var september = "";
                                var october = "";
                                var november = "";
                                var december = "";

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, january, february, march, april, may," +
                                    $"june, july, august, september, october, november, december) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{january.Trim()}', '{february.Trim()}', '{march.Trim()}', '{april.Trim()}'," +
                                    $"'{may.Trim()}', '{june.Trim()}', '{july.Trim()}', '{august.Trim()}', '{september.Trim()}', '{october.Trim()}', '{november.Trim()}'," +
                                    $"'{december.Trim()}')";
                            }
                            if (months == "Июнь")
                            {
                                var january = "";
                                var february = "";
                                var march = "";
                                var april = "";
                                var may = "";
                                var june = "Июнь";
                                var july = "";
                                var august = "";
                                var september = "";
                                var october = "";
                                var november = "";
                                var december = "";

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, january, february, march, april, may," +
                                    $"june, july, august, september, october, november, december) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{january.Trim()}', '{february.Trim()}', '{march.Trim()}', '{april.Trim()}'," +
                                    $"'{may.Trim()}', '{june.Trim()}', '{july.Trim()}', '{august.Trim()}', '{september.Trim()}', '{october.Trim()}', '{november.Trim()}'," +
                                    $"'{december.Trim()}')";
                            }
                            if (months == "Июль")
                            {
                                var january = "";
                                var february = "";
                                var march = "";
                                var april = "";
                                var may = "";
                                var june = "";
                                var july = "Июль";
                                var august = "";
                                var september = "";
                                var october = "";
                                var november = "";
                                var december = "";

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, january, february, march, april, may," +
                                    $"june, july, august, september, october, november, december) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{january.Trim()}', '{february.Trim()}', '{march.Trim()}', '{april.Trim()}'," +
                                    $"'{may.Trim()}', '{june.Trim()}', '{july.Trim()}', '{august.Trim()}', '{september.Trim()}', '{october.Trim()}', '{november.Trim()}'," +
                                    $"'{december.Trim()}')";
                            }
                            if (months == "Август")
                            {
                                var january = "";
                                var february = "";
                                var march = "";
                                var april = "";
                                var may = "";
                                var june = "";
                                var july = "";
                                var august = "Август";
                                var september = "";
                                var october = "";
                                var november = "";
                                var december = "";

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, january, february, march, april, may," +
                                    $"june, july, august, september, october, november, december) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{january.Trim()}', '{february.Trim()}', '{march.Trim()}', '{april.Trim()}'," +
                                    $"'{may.Trim()}', '{june.Trim()}', '{july.Trim()}', '{august.Trim()}', '{september.Trim()}', '{october.Trim()}', '{november.Trim()}'," +
                                    $"'{december.Trim()}')";
                            }
                            if (months == "Сентябрь")
                            {
                                var january = "";
                                var february = "";
                                var march = "";
                                var april = "";
                                var may = "";
                                var june = "";
                                var july = "";
                                var august = "";
                                var september = "Сентябрь";
                                var october = "";
                                var november = "";
                                var december = "";

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, january, february, march, april, may," +
                                    $"june, july, august, september, october, november, december) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{january.Trim()}', '{february.Trim()}', '{march.Trim()}', '{april.Trim()}'," +
                                    $"'{may.Trim()}', '{june.Trim()}', '{july.Trim()}', '{august.Trim()}', '{september.Trim()}', '{october.Trim()}', '{november.Trim()}'," +
                                    $"'{december.Trim()}')";
                            }
                            if (months == "Октябрь")
                            {
                                var january = "";
                                var february = "";
                                var march = "";
                                var april = "";
                                var may = "";
                                var june = "";
                                var july = "";
                                var august = "";
                                var september = "";
                                var october = "Октябрь";
                                var november = "";
                                var december = "";

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, january, february, march, april, may," +
                                    $"june, july, august, september, october, november, december) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{january.Trim()}', '{february.Trim()}', '{march.Trim()}', '{april.Trim()}'," +
                                    $"'{may.Trim()}', '{june.Trim()}', '{july.Trim()}', '{august.Trim()}', '{september.Trim()}', '{october.Trim()}', '{november.Trim()}'," +
                                    $"'{december.Trim()}')";
                            }
                            if (months == "Ноябрь")
                            {
                                var january = "";
                                var february = "";
                                var march = "";
                                var april = "";
                                var may = "";
                                var june = "";
                                var july = "";
                                var august = "";
                                var september = "";
                                var october = "";
                                var november = "Ноябрь";
                                var december = "";

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, january, february, march, april, may," +
                                    $"june, july, august, september, october, november, december) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{january.Trim()}', '{february.Trim()}', '{march.Trim()}', '{april.Trim()}'," +
                                    $"'{may.Trim()}', '{june.Trim()}', '{july.Trim()}', '{august.Trim()}', '{september.Trim()}', '{october.Trim()}', '{november.Trim()}'," +
                                    $"'{december.Trim()}')";
                            }
                            if (months == "Декабрь")
                            {
                                var january = "";
                                var february = "";
                                var march = "";
                                var april = "";
                                var may = "";
                                var june = "";
                                var july = "";
                                var august = "";
                                var september = "";
                                var october = "";
                                var november = "";
                                var december = "Декабрь";

                                AddExecutionQuery = $"INSERT INTO radiostantion_сomparison (poligon, company, location, model," +
                                    $"serialNumber, inventoryNumber, networkNumber, dateTO, numberAct, city, price, numberActRemont," +
                                    $"category, priceRemont, decommissionSerialNumber, comment, january, february, march, april, may," +
                                    $"june, july, august, september, october, november, december) VALUES ('{poligon.Trim()}', '{company.Trim()}'," +
                                    $"'{location.Trim()}', '{model.Trim()}', '{serialNumber.Trim()}', '{inventoryNumber.Trim()}', '{networkNumber.Trim()}'," +
                                    $"'{dateTO.Trim()}', '{numberAct.Trim()}', '{city.Trim()}', '{price.Trim()}', '{numberActRemont.Trim()}', '{category.Trim()}', '{priceRemont.Trim()}'," +
                                    $"'{decommissionSerialNumber.Trim()}', '{comment.Trim()}', '{january.Trim()}', '{february.Trim()}', '{march.Trim()}', '{april.Trim()}'," +
                                    $"'{may.Trim()}', '{june.Trim()}', '{july.Trim()}', '{august.Trim()}', '{september.Trim()}', '{october.Trim()}', '{november.Trim()}'," +
                                    $"'{december.Trim()}')";
                            }
                            using (MySqlCommand command = new MySqlCommand(AddExecutionQuery, DB.GetInstance.GetConnection()))
                            {
                                DB.GetInstance.OpenConnection();
                                command.ExecuteNonQuery();
                                DB.GetInstance.CloseConnection();
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

        #endregion
    }
}
