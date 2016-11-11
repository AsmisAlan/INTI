using GeoUsers.Services.Model.DataTransfer;
using System;
using System.Windows;

namespace GeoUsersUI.Models.ViewModels.UserControls
{
    public class MenuButton : BaseNotifierEntity
    {
        protected Visibility buttonVisibility { get; set; }

        protected string name { get; set; }

        protected Func<bool> onButtonClickAction { get; set; }

        public Func<bool> OnButtonClickAction
        {
            get { return onButtonClickAction; }
            set
            {
                onButtonClickAction = value;
                OnPropertyChanged(nameof(onButtonClickAction));
            }
        }

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

        public static MenuButton Copy(MenuButton button, Func<bool> newButtonAction)
        {
            return new MenuButton()
            {
                ButtonVisibility = button.ButtonVisibility,
                Name = button.Name,
                OnButtonClickAction = newButtonAction
            };
        }
    }
}
