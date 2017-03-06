using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.UserControls
{
    /// <summary>
    /// Interaction logic for EntityListButtonBar.xaml
    /// </summary>
    public partial class EntityListButtonBar : UserControl
    {
        // Create event.
        public static readonly RoutedEvent OnCreateButtonClickEvent = EventManager.RegisterRoutedEvent(
            "OnCreateButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(EntityListButtonBar));

        public event RoutedEventHandler OnCreateButtonClick
        {
            add { AddHandler(OnCreateButtonClickEvent, value); }
            remove { RemoveHandler(OnCreateButtonClickEvent, value); }
        }

        void RaiseCreateEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(OnCreateButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        // Edit event.
        public static readonly RoutedEvent OnEditButtonClickEvent = EventManager.RegisterRoutedEvent(
            "OnEditButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(EntityListButtonBar));

        public event RoutedEventHandler OnEditButtonClick
        {
            add { AddHandler(OnEditButtonClickEvent, value); }
            remove { RemoveHandler(OnEditButtonClickEvent, value); }
        }

        void RaiseEditEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(OnEditButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        // Delete event.
        public static readonly RoutedEvent OnDeleteButtonClickEvent = EventManager.RegisterRoutedEvent(
            "OnDeleteButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(EntityListButtonBar));

        public event RoutedEventHandler OnDeleteButtonClick
        {
            add { AddHandler(OnDeleteButtonClickEvent, value); }
            remove { RemoveHandler(OnDeleteButtonClickEvent, value); }
        }

        void RaiseDeleteEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(OnDeleteButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        // Close event.
        public static readonly RoutedEvent OnCloseButtonClickEvent = EventManager.RegisterRoutedEvent(
            "OnCloseButtonClick",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(EntityListButtonBar));

        public event RoutedEventHandler OnCloseButtonClick
        {
            add { AddHandler(OnCloseButtonClickEvent, value); }
            remove { RemoveHandler(OnCloseButtonClickEvent, value); }
        }

        void RaiseCloseEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(OnCloseButtonClickEvent);
            RaiseEvent(newEventArgs);
        }

        public EntityListButtonBar()
        {
            InitializeComponent();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEditEvent();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseDeleteEvent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseCloseEvent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseCreateEvent();
        }
    }
}
