using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WorkShipVersionII.Converter
{
    public class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value.ToString() == "Notification")
                {
                    return new SolidColorBrush(new Color()
                    {
                        A = 255,
                        R = 27,
                        G = 161,
                        B = 226
                    });
                }
                else
                {
                    return new SolidColorBrush(Colors.Green);
                }

            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
