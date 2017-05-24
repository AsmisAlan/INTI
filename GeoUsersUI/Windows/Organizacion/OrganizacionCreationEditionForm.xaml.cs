using GeoUsersUI.Models.ViewModels.Forms;
using System.Windows;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using GeoUsers.Services.Model.DataTransfer;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System;
using System.Windows.Controls;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for UsuarioCreationEditionForm.xaml
    /// </summary>
    public partial class OrganizacionCreationEditionForm : Window
    {
        private OrganizacionViewModel ViewModel { get; set; }
        ValidationError error;
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
    }
}
