using GeoUsers.Services.Utils;
using Microsoft.Practices.Unity;
using System;

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

            try
            {
                InjectionConfig.RegisterDependencies(container);

                SessionProvider = Container.Resolve<SessionProvider>();
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
        }
    }
}
