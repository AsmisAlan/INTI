using GeoUsers.Services.Model.Interfaces;

namespace GeoUsers.Services.Model.DataTransfer
{
    public class BaseComparableNotifierEntity : BaseNotifierEntity, IBaseIdentifier
    {
        public int Id { get; set; }

        public new bool Equals(object x, object y)
        {
            return ((BaseComparableNotifierEntity)x).Id == ((BaseComparableNotifierEntity)y).Id;
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }
}
