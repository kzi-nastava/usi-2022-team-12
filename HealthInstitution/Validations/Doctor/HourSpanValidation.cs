using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HealthInstitution.Validations.Doctor
{
    public  class HourSpanValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int hours = 0;
            try
            {
                hours = Convert.ToInt32(value);
            }
            catch
            {
                return new ValidationResult(false, "Hour span must be a whole number");
            }
            if (hours <= 0)
            {
                return new ValidationResult(false, "Hour span must be a positive number");
            }
            return ValidationResult.ValidResult;
        }
    }
}

