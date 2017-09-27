using System.Collections;

namespace GeoUsers.Services.Model.Interfaces
{
    public interface IBaseIdentifier : IEqualityComparer
    {
        int Id { get; set; }
    }
}
