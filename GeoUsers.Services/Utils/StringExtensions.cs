namespace GeoUsers.Services.Utils
{
    public static class StringExtensions
    {
        public static string EscapeQuoatationMarks(this string value)
        {
            return value.Replace("\"", "\\\"").Replace("'", "\\\'");
        }
    }
}
