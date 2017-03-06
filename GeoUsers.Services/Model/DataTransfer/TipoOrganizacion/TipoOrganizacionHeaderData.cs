namespace GeoUsers.Services.Model.DataTransfer
{
    public class TipoOrganizacionHeaderData : BaseComparableNotifierEntity
    {
        private string tipo { get; set; }

        protected bool isActive { get; set; }

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

        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;

                OnPropertyChanged(nameof(IsActive));
            }
        }
    }
}
