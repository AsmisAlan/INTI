using GeoUsers.Services.Model.Entities;
using NHibernate.Mapping.ByCode;

namespace GeoUsers.Services.Model.Mappings
{
    public class SectorMap : BaseClassMapping<Sector>
    {
        public SectorMap()
        {
            Table("SECTOR");

            Id<int>(x => x.Id, x =>
            {
                x.Column("IDSECTOR");
                x.Generator(new IdentityGeneratorDef());
                x.UnsavedValue(0);
            });

            Property(x => x.Nombre, "NOMBRE");


        }
    }
}
