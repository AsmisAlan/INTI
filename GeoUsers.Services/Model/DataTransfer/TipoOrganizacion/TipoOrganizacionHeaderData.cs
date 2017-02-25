namespace GeoUsers.Services.Model.DataTransfer
{
    public class TipoOrganizacionHeaderData : BaseComparableNotifierEntity
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
