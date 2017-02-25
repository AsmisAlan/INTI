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

namespace GeoUsersUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel CastedDataContext { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = CastedDataContext = App.Container.Resolve<MainViewModel>();

            var loadingFinished = Initialize();
        }

        public async Task<bool> Initialize()
        {
            InitializeMainMenu();

            await CastedDataContext.InitializationTask;

            var url = await GetMapUrl(CastedDataContext.Organizaciones);

            Browser.NavigateToString(url);

            return true;
        }

        private void InitializeMainMenu()
        {
            var newOrganizacionButton = new MenuButton()
            {
                Name = "Nuevo",
                OnButtonClickAction = () => { return OpenOrganizacionCreationForm(); },
                ButtonVisibility = Visibility.Collapsed
            };

            var listOrganizacionButton = new MenuButton()
            {
                Name = "Listado",
                OnButtonClickAction = () => { return OpenOrganizacionList(); },
                ButtonVisibility = Visibility.Collapsed
            };

            var organizacionButtons = new ObservableCollection<MenuButton>();

            organizacionButtons.Add(newOrganizacionButton);
            organizacionButtons.Add(listOrganizacionButton);

            var rubroButtons = new ObservableCollection<MenuButton>();

            rubroButtons.Add(MenuButton.Copy(newOrganizacionButton, () => { return OpenRubroCreationForm(); }));
            rubroButtons.Add(MenuButton.Copy(listOrganizacionButton, () => { return OpenRubroList(); }));

            var sectorButtons = new ObservableCollection<MenuButton>();

            sectorButtons.Add(MenuButton.Copy(newOrganizacionButton, () => { return OpenSectorCreationForm(); }));
            sectorButtons.Add(MenuButton.Copy(listOrganizacionButton, () => { return OpenSectorList(); }));

            var tipoOrganizacionButtons = new ObservableCollection<MenuButton>();

            tipoOrganizacionButtons.Add(MenuButton.Copy(newOrganizacionButton, () => { return OpenTipoOrganizacionCreationForm(); }));
            tipoOrganizacionButtons.Add(MenuButton.Copy(listOrganizacionButton, () => { return OpenTipoOrganizacionList(); }));

            var localidadButtons = new ObservableCollection<MenuButton>();

            localidadButtons.Add(MenuButton.Copy(newOrganizacionButton, () => { return OpenLocalidadCreationForm(); }));
            localidadButtons.Add(MenuButton.Copy(listOrganizacionButton, () => { return OpenLocalidadList(); }));

            CastedDataContext.OrganizacionMenuContainer = new MenuContainer()
            {
                Buttons = organizacionButtons,
                HeaderName = "Organizaciones",
                IsMenuOpened = false
            };

            CastedDataContext.RubroMenuContainer = new MenuContainer()
            {
                Buttons = rubroButtons,
                HeaderName = "Rubros",
                IsMenuOpened = false
            };

            CastedDataContext.SectorMenuContainer = new MenuContainer()
            {
                Buttons = sectorButtons,
                HeaderName = "Sectores",
                IsMenuOpened = false
            };

            CastedDataContext.TipoOrganizacionMenuContainer = new MenuContainer()
            {
                Buttons = tipoOrganizacionButtons,
                HeaderName = "Tipo Organizaciones",
                IsMenuOpened = false
            };

            CastedDataContext.LocalidadMenuContainer = new MenuContainer()
            {
                Buttons = localidadButtons,
                HeaderName = "Localidades",
                IsMenuOpened = false
            };
        }

        public async Task<string> GetMapUrl(IEnumerable<OrganizacionHeaderData> organizaciones)
        {
            return await Task.Run(() =>
            {
                var mapManager = new MapManager();

                foreach (var organizacion in (organizaciones))
                {
                    mapManager.addDireccion(organizacion.Direccion, organizacion.Nombre);
                }

                return mapManager.getHtmlString();
            });
        }

        private async Task<bool> UpdateMap()
        {
            var url = await GetMapUrl(CastedDataContext.Organizaciones);

            Browser.NavigateToString(url);

            return true;
        }

        private async Task<bool> UpdateUI()
        {
            await CastedDataContext.UpdateOrganizacionHeaders();

            return await UpdateMap();
        }

        private async void AddOrganizacionToMap(OrganizacionHeaderData organizacion)
        {
            CastedDataContext.Organizaciones.Add(organizacion);

            var url = await GetMapUrl(CastedDataContext.Organizaciones);

            Browser.NavigateToString(url);
        }

        private bool OpenOrganizacionCreationForm(int? organizacionId = null)
        {
            var form = new OrganizacionCreationEditionForm(organizacionId);

            if (form.ShowDialog().Value)
            {
                AddOrganizacionToMap(form.GetResult());
            }

            return true;
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

        private bool OpenOrganizacionList()
        {
            var form = new OrganizacionList();

            form.ShowDialog();

            return true;
        }

        private bool OpenRubroList()
        {
            var form = new RubroList();

            form.ShowDialog();

            return true;
        }

        private bool OpenSectorList()
        {
            var form = new SectorList();

            form.ShowDialog();

            return true;
        }

        private bool OpenTipoOrganizacionList()
        {
            var form = new TipoOrganizacionList();

            form.ShowDialog();

            return true;
        }

        private bool OpenLocalidadList()
        {
            var form = new LocalidadList();

            form.ShowDialog();

            return true;
        }

        private async void SectorFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var update = CastedDataContext.ApplySectorFilter();

            if (update.HasValue && update.Value)
            {
                await UpdateUI();
            }
        }

        private async void RubroFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var update = CastedDataContext.ApplyRubroFilter();

            if (update.HasValue && update.Value)
            {
                await UpdateUI();
            }
        }

        private async void TipoOrganizacionFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var update = CastedDataContext.ApplyTipoOrganizacionFilter();

            if (update.HasValue && update.Value)
            {
                await UpdateUI();
            }
        }

        private async void LocalidadFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var update = CastedDataContext.ApplyLocalidadFilter();

            if (update.HasValue && update.Value)
            {
                await UpdateUI();
            }
        }

        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CastedDataContext.InitializationTask != null && CastedDataContext.InitializationTask.IsCompleted)
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
    }
}
