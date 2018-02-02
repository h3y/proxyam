using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace proxyam.Converter
{
    class TariffToRating:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var count = 0;

            switch (value.ToString())
            {
                case "Silver Pack": count = 1; break;
                case "Platinum Pack": count = 2; break;
                case "Unlimited Pack": count = 3; break;
                case "Luxury Pack": count = 4; break;
                case "Absolute Pack": count = 5; break;
                default: count = 0; break;
            }

            var stars = new List<Path>();

            for (int i = 0; i < 5; i++)
            {
                stars.Add(new Path { Style = Application.Current.Resources["RaingStyle"] as Style });
                if (i < count)
                    stars[i].Fill = (SolidColorBrush)new BrushConverter().ConvertFromString("#2196f3");
            }
           
            return stars;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
