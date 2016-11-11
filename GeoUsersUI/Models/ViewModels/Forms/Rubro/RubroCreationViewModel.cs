using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels
{
    public class RubroCreationViewModel : BaseRubroViewModel
    {
        public RubroCreationViewModel() { }

        public RubroCreationViewModel(RubroLogic rubroLogic,
                                      SectorLogic sectorLogic) : base(rubroLogic, sectorLogic)
        {
            Rubro = new RubroCreationData();

            SubmitFunction = () =>
            {
                return CreateRubro();
            };
        }

        private bool CreateRubro()
        {
            Result = rubroLogic.Create(Rubro);

            return Result;
        }
    }
}
