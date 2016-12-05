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
        private BaseTipoOrganizacionViewModel ViewModel { get; set; }

        public TipoOrganizacionCreationEditionForm()
        {
            DataContext = App.Container.Resolve<TipoOrganizacionCreationViewModel>();

            Initialize();
        }

        public TipoOrganizacionCreationEditionForm(long tipoOrganizacionId)
        {
        }

        private void Initialize()
        {
            InitializeComponent();

            ViewModel = ((BaseTipoOrganizacionViewModel)DataContext);
        }

        public bool GetResult()
        {
            return ViewModel.Result;
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
