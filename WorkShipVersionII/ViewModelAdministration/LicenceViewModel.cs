using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.ViewModelAdministration
{

       public class LicenceViewModel : ViewModelBase
    {
        private readonly AdministrationContaxt sc;
        public ICommand HelpCommand { get; private set; }
        public LicenceViewModel()
        {
            if (sc == null)
            {
                sc = new AdministrationContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }
            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            try
            {
                LicenceClass data = (from c in sc.Vessels
                                     from a in sc.Versions
                                     select new LicenceClass()
                                     {
                                         VesselName = c.VesselName,
                                         IMO = c.imo,
                                         WebSite = c.website,
                                         Email = c.Email,
                                         Flag = c.Flag,
                                         Version = a.versions
                                     }).FirstOrDefault();

                loadLicence = data;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadLicence"));
                CommonExpiry.ExpiryMessages = GetExpiryDate();
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }
            //OnPropertyChanged(new PropertyChangedEventArgs("CommonExpiry"));

        }
      

        private string GetExpiryDate()
        {
            try
            {
                var txtpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "RecordDigiMoor.txt");
                if (File.Exists(txtpath) && new FileInfo(txtpath).Length > 0)
                {
                    StreamReader srd = new StreamReader(txtpath);

                    string LastYearRecordWork49Data = srd.ReadLine();
                    string OldRecordData = sc.Decrypt(LastYearRecordWork49Data, "KKPrajapat");
                    srd.Close();

                    string[] lastRecord = OldRecordData.Split(',');
                    DateTime expirydate = Convert.ToDateTime(lastRecord[1].ToString());
                    string message = "Expiring On  " + expirydate.ToString("dd-MMM-yyyy");
                    return message;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
                return string.Empty;
            }
        }

        private static LicenceClass loadLicence = new LicenceClass();
        public LicenceClass LoadLicence
        {
            get { return loadLicence; }
            set
            {
                loadLicence = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadLicence"));
            }
        }

        public static CommonPropertiesDeviation _CommonExpiry;
        public CommonPropertiesDeviation CommonExpiry
        {
            get
            {
                if (_CommonExpiry == null)
                    _CommonExpiry = new CommonPropertiesDeviation();
                return _CommonExpiry;
            }
            set
            {

                _CommonExpiry = value;
                RaisePropertyChanged("CommonExpiry");
            }
        }



        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }


    }
    public class LicenceClass
    {
        public string VesselName { get; set; }
        public int IMO { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
        public string Flag { get; set; }
        public string Version { get; set; }

      
    }

}
