using System.Windows;

namespace GeoUsersUI.UserControls
{
    public class OnSearchTermChangedEventArgs : RoutedEventArgs
    {
        public OnSearchTermChangedEventArgs()
        {
        }

        public OnSearchTermChangedEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public OnSearchTermChangedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }

        public string SearchTerm { get; set; }
    }
}
