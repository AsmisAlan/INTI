using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.UserControls
{
    /// <summary>
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl, INotifyPropertyChanged
    {
        public string SearchTerm
        {
            get
            {
                return (string)GetValue(SearchTermProperty);
            }
            set
            {
                SetValue(SearchTermProperty, value);

                OnPropertyChanged(nameof(SearchTerm));
            }
        }

        public static readonly DependencyProperty SearchTermProperty =
             DependencyProperty.Register("SearchTerm",
                                         typeof(string),
                                         typeof(SearchBar),
                                         new PropertyMetadata(""));

        public SearchType SearchType
        {
            get
            {
                return (SearchType)GetValue(SearchTypeProperty);
            }
            set
            {
                SetValue(SearchTypeProperty, value);

                OnPropertyChanged(nameof(SearchType));
            }
        }

        public static readonly DependencyProperty SearchTypeProperty =
             DependencyProperty.Register("SearchType",
                                         typeof(SearchType),
                                         typeof(SearchBar),
                                         new PropertyMetadata(SearchType.Trigger));

        public static readonly RoutedEvent OnSearchTermChangedEvent = EventManager.RegisterRoutedEvent(
            "OnSearchTermChanged",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(SearchBar));

        public event RoutedEventHandler OnSearchTermChanged
        {
            add { AddHandler(OnSearchTermChangedEvent, value); }
            remove { RemoveHandler(OnSearchTermChangedEvent, value); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SearchBar()
        {
            InitializeComponent();

            DataContext = this;

            if (SearchType == SearchType.QuickSearch)
            {
                SearchButton.Visibility = Visibility.Hidden;
            }
        }

        private void RaiseSearchTermChangedEvent()
        {
            var newEventArgs = new OnSearchTermChangedEventArgs(OnSearchTermChangedEvent);

            newEventArgs.SearchTerm = SearchTerm;

            RaiseEvent(newEventArgs);
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchType == SearchType.Trigger)
            {
                RaiseSearchTermChangedEvent();
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RaiseSearchTermChangedEvent();
        }
    }
}