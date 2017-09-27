namespace GeoUsers.Services.Model.DataTransfer
{
    public class TipoOrganizacionEditionData : BaseNotifierEntity
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

        public int? Id { get; set; }
    }
}
