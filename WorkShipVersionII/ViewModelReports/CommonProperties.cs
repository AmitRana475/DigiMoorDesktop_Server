using DataBuildingLayer;
using GalaSoft.MvvmLight;

namespace WorkShipVersionII.ViewModelReports
{
    public class CommonProperties : ViewModelBase
    {

        //private static CommonProperties _instance = null;
        //public static CommonProperties GetInstance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //            _instance = new CommonProperties();

        //        return _instance;
        //    }
        //    set
        //    {
        //        _instance = value;

        //    }

        //}

        private bool reportVisibles = false;
        public bool ReportVisibles
        {
            get
            {
                if (!reportVisibles)
                {
                    reportVisibles = false;
                }
                return reportVisibles;
            }
            set
            {
                reportVisibles = value;
                RaisePropertyChanged("ReportVisibles");
            }
        }

       


        private decimal totalhours = 0;
        public decimal TotalHours
        {
            get
            {

                return totalhours;
            }
            set
            {
                totalhours = value;
                RaisePropertyChanged("TotalHours");
            }
        }

        private decimal restHours = 24;
        public decimal RestHours
        {
            get
            {

                return restHours;
            }
            set
            {
                restHours = value;
                RaisePropertyChanged("RestHours");
            }
        }
        private decimal restHour7day = 168;
        public decimal RestHour7day
        {
            get
            {

                return restHour7day;
            }
            set
            {
                restHour7day = value;
                RaisePropertyChanged("RestHour7day");
            }
        }

        private string remarks;
        public string Remarks
        {
            get
            {

                return remarks;
            }
            set
            {
                remarks = value;
                RaisePropertyChanged("Remarks");
            }
        }

        private string nonConfirmities;
        public string NonConfirmities
        {
            get
            {

                return nonConfirmities;
            }
            set
            {
                nonConfirmities = value;
                RaisePropertyChanged("NonConfirmities");
            }
        }


        private string remarksPlanner;
        public string RemarksPlanner
        {
            get
            {

                return remarksPlanner;
            }
            set
            {
                remarksPlanner = value;
                RaisePropertyChanged("RemarksPlanner");
            }
        }

        private string nonConfirmitiesPlanner;
        public string NonConfirmitiesPlanner
        {
            get
            {

                return nonConfirmitiesPlanner;
            }
            set
            {
                nonConfirmitiesPlanner = value;
                RaisePropertyChanged("NonConfirmitiesPlanner");
            }
        }

        private string deviationsVisibles;
        public string DeviationsVisibles
        {
            get
            {
               
                return deviationsVisibles;
            }
            set
            {
                deviationsVisibles = value;
                RaisePropertyChanged("DeviationsVisibles");
            }
        }


        private CrewDetailClass userDetail;
        public CrewDetailClass GetUserDetail
        {
            get
            {
                return userDetail;
            }
            set
            {
                userDetail = value;
                RaisePropertyChanged("NonConfirmities");
            }
        }







        //..............



        private string overView;
        public string OverView
        {
            get
            {
                if (string.IsNullOrEmpty(overView))
                    overView = "Visible";

                return overView;
            }
            set
            {
                overView = value;
                RaisePropertyChanged("OverView");
            }
        }

        private string crewWorkHours;
        public string CrewWorkHours
        {
            get
            {
                if (string.IsNullOrEmpty(crewWorkHours))
                    crewWorkHours = "Visible";

                return crewWorkHours;
            }
            set
            {
                crewWorkHours = value;
                RaisePropertyChanged("CrewWorkHours");
            }
        }

        private string nonConfirmity;
        public string NonConfirmity
        {
            get
            {
                if (string.IsNullOrEmpty(nonConfirmity))
                    nonConfirmity = "Visible";

                return nonConfirmity;
            }
            set
            {
                nonConfirmity = value;
                RaisePropertyChanged("NonConfirmity");
            }
        }

        private string overTime;
        public string OverTime
        {
            get
            {
                if (string.IsNullOrEmpty(overTime))
                    overTime = "Visible";

                return overTime;
            }
            set
            {
                overTime = value;
                RaisePropertyChanged("OverTime");
            }
        }


        

       




    }
}
