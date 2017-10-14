using GeoUsersUI.Models.ViewModels.Forms;
using System.Windows;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using GeoUsers.Services.Model.DataTransfer;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System;
using System.Windows.Controls;
using GeoUsersUI.Utils;

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

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

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
            var hasValidCoordinates = true;

            if (!string.IsNullOrEmpty(ViewModel.Organizacion.Latitud))
            {
                hasValidCoordinates = ValidationUtils.ValidateCoordinates(ViewModel.Organizacion.Latitud);
            }

            if (hasValidCoordinates && !string.IsNullOrEmpty(ViewModel.Organizacion.Longitud))
            {
                hasValidCoordinates = ValidationUtils.ValidateCoordinates(ViewModel.Organizacion.Longitud);
            }

            if (!hasValidCoordinates) return;

            var result = await ViewModel.Submit();

            if (result)
            {
                DialogResult = true;

                Close();
            }
        }

        private void Dismiss(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void TextBoxCuit_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                ViewModel.AddError(e.Error);
            }
            else
            {
                ViewModel.RemoveError(e.Error);
            }
        }

        private void CuitValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");

            e.Handled = regex.IsMatch(e.Text) || TextBoxCuit.Text.Length > 11;
        }

        private void ButtonLocate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ViewModel.Organizacion.Latitud) ||
                string.IsNullOrEmpty(ViewModel.Organizacion.Longitud))
            {
                MessageBoxUtils.Error("Debe ingresar las coordenadas de la organizacion para localizarla");
            }

            var header = new OrganizacionHeaderData()
            {
                Direccion = ViewModel.Organizacion.Direccion,
                Latitud = ViewModel.Organizacion.Latitud,
                Longitud = ViewModel.Organizacion.Longitud,
                Email = ViewModel.Organizacion.Email,
                UsuarioInti = ViewModel.Organizacion.UsuarioInti,
                Telefono = ViewModel.Organizacion.Telefono,
                Nombre = ViewModel.Organizacion.Nombre,
                ContactoCargo = ViewModel.Organizacion.ContactoCargo
            };

            var window = new MapWindow(header);

            window.ShowDialog();
        }
    }
}
