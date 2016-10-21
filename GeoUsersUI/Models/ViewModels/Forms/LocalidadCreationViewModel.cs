using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Windows;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    public class LocalidadCreationViewModel : BaseSubmitViewModel<bool>
    {
        readonly LocalidadLogic localidadLogic;

        public LocalidadCreationData Localidad { get; set; }

        public LocalidadCreationViewModel() { }

        public LocalidadCreationViewModel(LocalidadLogic localidadLogic)
        {
            this.localidadLogic = localidadLogic;

            Localidad = new LocalidadCreationData();

            SubmitFunction = () =>
            {
                return CreateLocalidad();
            };

            SubmitValidation = () =>
            {
                return Localidad.CodigoPostal != null && !string.IsNullOrEmpty(Localidad.Nombre);
            };
        }

        private bool CreateLocalidad()
        {
            Result = localidadLogic.Create(Localidad);

            return Result;
        }
    }
}
