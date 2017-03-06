namespace GeoUsers.Services.Model.Entities
{
    public class Organizacion : BaseEntity
    {
        public virtual string Nombre { get; set; }

        public virtual string Direccion { get; set; }

        public virtual string Telefono { get; set; }

        public virtual string ContactoCargo { get; set; }

        public virtual int Personal { get; set; }

        public virtual string Email { get; set; }

        public virtual string Web { get; set; }

        public virtual long? Cuit { get; set; }

        public virtual bool UsuarioInti { get; set; }

        public virtual string Latitud { get; set; }

        public virtual string Longitud { get; set; }

        public virtual Localidad Localidad { get; set; }

        public virtual TipoOrganizacion TipoOrganizacion { get; set; }

        public virtual Rubro Rubro { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
