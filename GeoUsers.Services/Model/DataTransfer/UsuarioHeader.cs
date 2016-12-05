namespace GeoUsers.Services.Model.DataTransfer
{
    public class OrganizacionHeader : BaseNotifierEntity
    {
        protected long id { get; set; }
        protected string nombre { get; set; }
        protected string direccion { get; set; }
        protected string email { get; set; }
        protected string localidad { get; set; }

        public long Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }

        public string Direccion
        {
            get { return direccion; }
            set
            {
                direccion = value;
                OnPropertyChanged(nameof(Direccion));
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
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
    }
}
