using GeoUsers.Services.Model.Entities;
using NHibernate.Mapping.ByCode;

namespace GeoUsers.Services.Model.Mappings
{
    public class UsuarioMap : BaseClassMapping<Usuario>
    {
        public UsuarioMap()
        {
            Table("USUARIO");

            Id<int>(x => x.Id, x =>
            {
                x.Column("ID");
                x.Generator(new IdentityGeneratorDef());
                x.UnsavedValue(0);
            });

            Property(x => x.LoginId, "LOGINID");
            Property(x => x.Password, "PASSWORD");
        }
    }
}
