using GeoUsers.Services;
using GeoUsers.Services.Utils;
using GeoUsersUI.Windows;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GeoUsersUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IUnityContainer Container { get; set; }

        public bool IsUserAuthenticated { get; set; }

        public App()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            Container = new UnityContainer();

            GeoUsersServices.Initialize(Container);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var loginForm = new LoginForm();

            var loginResult = loginForm.ShowDialog();

            IsUserAuthenticated = loginResult.HasValue && loginResult.Value;

            ShutdownMode = ShutdownMode.OnLastWindowClose;
        }
    }
}
