using System.Collections.Generic;

namespace GeoUsers.Services.Model.DataTransfer
{
    public class FilterData
    {
        public ICollection<int> TipoOrganizacionIds { get; set; }

        public ICollection<int> SectorIds { get; set; }

        public ICollection<int> RubroIds { get; set; }

        public ICollection<int> LocalidadIds { get; set; }

        public bool UsuarioInti { get; set; }

        public FilterData()
        {
            TipoOrganizacionIds = new List<int>();
            SectorIds = new List<int>();
            RubroIds = new List<int>();
            LocalidadIds = new List<int>();
        }
    }
}
