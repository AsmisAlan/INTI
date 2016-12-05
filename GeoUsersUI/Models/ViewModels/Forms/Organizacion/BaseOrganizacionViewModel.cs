using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    public class BaseOrganizacionViewModel : BaseSubmitViewModel<OrganizacionHeader>
    {
        protected readonly OrganizacionLogic organizacionLogic;
        protected readonly LocalidadLogic localidadLogic;
        protected readonly RubroLogic rubroLogic;
        protected readonly TipoOrganizacionLogic tipoOrganizacionLogic;

        public OrganizacionCreationData Organizacion { get; set; }
        public IEnumerable<IdAndValue> Localidades { get; set; }
        public IEnumerable<IdAndValue> TipoOrganizaciones { get; set; }
        public IEnumerable<IdAndValue> Rubros { get; set; }

        public BaseOrganizacionViewModel()
        {
        }

        public BaseOrganizacionViewModel(OrganizacionLogic organizacionLogic,
                                         LocalidadLogic localidadLogic,
                                         RubroLogic rubroLogic,
                                         TipoOrganizacionLogic tipoOrganizacionLogic)
        {
            this.organizacionLogic = organizacionLogic;
            this.localidadLogic = localidadLogic;
            this.rubroLogic = rubroLogic;
            this.tipoOrganizacionLogic = tipoOrganizacionLogic;

            Organizacion = new OrganizacionCreationData();

            SubmitValidation = () =>
            {
                return Organizacion != null &&
                       !string.IsNullOrEmpty(Organizacion.Nombre) &&
                       !string.IsNullOrEmpty(Organizacion.Telefono) &&
                       !string.IsNullOrEmpty(Organizacion.ContactoCargo) &&
                       !string.IsNullOrEmpty(Organizacion.Direccion) &&
                       !string.IsNullOrEmpty(Organizacion.Email) &&
                       Organizacion.LocalidadId.HasValue &&
                       Organizacion.OrganizacionId.HasValue &&
                       Organizacion.RubroId.HasValue &&
                       Organizacion.Personal.HasValue;
            };
        }

        public async Task<bool> LoadData()
        {
            await Task.Run(() =>
            {
                using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    Localidades = localidadLogic.GetForSelection();
                    Rubros = rubroLogic.GetForSelection();
                    TipoOrganizaciones = organizacionLogic.GetForSelection();
                }
            });

            return true;
        }
    }
}
