namespace GeoUsers.Services.Model.Entities
{
    public class Usuario : BaseEntity
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

        public virtual Localidad Localidad { get; set; }

        public virtual Organizacion Organizacion { get; set; }

        public virtual Rubro Rubro { get; set; }
    }
}
