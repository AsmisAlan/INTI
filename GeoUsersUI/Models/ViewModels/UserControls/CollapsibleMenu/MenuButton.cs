using GeoUsers.Services.Model.DataTransfer;
using System;
using System.Windows;

namespace GeoUsersUI.Models.ViewModels.UserControls
{
    public class MenuButton : BaseNotifierEntity
    {
        protected Visibility buttonVisibility { get; set; }

        protected string name { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public Visibility ButtonVisibility
        {
            get { return buttonVisibility; }
            set
            {
                buttonVisibility = value;
                OnPropertyChanged(nameof(ButtonVisibility));
            }
        }

        public event EventHandler ButtonClick;

        public void OnButtonClick(EventArgs e)
        {
            ButtonClick?.Invoke(this, e);
        }

        public static MenuButton Copy(MenuButton button)
        {
            return new MenuButton()
            {
                ButtonVisibility = button.ButtonVisibility,
                Name = button.Name
            };
        }
    }
}
