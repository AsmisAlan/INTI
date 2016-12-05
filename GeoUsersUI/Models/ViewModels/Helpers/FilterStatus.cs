using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels.Helpers
{
    public class FilterStatus
    {
        public FilterData Filter { get; set; }

        public string TipoOrganizacionStatus
        {
            get
            {
                return $"Tipo Organizacion ({Filter.TipoOrganizacionIds.Count})";
            }
        }

        public string SectorStatus
        {
            get
            {
                return $"Sector ({Filter.SectorIds.Count})";
            }
        }

        public string RubroStatus
        {
            get
            {
                return $"Rubro ({Filter.RubroIds.Count})";
            }
        }

        public string LocalidadStatus
        {
            get
            {
                return $"Localidad ({Filter.LocalidadIds.Count})";
            }
        }

        public FilterStatus()
        {
            Filter = new FilterData();
        }
    }
}
