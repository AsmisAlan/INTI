using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Models.ViewModels.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class MainViewModel
    {
        readonly UsuarioLogic usuarioLogic;

        public ObservableCollection<UsuarioHeader> Usuarios { get; set; }

        public Task<IEnumerable<UsuarioHeader>> InitializationTask { get; private set; }

        public MainMenuContainer MainMenu { get; set; }

        public string Header { get; set; }

        public bool Loading { get; set; }

        public MainViewModel()
        {
        }

        public MainViewModel(UsuarioLogic usuarioLogic)
        {
            this.usuarioLogic = usuarioLogic;
            Header = "TEST";
            InitializationTask = ExecuteDataFunction();
        }

        private async Task<IEnumerable<UsuarioHeader>> ExecuteDataFunction()
        {
            Loading = true;

            var results = await Task.Run(() =>
            {
                using (var sessionBlock = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    return usuarioLogic.GetUsuarioHeaders();
                }
            });

            Usuarios = new ObservableCollection<UsuarioHeader>(results);

            Loading = false;

            return results;
        }


    }
}
