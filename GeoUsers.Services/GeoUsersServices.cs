using Microsoft.Practices.Unity;

namespace GeoUsers.Services
{
    public class GeoUsersServices
    {
        public static IUnityContainer Container { get; private set; }

        public static SessionProvider SessionProvider { get; private set; }

        public static void Initialize(IUnityContainer container)
        {
            //Dependency Injection
            Container = container;

            InjectionConfig.RegisterDependencies(container);

            SessionProvider = Container.Resolve<SessionProvider>();
        }
    }
}
