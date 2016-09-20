namespace GeoUsers.Services.Model.Entities
{
    public class Localidad : BaseEntity
    {
        public virtual int CodigoPostal { get; set; }

        public virtual string Nombre { get; set; }

        public override string ToString()
        {
            return $"{Nombre} ({CodigoPostal})";
        }
    }
}
