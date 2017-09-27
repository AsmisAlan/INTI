using GeoUsers.Services.Model.DataTransfer;
using GeoUsers.Services.SQLExceptions;
using GeoUsersUI.Models.ViewModels.Forms;
using GeoUsersUI.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for SectorList.xaml
    /// </summary>
    public partial class SectorList : Window
    {
        private SectorListViewModel ViewModel { get; set; }

        public SectorList()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DataContext = ViewModel = App.Container.Resolve<SectorListViewModel>();

            Initialize();
        }

        private async void Initialize()
        {
            await UpdateSectores();
        }

        private async Task<bool> UpdateSectores()
        {
            ViewModel.SetLoading(true);

            await ViewModel.LoadSectores();

            ViewModel.SetLoading(false);

            return true;
        }

        private async void OpenEditionForm()
        {
            if (SectorGrid.SelectedItem == null)
            {
                return;
            }

            var sector = (SectorHeaderData)SectorGrid.SelectedItem;

            var form = new SectorCreationEditionForm(sector.Id);

            var result = form.ShowDialog();

            if (result.HasValue && result.Value)
            {
                await UpdateSectores();
            }
        }

        private void SectorGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenEditionForm();
        }

        private void EntityListButtonBar_OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void EntityListButtonBar_OnCreateButtonClick(object sender, RoutedEventArgs e)
        {
            var form = new SectorCreationEditionForm();

            var result = form.ShowDialog();

            if (result.HasValue && result.Value)
            {
                await UpdateSectores();
            }
        }

        private async void EntityListButtonBar_OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBoxUtils.Confirmation("Seguro desea eliminar el sector ?");

            if (result == MessageBoxResult.Yes)
            {
                var sector = (SectorHeaderData)SectorGrid.SelectedItem;

                await ViewModel.Delete(sector.Id);

                await UpdateSectores();
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
            PrintUtils.PrintDataGrid(SectorGrid, "Sectores");
        }
    }
}
