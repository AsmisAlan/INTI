using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class SectorEditionViewModel : BaseSubmitViewModel<bool>
    {
        protected readonly SectorLogic sectorLogic;

        public SectorEditionData Sector { get; set; }

        public SectorEditionViewModel() { }

        public SectorEditionViewModel(SectorLogic sectorLogic)
        {
            this.sectorLogic = sectorLogic;

            SubmitValidation = () =>
            {
                return !string.IsNullOrEmpty(Sector.Nombre);
            };

            SubmitFunction = () =>
            {
                return Save();
            };

            Sector = new SectorEditionData();
        }

        public async Task<bool> Initialize(int? sectorId)
        {
            if (sectorId.HasValue)
            {
                await Task.Run(() =>
                {
                    using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                    {
                        var sectorData = sectorLogic.GetForEdition(sectorId.Value);

                        Sector.Update(sectorData);
                    }
                });
            }

            return true;
        }

        private bool Save()
        {
            Result = sectorLogic.Save(Sector);

            return Result;
        }
    }
}
