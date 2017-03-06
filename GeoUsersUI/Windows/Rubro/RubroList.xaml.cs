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
            await UpdateRubros();
        }

        private async Task<bool> UpdateRubros()
        {
            await ViewModel.LoadRubros();

            return true;
        }

        private async void OpenEditionForm()
        {
            if (RubroGrid.SelectedItem == null)
            {
                return;
            }

            var rubro = (RubroHeaderData)RubroGrid.SelectedItem;

            var form = new RubroCreationEditionForm(rubro.Id);

            var result = form.ShowDialog();

            if (result.HasValue && result.Value)
            {
                await UpdateRubros();
            }
        }

        private void RubroGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenEditionForm();
        }

        private void EntityListButtonBar_OnCloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void EntityListButtonBar_OnCreateButtonClick(object sender, RoutedEventArgs e)
        {
            var form = new RubroCreationEditionForm();

            var result = form.ShowDialog();

            if (result.HasValue && result.Value)
            {
                await UpdateRubros();
            }
        }

        private async void EntityListButtonBar_OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBoxUtils.Confirmation("Seguro desea eliminar el rubro ?");

            if (result == MessageBoxResult.Yes)
            {
                var rubro = (RubroHeaderData)RubroGrid.SelectedItem;

                try
                {
                    await ViewModel.Delete(rubro.Id);
                }
                catch (ReferencedEntityException)
                {
                    MessageBoxUtils.Error("El rubro que se intenta eliminar se encuentra en uso.");

                    return;
                }

                await UpdateRubros();
            }
        }

        private void EntityListButtonBar_OnEditButtonClick(object sender, RoutedEventArgs e)
        {
            OpenEditionForm();
        }
    }
}
