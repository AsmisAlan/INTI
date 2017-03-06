using GeoUsersUI.Models.ViewModels.Forms;
using System.Windows;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for UsuarioCreationEditionForm.xaml
    /// </summary>
    public partial class OrganizacionCreationEditionForm : Window
    {
        private OrganizacionViewModel ViewModel { get; set; }

        public OrganizacionCreationEditionForm(int? organizacionId = null)
        {
            InitializeComponent();

            DataContext = ViewModel = App.Container.Resolve<OrganizacionViewModel>();

            var initialized = Initialize(organizacionId);
        }

        public async Task<bool> Initialize(int? organizacionId = null)
        {
            await ViewModel.Initialize(organizacionId);

            return true;
        }

        public OrganizacionHeaderData GetResult()
        {
            var localidad = ((IdAndValue)ComboLocalidad.SelectedItem).Value;

            ViewModel.Result.Direccion = $"{ViewModel.Result.Direccion} {localidad}";

            return ViewModel.Result;
        }

        private async void Submit(object sender, RoutedEventArgs e)
        {
            DialogResult = await ViewModel.Submit();

            if (DialogResult.HasValue && DialogResult.Value)
            {
                Close();
            }
        }

        private void Dismiss(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
