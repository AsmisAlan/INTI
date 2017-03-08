using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using Microsoft.Practices.Unity;
using System.Windows;
using GeoUsersUI.Models.ViewModels;
using System.Threading.Tasks;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for OrganizacionList.xaml
    /// </summary>
    public partial class OrganizacionList : Window
    {
        private OrganizacionListViewModel ViewModel { get; set; }

        public OrganizacionList()
        {
            InitializeComponent();

            DataContext = ViewModel = App.Container.Resolve<OrganizacionListViewModel>();

            Initialize();
        }

        private async void Initialize()
        {
            await UpdateOrganizaciones();
        }

        private async Task<bool> UpdateOrganizaciones()
        {
            await ViewModel.LoadOrganizaciones();

            return true;
        }

        private async void OpenEditionForm()
        {
            if (OrganizacionGrid.SelectedItem == null)
            {
                return;
            }

            var organizacion = (OrganizacionHeaderData)OrganizacionGrid.SelectedItem;

            var form = new OrganizacionCreationEditionForm(organizacion.Id);

            var result = form.ShowDialog();

            if (result.HasValue && result.Value)
            {
                await UpdateOrganizaciones();
            }
        }

        private void OrganizacionGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenEditionForm();
        }

        private void EntityListButtonBar_OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void EntityListButtonBar_OnCreateButtonClick(object sender, RoutedEventArgs e)
        {
            var form = new OrganizacionCreationEditionForm();

            var result = form.ShowDialog();

            if (result.HasValue && result.Value)
            {
                await UpdateOrganizaciones();
            }
        }

        private async void EntityListButtonBar_OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBoxUtils.Confirmation("Seguro desea eliminar la organización ?");

            if (result == MessageBoxResult.Yes)
            {
                var organizacion = (OrganizacionHeaderData)OrganizacionGrid.SelectedItem;

                await ViewModel.Delete(organizacion.Id);

                await UpdateOrganizaciones();
            }
        }

        private void EntityListButtonBar_OnEditButtonClick(object sender, RoutedEventArgs e)
        {
            OpenEditionForm();
        }

        private void DataGridExportButtonBar_OnExportButtonClick(object sender, RoutedEventArgs e)
        {
            ExcelExportUtils.ExportToExcel(OrganizacionGrid);
        }

        private void DataGridExportButtonBar_OnPrintButtonClick(object sender, RoutedEventArgs e)
        {
            PrintUtils.PrintDataGrid(OrganizacionGrid, "Organizaciones");
        }
    }
}
