using System.Collections.ObjectModel;

namespace GeoUsers.Services.Model.DataTransfer
{
    public class SectorHeaderData : BaseComparableNotifierEntity
    {
        private string nombre { get; set; }

        protected bool isActive { get; set; }

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;

                OnPropertyChanged(nameof(Nombre));
            }
        }

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
    }
}
