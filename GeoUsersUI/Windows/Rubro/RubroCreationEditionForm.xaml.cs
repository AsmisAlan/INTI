using GeoUsersUI.Models.ViewModels;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for RubroCreationEditionForm.xaml
    /// </summary>
    public partial class RubroCreationEditionForm : Window
    {
        private RubroEditionViewModel ViewModel { get; set; }

        public RubroCreationEditionForm(int? rubroId = null)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DataContext = ViewModel = App.Container.Resolve<RubroEditionViewModel>();

            var initialized = Initialize(rubroId);
        }

        private async Task<bool> Initialize(int? rubroId = null)
        {
            await ViewModel.Initialize(rubroId);

            return true;
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            var result = await ViewModel.Submit();

            if (result)
            {
                DialogResult = true;

                Close();
            }
        }

        private void ButtonDismiss_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }
    }
}
