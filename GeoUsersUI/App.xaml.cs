using CefSharp;
using GeoUsers.Services;
using GeoUsers.Services.Exceptions;
using GeoUsers.Services.Utils;
using GeoUsersUI.Utils;
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

        public static bool IsUserAuthenticated { get; set; }

        public App()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            Container = new UnityContainer();

            try
            {
                GeoUsersServices.Initialize(Container);

                Cef.Initialize();
            }
            catch (BusinessException e)
            {
                Logger.Log(e);

                var errorForm = new ConnectionErrorForm();

                errorForm.ShowDialog();

                Shutdown();
            }
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
