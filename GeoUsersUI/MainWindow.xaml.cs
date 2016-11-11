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

            DataGrid.ItemsSource = new ObservableCollection<UsuarioHeader>();

            var loadingFinished = Initialize();
        }

        public async Task<bool> Initialize()
        {
            InitializeMainMenu();

            var usuarios = await CastedDataContext.InitializationTask;

            DataGrid.ItemsSource = CastedDataContext.Usuarios;

            var url = await GetMapUrl(usuarios);

            Browser.NavigateToString(url);

            return true;
        }

        private void InitializeMainMenu()
        {
            var newUsuarioButton = new MenuButton()
            {
                Name = "Nuevo",
                OnButtonClickAction = () => { return OpenUsuarioCreationForm(); }
            };

            var editUsuarioButton = new MenuButton()
            {
                Name = "Editar",
                OnButtonClickAction = () => { return OpenUsuarioCreationForm(); }
            };

            var deleteUsuarioButton = new MenuButton()
            {
                Name = "Eliminar",
                OnButtonClickAction = () => { return OpenUsuarioCreationForm(); }
            };

            var usuarioButtons = new ObservableCollection<MenuButton>();

            usuarioButtons.Add(newUsuarioButton);
            usuarioButtons.Add(editUsuarioButton);
            usuarioButtons.Add(deleteUsuarioButton);

            var rubroButtons = new ObservableCollection<MenuButton>();

            rubroButtons.Add(MenuButton.Copy(newUsuarioButton, () => { return OpenRubroCreationForm(); }));
            rubroButtons.Add(MenuButton.Copy(editUsuarioButton, () => { return OpenRubroCreationForm(); }));
            rubroButtons.Add(MenuButton.Copy(deleteUsuarioButton, () => { return OpenRubroDeletionForm(); }));

            var sectorButtons = new ObservableCollection<MenuButton>();

            sectorButtons.Add(MenuButton.Copy(newUsuarioButton, () => { return OpenSectorCreationForm(); }));
            sectorButtons.Add(MenuButton.Copy(editUsuarioButton, () => { return OpenSectorCreationForm(); }));
            sectorButtons.Add(MenuButton.Copy(deleteUsuarioButton, () => { return OpenSectorDeletionForm(); }));

            var organizacionButtons = new ObservableCollection<MenuButton>();

            organizacionButtons.Add(MenuButton.Copy(newUsuarioButton, () => { return OpenOrganizacionCreationForm(); }));
            organizacionButtons.Add(MenuButton.Copy(editUsuarioButton, () => { return OpenOrganizacionCreationForm(); }));
            organizacionButtons.Add(MenuButton.Copy(deleteUsuarioButton, () => { return OpenOrganizacionDeletionForm(); }));

            var localidadButtons = new ObservableCollection<MenuButton>();

            localidadButtons.Add(MenuButton.Copy(newUsuarioButton, () => { return OpenLocalidadCreationForm(); }));
            localidadButtons.Add(MenuButton.Copy(editUsuarioButton, () => { return OpenLocalidadCreationForm(); }));
            localidadButtons.Add(MenuButton.Copy(deleteUsuarioButton, () => { return OpenLocalidadDeletionForm(); }));

            UsuarioMenu.HeaderName = "Usuarios";
            UsuarioMenu.Buttons = usuarioButtons;
            RubroMenu.HeaderName = "Rubros";
            RubroMenu.Buttons = rubroButtons;
            SectorMenu.HeaderName = "Sectores";
            SectorMenu.Buttons = sectorButtons;
            OrganizacionMenu.HeaderName = "Organizaciones";
            OrganizacionMenu.Buttons = organizacionButtons;
            LocalidadMenu.HeaderName = "Localidades";
            LocalidadMenu.Buttons = localidadButtons;
        }

        public async Task<string> GetMapUrl(IEnumerable<UsuarioHeader> usuarios)
        {
            return await Task.Run(() =>
            {
                var mapManager = new MapManager();

                foreach (var usuario in (usuarios))
                {
                    mapManager.addDireccion($"{usuario.Direccion} {usuario.Localidad}", usuario.Nombre);
                }

                return mapManager.getHtmlString();
            });
        }

        private async void UpdateMap(UsuarioHeader usuario)
        {
            ((MainViewModel)DataContext).Usuarios.Add(usuario);

            var url = await GetMapUrl(((MainViewModel)DataContext).Usuarios);

            Browser.NavigateToString(url);
        }

        private bool OpenUsuarioCreationForm(int? usuarioId = null)
        {
            var form = new UsuarioCreationEditionForm();

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

        private bool OpenOrganizacionCreationForm(int? rubroId = null)
        {
            var form = new OrganizacionCreationEditionForm();

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

        private bool OpenUsuarioDeletionForm(int? usuarioId = null)
        {
            var form = new UsuarioCreationEditionForm();

            if (form.ShowDialog().Value)
            {
                UpdateMap(form.GetResult());
            }

            return true;
        }

        private bool OpenRubroDeletionForm(int? rubroId = null)
        {
            var form = new RubroCreationEditionForm();

            form.ShowDialog();

            return true;
        }

        private bool OpenOrganizacionDeletionForm(int? rubroId = null)
        {
            var form = new OrganizacionCreationEditionForm();

            form.ShowDialog();

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
