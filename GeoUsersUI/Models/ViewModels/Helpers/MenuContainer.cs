using GeoUsersUI.Models.ViewModels.UserControls;
using System.Collections.Generic;

namespace GeoUsersUI.Models.ViewModels
{
    public class MenuContainer
    {
        public string HeaderName { get; set; }

        public IEnumerable<MenuButton> Buttons { get; set; }
    }
}
