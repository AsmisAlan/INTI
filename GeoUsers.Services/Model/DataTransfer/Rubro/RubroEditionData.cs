namespace GeoUsers.Services.Model.DataTransfer
{
    public class RubroEditionData : RubroCreationData
    {
        private int id { get; set; }

        public int Id
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
