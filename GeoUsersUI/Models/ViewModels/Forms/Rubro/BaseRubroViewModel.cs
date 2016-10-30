using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public abstract class BaseRubroViewModel : BaseSubmitViewModel<bool>
    {
        protected readonly RubroLogic rubroLogic;
        protected readonly SectorLogic sectorLogic;

        public ObservableCollection<IdAndValue> Sectores { get; set; }

        public RubroCreationData Rubro { get; set; }

        public BaseRubroViewModel() { }

        public BaseRubroViewModel(RubroLogic rubroLogic,
                                  SectorLogic sectorLogic) : base()
        {
            this.rubroLogic = rubroLogic;
            this.sectorLogic = sectorLogic;

            SubmitValidation = () =>
            {
                return !string.IsNullOrEmpty(Rubro.Nombre) &&
                       Rubro.SectorId.HasValue;
            };
        }

        public async Task<bool> LoadData()
        {
            await Task.Run(() =>
            {
                using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    var sectores = sectorLogic.GetForSelection();

                    Sectores = new ObservableCollection<IdAndValue>(sectores);
                }
            });

            return true;
        }
    }
}
