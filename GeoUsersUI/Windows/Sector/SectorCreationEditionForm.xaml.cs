using GeoUsersUI.Models.ViewModels;
using Microsoft.Practices.Unity;
using System.Windows;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for SectorCreationEditionForm.xaml
    /// </summary>
    public partial class SectorCreationEditionForm : Window
    {
        private BaseSectorViewModel ViewModel { get; set; }

        public SectorCreationEditionForm()
        {
            DataContext = App.Container.Resolve<SectorCreationViewModel>();

            Initialize();
        }

        public SectorCreationEditionForm(long organizacionId)
        {
        }

        private void Initialize()
        {
            InitializeComponent();

            ViewModel = ((BaseSectorViewModel)DataContext);
        }

        private void ButtonDismiss_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = await ViewModel.Submit();

            if (DialogResult.HasValue && DialogResult.Value)
            {
                Close();
            }
        }
    }
}
