namespace GeoUsers.Services.Model.DataTransfer
{
    public class OrganizacionHeaderData : OrganizacionBaseData
    {
        protected string direccion { get; set; }

        protected bool isActive { get; set; }

        protected string latitud { get; set; }

        protected string longitud { get; set; }

        public string Direccion
        {
            get
            {
                return direccion;
            }
            set
            {
                direccion = value;
                OnPropertyChanged(nameof(Direccion));
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

        public string Latitud
        {
            get
            {
                return latitud;
            }
            set
            {
                latitud = value;
                OnPropertyChanged(nameof(Latitud));
            }
        }

        public string Longitud
        {
            get
            {
                return longitud;
            }
            set
            {
                longitud = value;
                OnPropertyChanged(nameof(Longitud));
            }
        }
    }
}
