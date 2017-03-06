using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels
{
    public class BaseWindowViewModel : BaseNotifierEntity
    {
        protected string windowTitle { get; set; }

        public string WindowTitle
        {
            get
            {
                return windowTitle;
            }
            set
            {
                windowTitle = value;

                OnPropertyChanged(nameof(WindowTitle));
            }
        }
    }
}
