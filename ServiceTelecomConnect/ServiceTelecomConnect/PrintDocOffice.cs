using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ServiceTelecomConnect
{
    class PrintDocOffice
    {
        /// <summary>
        /// АКТ ТО
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void PrintExcelActTo(DataGridView dgw, string numberAct, string dateTO,
            string company, string location, string FIO_chief, string post, string representative,
            string numberIdentification, string FIO_Engineer, string doverennost, string polinon_full,
            string dateIssue, string city, string poligon)
        {
            Excel.Application exApp = new Excel.Application();

            try
            {
                if (numberAct != "")
                {
                    if (dgw.Rows.Count > 21)
                    {
                        string Mesage2;

                        Mesage2 = "В акте может быть только 20-ать радиостанций! Нажмите \"Enter\" или \"два раза мышью\" на панель в графе \"Акт №:\"";

                        if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        Type officeType = Type.GetTypeFromProgID("Excel.Application");

                        if (officeType == null)
                        {
                            string Mesage2 = "У Вас не установлен Excel!";

                            if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                            {
                                return;
                            }
                        }
                        else
                        {
                            exApp.SheetsInNewWorkbook = 3;
                            exApp.Workbooks.Add();
                            exApp.DisplayAlerts = false;

                            Excel.Worksheet workSheet = (Excel.Worksheet)exApp.Worksheets.get_Item(1);
                            Excel.Worksheet workSheet2 = (Excel.Worksheet)exApp.Worksheets.get_Item(2);
                            Excel.Worksheet workSheet3 = (Excel.Worksheet)exApp.Worksheets.get_Item(3);

                            workSheet.Name = $"Накладная №{numberAct.Replace('/', '.')}";
                            workSheet2.Name = $"Ведомость №{numberAct.Replace('/', '.')}";
                            workSheet3.Name = $"Акт №{numberAct.Replace('/', '.')}";


                            #region Накладная ТО 1 Item

                            workSheet.PageSetup.Zoom = false;
                            workSheet.PageSetup.FitToPagesWide = 1;
                            workSheet.PageSetup.FitToPagesTall = 1;

                            workSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;

                            workSheet.Rows.Font.Size = 9.5;
                            workSheet.Rows.Font.Name = "Times New Roman";

                            //workSheet.PageSetup.PrintArea = "A1:N39";

                            Excel.Range _excelCells2 = (Excel.Range)workSheet.get_Range("E1", "G1").Cells;
                            Excel.Range _excelCells1 = (Excel.Range)workSheet.get_Range("M1", "O1").Cells;
                            Excel.Range _excelCells3 = (Excel.Range)workSheet.get_Range("C3", "D3").Cells;
                            Excel.Range _excelCells4 = (Excel.Range)workSheet.get_Range("K3", "L3").Cells;
                            Excel.Range _excelCells5 = (Excel.Range)workSheet.get_Range("A5", "B5").Cells;
                            Excel.Range _excelCells6 = (Excel.Range)workSheet.get_Range("C5", "D5").Cells;
                            Excel.Range _excelCells7 = (Excel.Range)workSheet.get_Range("E5", "G5").Cells;
                            Excel.Range _excelCells8 = (Excel.Range)workSheet.get_Range("I5", "J5").Cells;
                            Excel.Range _excelCells9 = (Excel.Range)workSheet.get_Range("E3").Cells;
                            Excel.Range _excelCells10 = (Excel.Range)workSheet.get_Range("M3").Cells;
                            Excel.Range _excelCells11 = (Excel.Range)workSheet.get_Range("K5", "L5").Cells;
                            Excel.Range _excelCells12 = (Excel.Range)workSheet.get_Range("M5", "O5").Cells;
                            Excel.Range _excelCells13 = (Excel.Range)workSheet.get_Range("A6", "B6").Cells;
                            Excel.Range _excelCells14 = (Excel.Range)workSheet.get_Range("C6", "D6").Cells;
                            Excel.Range _excelCells15 = (Excel.Range)workSheet.get_Range("I6", "J6").Cells;
                            Excel.Range _excelCells16 = (Excel.Range)workSheet.get_Range("K6", "L6").Cells;
                            Excel.Range _excelCells17 = (Excel.Range)workSheet.get_Range("A7", "A27").Cells;
                            Excel.Range _excelCells18 = (Excel.Range)workSheet.get_Range("B7", "C7").Cells;
                            Excel.Range _excelCells19 = (Excel.Range)workSheet.get_Range("A7", "O7").Cells;
                            Excel.Range _excelCells20 = (Excel.Range)workSheet.get_Range("I7", "I27").Cells;
                            Excel.Range _excelCells21 = (Excel.Range)workSheet.get_Range("J7", "K7").Cells;
                            Excel.Range _excelCells32 = (Excel.Range)workSheet.get_Range("A8", "O27").Cells;
                            Excel.Range _excelCells33 = (Excel.Range)workSheet.get_Range("A7", "G27").Cells;
                            Excel.Range _excelCells34 = (Excel.Range)workSheet.get_Range("I7", "O27").Cells;
                            Excel.Range _excelCells35 = (Excel.Range)workSheet.get_Range("A28", "G28").Cells;
                            Excel.Range _excelCells36 = (Excel.Range)workSheet.get_Range("I28", "O28").Cells;
                            Excel.Range _excelCells37 = (Excel.Range)workSheet.get_Range("A29", "C30").Cells;
                            Excel.Range _excelCells38 = (Excel.Range)workSheet.get_Range("A31", "C31").Cells;
                            Excel.Range _excelCells39 = (Excel.Range)workSheet.get_Range("A33", "C33").Cells;
                            Excel.Range _excelCells40 = (Excel.Range)workSheet.get_Range("B33", "C33").Cells;
                            Excel.Range _excelCells41 = (Excel.Range)workSheet.get_Range("B34", "C34").Cells;
                            Excel.Range _excelCells42 = (Excel.Range)workSheet.get_Range("A36", "C37").Cells;
                            Excel.Range _excelCells43 = (Excel.Range)workSheet.get_Range("A38", "C38").Cells;
                            Excel.Range _excelCells44 = (Excel.Range)workSheet.get_Range("A40", "C40").Cells;
                            Excel.Range _excelCells45 = (Excel.Range)workSheet.get_Range("B40", "C40").Cells;
                            Excel.Range _excelCells46 = (Excel.Range)workSheet.get_Range("B41", "C41").Cells;
                            Excel.Range _excelCells47 = (Excel.Range)workSheet.get_Range("E29", "G30").Cells;
                            Excel.Range _excelCells48 = (Excel.Range)workSheet.get_Range("E31", "G31").Cells;
                            Excel.Range _excelCells49 = (Excel.Range)workSheet.get_Range("E33", "G33").Cells;
                            Excel.Range _excelCells50 = (Excel.Range)workSheet.get_Range("F33", "G33").Cells;
                            Excel.Range _excelCells51 = (Excel.Range)workSheet.get_Range("F34", "G34").Cells;
                            Excel.Range _excelCells52 = (Excel.Range)workSheet.get_Range("E36", "G37").Cells;
                            Excel.Range _excelCells53 = (Excel.Range)workSheet.get_Range("E38", "G38").Cells;
                            Excel.Range _excelCells54 = (Excel.Range)workSheet.get_Range("E40", "G40").Cells;
                            Excel.Range _excelCells55 = (Excel.Range)workSheet.get_Range("F40", "G40").Cells;
                            Excel.Range _excelCells56 = (Excel.Range)workSheet.get_Range("F41", "G41").Cells;
                            Excel.Range _excelCells57 = (Excel.Range)workSheet.get_Range("D42", "G42").Cells;
                            Excel.Range _excelCells58 = (Excel.Range)workSheet.get_Range("D43", "G43").Cells;
                            Excel.Range _excelCells59 = (Excel.Range)workSheet.get_Range("I29", "K30").Cells;
                            Excel.Range _excelCells60 = (Excel.Range)workSheet.get_Range("I31", "K31").Cells;
                            Excel.Range _excelCells61 = (Excel.Range)workSheet.get_Range("I33", "K33").Cells;
                            Excel.Range _excelCells62 = (Excel.Range)workSheet.get_Range("J33", "K33").Cells;
                            Excel.Range _excelCells63 = (Excel.Range)workSheet.get_Range("J34", "K34").Cells;
                            Excel.Range _excelCells64 = (Excel.Range)workSheet.get_Range("I36", "K37").Cells;
                            Excel.Range _excelCells65 = (Excel.Range)workSheet.get_Range("I38", "K38").Cells;
                            Excel.Range _excelCells66 = (Excel.Range)workSheet.get_Range("I40", "K40").Cells;
                            Excel.Range _excelCells67 = (Excel.Range)workSheet.get_Range("J40", "K40").Cells;
                            Excel.Range _excelCells68 = (Excel.Range)workSheet.get_Range("J41", "K41").Cells;
                            Excel.Range _excelCells69 = (Excel.Range)workSheet.get_Range("M29", "O30").Cells;
                            Excel.Range _excelCells70 = (Excel.Range)workSheet.get_Range("M31", "O31").Cells;
                            Excel.Range _excelCells71 = (Excel.Range)workSheet.get_Range("M33", "O33").Cells;
                            Excel.Range _excelCells72 = (Excel.Range)workSheet.get_Range("N33", "O33").Cells;
                            Excel.Range _excelCells73 = (Excel.Range)workSheet.get_Range("M36", "O37").Cells;
                            Excel.Range _excelCells74 = (Excel.Range)workSheet.get_Range("M38", "O38").Cells;
                            Excel.Range _excelCells75 = (Excel.Range)workSheet.get_Range("M40", "O40").Cells;
                            Excel.Range _excelCells76 = (Excel.Range)workSheet.get_Range("N40", "O40").Cells;
                            Excel.Range _excelCells77 = (Excel.Range)workSheet.get_Range("N41", "O41").Cells;
                            Excel.Range _excelCells78 = (Excel.Range)workSheet.get_Range("L42", "O42").Cells;
                            Excel.Range _excelCells79 = (Excel.Range)workSheet.get_Range("L43", "O43").Cells;

                            _excelCells1.Merge(Type.Missing);
                            _excelCells2.Merge(Type.Missing);
                            _excelCells3.Merge(Type.Missing);
                            _excelCells4.Merge(Type.Missing);
                            _excelCells5.Merge(Type.Missing);
                            _excelCells6.Merge(Type.Missing);
                            _excelCells7.Merge(Type.Missing);
                            _excelCells8.Merge(Type.Missing);
                            _excelCells11.Merge(Type.Missing);
                            _excelCells12.Merge(Type.Missing);
                            _excelCells13.Merge(Type.Missing);
                            _excelCells14.Merge(Type.Missing);
                            _excelCells15.Merge(Type.Missing);
                            _excelCells16.Merge(Type.Missing);
                            _excelCells18.Merge(Type.Missing);
                            _excelCells21.Merge(Type.Missing);
                            _excelCells35.Merge(Type.Missing);
                            _excelCells36.Merge(Type.Missing);
                            _excelCells37.Merge(Type.Missing);
                            _excelCells38.Merge(Type.Missing);
                            _excelCells40.Merge(Type.Missing);
                            _excelCells41.Merge(Type.Missing);
                            _excelCells42.Merge(Type.Missing);
                            _excelCells43.Merge(Type.Missing);
                            _excelCells45.Merge(Type.Missing);
                            _excelCells46.Merge(Type.Missing);
                            _excelCells47.Merge(Type.Missing);
                            _excelCells48.Merge(Type.Missing);
                            _excelCells50.Merge(Type.Missing);
                            _excelCells51.Merge(Type.Missing);
                            _excelCells52.Merge(Type.Missing);
                            _excelCells53.Merge(Type.Missing);
                            _excelCells55.Merge(Type.Missing);
                            _excelCells56.Merge(Type.Missing);
                            _excelCells57.Merge(Type.Missing);
                            _excelCells58.Merge(Type.Missing);
                            _excelCells59.Merge(Type.Missing);
                            _excelCells60.Merge(Type.Missing);
                            _excelCells62.Merge(Type.Missing);
                            _excelCells63.Merge(Type.Missing);
                            _excelCells64.Merge(Type.Missing);
                            _excelCells65.Merge(Type.Missing);
                            _excelCells67.Merge(Type.Missing);
                            _excelCells68.Merge(Type.Missing);
                            _excelCells69.Merge(Type.Missing);
                            _excelCells70.Merge(Type.Missing);
                            _excelCells72.Merge(Type.Missing);
                            _excelCells73.Merge(Type.Missing);
                            _excelCells74.Merge(Type.Missing);
                            _excelCells76.Merge(Type.Missing);
                            _excelCells77.Merge(Type.Missing);
                            _excelCells78.Merge(Type.Missing);
                            _excelCells79.Merge(Type.Missing);

                            _excelCells1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells4.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells5.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells6.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells7.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells8.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells11.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells12.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells13.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells14.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells15.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells16.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells17.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells18.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells19.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells19.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells20.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells21.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells32.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells35.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells36.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells38.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells40.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells41.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells43.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells45.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells46.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells47.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells48.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells50.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells51.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells52.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells53.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells55.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells56.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells57.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells58.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells59.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells60.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells62.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells63.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells64.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells65.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells67.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells68.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells69.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells70.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells72.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells73.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells74.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells76.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells77.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells78.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells79.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells1.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells2.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells9.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells10.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells14.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells16.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells37.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells39.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells42.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells44.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells47.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells49.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells52.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells54.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells59.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells61.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells64.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells66.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells69.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells71.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells73.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells75.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells33.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells33.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells34.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells34.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells34.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells34.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells34.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells34.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                            Excel.Range rowColum = workSheet.get_Range("A7", "A27");
                            rowColum.EntireColumn.ColumnWidth = 5; //

                            Excel.Range rowHeight = workSheet.get_Range("A7", "O7");
                            rowHeight.EntireRow.RowHeight = 25; //   

                            Excel.Range rowColum2 = workSheet.get_Range("I7", "I27");
                            rowColum2.EntireColumn.ColumnWidth = 5; //

                            Excel.Range rowColum3 = workSheet.get_Range("D7", "D27");
                            rowColum3.EntireColumn.ColumnWidth = 15; //

                            Excel.Range rowColum4 = workSheet.get_Range("L7", "L27");
                            rowColum4.EntireColumn.ColumnWidth = 15; //

                            Excel.Range rowColum5 = workSheet.get_Range("B7", "C7");
                            rowColum5.EntireColumn.ColumnWidth = 10; //

                            Excel.Range rowColum6 = workSheet.get_Range("J7", "K7");
                            rowColum6.EntireColumn.ColumnWidth = 10; //

                            Excel.Range rowHeight2 = workSheet.get_Range("A31", "O31");
                            rowHeight2.EntireRow.RowHeight = 10; //

                            Excel.Range rowHeight3 = workSheet.get_Range("A34", "O34");
                            rowHeight3.EntireRow.RowHeight = 10; //

                            Excel.Range rowHeight4 = workSheet.get_Range("A38", "O38");
                            rowHeight4.EntireRow.RowHeight = 10; //

                            Excel.Range rowHeight5 = workSheet.get_Range("A41", "O41");
                            rowHeight5.EntireRow.RowHeight = 10; //

                            Excel.Range range_Consolidated = workSheet.Rows.get_Range("E1", "O1");
                            Excel.Range range_Consolidated2 = workSheet.Rows.get_Range("C3", "M3");
                            Excel.Range range_Consolidated3 = workSheet.Rows.get_Range("A7", "O7");
                            Excel.Range range_Consolidated4 = workSheet.Rows.get_Range("A8", "O27");
                            Excel.Range range_Consolidated5 = workSheet.Rows.get_Range("A28", "O28");
                            Excel.Range range_Consolidated6 = workSheet.Rows.get_Range("A31", "O31");
                            Excel.Range range_Consolidated7 = workSheet.Rows.get_Range("A34", "O34");
                            Excel.Range range_Consolidated8 = workSheet.Rows.get_Range("A38", "O38");
                            Excel.Range range_Consolidated9 = workSheet.Rows.get_Range("A41", "O41");
                            Excel.Range range_Consolidated10 = workSheet.Rows.get_Range("A42", "O43");
                            Excel.Range range_Consolidated11 = workSheet.Rows.get_Range("B8", "C27");
                            Excel.Range range_Consolidated12 = workSheet.Rows.get_Range("J8", "K27");
                            Excel.Range range_Consolidated13 = workSheet.Rows.get_Range("E29", "G30");
                            Excel.Range range_Consolidated14 = workSheet.Rows.get_Range("E36", "G37");
                            Excel.Range range_Consolidated15 = workSheet.Rows.get_Range("M29", "O30");
                            Excel.Range range_Consolidated16 = workSheet.Rows.get_Range("M36", "O37");

                            range_Consolidated.Font.Bold = true;
                            range_Consolidated.Font.Size = 10;
                            range_Consolidated2.Font.Bold = true;
                            range_Consolidated2.Font.Size = 12;
                            range_Consolidated3.Font.Bold = true;
                            range_Consolidated4.Font.Size = 8;
                            range_Consolidated5.Font.Size = 7;
                            range_Consolidated6.Font.Size = 7;
                            range_Consolidated7.Font.Size = 7;
                            range_Consolidated8.Font.Size = 7;
                            range_Consolidated9.Font.Size = 7;
                            range_Consolidated10.Font.Size = 8;
                            range_Consolidated10.Font.Bold = true;
                            range_Consolidated11.NumberFormat = "@";
                            range_Consolidated12.NumberFormat = "@";
                            range_Consolidated13.Font.Size = 7;
                            range_Consolidated13.Style.WrapText = true;
                            range_Consolidated14.Font.Size = 7;
                            range_Consolidated14.Style.WrapText = true;
                            range_Consolidated15.Font.Size = 7;
                            range_Consolidated15.Style.WrapText = true;
                            range_Consolidated16.Font.Size = 7;
                            range_Consolidated16.Style.WrapText = true;

                            workSheet.Cells[1, 5] = $"{dateTO.Remove(dateTO.IndexOf(" "))}";
                            workSheet.Cells[1, 13] = $"{dateTO.Remove(dateTO.IndexOf(" "))}";
                            workSheet.Cells[3, 3] = $"НАКЛАДНАЯ №";
                            workSheet.Cells[3, 5] = $"{numberAct}";
                            workSheet.Cells[3, 11] = $"НАКЛАДНАЯ №";
                            workSheet.Cells[3, 13] = $"{numberAct}";
                            workSheet.Cells[5, 1] = $"От кого";
                            workSheet.Cells[5, 3] = $"{company}";
                            workSheet.Cells[5, 5] = $"{location}";
                            workSheet.Cells[5, 9] = $"От кого";
                            workSheet.Cells[5, 11] = $"{company}";
                            workSheet.Cells[5, 13] = $"{location}";
                            workSheet.Cells[6, 1] = $"Кому";
                            workSheet.Cells[6, 3] = $" ООО \"СервисТелеком\"";
                            workSheet.Cells[6, 9] = $"Кому";
                            workSheet.Cells[6, 11] = $" ООО \"СервисТелеком\"";
                            workSheet.Cells[7, 1] = $"№";
                            workSheet.Cells[7, 2] = $"Заводской номер";
                            workSheet.Cells[7, 4] = $"№ АКБ";
                            workSheet.Cells[7, 5] = $"ЗУ\n(шт.)";
                            workSheet.Cells[7, 6] = $"АНТ\n(шт.)";
                            workSheet.Cells[7, 7] = $"МАН\n(шт.)";
                            workSheet.Cells[7, 9] = $"№";
                            workSheet.Cells[7, 10] = $"Заводской номер";
                            workSheet.Cells[7, 12] = $"№ АКБ";
                            workSheet.Cells[7, 13] = $"ЗУ\n(шт.)";
                            workSheet.Cells[7, 14] = $"АНТ\n(шт.)";
                            workSheet.Cells[7, 15] = $"МАН\n(шт.)";
                            workSheet.Cells[28, 1] = $"Комплектность и работоспособность проверены в присутствии Заказчика. Знаки соответствия нанесены.";
                            workSheet.Cells[28, 9] = $"Комплектность и работоспособность проверены в присутствии Заказчика. Знаки соответствия нанесены.";
                            workSheet.Cells[29, 1] = $"Принял: Начальник участка\n по ТО и ремонту СРС";
                            workSheet.Cells[31, 1] = $"должность";
                            workSheet.Cells[33, 2] = $"{FIO_chief}";
                            workSheet.Cells[34, 1] = $"подпись";
                            workSheet.Cells[34, 2] = $"расшифровка подписи";
                            workSheet.Cells[36, 1] = $"Сдал: Начальник участка\n по ТО и ремонту СРС";
                            workSheet.Cells[38, 1] = $"должность";
                            workSheet.Cells[40, 2] = $"{FIO_chief}";
                            workSheet.Cells[41, 1] = $"подпись";
                            workSheet.Cells[41, 2] = $"расшифровка подписи";
                            workSheet.Cells[29, 5] = $"Сдал: {post}";
                            workSheet.Cells[31, 5] = $"должность";
                            workSheet.Cells[33, 6] = $"{representative}";
                            workSheet.Cells[34, 5] = $"подпись";
                            workSheet.Cells[34, 6] = $"расшифровка подписи";
                            workSheet.Cells[36, 5] = $"Принял: {post}";
                            workSheet.Cells[38, 5] = $"должность";
                            workSheet.Cells[40, 6] = $"{representative}";
                            workSheet.Cells[41, 5] = $"подпись";
                            workSheet.Cells[41, 6] = $"расшифровка подписи";
                            workSheet.Cells[42, 4] = $"Ведомость измерения параметров получил";
                            workSheet.Cells[43, 4] = $"Удостоверение \"№\":{numberIdentification}";
                            workSheet.Cells[29, 9] = $"Принял: Начальник участка\n по ТО и ремонту СРС";
                            workSheet.Cells[31, 9] = $"должность";
                            workSheet.Cells[33, 10] = $"{FIO_chief}";
                            workSheet.Cells[34, 9] = $"подпись";
                            workSheet.Cells[34, 10] = $"расшифровка подписи";
                            workSheet.Cells[36, 9] = $"Сдал: Начальник участка\n по ТО и ремонту СРС";
                            workSheet.Cells[38, 9] = $"подпись";
                            workSheet.Cells[40, 10] = $"{FIO_chief}";
                            workSheet.Cells[41, 9] = $"подпись";
                            workSheet.Cells[41, 10] = $"расшифровка подписи";
                            workSheet.Cells[29, 13] = $"Сдал: {post}\n";
                            workSheet.Cells[31, 13] = $"должность";
                            workSheet.Cells[33, 14] = $"{representative}";
                            workSheet.Cells[34, 13] = $"подпись";
                            workSheet.Cells[34, 14] = $"расшифровка подписи";
                            workSheet.Cells[36, 13] = $"Принял: {post}\n";
                            workSheet.Cells[38, 13] = $"должность";
                            workSheet.Cells[40, 14] = $"{representative}";
                            workSheet.Cells[41, 13] = $"подпись";
                            workSheet.Cells[41, 14] = $"расшифровка подписи";
                            workSheet.Cells[42, 12] = $"Ведомость измерения параметров получил";
                            workSheet.Cells[43, 12] = $"Удостоверение \"№\":{numberIdentification}";

                            int s = 1;
                            int j = 8;

                            for (int i = 0; i < dgw.Rows.Count; i++)
                            {
                                workSheet.Cells[7 + s, 1] = s;
                                workSheet.Cells[7 + s, 9] = s;

                                Excel.Range _excelCells22 = (Excel.Range)workSheet.get_Range($"B{j}", $"C{j}").Cells;
                                _excelCells22.Merge(Type.Missing);
                                _excelCells22.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 2] = dgw.Rows[i].Cells["serialNumber"].Value.ToString();

                                Excel.Range _excelCells23 = (Excel.Range)workSheet.get_Range($"J{j}", $"K{j}").Cells;
                                _excelCells23.Merge(Type.Missing);
                                _excelCells23.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 10] = dgw.Rows[i].Cells["serialNumber"].Value.ToString();

                                Excel.Range _excelCells24 = (Excel.Range)workSheet.get_Range($"D{j}").Cells;
                                _excelCells24.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 4] = dgw.Rows[i].Cells["AKB"].Value.ToString();

                                Excel.Range _excelCells25 = (Excel.Range)workSheet.get_Range($"K{j}").Cells;
                                _excelCells25.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 12] = dgw.Rows[i].Cells["AKB"].Value.ToString();

                                Excel.Range _excelCells26 = (Excel.Range)workSheet.get_Range($"E{j}").Cells;
                                _excelCells26.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 5] = dgw.Rows[i].Cells["batteryСharger"].Value.ToString();

                                Excel.Range _excelCells27 = (Excel.Range)workSheet.get_Range($"L{j}").Cells;
                                _excelCells27.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 13] = dgw.Rows[i].Cells["batteryСharger"].Value.ToString();

                                Excel.Range _excelCells28 = (Excel.Range)workSheet.get_Range($"F{j}").Cells;
                                _excelCells28.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 6] = dgw.Rows[i].Cells["antenna"].Value.ToString();

                                Excel.Range _excelCells29 = (Excel.Range)workSheet.get_Range($"M{j}").Cells;
                                _excelCells29.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 14] = dgw.Rows[i].Cells["antenna"].Value.ToString();

                                Excel.Range _excelCells30 = (Excel.Range)workSheet.get_Range($"G{j}").Cells;
                                _excelCells30.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 7] = dgw.Rows[i].Cells["manipulator"].Value.ToString();

                                Excel.Range _excelCells31 = (Excel.Range)workSheet.get_Range($"N{j}").Cells;
                                _excelCells31.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet.Cells[7 + s, 15] = dgw.Rows[i].Cells["manipulator"].Value.ToString();

                                s++;
                                j++;
                            }

                            while (s <= 20)
                            {
                                workSheet.Cells[7 + s, 1] = s;
                                workSheet.Cells[7 + s, 9] = s;

                                Excel.Range _excelCells22 = (Excel.Range)workSheet.get_Range($"B{j}", $"C{j}").Cells;
                                _excelCells22.Merge(Type.Missing);
                                _excelCells22.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells23 = (Excel.Range)workSheet.get_Range($"J{j}", $"K{j}").Cells;
                                _excelCells23.Merge(Type.Missing);
                                _excelCells23.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells24 = (Excel.Range)workSheet.get_Range($"D{j}").Cells;
                                _excelCells24.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells25 = (Excel.Range)workSheet.get_Range($"K{j}").Cells;
                                _excelCells25.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells26 = (Excel.Range)workSheet.get_Range($"E{j}").Cells;
                                _excelCells26.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells27 = (Excel.Range)workSheet.get_Range($"L{j}").Cells;
                                _excelCells27.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells28 = (Excel.Range)workSheet.get_Range($"F{j}").Cells;
                                _excelCells28.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells29 = (Excel.Range)workSheet.get_Range($"M{j}").Cells;
                                _excelCells29.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells30 = (Excel.Range)workSheet.get_Range($"G{j}").Cells;
                                _excelCells30.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells31 = (Excel.Range)workSheet.get_Range($"N{j}").Cells;
                                _excelCells31.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                s++;
                                j++;
                            }

                            #endregion

                            #region Ведомость То 2 Item


                            workSheet2.PageSetup.Zoom = false;
                            workSheet2.PageSetup.FitToPagesWide = 1;
                            workSheet2.PageSetup.FitToPagesTall = 1;

                            workSheet2.Rows.Font.Size = 15;
                            workSheet2.Rows.Font.Name = "Times New Roman";

                            workSheet2.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                            workSheet2.PageSetup.CenterHorizontally = true;
                            workSheet2.PageSetup.CenterVertically = true;
                            workSheet2.PageSetup.TopMargin = 0;
                            workSheet2.PageSetup.BottomMargin = 0;
                            workSheet2.PageSetup.LeftMargin = 0;
                            workSheet2.PageSetup.RightMargin = 0;

                            Excel.Range _excelCells200 = (Excel.Range)workSheet2.get_Range("A1", "G1").Cells;
                            Excel.Range _excelCells201 = (Excel.Range)workSheet2.get_Range("H1", "I1").Cells;
                            Excel.Range _excelCells202 = (Excel.Range)workSheet2.get_Range("J1", "K1").Cells;
                            Excel.Range _excelCells203 = (Excel.Range)workSheet2.get_Range("L1", "M1").Cells;
                            Excel.Range _excelCells204 = (Excel.Range)workSheet2.get_Range("T1", "V1").Cells;
                            Excel.Range _excelCells205 = (Excel.Range)workSheet2.get_Range("A2", "A19").Cells;
                            Excel.Range _excelCells206 = (Excel.Range)workSheet2.get_Range("B2", "C2").Cells;
                            Excel.Range _excelCells207 = (Excel.Range)workSheet2.get_Range("B3", "B19").Cells;
                            Excel.Range _excelCells208 = (Excel.Range)workSheet2.get_Range("C3", "C19").Cells;
                            Excel.Range _excelCells209 = (Excel.Range)workSheet2.get_Range("D2", "I2").Cells;
                            Excel.Range _excelCells210 = (Excel.Range)workSheet2.get_Range("D3", "E6").Cells;
                            Excel.Range _excelCells211 = (Excel.Range)workSheet2.get_Range("D7", "D19").Cells;
                            Excel.Range _excelCells212 = (Excel.Range)workSheet2.get_Range("E7", "E19").Cells;
                            Excel.Range _excelCells213 = (Excel.Range)workSheet2.get_Range("F3", "F19").Cells;
                            Excel.Range _excelCells214 = (Excel.Range)workSheet2.get_Range("G3", "G19").Cells;
                            Excel.Range _excelCells215 = (Excel.Range)workSheet2.get_Range("H3", "H19").Cells;
                            Excel.Range _excelCells216 = (Excel.Range)workSheet2.get_Range("I3", "I19").Cells;
                            Excel.Range _excelCells217 = (Excel.Range)workSheet2.get_Range("J2", "O2").Cells;
                            Excel.Range _excelCells218 = (Excel.Range)workSheet2.get_Range("J3", "J19").Cells;
                            Excel.Range _excelCells219 = (Excel.Range)workSheet2.get_Range("K3", "L18").Cells;
                            Excel.Range _excelCells220 = (Excel.Range)workSheet2.get_Range("K19").Cells;
                            Excel.Range _excelCells221 = (Excel.Range)workSheet2.get_Range("L19").Cells;
                            Excel.Range _excelCells222 = (Excel.Range)workSheet2.get_Range("M3", "M19").Cells;
                            Excel.Range _excelCells223 = (Excel.Range)workSheet2.get_Range("N3", "N19").Cells;
                            Excel.Range _excelCells224 = (Excel.Range)workSheet2.get_Range("O3", "O19").Cells;
                            Excel.Range _excelCells225 = (Excel.Range)workSheet2.get_Range("P2", "R5").Cells;
                            Excel.Range _excelCells226 = (Excel.Range)workSheet2.get_Range("P6", "P19").Cells;
                            Excel.Range _excelCells227 = (Excel.Range)workSheet2.get_Range("Q6", "Q19").Cells;
                            Excel.Range _excelCells228 = (Excel.Range)workSheet2.get_Range("R6", "R19").Cells;
                            Excel.Range _excelCells229 = (Excel.Range)workSheet2.get_Range("S2", "S19").Cells;
                            Excel.Range _excelCells230 = (Excel.Range)workSheet2.get_Range("T2", "U4").Cells;
                            Excel.Range _excelCells231 = (Excel.Range)workSheet2.get_Range("T5", "T19").Cells;
                            Excel.Range _excelCells232 = (Excel.Range)workSheet2.get_Range("U5", "U19").Cells;
                            Excel.Range _excelCells233 = (Excel.Range)workSheet2.get_Range("V2", "Y2").Cells;
                            Excel.Range _excelCells234 = (Excel.Range)workSheet2.get_Range("V3", "W3").Cells;
                            Excel.Range _excelCells235 = (Excel.Range)workSheet2.get_Range("X3", "Y3").Cells;
                            Excel.Range _excelCells238 = (Excel.Range)workSheet2.get_Range("A2", "U39").Cells;
                            Excel.Range _excelCells240 = (Excel.Range)workSheet2.get_Range("V4", "Y39").Cells;
                            Excel.Range _excelCells241 = (Excel.Range)workSheet2.get_Range("V3", "Y3").Cells;
                            Excel.Range _excelCells243 = (Excel.Range)workSheet2.get_Range("A41").Cells;
                            Excel.Range _excelCells244 = (Excel.Range)workSheet2.get_Range("B41", "G41").Cells;
                            Excel.Range _excelCells245 = (Excel.Range)workSheet2.get_Range("H41", "J41").Cells;
                            Excel.Range _excelCells246 = (Excel.Range)workSheet2.get_Range("K41", "P41").Cells;
                            Excel.Range _excelCells247 = (Excel.Range)workSheet2.get_Range("T41", "Y41").Cells;
                            Excel.Range _excelCells248 = (Excel.Range)workSheet2.get_Range("B42", "G42").Cells;
                            Excel.Range _excelCells249 = (Excel.Range)workSheet2.get_Range("H42", "J42").Cells;
                            Excel.Range _excelCells250 = (Excel.Range)workSheet2.get_Range("K42", "P42").Cells;
                            Excel.Range _excelCells251 = (Excel.Range)workSheet2.get_Range("B44", "G44").Cells;
                            Excel.Range _excelCells252 = (Excel.Range)workSheet2.get_Range("H44", "J44").Cells;
                            Excel.Range _excelCells253 = (Excel.Range)workSheet2.get_Range("K44", "P44").Cells;
                            Excel.Range _excelCells254 = (Excel.Range)workSheet2.get_Range("B45", "G45").Cells;
                            Excel.Range _excelCells255 = (Excel.Range)workSheet2.get_Range("H45", "J45").Cells;
                            Excel.Range _excelCells256 = (Excel.Range)workSheet2.get_Range("K45", "P45").Cells;
                            Excel.Range _excelCells257 = (Excel.Range)workSheet2.get_Range("T42", "Y42").Cells;
                            Excel.Range _excelCells258 = (Excel.Range)workSheet2.get_Range("S44", "U44").Cells;
                            Excel.Range _excelCells259 = (Excel.Range)workSheet2.get_Range("V44", "X44").Cells;
                            Excel.Range _excelCells260 = (Excel.Range)workSheet2.get_Range("B47", "F47").Cells;
                            Excel.Range _excelCells261 = (Excel.Range)workSheet2.get_Range("H47", "L47").Cells;
                            Excel.Range _excelCells262 = (Excel.Range)workSheet2.get_Range("D2", "I39").Cells;
                            Excel.Range _excelCells263 = (Excel.Range)workSheet2.get_Range("J2", "O39").Cells;
                            Excel.Range _excelCells264 = (Excel.Range)workSheet2.get_Range("B2", "C39").Cells;
                            Excel.Range _excelCells265 = (Excel.Range)workSheet2.get_Range("P2", "S39").Cells;
                            Excel.Range _excelCells266 = (Excel.Range)workSheet2.get_Range("A2", "A39").Cells;
                            Excel.Range _excelCells267 = (Excel.Range)workSheet2.get_Range("T2", "U39").Cells;
                            Excel.Range _excelCells268 = (Excel.Range)workSheet2.get_Range("V2", "Y39").Cells;
                            Excel.Range _excelCells269 = (Excel.Range)workSheet2.get_Range("A20", "U39").Cells;

                            _excelCells200.Merge(Type.Missing);
                            _excelCells201.Merge(Type.Missing);
                            _excelCells202.Merge(Type.Missing);
                            _excelCells203.Merge(Type.Missing);
                            _excelCells204.Merge(Type.Missing);
                            _excelCells205.Merge(Type.Missing);
                            _excelCells206.Merge(Type.Missing);
                            _excelCells207.Merge(Type.Missing);
                            _excelCells208.Merge(Type.Missing);
                            _excelCells209.Merge(Type.Missing);
                            _excelCells210.Merge(Type.Missing);
                            _excelCells211.Merge(Type.Missing);
                            _excelCells212.Merge(Type.Missing);
                            _excelCells213.Merge(Type.Missing);
                            _excelCells214.Merge(Type.Missing);
                            _excelCells215.Merge(Type.Missing);
                            _excelCells216.Merge(Type.Missing);
                            _excelCells217.Merge(Type.Missing);
                            _excelCells218.Merge(Type.Missing);
                            _excelCells219.Merge(Type.Missing);
                            _excelCells222.Merge(Type.Missing);
                            _excelCells223.Merge(Type.Missing);
                            _excelCells224.Merge(Type.Missing);
                            _excelCells225.Merge(Type.Missing);
                            _excelCells226.Merge(Type.Missing);
                            _excelCells227.Merge(Type.Missing);
                            _excelCells228.Merge(Type.Missing);
                            _excelCells229.Merge(Type.Missing);
                            _excelCells230.Merge(Type.Missing);
                            _excelCells231.Merge(Type.Missing);
                            _excelCells232.Merge(Type.Missing);
                            _excelCells233.Merge(Type.Missing);
                            _excelCells234.Merge(Type.Missing);
                            _excelCells235.Merge(Type.Missing);
                            _excelCells244.Merge(Type.Missing);
                            _excelCells245.Merge(Type.Missing);
                            _excelCells246.Merge(Type.Missing);
                            _excelCells247.Merge(Type.Missing);
                            _excelCells248.Merge(Type.Missing);
                            _excelCells249.Merge(Type.Missing);
                            _excelCells250.Merge(Type.Missing);
                            _excelCells251.Merge(Type.Missing);
                            _excelCells252.Merge(Type.Missing);
                            _excelCells253.Merge(Type.Missing);
                            _excelCells254.Merge(Type.Missing);
                            _excelCells255.Merge(Type.Missing);
                            _excelCells256.Merge(Type.Missing);
                            _excelCells257.Merge(Type.Missing);
                            _excelCells258.Merge(Type.Missing);
                            _excelCells259.Merge(Type.Missing);
                            _excelCells260.Merge(Type.Missing);
                            _excelCells261.Merge(Type.Missing);

                            _excelCells238.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells238.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells238.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells238.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells238.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells238.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells233.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells233.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells233.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells233.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells241.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells241.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells241.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells241.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells240.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells240.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells240.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells240.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells240.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells240.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells244.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells245.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells246.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells247.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells251.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells252.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells253.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells259.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells262.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells262.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells262.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells262.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells263.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells263.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells263.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells263.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells264.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells264.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells264.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells264.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells265.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells265.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells265.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells265.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells266.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells266.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells266.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells266.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells267.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells267.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells267.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells267.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells268.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells268.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells268.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDouble;
                            _excelCells268.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDouble;

                            _excelCells269.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlDash;
                            //_excelCells269.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlDash;

                            _excelCells200.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells201.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells202.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells203.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells204.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells205.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells205.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells206.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells207.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells207.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells207.Orientation = 90;

                            _excelCells208.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells208.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells208.Orientation = 90;

                            _excelCells209.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells210.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells210.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells211.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells211.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells211.Orientation = 90;

                            _excelCells212.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells212.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells212.Orientation = 90;

                            _excelCells213.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells213.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells213.Orientation = 90;

                            _excelCells214.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells214.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells214.Orientation = 90;

                            _excelCells215.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells215.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells215.Orientation = 90;

                            _excelCells216.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells216.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells216.Orientation = 90;

                            _excelCells217.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells218.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells218.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells218.Orientation = 90;

                            _excelCells219.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells219.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells219.Orientation = 90;

                            _excelCells220.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells221.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells222.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells222.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells222.Orientation = 90;

                            _excelCells223.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells223.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells223.Orientation = 90;

                            _excelCells224.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells224.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells224.Orientation = 90;

                            _excelCells225.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells225.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells226.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells226.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells226.Orientation = 90;

                            _excelCells227.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells227.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells227.Orientation = 90;

                            _excelCells228.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells228.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells228.Orientation = 90;

                            _excelCells229.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells229.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells229.Orientation = 90;

                            _excelCells230.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells230.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            _excelCells231.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells231.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells231.Orientation = 90;

                            _excelCells232.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells232.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells232.Orientation = 90;

                            _excelCells233.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells234.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells235.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells243.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells244.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells246.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells247.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells248.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells249.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells250.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells251.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells252.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells253.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells254.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells255.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells256.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells257.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells258.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells259.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells260.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells261.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                            Excel.Range rowHeight200 = workSheet2.get_Range("A1", "G1");
                            rowHeight200.EntireRow.RowHeight = 15; //

                            Excel.Range rowColum200 = workSheet2.get_Range("A1", "G1");
                            rowColum200.EntireColumn.ColumnWidth = 5; //

                            Excel.Range rowHeight201 = workSheet2.get_Range("A2", "A19");
                            rowHeight201.EntireRow.RowHeight = 15; //

                            Excel.Range rowColum201 = workSheet2.get_Range("A2", "A19");
                            rowColum201.EntireColumn.ColumnWidth = 25; //

                            Excel.Range rowColum202 = workSheet2.get_Range("B2", "C2");
                            rowColum202.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum203 = workSheet2.get_Range("D7", "E7");
                            rowColum203.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum204 = workSheet2.get_Range("F3", "F14");
                            rowColum204.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum205 = workSheet2.get_Range("G3", "G14");
                            rowColum205.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum206 = workSheet2.get_Range("H3", "H14");
                            rowColum206.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum207 = workSheet2.get_Range("I3", "I14");
                            rowColum207.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum208 = workSheet2.get_Range("V4", "V19");
                            rowColum208.EntireColumn.ColumnWidth = 8;

                            Excel.Range rowColum209 = workSheet2.get_Range("X3", "Y3");
                            rowColum209.EntireColumn.ColumnWidth = 15;

                            Excel.Range rowHeight210 = workSheet2.get_Range("A20", "A39");
                            rowHeight210.EntireRow.RowHeight = 25; //

                            Excel.Range rowHeight211 = workSheet2.get_Range("A42", "Y42");
                            rowHeight211.EntireRow.RowHeight = 12; //

                            Excel.Range rowHeight212 = workSheet2.get_Range("A45", "P45");
                            rowHeight212.EntireRow.RowHeight = 12; //

                            Excel.Range rowHeight213 = workSheet2.get_Range("B47", "L47");
                            rowHeight213.EntireRow.RowHeight = 12; //

                            Excel.Range rowColum214 = workSheet2.get_Range("V3", "W3");
                            rowColum214.EntireColumn.ColumnWidth = 2;

                            Excel.Range rowHeight215 = workSheet2.get_Range("V4", "V19");
                            rowHeight215.EntireRow.RowHeight = 20;

                            Excel.Range rowColum216 = workSheet2.get_Range("B3", "B19");
                            rowColum216.EntireColumn.ColumnWidth = 15;

                            Excel.Range rowHeight217 = workSheet2.get_Range("A1", "Y1");
                            rowHeight217.EntireRow.RowHeight = 25;

                            Excel.Range rowHeight218 = workSheet2.get_Range("A2", "Y2");
                            rowHeight218.EntireRow.RowHeight = 20;

                            Excel.Range rowHeight219 = workSheet2.get_Range("A20", "A39");
                            rowHeight219.EntireRow.RowHeight = 25;

                            Excel.Range range_Consolidated200 = workSheet2.Rows.get_Range("H1", "I1");
                            Excel.Range range_Consolidated201 = workSheet2.Rows.get_Range("L1", "M1");
                            Excel.Range range_Consolidated202 = workSheet2.Rows.get_Range("T1", "V1");
                            Excel.Range range_Consolidated203 = workSheet2.Rows.get_Range("V4", "V19");
                            Excel.Range range_Consolidated204 = workSheet2.Rows.get_Range("A42", "Y42");
                            Excel.Range range_Consolidated205 = workSheet2.Rows.get_Range("A45", "P45");
                            Excel.Range range_Consolidated206 = workSheet2.Rows.get_Range("V44", "X44");
                            Excel.Range range_Consolidated207 = workSheet2.Rows.get_Range("B47", "L47");
                            Excel.Range range_Consolidated208 = workSheet2.Rows.get_Range("A2", "U19");
                            Excel.Range range_Consolidated209 = workSheet2.Rows.get_Range("A1", "G1");
                            Excel.Range range_Consolidated210 = workSheet2.Rows.get_Range("A20", "A39");
                            Excel.Range range_Consolidated211 = workSheet2.Rows.get_Range("K41", "P41");
                            Excel.Range range_Consolidated212 = workSheet2.Rows.get_Range("T41", "Y41");
                            Excel.Range range_Consolidated213 = workSheet2.Rows.get_Range("A20", "A39");

                            range_Consolidated200.Font.Size = 18;
                            range_Consolidated200.Font.Bold = true;
                            range_Consolidated201.Font.Size = 18;
                            range_Consolidated201.Font.Bold = true;
                            range_Consolidated202.Font.Size = 18;
                            range_Consolidated202.Font.Bold = true;
                            range_Consolidated203.Font.Size = 10;
                            range_Consolidated203.Font.Bold = true;
                            range_Consolidated204.Font.Size = 10;
                            range_Consolidated205.Font.Size = 10;
                            range_Consolidated206.Font.Bold = true;
                            range_Consolidated207.Font.Size = 10;
                            range_Consolidated208.Font.Bold = true;
                            range_Consolidated209.Font.Size = 16;
                            range_Consolidated210.Font.Size = 18;
                            range_Consolidated210.Font.Bold = true;
                            range_Consolidated211.Font.Bold = true;
                            range_Consolidated212.Font.Bold = true;
                            range_Consolidated213.NumberFormat = "@";

                            workSheet2.Cells[1, 1] = $"Ведомость проверки параметров радиостанций №:";
                            workSheet2.Cells[1, 8] = $"{numberAct}";
                            workSheet2.Cells[1, 10] = $"Предприятие:";
                            workSheet2.Cells[1, 12] = $"{company}";
                            workSheet2.Cells[1, 20] = $"{location}";
                            workSheet2.Cells[2, 1] = $"№ р/с";
                            workSheet2.Cells[2, 2] = $"АКБ";
                            workSheet2.Cells[3, 2] = $"серия, № АКБ";
                            workSheet2.Cells[3, 3] = $"Остаточная ёмкость АКБ, %";
                            workSheet2.Cells[2, 4] = $"Параметры передатчика";
                            workSheet2.Cells[3, 4] = $"Выходная\n мощность\n передатчика, Вт";
                            workSheet2.Cells[7, 4] = $"Низкий уровень";
                            workSheet2.Cells[7, 5] = $"Высокий уровень";
                            workSheet2.Cells[3, 6] = $"Отклонение частоты\n от номинала, Гц";
                            workSheet2.Cells[3, 7] = $"КНИ, %";
                            workSheet2.Cells[3, 8] = $"Чувствительность\n модуляционного входа, мВ";
                            workSheet2.Cells[3, 9] = $"Максимальная девиация\n частоты, кГц";
                            workSheet2.Cells[2, 10] = $"Параметры приёмника";
                            workSheet2.Cells[3, 10] = $"Чувствительность\n приемника, мкВ";
                            workSheet2.Cells[3, 11] = $"Выходная мощность приёмника, В";
                            workSheet2.Cells[19, 11] = $"В";
                            workSheet2.Cells[19, 12] = $"Вт";
                            workSheet2.Cells[3, 13] = $"Избирательность\n по соседнему каналу, дБ";
                            workSheet2.Cells[3, 14] = $"КНИ, %";
                            workSheet2.Cells[3, 15] = $"Порог срабатывания\n шумоподавителя, мкВ";
                            workSheet2.Cells[2, 16] = $"Потребляемый ток";
                            workSheet2.Cells[6, 16] = $"\"Дежурный режим\", мА";
                            workSheet2.Cells[6, 17] = $"\"Режим приём, мА\", мА";
                            workSheet2.Cells[6, 18] = $"\"Режим передачи\n (высокая мощность)\", А";
                            workSheet2.Cells[2, 19] = $"Сигнализация разряда АКБ, В";
                            workSheet2.Cells[2, 20] = $"Аксессуары \n(при наличии)";
                            workSheet2.Cells[5, 20] = $"ЗУ испр / неиспр";
                            workSheet2.Cells[5, 21] = $"Манипулятор: \n испр / неиспр";
                            workSheet2.Cells[2, 22] = $"Частоты (МГц)";
                            workSheet2.Cells[3, 22] = $"";
                            workSheet2.Cells[3, 24] = $"передача / приём    ";
                            workSheet2.Cells[41, 1] = $"Исполнитель работ:";
                            workSheet2.Cells[41, 2] = $"Инженер по ТО и ремонту СРС";
                            workSheet2.Cells[41, 8] = $"/                                     /";
                            workSheet2.Cells[41, 11] = $"{FIO_Engineer}";
                            workSheet2.Cells[41, 20] = $"{dateTO.Remove(dateTO.IndexOf(" "))} г.";
                            workSheet2.Cells[42, 2] = $"должность";
                            workSheet2.Cells[42, 8] = $"подпись";
                            workSheet2.Cells[42, 11] = $"расшифровка подписи";
                            workSheet2.Cells[42, 20] = $"дата проведения технического обслуживания";
                            workSheet2.Cells[44, 1] = $"Представитель РЦС:";
                            workSheet2.Cells[44, 2] = $"";
                            workSheet2.Cells[44, 8] = $"/                                     /";
                            workSheet2.Cells[44, 11] = $"";
                            workSheet2.Cells[45, 2] = $"должность";
                            workSheet2.Cells[45, 8] = $"подпись";
                            workSheet2.Cells[45, 11] = $"расшифровка подписи";
                            workSheet2.Cells[44, 19] = $"Частота проверки:";
                            workSheet2.Cells[44, 22] = $"151.825";
                            workSheet2.Cells[47, 2] = $"Примечание: 1. \" - \" - не предоставлено для ТО";
                            workSheet2.Cells[47, 8] = $"2. \" б/н \" - без номера (номер отсутсвует)";


                            int s3 = 1;
                            int j3 = 4;

                            for (int i = 0; i < 16; i++)
                            {
                                workSheet2.Cells[3 + s3, 22] = s3;
                                Excel.Range _excelCells236 = (Excel.Range)workSheet2.get_Range($"V{j3}").Cells;
                                _excelCells236.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;


                                Excel.Range _excelCells237 = (Excel.Range)workSheet2.get_Range($"W{j3}", $"Y{j3}").Cells;
                                _excelCells237.Merge(Type.Missing);

                                s3++;
                                j3++;

                            }
                            int j4 = 20;
                            int s4 = 1;
                            for (int i = 0; i < dgw.Rows.Count; i++)
                            {
                                Excel.Range _excelCells242 = (Excel.Range)workSheet2.get_Range($"A{j4}").Cells;
                                _excelCells242.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                _excelCells242.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                workSheet2.Cells[19 + s4, 1] = dgw.Rows[i].Cells["serialNumber"].Value.ToString();

                                Excel.Range _excelCells239 = (Excel.Range)workSheet2.get_Range($"V{j4}", $"Y{j4}").Cells;
                                _excelCells239.Merge(Type.Missing);

                                s4++;
                                j4++;

                            }
                            while (s4 <= 20)
                            {
                                Excel.Range _excelCells242 = (Excel.Range)workSheet2.get_Range($"A{j4}").Cells;
                                _excelCells242.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells239 = (Excel.Range)workSheet2.get_Range($"V{j4}", $"Y{j4}").Cells;
                                _excelCells239.Merge(Type.Missing);

                                s4++;
                                j4++;
                            }


                            #endregion

                            #region АКТ ТО 3 Item
                            workSheet3.PageSetup.Zoom = false;
                            workSheet3.PageSetup.FitToPagesWide = 1;
                            workSheet3.PageSetup.FitToPagesTall = 1;

                            workSheet3.Rows.Font.Size = 11;
                            workSheet3.Rows.Font.Name = "Times New Roman";

                            workSheet2.PageSetup.CenterHorizontally = true;
                            workSheet2.PageSetup.CenterVertically = true;
                            workSheet3.PageSetup.TopMargin = 50;
                            workSheet3.PageSetup.BottomMargin = 0;
                            workSheet3.PageSetup.LeftMargin = 90;
                            workSheet3.PageSetup.RightMargin = 0;

                            workSheet3.PageSetup.Zoom = 97;

                            Excel.Range _excelCells101 = (Excel.Range)workSheet3.get_Range("A1", "I1").Cells;
                            Excel.Range _excelCells102 = (Excel.Range)workSheet3.get_Range("A2", "I2").Cells;
                            Excel.Range _excelCells103 = (Excel.Range)workSheet3.get_Range("A4", "C4").Cells;
                            Excel.Range _excelCells104 = (Excel.Range)workSheet3.get_Range("H4", "I4").Cells;
                            Excel.Range _excelCells105 = (Excel.Range)workSheet3.get_Range("A5", "C5").Cells;
                            Excel.Range _excelCells106 = (Excel.Range)workSheet3.get_Range("H5", "I5").Cells;
                            Excel.Range _excelCells107 = (Excel.Range)workSheet3.get_Range("A6", "G6").Cells;
                            Excel.Range _excelCells108 = (Excel.Range)workSheet3.get_Range("A7", "I7").Cells;
                            Excel.Range _excelCells109 = (Excel.Range)workSheet3.get_Range("A8", "G8").Cells;
                            Excel.Range _excelCells110 = (Excel.Range)workSheet3.get_Range("H8", "I8").Cells;
                            Excel.Range _excelCells111 = (Excel.Range)workSheet3.get_Range("A9", "I9").Cells;
                            Excel.Range _excelCells112 = (Excel.Range)workSheet3.get_Range("A10", "I10").Cells;
                            Excel.Range _excelCells113 = (Excel.Range)workSheet3.get_Range("A11", "E11").Cells;
                            Excel.Range _excelCells114 = (Excel.Range)workSheet3.get_Range("A12", "E12").Cells;
                            Excel.Range _excelCells115 = (Excel.Range)workSheet3.get_Range("G12", "I12").Cells;
                            Excel.Range _excelCells116 = (Excel.Range)workSheet3.get_Range("A13", "C13").Cells;
                            Excel.Range _excelCells117 = (Excel.Range)workSheet3.get_Range("D13", "E13").Cells;
                            Excel.Range _excelCells118 = (Excel.Range)workSheet3.get_Range("F13", "G13").Cells;
                            Excel.Range _excelCells119 = (Excel.Range)workSheet3.get_Range("A14", "I14").Cells;
                            Excel.Range _excelCells120 = (Excel.Range)workSheet3.get_Range("A15", "I15").Cells;
                            Excel.Range _excelCells121 = (Excel.Range)workSheet3.get_Range("A16", "I16").Cells;
                            Excel.Range _excelCells122 = (Excel.Range)workSheet3.get_Range("A18", "A37").Cells;
                            Excel.Range _excelCells123 = (Excel.Range)workSheet3.get_Range("A17").Cells;
                            Excel.Range _excelCells124 = (Excel.Range)workSheet3.get_Range("B17", "C17").Cells;
                            Excel.Range _excelCells125 = (Excel.Range)workSheet3.get_Range("D17", "F17").Cells;
                            Excel.Range _excelCells126 = (Excel.Range)workSheet3.get_Range("G17", "I17").Cells;
                            Excel.Range _excelCells127 = (Excel.Range)workSheet3.get_Range("A17", "I37").Cells;
                            Excel.Range _excelCells131 = (Excel.Range)workSheet3.get_Range("A38", "I38").Cells;
                            Excel.Range _excelCells132 = (Excel.Range)workSheet3.get_Range("A39", "I39").Cells;
                            Excel.Range _excelCells133 = (Excel.Range)workSheet3.get_Range("A40", "I40").Cells;
                            Excel.Range _excelCells134 = (Excel.Range)workSheet3.get_Range("A41", "I41").Cells;
                            Excel.Range _excelCells135 = (Excel.Range)workSheet3.get_Range("A42", "C42").Cells;
                            Excel.Range _excelCells136 = (Excel.Range)workSheet3.get_Range("F42", "H42").Cells;
                            Excel.Range _excelCells137 = (Excel.Range)workSheet3.get_Range("A43", "C43").Cells;
                            Excel.Range _excelCells138 = (Excel.Range)workSheet3.get_Range("C45", "D45").Cells;
                            Excel.Range _excelCells139 = (Excel.Range)workSheet3.get_Range("F44", "G44").Cells;
                            Excel.Range _excelCells140 = (Excel.Range)workSheet3.get_Range("H44", "I44").Cells;
                            Excel.Range _excelCells141 = (Excel.Range)workSheet3.get_Range("A46", "B46").Cells;
                            Excel.Range _excelCells142 = (Excel.Range)workSheet3.get_Range("C46", "D46").Cells;
                            Excel.Range _excelCells143 = (Excel.Range)workSheet3.get_Range("F45", "G45").Cells;
                            Excel.Range _excelCells144 = (Excel.Range)workSheet3.get_Range("A47", "B47").Cells;
                            Excel.Range _excelCells145 = (Excel.Range)workSheet3.get_Range("A49", "D49").Cells;
                            Excel.Range _excelCells146 = (Excel.Range)workSheet3.get_Range("A50", "B50").Cells;
                            Excel.Range _excelCells147 = (Excel.Range)workSheet3.get_Range("C50", "D50").Cells;
                            Excel.Range _excelCells148 = (Excel.Range)workSheet3.get_Range("G11", "I11").Cells;
                            Excel.Range _excelCells150 = (Excel.Range)workSheet3.get_Range("D47");
                            Excel.Range _excelCells151 = (Excel.Range)workSheet3.get_Range("G47");
                            Excel.Range _excelCells152 = (Excel.Range)workSheet3.get_Range("A45", "B45").Cells;
                            Excel.Range _excelCells153 = (Excel.Range)workSheet3.get_Range("H45", "I45").Cells;
                            Excel.Range _excelCells154 = (Excel.Range)workSheet3.get_Range("H13", "I13").Cells;
                            Excel.Range _excelCells155 = (Excel.Range)workSheet3.get_Range("A17", "I17").Cells;

                            _excelCells101.Merge(Type.Missing);
                            _excelCells102.Merge(Type.Missing);
                            _excelCells103.Merge(Type.Missing);
                            _excelCells104.Merge(Type.Missing);
                            _excelCells105.Merge(Type.Missing);
                            _excelCells106.Merge(Type.Missing);
                            _excelCells107.Merge(Type.Missing);
                            _excelCells108.Merge(Type.Missing);
                            _excelCells109.Merge(Type.Missing);
                            _excelCells110.Merge(Type.Missing);
                            _excelCells111.Merge(Type.Missing);
                            _excelCells112.Merge(Type.Missing);
                            _excelCells113.Merge(Type.Missing);
                            _excelCells114.Merge(Type.Missing);
                            _excelCells115.Merge(Type.Missing);
                            _excelCells116.Merge(Type.Missing);
                            _excelCells117.Merge(Type.Missing);
                            _excelCells118.Merge(Type.Missing);
                            _excelCells119.Merge(Type.Missing);
                            _excelCells120.Merge(Type.Missing);
                            _excelCells121.Merge(Type.Missing);
                            _excelCells124.Merge(Type.Missing);
                            _excelCells125.Merge(Type.Missing);
                            _excelCells126.Merge(Type.Missing);
                            _excelCells131.Merge(Type.Missing);
                            _excelCells132.Merge(Type.Missing);
                            _excelCells133.Merge(Type.Missing);
                            _excelCells134.Merge(Type.Missing);
                            _excelCells135.Merge(Type.Missing);
                            _excelCells136.Merge(Type.Missing);
                            _excelCells137.Merge(Type.Missing);
                            _excelCells138.Merge(Type.Missing);
                            _excelCells139.Merge(Type.Missing);
                            _excelCells140.Merge(Type.Missing);
                            _excelCells141.Merge(Type.Missing);
                            _excelCells142.Merge(Type.Missing);
                            _excelCells143.Merge(Type.Missing);
                            _excelCells144.Merge(Type.Missing);
                            _excelCells145.Merge(Type.Missing);
                            _excelCells146.Merge(Type.Missing);
                            _excelCells147.Merge(Type.Missing);
                            _excelCells148.Merge(Type.Missing);
                            _excelCells152.Merge(Type.Missing);
                            _excelCells153.Merge(Type.Missing);
                            _excelCells154.Merge(Type.Missing);

                            _excelCells101.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells102.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells103.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells104.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells105.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells106.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells107.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells108.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells109.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells110.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells111.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells112.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells113.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells114.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells115.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells116.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells117.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells118.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            _excelCells119.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells120.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells121.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells122.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells123.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells124.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells125.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells126.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells131.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells132.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells133.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells134.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells135.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells136.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells137.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells138.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells140.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells141.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells142.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells143.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells144.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            _excelCells145.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells146.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells147.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells148.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells150.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells151.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells153.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells154.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells155.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            _excelCells155.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;


                            _excelCells103.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells104.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                            _excelCells108.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells111.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells112.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells113.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells117.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;

                            _excelCells127.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells127.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells127.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells127.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells127.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells127.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlDot;

                            _excelCells138.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells139.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells140.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells145.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells148.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;
                            _excelCells152.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            _excelCells154.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDot;

                            Excel.Range range_Consolidated102 = workSheet3.Rows.get_Range("A1", "H2");
                            Excel.Range range_Consolidated103 = workSheet3.Rows.get_Range("A4", "B4");
                            Excel.Range range_Consolidated104 = workSheet3.Rows.get_Range("A5", "I5");
                            Excel.Range range_Consolidated105 = workSheet3.Rows.get_Range("A6", "G6");
                            Excel.Range range_Consolidated106 = workSheet3.Rows.get_Range("A7", "I7");
                            Excel.Range range_Consolidated107 = workSheet3.Rows.get_Range("B8", "C8");
                            Excel.Range range_Consolidated108 = workSheet3.Rows.get_Range("H8", "I8");
                            Excel.Range range_Consolidated109 = workSheet3.Rows.get_Range("A9", "I9");
                            Excel.Range range_Consolidated110 = workSheet3.Rows.get_Range("A10", "I10");
                            Excel.Range range_Consolidated111 = workSheet3.Rows.get_Range("A11", "I11");
                            Excel.Range range_Consolidated112 = workSheet3.Rows.get_Range("B12", "C12");
                            Excel.Range range_Consolidated113 = workSheet3.Rows.get_Range("A12", "H12");
                            Excel.Range range_Consolidated114 = workSheet3.Rows.get_Range("A13", "C13");
                            Excel.Range range_Consolidated115 = workSheet3.Rows.get_Range("D13", "E13");
                            Excel.Range range_Consolidated116 = workSheet3.Rows.get_Range("F13", "I13");
                            Excel.Range range_Consolidated117 = workSheet3.Rows.get_Range("A14", "I14");
                            Excel.Range range_Consolidated118 = workSheet3.Rows.get_Range("A15", "I15");
                            Excel.Range range_Consolidated119 = workSheet3.Rows.get_Range("A16", "I16");
                            Excel.Range range_Consolidated120 = workSheet3.Rows.get_Range("B18", "I37");
                            Excel.Range range_Consolidated121 = workSheet3.Rows.get_Range("A17", "I17");
                            Excel.Range range_Consolidated122 = workSheet3.Rows.get_Range("A38", "I41");
                            Excel.Range range_Consolidated123 = workSheet3.Rows.get_Range("A42", "I43");
                            Excel.Range range_Consolidated124 = workSheet3.Rows.get_Range("A44", "I44");
                            Excel.Range range_Consolidated125 = workSheet3.Rows.get_Range("A46", "I46");
                            Excel.Range range_Consolidated126 = workSheet3.Rows.get_Range("A47", "B47");
                            Excel.Range range_Consolidated127 = workSheet3.Rows.get_Range("A50", "D50");
                            Excel.Range range_Consolidated128 = workSheet3.Rows.get_Range("D51");
                            Excel.Range range_Consolidated129 = workSheet3.Rows.get_Range("C45", "D45");
                            Excel.Range range_Consolidated130 = workSheet3.Rows.get_Range("F45", "I45");
                            Excel.Range range_Consolidated131 = workSheet3.Rows.get_Range("A18", "I37");
                            Excel.Range range_Consolidated132 = workSheet3.Rows.get_Range("D18", "F37");
                            Excel.Range range_Consolidated133 = workSheet3.Rows.get_Range("A11", "E11");


                            Excel.Range rowHeight100 = workSheet3.get_Range("A38", "I41");
                            rowHeight100.EntireRow.RowHeight = 10; //

                            Excel.Range rowColum100 = workSheet3.get_Range("A17", "A37");
                            rowColum100.EntireColumn.ColumnWidth = 3; //

                            Excel.Range rowColum102 = workSheet3.get_Range("B17", "C37");
                            rowColum102.EntireColumn.ColumnWidth = 10; //

                            Excel.Range rowColum103 = workSheet3.get_Range("D17", "F37");
                            rowColum103.EntireColumn.ColumnWidth = 8; //

                            Excel.Range rowHeight101 = workSheet3.get_Range("A14", "I15");
                            rowHeight101.EntireRow.RowHeight = 12;

                            Excel.Range rowHeight102 = workSheet3.get_Range("A5", "I5");
                            rowHeight102.EntireRow.RowHeight = 11;

                            Excel.Range rowHeight103 = workSheet3.get_Range("A8", "I8");
                            rowHeight103.EntireRow.RowHeight = 11;

                            Excel.Range rowHeight104 = workSheet3.get_Range("A12", "I12");
                            rowHeight104.EntireRow.RowHeight = 11;

                            Excel.Range rowHeight105 = workSheet3.get_Range("A42", "I42");
                            rowHeight105.EntireRow.RowHeight = 18; //

                            Excel.Range rowHeight106 = workSheet3.get_Range("A14", "I14");
                            rowHeight106.EntireRow.RowHeight = 16;

                            Excel.Range rowHeight107 = workSheet3.get_Range("A38", "I38");
                            rowHeight107.EntireRow.RowHeight = 12;

                            Excel.Range rowHeight108 = workSheet3.get_Range("A17", "I17");
                            rowHeight108.EntireRow.RowHeight = 30;

                            range_Consolidated102.Font.Bold = true;
                            range_Consolidated102.Font.Size = 10;
                            range_Consolidated103.Font.Bold = true;
                            range_Consolidated103.Font.Size = 10;
                            range_Consolidated104.Font.Size = 8;
                            range_Consolidated105.Font.Size = 9;
                            range_Consolidated106.Font.Size = 9;
                            range_Consolidated107.Font.Size = 8;
                            range_Consolidated108.Font.Size = 8;
                            range_Consolidated109.Font.Size = 9;
                            range_Consolidated110.Font.Size = 9;
                            range_Consolidated111.Font.Size = 9;
                            range_Consolidated112.Font.Size = 8;
                            range_Consolidated113.Font.Size = 8;
                            range_Consolidated114.Font.Size = 9;
                            range_Consolidated115.Font.Size = 9;
                            range_Consolidated116.Font.Size = 9;
                            range_Consolidated117.Font.Size = 9;
                            range_Consolidated118.Font.Size = 9;
                            range_Consolidated119.Font.Size = 7;
                            range_Consolidated120.Font.Size = 8.5;
                            range_Consolidated121.Font.Size = 9;
                            range_Consolidated121.Font.Bold = true;
                            range_Consolidated122.Font.Size = 7;
                            range_Consolidated123.Font.Bold = true;
                            range_Consolidated123.Font.Size = 8;
                            range_Consolidated124.Font.Bold = true;
                            range_Consolidated124.Font.Size = 8;
                            range_Consolidated125.Font.Size = 6;
                            range_Consolidated126.Font.Size = 8;
                            range_Consolidated126.Font.Bold = true;
                            range_Consolidated127.Font.Size = 6;
                            range_Consolidated128.Font.Size = 6;
                            range_Consolidated130.Font.Size = 6;
                            range_Consolidated129.Font.Bold = true;
                            range_Consolidated129.Font.Size = 8;
                            range_Consolidated131.Font.Size = 7;
                            range_Consolidated132.NumberFormat = "@";
                            range_Consolidated133.Font.Size = 7;

                            workSheet3.Cells[1, 1] = $"ПЕРВИЧНЫЙ ТЕХНИЧЕСКИЙ АКТ № {numberAct}";
                            workSheet3.Cells[2, 1] = $"ОКАЗАННЫХ УСЛУГ ПО ТЕХНИЧЕСКОМУ ОБСЛУЖИВАНИЮ СИСТЕМ РАДИОСВЯЗИ";
                            workSheet3.Cells[4, 1] = $"{city}";
                            workSheet3.Cells[5, 1] = $"город";
                            workSheet3.Cells[5, 8] = $"дата";
                            workSheet3.Cells[6, 1] = $"Мы, нижеподписавшиеся, представитель Исполнителя:";
                            workSheet3.Cells[7, 1] = $"Начальник участка по техническому обслуживанию и ремонту систем радиосвязи:            {FIO_chief}";
                            workSheet3.Cells[8, 2] = $"должность";
                            workSheet3.Cells[8, 8] = $"фамилия, инициалы";
                            workSheet3.Cells[9, 1] = $"действующий по доверенности № {doverennost} с одной стороны и представитель Заказчика";
                            workSheet3.Cells[10, 1] = $"(эксплуатирующей организации):             {company}             {polinon_full} (полигон {poligon})";
                            workSheet3.Cells[11, 1] = $"{post}";
                            workSheet3.Cells[11, 7] = $"{representative}";
                            workSheet3.Cells[12, 1] = $"должность";
                            workSheet3.Cells[12, 7] = $"фамилия, инициалы";
                            workSheet3.Cells[13, 1] = $"служебное удостоверение №";
                            workSheet3.Cells[13, 4] = $"{numberIdentification}";
                            workSheet3.Cells[13, 6] = $"дата выдачи:";
                            workSheet3.Cells[13, 8] = $"{dateIssue} г.";
                            workSheet3.Cells[14, 1] = $"с другой стороны составили настоящий акт в том, что во исполнение договора № 4176190 от 07 декабря 2020 г.,";
                            workSheet3.Cells[15, 1] = $"были  оказаны  услуги по техническому обслуживанию систем радиосвязи:";
                            workSheet3.Cells[16, 1] = $"Заводские номера  и марки портативных (носимых)  систем радиосвязи:";
                            workSheet3.Cells[17, 1] = $"№";
                            workSheet3.Cells[17, 2] = $"Тип РЭС";
                            workSheet3.Cells[17, 4] = $"Заводской № РЭС";
                            workSheet3.Cells[17, 7] = $"Место нахождения РЭС";
                            workSheet3.Cells[38, 1] = $"Вышеперечисленные носимые радиостанции участвуют в технологических процессах, требующих режима немедленной связи, и включены";
                            workSheet3.Cells[39, 1] = $"в перечень носимых радиостанций по данному структурному подразделению подлежащих периодической проверке. Системы радиосвязи исправны,";
                            workSheet3.Cells[40, 1] = $"технические характеристики вышеперечисленных систем радиосвязи после проведенного технического обслуживания соответствуют нормам.";
                            workSheet3.Cells[41, 1] = $"Представитель эксплуатирующей организации по качеству оказанных услуг претензий к Исполнителю не имеет.";
                            workSheet3.Cells[42, 1] = $"Представитель Заказчика";
                            workSheet3.Cells[42, 6] = $"Представитель Исполнителя: ";
                            workSheet3.Cells[43, 1] = $"(эксплуатирующей организации):";
                            workSheet3.Cells[44, 8] = $"{FIO_chief}";
                            workSheet3.Cells[45, 3] = $"{representative}";
                            workSheet3.Cells[45, 6] = $"подпись";
                            workSheet3.Cells[45, 8] = $"расшифровка  подписи";
                            workSheet3.Cells[46, 1] = $"подпись";
                            workSheet3.Cells[46, 3] = $"расшифровка  подписи";
                            workSheet3.Cells[46, 7] = $"МП";
                            workSheet3.Cells[47, 1] = $"Представитель РЦС:";
                            workSheet3.Cells[50, 1] = $"подпись";
                            workSheet3.Cells[50, 3] = $"расшифровка  подписи";
                            workSheet3.Cells[51, 4] = $"МП";

                            int s2 = 1;
                            int j2 = 18;

                            for (int i = 0; i < dgw.Rows.Count; i++)
                            {
                                workSheet3.Cells[17 + s2, 1] = s2;

                                Excel.Range _excelCells128 = (Excel.Range)workSheet3.get_Range($"B{j2}", $"C{j2}").Cells;
                                _excelCells128.Merge(Type.Missing);
                                _excelCells128.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet3.Cells[17 + s2, 2] = dgw.Rows[i].Cells["model"].Value.ToString();

                                Excel.Range _excelCells129 = (Excel.Range)workSheet3.get_Range($"D{j2}", $"F{j2}").Cells;
                                _excelCells129.Merge(Type.Missing);
                                _excelCells129.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet3.Cells[17 + s2, 4] = dgw.Rows[i].Cells["serialNumber"].Value.ToString();

                                Excel.Range _excelCells130 = (Excel.Range)workSheet3.get_Range($"G{j2}", $"I{j2}").Cells;
                                _excelCells130.Merge(Type.Missing);
                                _excelCells130.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                workSheet3.Cells[17 + s2, 7] = dgw.Rows[i].Cells["location"].Value.ToString();

                                s2++;
                                j2++;
                            }

                            while (s2 <= 20)
                            {
                                workSheet3.Cells[17 + s2, 1] = s2;

                                Excel.Range _excelCells128 = (Excel.Range)workSheet3.get_Range($"B{j2}", $"C{j2}").Cells;
                                _excelCells128.Merge(Type.Missing);
                                _excelCells128.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells129 = (Excel.Range)workSheet3.get_Range($"D{j2}", $"F{j2}").Cells;
                                _excelCells129.Merge(Type.Missing);
                                _excelCells129.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                                Excel.Range _excelCells130 = (Excel.Range)workSheet3.get_Range($"G{j2}", $"I{j2}").Cells;
                                _excelCells130.Merge(Type.Missing);
                                _excelCells130.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                s2++;
                                j2++;
                            }
                            #endregion

                            var file = $"{numberAct.Replace('/', '.')}-{company}_Акт.xlsx";

                            if (!File.Exists($@"С:\Documents_ServiceTelekom\Акты ТО\{city}\"))
                            {
                                try
                                {
                                    Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\Акты ТО\{city}\");

                                    workSheet3.SaveAs($@"C:\Documents_ServiceTelekom\Акты ТО\{city}\" + file);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    MessageBox.Show("Не удаётся сохранить файл.");
                                }
                            }
                            else
                            {
                                try
                                {
                                    workSheet3.SaveAs($@"C:\Documents_ServiceTelekom\Акты ТО\{city}\" + file);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    MessageBox.Show("Не удаётся сохранить файл.");
                                }
                            }
                            exApp.Visible = true;
                        }
                    }
                }
                else
                {
                    string Mesage2;

                    Mesage2 = "Выберете акт, который хотите напечатать!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                if (exApp != null)
                {
                    exApp = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Environment.Exit(0);
                MessageBox.Show(ex.ToString());
            }
        }
        internal static void PrintExcelActRemont(DataGridView dgw, string numberAct, string dateTO,
            string company, string location, string FIO_chief, string post, string representative,
            string numberIdentification, string FIO_Engineer, string doverennost, string polinon_full,
            string dateIssue, string city, string poligon, string сategory, string model, string serialNumber, 
            string inventoryNumber, string networkNumber, string сompleted_works_1, string parts_1, 
            string сompleted_works_2, string parts_2, string сompleted_works_3, string parts_3,
            string сompleted_works_4, string parts_4, string сompleted_works_5, string parts_5,
            string сompleted_works_6, string parts_6, string сompleted_works_7, string parts_7, string OKPO_remont, 
            string BE_remont, string Full_name_company,string director_FIO_remont_company,string numberActRemont, 
            string chairman_post_remont_company, string chairman_FIO_remont_company, string txb_1_post_remont_company, 
            string txb_1_FIO_remont_company, string txb_2_post_remont_company, string txb_2_FIO_remont_company, 
            string txb_3_post_remont_company, string txb_3_FIO_remont_company)
        {
            Excel.Application exApp = new Excel.Application();

            try
            {


                Type officeType = Type.GetTypeFromProgID("Excel.Application");

                if (officeType == null)
                {
                    string Mesage2 = "У Вас не установлен Excel!";

                    if (MessageBox.Show(Mesage2, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {

                    exApp.SheetsInNewWorkbook = 2;

                    exApp.Workbooks.Add();
                    exApp.DisplayAlerts = false;

                    Excel.Worksheet workSheet = (Excel.Worksheet)exApp.Worksheets.get_Item(1);
                    Excel.Worksheet workSheet2 = (Excel.Worksheet)exApp.Worksheets.get_Item(2);

                    workSheet.Name = $"Акт ремонта №{numberAct.Replace('/', '.')}";
                    workSheet2.Name = $"ФОУ №{numberAct.Replace('/', '.')}";

                    #region Акт ремонта

                    workSheet.Rows.Font.Size = 11;
                    workSheet.Rows.Font.Name = "Times New Roman";

                    workSheet.PageSetup.CenterHorizontally = true;
                    //workSheet.PageSetup.CenterVertically = true;
                    workSheet.PageSetup.TopMargin = 0.0;
                    workSheet.PageSetup.BottomMargin = 0;
                    workSheet.PageSetup.LeftMargin = 0;
                    workSheet.PageSetup.RightMargin = 0;

                    workSheet.PageSetup.Zoom = 90;

                    Excel.Range _excelCells1 = (Excel.Range)workSheet.get_Range("A1", "F1").Cells;
                    Excel.Range _excelCells2 = (Excel.Range)workSheet.get_Range("G1", "I1").Cells;
                    Excel.Range _excelCells3 = (Excel.Range)workSheet.get_Range("G1").Cells;
                    Excel.Range _excelCells4 = (Excel.Range)workSheet.get_Range("A2", "I2").Cells;
                    Excel.Range _excelCells5 = (Excel.Range)workSheet.get_Range("A4", "C4").Cells;
                    Excel.Range _excelCells6 = (Excel.Range)workSheet.get_Range("G4", "I4").Cells;
                    Excel.Range _excelCells7 = (Excel.Range)workSheet.get_Range("A5", "C5").Cells;
                    Excel.Range _excelCells8 = (Excel.Range)workSheet.get_Range("G5", "I5").Cells;
                    Excel.Range _excelCells9 = (Excel.Range)workSheet.get_Range("A6", "F6").Cells;
                    Excel.Range _excelCells10 = (Excel.Range)workSheet.get_Range("A7", "D7").Cells;
                    Excel.Range _excelCells11 = (Excel.Range)workSheet.get_Range("G7", "I7").Cells;
                    Excel.Range _excelCells12 = (Excel.Range)workSheet.get_Range("A8", "D8").Cells;
                    Excel.Range _excelCells13 = (Excel.Range)workSheet.get_Range("G8", "I8").Cells;
                    Excel.Range _excelCells14 = (Excel.Range)workSheet.get_Range("A9", "I9").Cells;
                    Excel.Range _excelCells15 = (Excel.Range)workSheet.get_Range("A10", "C10").Cells;
                    Excel.Range _excelCells16 = (Excel.Range)workSheet.get_Range("D10", "E10").Cells;
                    Excel.Range _excelCells17 = (Excel.Range)workSheet.get_Range("F10", "I10").Cells;
                    Excel.Range _excelCells18 = (Excel.Range)workSheet.get_Range("A11", "E11").Cells;
                    Excel.Range _excelCells19 = (Excel.Range)workSheet.get_Range("F11", "I11").Cells;
                    Excel.Range _excelCells20 = (Excel.Range)workSheet.get_Range("A12", "E12").Cells;
                    Excel.Range _excelCells21 = (Excel.Range)workSheet.get_Range("F12", "I12").Cells;
                    Excel.Range _excelCells22 = (Excel.Range)workSheet.get_Range("A13", "B13").Cells;
                    Excel.Range _excelCells23 = (Excel.Range)workSheet.get_Range("C13", "D13").Cells;
                    Excel.Range _excelCells24 = (Excel.Range)workSheet.get_Range("E13", "G13").Cells;
                    Excel.Range _excelCells25 = (Excel.Range)workSheet.get_Range("H13", "I13").Cells;
                    Excel.Range _excelCells26 = (Excel.Range)workSheet.get_Range("A14", "I14").Cells;
                    Excel.Range _excelCells27 = (Excel.Range)workSheet.get_Range("A15").Cells;
                    Excel.Range _excelCells28 = (Excel.Range)workSheet.get_Range("B15").Cells;
                    Excel.Range _excelCells29 = (Excel.Range)workSheet.get_Range("C15", "D15").Cells;
                    Excel.Range _excelCells30 = (Excel.Range)workSheet.get_Range("E15", "F15").Cells;
                    Excel.Range _excelCells31 = (Excel.Range)workSheet.get_Range("G15", "H15").Cells;
                    Excel.Range _excelCells32 = (Excel.Range)workSheet.get_Range("I15").Cells;
                    Excel.Range _excelCells33 = (Excel.Range)workSheet.get_Range("A15", "I23").Cells;
                    Excel.Range _excelCells34 = (Excel.Range)workSheet.get_Range("A24", "I24").Cells;
                    Excel.Range _excelCells38 = (Excel.Range)workSheet.get_Range("A26", "D26").Cells;
                    Excel.Range _excelCells39 = (Excel.Range)workSheet.get_Range("E26", "F26").Cells;
                    Excel.Range _excelCells40 = (Excel.Range)workSheet.get_Range("G26", "I26").Cells;
                    Excel.Range _excelCells41 = (Excel.Range)workSheet.get_Range("A27", "D27").Cells;
                    Excel.Range _excelCells42 = (Excel.Range)workSheet.get_Range("E27", "F27").Cells;
                    Excel.Range _excelCells43 = (Excel.Range)workSheet.get_Range("G27", "I27").Cells;
                    Excel.Range _excelCells44 = (Excel.Range)workSheet.get_Range("A28", "E28").Cells;
                    Excel.Range _excelCells45 = (Excel.Range)workSheet.get_Range("F28", "I28").Cells;
                    Excel.Range _excelCells46 = (Excel.Range)workSheet.get_Range("A30", "I30").Cells;
                    Excel.Range _excelCells47 = (Excel.Range)workSheet.get_Range("C30", "D30").Cells;
                    Excel.Range _excelCells48 = (Excel.Range)workSheet.get_Range("H30", "I30").Cells;
                    Excel.Range _excelCells49 = (Excel.Range)workSheet.get_Range("A31", "B31").Cells;
                    Excel.Range _excelCells50 = (Excel.Range)workSheet.get_Range("A31", "I31").Cells;
                    Excel.Range _excelCells51 = (Excel.Range)workSheet.get_Range("C31", "D31").Cells;
                    Excel.Range _excelCells52 = (Excel.Range)workSheet.get_Range("F31", "G31").Cells;
                    Excel.Range _excelCells53 = (Excel.Range)workSheet.get_Range("H31", "I31").Cells;
                    Excel.Range _excelCells54 = (Excel.Range)workSheet.get_Range("A32", "E32").Cells;
                    Excel.Range _excelCells55 = (Excel.Range)workSheet.get_Range("A34", "D34").Cells;
                    Excel.Range _excelCells56 = (Excel.Range)workSheet.get_Range("A35", "D35").Cells;
                    Excel.Range _excelCells57 = (Excel.Range)workSheet.get_Range("A35", "B35").Cells;
                    Excel.Range _excelCells58 = (Excel.Range)workSheet.get_Range("C35", "D35").Cells;
                    Excel.Range _excelCells59 = (Excel.Range)workSheet.get_Range("C36", "D36").Cells;
                    Excel.Range _excelCells60 = (Excel.Range)workSheet.get_Range("F32", "G32").Cells;

                    int xy = 16;
                    int xz = 16;
                    for (int i = 0; i < 8; i++)
                    {
                        Excel.Range _excelCells35 = (Excel.Range)workSheet.get_Range($"C{xy}", $"D{xy}").Cells;
                        Excel.Range _excelCells36 = (Excel.Range)workSheet.get_Range($"E{xz}", $"F{xz}").Cells;
                        Excel.Range _excelCells37 = (Excel.Range)workSheet.get_Range($"G{xz}", $"H{xz}").Cells;

                        _excelCells35.Merge(Type.Missing);
                        _excelCells36.Merge(Type.Missing);
                        _excelCells37.Merge(Type.Missing);

                        xy++;
                        xz++;
                    }

                    _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlDash;
                    _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlDash;
                    _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlDash;
                    _excelCells33.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlDash;
                    _excelCells33.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlDash;
                    _excelCells33.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlDash;

                    _excelCells1.Merge(Type.Missing);
                    _excelCells2.Merge(Type.Missing);
                    _excelCells4.Merge(Type.Missing);
                    _excelCells5.Merge(Type.Missing);
                    _excelCells6.Merge(Type.Missing);
                    _excelCells7.Merge(Type.Missing);
                    _excelCells8.Merge(Type.Missing);
                    _excelCells9.Merge(Type.Missing);
                    _excelCells10.Merge(Type.Missing);
                    _excelCells11.Merge(Type.Missing);
                    _excelCells12.Merge(Type.Missing);
                    _excelCells13.Merge(Type.Missing);
                    _excelCells14.Merge(Type.Missing);
                    _excelCells15.Merge(Type.Missing);
                    _excelCells16.Merge(Type.Missing);
                    _excelCells17.Merge(Type.Missing);
                    _excelCells18.Merge(Type.Missing);
                    _excelCells19.Merge(Type.Missing);
                    _excelCells20.Merge(Type.Missing);
                    _excelCells21.Merge(Type.Missing);
                    _excelCells22.Merge(Type.Missing);
                    _excelCells23.Merge(Type.Missing);
                    _excelCells24.Merge(Type.Missing);
                    _excelCells25.Merge(Type.Missing);
                    _excelCells26.Merge(Type.Missing);
                    _excelCells29.Merge(Type.Missing);
                    _excelCells30.Merge(Type.Missing);
                    _excelCells31.Merge(Type.Missing);
                    _excelCells34.Merge(Type.Missing);
                    _excelCells38.Merge(Type.Missing);
                    _excelCells39.Merge(Type.Missing);
                    _excelCells40.Merge(Type.Missing);
                    _excelCells41.Merge(Type.Missing);
                    _excelCells42.Merge(Type.Missing);
                    _excelCells43.Merge(Type.Missing);
                    _excelCells44.Merge(Type.Missing);
                    _excelCells45.Merge(Type.Missing);
                    _excelCells47.Merge(Type.Missing);
                    _excelCells48.Merge(Type.Missing);
                    _excelCells49.Merge(Type.Missing);
                    _excelCells51.Merge(Type.Missing);
                    _excelCells52.Merge(Type.Missing);
                    _excelCells53.Merge(Type.Missing);
                    _excelCells54.Merge(Type.Missing);
                    _excelCells57.Merge(Type.Missing);
                    _excelCells58.Merge(Type.Missing);
                    _excelCells59.Merge(Type.Missing);
                    _excelCells60.Merge(Type.Missing);

                    _excelCells1.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    _excelCells2.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells4.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells5.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells6.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells7.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells8.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells9.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells10.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells11.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells12.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells13.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells14.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells15.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells16.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells17.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells18.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells19.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells20.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells21.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells22.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    _excelCells23.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells24.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells25.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells26.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells27.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells27.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells28.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells28.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells29.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells29.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells30.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells30.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells31.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells31.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells32.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells32.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells32.Orientation = 90;

                    _excelCells33.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells33.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells34.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells38.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells39.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells40.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells41.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells42.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells43.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells44.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells45.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells46.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells50.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells54.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells56.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells59.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells60.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    _excelCells3.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells5.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells6.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells10.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells11.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells15.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells16.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells17.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells18.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells19.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells23.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells25.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells38.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells39.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells40.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells46.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells55.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                    Excel.Range range_Consolidated = workSheet.Rows.get_Range("A1", "I2");
                    Excel.Range range_Consolidated2 = workSheet.Rows.get_Range("A4", "I4");
                    Excel.Range range_Consolidated3 = workSheet.Rows.get_Range("A5", "I5");
                    Excel.Range range_Consolidated4 = workSheet.Rows.get_Range("A8", "I8");
                    Excel.Range range_Consolidated5 = workSheet.Rows.get_Range("A12", "I12");
                    Excel.Range range_Consolidated6 = workSheet.Rows.get_Range("A15", "I15");
                    Excel.Range range_Consolidated7 = workSheet.Rows.get_Range("A16", "I23");
                    Excel.Range range_Consolidated8 = workSheet.Rows.get_Range("A27", "I27");
                    Excel.Range range_Consolidated9 = workSheet.Rows.get_Range("A28", "I28");
                    Excel.Range range_Consolidated10 = workSheet.Rows.get_Range("A31", "I31");
                    Excel.Range range_Consolidated11 = workSheet.Rows.get_Range("A32", "E33");
                    Excel.Range range_Consolidated12 = workSheet.Rows.get_Range("A35", "I36");
                    Excel.Range range_Consolidated13 = workSheet.Rows.get_Range("F32", "G32");
                    Excel.Range range_Consolidated14 = workSheet.Rows.get_Range("C17", "D17");
                    Excel.Range range_Consolidated15 = workSheet.Rows.get_Range("C18", "D18");
                    Excel.Range range_Consolidated16 = workSheet.Rows.get_Range("C16", "D16");

                    range_Consolidated.Font.Bold = true;
                    range_Consolidated.Font.Size = 14;
                    range_Consolidated2.Font.Bold = true;
                    range_Consolidated2.Font.Size = 12;
                    range_Consolidated3.Font.Size = 8;
                    range_Consolidated4.Font.Size = 8;
                    range_Consolidated5.Font.Size = 8;
                    range_Consolidated6.Font.Size = 11;
                    range_Consolidated7.Font.Size = 8.5;
                    range_Consolidated8.Font.Size = 8;
                    range_Consolidated9.Font.Bold = true;
                    range_Consolidated10.Font.Size = 8;
                    range_Consolidated11.Font.Bold = true;
                    range_Consolidated12.Font.Size = 8;
                    range_Consolidated13.Font.Size = 8;
                    range_Consolidated14.NumberFormat = "@";
                    range_Consolidated15.NumberFormat = "@";
                    range_Consolidated16.NumberFormat = "@";

                    Excel.Range rowHeight = workSheet.get_Range("A5", "I5");
                    rowHeight.EntireRow.RowHeight = 12;

                    Excel.Range rowHeight2 = workSheet.get_Range("A7", "I7");
                    rowHeight2.EntireRow.RowHeight = 20;

                    Excel.Range rowHeight3 = workSheet.get_Range("A8", "I8");
                    rowHeight3.EntireRow.RowHeight = 12;

                    Excel.Range rowHeight4 = workSheet.get_Range("A12", "I12");
                    rowHeight4.EntireRow.RowHeight = 12;

                    Excel.Range rowHeight5 = workSheet.get_Range("A14", "I14");
                    rowHeight5.EntireRow.RowHeight = 45;

                    Excel.Range rowHeight6 = workSheet.get_Range("A15", "I15");
                    rowHeight6.EntireRow.RowHeight = 65;

                    Excel.Range rowColum7 = workSheet.get_Range("A15");
                    rowColum7.EntireColumn.ColumnWidth = 10;
                    Excel.Range rowColum8 = workSheet.get_Range("B15");
                    rowColum8.EntireColumn.ColumnWidth = 14;
                    Excel.Range rowColum9 = workSheet.get_Range("C15", "D15");
                    rowColum9.EntireColumn.ColumnWidth = 11;
                    Excel.Range rowColum10 = workSheet.get_Range("E15", "F15");
                    rowColum10.EntireColumn.ColumnWidth = 10;
                    Excel.Range rowColum11 = workSheet.get_Range("G15", "H15");
                    rowColum11.EntireColumn.ColumnWidth = 9;

                    Excel.Range rowHeight12 = workSheet.get_Range("A16", "I16");
                    rowHeight12.EntireRow.RowHeight = 40;
                    Excel.Range rowHeight13 = workSheet.get_Range("A17", "I17");
                    rowHeight13.EntireRow.RowHeight = 40;
                    Excel.Range rowHeight14 = workSheet.get_Range("A18", "I18");
                    rowHeight14.EntireRow.RowHeight = 40;
                    Excel.Range rowHeight15 = workSheet.get_Range("A19", "I19");
                    rowHeight15.EntireRow.RowHeight = 40;
                    Excel.Range rowHeight16 = workSheet.get_Range("A20", "I20");
                    rowHeight16.EntireRow.RowHeight = 40;
                    Excel.Range rowHeight17 = workSheet.get_Range("A21", "I21");
                    rowHeight17.EntireRow.RowHeight = 40;
                    Excel.Range rowHeight18 = workSheet.get_Range("A22", "I22");
                    rowHeight18.EntireRow.RowHeight = 40;
                    Excel.Range rowHeight19 = workSheet.get_Range("A23", "I23");
                    rowHeight19.EntireRow.RowHeight = 40;

                    Excel.Range rowHeight20 = workSheet.get_Range("A24", "I24");
                    rowHeight20.EntireRow.RowHeight = 35;

                    Excel.Range rowHeight21 = workSheet.get_Range("A27", "I27");
                    rowHeight21.EntireRow.RowHeight = 12;
                    Excel.Range rowHeight22 = workSheet.get_Range("A31", "I31");
                    rowHeight22.EntireRow.RowHeight = 12;
                    Excel.Range rowHeight23 = workSheet.get_Range("A35", "I35");
                    rowHeight23.EntireRow.RowHeight = 12;


                    workSheet.Cells[1, 1] = $"ПЕРВИЧНЫЙ ТЕХНИЧЕСКИЙ АКТ №";
                    workSheet.Cells[1, 7] = $"{numberAct}";
                    workSheet.Cells[2, 1] = $"выполненных работ по ремонту систем радиосвязи";
                    workSheet.Cells[4, 1] = $"{city}";
                    workSheet.Cells[5, 1] = $"город";
                    workSheet.Cells[5, 7] = $"дата";
                    workSheet.Cells[6, 1] = $"Мы, нижеподписавшиеся, представитель Исполнителя :";
                    workSheet.Cells[7, 1] = $"Начальник участка по ТО и ремонту СРС";
                    workSheet.Cells[7, 7] = $"{FIO_chief}";
                    workSheet.Cells[8, 1] = $"должность";
                    workSheet.Cells[8, 7] = $"фамилия, инициалы";
                    workSheet.Cells[9, 1] = $"действующий по доверенности № {doverennost} с одной стороны и представитель";
                    workSheet.Cells[10, 1] = $"эксплуатирующей организации:";
                    workSheet.Cells[10, 4] = $"{company}";
                    workSheet.Cells[10, 6] = $"{polinon_full} (полигон {poligon})";
                    workSheet.Cells[11, 1] = $"{post}";
                    workSheet.Cells[11, 6] = $"{representative}";
                    workSheet.Cells[12, 1] = $"должность";
                    workSheet.Cells[12, 6] = $"фамилия, инициалы";
                    workSheet.Cells[13, 1] = $"дата выдачи:";
                    workSheet.Cells[13, 3] = $"{dateIssue}";
                    workSheet.Cells[13, 5] = $"служебное удостоверение №:";
                    workSheet.Cells[13, 8] = $"{numberIdentification}";
                    workSheet.Cells[14, 1] = $"с другой стороны составили настоящий акт в том, что во исполнение договора № 4176190 от 07 декабря 2020 г.,\n" +
                                             $"были выполнены работы по ремонту систем радиосвязи и использованы заменяемые детали и расходные\n" +
                                             $"материалы в количестве:";
                    workSheet.Cells[15, 1] = $"Категория\nсложности\nремонтных работ";
                    workSheet.Cells[15, 2] = $"Модель";
                    workSheet.Cells[15, 3] = $"Учетный номер\nрадиостанции";
                    workSheet.Cells[15, 5] = $"Выполненные работы";
                    workSheet.Cells[15, 7] = $"Израсходованные\nматериалы и детали";
                    workSheet.Cells[15, 9] = $"Кол-во\n(шт.)";
                    workSheet.Cells[16, 1] = $"{сategory}";
                    workSheet.Cells[16, 2] = $"{model}";
                    workSheet.Cells[16, 3] = $"{serialNumber}";
                    workSheet.Cells[17, 3] = $"инв№ {inventoryNumber}";
                    workSheet.Cells[18, 3] = $"сет№ {networkNumber}";
                    workSheet.Cells[17, 5] = $"\n{сompleted_works_1}\n";
                    workSheet.Cells[17, 7] = $"\n{parts_1}\n";
                    if (сompleted_works_1 != "" || parts_1 != "")
                    {
                        workSheet.Cells[17, 9] = $"1";
                    }
                    workSheet.Cells[18, 5] = $"\n{сompleted_works_2}\n";
                    workSheet.Cells[18, 7] = $"\n{parts_2}\n";
                    if (сompleted_works_2 != "" || parts_2 != "")
                    {
                        workSheet.Cells[18, 9] = $"1";
                    }
                    workSheet.Cells[19, 5] = $"\n{сompleted_works_3}\n";
                    workSheet.Cells[19, 7] = $"\n{parts_3}\n";
                    if (сompleted_works_3 != "" || parts_3 != "")
                    {
                        workSheet.Cells[19, 9] = $"1";
                    }
                    workSheet.Cells[20, 5] = $"\n{сompleted_works_4}\n";
                    workSheet.Cells[20, 7] = $"\n{parts_4}\n";
                    if (сompleted_works_4 != "" || parts_4 != "")
                    {
                        workSheet.Cells[20, 9] = $"1";
                    }
                    workSheet.Cells[21, 5] = $"\n{сompleted_works_5}\n";
                    workSheet.Cells[21, 7] = $"\n{parts_5}\n";
                    if (сompleted_works_5 != "" || parts_5 != "")
                    {
                        workSheet.Cells[21, 9] = $"1";
                    }
                    workSheet.Cells[22, 5] = $"\n{сompleted_works_6}\n";
                    workSheet.Cells[22, 7] = $"\n{parts_6}\n";
                    if (сompleted_works_6 != "" || parts_6 != "")
                    {
                        workSheet.Cells[22, 9] = $"1";
                    }
                    workSheet.Cells[23, 5] = $"\n{сompleted_works_7}\n";
                    workSheet.Cells[23, 7] = $"\n{parts_7}\n";
                    if (сompleted_works_7 != "" || parts_7 != "")
                    {
                        workSheet.Cells[23, 9] = $"1";
                    }
                    workSheet.Cells[24, 1] = $"Системы радиосвязи работоспособны, технические характеристики вышеперечисленных соответствуют нормам.\n" +
                                             $"Представитель эксплуатирующей организации по качеству выполненных работ претензий к исполнителю не имеет.";

                    workSheet.Cells[26, 1] = $"Исполнитель работ: инженер по ТО и ремонту СРС";
                    workSheet.Cells[26, 7] = $"{FIO_Engineer}";
                    workSheet.Cells[27, 1] = $"должность";
                    workSheet.Cells[27, 5] = $"подпись";
                    workSheet.Cells[27, 7] = $"расшифровка подписи";
                    workSheet.Cells[28, 1] = $"Представитель Заказчика (зксплуатирующей организации):";
                    workSheet.Cells[28, 6] = $"Представитель Исполнителя:";
                    workSheet.Cells[30, 3] = $"{representative}";
                    workSheet.Cells[30, 8] = $"{FIO_chief}";
                    workSheet.Cells[31, 1] = $"подпись";
                    workSheet.Cells[31, 3] = $"расшифровка подписи";
                    workSheet.Cells[31, 6] = $"подпись";
                    workSheet.Cells[31, 8] = $"расшифровка подписи";
                    workSheet.Cells[32, 1] = $"Представитель РЦС:";
                    workSheet.Cells[35, 1] = $"подпись";
                    workSheet.Cells[35, 3] = $"расшифровка подписи";
                    workSheet.Cells[36, 3] = $"М.П.";
                    workSheet.Cells[32, 6] = $"М.П.";

                    #endregion

                    #region ФОУ-18

                    workSheet2.Rows.Font.Size = 9;
                    workSheet2.Rows.Font.Name = "Times New Roman";

                    workSheet2.PageSetup.CenterHorizontally = true;
                    workSheet2.PageSetup.TopMargin = 0;
                    workSheet2.PageSetup.BottomMargin = 0;
                    workSheet2.PageSetup.LeftMargin = 0;
                    workSheet2.PageSetup.RightMargin = 0;

                    workSheet2.PageSetup.Zoom = 90;

                    Excel.Range _excelCells100 = (Excel.Range)workSheet2.get_Range("G1", "K1").Cells;
                    Excel.Range _excelCells101 = (Excel.Range)workSheet2.get_Range("F2", "K2").Cells;
                    Excel.Range _excelCells102 = (Excel.Range)workSheet2.get_Range("I5", "J5").Cells;
                    Excel.Range _excelCells103 = (Excel.Range)workSheet2.get_Range("J6", "J7").Cells;
                    Excel.Range _excelCells104 = (Excel.Range)workSheet2.get_Range("J8", "J9").Cells;
                    Excel.Range _excelCells105 = (Excel.Range)workSheet2.get_Range("K4", "K9").Cells;
                    Excel.Range _excelCells106 = (Excel.Range)workSheet2.get_Range("K6", "K7").Cells;
                    Excel.Range _excelCells107 = (Excel.Range)workSheet2.get_Range("K8", "K9").Cells;
                    Excel.Range _excelCells108 = (Excel.Range)workSheet2.get_Range("K5", "K9").Cells;
                    Excel.Range _excelCells109 = (Excel.Range)workSheet2.get_Range("A7", "I7").Cells;
                    Excel.Range _excelCells110 = (Excel.Range)workSheet2.get_Range("A7", "I10").Cells;
                    Excel.Range _excelCells111 = (Excel.Range)workSheet2.get_Range("A8", "I8").Cells;
                    Excel.Range _excelCells112 = (Excel.Range)workSheet2.get_Range("A9", "I9").Cells;
                    Excel.Range _excelCells113 = (Excel.Range)workSheet2.get_Range("A10", "I10").Cells;
                    Excel.Range _excelCells114 = (Excel.Range)workSheet2.get_Range("J11", "K12").Cells;
                    Excel.Range _excelCells115 = (Excel.Range)workSheet2.get_Range("H12", "I12").Cells;
                    Excel.Range _excelCells116 = (Excel.Range)workSheet2.get_Range("J13", "K13").Cells;
                    Excel.Range _excelCells117 = (Excel.Range)workSheet2.get_Range("J14", "K14").Cells;
                    Excel.Range _excelCells118 = (Excel.Range)workSheet2.get_Range("I14", "K14").Cells;
                    Excel.Range _excelCells119 = (Excel.Range)workSheet2.get_Range("J15", "K15").Cells;
                    Excel.Range _excelCells120 = (Excel.Range)workSheet2.get_Range("D17", "H17").Cells;
                    Excel.Range _excelCells121 = (Excel.Range)workSheet2.get_Range("D18", "F18").Cells;
                    Excel.Range _excelCells122 = (Excel.Range)workSheet2.get_Range("G18", "H18").Cells;
                    Excel.Range _excelCells123 = (Excel.Range)workSheet2.get_Range("D18", "H19").Cells;
                    Excel.Range _excelCells124 = (Excel.Range)workSheet2.get_Range("D19", "F19").Cells;
                    Excel.Range _excelCells125 = (Excel.Range)workSheet2.get_Range("G19", "H19").Cells;
                    Excel.Range _excelCells126 = (Excel.Range)workSheet2.get_Range("A21", "D21").Cells;
                    Excel.Range _excelCells127 = (Excel.Range)workSheet2.get_Range("E21", "G21").Cells;
                    Excel.Range _excelCells128 = (Excel.Range)workSheet2.get_Range("H21", "K21").Cells;
                    Excel.Range _excelCells129 = (Excel.Range)workSheet2.get_Range("A22", "B22").Cells;
                    Excel.Range _excelCells130 = (Excel.Range)workSheet2.get_Range("C22", "E22").Cells;
                    Excel.Range _excelCells131 = (Excel.Range)workSheet2.get_Range("F22", "G22").Cells;
                    Excel.Range _excelCells132 = (Excel.Range)workSheet2.get_Range("H22", "K22").Cells;
                    Excel.Range _excelCells133 = (Excel.Range)workSheet2.get_Range("A23", "C23").Cells;
                    Excel.Range _excelCells134 = (Excel.Range)workSheet2.get_Range("D23", "G23").Cells;
                    Excel.Range _excelCells135 = (Excel.Range)workSheet2.get_Range("A24", "B24").Cells;
                    Excel.Range _excelCells136 = (Excel.Range)workSheet2.get_Range("C24", "K24").Cells;
                    Excel.Range _excelCells137 = (Excel.Range)workSheet2.get_Range("A25", "K25").Cells;
                    Excel.Range _excelCells138 = (Excel.Range)workSheet2.get_Range("C25", "G25").Cells;
                    Excel.Range _excelCells139 = (Excel.Range)workSheet2.get_Range("A26", "H26").Cells;
                    Excel.Range _excelCells140 = (Excel.Range)workSheet2.get_Range("I26", "K26").Cells;
                    Excel.Range _excelCells141 = (Excel.Range)workSheet2.get_Range("I27", "K27").Cells;
                    Excel.Range _excelCells143 = (Excel.Range)workSheet2.get_Range("D28", "K28").Cells;
                    Excel.Range _excelCells144 = (Excel.Range)workSheet2.get_Range("B29").Cells;
                    Excel.Range _excelCells145 = (Excel.Range)workSheet2.get_Range("C29", "K29").Cells;
                    Excel.Range _excelCells146 = (Excel.Range)workSheet2.get_Range("A28", "A36").Cells;
                    Excel.Range _excelCells148 = (Excel.Range)workSheet2.get_Range("F30", "K30").Cells;
                    Excel.Range _excelCells149 = (Excel.Range)workSheet2.get_Range("B33", "K33").Cells;
                    Excel.Range _excelCells150 = (Excel.Range)workSheet2.get_Range("B34", "K34").Cells;
                    Excel.Range _excelCells152 = (Excel.Range)workSheet2.get_Range("G35", "K35").Cells;
                    Excel.Range _excelCells153 = (Excel.Range)workSheet2.get_Range("B36", "K36").Cells;
                    Excel.Range _excelCells154 = (Excel.Range)workSheet2.get_Range("A38", "K38").Cells;
                    Excel.Range _excelCells155 = (Excel.Range)workSheet2.get_Range("B38", "C38").Cells;
                    Excel.Range _excelCells156 = (Excel.Range)workSheet2.get_Range("D38", "E38").Cells;
                    Excel.Range _excelCells157 = (Excel.Range)workSheet2.get_Range("A38", "K46").Cells;
                    Excel.Range _excelCells158 = (Excel.Range)workSheet2.get_Range("A40", "K46").Cells;
                    Excel.Range _excelCells161 = (Excel.Range)workSheet2.get_Range("B49", "D49").Cells;
                    Excel.Range _excelCells162 = (Excel.Range)workSheet2.get_Range("I49", "J49").Cells;
                    Excel.Range _excelCells163 = (Excel.Range)workSheet2.get_Range("B50", "D50").Cells;
                    Excel.Range _excelCells164 = (Excel.Range)workSheet2.get_Range("F50", "G50").Cells;
                    Excel.Range _excelCells165 = (Excel.Range)workSheet2.get_Range("I50", "J50").Cells;
                    Excel.Range _excelCells166 = (Excel.Range)workSheet2.get_Range("F49", "G49").Cells;
                    Excel.Range _excelCells167 = (Excel.Range)workSheet2.get_Range("B52", "D52").Cells;
                    Excel.Range _excelCells168 = (Excel.Range)workSheet2.get_Range("F52", "G52").Cells;
                    Excel.Range _excelCells169 = (Excel.Range)workSheet2.get_Range("I52", "J52").Cells;
                    Excel.Range _excelCells170 = (Excel.Range)workSheet2.get_Range("B53", "D53").Cells;
                    Excel.Range _excelCells171 = (Excel.Range)workSheet2.get_Range("F53", "G53").Cells;
                    Excel.Range _excelCells172 = (Excel.Range)workSheet2.get_Range("I53", "J53").Cells;
                    Excel.Range _excelCells173 = (Excel.Range)workSheet2.get_Range("B55", "D55").Cells;
                    Excel.Range _excelCells174 = (Excel.Range)workSheet2.get_Range("F55", "G55").Cells;
                    Excel.Range _excelCells175 = (Excel.Range)workSheet2.get_Range("I55", "J55").Cells;
                    Excel.Range _excelCells176 = (Excel.Range)workSheet2.get_Range("B56", "D56").Cells;
                    Excel.Range _excelCells177 = (Excel.Range)workSheet2.get_Range("F56", "G56").Cells;
                    Excel.Range _excelCells178 = (Excel.Range)workSheet2.get_Range("I56", "J56").Cells;
                    Excel.Range _excelCells179 = (Excel.Range)workSheet2.get_Range("B58", "D58").Cells;
                    Excel.Range _excelCells180 = (Excel.Range)workSheet2.get_Range("F58", "G58").Cells;
                    Excel.Range _excelCells181 = (Excel.Range)workSheet2.get_Range("I58", "J58").Cells;
                    Excel.Range _excelCells182 = (Excel.Range)workSheet2.get_Range("B59", "D59").Cells;
                    Excel.Range _excelCells183 = (Excel.Range)workSheet2.get_Range("F59", "G59").Cells;
                    Excel.Range _excelCells184 = (Excel.Range)workSheet2.get_Range("I59", "J59").Cells;
                    Excel.Range _excelCells185 = (Excel.Range)workSheet2.get_Range("A63", "D63").Cells;
                    Excel.Range _excelCells186 = (Excel.Range)workSheet2.get_Range("F62", "G62").Cells;
                    Excel.Range _excelCells187 = (Excel.Range)workSheet2.get_Range("I62", "J62").Cells;
                    Excel.Range _excelCells188 = (Excel.Range)workSheet2.get_Range("F63", "G63").Cells;
                    Excel.Range _excelCells189 = (Excel.Range)workSheet2.get_Range("I63", "J63").Cells;



                    int z1 = 39;
                    int z2 = 39;
                    for (int i = 0; i < 8; i++)
                    {
                        Excel.Range _excelCells159 = (Excel.Range)workSheet2.get_Range($"B{z1}", $"C{z1}").Cells;
                        Excel.Range _excelCells160 = (Excel.Range)workSheet2.get_Range($"D{z2}", $"E{z2}").Cells;
                        _excelCells159.Merge(Type.Missing);
                        _excelCells160.Merge(Type.Missing);

                        z1++;
                        z2++;
                    }

                    _excelCells108.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells108.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells108.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells108.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells108.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells108.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells109.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells112.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells114.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells118.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                    _excelCells123.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells123.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells123.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells123.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells123.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells123.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;

                    _excelCells127.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells128.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells130.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells132.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells134.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells136.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells137.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells143.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells145.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells148.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells149.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells150.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells152.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells153.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;

                    _excelCells157.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells157.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells157.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells157.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells157.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells157.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells161.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells162.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells166.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells167.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells168.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells169.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells173.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells174.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells175.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells179.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells180.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells181.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells186.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                    _excelCells187.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;


                    _excelCells100.Merge(Type.Missing);
                    _excelCells101.Merge(Type.Missing);
                    _excelCells102.Merge(Type.Missing);
                    _excelCells103.Merge(Type.Missing);
                    _excelCells104.Merge(Type.Missing);
                    _excelCells106.Merge(Type.Missing);
                    _excelCells107.Merge(Type.Missing);
                    _excelCells109.Merge(Type.Missing);
                    _excelCells111.Merge(Type.Missing);
                    _excelCells112.Merge(Type.Missing);
                    _excelCells113.Merge(Type.Missing);
                    _excelCells114.Merge(Type.Missing);
                    _excelCells115.Merge(Type.Missing);
                    _excelCells116.Merge(Type.Missing);
                    _excelCells117.Merge(Type.Missing);
                    _excelCells119.Merge(Type.Missing);
                    _excelCells120.Merge(Type.Missing);
                    _excelCells121.Merge(Type.Missing);
                    _excelCells122.Merge(Type.Missing);
                    _excelCells124.Merge(Type.Missing);
                    _excelCells125.Merge(Type.Missing);
                    _excelCells126.Merge(Type.Missing);
                    _excelCells127.Merge(Type.Missing);
                    _excelCells128.Merge(Type.Missing);
                    _excelCells129.Merge(Type.Missing);
                    _excelCells130.Merge(Type.Missing);
                    _excelCells131.Merge(Type.Missing);
                    _excelCells132.Merge(Type.Missing);
                    _excelCells133.Merge(Type.Missing);
                    _excelCells134.Merge(Type.Missing);
                    _excelCells135.Merge(Type.Missing);
                    _excelCells136.Merge(Type.Missing);
                    _excelCells138.Merge(Type.Missing);
                    _excelCells139.Merge(Type.Missing);
                    _excelCells140.Merge(Type.Missing);
                    _excelCells141.Merge(Type.Missing);
                    _excelCells144.Merge(Type.Missing);
                    _excelCells155.Merge(Type.Missing);
                    _excelCells156.Merge(Type.Missing);
                    _excelCells161.Merge(Type.Missing);
                    _excelCells162.Merge(Type.Missing);
                    _excelCells163.Merge(Type.Missing);
                    _excelCells164.Merge(Type.Missing);
                    _excelCells165.Merge(Type.Missing);
                    _excelCells166.Merge(Type.Missing);
                    _excelCells167.Merge(Type.Missing);
                    _excelCells168.Merge(Type.Missing);
                    _excelCells169.Merge(Type.Missing);
                    _excelCells170.Merge(Type.Missing);
                    _excelCells171.Merge(Type.Missing);
                    _excelCells172.Merge(Type.Missing);
                    _excelCells173.Merge(Type.Missing);
                    _excelCells174.Merge(Type.Missing);
                    _excelCells175.Merge(Type.Missing);
                    _excelCells176.Merge(Type.Missing);
                    _excelCells177.Merge(Type.Missing);
                    _excelCells178.Merge(Type.Missing);
                    _excelCells179.Merge(Type.Missing);
                    _excelCells180.Merge(Type.Missing);
                    _excelCells181.Merge(Type.Missing);
                    _excelCells182.Merge(Type.Missing);
                    _excelCells183.Merge(Type.Missing);
                    _excelCells184.Merge(Type.Missing);
                    _excelCells185.Merge(Type.Missing);
                    _excelCells186.Merge(Type.Missing);
                    _excelCells187.Merge(Type.Missing);
                    _excelCells188.Merge(Type.Missing);
                    _excelCells189.Merge(Type.Missing);

                    _excelCells100.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    _excelCells101.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    _excelCells102.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    _excelCells103.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    _excelCells104.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    _excelCells105.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells109.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells110.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells112.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells112.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells114.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells114.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells115.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    _excelCells116.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells117.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells119.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells120.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells120.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells123.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells126.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells127.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells128.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells129.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells130.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells131.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells132.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells132.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells134.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells135.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells136.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells138.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells139.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells140.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells141.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells144.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    _excelCells146.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells154.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells154.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells158.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells158.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells161.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells162.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells163.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells164.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells165.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells167.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells169.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells170.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells171.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells172.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells173.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells174.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells175.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells176.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells177.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells178.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells179.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells180.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells181.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells182.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells183.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells184.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells185.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells186.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells187.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells188.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    _excelCells189.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                    Excel.Range rowHeight100 = workSheet2.get_Range("A9", "I9");
                    rowHeight100.EntireRow.RowHeight = 40;

                    Excel.Range rowHeight101 = workSheet2.get_Range("J11", "K11");
                    rowHeight101.EntireRow.RowHeight = 15;

                    Excel.Range rowHeight102 = workSheet2.get_Range("J14", "K14");
                    rowHeight102.EntireRow.RowHeight = 15;

                    Excel.Range rowHeight103 = workSheet2.get_Range("A27", "K27");
                    rowHeight103.EntireRow.RowHeight = 8;

                    Excel.Range rowColum104 = workSheet2.get_Range("A28", "A36");
                    rowColum104.EntireColumn.ColumnWidth = 5;

                    Excel.Range rowColum105 = workSheet2.get_Range("B28", "B36");
                    rowColum105.EntireColumn.ColumnWidth = 10;

                    Excel.Range rowHeight106 = workSheet2.get_Range("A38", "K38");
                    rowHeight106.EntireRow.RowHeight = 55;

                    Excel.Range rowHeight107 = workSheet2.get_Range("A37", "K37");
                    rowHeight107.EntireRow.RowHeight = 6;

                    Excel.Range rowColum108 = workSheet2.get_Range("D38", "E38");
                    rowColum108.EntireColumn.ColumnWidth = 6;

                    Excel.Range rowColum109 = workSheet2.get_Range("F38");
                    rowColum109.EntireColumn.ColumnWidth = 14;

                    Excel.Range rowColum110 = workSheet2.get_Range("B38", "C38");
                    rowColum110.EntireColumn.ColumnWidth = 9;

                    Excel.Range rowColum111 = workSheet2.get_Range("H38");
                    rowColum111.EntireColumn.ColumnWidth = 10;

                    Excel.Range rowColum112 = workSheet2.get_Range("I38");
                    rowColum112.EntireColumn.ColumnWidth = 10;

                    Excel.Range rowColum113 = workSheet2.get_Range("K38");
                    rowColum113.EntireColumn.ColumnWidth = 10;

                    Excel.Range rowHeight114 = workSheet2.get_Range("A39", "K39");
                    rowHeight114.EntireRow.RowHeight = 6;

                    Excel.Range rowHeight115 = workSheet2.get_Range("A40", "A46");
                    rowHeight115.EntireRow.RowHeight = 35;

                    Excel.Range rowHeight116 = workSheet2.get_Range("A28", "A36");
                    rowHeight116.EntireRow.RowHeight = 8;

                    Excel.Range rowHeight117 = workSheet2.get_Range("A47", "K47");
                    rowHeight117.EntireRow.RowHeight = 6;

                    Excel.Range rowHeight118 = workSheet2.get_Range("A50", "K50");
                    rowHeight118.EntireRow.RowHeight = 10;

                    Excel.Range rowHeight119 = workSheet2.get_Range("A49", "K49");
                    rowHeight119.EntireRow.RowHeight = 20;

                    Excel.Range rowHeight120 = workSheet2.get_Range("A53", "K53");
                    rowHeight120.EntireRow.RowHeight = 10;

                    Excel.Range rowHeight121 = workSheet2.get_Range("A56", "K56");
                    rowHeight121.EntireRow.RowHeight = 10;

                    Excel.Range rowHeight122 = workSheet2.get_Range("A59", "K59");
                    rowHeight122.EntireRow.RowHeight = 10;

                    Excel.Range rowHeight123 = workSheet2.get_Range("A63", "K63");
                    rowHeight123.EntireRow.RowHeight = 10;


                    Excel.Range range_Consolidated100 = workSheet2.Rows.get_Range("F1", "K2");
                    Excel.Range range_Consolidated101 = workSheet2.Rows.get_Range("A7", "I7");
                    Excel.Range range_Consolidated102 = workSheet2.Rows.get_Range("A9", "I9");
                    Excel.Range range_Consolidated103 = workSheet2.Rows.get_Range("J11", "K12");
                    Excel.Range range_Consolidated104 = workSheet2.Rows.get_Range("J13", "K13");
                    Excel.Range range_Consolidated105 = workSheet2.Rows.get_Range("J14", "K14");
                    Excel.Range range_Consolidated106 = workSheet2.Rows.get_Range("I15", "K15");
                    Excel.Range range_Consolidated107 = workSheet2.Rows.get_Range("D17", "H17");
                    Excel.Range range_Consolidated108 = workSheet2.Rows.get_Range("D19", "H19");
                    Excel.Range range_Consolidated109 = workSheet2.Rows.get_Range("H22", "K22");
                    Excel.Range range_Consolidated110 = workSheet2.Rows.get_Range("C22", "E22");
                    Excel.Range range_Consolidated111 = workSheet2.Rows.get_Range("A26", "H36");
                    Excel.Range range_Consolidated112 = workSheet2.Rows.get_Range("I26", "K27");
                    Excel.Range range_Consolidated113 = workSheet2.Rows.get_Range("A26", "K36");
                    Excel.Range range_Consolidated114 = workSheet2.Rows.get_Range("A38", "K38");
                    Excel.Range range_Consolidated115 = workSheet2.Rows.get_Range("A40", "K46");
                    Excel.Range range_Consolidated116 = workSheet2.Rows.get_Range("A50", "K50");
                    Excel.Range range_Consolidated117 = workSheet2.Rows.get_Range("A49", "K49");
                    Excel.Range range_Consolidated118 = workSheet2.Rows.get_Range("A52", "K52");
                    Excel.Range range_Consolidated119 = workSheet2.Rows.get_Range("A53", "K53");
                    Excel.Range range_Consolidated120 = workSheet2.Rows.get_Range("A55", "K55");
                    Excel.Range range_Consolidated121 = workSheet2.Rows.get_Range("A56", "K56");
                    Excel.Range range_Consolidated122 = workSheet2.Rows.get_Range("A58", "K58");
                    Excel.Range range_Consolidated123 = workSheet2.Rows.get_Range("A59", "K59");
                    Excel.Range range_Consolidated124 = workSheet2.Rows.get_Range("A62", "E62");
                    Excel.Range range_Consolidated125 = workSheet2.Rows.get_Range("A63", "K63");
                    Excel.Range range_Consolidated126 = workSheet2.Rows.get_Range("I62", "J62");

                    range_Consolidated100.Font.Size = 9;
                    range_Consolidated101.Font.Bold = true;
                    range_Consolidated102.Font.Bold = true;
                    range_Consolidated103.Font.Size = 9;
                    range_Consolidated103.Font.Bold = true;
                    range_Consolidated104.Font.Size = 9;
                    range_Consolidated105.Font.Bold = true;
                    range_Consolidated105.Font.Size = 9;
                    range_Consolidated106.Font.Size = 9;
                    range_Consolidated107.Font.Size = 12;
                    range_Consolidated107.Font.Bold = true;
                    range_Consolidated108.Font.Bold = true;
                    range_Consolidated109.NumberFormat = "@";
                    range_Consolidated110.NumberFormat = "@";
                    range_Consolidated111.Font.Italic = true;
                    range_Consolidated111.Font.Size = 7;
                    range_Consolidated112.Font.Size = 8;
                    range_Consolidated112.Font.Bold = true;
                    range_Consolidated112.Font.Italic = true;
                    range_Consolidated113.Font.Italic = true;
                    range_Consolidated114.Font.Bold = true;
                    range_Consolidated115.Font.Size = 7;
                    range_Consolidated116.Font.Size = 7;
                    range_Consolidated117.Font.Bold = true;
                    range_Consolidated118.Font.Bold = true;
                    range_Consolidated119.Font.Size = 7;
                    range_Consolidated120.Font.Bold = true;
                    range_Consolidated121.Font.Size = 7;
                    range_Consolidated122.Font.Bold = true;
                    range_Consolidated123.Font.Size = 7;
                    range_Consolidated124.Font.Bold = true;
                    range_Consolidated124.Font.Underline = true;
                    range_Consolidated125.Font.Size = 7;
                    range_Consolidated126.Font.Bold = true;

                    workSheet2.Cells[1, 7] = $"Специализированная форма № ФОУ-18";
                    workSheet2.Cells[2, 6] = $"Утверждена распоряжением ОАО «РЖД» от 29.01.2015 № 190р";
                    workSheet2.Cells[4, 11] = $"Код";
                    workSheet2.Cells[5, 9] = $"Форма по ОКУД";
                    workSheet2.Cells[5, 11] = $"0306831";
                    workSheet2.Cells[6, 10] = $"по ОКПО";
                    workSheet2.Cells[6, 11] = $"{OKPO_remont}";
                    workSheet2.Cells[7, 1] = $"ОАО \"Российские железные дороги\"";
                    workSheet2.Cells[8, 1] = $"организация";
                    workSheet2.Cells[8, 10] = $"БЕ";
                    workSheet2.Cells[8, 11] = $"{BE_remont}";
                    workSheet2.Cells[9, 1] = $"\n{Full_name_company}\n";
                    workSheet2.Cells[10, 1] = $"структурное подразделение";
                    workSheet2.Cells[11, 10] = $"Начальник";
                    workSheet2.Cells[12, 8] = $"УТВЕРЖДАЮ:";
                    workSheet2.Cells[13, 10] = $"(должность)";
                    workSheet2.Cells[14, 10] = $"{director_FIO_remont_company}";
                    workSheet2.Cells[15, 9] = $"(подпись)";
                    workSheet2.Cells[15, 10] = $"(расшифровка подписи)";
                    workSheet2.Cells[17, 4] = $"ДЕФЕКТНАЯ ВЕДОМОСТЬ";
                    workSheet2.Cells[18, 4] = $"Номер документа";
                    workSheet2.Cells[18, 7] = $"Дата составления";
                    workSheet2.Cells[19, 4] = $"{numberActRemont}";
                    workSheet2.Cells[19, 7] = $"{dateTO.Remove(dateTO.IndexOf(" "))}";
                    workSheet2.Cells[21, 1] = $"Основное средство (здание, оборудование):";
                    workSheet2.Cells[21, 5] = $"{model}";
                    workSheet2.Cells[21, 8] = $"Заводской № {serialNumber}";
                    workSheet2.Cells[22, 1] = $"Инвентарный номер:";
                    workSheet2.Cells[22, 3] = $"{inventoryNumber}";
                    workSheet2.Cells[22, 6] = $"Сетевой номер:";
                    workSheet2.Cells[22, 8] = $"{networkNumber}";
                    workSheet2.Cells[23, 1] = $"Местонахождение объекта:";
                    workSheet2.Cells[23, 4] = $"{city}";
                    workSheet2.Cells[24, 1] = $"Комиссия в составе:";
                    workSheet2.Cells[24, 3] = $"{chairman_post_remont_company} {chairman_FIO_remont_company}," +
                        $" {txb_1_post_remont_company} {txb_1_FIO_remont_company}";
                    workSheet2.Cells[25, 3] = $"{txb_2_post_remont_company} {txb_2_FIO_remont_company}, " +
                        $"{txb_3_post_remont_company} {txb_3_FIO_remont_company}";
                    workSheet2.Cells[26, 1] = $"произвела осмотр объектов (указать наименование) и отметила следующее:";
                    workSheet2.Cells[26, 9] = $"(заполняется при капитальном";
                    workSheet2.Cells[27, 9] = $"ремонте зданий и сооружений)";
                    workSheet2.Cells[28, 1] = $"I:";
                    workSheet2.Cells[28, 2] = $"Общие сведения по объекту:";
                    workSheet2.Cells[29, 2] = $"Год постройки:";
                    workSheet2.Cells[30, 2] = $"Этажность, общая высота, площать, протяженность и др.:";
                    workSheet2.Cells[31, 1] = $"II:";
                    workSheet2.Cells[31, 2] = $"Подробное описание конструкций (с указанием материала) и технического состояния";
                    workSheet2.Cells[32, 2] = $"объекта (основания, фундаменты, стены, колонны, перекрытия и др.):";
                    workSheet2.Cells[35, 1] = $"III:";
                    workSheet2.Cells[35, 2] = $"Выводы и предложения по проведению ремонта с перечислением состава работ:";
                    workSheet2.Cells[38, 1] = $"№\nп/п";
                    workSheet2.Cells[38, 2] = $"\nНаименование изделия, узла, агрегата, конструкции, подлежащего ремонту\n";
                    workSheet2.Cells[38, 4] = $"\nНаименование деталей, элементов\n";
                    workSheet2.Cells[38, 6] = $"\nНаименование работ по устранению дефектов\n";
                    workSheet2.Cells[38, 7] = $"\nФормула подсчета\n";
                    workSheet2.Cells[38, 8] = $"\nЕдиница измерения\n";
                    workSheet2.Cells[38, 9] = $"\nКоличество, объем\n";
                    workSheet2.Cells[38, 10] = $"\nДефект (степень износа)\n";
                    workSheet2.Cells[38, 11] = $"\nПримечание\n";
                    workSheet2.Cells[40, 1] = $"1";
                    workSheet2.Cells[41, 1] = $"2";
                    workSheet2.Cells[42, 1] = $"3";
                    workSheet2.Cells[43, 1] = $"4";
                    workSheet2.Cells[44, 1] = $"5";
                    workSheet2.Cells[45, 1] = $"6";
                    workSheet2.Cells[46, 1] = $"6";
                    workSheet2.Cells[46, 1] = $"7";
                    workSheet2.Cells[40, 2] = $"{model}";
                    workSheet2.Cells[40, 4] = $"\n{parts_1}\n";
                    workSheet2.Cells[40, 6] = $"\n{сompleted_works_1}\n";
                    if (parts_1 != "" || сompleted_works_1 != "")
                    {
                        workSheet2.Cells[40, 8] = $"шт.";
                        workSheet2.Cells[40, 9] = $"1";
                        workSheet2.Cells[40, 10] = $"100%";
                    }
                    workSheet2.Cells[41, 4] = $"\n{parts_2}\n";
                    workSheet2.Cells[41, 6] = $"\n{сompleted_works_2}\n";
                    if (parts_2 != "" || сompleted_works_2 != "")
                    {
                        workSheet2.Cells[41, 8] = $"шт.";
                        workSheet2.Cells[41, 9] = $"1";
                        workSheet2.Cells[41, 10] = $"100%";
                    }
                    workSheet2.Cells[42, 4] = $"\n{parts_3}\n";
                    workSheet2.Cells[42, 6] = $"\n{сompleted_works_3}\n";
                    if (parts_3 != "" || сompleted_works_3 != "")
                    {
                        workSheet2.Cells[42, 8] = $"шт.";
                        workSheet2.Cells[42, 9] = $"1";
                        workSheet2.Cells[42, 10] = $"100%";
                    }
                    workSheet2.Cells[43, 4] = $"\n{parts_4}\n";
                    workSheet2.Cells[43, 6] = $"\n{сompleted_works_4}\n";
                    if (parts_4 != "" || сompleted_works_4 != "")
                    {
                        workSheet2.Cells[43, 8] = $"шт.";
                        workSheet2.Cells[43, 9] = $"1";
                        workSheet2.Cells[43, 10] = $"100%";
                    }
                    workSheet2.Cells[44, 4] = $"\n{parts_5}\n";
                    workSheet2.Cells[44, 6] = $"\n{сompleted_works_5}\n";
                    if (parts_5 != "" || сompleted_works_5 != "")
                    {
                        workSheet2.Cells[44, 8] = $"шт.";
                        workSheet2.Cells[44, 9] = $"1";
                        workSheet2.Cells[44, 10] = $"100%";
                    }
                    workSheet2.Cells[45, 4] = $"\n{parts_6}\n";
                    workSheet2.Cells[45, 6] = $"\n{сompleted_works_6}\n";
                    if (parts_6 != "" || сompleted_works_6 != "")
                    {
                        workSheet2.Cells[45, 8] = $"шт.";
                        workSheet2.Cells[45, 9] = $"1";
                        workSheet2.Cells[45, 10] = $"100%";
                    }
                    workSheet2.Cells[46, 4] = $"\n{parts_7}\n";
                    workSheet2.Cells[46, 6] = $"\n{сompleted_works_7}\n";
                    if (parts_7 != "" || сompleted_works_7 != "")
                    {
                        workSheet2.Cells[46, 8] = $"шт.";
                        workSheet2.Cells[46, 9] = $"1";
                        workSheet2.Cells[46, 10] = $"100%";
                    }
                    workSheet2.Cells[48, 1] = $"Комиссия:";
                    workSheet2.Cells[49, 2] = $"{chairman_post_remont_company}";
                    workSheet2.Cells[49, 9] = $"{chairman_FIO_remont_company}";
                    workSheet2.Cells[50, 2] = $"(должность)";
                    workSheet2.Cells[50, 6] = $"(подпись)";
                    workSheet2.Cells[50, 9] = $"(расшифровка подписи)";
                    workSheet2.Cells[52, 2] = $"{txb_1_post_remont_company}";
                    workSheet2.Cells[52, 9] = $"{txb_1_FIO_remont_company}";
                    workSheet2.Cells[53, 2] = $"(должность)";
                    workSheet2.Cells[53, 6] = $"(подпись)";
                    workSheet2.Cells[53, 9] = $"(расшифровка подписи)";
                    workSheet2.Cells[55, 2] = $"{txb_2_post_remont_company}";
                    workSheet2.Cells[55, 9] = $"{txb_2_FIO_remont_company}";
                    workSheet2.Cells[56, 2] = $"(должность)";
                    workSheet2.Cells[56, 6] = $"(подпись)";
                    workSheet2.Cells[56, 9] = $"(расшифровка подписи)";
                    workSheet2.Cells[58, 2] = $"{txb_3_post_remont_company}";
                    workSheet2.Cells[58, 9] = $"{txb_3_FIO_remont_company}";
                    workSheet2.Cells[59, 2] = $"(должность)";
                    workSheet2.Cells[59, 6] = $"(подпись)";
                    workSheet2.Cells[59, 9] = $"(расшифровка подписи)";
                    workSheet2.Cells[60, 1] = $"Исполнитель:";
                    workSheet2.Cells[62, 1] = $"Начальник участка по ТО и ремонту СРС";
                    workSheet2.Cells[62, 9] = $"{FIO_chief}";
                    workSheet2.Cells[63, 2] = $"(должность)";
                    workSheet2.Cells[63, 6] = $"(подпись)";
                    workSheet2.Cells[63, 9] = $"(расшифровка подписи)";

                    #endregion

                    var file = $"{numberActRemont.Replace('/', '.')}-{company}_Акт.xlsx";

                    if (!File.Exists($@"C:\Documents_ServiceTelekom\Акты_ремонта\{city}\{company}\"))
                    {
                        try
                        {
                            Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\Акты_ремонта\{city}\{company}\");

                            workSheet.SaveAs($@"C:\Documents_ServiceTelekom\Акты_ремонта\{city}\{company}\" + file);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            MessageBox.Show("Не удаётся сохранить файл.");
                        }
                    }
                    else
                    {
                        try
                        {
                            workSheet.SaveAs($@"C:\Documents_ServiceTelekom\Акты_ремонта\{city}\{company}\" + file);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            MessageBox.Show("Не удаётся сохранить файл.");
                        }

                    }

                    exApp.Visible = true;
                }

                dgw.Enabled = true;
            }
            catch (Exception ex)
            {
                if (exApp != null)
                {
                    exApp = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();

                Environment.Exit(0);
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
