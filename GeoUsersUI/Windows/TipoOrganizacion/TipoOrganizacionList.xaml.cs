using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Models.ViewModels.Forms;
using GeoUsersUI.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for TipoOrganizacionList.xaml
    /// </summary>
    public partial class TipoOrganizacionList : Window
    {
        private TipoOrganizacionListViewModel ViewModel { get; set; }

        public TipoOrganizacionList()
        {
            InitializeComponent();

            DataContext = ViewModel = App.Container.Resolve<TipoOrganizacionListViewModel>();

            Initialize();
        }

        private async void Initialize()
        {
            ViewModel.CloseFunction = () =>
            {
                Close();

                return true;
            };

            ViewModel.EditFunction = GetEditFunction();
            ViewModel.DeleteFunction = GetDeleteFunction();
            ViewModel.CreateFunction = GetCreateFunction();

            await UpdateTipoOrganizaciones();
        }

        private async Task<bool> UpdateTipoOrganizaciones()
        {
            await ViewModel.LoadTipoOrganizaciones();

            return true;
        }

        private Func<Task<bool>> GetCreateFunction()
        {
            return async () =>
            {
                var form = new TipoOrganizacionCreationEditionForm();

                var result = form.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    await UpdateTipoOrganizaciones();
                }

                return true;
            };
        }

        private Func<Task<bool>> GetEditFunction()
        {
            return async () =>
            {
                if (TipoOrganizacionGrid.SelectedItem == null)
                {
                    return false;
                }

                var tipoOrganizacion = (TipoOrganizacionHeaderData)TipoOrganizacionGrid.SelectedItem;

                var form = new TipoOrganizacionCreationEditionForm(tipoOrganizacion.Id);

                var result = form.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    await UpdateTipoOrganizaciones();
                }

                return true;
            };
        }

        private Func<Task<bool>> GetDeleteFunction()
        {
            return async () =>
            {
                var result = MessageBoxUtils.ShowConfirmationBox("Seguro desea eliminar el tipo de organización ?");

                if (result == MessageBoxResult.Yes)
                {
                    var tipoOrganizacion = (TipoOrganizacionHeaderData)TipoOrganizacionGrid.SelectedItem;

                    await ViewModel.Delete(tipoOrganizacion.Id);

                    await UpdateTipoOrganizaciones();
                }

                return true;
            };
        }

        private void TipoOrganizacionGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewModel.EditFunction();
        }
    }
}
