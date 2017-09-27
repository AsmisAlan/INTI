using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

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

            Organizacion.AutoDetectCoordinates = true;

            Localidades = new ObservableCollection<IdAndValue>();
            Rubros = new ObservableCollection<IdAndValue>();
            TipoOrganizaciones = new ObservableCollection<IdAndValue>();

            SubmitValidation = () =>
            {
                return Validate();
            };

            SubmitFunction = SubmitForm;
        }

        public async Task<bool> Initialize(int? organizacionId = null)
        {
            IEnumerable<IdAndValue> localidades = null;
            IEnumerable<IdAndValue> rubros = null;
            IEnumerable<IdAndValue> tipoOrganizaciones = null;

            await RequestService.Execute(() =>
            {
                localidades = localidadLogic.GetForSelection();
                rubros = rubroLogic.GetForSelection();
                tipoOrganizaciones = tipoOrganizacionLogic.GetForSelection();

                if (organizacionId.HasValue)
                {
                    WindowTitle = "Modificar Organizacion";

                    var organizacionData = organizacionLogic.GetForEdition(organizacionId.Value);

                    Organizacion.ContactoCargo = organizacionData.ContactoCargo;
                    Organizacion.Cuit = organizacionData.Cuit;
                    Organizacion.Direccion = organizacionData.Direccion;
                    Organizacion.Email = organizacionData.Email;
                    Organizacion.Id = organizacionData.Id;
                    Organizacion.LocalidadId = organizacionData.LocalidadId;
                    Organizacion.Nombre = organizacionData.Nombre;
                    Organizacion.Personal = organizacionData.Personal;
                    Organizacion.RubroId = organizacionData.RubroId;
                    Organizacion.Telefono = organizacionData.Telefono;
                    Organizacion.TipoOrganizacionId = organizacionData.TipoOrganizacionId;
                    Organizacion.UsuarioInti = organizacionData.UsuarioInti;
                    Organizacion.Web = organizacionData.Web;
                    Organizacion.Latitud = organizacionData.Latitud;
                    Organizacion.Longitud = organizacionData.Longitud;
                    Organizacion.AutoDetectCoordinates = false;
                }
                else
                {
                    WindowTitle = "Crear Organizacion";
                }
            });

            Localidades.Update(localidades);
            Rubros.Update(rubros);
            TipoOrganizaciones.Update(tipoOrganizaciones);

            return true;
        }

        public void AddError(ValidationError error)
        {
            if (!Errors.Contains(error))
            {
                Errors.Add(error);
            }
        }

        public void RemoveError(ValidationError error)
        {
            Errors.Remove(error);
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

        private bool Validate()
        {
            var mandatoryValidation = Organizacion != null &&
                                      !string.IsNullOrEmpty(Organizacion.Nombre) &&
                                      !string.IsNullOrEmpty(Organizacion.Telefono) &&
                                      !string.IsNullOrEmpty(Organizacion.ContactoCargo) &&
                                      !string.IsNullOrEmpty(Organizacion.Direccion) &&
                                      !string.IsNullOrEmpty(Organizacion.Email) &&
                                      Organizacion.LocalidadId.HasValue &&
                                      Organizacion.TipoOrganizacionId.HasValue &&
                                      Organizacion.RubroId.HasValue &&
                                      (Organizacion.AutoDetectCoordinates ||
                                      !Organizacion.AutoDetectCoordinates &&
                                      !string.IsNullOrEmpty(Organizacion.Longitud) &&
                                      !string.IsNullOrEmpty(Organizacion.Latitud));

            return mandatoryValidation;
        }
    }
}
