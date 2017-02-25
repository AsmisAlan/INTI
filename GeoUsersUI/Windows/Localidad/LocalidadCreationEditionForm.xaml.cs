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
            DialogResult = await ViewModel.Submit();

            Close();
        }

        private void ButtonDismiss_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }
    }
}
