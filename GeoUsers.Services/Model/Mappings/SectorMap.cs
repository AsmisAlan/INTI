using GeoUsers.Services.Model.Entities;
using NHibernate.Mapping.ByCode;

namespace GeoUsers.Services.Model.Mappings
{
    public class SectorMap : BaseClassMapping<Sector>
    {
        public SectorMap()
        {
            Table("SECTOR");

            Id(x => x.Id, x =>
            {
                x.Column("ID");
                x.Generator(new IdentityGeneratorDef());
                x.UnsavedValue(0);
            });

            Property(x => x.Nombre, "NOMBRE");

            ManyToOne(x => x.Icono, map =>
            {
                map.Column("IDICONO");
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
            });

            Set(x => x.Rubros, collectionMapping =>
            {
                collectionMapping.Key(key =>
                {
                    key.Column("IDSECTOR");
                });

                collectionMapping.Table("RUBRO");
                collectionMapping.Cascade(Cascade.All | Cascade.DeleteOrphans);
                collectionMapping.Inverse(true);
            },
            mapping => mapping.OneToMany());
        }
    }
}