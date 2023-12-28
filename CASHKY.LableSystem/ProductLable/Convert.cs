using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace CASHKY.LableSystem.ProductLable
{
    public class TopMarginConvert : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double top = ((Thickness)value).Top;
            return top / 96 * 25.4;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness thickness = new Thickness(5, Convert.ToDouble(value) / 25.4 * 96, 5, 5);
            return thickness;
        }
    }
    public class HeightConvert : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value / 96.0 * 25.4;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var r = Convert.ToDouble(value) / 25.4 * 96;
            return r;
        }
    }
}
