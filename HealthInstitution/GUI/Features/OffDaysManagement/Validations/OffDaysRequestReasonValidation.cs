using System.Globalization;
using System.Windows.Controls;

namespace HealthInstitution.GUI.Features.OffDaysManagement.Validations
{
    public class OffDaysRequestReasonValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? reason = value.ToString();
            if (string.IsNullOrEmpty(reason))
            {
                return new ValidationResult(false, "Off days request reason cannot be empty");
            }
            return ValidationResult.ValidResult;
        }
    }
}
