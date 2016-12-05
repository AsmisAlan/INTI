using GeoUsersUI.Models.ViewModels.UserControls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.UserControls
{
    /// <summary>
    /// Interaction logic for CollapsibleMenu.xaml
    /// </summary>
    public partial class CollapsibleMenu : UserControl, INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<MenuButton> Buttons
        {
            get
            {
                return (ObservableCollection<MenuButton>)GetValue(ButtonsProperty);
            }
            set
            {
                SetValue(ButtonsProperty, value);

                CastedDataContext.Buttons = value;

                OnPropertyChanged(nameof(Buttons));
            }
        }

        // Using a DependencyProperty as the backing store for Buttons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsProperty =
             DependencyProperty.Register("Buttons",
                                         typeof(ObservableCollection<MenuButton>),
                                         typeof(CollapsibleMenu),
                                         new PropertyMetadata(new ObservableCollection<MenuButton>()));

        public string HeaderName
        {
            get
            {
                return (string)GetValue(HeaderNameProperty);
            }
            set
            {
                SetValue(HeaderNameProperty, value);

                CastedDataContext.HeaderName = value;

                OnPropertyChanged(nameof(HeaderName));
            }
        }

        public static readonly DependencyProperty HeaderNameProperty =
             DependencyProperty.Register("HeaderName",
                                         typeof(string),
                                         typeof(CollapsibleMenu),
                                         new PropertyMetadata("Header"));

        public bool IsMenuOpened
        {
            get
            {
                return (bool)GetValue(IsMenuOpenedProperty);
            }
            set
            {
                SetValue(IsMenuOpenedProperty, value);

                CastedDataContext.IsMenuOpened = value;

                OnPropertyChanged(nameof(IsMenuOpened));
            }
        }

        public static readonly DependencyProperty IsMenuOpenedProperty =
             DependencyProperty.Register("IsMenuOpened",
                                         typeof(bool),
                                         typeof(CollapsibleMenu),
                                         new PropertyMetadata(true));

        public event PropertyChangedEventHandler PropertyChanged;

        private CollapsibleMenuViewModel CastedDataContext { get; set; }

        public CollapsibleMenu()
        {
            InitializeComponent();

            DataContext = CastedDataContext = new CollapsibleMenuViewModel(HeaderName, Buttons, IsMenuOpened);
        }

        public void ToggleButtonsVisibility()
        {
            var visibility = CastedDataContext.IsMenuOpened ? Visibility.Collapsed : Visibility.Visible;

            CastedDataContext.IsMenuOpened = !CastedDataContext.IsMenuOpened;

            foreach (var button in CastedDataContext.Buttons)
            {
                button.ButtonVisibility = visibility;
            }
        }

        private void ButtonHeader_Click(object sender, RoutedEventArgs e)
        {
            ToggleButtonsVisibility();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((MenuButton)((Button)sender).DataContext).OnButtonClickAction();
        }
    }
}
