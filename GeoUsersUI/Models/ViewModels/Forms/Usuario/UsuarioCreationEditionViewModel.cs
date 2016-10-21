using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    public class UsuarioCreationEditionViewModel : BaseUsuarioViewModel
    {
        public UsuarioCreationEditionViewModel()
        {
        }

        public UsuarioCreationEditionViewModel(UsuarioLogic usuarioLogic,
                                               LocalidadLogic localidadLogic,
                                               RubroLogic rubroLogic,
                                               OrganizacionLogic organizacionLogic) : base(usuarioLogic,
                                                                                           localidadLogic,
                                                                                           rubroLogic,
                                                                                           organizacionLogic)
        {
            SubmitFunction = () =>
            {
                return Create();
            };
        }

        public UsuarioHeader Create()
        {
            var result = usuarioLogic.Create(Usuario);

            if (result)
            {
                Result = new UsuarioHeader()
                {
                    Id = Usuario.Id,
                    Email = Usuario.Email,
                    Nombre = Usuario.Nombre,
                    Direccion = Usuario.Direccion
                };
            }

            return Result;
        }
    }
}
