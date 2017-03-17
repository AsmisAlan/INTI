using GeoUsers.Services;
using GeoUsers.Services.Logics;
using GeoUsers.Services.Model.DataTransfer;
using GeoUsersUI.Utils;
using System.Threading.Tasks;

namespace GeoUsersUI.Models.ViewModels
{
    public class SectorEditionViewModel : BaseSubmitViewModel<bool>
    {
        protected readonly SectorLogic sectorLogic;

        public SectorEditionData Sector { get; set; }

        public SectorEditionViewModel() { }

        public SectorEditionViewModel(SectorLogic sectorLogic)
        {
            this.sectorLogic = sectorLogic;

            SubmitValidation = () =>
            {
                return !string.IsNullOrEmpty(Sector.Nombre);
            };

            SubmitFunction = () =>
            {
                return Save();
            };

            Sector = new SectorEditionData();

            Sector.Icono = new ArchivoEditionData();
        }

        public async Task<bool> Initialize(int? sectorId)
        {
            if (sectorId.HasValue)
            {
                WindowTitle = "Modificar Sector";

                await RequestService.Execute(() =>
                {
                    var sectorData = sectorLogic.GetForEdition(sectorId.Value);

                    Sector.Id = sectorData.Id;
                    Sector.Icono = sectorData.Icono;
                    Sector.Nombre = sectorData.Nombre;
                });
            }
            else
            {
                WindowTitle = "Crear Sector";
            }

            return true;
        }

        public void SetIconoData(string name, string path, byte[] data)
        {
            var extension = name.Substring(name.LastIndexOf('.') + 1);

            if (Sector.Icono == null)
            {
                Sector.Icono = new ArchivoEditionData();
            }

            Sector.Icono.Extension = extension;
            Sector.Icono.Nombre = name;
            Sector.Icono.Data = data;
            Sector.Icono.Ruta = path;
        }

        public void DeleteSectorIcono()
        {
            Sector.Icono = null;
        }

        private bool Save()
        {
            Result = sectorLogic.Save(Sector);

            return Result;
        }
    }
}
