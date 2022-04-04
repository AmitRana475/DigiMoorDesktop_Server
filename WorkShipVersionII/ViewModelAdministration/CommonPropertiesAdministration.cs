using GalaSoft.MvvmLight;


namespace WorkShipVersionII.ViewModelAdministration
{
    public class CommonPropertiesAdministration : ViewModelBase
    {
        private string importExport;
        public string ImportExport
        {
            get
            {
                if (string.IsNullOrEmpty(importExport))
                    importExport = "Visible";

                return importExport;
            }
            set
            {
                importExport = value;
                RaisePropertyChanged("ImportExport");
            }
        }

        private string backupRestore;
        public string BackupRestore
        {
            get
            {
                if (string.IsNullOrEmpty(backupRestore))
                    backupRestore = "Visible";

                return backupRestore;
            }
            set
            {
                backupRestore = value;
                RaisePropertyChanged("BackupRestore");
            }
        }

        private string applicationLog;
        public string ApplicationLog
        {
            get
            {
                if (string.IsNullOrEmpty(applicationLog))
                    applicationLog = "Visible";

                return applicationLog;
            }
            set
            {
                applicationLog = value;
                RaisePropertyChanged("ApplicationLog");
            }
        }

        private string rules;
        public string Rules
        {
            get
            {
                if (string.IsNullOrEmpty(rules))
                    rules = "Visible";

                return rules;
            }
            set
            {
                rules = value;
                RaisePropertyChanged("Rules");
            }
        }

        private string lincenc;
        public string Lincenc
        {
            get
            {
                if (string.IsNullOrEmpty(lincenc))
                    lincenc = "Visible";

                return lincenc;
            }
            set
            {
                lincenc = value;
                RaisePropertyChanged("Lincenc");
            }
        }

        private string errorLog;
        public string ErrorLog
        {
            get
            {
                if (string.IsNullOrEmpty(errorLog))
                    errorLog = "Visible";

                return errorLog;
            }
            set
            {
                errorLog = value;
                RaisePropertyChanged("ErrorLog");
            }
        }

    }
}
