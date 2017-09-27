namespace GeoUsersUI.Utils
{
    public static class ObjectExtensions
    {
        public static void Update(this object source, object newObject)
        {
            var properties = source.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(newObject);

                property.SetValue(source, value);
            }
        }
    }
}
