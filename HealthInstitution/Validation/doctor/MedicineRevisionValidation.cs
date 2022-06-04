using System.Globalization;
using System.Windows.Controls;

namespace HealthInstitution.Validation.doctor
{
    public class MedicineRevisionValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? revision = value.ToString();
            if (string.IsNullOrWhiteSpace(revision))
            {
                return new ValidationResult(false, "Revision comment cannot be empty");
            }
            return ValidationResult.ValidResult;
        }
    }
}
