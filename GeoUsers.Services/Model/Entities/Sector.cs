using System.Collections.Generic;

namespace GeoUsers.Services.Model.Entities
{
    public class Sector : BaseEntity
    {
        public virtual string Nombre { get; set; }

        public virtual Archivo Icono { get; set; }

        public virtual ISet<Rubro> Rubros { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
