using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class LoginFormViewModel : BaseNotifierEntity
    {
        private readonly UsuarioLogic usuarioLogic;

        private string loginId;

        private bool loading;

        public bool Loading
        {
            get
            {
                return loading;
            }

            set
            {
                loading = value;

                OnPropertyChanged(nameof(Loading));
            }
        }

        public string LoginId
        {
            get
            {
                return loginId;
            }

            set
            {
                loginId = value;

                OnPropertyChanged(nameof(LoginId));
            }
        }

        public LoginFormViewModel(UsuarioLogic usuarioLogic)
        {
            this.usuarioLogic = usuarioLogic;
        }

        public async Task<bool> LogIn(string password)
        {
            Loading = true;

            var success = false;

            await RequestService.Execute(() =>
            {
                success = usuarioLogic.LogIn(LoginId, password);
            });

            Loading = false;

            return success;
        }
    }
}
