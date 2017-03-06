using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    public class OrganizacionViewModel : BaseSubmitViewModel<OrganizacionHeaderData>
    {
        protected readonly OrganizacionLogic organizacionLogic;
        protected readonly LocalidadLogic localidadLogic;
        protected readonly RubroLogic rubroLogic;
        protected readonly TipoOrganizacionLogic tipoOrganizacionLogic;

        public OrganizacionEditionData Organizacion { get; set; }
        public ObservableCollection<IdAndValue> Localidades { get; set; }
        public ObservableCollection<IdAndValue> TipoOrganizaciones { get; set; }
        public ObservableCollection<IdAndValue> Rubros { get; set; }

        public OrganizacionViewModel()
        {
        }

        public OrganizacionViewModel(OrganizacionLogic organizacionLogic,
                                     LocalidadLogic localidadLogic,
                                     RubroLogic rubroLogic,
                                     TipoOrganizacionLogic tipoOrganizacionLogic)
        {
            this.organizacionLogic = organizacionLogic;
            this.localidadLogic = localidadLogic;
            this.rubroLogic = rubroLogic;
            this.tipoOrganizacionLogic = tipoOrganizacionLogic;

            Organizacion = new OrganizacionEditionData();

            Localidades = new ObservableCollection<IdAndValue>();
            Rubros = new ObservableCollection<IdAndValue>();
            TipoOrganizaciones = new ObservableCollection<IdAndValue>();

            SubmitValidation = () =>
            {
                return Organizacion != null &&
                       !string.IsNullOrEmpty(Organizacion.Nombre) &&
                       !string.IsNullOrEmpty(Organizacion.Telefono) &&
                       !string.IsNullOrEmpty(Organizacion.ContactoCargo) &&
                       !string.IsNullOrEmpty(Organizacion.Direccion) &&
                       !string.IsNullOrEmpty(Organizacion.Email) &&
                       Organizacion.LocalidadId.HasValue &&
                       Organizacion.TipoOrganizacionId.HasValue &&
                       Organizacion.RubroId.HasValue &&
                       Organizacion.Personal.HasValue;
            };

            SubmitFunction = SubmitForm;
        }

        public async Task<bool> Initialize(int? organizacionId = null)
        {
            IEnumerable<IdAndValue> localidades = null;
            IEnumerable<IdAndValue> rubros = null;
            IEnumerable<IdAndValue> tipoOrganizaciones = null;

            await Task.Run(() =>
            {
                using (var sessionContext = GeoUsersServices.SessionProvider.GetSessionContextBlock())
                {
                    localidades = localidadLogic.GetForSelection();
                    rubros = rubroLogic.GetForSelection();
                    tipoOrganizaciones = tipoOrganizacionLogic.GetForSelection();

                    if (organizacionId.HasValue)
                    {
                        WindowTitle = "Modificar Organizacion";

                        var organizacionData = organizacionLogic.GetForEdition(organizacionId.Value);

                        Organizacion.Update(organizacionData);
                    }
                    else
                    {
                        WindowTitle = "Crear Organizacion";
                    }
                }
            });

            Localidades.Update(localidades);
            Rubros.Update(rubros);
            TipoOrganizaciones.Update(tipoOrganizaciones);

            return true;
        }

        private OrganizacionHeaderData SubmitForm()
        {
            var result = Organizacion.Id.HasValue ? organizacionLogic.Edit(Organizacion) : organizacionLogic.Create(Organizacion);

            Result = new OrganizacionHeaderData()
            {
                Id = result,
                Email = Organizacion.Email,
                Nombre = Organizacion.Nombre,
                Direccion = Organizacion.Direccion
            };

            return Result;
        }
    }
}
