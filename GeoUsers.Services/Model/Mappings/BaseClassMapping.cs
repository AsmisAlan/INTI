using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Linq.Expressions;

namespace GeoUsers.Services.Model.Mappings
{
    public class BaseClassMapping<TEntity> : ClassMapping<TEntity> where TEntity : class
    {
        public const string SCHEMA = "GEOUSERS";

        public BaseClassMapping() : this(SCHEMA, true) { }

        public BaseClassMapping(string schema, bool mutable)
        {
            Schema(schema);
            Mutable(mutable);
        }

        protected void Property<TProperty>(Expression<Func<TEntity, TProperty>> property, string columName)
        {
            Property(property, x => x.Column(columName));
        }
    }
}
