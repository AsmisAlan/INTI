using GeoUsers.Services;
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

        public App()
        {
            Container = new UnityContainer();

            GeoUsersServices.Initialize(Container);
        }
    }
}
