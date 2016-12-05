namespace GeoUsers.Services.Model.DataTransfer
{
    public class TipoOrganizacionEditionData : TipoOrganizacionCreationData
    {
        private long id { get; set; }

        public long Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;

                OnPropertyChanged(nameof(Id));
            }
        }
    }
}
