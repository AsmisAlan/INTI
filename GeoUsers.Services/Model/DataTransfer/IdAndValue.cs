namespace GeoUsers.Services.Model.DataTransfer
{
    public class IdAndValue : BaseComparableNotifierEntity
    {
        private string value { get; set; }

        public string Value
        {
            get { return value; }

            set
            {
                this.value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
