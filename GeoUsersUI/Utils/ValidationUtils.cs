using System;

namespace GeoUsersUI.Utils
{
    public static class ValidationUtils
    {
        public static bool ValidateCoordinates(string coordinates)
        {
            double result;

            var isValid = Double.TryParse(coordinates, out result);

            if (!isValid)
            {
                MessageBoxUtils.Error("Las coordenadas ingresadas son invalidas.\nDeben ingresarse en valor decimal: (-)xx.xx");
            }

            return isValid;
        }
    }
}
