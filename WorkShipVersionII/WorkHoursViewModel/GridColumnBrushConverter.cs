using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class GridColumnBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = value as string;
            DateTime dt1 = DateTime.Now.Date;

            if (input != "No Entry")
            {
                var dt = System.Convert.ToDateTime(input);
                if (dt.Date.AddDays(1) < dt1)
                    return new SolidColorBrush(Colors.Red);
                else
                    return DependencyProperty.UnsetValue;
            }
            else
                return new SolidColorBrush(Colors.Red);




        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
