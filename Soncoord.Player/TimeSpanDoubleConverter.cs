using System;
using System.Globalization;
using System.Windows.Data;

namespace Soncoord.Player
{
    public class TimeSpanDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = (TimeSpan)value;
            if (timeSpan == null)
            {
                return 0d;
            }

            return timeSpan.TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
