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
        private SectorEditionViewModel CastedDataContext { get; set; }

        public SectorCreationEditionForm(int? sectorId = null)
        {
            InitializeComponent();

            DataContext = CastedDataContext = App.Container.Resolve<SectorEditionViewModel>();

            Initialize(sectorId);
        }

        private async void Initialize(int? sectorId)
        {
            await CastedDataContext.Initialize(sectorId);
        }

        private void ButtonDismiss_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = await CastedDataContext.Submit();

            if (DialogResult.HasValue && DialogResult.Value)
            {
                Close();
            }
        }
    }
}
