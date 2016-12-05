using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels
{
    public class TipoOrganizacionCreationViewModel : BaseTipoOrganizacionViewModel
    {
        public TipoOrganizacionCreationViewModel() { }

        public TipoOrganizacionCreationViewModel(TipoOrganizacionLogic tipoOrganizacionLogic) : base(tipoOrganizacionLogic)
        {
            TipoOrganizacion = new TipoOrganizacionEditionData();

            SubmitFunction = () =>
            {
                return CreateOrganizacion();
            };
        }

        private bool CreateOrganizacion()
        {
            Result = tipoOrganizacionLogic.Create(TipoOrganizacion);

            return Result;
        }
    }
}
