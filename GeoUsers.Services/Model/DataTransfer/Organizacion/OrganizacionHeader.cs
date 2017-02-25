namespace GeoUsers.Services.Model.DataTransfer
{
    public class OrganizacionHeaderData : OrganizacionBaseData
    {
        protected string direccion { get; set; }

        public string Direccion
        {
            get { return direccion; }
            set
            {
                direccion = value;
                OnPropertyChanged(nameof(Direccion));
            }
        }
    }
}
