using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.UserControls
{
    /// <summary>
    /// Interaction logic for DataGridExportButtonBar.xaml
    /// </summary>
    public partial class DataGridExportButtonBar : UserControl
    {
        // Create event.
        public static readonly RoutedEvent OnPrintButtonClickEvent = EventManager.RegisterRoutedEvent(
            "OnPrintButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(DataGridExportButtonBar));

        public event RoutedEventHandler OnPrintButtonClick
        {
            add { AddHandler(OnPrintButtonClickEvent, value); }
            remove { RemoveHandler(OnPrintButtonClickEvent, value); }
        }

        // Edit event.
        public static readonly RoutedEvent OnExportButtonClickEvent = EventManager.RegisterRoutedEvent(
            "OnExportButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(DataGridExportButtonBar));

        public event RoutedEventHandler OnExportButtonClick
        {
            add { AddHandler(OnExportButtonClickEvent, value); }
            remove { RemoveHandler(OnExportButtonClickEvent, value); }
        }

        public DataGridExportButtonBar()
        {
            InitializeComponent();
        }

        void RaisePrintEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(OnPrintButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        void RaiseExportEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(OnExportButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseExportEvent();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            RaisePrintEvent();
        }
    }
}
