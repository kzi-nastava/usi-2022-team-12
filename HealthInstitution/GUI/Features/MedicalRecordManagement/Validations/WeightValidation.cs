using System;
using System.Globalization;
using System.Windows.Controls;

namespace HealthInstitution.GUI.Features.MedicalRecordManagement.Validations
{
    public class WeightValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double weight = 0;
            try
            {
                weight = Convert.ToDouble(value);
            }
            catch
            {
                return new ValidationResult(false, "Weight must be a number");
            }
            if (weight <= 0)
            {
                return new ValidationResult(false, "Weight must be a positive number");
            }
            return ValidationResult.ValidResult;
        }
    }
}
