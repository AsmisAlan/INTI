namespace TestApplication
{
    public class User : BaseNotifierEntity
    {
        protected long phone { get; set; }
        protected string web { get; set; }

        public long Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Web
        {
            get { return web; }
            set
            {
                web = value;
                OnPropertyChanged(nameof(Web));
            }
        }
    }
}
