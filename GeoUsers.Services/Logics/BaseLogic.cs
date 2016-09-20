using AutoMapper;
using NHibernate;

namespace GeoUsers.Services.Logics
{
    public class BaseLogic
    {
        readonly IMapper autoMapper;
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
                return autoMapper;
            }
        }

        public BaseLogic(IMapper autoMapper,
                         ISessionFactory sessionFactory)
        {
            this.autoMapper = autoMapper;
            this.sessionFactory = sessionFactory;
        }
    }
}
