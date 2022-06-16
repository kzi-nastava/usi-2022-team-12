using System;
using System.Globalization;
using System.Windows.Data;

namespace HealthInstitution.GUI.Converters
{
    public class BoolToYNConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (bool)value;
            if (val)
            {
                return "Yes";
            }
            return "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
