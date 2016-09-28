using System;
using System.Globalization;
using System.Windows.Controls;

namespace GeoUsersUI.ValidationRules
{
    public class NumericInputValidationRule : ValidationRule
    {
        public NumericInputValidationRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                Int32.Parse((string)value);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Debe introducir un valor numerico");
            }

            return new ValidationResult(true, null);
        }
    }
}
