using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace GeoUsersUI.Utils
{
    public static class ExcelExportUtils
    {
        public static void ExportToExcel(DataGrid dataGrid, string fileName)
        {
            var i = 0;
            var columnIndex = 1;
            var rowIndex = 1;

            var rows = GetDataGridRows(dataGrid);

            var excelApp = new Application();

            var excelWorkBooks = excelApp.Workbooks;

            var excelBook = excelWorkBooks.Add(1);

            var excelSheet = (Worksheet)excelBook.ActiveSheet;

            try
            {
                for (i = 1; i <= dataGrid.Columns.Count; i++)
                {
                    excelSheet.Cells[1, i] = dataGrid.Columns[i - 1].Header.ToString();
                }

                foreach (var row in rows)
                {
                    foreach (var column in dataGrid.Columns)
                    {
                        if (column.GetCellContent(row) is TextBlock)
                        {
                            var cellContent = column.GetCellContent(row) as TextBlock;

                            excelSheet.Cells[rowIndex + 1, columnIndex] = cellContent.Text.Trim();

                            columnIndex++;
                        }
                    }

                    columnIndex = 1;
                    rowIndex++;
                }

                excelApp.Visible = false;

                excelBook.SaveAs(fileName,
                                 XlFileFormat.xlOpenXMLWorkbook,
                                 Missing.Value,
                                 Missing.Value,
                                 false,
                                 false,
                                 XlSaveAsAccessMode.xlNoChange,
                                 XlSaveConflictResolution.xlUserResolution,
                                 true,
                                 Missing.Value,
                                 Missing.Value,
                                 Missing.Value);

                excelBook.Close(false, fileName, Missing.Value);
            }
            finally
            {
                Marshal.FinalReleaseComObject(excelWorkBooks);
                Marshal.FinalReleaseComObject(excelSheet);
                Marshal.FinalReleaseComObject(excelBook);
                Marshal.FinalReleaseComObject(excelApp);
            }
        }

        private static IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource;

            if (null == itemsSource)
            {
                yield return null;
            }

            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;

                if (null != row)
                {
                    yield return row;
                }
            }
        }
    }
}
