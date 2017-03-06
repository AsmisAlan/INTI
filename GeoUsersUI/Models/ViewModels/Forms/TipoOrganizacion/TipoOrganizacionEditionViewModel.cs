using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class TipoOrganizacionEditionViewModel : BaseSubmitViewModel<bool>
    {
        protected readonly TipoOrganizacionLogic tipoOrganizacionLogic;

        public TipoOrganizacionEditionData TipoOrganizacion { get; set; }

        public TipoOrganizacionEditionViewModel() { }

        public TipoOrganizacionEditionViewModel(TipoOrganizacionLogic tipoOrganizacionLogic)
        {
            this.tipoOrganizacionLogic = tipoOrganizacionLogic;

            SubmitValidation = () =>
            {
                return !string.IsNullOrEmpty(TipoOrganizacion.Tipo);
            };

            SubmitFunction = () =>
            {
                return Save();
            };

            TipoOrganizacion = new TipoOrganizacionEditionData();
        }

        public async Task<bool> Initialize(int? sectorId)
        {
            if (sectorId.HasValue)
            {
                WindowTitle = "Modificar Tipo de Organizacion";

                await Task.Run(() =>
                {
                    using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                    {
                        var tipoOrganizacionData = tipoOrganizacionLogic.GetForEdition(sectorId.Value);

                        TipoOrganizacion.Update(tipoOrganizacionData);
                    }
                });
            }
            else
            {
                WindowTitle = "Crear Tipo de Organizacion";
            }

            return true;
        }

        private bool Save()
        {
            Result = tipoOrganizacionLogic.Save(TipoOrganizacion);

            return Result;
        }
    }
}
