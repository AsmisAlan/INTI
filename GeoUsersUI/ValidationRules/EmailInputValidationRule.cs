using System.Globalization;
using System.Windows.Controls;

namespace GeoUsersUI.ValidationRules
{
    public class EmailInputValidationRule : ValidationRule
    {
        public EmailInputValidationRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var parsedValue = (string)value;

            if (parsedValue.Contains("@"))
            {
                return new ValidationResult(true, null);
            }
            return new ValidationResult(false, "La dirección de email no es válida.");
        }
    }
}