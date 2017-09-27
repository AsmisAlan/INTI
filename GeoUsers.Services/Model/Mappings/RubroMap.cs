using GeoUsers.Services.Model.Entities;
using NHibernate.Mapping.ByCode;

namespace GeoUsers.Services.Model.Mappings
{
    public class RubroMap : BaseClassMapping<Rubro>
    {
        public RubroMap()
        {
            Table("RUBRO");

            Id(x => x.Id, x =>
            {
                x.Column("IDRUBRO");
                x.Generator(new IdentityGeneratorDef());
                x.UnsavedValue(0);
            });

            Property(x => x.Nombre, "NOMBRE");

            ManyToOne(x => x.Sector, map =>
            {
                map.Column("IDSECTOR");
            });
        }
    }
}