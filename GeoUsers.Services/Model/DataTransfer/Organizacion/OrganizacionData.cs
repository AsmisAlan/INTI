namespace GeoUsers.Services.Model.DataTransfer
{
    public class OrganizacionData : OrganizacionPersonalData
    {
        protected string tipoOrganizacion { get; set; }

        protected string localidad { get; set; }

        protected string rubro { get; set; }

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
