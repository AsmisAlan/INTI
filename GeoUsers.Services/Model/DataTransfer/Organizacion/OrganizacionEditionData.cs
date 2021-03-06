﻿namespace GeoUsers.Services.Model.DataTransfer
{
    public class OrganizacionEditionData : BaseNotifierEntity
    {
        protected int? localidadId { get; set; }

        protected int? tipoOrganizacionId { get; set; }

        protected int? rubroId { get; set; }

        protected string direccion { get; set; }

        protected string contactoCargo { get; set; }

        protected int? personal { get; set; }

        protected string web { get; set; }

        protected long? cuit { get; set; }

        protected bool usuarioInti { get; set; }

        protected string nombre { get; set; }

        protected string telefono { get; set; }

        protected string email { get; set; }

        protected string latitud { get; set; }

        protected string longitud { get; set; }

        protected bool autoDetectCoordinates;

        public int? Id { get; set; }

        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Telefono
        {
            get { return telefono; }
            set
            {
                telefono = value;
                OnPropertyChanged(nameof(Telefono));
            }
        }

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

        public string Direccion
        {
            get { return direccion; }
            set
            {
                direccion = value;
                OnPropertyChanged(nameof(Direccion));
            }
        }

        public int? LocalidadId
        {
            get { return localidadId; }
            set
            {
                localidadId = value;
                OnPropertyChanged(nameof(LocalidadId));
            }
        }

        public int? TipoOrganizacionId
        {
            get { return tipoOrganizacionId; }
            set
            {
                tipoOrganizacionId = value;
                OnPropertyChanged(nameof(TipoOrganizacionId));
            }
        }

        public int? RubroId
        {
            get { return rubroId; }
            set
            {
                rubroId = value;
                OnPropertyChanged(nameof(RubroId));
            }
        }

        public string Latitud
        {
            get
            {
                return latitud;
            }
            set
            {
                latitud = value;

                OnPropertyChanged(nameof(Latitud));
            }
        }

        public string Longitud
        {
            get
            {
                return longitud;
            }
            set
            {
                longitud = value;

                OnPropertyChanged(nameof(Longitud));
            }
        }

        public bool AutoDetectCoordinates
        {
            get
            {
                return autoDetectCoordinates;
            }
            set
            {
                autoDetectCoordinates = value;

                OnPropertyChanged(nameof(AutoDetectCoordinates));
            }
        }
    }
}
