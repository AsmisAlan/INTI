﻿using GeoUsers.Services.Model.Entities;
using NHibernate.Mapping.ByCode;

namespace GeoUsers.Services.Model.Mappings
{
    public class ArchivoMap : BaseClassMapping<Archivo>
    {
        public ArchivoMap()
        {
            Table("ARCHIVO");

            Id(x => x.Id, x =>
            {
                x.Column("ID");
                x.Generator(new IdentityGeneratorDef());
                x.UnsavedValue(0);
            });

            Property(x => x.Extension, "EXTENSION");
            Property(x => x.Nombre, "NOMBRE");
            Property(x => x.Ruta, "RUTA");
        }
    }
}
