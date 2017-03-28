using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.UserControls
{
    /// <summary>
    /// Interaction logic for FormButtonBar.xaml
    /// </summary>
    public partial class FormButtonBar : UserControl, INotifyPropertyChanged
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
                                         typeof(FormButtonBar),
                                         new PropertyMetadata(false));

        public static readonly RoutedEvent OnCancelButtonClickEvent = EventManager.RegisterRoutedEvent(
            "OnCancelButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(FormButtonBar));

        public static readonly RoutedEvent OnSubmitButtonClickEvent = EventManager.RegisterRoutedEvent(
            "OnSubmitButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(FormButtonBar));

        public event RoutedEventHandler OnCancelButtonClick
        {
            add { AddHandler(OnCancelButtonClickEvent, value); }
            remove { RemoveHandler(OnCancelButtonClickEvent, value); }
        }

        public event RoutedEventHandler OnSubmitButtonClick
        {
            add { AddHandler(OnSubmitButtonClickEvent, value); }
            remove { RemoveHandler(OnSubmitButtonClickEvent, value); }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public FormButtonBar()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseSubmitEvent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseCancelEvent();
        }

        void RaiseCancelEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(OnCancelButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        void RaiseSubmitEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(OnSubmitButtonClickEvent);
            RaiseEvent(newEventArgs);
        }
    }
}
