using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WorkShipVersionII.ViewModelReports
{
    public class CrewWorkHoursViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public CrewWorkHoursViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            yearname = sc.GetYear();
            monthname = sc.GetMonth();
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


        private static string syearname;
        public string SYearName
        {
            get
            {
                if (syearname == null)
                    syearname = DateTime.Now.Year.ToString();

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

                return smonthname;
            }

            set
            {
                smonthname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SMonthName"));
            }
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


    }
}
