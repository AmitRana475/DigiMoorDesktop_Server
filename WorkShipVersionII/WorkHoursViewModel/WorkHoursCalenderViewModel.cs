using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
    public class WorkHoursCalenderViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        static CrewDetailClass obj1;
        static DateTime today = DateTime.Now.Date;
   
        public WorkHoursCalenderViewModel(CrewDetailClass obj)
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            obj1 = sc.CrewDetails.Where(x => x.UserName.Equals(obj.UserName)).FirstOrDefault();
            yearname = GetYear();
            monthname = GetMonth();

            Name = obj1.UserName;
            RaisePropertyChanged("Name");
            username = obj.name;
            RaisePropertyChanged("UserName");
            position = obj.position;
            RaisePropertyChanged("Position");
            actualorPlanner = obj.ActualorPlanner + " " + "Calendar";
            RaisePropertyChanged("ActualorPlanner");

            //showChildwindowTimeSlotCommand = new RelayCommand<object>(ShowPopupTimeSlot);

            CalenderBackgroundClass.UserName = obj1.UserName;
            CalenderBackgroundClass.Position = obj.position;
            CalenderBackgroundClass.MonthName = SMonthName;
            CalenderBackgroundClass.Years = SYearName;


        }


        //private ICommand showChildwindowTimeSlotCommand;
        //public ICommand ShowChildwindowTimeSlotCommand
        //{
        //    get { return showChildwindowTimeSlotCommand; }

        //}


        public static void ShowPopupTimeSlot(object obj)
        {

            string s1, s2, str = "";
            s1 = obj.ToString().Substring(0, 2);
            s2 = obj.ToString().Substring(obj.ToString().Length - 3);

            if (s2 == "IDL")
                str = s1.Trim() + s2;
            else
                str = s1.Trim();

            var monthid = Convert.ToDateTime(s1 + "-" + smonthname + "-" + syearname);
            DateTime dates = Convert.ToDateTime(monthid);
            var user = new TSUserClass
            {
                DateId = str,
                Dt11 = dates,
                UserName = obj1.UserName,
                FullName = obj1.name,
                Position = obj1.position,
                Department = obj1.department

            };
            if (s2 == "IDL")
                user.IDL = s2;


            var CheckPlanner = actualorPlanner.ToString();

            if (obj1.chkyoungs)
            {
                if (CheckPlanner == "Planner Calendar")
                {
                    if (dates.Date >= DateTime.Now.Date)
                    {

                        TimeSlotYoungPlannerViewModel vmc = new TimeSlotYoungPlannerViewModel(user);
                        ChildWindowManager.Instance.ShowChildWindow(new TimeSlotYoungPlannerView() { DataContext = vmc });
                    }
                    else
                        MessageBox.Show("Sorry! You cannot create past date planners.", "Crew Planner", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
                else
                {
                    if (obj1.ServiceTo.Date >= dates.Date)
                    {
                        TimeSlotYoungViewModel vmc = new TimeSlotYoungViewModel(user);
                        ChildWindowManager.Instance.ShowChildWindow(new TimeSlotYoungView() { DataContext = vmc });
                    }
                    else
                        MessageBox.Show("Sorry! This Date is not between Service Date From " + obj1.ServiceFrom.ToString("dd-MMM-yyyy") + " To " + obj1.ServiceTo.ToString("dd-MMM-yyyy"), "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {

                if (CheckPlanner == "Planner Calendar")
                {
                    if (dates.Date >= today)
                    {

                        TimeSlotPlannerViewModel vmc = new TimeSlotPlannerViewModel(user);
                        ChildWindowManager.Instance.ShowChildWindow(new TimeSlotPlannerView() { DataContext = vmc });
                    }
                    else
                        MessageBox.Show("Sorry! You cannot create past date planners.", "Crew Planner", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if (obj1.ServiceTo.Date >= dates.Date)
                    {
                        TimeSlotViewModel vmc = new TimeSlotViewModel(user);
                        ChildWindowManager.Instance.ShowChildWindow(new TimeSlotView() { DataContext = vmc });
                    }
                    else
                        MessageBox.Show("Sorry! This Date is not between Service Date From " + obj1.ServiceFrom.ToString("dd-MMM-yyyy") + " To " + obj1.ServiceTo.ToString("dd-MMM-yyyy"), "", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void BindCalender(string syearname, string smonthname)
        {

            //syearname = syearname ?? DateTime.Now.Year.ToString();
            //smonthname = smonthname ?? DateTime.Now.ToString("MMMM");



            //int years =  Convert.ToInt32(syearname);
            //var monthid = DateTime.ParseExact(smonthname, "MMMM", CultureInfo.CurrentCulture).Month;
            //int days = DateTime.DaysInMonth(years, monthid);


            //string weeks = "";
            //string noofdays = "";
            //for (int i = 1; i <= days; i++)
            //{

            //    var birthDate = new DateTime(years, monthid, i);
            //    var thisYear = new DateTime(years, birthDate.Month, birthDate.Day);
            //    var dayOfWeek = thisYear.ToString("ddd");
            //    weeks = dayOfWeek;

            //    string dateis = i + " " + Environment.NewLine + weeks;
            //    int valueNClenth = dateis.Length;

            //    noofdays += dateis + ",";


            //}
            string[] arr = sc.CommonCalender(syearname, smonthname);

            int fularr = arr.Count();
            loadCalender.Clear();

            for (int k = 0; k < fularr; k++)
            {


                loadCalender.Add(new GridClass() { Day1 = fularr - k >= 0 ? arr[k] : string.Empty, Day2 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day3 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day4 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day5 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day6 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty, Day7 = fularr - k >= 2 ? arr[k = (k + 1)] : string.Empty });


            }

        }

        public static ObservableCollection<GridClass> loadCalender = new ObservableCollection<GridClass>();
        public ObservableCollection<GridClass> LoadCalender
        {
            get
            {
                return loadCalender;
            }
            set
            {
                loadCalender = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadCalender"));

            }
        }

        private static ObservableCollection<string> yearname = new ObservableCollection<string>();
        public ObservableCollection<string> YearName
        {
            get
            {
                return yearname;
            }
            set
            {
                yearname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("YearName"));
            }
        }

        private static ObservableCollection<string> monthname = new ObservableCollection<string>();
        public ObservableCollection<string> MonthName
        {
            get
            {
                return monthname;
            }
            set
            {
                monthname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MonthName"));
            }
        }



        private string username;
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                RaisePropertyChanged("UserName");
            }
        }


        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }

        private string position;
        public string Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                RaisePropertyChanged("Position");
            }
        }

        private static string actualorPlanner;
        public string ActualorPlanner
        {
            get
            {
                CalenderBackgroundClass.Planner = actualorPlanner;
                return actualorPlanner;
            }
            set
            {
                actualorPlanner = value;
                RaisePropertyChanged("ActualorPlanner");
            }
        }


        private static string syearname;
        public string SYearName
        {
            get
            {
                if (syearname == null)
                    syearname = DateTime.Now.Year.ToString();
                CalenderBackgroundClass.Years = syearname;
                BindCalender(syearname, smonthname);
                return syearname;
            }

            set
            {
                syearname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SYearName"));
            }
        }

        private static string smonthname;
        public string SMonthName
        {
            get
            {
                if (smonthname == null)
                    smonthname = DateTime.Now.ToString("MMMM");
                CalenderBackgroundClass.MonthName = smonthname;
                BindCalender(syearname, smonthname);
                return smonthname;
            }

            set
            {
                smonthname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SMonthName"));
            }
        }

        private ObservableCollection<string> GetMonth()
        {
            var years = new ObservableCollection<string>();
            for (int i = 0; i < 12; i++)
            {
                years.Add(CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[i]);

            }
            return years;
        }

        private ObservableCollection<string> GetYear()
        {
            var years = new ObservableCollection<string>();
            int year = DateTime.Now.Year;
            for (int i = year - 7; i <= year + 7; i++)
            {
                years.Add(i.ToString());
            }
            return years;
        }



        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }


}
