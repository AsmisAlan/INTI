using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels.Forms
{
    public class LocalidadEditionViewModel : BaseSubmitViewModel<bool>
    {
        readonly LocalidadLogic localidadLogic;

        public LocalidadEditionData Localidad { get; set; }

        public LocalidadEditionViewModel() { }

        public LocalidadEditionViewModel(LocalidadLogic localidadLogic)
        {
            this.localidadLogic = localidadLogic;

            Localidad = new LocalidadEditionData();

            SubmitFunction = () =>
            {
                return CreateLocalidad();
            };

            SubmitValidation = () =>
            {
                return Localidad.CodigoPostal != null && !string.IsNullOrEmpty(Localidad.Nombre);
            };
        }

        public async Task<bool> Initialize(int? localidadId)
        {
            if (localidadId.HasValue)
            {
                WindowTitle = "Modificar Localidad";

                await RequestService.Execute(() =>
                    {
                        var localidadData = localidadLogic.GetForEdition(localidadId.Value);

                        Localidad.CodigoPostal = localidadData.CodigoPostal;
                        Localidad.Nombre = localidadData.Nombre;
                        Localidad.Id = localidadData.Id;
                    });
            }
            else
            {
                WindowTitle = "Crear Localidad";
            }

            return true;
        }

        private bool CreateLocalidad()
        {
            Result = localidadLogic.Save(Localidad);

            return Result;
        }
    }
}
