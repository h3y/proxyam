using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace proxyam.Converter
{
    internal class CountryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var filePath = new Uri($"../Resources/flags/{value?.ToString().ToLower() ?? "none"}.png", UriKind.Relative);
            return filePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}