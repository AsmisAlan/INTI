using GeoUsers.Services.SQLExceptions;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using System.Reflection;

namespace GeoUsers.Services
{
    public class NhibernateConfiguration
    {
        public static ISessionFactory Configure()
        {
            var nhibernateConfiguration = new Configuration().Configure("hibernate.cfg.xml");

            nhibernateConfiguration.SetProperty(Environment.ConnectionString, Properties.Settings.Default.ConnectionString);

            nhibernateConfiguration.CurrentSessionContext<ThreadStaticSessionContext>();

            nhibernateConfiguration.SetProperty(Environment.SqlExceptionConverter, typeof(SqlExceptionConverter).AssemblyQualifiedName);

            var mapper = new ModelMapper();

            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            nhibernateConfiguration.AddMapping(mapping);

            return nhibernateConfiguration.BuildSessionFactory();
        }
    }
}
