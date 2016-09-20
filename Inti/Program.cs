using Microsoft.Practices.Unity;
using System;
using GeoUsers.Services;
using System.Windows.Forms;

namespace Inti
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static IUnityContainer Container { get; set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Container = new UnityContainer();

            GeoUsersServices.Initialize(Container);

            Application.Run(Container.Resolve<Main>());
        }
    }
}
