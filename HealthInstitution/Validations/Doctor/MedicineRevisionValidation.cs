using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HealthInstitution.Validations.Doctor
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
