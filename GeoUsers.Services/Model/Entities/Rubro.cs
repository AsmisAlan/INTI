namespace GeoUsers.Services.Model.Entities
{
    public class Rubro : BaseEntity
    {
        public virtual string Nombre { get; set; }

        public virtual Sector Sector { get; set; }
    }
}
