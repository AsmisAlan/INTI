using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.UserControls
{
    /// <summary>
    /// Interaction logic for DataGridExportButtonBar.xaml
    /// </summary>
    public partial class DataGridExportButtonBar : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool Loading
        {
            get
            {
                return (bool)GetValue(LoadingProperty);
            }
            set
            {
                SetValue(LoadingProperty, value);

                OnPropertyChanged(nameof(Loading));
            }
        }

        public static readonly DependencyProperty LoadingProperty =
             DependencyProperty.Register("Loading",
                                         typeof(bool),
                                         typeof(DataGridExportButtonBar),
                                         new PropertyMetadata(false));

        public static readonly RoutedEvent OnPrintButtonClickEvent = EventManager.RegisterRoutedEvent(
            "OnPrintButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(DataGridExportButtonBar));

        public static readonly RoutedEvent OnExportButtonClickEvent = EventManager.RegisterRoutedEvent(
            "OnExportButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(DataGridExportButtonBar));

        public event RoutedEventHandler OnPrintButtonClick
        {
            add { AddHandler(OnPrintButtonClickEvent, value); }
            remove { RemoveHandler(OnPrintButtonClickEvent, value); }
        }

        public event RoutedEventHandler OnExportButtonClick
        {
            add { AddHandler(OnExportButtonClickEvent, value); }
            remove { RemoveHandler(OnExportButtonClickEvent, value); }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public DataGridExportButtonBar()
        {
            InitializeComponent();

            DataContext = this;
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
