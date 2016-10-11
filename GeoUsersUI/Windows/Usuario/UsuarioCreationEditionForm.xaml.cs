using GeoUsersUI.Models.ViewModels.Forms;
using System.Windows;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Windows.Usuario
{
    /// <summary>
    /// Interaction logic for UsuarioCreationEditionForm.xaml
    /// </summary>
    public partial class UsuarioCreationEditionForm : Window
    {
        public UsuarioHeader Result { get; set; }

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
            if (((UsuarioCreationEditionViewModel)DataContext).IsUsuarioValid)
            {
                ((UsuarioCreationEditionViewModel)DataContext).Create();

                Result = new UsuarioHeader()
                {
                    Id = ((UsuarioCreationEditionViewModel)DataContext).Usuario.Id,
                    Email = ((UsuarioCreationEditionViewModel)DataContext).Usuario.Email,
                    Nombre = ((UsuarioCreationEditionViewModel)DataContext).Usuario.Nombre,
                    Direccion = ((UsuarioCreationEditionViewModel)DataContext).Usuario.Direccion
                };

                DialogResult = true;
                Close();
            }
            else
            {
                var message = MessageBox.Show("Hay campos obligatorios incompletos.", "Error", MessageBoxButton.OK);
            }
        }

        private void Dismiss(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
