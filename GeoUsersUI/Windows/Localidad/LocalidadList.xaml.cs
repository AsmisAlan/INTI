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
    /// Interaction logic for LocalidadList.xaml
    /// </summary>
    public partial class LocalidadList : Window
    {
        private LocalidadListViewModel ViewModel { get; set; }

        public LocalidadList()
        {
            InitializeComponent();

            DataContext = ViewModel = App.Container.Resolve<LocalidadListViewModel>();

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

            await UpdateLocalidades();
        }

        private async Task<bool> UpdateLocalidades()
        {
            await ViewModel.LoadLocalidades();

            return true;
        }

        private Func<Task<bool>> GetCreateFunction()
        {
            return async () =>
            {
                var form = new LocalidadCreationEditionForm();

                var result = form.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    await UpdateLocalidades();
                }

                return true;
            };
        }

        private Func<Task<bool>> GetEditFunction()
        {
            return async () =>
            {
                if (LocalidadGrid.SelectedItem == null)
                {
                    return false;
                }

                var localidad = (LocalidadHeaderData)LocalidadGrid.SelectedItem;

                var form = new LocalidadCreationEditionForm(localidad.Id);

                var result = form.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    await UpdateLocalidades();
                }

                return true;
            };
        }

        private Func<Task<bool>> GetDeleteFunction()
        {
            return async () =>
            {
                var result = MessageBoxUtils.ShowConfirmationBox("Seguro desea eliminar la localidad ?");

                if (result == MessageBoxResult.Yes)
                {
                    var localidad = (LocalidadHeaderData)LocalidadGrid.SelectedItem;

                    await ViewModel.Delete(localidad.Id);

                    await UpdateLocalidades();
                }

                return true;
            };
        }

        private void LocalidadGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewModel.EditFunction();
        }
    }
}
