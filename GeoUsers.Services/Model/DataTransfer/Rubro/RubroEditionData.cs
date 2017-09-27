namespace GeoUsers.Services.Model.DataTransfer
{
    public class RubroEditionData : BaseNotifierEntity
    {
        protected string nombre { get; set; }

        protected int? sectorId { get; set; }

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;

                OnPropertyChanged(nameof(Nombre));
            }
        }

        public int? SectorId
        {
            get
            {
                return sectorId;
            }
            set
            {
                sectorId = value;

                OnPropertyChanged(nameof(SectorId));
            }
        }

        public int? Id { get; set; }
    }
}
