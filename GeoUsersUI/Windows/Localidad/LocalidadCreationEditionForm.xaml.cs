using GeoUsersUI.Models.ViewModels.Forms;
using System.Windows;
using Microsoft.Practices.Unity;
using GeoUsersUI.Models.ViewModels;

namespace GeoUsersUI.Windows.Localidad
{
    /// <summary>
    /// Interaction logic for LocalidadCreationEditionForm.xaml
    /// </summary>
    public partial class LocalidadCreationEditionForm : Window
    {
        private BaseSubmitViewModel<bool> CastViewModel { get; set; }

        public LocalidadCreationEditionForm()
        {
            InitializeComponent();

            DataContext = App.Container.Resolve<LocalidadCreationViewModel>();

            CastViewModel = ((BaseSubmitViewModel<bool>)DataContext);
        }

        public bool GetResult()
        {
            return CastViewModel.Result;
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            await CastViewModel.Submit();

            Close();
        }

        private void ButtonDismiss_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
