using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using Microsoft.Practices.Unity;
using System.Windows;
using GeoUsersUI.Models.ViewModels;
using System.Threading.Tasks;
using System;

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
            ViewModel.CloseFunction = () =>
            {
                Close();

                return true;
            };

            ViewModel.EditFunction = GetEditFunction();
            ViewModel.DeleteFunction = GetDeleteFunction();
            ViewModel.CreateFunction = GetCreateFunction();

            await UpdateOrganizaciones();
        }

        private async Task<bool> UpdateOrganizaciones()
        {
            await ViewModel.LoadOrganizaciones();

            return true;
        }

        private Func<Task<bool>> GetCreateFunction()
        {
            return async () =>
            {
                var form = new OrganizacionCreationEditionForm();

                var result = form.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    await UpdateOrganizaciones();
                }

                return true;
            };
        }

        private Func<Task<bool>> GetEditFunction()
        {
            return async () =>
            {
                if (OrganizacionGrid.SelectedItem == null)
                {
                    return false;
                }

                var organizacion = (OrganizacionHeaderData)OrganizacionGrid.SelectedItem;

                var form = new OrganizacionCreationEditionForm(organizacion.Id);

                var result = form.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    await UpdateOrganizaciones();
                }

                return true;
            };
        }

        private Func<Task<bool>> GetDeleteFunction()
        {
            Func<Task<bool>> function = async () =>
            {
                var result = MessageBoxUtils.ShowConfirmationBox("Seguro desea eliminar la organización ?");

                if (result == MessageBoxResult.Yes)
                {
                    var organizacion = (OrganizacionHeaderData)OrganizacionGrid.SelectedItem;

                    await ViewModel.Delete(organizacion.Id);

                    await UpdateOrganizaciones();
                }

                return true;
            };

            return function;
        }

        private void OrganizacionGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewModel.EditFunction();
        }
    }
}
