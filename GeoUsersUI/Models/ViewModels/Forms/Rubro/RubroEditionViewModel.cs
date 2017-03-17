using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class RubroEditionViewModel : BaseSubmitViewModel<bool>
    {
        protected readonly RubroLogic rubroLogic;
        protected readonly SectorLogic sectorLogic;

        public ObservableCollection<IdAndValue> Sectores { get; set; }

        public RubroEditionData Rubro { get; set; }

        public RubroEditionViewModel() { }

        public RubroEditionViewModel(RubroLogic rubroLogic,
                                     SectorLogic sectorLogic)
            : base()
        {
            this.rubroLogic = rubroLogic;
            this.sectorLogic = sectorLogic;

            Rubro = new RubroEditionData();
            Sectores = new ObservableCollection<IdAndValue>();

            SubmitValidation = () =>
            {
                return !string.IsNullOrEmpty(Rubro.Nombre) &&
                       Rubro.SectorId.HasValue;
            };

            SubmitFunction = () =>
            {
                return Save();
            };
        }

        private bool Save()
        {
            Result = rubroLogic.Save(Rubro);

            return Result;
        }

        public async Task<bool> Initialize(int? rubroId = null)
        {
            IEnumerable<IdAndValue> sectores = null;

            await RequestService.Execute(() =>
            {
                sectores = sectorLogic.GetForSelection();

                if (rubroId.HasValue)
                {
                    WindowTitle = "Modificar Rubro";

                    var rubroData = rubroLogic.GetForEdition(rubroId.Value);

                    Rubro.Id = rubroData.Id;
                    Rubro.Nombre = rubroData.Nombre;
                    Rubro.SectorId = rubroData.SectorId;
                }
                else
                {
                    WindowTitle = "Crear Rubro";
                }
            });

            Sectores.Update(sectores);

            return true;
        }
    }
}
