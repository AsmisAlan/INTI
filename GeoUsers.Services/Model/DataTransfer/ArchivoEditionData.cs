namespace GeoUsers.Services.Model.DataTransfer
{
    public class ArchivoEditionData : BaseNotifierEntity
    {
        private string nombre;

        private string ruta;

        public int? Id { get; set; }

        public string Ruta
        {
            get
            {
                return ruta;
            }
            set
            {
                ruta = value;

                OnPropertyChanged(nameof(Ruta));
            }
        }

        public string Extension { get; set; }

        public byte[] Data { get; set; }

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
