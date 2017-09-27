using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.SQLExceptions;
using GeoUsersUI.Models.ViewModels.Forms;
using GeoUsersUI.Utils;
using Microsoft.Practices.Unity;
using System.Threading.Tasks;
using System.Windows;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for LocalidadList.xaml
    /// </summary>
    public partial class LocalidadList : Window
    {
        private LocalidadListViewModel ViewModel { get; set; }

        public LocalidadList()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DataContext = ViewModel = App.Container.Resolve<LocalidadListViewModel>();

            Initialize();
        }

        private async void Initialize()
        {
            await UpdateLocalidades();
        }

        private async Task<bool> UpdateLocalidades()
        {
            ViewModel.SetLoading(true);

            await ViewModel.LoadLocalidades();

            ViewModel.SetLoading(false);

            return true;
        }

        private async void OpenEditionForm()
        {
            if (LocalidadGrid.SelectedItem == null)
            {
                return;
            }

            var localidad = (LocalidadHeaderData)LocalidadGrid.SelectedItem;

            var form = new LocalidadCreationEditionForm(localidad.Id);

            var result = form.ShowDialog();

            if (result.HasValue && result.Value)
            {
                await UpdateLocalidades();
            }
        }

        private async void ButtonBar_OnCreateButtonClick(object sender, RoutedEventArgs e)
        {
            var form = new LocalidadCreationEditionForm();

            var result = form.ShowDialog();

            if (result.HasValue && result.Value)
            {
                await UpdateLocalidades();
            }
        }

        private void LocalidadGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenEditionForm();
        }

        private void ButtonBar_OnEditButtonClick(object sender, RoutedEventArgs e)
        {
            OpenEditionForm();
        }

        private async void ButtonBar_OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBoxUtils.Confirmation("Seguro desea eliminar la localidad ?");

            if (result == MessageBoxResult.Yes)
            {
                var localidad = (LocalidadHeaderData)LocalidadGrid.SelectedItem;

                try
                {
                    await ViewModel.Delete(localidad.Id);
                }
                catch
                {
                }

                await UpdateLocalidades();
            }
        }

        private void ButtonBar_OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void DataGridExportButtonBar_OnExportButtonClick(object sender, RoutedEventArgs e)
        {
            await ViewModel.ExportToExcel();
        }

        private void DataGridExportButtonBar_OnPrintButtonClick(object sender, RoutedEventArgs e)
        {
            PrintUtils.PrintDataGrid(LocalidadGrid, "Localidades");
        }
    }
}
