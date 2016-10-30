using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels
{
    public abstract class BaseSectorViewModel : BaseSubmitViewModel<bool>
    {
        protected readonly SectorLogic sectorLogic;

        public SectorCreationData Sector { get; set; }

        public BaseSectorViewModel() { }

        public BaseSectorViewModel(SectorLogic sectorLogic) : base()
        {
            this.sectorLogic = sectorLogic;

            SubmitValidation = () =>
            {
                return !string.IsNullOrEmpty(Sector.Nombre);
            };
        }
    }
}
