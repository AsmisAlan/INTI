namespace GeoUsers.Services.Model.DataTransfer
{
    public class TipoOrganizacionCreationData : BaseNotifierEntity
    {
        private string tipo { get; set; }

        public string Tipo
        {
            get
            {
                return tipo;
            }
            set
            {
                tipo = value;

                OnPropertyChanged(nameof(Tipo));
            }
        }
    }
}
