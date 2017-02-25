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
    /// Interaction logic for SectorList.xaml
    /// </summary>
    public partial class SectorList : Window
    {
        private SectorListViewModel ViewModel { get; set; }

        public SectorList()
        {
            InitializeComponent();

            DataContext = ViewModel = App.Container.Resolve<SectorListViewModel>();

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

            await UpdateSectores();
        }

        private async Task<bool> UpdateSectores()
        {
            await ViewModel.LoadSectores();

            return true;
        }

        private Func<Task<bool>> GetCreateFunction()
        {
            return async () =>
            {
                var form = new SectorCreationEditionForm();

                var result = form.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    await UpdateSectores();
                }

                return true;
            };
        }

        private Func<Task<bool>> GetEditFunction()
        {
            return async () =>
            {
                if (SectorGrid.SelectedItem == null)
                {
                    return false;
                }

                var sector = (SectorHeaderData)SectorGrid.SelectedItem;

                var form = new SectorCreationEditionForm(sector.Id);

                var result = form.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    await UpdateSectores();
                }

                return true;
            };
        }

        private Func<Task<bool>> GetDeleteFunction()
        {
            return async () =>
            {
                var result = MessageBoxUtils.ShowConfirmationBox("Seguro desea eliminar el sector ?");

                if (result == MessageBoxResult.Yes)
                {
                    var sector = (SectorHeaderData)SectorGrid.SelectedItem;

                    await ViewModel.Delete(sector.Id);

                    await UpdateSectores();
                }

                return true;
            };
        }

        private void SectorGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewModel.EditFunction();
        }
    }
}
