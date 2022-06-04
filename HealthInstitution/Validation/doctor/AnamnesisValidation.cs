using System.Globalization;
using System.Windows.Controls;

namespace HealthInstitution.Validation.doctor
{
    public class AnamnesisValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? anamnesis = value.ToString();
            if (string.IsNullOrWhiteSpace(anamnesis))
            {
                return new ValidationResult(false, "Anamnesis cannot be empty");
            }
            return ValidationResult.ValidResult;
        }
    }
}
