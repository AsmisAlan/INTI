namespace GeoUsers.Services.Model.Entities
{
    public class Usuario : BaseEntity
    {
        public virtual string LoginId { get; set; }

        public virtual string Password { get; set; }
    }
}
