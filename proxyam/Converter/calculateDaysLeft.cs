using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace proxyam.Converter
{
    class CalculateDaysLeft:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "undefine";

            DateTime first = DateTime.Today;
            DateTime last = DateTime.Parse(value.ToString(),DateTimeFormatInfo.InvariantInfo);
            TimeSpan elapsed = last.AddDays(1).Subtract(first);
            return $@"{elapsed.Days.ToString()} дней";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
