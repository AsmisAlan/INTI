using GeoUsersUI.Models.ViewModels.Forms;
using System.Windows;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;

namespace GeoUsersUI.Windows.Usuario
{
    /// <summary>
    /// Interaction logic for UsuarioCreationEditionForm.xaml
    /// </summary>
    public partial class UsuarioCreationEditionForm : Window
    {
        public UsuarioCreationEditionForm()
        {
            InitializeComponent();

            DataContext = App.Container.Resolve<UsuarioCreationEditionViewModel>();

            var initialized = Initialize();
        }

        public async Task<bool> Initialize()
        {
            await ((UsuarioCreationEditionViewModel)DataContext).LoadData();

            ComboLocalidad.ItemsSource = ((UsuarioCreationEditionViewModel)DataContext).Localidades;
            ComboOrganizacion.ItemsSource = ((UsuarioCreationEditionViewModel)DataContext).Organizaciones;
            ComboRubro.ItemsSource = ((UsuarioCreationEditionViewModel)DataContext).Rubros;

            return true;
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            ((UsuarioCreationEditionViewModel)DataContext).Create();

            Close();
        }

        private void Dismiss(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
