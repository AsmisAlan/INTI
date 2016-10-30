using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels
{
    public abstract class BaseOrganizacionViewModel : BaseSubmitViewModel<bool>
    {
        protected readonly OrganizacionLogic organizacionLogic;

        public OrganizacionCreationData Organizacion { get; set; }

        public BaseOrganizacionViewModel() { }

        public BaseOrganizacionViewModel(OrganizacionLogic organizacionLogic) : base()
        {
            this.organizacionLogic = organizacionLogic;

            SubmitValidation = () =>
            {
                return !string.IsNullOrEmpty(Organizacion.Tipo);
            };
        }
    }
}
