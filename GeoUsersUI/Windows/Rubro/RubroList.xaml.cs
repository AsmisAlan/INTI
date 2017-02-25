using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Models.ViewModels.Forms;
using GeoUsersUI.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for RubroList.xaml
    /// </summary>
    public partial class RubroList : Window
    {
        private RubroListViewModel ViewModel { get; set; }

        public RubroList()
        {
            InitializeComponent();

            DataContext = ViewModel = App.Container.Resolve<RubroListViewModel>();

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

            await UpdateRubros();
        }

        private async Task<bool> UpdateRubros()
        {
            await ViewModel.LoadRubros();

            return true;
        }

        private Func<Task<bool>> GetCreateFunction()
        {
            return async () =>
            {
                var form = new RubroCreationEditionForm();

                var result = form.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    await UpdateRubros();
                }

                return true;
            };
        }

        private Func<Task<bool>> GetEditFunction()
        {
            return async () =>
            {
                if (RubroGrid.SelectedItem == null)
                {
                    return false;
                }

                var rubro = (RubroHeaderData)RubroGrid.SelectedItem;

                var form = new RubroCreationEditionForm(rubro.Id);

                var result = form.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    await UpdateRubros();
                }

                return true;
            };
        }

        private Func<Task<bool>> GetDeleteFunction()
        {
            return async () =>
            {
                var result = MessageBoxUtils.ShowConfirmationBox("Seguro desea eliminar el rubro ?");

                if (result == MessageBoxResult.Yes)
                {
                    var rubro = (RubroHeaderData)RubroGrid.SelectedItem;

                    await ViewModel.Delete(rubro.Id);

                    await UpdateRubros();
                }

                return true;
            };
        }

        private void RubroGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ViewModel.EditFunction();
        }
    }
}
