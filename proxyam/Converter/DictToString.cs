using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using proxyam.Model;

namespace proxyam.Converter
{
    class DictToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var keyValue = (KeyValuePair<string, FilterModel.CountryCount>)value;
            return keyValue.Key + "[" + keyValue.Value.Count.ToString() + "]";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
