using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels.UserControls
{
    public class SmartSelectItem : IdAndValue
    {
        private bool isActive { get; set; }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;

                OnPropertyChanged(nameof(IsActive));
            }
        }

        public SmartSelectItem Copy()
        {
            return new SmartSelectItem()
            {
                Id = Id,
                IsActive = IsActive,
                Value = Value
            };
        }
    }
}
