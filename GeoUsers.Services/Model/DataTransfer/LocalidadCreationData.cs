namespace GeoUsers.Services.Model.DataTransfer
{
    public class LocalidadCreationData : BaseNotifierEntity
    {
        private string nombre { get; set; }

        private int? codigoPostal { get; set; }

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

        public int? CodigoPostal
        {
            get
            {
                return codigoPostal;
            }
            set
            {
                codigoPostal = value;

                OnPropertyChanged(nameof(CodigoPostal));
            }
        }
    }
}
