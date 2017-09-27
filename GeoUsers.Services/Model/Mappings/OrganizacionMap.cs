using GeoUsers.Services.Model.Entities;
using NHibernate.Mapping.ByCode;

namespace GeoUsers.Services.Model.Mappings
{
    public class OrganizacionMap : BaseClassMapping<Organizacion>
    {
        public OrganizacionMap()
        {
            Table("ORGANIZACION");

            Id(x => x.Id, x =>
                {
                    x.Column("IDORGANIZACION");
                    x.Generator(new IdentityGeneratorDef());
                    x.UnsavedValue(0);
                });

            Property(x => x.Nombre, "NOMBRE");
            Property(x => x.Personal, "PERSONAL");
            Property(x => x.Telefono, "TELEFONO");
            Property(x => x.Web, "WEB");
            Property(x => x.UsuarioInti, "USUARIOINTI");
            Property(x => x.ContactoCargo, "CONTACTOCARGO");
            Property(x => x.Cuit, "CUIT");
            Property(x => x.Direccion, "DIRECCION");
            Property(x => x.Email, "EMAIL");
            Property(x => x.Latitud, "LATITUD");
            Property(x => x.Longitud, "LONGITUD");

            ManyToOne(x => x.Localidad, map =>
            {
                map.Column("IDLOCALIDAD");
            });

            ManyToOne(x => x.TipoOrganizacion, map =>
            {
                map.Column("IDTIPOORGANIZACION");
            });

            ManyToOne(x => x.Rubro, map =>
            {
                map.Column("IDRUBRO");
            });
        }
    }
}
