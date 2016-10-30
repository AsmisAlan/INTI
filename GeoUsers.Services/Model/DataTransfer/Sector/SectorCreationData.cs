namespace GeoUsers.Services.Model.DataTransfer
{
    public class SectorCreationData : BaseNotifierEntity
    {
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
