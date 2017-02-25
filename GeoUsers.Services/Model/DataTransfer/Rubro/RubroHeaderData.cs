namespace GeoUsers.Services.Model.DataTransfer
{
    public class RubroHeaderData : BaseComparableNotifierEntity
    {
        protected string nombre { get; set; }

        protected string sector { get; set; }

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

        public string Sector
        {
            get
            {
                return sector;
            }
            set
            {
                sector = value;

                OnPropertyChanged(nameof(Sector));
            }
        }
    }
}
