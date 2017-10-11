using CefSharp;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.GoogleMaps;
using GeoUsersUI.Models.ViewModels;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GeoUsersUI.Windows
{
    /// <summary>
    /// Interaction logic for MapWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        public MapWindowViewModel ViewModel { get; set; }

        private int OrganizacionId { get; set; }

        public MapWindow(int organizacionId)
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Browser.FrameLoadEnd += Browser_FrameLoadEnd;

            DataContext = ViewModel = App.Container.Resolve<MapWindowViewModel>();
            OrganizacionId = organizacionId;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadingMap = true;

            await ViewModel.LoadOrganizacion(OrganizacionId);

            var mapManager = new GoogleMapsManager();
            var organizaciones = new List<OrganizacionHeaderData>() { ViewModel.Organizacion };
            var url = mapManager.GetHtmlString(organizaciones.ToList());

            Browser.LoadHtml(url, "http://www.geousers.com.ar");
        }

        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            ViewModel.LoadingMap = false;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
