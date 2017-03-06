namespace GeoUsers.Services.Model.DataTransfer
{
    public class LocalidadHeaderData : BaseComparableNotifierEntity
    {
        private string nombre { get; set; }

        private string codigoPostal { get; set; }

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

        public string CodigoPostal
        {
            get
            {
                return codigoPostal;
            }
            set
            {
                codigoPostal = value;

                OnPropertyChanged(nameof(CodigoPostal));
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
