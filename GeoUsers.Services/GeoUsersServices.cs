using Microsoft.Practices.Unity;
using static GeoUsers.Services.SessionProvider;

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

        public ContextSessionBlock GetContextSessionBlock()
        {
            return SessionProvider.GetSessionContextBlock();
        }
    }
}
