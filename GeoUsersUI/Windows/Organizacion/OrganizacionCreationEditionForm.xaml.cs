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
        private BaseOrganizacionViewModel CastedDataContext { get; set; }

        public OrganizacionCreationEditionForm()
        {
            InitializeComponent();

            DataContext = CastedDataContext = App.Container.Resolve<OrganizacionCreationEditionViewModel>();

            var initialized = Initialize();
        }

        public async Task<bool> Initialize()
        {
            await ((BaseOrganizacionViewModel)DataContext).LoadData();

            ComboLocalidad.ItemsSource = CastedDataContext.Localidades;
            ComboOrganizacion.ItemsSource = CastedDataContext.TipoOrganizaciones;
            ComboRubro.ItemsSource = CastedDataContext.Rubros;

            return true;
        }

        public OrganizacionHeader GetResult()
        {
            CastedDataContext.Result.Localidad = ((IdAndValue)ComboLocalidad.SelectedItem).Value;
            return CastedDataContext.Result;
        }

        private async void Submit(object sender, RoutedEventArgs e)
        {
            DialogResult = await CastedDataContext.Submit();

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
