using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    public class BaseUsuarioViewModel : BaseSubmitViewModel<UsuarioHeader>
    {
        protected readonly UsuarioLogic usuarioLogic;
        protected readonly LocalidadLogic localidadLogic;
        protected readonly RubroLogic rubroLogic;
        protected readonly OrganizacionLogic organizacionLogic;

        public UsuarioCreationData Usuario { get; set; }
        public IEnumerable<IdAndValue> Localidades { get; set; }
        public IEnumerable<IdAndValue> Organizaciones { get; set; }
        public IEnumerable<IdAndValue> Rubros { get; set; }

        public BaseUsuarioViewModel()
        {
        }

        public BaseUsuarioViewModel(UsuarioLogic usuarioLogic,
                                    LocalidadLogic localidadLogic,
                                    RubroLogic rubroLogic,
                                    OrganizacionLogic organizacionLogic)
        {
            this.usuarioLogic = usuarioLogic;
            this.localidadLogic = localidadLogic;
            this.rubroLogic = rubroLogic;
            this.organizacionLogic = organizacionLogic;

            Usuario = new UsuarioCreationData();

            SubmitValidation = () =>
            {
                return Usuario != null &&
                       !string.IsNullOrEmpty(Usuario.Nombre) &&
                       !string.IsNullOrEmpty(Usuario.Telefono) &&
                       !string.IsNullOrEmpty(Usuario.ContactoCargo) &&
                       !string.IsNullOrEmpty(Usuario.Direccion) &&
                       !string.IsNullOrEmpty(Usuario.Email) &&
                       Usuario.LocalidadId.HasValue &&
                       Usuario.OrganizacionId.HasValue &&
                       Usuario.RubroId.HasValue &&
                       Usuario.Personal.HasValue;
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
                    Organizaciones = organizacionLogic.GetForSelection();
                }
            });

            return true;
        }
    }
}
