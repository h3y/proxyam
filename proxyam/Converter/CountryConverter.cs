using System;
using System.Globalization;
using System.Windows.Data;

namespace proxyam.Converter
{
    class CountryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Uri($"../Resources/flags/{value?.ToString().ToLower() ?? "none"}.png",UriKind.Relative);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

