using GeoUsers.Services.Model.Entities;
using NHibernate.Mapping.ByCode;

namespace GeoUsers.Services.Model.Mappings
{
    public class LocalidadMap : BaseClassMapping<Localidad>
    {
        public LocalidadMap()
        {
            Table("LOCALIDAD");

            Id(x => x.Id, x =>
            {
                x.Column("ID");
                x.Generator(new IdentityGeneratorDef());
                x.UnsavedValue(0);
            });

            Property(x => x.Nombre, "NOMBRE");
            Property(x => x.CodigoPostal, "CODIGOPOSTAL");
        }
    }
}
