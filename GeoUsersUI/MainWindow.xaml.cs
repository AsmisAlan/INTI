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
using GeoUsersUI.Models.ViewModels.UserControls;
using CefSharp;
using GeoUsersUI.UserControls;
using System;
using System.Linq;

namespace GeoUsersUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Browser.FrameLoadEnd += Browser_FrameLoadEnd;

            DataContext = ViewModel = App.Container.Resolve<MainViewModel>();

            var loadingFinished = Initialize();
        }

        private async Task<bool> Initialize()
        {
            InitializeMainMenu();

            await ViewModel.InitializationTask;

            ViewModel.LoadingTable = false;

            var markersFunctions = await GetMapMarkersFunction(ViewModel.Organizaciones);

            loadStaticMap(); //carga el html local 

            executeJS(markersFunctions); //ejecuta una funcion js en la pagina levantada

            return true;
        }

        private void loadStaticMap()
        {
            Browser.Load("file:///C:/Proyectos/Visual%20Studio/INTI/GeoUsers.Services/Utils/GoogleMaps/googleMap/index.html");
        }

        private void executeJS(string jsFunction)
        {
            Browser.ExecuteScriptAsync(jsFunction);
        }


        private void InitializeMainMenu()
        {
            var listOrganizacionButton = new MenuButton()
            {
                Name = "Listado",
                ButtonVisibility = Visibility.Collapsed
            };

            listOrganizacionButton.ButtonClick += OpenOrganizacionList;

            var organizacionButtons = new ObservableCollection<MenuButton>();

            organizacionButtons.Add(listOrganizacionButton);

            var rubroButtons = new ObservableCollection<MenuButton>();

            var listRubroButton = MenuButton.Copy(listOrganizacionButton);

            listRubroButton.ButtonClick += OpenRubroList;

            rubroButtons.Add(listRubroButton);

            var sectorButtons = new ObservableCollection<MenuButton>();

            var listSectorButton = MenuButton.Copy(listOrganizacionButton);

            listSectorButton.ButtonClick += OpenSectorList;

            sectorButtons.Add(listSectorButton);

            var tipoOrganizacionButtons = new ObservableCollection<MenuButton>();

            var listTipoOrganizacionButton = MenuButton.Copy(listOrganizacionButton);

            listTipoOrganizacionButton.ButtonClick += OpenTipoOrganizacionList;

            tipoOrganizacionButtons.Add(listTipoOrganizacionButton);

            var localidadButtons = new ObservableCollection<MenuButton>();

            var localidadListButton = MenuButton.Copy(listOrganizacionButton);

            localidadListButton.ButtonClick += OpenLocalidadList;

            localidadButtons.Add(localidadListButton);

            if (App.IsUserAuthenticated)
            {
                var newOrganizacionButton = new MenuButton()
                {
                    Name = "Nuevo",
                    ButtonVisibility = Visibility.Collapsed
                };

                newOrganizacionButton.ButtonClick += NewOrganizacionButton_ButtonClick;

                organizacionButtons.Add(newOrganizacionButton);

                var newRubroButton = MenuButton.Copy(newOrganizacionButton);

                newRubroButton.ButtonClick += NewRubroButton_ButtonClick;

                rubroButtons.Add(newRubroButton);

                var newSectorButton = MenuButton.Copy(newOrganizacionButton);

                newSectorButton.ButtonClick += NewSectorButton_ButtonClick;

                sectorButtons.Add(newSectorButton);

                var newTipoOrganizacionButton = MenuButton.Copy(newOrganizacionButton);

                newTipoOrganizacionButton.ButtonClick += NewTipoOrganizacionButton_ButtonClick;

                tipoOrganizacionButtons.Add(newTipoOrganizacionButton);

                var newLocalidadButton = MenuButton.Copy(newOrganizacionButton);

                newLocalidadButton.ButtonClick += NewLocalidadButton_ButtonClick;

                localidadButtons.Add(newLocalidadButton);
            }

            ViewModel.OrganizacionMenuContainer = new MenuContainer()
            {
                Buttons = organizacionButtons,
                HeaderName = "Organizaciones",
                IsMenuOpened = false
            };

            ViewModel.RubroMenuContainer = new MenuContainer()
            {
                Buttons = rubroButtons,
                HeaderName = "Rubros",
                IsMenuOpened = false
            };

            ViewModel.SectorMenuContainer = new MenuContainer()
            {
                Buttons = sectorButtons,
                HeaderName = "Sectores",
                IsMenuOpened = false
            };

            ViewModel.TipoOrganizacionMenuContainer = new MenuContainer()
            {
                Buttons = tipoOrganizacionButtons,
                HeaderName = "Tipo Organizaciones",
                IsMenuOpened = false
            };

            ViewModel.LocalidadMenuContainer = new MenuContainer()
            {
                Buttons = localidadButtons,
                HeaderName = "Localidades",
                IsMenuOpened = false
            };
        }


        private async Task<string> GetMapMarkersFunction(IEnumerable<OrganizacionHeaderData> organizaciones)
        /*create the string js funtion and add the parameters*/
        {
            return await Task.Run(() =>
            {
                var mapManager = new GoogleMapsManager();

                return mapManager.createMarkersFunction(organizaciones.ToList());
            });
        }


        private async Task<bool> UpdateMap()
        /*update the map markers*/
        {
            var markers = await GetMapMarkersFunction(ViewModel.Organizaciones);
            executeJS(markers);
            return true;
        }

        private async Task<bool> UpdateUI()
        {
            //ViewModel.LoadingMap = true;
            ViewModel.LoadingTable = true;

            await ViewModel.UpdateOrganizacionHeaders();

            ViewModel.LoadingTable = false;

            return await UpdateMap();
        }

        private async void OpenOrganizacionCreationForm(int? organizacionId = null)
        {
            var form = new OrganizacionCreationEditionForm(organizacionId);

            if (form.ShowDialog().Value)
            {
                await UpdateUI();
            }
        }

        private bool OpenRubroCreationForm(int? rubroId = null)
        {
            var form = new RubroCreationEditionForm();

            form.ShowDialog();

            return true;
        }

        private bool OpenTipoOrganizacionCreationForm(int? tipoOrganizacion = null)
        {
            var form = new TipoOrganizacionCreationEditionForm();

            form.ShowDialog();

            return true;
        }

        private bool OpenSectorCreationForm(int? sectorId = null)
        {
            var form = new SectorCreationEditionForm();

            form.ShowDialog();

            return true;
        }

        private bool OpenLocalidadCreationForm(int? localidadId = null)
        {
            var form = new LocalidadCreationEditionForm();

            form.ShowDialog();

            return true;
        }

        private void NewLocalidadButton_ButtonClick(object sender, EventArgs e)
        {
            OpenLocalidadCreationForm();
        }

        private void NewTipoOrganizacionButton_ButtonClick(object sender, EventArgs e)
        {
            OpenTipoOrganizacionCreationForm();
        }

        private void NewSectorButton_ButtonClick(object sender, EventArgs e)
        {
            OpenSectorCreationForm();
        }

        private void NewRubroButton_ButtonClick(object sender, EventArgs e)
        {
            OpenRubroCreationForm();
        }

        private void NewOrganizacionButton_ButtonClick(object sender, System.EventArgs e)
        {
            OpenOrganizacionCreationForm();
        }

        private void OpenOrganizacionList(object sender, EventArgs e)
        {
            var form = new OrganizacionList();

            form.ShowDialog();
        }

        private void OpenRubroList(object sender, EventArgs e)
        {
            var form = new RubroList();

            form.ShowDialog();
        }

        private void OpenSectorList(object sender, EventArgs e)
        {
            var form = new SectorList();

            form.ShowDialog();
        }

        private void OpenTipoOrganizacionList(object sender, EventArgs e)
        {
            var form = new TipoOrganizacionList();

            form.ShowDialog();
        }

        private void OpenLocalidadList(object sender, EventArgs e)
        {
            var form = new LocalidadList();

            form.ShowDialog();
        }

        private async void SectorFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var update = ViewModel.ApplySectorFilter();

            if (update.HasValue && update.Value)
            {
                await UpdateUI();
            }
        }

        private async void RubroFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var update = ViewModel.ApplyRubroFilter();

            if (update.HasValue && update.Value)
            {
                await UpdateUI();
            }
        }

        private async void TipoOrganizacionFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var update = ViewModel.ApplyTipoOrganizacionFilter();

            if (update.HasValue && update.Value)
            {
                await UpdateUI();
            }
        }

        private async void LocalidadFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var update = ViewModel.ApplyLocalidadFilter();

            if (update.HasValue && update.Value)
            {
                await UpdateUI();
            }
        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ViewModel.InitializationTask != null && ViewModel.InitializationTask.IsCompleted)
            {
                await UpdateUI();
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DataGrid.SelectedItem == null)
            {
                return;
            }

            var organizacion = (OrganizacionHeaderData)DataGrid.SelectedItem;

            OpenOrganizacionCreationForm(organizacion.Id);
        }

        private void SearchBar_OnSearchTermChanged(object sender, RoutedEventArgs e)
        {
            ViewModel.FilterOrganizaciones(((OnSearchTermChangedEventArgs)e).SearchTerm);
        }

        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            ViewModel.LoadingMap = false;
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            await UpdateUI();
        }
    }
}
