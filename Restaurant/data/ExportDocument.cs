using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Excel = Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;
using Word = Microsoft.Office.Interop.Word;
using Restaurant.app.model;
using System.Collections.ObjectModel;

namespace Restaurant.data;

public class ExportDocument 
{
    public DataGrid DocumentGrid { get; set; }
    public ExportDocument(DataGrid dataGrid)
    {
        DocumentGrid = dataGrid;
        ExportSuppliesToExcel();
    }
    public ExportDocument(Supply supply, ObservableCollection<SuppliesProducts> products)
    {
        ExportOrderToWord(supply, products);
    }

    private void ExportSuppliesToExcel()
    {
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

    private void ExportOrderToWord(Supply supply, ObservableCollection<SuppliesProducts> products)
    {
        string testText = "Экспорт в Word завершен с успехом!";

        Word.Application wordapp = new Word.Application();
        wordapp.Visible = false;
        Word.Document worddoc;
        object wordobj = System.Reflection.Missing.Value;
        worddoc = wordapp.Documents.Add(ref wordobj);

        worddoc.Content.Text = "Номер заказа: " + supply.SupplyID.ToString() + "\n" +
            "Дата: " + supply.SupplyDate.ToString() + "\n" +
            "Поставщик: " + supply.Supplier.SupplierName.ToString() + "\n" + "Стоимость: " + supply.PurchasePrice.ToString() + "\n" +
            "Продукты:\n";


        Word.Paragraph para = worddoc.Content.Paragraphs.Add();

        // Создаем таблицу после параграфа
        Word.Table table = worddoc.Tables.Add(para.Range, products.Count + 1, 3); //Range, кол-ко столбцов, кол-во строк
        table.Borders.Enable = 1; // Включение границ таблицы

        // Заполнение заголовков столбцов таблицы
        table.Cell(1, 1).Range.Text = "Название";
        table.Cell(1, 2).Range.Text = "Ед. измерения";
        table.Cell(1, 3).Range.Text = "Количество";

        for (int i = 0; i < products.Count; i++)
        {
            table.Cell(i + 2, 1).Range.Text = products[i].Product.ProductName.ToString();
            table.Cell(i + 2, 2).Range.Text = products[i].Product.UnitOfMeasure.UnitsOfMeasureName.ToString();
            table.Cell(i + 2, 3).Range.Text = products[i].DeliveredQuantity.ToString();
        }



        wordapp.Visible = true;

        wordapp = null;
    }
}

