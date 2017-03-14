namespace GeoUsers.Services.Model.Entities
{
    public class Archivo : BaseEntity
    {
        public virtual string Ruta { get; set; }

        public virtual string Nombre { get; set; }

        public virtual string Extension { get; set; }
    }
}
