using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.UserControls
{
    /// <summary>
    /// Interaction logic for FormButtonBar.xaml
    /// </summary>
    public partial class FormButtonBar : UserControl
    {
        // Submit event.
        public static readonly RoutedEvent OnSubmitButtonClickEvent = EventManager.RegisterRoutedEvent(
            "OnSubmitButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(FormButtonBar));

        public event RoutedEventHandler OnSubmitButtonClick
        {
            add { AddHandler(OnSubmitButtonClickEvent, value); }
            remove { RemoveHandler(OnSubmitButtonClickEvent, value); }
        }

        void RaiseSubmitEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(OnSubmitButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        // Cancel event.
        public static readonly RoutedEvent OnCancelButtonClickEvent = EventManager.RegisterRoutedEvent(
            "OnCancelButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(FormButtonBar));

        public event RoutedEventHandler OnCancelButtonClick
        {
            add { AddHandler(OnCancelButtonClickEvent, value); }
            remove { RemoveHandler(OnCancelButtonClickEvent, value); }
        }

        void RaiseCancelEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(OnCancelButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        public FormButtonBar()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RaiseSubmitEvent();
        }

        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            RaiseCancelEvent();
        }
    }
}
