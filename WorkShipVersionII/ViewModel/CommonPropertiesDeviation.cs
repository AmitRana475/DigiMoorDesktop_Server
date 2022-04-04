using GalaSoft.MvvmLight;

namespace WorkShipVersionII.ViewModel
{
    public class CommonPropertiesDeviation : ViewModelBase
    {
        public CommonPropertiesDeviation()
        {

        }

        private static string deviation;
        public string Deviation
        {
            get
            {
                if (deviation == null)
                    deviation = "Visible";
                return deviation;
            }
            set
            {
                deviation = value;
                RaisePropertyChanged("Deviation");
            }
        }

        private static string deviationRed;
        public string DeviationRed
        {
            get
            {
                if (deviationRed == null)
                    deviationRed = "Hidden";
                return deviationRed;
            }
            set
            {
                deviationRed = value;
                RaisePropertyChanged("DeviationRed");
            }
        }


        private static string expiryMessages;
        public string ExpiryMessages
        {
            get
            {

                return expiryMessages;
            }
            set
            {
                expiryMessages = value;
                RaisePropertyChanged("ExpiryMessages");
            }
        }




        private static string notificationVisible;
        public string NotificationVisible
        {
            get
            {
                if (string.IsNullOrEmpty(notificationVisible))
                    notificationVisible = "Visible";
                //notificationVisible = "Collapsed"; 
                return notificationVisible;
            }
            set
            {
                notificationVisible = value;
                RaisePropertyChanged("NotificationVisible");
            }
        }

        private static string crewManagementVisible;
        public string CrewManagementVisible
        {
            get
            {
                if (string.IsNullOrEmpty(crewManagementVisible))
                    crewManagementVisible = "Visible";
                return crewManagementVisible;
            }
            set
            {
                crewManagementVisible = value;
                RaisePropertyChanged("CrewManagementVisible");
            }
        }

        private static string workRestHoursVisible;
        public string WorkRestHoursVisible
        {
            get
            {
                if (string.IsNullOrEmpty(workRestHoursVisible))
                    workRestHoursVisible = "Visible";

                return workRestHoursVisible;
            }
            set
            {
                workRestHoursVisible = value;
                RaisePropertyChanged("WorkRestHoursVisible");
            }
        }


        private static string reportsVisible;
        public string ReportsVisible
        {
            get
            {
                if (string.IsNullOrEmpty(reportsVisible))
                    reportsVisible = "Visible";

                return reportsVisible;
            }
            set
            {
                reportsVisible = value;
                RaisePropertyChanged("ReportsVisible");
            }
        }


        private static string administrationVisible;
        public string AdministrationVisible
        {
            get
            {
                if (string.IsNullOrEmpty(administrationVisible))
                    administrationVisible = "Visible";

                return administrationVisible;
            }
            set
            {
                administrationVisible = value;
                RaisePropertyChanged("AdministrationVisible");
            }
        }

        private static string certificationVisible;
        public string CertificationVisible
        {
            get
            {
                if (string.IsNullOrEmpty(certificationVisible))
                    certificationVisible = "Visible";

                return certificationVisible;
            }
            set
            {
                certificationVisible = value;
                RaisePropertyChanged("CertificationVisible");
            }
        }







        private static string notiWidth;
        public string NotiWidth
        {
            get
            {
                if (string.IsNullOrEmpty(notiWidth))
                    notiWidth = "*";
                return notiWidth;
            }
            set
            {
                notiWidth = value;
                RaisePropertyChanged("NotiWidth");
            }
        }

        private static string crewWidth;
        public string CrewWidth
        {
            get
            {
                if (string.IsNullOrEmpty(crewWidth))
                    crewWidth = "*";

                return crewWidth;
            }
            set
            {
                crewWidth = value;
                RaisePropertyChanged("CrewWidth");
            }
        }


        private static string workWidth;
        public string WorkWidth
        {
            get
            {
                if (string.IsNullOrEmpty(workWidth))
                    workWidth = "*";

                return workWidth;
            }
            set
            {
                workWidth = value;
                RaisePropertyChanged("WorkWidth");
            }
        }

        private static string reportWidth;
        public string ReportWidth
        {
            get
            {
                if (string.IsNullOrEmpty(reportWidth))
                    reportWidth = "*";

                return reportWidth;
            }
            set
            {
                reportWidth = value;
                RaisePropertyChanged("ReportWidth");
            }
        }


        private static string adminWidth;
        public string AdminWidth
        {
            get
            {
                if (string.IsNullOrEmpty(adminWidth))
                    adminWidth = "*";

                return adminWidth;
            }
            set
            {
                adminWidth = value;
                RaisePropertyChanged("AdminWidth");
            }
        }

        private static string certiWidth;
        public string CertiWidth
        {
            get
            {
                if (string.IsNullOrEmpty(certiWidth))
                    certiWidth = "*";

                return certiWidth;
            }
            set
            {
                certiWidth = value;
                RaisePropertyChanged("CertiWidth");
            }
        }



        private static string cerNotification;
        public string CerNotification
        {
            get
            {
                if (string.IsNullOrEmpty(cerNotification))
                    cerNotification = "Visible";

                return cerNotification;
            }
            set
            {
                cerNotification = value;
                RaisePropertyChanged("CerNotification");
            }
        }

        private static string oCNotification;
        public string OCNotification
        {
            get
            {
                if (string.IsNullOrEmpty(oCNotification))
                    oCNotification = "Visible";

                return oCNotification;
            }
            set
            {
                oCNotification = value;
                RaisePropertyChanged("OCNotification");
            }
        }


        private static string cerNotiWidth;
        public string CerNotiWidth
        {
            get
            {
                if (string.IsNullOrEmpty(cerNotiWidth))
                    cerNotiWidth = "34*";

                return cerNotiWidth;
            }
            set
            {
                cerNotiWidth = value;
                RaisePropertyChanged("CerNotiWidth");
            }
        }

        private static string oCNotiWidth;
        public string OCNotiWidth
        {
            get
            {
                if (string.IsNullOrEmpty(oCNotiWidth))
                    oCNotiWidth = "34*";

                return oCNotiWidth;
            }
            set
            {
                oCNotiWidth = value;
                RaisePropertyChanged("OCNotiWidth");
            }
        }




    }
}
