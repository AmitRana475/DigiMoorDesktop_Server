using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace WorkShipVersionII.Converter
{
    public class ButtonEnableConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null)
            {
                bool status = false;
                for (int i = 0; i < values.Length; i++)
                {

                    if (Regex.IsMatch(values[i].ToString(), @"^\d{1,16}(\.\d{1,6})?$"))
                        status = true;
                    else if (Regex.IsMatch(values[i].ToString(), @"^-?[0-9]\d*(\.\d+)?$"))
                        status = true;
                    else
                    {
                        status = false;
                        break;
                    }
                }

                return status;
            }
            else
                return false;
        }


        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    throw new NotImplementedException();
        //}

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
