using GeoUsersUI.Models.ViewModels;
using System.Windows;
using Microsoft.Practices.Unity;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for OrganizacionCreationEditionForm.xaml
    /// </summary>
    public partial class OrganizacionCreationEditionForm : Window
    {
        private BaseOrganizacionViewModel ViewModel { get; set; }

        public OrganizacionCreationEditionForm()
        {
            DataContext = App.Container.Resolve<OrganizacionCreationViewModel>();

            Initialize();
        }

        public OrganizacionCreationEditionForm(long organizacionId)
        {
        }

        private void Initialize()
        {
            InitializeComponent();

            ViewModel = ((BaseOrganizacionViewModel)DataContext);
        }

        public bool GetResult()
        {
            return ViewModel.Result;
        }

        private async void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = await ViewModel.Submit();

            Close();
        }

        private void ButtonDismiss_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }
    }
}
