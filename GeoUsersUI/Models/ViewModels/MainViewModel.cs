using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Models.ViewModels.Helpers;
using GeoUsersUI.Models.ViewModels.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class MainViewModel
    {
        readonly OrganizacionLogic organizacionLogic;

        public ObservableCollection<OrganizacionHeader> Organizaciones { get; set; }

        public Task<IEnumerable<OrganizacionHeader>> InitializationTask { get; private set; }

        public MainMenuContainer MainMenu { get; set; }

        public FilterStatus OrganizacionesFilter { get; set; }

        public bool Loading { get; set; }

        public MainViewModel()
        {
        }

        public MainViewModel(OrganizacionLogic organizacionLogic)
        {
            this.organizacionLogic = organizacionLogic;

            OrganizacionesFilter = new FilterStatus();
            InitializationTask = ExecuteDataFunction();
        }

        private async Task<IEnumerable<OrganizacionHeader>> ExecuteDataFunction()
        {
            Loading = true;

            var results = await Task.Run(() =>
            {
                using (var sessionBlock = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    return organizacionLogic.GetOrganizacionHeaders();
                }
            });

            Organizaciones = new ObservableCollection<OrganizacionHeader>(results);

            Loading = false;

            return results;
        }
    }
}
