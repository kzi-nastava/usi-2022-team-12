using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HealthInstitution.Validations.Doctor
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
