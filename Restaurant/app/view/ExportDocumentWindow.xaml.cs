using Restaurant.app.model;
using Restaurant.app.view_model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;

namespace Restaurant.app.view
{
    /// <summary>
    /// Логика взаимодействия для ExportDocumentWindow.xaml
    /// </summary>
    public partial class ExportDocumentWindow : Excel.Window
    {
        public DataGrid DocumentGrid { get; set; }
        public ExportDocumentWindow(DataGrid dataGrid)
        {
            InitializeComponent();
            DocumentGrid = dataGrid;
            //DataContext = new ExportDocumentViewModel();

            ExportSuppliesToExcelButton_Click(new object(), new RoutedEventArgs());
            Close();
        }

        private bool ValidateNameFile()
        {
            if (string.IsNullOrEmpty(NameFileTextBox.Text.Trim()))
            {
                return true;
            }

            return false;
        }

        private void ExportSuppliesToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            //if (ValidateNameFile())
            //{
            //    MessageBox.Show("Введите название файла", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return;
            //}

            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < DocumentGrid.Columns.Count; j++) 
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true; 
                sheet1.Columns[j + 1].ColumnWidth = 15; 
                myRange.Value2 = DocumentGrid.Columns[j].Header;
            }
            for (int i = 0; i < DocumentGrid.Columns.Count; i++)
            { 
                for (int j = 0; j < DocumentGrid.Items.Count; j++)
                {
                    TextBlock b = DocumentGrid.Columns[i].GetCellContent(DocumentGrid.Items[j]) as TextBlock;
                    Range myRange = (Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }

        dynamic Excel.Window.Activate()
        {
            throw new NotImplementedException();
        }

        dynamic Excel.Window.ActivateNext()
        {
            throw new NotImplementedException();
        }

        dynamic Excel.Window.ActivatePrevious()
        {
            throw new NotImplementedException();
        }

        bool Excel.Window.Close(object SaveChanges, object Filename, object RouteWorkbook)
        {
            throw new NotImplementedException();
        }

        dynamic Excel.Window.LargeScroll(object Down, object Up, object ToRight, object ToLeft)
        {
            throw new NotImplementedException();
        }

        Excel.Window Excel.Window.NewWindow()
        {
            throw new NotImplementedException();
        }

        dynamic Excel.Window._PrintOut(object From, object To, object Copies, object Preview, object ActivePrinter, object PrintToFile, object Collate, object PrToFileName)
        {
            throw new NotImplementedException();
        }

        dynamic Excel.Window.PrintPreview(object EnableChanges)
        {
            throw new NotImplementedException();
        }

        dynamic Excel.Window.ScrollWorkbookTabs(object Sheets, object Position)
        {
            throw new NotImplementedException();
        }

        dynamic Excel.Window.SmallScroll(object Down, object Up, object ToRight, object ToLeft)
        {
            throw new NotImplementedException();
        }

        int Excel.Window.PointsToScreenPixelsX(int Points)
        {
            throw new NotImplementedException();
        }

        int Excel.Window.PointsToScreenPixelsY(int Points)
        {
            throw new NotImplementedException();
        }

        dynamic Excel.Window.RangeFromPoint(int x, int y)
        {
            throw new NotImplementedException();
        }

        void Excel.Window.ScrollIntoView(int Left, int Top, int Width, int Height, object Start)
        {
            throw new NotImplementedException();
        }

        dynamic Excel.Window.PrintOut(object From, object To, object Copies, object Preview, object ActivePrinter, object PrintToFile, object Collate, object PrToFileName)
        {
            throw new NotImplementedException();
        }

        Excel.Application Excel.Window.Application => throw new NotImplementedException();

        XlCreator Excel.Window.Creator => throw new NotImplementedException();

        dynamic Excel.Window.Parent => throw new NotImplementedException();

        Excel.Range Excel.Window.ActiveCell => throw new NotImplementedException();

        Chart Excel.Window.ActiveChart => throw new NotImplementedException();

        Pane Excel.Window.ActivePane => throw new NotImplementedException();

        dynamic Excel.Window.ActiveSheet => throw new NotImplementedException();

        dynamic Excel.Window.Caption { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.DisplayFormulas { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.DisplayGridlines { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.DisplayHeadings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.DisplayHorizontalScrollBar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.DisplayOutline { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window._DisplayRightToLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.DisplayVerticalScrollBar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.DisplayWorkbookTabs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.DisplayZeros { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.EnableResize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.FreezePanes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int Excel.Window.GridlineColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        XlColorIndex Excel.Window.GridlineColorIndex { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double Excel.Window.Height { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        int Excel.Window.Index => throw new NotImplementedException();

        double Excel.Window.Left { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string Excel.Window.OnWindow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Panes Excel.Window.Panes => throw new NotImplementedException();

        Excel.Range Excel.Window.RangeSelection => throw new NotImplementedException();

        int Excel.Window.ScrollColumn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int Excel.Window.ScrollRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Sheets Excel.Window.SelectedSheets => throw new NotImplementedException();

        dynamic Excel.Window.Selection => throw new NotImplementedException();

        bool Excel.Window.Split { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int Excel.Window.SplitColumn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double Excel.Window.SplitHorizontal { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int Excel.Window.SplitRow { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double Excel.Window.SplitVertical { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double Excel.Window.TabRatio { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        double Excel.Window.Top { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        XlWindowType Excel.Window.Type => throw new NotImplementedException();

        double Excel.Window.UsableHeight => throw new NotImplementedException();

        double Excel.Window.UsableWidth => throw new NotImplementedException();

        bool Excel.Window.Visible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Excel.Range Excel.Window.VisibleRange => throw new NotImplementedException();

        double Excel.Window.Width { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        int Excel.Window.WindowNumber => throw new NotImplementedException();

        XlWindowState Excel.Window.WindowState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        dynamic Excel.Window.Zoom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        XlWindowView Excel.Window.View { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.DisplayRightToLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        SheetViews Excel.Window.SheetViews => throw new NotImplementedException();

        dynamic Excel.Window.ActiveSheetView => throw new NotImplementedException();

        bool Excel.Window.DisplayRuler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.AutoFilterDateGrouping { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        bool Excel.Window.DisplayWhitespace { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        int Excel.Window.Hwnd => throw new NotImplementedException();



        //private void ExportToExcel(List<ExportPassenger> myExportList, string xlsxFilePath)
        //{
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        var ws = wb.Worksheets.Add("PassengerData");

        //        var headers = new[]
        //            { "ID", "Имя", "Фамилия", "Отчество", "Серия паспорта", "Номер паспорта", "Дата выдачи", "Кем выдан" };
        //        for (int col = 0; col < headers.Length; col++)
        //        {
        //            ws.Cell(1, col + 1).Value = headers[col];
        //        }

        //        for (int row = 0; row < myExportList.Count; row++)
        //        {
        //            var passenger = myExportList[row];

        //            ws.Cell(row + 2, 1).Value = passenger.Id;
        //            ws.Cell(row + 2, 2).Value = passenger.FirstName;
        //            ws.Cell(row + 2, 3).Value = passenger.LastName;
        //            ws.Cell(row + 2, 4).Value = passenger.Patronymic;
        //            ws.Cell(row + 2, 5).Value = passenger.PassportSeries;
        //            ws.Cell(row + 2, 6).Value = passenger.PassportId;
        //            ws.Cell(row + 2, 7).Value = passenger.DateOfIssue;
        //            ws.Cell(row + 2, 8).Value = passenger.IssuedBy;
        //        }

        //        wb.SaveAs(xlsxFilePath);
        //    }
        //}

        //private void ExportToWord(List<ExportPassenger> myExportList, string docxFilePath)
        //{
        //    using (DocX doc = DocX.Create(docxFilePath))
        //    {
        //        int rowCount = myExportList.Count + 1;
        //        int columnCount = 8;

        //        Table table = doc.AddTable(rowCount, columnCount);
        //        table.Design = TableDesign.None;

        //        for (int col = 0; col < columnCount; col++)
        //        {
        //            table.Rows[0].Cells[col].Paragraphs.First().Append(GetColumnName(col));
        //        }

        //        for (int row = 1; row < rowCount; row++)
        //        {
        //            ExportPassenger passenger = myExportList[row - 1];

        //            table.Rows[row].Cells[0].Paragraphs.First().Append(passenger.Id);
        //            table.Rows[row].Cells[1].Paragraphs.First().Append(passenger.FirstName);
        //            table.Rows[row].Cells[2].Paragraphs.First().Append(passenger.LastName);
        //            table.Rows[row].Cells[3].Paragraphs.First().Append(passenger.Patronymic);
        //            table.Rows[row].Cells[4].Paragraphs.First().Append(passenger.PassportSeries);
        //            table.Rows[row].Cells[5].Paragraphs.First().Append(passenger.PassportId);
        //            table.Rows[row].Cells[6].Paragraphs.First().Append(passenger.DateOfIssue);
        //            table.Rows[row].Cells[7].Paragraphs.First().Append(passenger.IssuedBy);
        //        }

        //        doc.InsertTable(table);
        //        doc.Save();
        //    }
        //}
    }
}
