using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class OrganizacionListViewModel : BaseListViewModel
    {
        private readonly OrganizacionLogic organizacionLogic;

        public ObservableCollection<OrganizacionData> Organizaciones { get; set; }

        public OrganizacionListViewModel() { }

        public OrganizacionListViewModel(OrganizacionLogic organizacionLogic)
        {
            this.organizacionLogic = organizacionLogic;

            Organizaciones = new ObservableCollection<OrganizacionData>();
        }

        public async Task<bool> LoadOrganizaciones()
        {
            IEnumerable<OrganizacionData> organizaciones = null;

            await Task.Run(() =>
            {
                using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    organizaciones = organizacionLogic.GetAll();
                }
            });

            Organizaciones.Update(organizaciones);

            return true;
        }

        public async Task<bool> Delete(int organizacionId)
        {
            await Task.Run(() =>
            {
                using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    organizacionLogic.Delete(organizacionId);
                }
            });

            return true;
        }
    }
}
