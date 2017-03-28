using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    class TipoOrganizacionListViewModel : BaseListViewModel
    {
        private readonly TipoOrganizacionLogic tipoOrganizacionLogic;

        public ObservableCollection<TipoOrganizacionHeaderData> TipoOrganizaciones { get; set; }

        public TipoOrganizacionListViewModel() { }

        public TipoOrganizacionListViewModel(TipoOrganizacionLogic tipoOrganizacionLogic)
        {
            this.tipoOrganizacionLogic = tipoOrganizacionLogic;

            TipoOrganizaciones = new ObservableCollection<TipoOrganizacionHeaderData>();
        }

        public async Task<bool> LoadTipoOrganizaciones()
        {
            IEnumerable<TipoOrganizacionHeaderData> tipoOrganizaciones = null;

            await RequestService.Execute(() =>
            {
                tipoOrganizaciones = tipoOrganizacionLogic.GetAll();
            });

            TipoOrganizaciones.Update(tipoOrganizaciones);

            return true;
        }

        public async Task<bool> Delete(int tipoOrganizacionId)
        {
            await RequestService.Execute(() =>
            {
                tipoOrganizacionLogic.Delete(tipoOrganizacionId);
            });

            return true;
        }
    }
}
