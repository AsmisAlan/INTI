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

            WindowTitle = "Localidades";
            Localidades = new ObservableCollection<LocalidadHeaderData>();
        }

        public async Task<bool> LoadLocalidades()
        {
            IEnumerable<LocalidadHeaderData> localidades = null;

            await RequestService.Execute(() =>
            {
                localidades = localidadLogic.GetAll();
            });

            Localidades.Update(localidades);

            return true;
        }

        public async Task<bool> Delete(int localidadId)
        {
            await RequestService.Execute(() => localidadLogic.Delete(localidadId));

            return true;
        }

        protected override void Export(string filePath)
        {
            localidadLogic.ExportToExcel(filePath);
        }
    }
}
