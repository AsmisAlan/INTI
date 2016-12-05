using System;
using System.Collections.Generic;
using System.Linq;
using GeoUsers.Services.Model.Entities;
using NHibernate.Mapping.ByCode;

namespace GeoUsers.Services.Model.Mappings
{
    public class TipoOrganizacionMap : BaseClassMapping<TipoOrganizacion>
    {
        public TipoOrganizacionMap()
        {
            Table("ORGANIZACION");

            Id<int>(x => x.Id, x =>
            {
                x.Column("IDORGANIZACION");
                x.Generator(new IdentityGeneratorDef());
                x.UnsavedValue(0);
            });

            Property(x => x.Tipo, "TIPO");
        }
    }
}
