using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels
{
    public class SectorCreationViewModel : BaseSectorViewModel
    {
        public SectorCreationViewModel() { }

        public SectorCreationViewModel(SectorLogic sectorLogic) : base(sectorLogic)
        {
            Sector = new SectorCreationData();

            SubmitFunction = () =>
            {
                return CreateOrganizacion();
            };
        }

        private bool CreateOrganizacion()
        {
            Result = sectorLogic.Create(Sector);

            return Result;
        }
    }
}
