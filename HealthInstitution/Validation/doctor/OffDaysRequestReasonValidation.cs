using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HealthInstitution.Validation.doctor
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
