namespace GeoUsers.Services.Model.DataTransfer
{
    public class OrganizacionPersonalData : OrganizacionHeaderData
    {
        protected string contactoCargo { get; set; }

        protected virtual int? personal { get; set; }

        protected virtual string web { get; set; }

        protected virtual long? cuit { get; set; }

        protected virtual bool usuarioInti { get; set; }

        public string ContactoCargo
        {
            get { return contactoCargo; }
            set
            {
                contactoCargo = value;
                OnPropertyChanged(nameof(ContactoCargo));
            }
        }

        public virtual int? Personal
        {
            get { return personal; }
            set
            {
                personal = value;
                OnPropertyChanged(nameof(Personal));
            }
        }

        public virtual string Web
        {
            get { return web; }
            set
            {
                web = value;
                OnPropertyChanged(nameof(Web));
            }
        }

        public virtual long? Cuit
        {
            get { return cuit; }
            set
            {
                cuit = value;
                OnPropertyChanged(nameof(Cuit));
            }
        }

        public virtual bool UsuarioInti
        {
            get { return usuarioInti; }
            set
            {
                usuarioInti = value;
                OnPropertyChanged(nameof(UsuarioInti));
            }
        }
    }
}
