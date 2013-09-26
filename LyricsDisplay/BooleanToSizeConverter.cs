using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LyricsDisplay
{
    public class BooleanToSizeConverter : IValueConverter
    {
        public object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return (Boolean)value ? (Int32)25 : (Int32)22;
        }
        public object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
