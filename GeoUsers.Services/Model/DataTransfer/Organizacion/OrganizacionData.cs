namespace GeoUsers.Services.Model.DataTransfer
{
    public class OrganizacionData : OrganizacionBaseData
    {
        protected string tipoOrganizacion { get; set; }

        protected string localidad { get; set; }

        protected string rubro { get; set; }

        protected string contactoCargo { get; set; }

        protected int? personal { get; set; }

        protected string web { get; set; }

        protected long? cuit { get; set; }

        protected string direccion { get; set; }

        protected bool isActive { get; set; }

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

        public string ContactoCargo
        {
            get { return contactoCargo; }
            set
            {
                contactoCargo = value;
                OnPropertyChanged(nameof(ContactoCargo));
            }
        }

        public virtual int? Personal
        {
            get { return personal; }
            set
            {
                personal = value;
                OnPropertyChanged(nameof(Personal));
            }
        }

        public string Web
        {
            get { return web; }
            set
            {
                web = value;
                OnPropertyChanged(nameof(Web));
            }
        }

        public long? Cuit
        {
            get { return cuit; }
            set
            {
                cuit = value;
                OnPropertyChanged(nameof(Cuit));
            }
        }

        public string TipoOrganizacion
        {
            get { return tipoOrganizacion; }
            set
            {
                tipoOrganizacion = value;
                OnPropertyChanged(nameof(TipoOrganizacion));
            }
        }

        public string Localidad
        {
            get { return localidad; }
            set
            {
                localidad = value;
                OnPropertyChanged(nameof(Localidad));
            }
        }

        public string Rubro
        {
            get { return rubro; }
            set
            {
                rubro = value;
                OnPropertyChanged(nameof(Rubro));
            }
        }
    }
}
