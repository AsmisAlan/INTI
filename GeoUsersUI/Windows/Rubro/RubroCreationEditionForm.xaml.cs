using GeoUsersUI.Models.ViewModels;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for RubroCreationEditionForm.xaml
    /// </summary>
    public partial class RubroCreationEditionForm : Window
    {
        public RubroCreationEditionForm()
        {
            InitializeComponent();

            DataContext = App.Container.Resolve<RubroCreationViewModel>();

            var initialized = Initialize();
        }

        public RubroCreationEditionForm(long rubroId)
        {
            InitializeComponent();
        }

        private async Task<bool> Initialize()
        {
            await ((RubroCreationViewModel)DataContext).LoadData();

            ComboSector.ItemsSource = ((RubroCreationViewModel)DataContext).Sectores;

            return true;
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = await ((RubroCreationViewModel)DataContext).Submit();

            if (DialogResult.HasValue && DialogResult.Value)
            {
                Close();
            }
        }

        private void ButtonDismiss_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
