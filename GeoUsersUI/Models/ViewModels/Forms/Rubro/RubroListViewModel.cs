using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    public class RubroListViewModel
    {
        private readonly RubroLogic rubroLogic;

        public ObservableCollection<RubroHeaderData> Rubros { get; set; }

        public RubroListViewModel() { }

        public RubroListViewModel(RubroLogic rubroLogic)
        {
            this.rubroLogic = rubroLogic;

            Rubros = new ObservableCollection<RubroHeaderData>();
        }

        public async Task<bool> LoadRubros()
        {
            IEnumerable<RubroHeaderData> rubros = null;

            await RequestService.Execute(() =>
            {
                rubros = rubroLogic.GetAll();
            });

            Rubros.Update(rubros);

            return true;
        }

        public async Task<bool> Delete(int rubroId)
        {
            await RequestService.Execute(() =>
            {
                rubroLogic.Delete(rubroId);
            });

            return true;
        }
    }
}
