using System;
using System.Globalization;
using System.Windows.Controls;

namespace TestApplication.ValidationRules
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
                int.Parse((string)value);
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Input must be a number.");
            }

            return new ValidationResult(true, null);
        }
    }
}