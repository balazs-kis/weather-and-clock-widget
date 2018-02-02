using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WeatherAndClockWidget
{
    public class BoolToBackgroundColorConverter : IValueConverter
    {
        private static readonly SolidColorBrush GrayBrush;
        private static readonly SolidColorBrush TransparentBrush;

        static BoolToBackgroundColorConverter()
        {
            TransparentBrush = new SolidColorBrush(Colors.Transparent);
            GrayBrush = new SolidColorBrush(Color.FromArgb(System.Convert.ToByte("51"), System.Convert.ToByte("0"), System.Convert.ToByte("0"), System.Convert.ToByte("0")));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                throw new NullReferenceException("The value to convert to Background color was null");
            }

            var convertedValue = (bool)value;
            return convertedValue ? GrayBrush : TransparentBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}