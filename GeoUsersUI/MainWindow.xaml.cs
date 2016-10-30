using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using GeoUsersUI.GoogleMaps;
using System.Collections.ObjectModel;
using GeoUsersUI.Windows;

namespace GeoUsersUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = App.Container.Resolve<MainViewModel>();

            DataGrid.ItemsSource = new ObservableCollection<UsuarioHeader>();

            var loadingFinished = Initialize();
        }

        public async Task<bool> Initialize()
        {
            var usuarios = await ((MainViewModel)DataContext).InitializationTask;

            DataGrid.ItemsSource = ((MainViewModel)DataContext).Usuarios;

            var url = await GetMapUrl(usuarios);

            Browser.NavigateToString(url);

            return true;
        }

        public async Task<string> GetMapUrl(IEnumerable<UsuarioHeader> usuarios)
        {
            return await Task.Run(() =>
            {
                var mapManager = new MapManager();

                foreach (var usuario in (usuarios))
                {
                    mapManager.addDireccion(usuario.Direccion, usuario.Nombre);
                }

                return mapManager.getHtmlString();
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void ButtonAddUsuario_Click(object sender, RoutedEventArgs e)
        {
            var form = new UsuarioCreationEditionForm();

            if (form.ShowDialog().Value)
            {
                ((MainViewModel)DataContext).Usuarios.Add(form.GetResult());

                var url = await GetMapUrl(((MainViewModel)DataContext).Usuarios);

                Browser.NavigateToString(url);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonNewLocalidad_Click(object sender, RoutedEventArgs e)
        {
            var form = new RubroCreationEditionForm();

            form.ShowDialog();

            //var form = new SectorCreationEditionForm();

            //form.ShowDialog();

            //var form = new LocalidadCreationEditionForm();

            //form.ShowDialog();
        }

        private void ButtonNewOrganizacion_Click(object sender, RoutedEventArgs e)
        {
            var form = new OrganizacionCreationEditionForm();

            form.ShowDialog();
        }
    }
}
