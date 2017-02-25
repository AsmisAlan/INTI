using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Models.ViewModels.UserControls;
using System.Collections.ObjectModel;

namespace GeoUsersUI.Models.ViewModels
{
    public class MenuContainer : BaseNotifierEntity
    {
        private string headerName { get; set; }

        private ObservableCollection<MenuButton> buttons { get; set; }

        private bool isMenuOpened { get; set; }

        public string HeaderName
        {
            get
            {
                return headerName;
            }
            set
            {
                headerName = value;

                OnPropertyChanged(nameof(HeaderName));
            }
        }

        public ObservableCollection<MenuButton> Buttons
        {
            get
            {
                return buttons;
            }
            set
            {
                buttons = value;

                OnPropertyChanged(nameof(Buttons));
            }
        }

        public bool IsMenuOpened
        {
            get
            {
                return isMenuOpened;
            }
            set
            {
                isMenuOpened = value;

                OnPropertyChanged(nameof(IsMenuOpened));
            }
        }
    }
}
