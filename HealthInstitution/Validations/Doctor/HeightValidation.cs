using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HealthInstitution.Validations.Doctor
{
    public class HeightValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double height = 0;
            try
            {
                height = Convert.ToDouble(value);
            }
            catch
            {
                return new ValidationResult(false, "Height must be a number");
            }
            if(height <= 0)
            {
                return new ValidationResult(false, "Height must be a positive number");
            }
            return ValidationResult.ValidResult;
        }
    }
}
