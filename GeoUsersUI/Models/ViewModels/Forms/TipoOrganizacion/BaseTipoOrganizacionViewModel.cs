using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels
{
    public abstract class BaseTipoOrganizacionViewModel : BaseSubmitViewModel<bool>
    {
        protected readonly TipoOrganizacionLogic tipoOrganizacionLogic;

        public TipoOrganizacionCreationData TipoOrganizacion { get; set; }

        public BaseTipoOrganizacionViewModel() { }

        public BaseTipoOrganizacionViewModel(TipoOrganizacionLogic tipoOrganizacionLogic) : base()
        {
            this.tipoOrganizacionLogic = tipoOrganizacionLogic;

            SubmitValidation = () =>
            {
                return !string.IsNullOrEmpty(TipoOrganizacion.Tipo);
            };
        }
    }
}
