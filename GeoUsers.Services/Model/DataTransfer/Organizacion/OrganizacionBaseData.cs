namespace GeoUsers.Services.Model.DataTransfer
{
    public class OrganizacionBaseData : BaseComparableNotifierEntity
    {
        protected string nombre { get; set; }
        protected string telefono { get; set; }
        protected string email { get; set; }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged(nameof(Nombre));
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

        public string Telefono
        {
            get { return telefono; }
            set
            {
                telefono = value;
                OnPropertyChanged(nameof(Telefono));
            }
        }
    }
}
