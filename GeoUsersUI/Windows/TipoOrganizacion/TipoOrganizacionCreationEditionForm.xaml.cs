using GeoUsersUI.Models.ViewModels;
using System.Windows;
using Microsoft.Practices.Unity;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for OrganizacionCreationEditionForm.xaml
    /// </summary>
    public partial class TipoOrganizacionCreationEditionForm : Window
    {
        private TipoOrganizacionEditionViewModel ViewModel { get; set; }

        public TipoOrganizacionCreationEditionForm(int? tipoOrganizacionId = null)
        {
            InitializeComponent();

            DataContext = ViewModel = App.Container.Resolve<TipoOrganizacionEditionViewModel>();

            Initialize(tipoOrganizacionId);
        }

        public bool GetResult()
        {
            return ViewModel.Result;
        }

        private async void Initialize(int? tipoOrganizacionId)
        {
            await ViewModel.Initialize(tipoOrganizacionId);
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
