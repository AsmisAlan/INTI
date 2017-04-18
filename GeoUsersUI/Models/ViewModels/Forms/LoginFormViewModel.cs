using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    public class LoginFormViewModel : BaseNotifierEntity
    {
        private string loginId;

        private string password;

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

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;

                OnPropertyChanged(nameof(Password));
            }
        }

        //public LoginFormViewModel()
    }
}
