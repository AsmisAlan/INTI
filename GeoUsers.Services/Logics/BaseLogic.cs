using AutoMapper;
using NHibernate;

namespace GeoUsers.Services.Logics
{
    public class BaseLogic
    {
        readonly IMapper mapper;
        readonly ISessionFactory sessionFactory;

        protected ISession Session
        {
            get
            {
                return sessionFactory.GetCurrentSession();
            }
            private set { }
        }

        protected IMapper Mapper
        {
            get
            {
                return mapper;
            }
        }

        public BaseLogic(IMapper autoMapper,
                         ISessionFactory sessionFactory)
        {
            this.mapper = autoMapper;
            this.sessionFactory = sessionFactory;
        }
    }
}
