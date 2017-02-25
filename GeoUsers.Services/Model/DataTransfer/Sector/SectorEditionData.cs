namespace GeoUsers.Services.Model.DataTransfer
{
    public class SectorEditionData : BaseNotifierEntity
    {
        public int? Id { get; set; }

        private string nombre { get; set; }

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
    }
}
