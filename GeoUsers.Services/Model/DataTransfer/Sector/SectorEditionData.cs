namespace GeoUsers.Services.Model.DataTransfer
{
    public class SectorEditionData : BaseNotifierEntity
    {
        private string nombre;

        private ArchivoEditionData icono { get; set; }

        public int? Id { get; set; }

        public ArchivoEditionData Icono
        {
            get
            {
                return icono;
            }
            set
            {
                icono = value;

                OnPropertyChanged(nameof(Icono));
            }
        }

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
