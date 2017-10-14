using CefSharp;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.GoogleMaps;
using GeoUsersUI.Models.ViewModels;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Windows;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for MapWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        private readonly GoogleMapsManager MapManager = new GoogleMapsManager();

        public MapWindowViewModel ViewModel { get; set; }

        private OrganizacionHeaderData Organizacion { get; set; }

        public MapWindow(OrganizacionHeaderData organizacion)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Browser.FrameLoadEnd += Browser_FrameLoadEnd;

            DataContext = ViewModel = App.Container.Resolve<MapWindowViewModel>();
            Organizacion = organizacion;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadingMap = true;

            var path = MapManager.GetWebManagerPath();

            Browser.Load(path);
        }

        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            ViewModel.LoadingMap = false;

            var organizacionesHelper = new List<OrganizacionHeaderData>() { Organizacion };
            var markersFunctions = MapManager.CreateMarkersFunction(organizacionesHelper);

            Browser.ExecuteScriptAsync(markersFunctions);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
