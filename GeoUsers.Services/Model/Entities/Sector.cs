using System.Collections.Generic;

namespace GeoUsers.Services.Model.Entities
{
    public class Sector : BaseEntity
    {
        public virtual string Nombre { get; set; }

        public virtual IEnumerable<Rubro> Rubros { get; set; }
    }
}
