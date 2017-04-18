using AutoMapper;
using NHibernate;

namespace GeoUsers.Services.Logics
{
    public class UsuarioLogic : BaseLogic
    {
        public UsuarioLogic(IMapper autoMapper,
                            ISessionFactory sessionFactory) : base(autoMapper, sessionFactory)
        { }
    }
}
