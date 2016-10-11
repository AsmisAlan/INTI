using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeoUsers.Services
{
    public class NhibernateConfiguration
    {
        public static ISessionFactory Configure()
        {
            var nhibernateConfiguration = new Configuration().Configure("hibernate.cfg.xml");

            nhibernateConfiguration.CurrentSessionContext<ThreadStaticSessionContext>();

            var mapper = new ModelMapper();

            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            nhibernateConfiguration.AddMapping(mapping);

            return nhibernateConfiguration.BuildSessionFactory();
        }
    }
}
