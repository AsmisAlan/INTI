using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using Microsoft.Practices.Unity;
using System.Windows;
using GeoUsersUI.Models.ViewModels;
using System.Threading.Tasks;
using System.Linq;

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

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DataContext = ViewModel = App.Container.Resolve<OrganizacionListViewModel>();

            Initialize();
        }

        private async void Initialize()
        {
            await UpdateOrganizaciones();
        }

        private async Task<bool> UpdateOrganizaciones()
        {
            ViewModel.StartLoadingTable();

            await ViewModel.LoadOrganizaciones();

            ViewModel.StopLoadingTable();

            return true;
        }

        private async void OpenEditionForm()
        {
            if (OrganizacionGrid.SelectedItem == null)
            {
                return;
            }

            var organizacion = (OrganizacionData)OrganizacionGrid.SelectedItem;

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
                var organizacion = (OrganizacionData)OrganizacionGrid.SelectedItem;

                await ViewModel.Delete(organizacion.Id);

                await UpdateOrganizaciones();
            }
        }

        private void EntityListButtonBar_OnEditButtonClick(object sender, RoutedEventArgs e)
        {
            OpenEditionForm();
        }

        private async void DataGridExportButtonBar_OnExportButtonClick(object sender, RoutedEventArgs e)
        {
            await ViewModel.ExportToExcel();
        }

        private void DataGridExportButtonBar_OnPrintButtonClick(object sender, RoutedEventArgs e)
        {
            PrintUtils.PrintDataGrid(OrganizacionGrid, "Organizaciones");
        }

        private void RubroFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new SmartSelectWindow(() => { return ViewModel.GetRubros(); },
                                             () => { return ViewModel.GetRubrosByIds(); },
                                             ViewModel.OrganizacionesFilter.Filter.RubroIds,
                                             "Rubros");
            form.ShowDialog();

            if (form.DialogResult.HasValue && form.DialogResult.Value)
            {
                ViewModel.OrganizacionesFilter.Filter.RubroIds = form.GetSelection().ToList();

                ViewModel.OrganizacionesFilter.UpdateStatuses();
            }
        }

        private void TipoOrganizacionFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var currentIds = ViewModel.OrganizacionesFilter.Filter.TipoOrganizacionIds;

            var form = new SmartSelectWindow(() => { return ViewModel.GetTipoOrganizaciones(); },
                                             () => { return ViewModel.GetTipoOrganizacionesByIds(); },
                                             currentIds,
                                             "Tipos de Organizaciones");
            form.ShowDialog();

            if (form.DialogResult.HasValue && form.DialogResult.Value)
            {
                ViewModel.OrganizacionesFilter.Filter.TipoOrganizacionIds = form.GetSelection().ToList();

                ViewModel.OrganizacionesFilter.UpdateStatuses();
            }
        }

        private void LocalidadFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new SmartSelectWindow(() => { return ViewModel.GetLocalidades(); },
                                             () => { return ViewModel.GetLocalidadesByIds(); },
                                             ViewModel.OrganizacionesFilter.Filter.LocalidadIds,
                                             "Localidades");
            form.ShowDialog();

            if (form.DialogResult.HasValue && form.DialogResult.Value)
            {
                ViewModel.OrganizacionesFilter.Filter.LocalidadIds = form.GetSelection().ToList();

                ViewModel.OrganizacionesFilter.UpdateStatuses();
            }
        }

        private void SectorFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var form = new SmartSelectWindow(() => { return ViewModel.GetSectores(); },
                                             () => { return ViewModel.GetSectoresByIds(); },
                                             ViewModel.OrganizacionesFilter.Filter.SectorIds,
                                             "Sectores");
            form.ShowDialog();

            if (form.DialogResult.HasValue && form.DialogResult.Value)
            {
                ViewModel.OrganizacionesFilter.Filter.SectorIds = form.GetSelection().ToList();

                ViewModel.OrganizacionesFilter.UpdateStatuses();
            }
        }

        private void FilterCollapseButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.FiltersVisibility = ViewModel.FiltersVisibility == Visibility.Collapsed ? Visibility.Visible :
                                                                                                Visibility.Collapsed;
        }

        private async void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadOrganizaciones();
        }
    }
}
