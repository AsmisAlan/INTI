using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GeoUsersUI.Models.ViewModels.UserControls
{
    public class CollapsibleMenuViewModel
    {
        public string HeaderName { get; set; }

        public bool IsMenuOpened { get; set; }

        public ObservableCollection<MenuButton> Buttons { get; set; }

        public CollapsibleMenuViewModel() { }

        public CollapsibleMenuViewModel(string headerName,
                                        IEnumerable<MenuButton> buttons,
                                        bool isMenuOpened)
        {
            HeaderName = headerName;
            Buttons = new ObservableCollection<MenuButton>(buttons);
            IsMenuOpened = isMenuOpened;
        }
    }
}
