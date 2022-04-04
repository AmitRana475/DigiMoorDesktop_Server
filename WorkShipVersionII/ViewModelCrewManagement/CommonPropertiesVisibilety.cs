using GalaSoft.MvvmLight;


namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class CommonPropertiesVisibilety : ViewModelBase
    {

        private string crewManagement;
        public string CrewManagement
        {
            get
            {
                if (string.IsNullOrEmpty(crewManagement))
                    crewManagement = "Visible";

                return crewManagement;
            }
            set
            {
                crewManagement = value;
                RaisePropertyChanged("CrewManagement");
            }
        }

        private string crewRank;
        public string CrewRank
        {
            get
            {
                if (string.IsNullOrEmpty(crewRank))
                    crewRank = "Visible";
                return crewRank;
            }
            set
            {
                crewRank = value;
                RaisePropertyChanged("CrewRank");
            }
        }

        private string department;
        public string Department
        {
            get
            {
                if (string.IsNullOrEmpty(department))
                    department = "Visible";

                return department;
            }
            set
            {
                department = value;
                RaisePropertyChanged("Department");
            }
        }


        private string holidayGroup;
        public string HolidayGroup
        {
            get
            {
                if (string.IsNullOrEmpty(holidayGroup))
                    holidayGroup = "Visible";

                return holidayGroup;
            }
            set
            {
                holidayGroup = value;
                RaisePropertyChanged("HolidayGroup");
            }
        }


        private string hOD;
        public string HOD
        {
            get
            {
                if (string.IsNullOrEmpty(hOD))
                    hOD = "Visible";

                return hOD;
            }
            set
            {
                hOD = value;
                RaisePropertyChanged("HOD");
            }
        }

        private string resetPassword;
        public string ResetPassword
        {
            get
            {
                if (string.IsNullOrEmpty(resetPassword))
                    resetPassword = "Visible";

                return resetPassword;
            }
            set
            {
                resetPassword = value;
                RaisePropertyChanged("ResetPassword");
            }
        }


        private string freezeUnfreezeAll;
        public string FreezeUnfreezeAll
        {
            get
            {
                if (string.IsNullOrEmpty(freezeUnfreezeAll))
                    freezeUnfreezeAll = "Visible";

                return freezeUnfreezeAll;
            }
            set
            {
                freezeUnfreezeAll = value;
                RaisePropertyChanged("FreezeUnfreezeAll");
            }
        }
    }
}
