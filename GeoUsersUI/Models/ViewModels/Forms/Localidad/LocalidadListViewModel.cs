using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    public class LocalidadListViewModel : BaseListViewModel
    {
        private readonly LocalidadLogic localidadLogic;

        public ObservableCollection<LocalidadHeaderData> Localidades { get; set; }

        public LocalidadListViewModel() { }

        public LocalidadListViewModel(LocalidadLogic localidadLogic)
        {
            this.localidadLogic = localidadLogic;

            Localidades = new ObservableCollection<LocalidadHeaderData>();
        }

        public async Task<bool> LoadLocalidades()
        {
            IEnumerable<LocalidadHeaderData> localidades = null;

            await Task.Run(() =>
            {
                using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    localidades = localidadLogic.GetAll();
                }
            });

            Localidades.Update(localidades);

            return true;
        }

        public async Task<bool> Delete(int localidadId)
        {
            await Task.Run(() =>
            {
                using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    localidadLogic.Delete(localidadId);
                }
            });

            return true;
        }
    }
}
