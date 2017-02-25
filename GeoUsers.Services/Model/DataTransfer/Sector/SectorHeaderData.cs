using System.Collections.ObjectModel;

namespace GeoUsers.Services.Model.DataTransfer
{
    public class SectorHeaderData : BaseComparableNotifierEntity
    {
        private string nombre { get; set; }

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
    }
}
