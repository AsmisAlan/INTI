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

            DataGrid.ItemsSource = new ObservableCollection<OrganizacionHeader>();

            var loadingFinished = Initialize();
        }

        public async Task<bool> Initialize()
        {
            InitializeMainMenu();

            //var organizacions = await CastedDataContext.InitializationTask;

            //DataGrid.ItemsSource = CastedDataContext.Organizacions;

            //var url = await GetMapUrl(organizacions);

            //Browser.NavigateToString(url);

            return true;
        }

        private void InitializeMainMenu()
        {
            var newOrganizacionButton = new MenuButton()
            {
                Name = "Nuevo",
                OnButtonClickAction = () => { return OpenOrganizacionCreationForm(); }
            };

            var editOrganizacionButton = new MenuButton()
            {
                Name = "Editar",
                OnButtonClickAction = () => { return OpenOrganizacionCreationForm(); }
            };

            var deleteOrganizacionButton = new MenuButton()
            {
                Name = "Eliminar",
                OnButtonClickAction = () => { return OpenOrganizacionCreationForm(); }
            };

            var organizacionButtons = new ObservableCollection<MenuButton>();

            organizacionButtons.Add(newOrganizacionButton);
            organizacionButtons.Add(editOrganizacionButton);
            organizacionButtons.Add(deleteOrganizacionButton);

            var rubroButtons = new ObservableCollection<MenuButton>();

            rubroButtons.Add(MenuButton.Copy(newOrganizacionButton, () => { return OpenRubroCreationForm(); }));
            rubroButtons.Add(MenuButton.Copy(editOrganizacionButton, () => { return OpenRubroCreationForm(); }));
            rubroButtons.Add(MenuButton.Copy(deleteOrganizacionButton, () => { return OpenRubroDeletionForm(); }));

            var sectorButtons = new ObservableCollection<MenuButton>();

            sectorButtons.Add(MenuButton.Copy(newOrganizacionButton, () => { return OpenSectorCreationForm(); }));
            sectorButtons.Add(MenuButton.Copy(editOrganizacionButton, () => { return OpenSectorCreationForm(); }));
            sectorButtons.Add(MenuButton.Copy(deleteOrganizacionButton, () => { return OpenSectorDeletionForm(); }));

            var tipoOrganizacionButtons = new ObservableCollection<MenuButton>();

            tipoOrganizacionButtons.Add(MenuButton.Copy(newOrganizacionButton, () => { return OpenOrganizacionCreationForm(); }));
            tipoOrganizacionButtons.Add(MenuButton.Copy(editOrganizacionButton, () => { return OpenOrganizacionCreationForm(); }));
            tipoOrganizacionButtons.Add(MenuButton.Copy(deleteOrganizacionButton, () => { return OpenOrganizacionDeletionForm(); }));

            var localidadButtons = new ObservableCollection<MenuButton>();

            localidadButtons.Add(MenuButton.Copy(newOrganizacionButton, () => { return OpenLocalidadCreationForm(); }));
            localidadButtons.Add(MenuButton.Copy(editOrganizacionButton, () => { return OpenLocalidadCreationForm(); }));
            localidadButtons.Add(MenuButton.Copy(deleteOrganizacionButton, () => { return OpenLocalidadDeletionForm(); }));

            OrganizacionMenu.HeaderName = "Organizaciones";
            OrganizacionMenu.Buttons = organizacionButtons;
            OrganizacionMenu.ToggleButtonsVisibility();

            RubroMenu.HeaderName = "Rubros";
            RubroMenu.Buttons = rubroButtons;
            RubroMenu.ToggleButtonsVisibility();

            SectorMenu.HeaderName = "Sectores";
            SectorMenu.Buttons = sectorButtons;
            SectorMenu.ToggleButtonsVisibility();

            TipoOrganizacionMenu.HeaderName = "Tipo Organizaciones";
            TipoOrganizacionMenu.Buttons = tipoOrganizacionButtons;
            TipoOrganizacionMenu.ToggleButtonsVisibility();

            LocalidadMenu.HeaderName = "Localidades";
            LocalidadMenu.Buttons = localidadButtons;
            LocalidadMenu.ToggleButtonsVisibility();
        }

        public async Task<string> GetMapUrl(IEnumerable<OrganizacionHeader> organizacions)
        {
            return await Task.Run(() =>
            {
                var mapManager = new MapManager();

                foreach (var organizacion in (organizacions))
                {
                    mapManager.addDireccion($"{organizacion.Direccion} {organizacion.Localidad}", organizacion.Nombre);
                }

                return mapManager.getHtmlString();
            });
        }

        private async void UpdateMap(OrganizacionHeader organizacion)
        {
            CastedDataContext.Organizaciones.Add(organizacion);

            var url = await GetMapUrl(CastedDataContext.Organizaciones);

            Browser.NavigateToString(url);
        }

        private bool OpenOrganizacionCreationForm(int? organizacionId = null)
        {
            var form = new OrganizacionCreationEditionForm();

            if (form.ShowDialog().Value)
            {
                UpdateMap(form.GetResult());
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

        private bool OpenOrganizacionDeletionForm(int? organizacionId = null)
        {

            //if (form.ShowDialog().Value)
            //{
            //    UpdateMap(form.GetResult());
            //}

            return true;
        }

        private bool OpenRubroDeletionForm(int? rubroId = null)
        {
            var form = new RubroCreationEditionForm();

            form.ShowDialog();

            return true;
        }

        private bool OpenTipoOrganizacionDeletionForm(int? organizacionId = null)
        {
            //var form = new TipoOrganizacionCreationEditionForm();

            //form.ShowDialog();

            return true;
        }

        private bool OpenSectorDeletionForm(int? sectorId = null)
        {
            var form = new SectorCreationEditionForm();

            form.ShowDialog();

            return true;
        }

        private bool OpenLocalidadDeletionForm(int? localidadId = null)
        {
            var form = new LocalidadCreationEditionForm();

            form.ShowDialog();

            return true;
        }
    }
}
