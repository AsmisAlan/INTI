using GeoUsersUI.Models.ViewModels;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for SectorCreationEditionForm.xaml
    /// </summary>
    public partial class SectorCreationEditionForm : Window
    {
        private SectorEditionViewModel ViewModel { get; set; }

        public SectorCreationEditionForm(int? sectorId = null)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DataContext = ViewModel = App.Container.Resolve<SectorEditionViewModel>();

            Initialize(sectorId);
        }

        private async void Initialize(int? sectorId)
        {
            await ViewModel.Initialize(sectorId);
        }

        private void ButtonDismiss_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            var result = await ViewModel.Submit();

            if (result)
            {
                DialogResult = true;

                Close();
            }
        }

        private void ButtonIcono_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            fileDialog.Multiselect = false;

            var result = fileDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                byte[] fileData = null;

                using (var file = fileDialog.OpenFile())
                {
                    var bytes = new byte[file.Length];

                    file.Read(bytes, 0, (int)file.Length);

                    fileData = bytes;
                }

                ViewModel.SetIconoData(fileDialog.SafeFileName, fileDialog.FileName, fileData);
            }
        }

        private void ButtonRemoveIcono_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteSectorIcono();
        }
    }
}
