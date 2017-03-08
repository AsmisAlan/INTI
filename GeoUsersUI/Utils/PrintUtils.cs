using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.Utils
{
    public static class PrintUtils
    {
        public static void PrintDataGrid(DataGrid dataGrid, string title)
        {
            var printDialog = new PrintDialog();

            if (printDialog.ShowDialog().Value)
            {
                var pagesize = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

                dataGrid.Measure(pagesize);

                dataGrid.Arrange(new Rect(5, 5, pagesize.Width, pagesize.Height));

                printDialog.PrintVisual(dataGrid, title);
            }
        }
    }
}
