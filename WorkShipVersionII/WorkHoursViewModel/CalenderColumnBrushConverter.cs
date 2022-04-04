using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class CalenderColumnBrushConverter : IValueConverter
    {
        private readonly ShipmentContaxt sc;

        public CalenderColumnBrushConverter()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string input = value as string;

            if (!string.IsNullOrEmpty(input))
            {
                string s1, s2, str = "";
                s1 = input.ToString().Substring(0, 2);
                s2 = input.ToString().Substring(input.ToString().Length - 3);

                if (s2 == "IDL")
                    str = s1.Trim() + s2;
                else
                    str = s1.Trim();

                DateTime monthid = System.Convert.ToDateTime(s1 + "-" + CalenderBackgroundClass.MonthName + "-" + CalenderBackgroundClass.Years);
                DateTime dates = System.Convert.ToDateTime(monthid);

                var planner = CalenderBackgroundClass.Planner;

                if (planner == "Actual Calendar")
                {
                    var data = WorkHoursList.Where(x => x.WRID == str && x.dates.ToShortDateString() == dates.ToShortDateString() && x.UserName == CalenderBackgroundClass.UserName && x.Position == CalenderBackgroundClass.Position).FirstOrDefault();

                    if (data != null)
                    {
                        if (!string.IsNullOrEmpty(data.NonConfirmities))
                        {
                            //return new SolidColorBrush(Colors.Red); 255	90	99
                            return new SolidColorBrush(Color.FromRgb(255, 90, 99));
                        }
                        else if (data.TotalHours > 0)
                        {

                            //return new SolidColorBrush(Color.FromRgb(0, 51, 102));
                            return new SolidColorBrush(Color.FromRgb(113, 219, 147));

                        }
                        else
                        {
                            return DependencyProperty.UnsetValue;
                        }
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }
                else
                {
                    //it's make for planner
                    var data = WorkHoursListPlanner.Where(x => x.WRID == str && x.dates.ToShortDateString() == dates.ToShortDateString() && x.UserName == CalenderBackgroundClass.UserName && x.Position == CalenderBackgroundClass.Position).FirstOrDefault();

                    if (data != null)
                    {
                        if (!string.IsNullOrEmpty(data.NonConfirmities))
                        {
                            //return new SolidColorBrush(Colors.Red); 255	90	99
                            return new SolidColorBrush(Color.FromRgb(255, 90, 99));
                        }
                        else if (data.TotalHours > 0)
                        {

                            //return new SolidColorBrush(Color.FromRgb(0, 51, 102));
                            return new SolidColorBrush(Color.FromRgb(113, 219, 147));

                        }
                        else
                        {
                            return DependencyProperty.UnsetValue;
                        }
                    }
                    else
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }



            }
            else
            {
                return DependencyProperty.UnsetValue;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private List<WorkHoursClass> _WorkHoursList;
        public List<WorkHoursClass> WorkHoursList
        {
            get
            {
                if (_WorkHoursList == null)
                {
                    using (ShipmentContaxt sc1 = new ShipmentContaxt())
                    {
                        _WorkHoursList = sc1.WorkHourss.Where(x => x.UserName.Equals(CalenderBackgroundClass.UserName.Trim()) && x.Position.Equals(CalenderBackgroundClass.Position.Trim())).ToList();
                    }
                }
                return _WorkHoursList;
            }
            set
            {
                _WorkHoursList = value;

            }
        }

        private List<WorkHoursPlannerClass> _WorkHoursListPlanner;
        public List<WorkHoursPlannerClass> WorkHoursListPlanner
        {
            get
            {
                if (_WorkHoursListPlanner == null)
                {
                    using (ShipmentContaxt sc1 = new ShipmentContaxt())
                    {
                        _WorkHoursListPlanner = sc1.WorkHourPlanners.Where(x => x.UserName.Equals(CalenderBackgroundClass.UserName.Trim()) && x.Position.Equals(CalenderBackgroundClass.Position.Trim())).ToList();
                    }
                }
                return _WorkHoursListPlanner;
            }
            set
            {
                _WorkHoursListPlanner = value;

            }
        }


    }
}
