using GeoUsersUI.Models.ViewModels;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        private async Task LogIn()
        {
            var loginResult = await ViewModel.LogIn(passwordBox.Password);

            if (loginResult)
            {
                DialogResult = true;

                Close();
            }
        }

        private async void LoginGuestButton_Click(object sender, RoutedEventArgs e)
        {
            await LogIn();
        }

        private void LoginAuthenticatedButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }

        private async void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await LogIn();
            }
        }
    }
}
