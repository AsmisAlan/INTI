namespace GeoUsers.Services.Model.Entities
{
    public class TipoOrganizacion : BaseEntity
    {
        public virtual string Tipo { get; set; }

        public override string ToString()
        {
            return Tipo;
        }
    }
}
