using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;
using GeoUsersUI.GoogleMaps;
using GeoUsers.Services.Logics;
using GeoUsers.Services;
using System.Collections.ObjectModel;
using GeoUsersUI.Windows.Usuario;
using GeoUsersUI.Models.ViewModels.Forms;

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
                ((MainViewModel)DataContext).Usuarios.Add(form.Result);

                var url = await GetMapUrl(((MainViewModel)DataContext).Usuarios);

                Browser.NavigateToString(url);
            }
        }
    }
}
