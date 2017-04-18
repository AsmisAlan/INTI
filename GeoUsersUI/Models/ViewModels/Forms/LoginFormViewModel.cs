using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class LoginFormViewModel : BaseNotifierEntity
    {
        private readonly UsuarioLogic usuarioLogic;

        private string loginId;

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
            var success = false;

            await RequestService.Execute(() =>
            {
                success = usuarioLogic.LogIn(LoginId, password);
            });

            return success;
        }
    }
}
