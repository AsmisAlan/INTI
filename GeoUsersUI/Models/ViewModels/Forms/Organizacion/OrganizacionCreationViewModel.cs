using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels
{
    public class OrganizacionCreationViewModel : BaseOrganizacionViewModel
    {
        public OrganizacionCreationViewModel() { }

        public OrganizacionCreationViewModel(OrganizacionLogic organizacionLogic) : base(organizacionLogic)
        {
            Organizacion = new OrganizacionEditionData();

            SubmitFunction = () =>
            {
                return CreateOrganizacion();
            };
        }

        private bool CreateOrganizacion()
        {
            Result = organizacionLogic.Create(Organizacion);

            return Result;
        }
    }
}
