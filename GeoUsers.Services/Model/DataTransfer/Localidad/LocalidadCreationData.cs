namespace GeoUsers.Services.Model.DataTransfer
{
    public class LocalidadEditionData : BaseNotifierEntity
    {
        private string nombre { get; set; }

        private int? codigoPostal { get; set; }

        public int? Id { get; set; }

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
