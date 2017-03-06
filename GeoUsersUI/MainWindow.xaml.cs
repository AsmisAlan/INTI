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

            Browser.FrameLoadEnd += Browser_FrameLoadEnd;

            DataContext = ViewModel = App.Container.Resolve<MainViewModel>();

            var loadingFinished = Initialize();
        }

        private async Task<bool> Initialize()
        {
            InitializeMainMenu();

            await ViewModel.InitializationTask;

            var url = await GetMapUrl(ViewModel.Organizaciones);

            UpdateMapUrl(url);

            return true;
        }

        private void UpdateMapUrl(string url)
        {
            Browser.LoadHtml(url, "http://www.geousers.com.ar");
        }

        private void InitializeMainMenu()
        {
            var newOrganizacionButton = new MenuButton()
            {
                Name = "Nuevo",
                ButtonVisibility = Visibility.Collapsed
            };

            newOrganizacionButton.ButtonClick += NewOrganizacionButton_ButtonClick;

            var listOrganizacionButton = new MenuButton()
            {
                Name = "Listado",
                ButtonVisibility = Visibility.Collapsed
            };

            listOrganizacionButton.ButtonClick += OpenOrganizacionList;

            var organizacionButtons = new ObservableCollection<MenuButton>();

            organizacionButtons.Add(newOrganizacionButton);
            organizacionButtons.Add(listOrganizacionButton);

            var rubroButtons = new ObservableCollection<MenuButton>();

            var newRubroButton = MenuButton.Copy(newOrganizacionButton);
            var listRubroButton = MenuButton.Copy(listOrganizacionButton);

            newRubroButton.ButtonClick += NewRubroButton_ButtonClick;
            listRubroButton.ButtonClick += OpenRubroList;

            rubroButtons.Add(MenuButton.Copy(newOrganizacionButton));
            rubroButtons.Add(MenuButton.Copy(listOrganizacionButton));

            var sectorButtons = new ObservableCollection<MenuButton>();

            var newSectorButton = MenuButton.Copy(newOrganizacionButton);
            var listSectorButton = MenuButton.Copy(listOrganizacionButton);

            newSectorButton.ButtonClick += NewSectorButton_ButtonClick; ;
            listSectorButton.ButtonClick += OpenSectorList;

            sectorButtons.Add(newSectorButton);
            sectorButtons.Add(listSectorButton);

            var tipoOrganizacionButtons = new ObservableCollection<MenuButton>();

            var newTipoOrganizacionButton = MenuButton.Copy(newOrganizacionButton);
            var listTipoOrganizacionButton = MenuButton.Copy(listOrganizacionButton);

            newTipoOrganizacionButton.ButtonClick += NewTipoOrganizacionButton_ButtonClick;
            listTipoOrganizacionButton.ButtonClick += OpenTipoOrganizacionList;

            tipoOrganizacionButtons.Add(newTipoOrganizacionButton);
            tipoOrganizacionButtons.Add(listTipoOrganizacionButton);

            var localidadButtons = new ObservableCollection<MenuButton>();

            var newLocalidadButton = MenuButton.Copy(newOrganizacionButton);
            var localidadListButton = MenuButton.Copy(listOrganizacionButton);

            newLocalidadButton.ButtonClick += NewLocalidadButton_ButtonClick;
            localidadListButton.ButtonClick += OpenLocalidadList;

            localidadButtons.Add(newLocalidadButton);
            localidadButtons.Add(localidadListButton);

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



        private async Task<string> GetMapUrl(IEnumerable<OrganizacionHeaderData> organizaciones)
        {
            return await Task.Run(() =>
            {
                var mapManager = new GoogleMapsManager();

                return mapManager.GetHtmlString(organizaciones);
            });
        }

        private async Task<bool> UpdateMap()
        {
            var url = await GetMapUrl(ViewModel.Organizaciones);

            UpdateMapUrl(url);

            return true;
        }

        private async Task<bool> UpdateUI()
        {
            ViewModel.LoadingMap = Visibility.Visible;

            await ViewModel.UpdateOrganizacionHeaders();

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
            ViewModel.LoadingMap = Visibility.Hidden;
        }
    }
}
