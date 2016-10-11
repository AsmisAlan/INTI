using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class MainViewModel
    {
        readonly UsuarioLogic usuarioLogic;

        public ObservableCollection<UsuarioHeader> Usuarios { get; set; }

        public Task<IEnumerable<UsuarioHeader>> InitializationTask { get; private set; }

        public bool Loading { get; set; }

        public MainViewModel()
        {
        }

        public MainViewModel(UsuarioLogic usuarioLogic)
        {
            this.usuarioLogic = usuarioLogic;

            InitializationTask = executeDataFunction();
        }

        private async Task<IEnumerable<UsuarioHeader>> executeDataFunction()
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
