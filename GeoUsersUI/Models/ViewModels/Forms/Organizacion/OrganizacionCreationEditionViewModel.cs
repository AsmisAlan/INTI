using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    public class OrganizacionCreationEditionViewModel : BaseOrganizacionViewModel
    {
        public OrganizacionCreationEditionViewModel()
        {
        }

        public OrganizacionCreationEditionViewModel(OrganizacionLogic organizacionLogic,
                                                    LocalidadLogic localidadLogic,
                                                    RubroLogic rubroLogic,
                                                    TipoOrganizacionLogic tipoOrganizacionLogic) : base(organizacionLogic,
                                                                                                        localidadLogic,
                                                                                                        rubroLogic,
                                                                                                        tipoOrganizacionLogic)
        {
            SubmitFunction = () =>
            {
                return Create();
            };
        }

        public OrganizacionHeader Create()
        {
            var result = organizacionLogic.Create(Organizacion);

            if (result)
            {
                Result = new OrganizacionHeader()
                {
                    Id = Organizacion.Id,
                    Email = Organizacion.Email,
                    Nombre = Organizacion.Nombre,
                    Direccion = Organizacion.Direccion
                };
            }

            return Result;
        }
    }
}
