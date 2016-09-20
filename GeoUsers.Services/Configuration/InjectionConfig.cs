using AutoMapper;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Cfg;

namespace GeoUsers.Services
{
    public class InjectionConfig
    {
        public static IUnityContainer RegisterDependencies(IUnityContainer container)
        {
            container.RegisterType<ISessionFactory>(
                new ContainerControlledLifetimeManager(),
                new InjectionFactory(c =>
                {
                    return NhibernateConfiguration.Configure();
                })
                );

            container.RegisterInstance<IMapper>(AutoMapperConfiguration.ConfigureAutomapper());

            return container;
        }
    }
}
