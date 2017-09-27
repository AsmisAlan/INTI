using GeoUsers.Services.Model.DataTransfer;

namespace GeoUsersUI.Models.ViewModels.Helpers
{
    public class FilterStatus : BaseNotifierEntity
    {
        private string tipoOrganizacionStatus { get; set; }
        private string sectorStatus { get; set; }
        private string rubroStatus { get; set; }
        private string localidadStatus { get; set; }

        public FilterData Filter { get; set; }

        public string TipoOrganizacionStatus
        {
            get
            {
                return tipoOrganizacionStatus;
            }
            set
            {
                tipoOrganizacionStatus = value;

                OnPropertyChanged(nameof(TipoOrganizacionStatus));
            }
        }

        public string SectorStatus
        {
            get
            {
                return sectorStatus;
            }
            set
            {
                sectorStatus = value;

                OnPropertyChanged(nameof(SectorStatus));
            }
        }

        public string RubroStatus
        {
            get
            {
                return rubroStatus;
            }
            set
            {
                rubroStatus = value;

                OnPropertyChanged(nameof(RubroStatus));
            }
        }

        public string LocalidadStatus
        {
            get
            {
                return localidadStatus;
            }
            set
            {
                localidadStatus = value;

                OnPropertyChanged(nameof(LocalidadStatus));
            }
        }

        public FilterStatus()
        {
            Filter = new FilterData();

            UpdateStatuses();
        }

        public void UpdateStatuses()
        {
            TipoOrganizacionStatus = $"Tipo Organizacion ({Filter.TipoOrganizacionIds.Count})";
            SectorStatus = $"Sector ({Filter.SectorIds.Count})";
            RubroStatus = $"Rubro ({Filter.RubroIds.Count})";
            LocalidadStatus = $"Localidad ({Filter.LocalidadIds.Count})";
        }
    }
}
