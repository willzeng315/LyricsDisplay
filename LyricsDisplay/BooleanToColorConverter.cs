using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Windows.UI;

namespace LyricsDisplay
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var val = (bool)value;
            return new SolidColorBrush(val ? Colors.Red : Colors.White);
        }
        public object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return value;
        }

    }
}
