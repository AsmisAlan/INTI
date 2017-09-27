using GeoUsersUI.Models.ViewModels.Forms;
using System.Windows;
using Microsoft.Practices.Unity;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for LocalidadCreationEditionForm.xaml
    /// </summary>
    public partial class LocalidadCreationEditionForm : Window
    {
        private LocalidadEditionViewModel ViewModel { get; set; }

        public LocalidadCreationEditionForm(int? localidadId = null)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DataContext = ViewModel = App.Container.Resolve<LocalidadEditionViewModel>();

            Initialize(localidadId);
        }

        public bool GetResult()
        {
            return ViewModel.Result;
        }

        private async void Initialize(int? localidadId)
        {
            await ViewModel.Initialize(localidadId);
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
