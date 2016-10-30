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
    public partial class UsuarioCreationEditionForm : Window
    {
        private BaseUsuarioViewModel CastedDataContext { get; set; }

        public UsuarioCreationEditionForm()
        {
            InitializeComponent();

            DataContext = CastedDataContext = App.Container.Resolve<UsuarioCreationEditionViewModel>();

            var initialized = Initialize();
        }

        public async Task<bool> Initialize()
        {
            await ((BaseUsuarioViewModel)DataContext).LoadData();

            ComboLocalidad.ItemsSource = CastedDataContext.Localidades;
            ComboOrganizacion.ItemsSource = CastedDataContext.Organizaciones;
            ComboRubro.ItemsSource = CastedDataContext.Rubros;

            return true;
        }

        public UsuarioHeader GetResult()
        {
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
