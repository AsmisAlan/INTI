using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeoUsersUI.Utils
{
    public static class ValidationUtils
    {
        public static bool ValidateCoordinates(string coordinates)
        {
            var regex = new Regex(@"^\-?[0-9]+(?:\.[0-9]+)?$");

            var matches = regex.IsMatch(coordinates);

            if (!matches)
            {
                MessageBoxUtils.Error("Las coordenadas ingresadas son invalidas. \n Deben tener la forma -xx.xx o xx.xx");
            }

            return matches;
        }
    }
}
