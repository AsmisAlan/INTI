using GeoUsersUI.Models.ViewModels;
using Microsoft.Practices.Unity;
using System.Windows;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private LoginFormViewModel ViewModel { get; set; }

        public LoginForm()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();

            DataContext = ViewModel = App.Container.Resolve<LoginFormViewModel>();
        }

        private async void LoginGuestButton_Click(object sender, RoutedEventArgs e)
        {
            var loginResult = await ViewModel.LogIn(passwordBox.Password);

            if (loginResult)
            {
                DialogResult = true;

                Close();
            }
        }

        private void LoginAuthenticatedButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }
    }
}
