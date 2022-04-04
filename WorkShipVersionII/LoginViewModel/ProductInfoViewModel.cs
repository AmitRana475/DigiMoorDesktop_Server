using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WorkShipVersionII.ViewModelCrewManagement;

namespace WorkShipVersionII.LoginViewModel
{
    public class ProductInfoViewModel : ViewModelBase
    {
        private int erinfo;
        private readonly AdministrationContaxt sc;



     

        string key1 = "";
        int year1 = 0; // year2 = 0, year3 = 0, year4 = 0, year5 = 0, year6 = 0, year7 = 0, year8 = 0, year9 = 0, year10 = 0, year11 = 0, year12 = 0, year13 = 0, year14 = 0, year15 = 0, year16 = 0, year17 = 0, year18 = 0, year19 = 0, year20 = 0, year21 = 0;
        DateTime yr1; //yr2, yr3, yr4, yr5, yr6, yr7, yr8, yr9, yr10, yr11, yr12, yr13, yr14, yr15, yr16, yr17, yr18, yr19, yr20, yr21;

        public ProductInfoViewModel()
        {
            if (sc == null)
            {
                sc = new AdministrationContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            validateCommand = new RelayCommand<ProductInfoClass>(ValidateMethod);
        }



        private ICommand validateCommand;
        public ICommand ValidateCommand
        {
            get
            {
                return validateCommand;
            }

        }


        private static ProductInfoClass productInfoAccess = new ProductInfoClass();
        public ProductInfoClass ProductInfoAccess
        {
            get
            {
                if (erinfo != 0)
                {
                    RefreshMessage(productInfoAccess);
                    RaisePropertyChanged("ProductMessage");
                }
                return productInfoAccess;
            }
            set
            {
                productInfoAccess = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ProductInfoAccess"));
            }
        }

        private string productMessage;
        public string ProductMessage
        {
            get
            {
                return productMessage;
            }
            set
            {
                productMessage = value;
                RaisePropertyChanged("ProductMessage");
            }
        }


        private void ValidateMethod(ProductInfoClass obj)
        {
            DateTime expirydate;
            erinfo = 1;
            RefreshMessage(obj);
            if (CheckErrorMessage.CheckErrorMessages)
            {
                string dtt = DateTime.Now.ToString("yyyy-MM-dd"); //dt.Rows[0][0].ToString(); //sr.ReadLine().ToString();
                DateTime dttt = Convert.ToDateTime(dtt);


                var vesseldetail = sc.Vessels.FirstOrDefault();

                int mm1 = dttt.Month;
                int dd1 = dttt.Day;
                int year = dttt.Year;


                yr1 = new DateTime(year, mm1, dd1);
                year1 = yr1.Year;

                //yr2 = yr1.AddYears(1);
                //year2 = yr2.Year;

                //yr3 = yr2.AddYears(1);
                //year3 = yr3.Year;

                //yr4 = yr3.AddYears(1);
                //year4 = yr4.Year;

                //yr5 = yr4.AddYears(1);
                //year5 = yr5.Year;

                //yr6 = yr5.AddYears(1);
                //year6 = yr6.Year;

                //yr7 = yr6.AddYears(1);
                //year7 = yr7.Year;

                //yr8 = yr7.AddYears(1);
                //year8 = yr8.Year;

                //yr9 = yr8.AddYears(1);
                //year9 = yr9.Year;

                //yr10 = yr9.AddYears(1);
                //year10 = yr10.Year;

                //yr11 = yr10.AddYears(1);
                //year11 = yr11.Year;

                //yr12 = yr11.AddYears(1);
                //year12 = yr12.Year;

                //yr13 = yr12.AddYears(1);
                //year13 = yr13.Year;

                //yr14 = yr13.AddYears(1);
                //year14 = yr14.Year;

                //yr15 = yr14.AddYears(1);
                //year15 = yr15.Year;

                //yr16 = yr15.AddYears(1);
                //year16 = yr16.Year;

                //yr17 = yr16.AddYears(1);
                //year17 = yr17.Year;

                //yr18 = yr17.AddYears(1);
                //year18 = yr18.Year;

                //yr19 = yr18.AddYears(1);
                //year19 = yr19.Year;

                //yr20 = yr19.AddYears(1);
                //year20 = yr20.Year;

                //yr21 = yr20.AddYears(1);
                //year21 = yr21.Year;

                var key1p = sc.GetLicencekeys().ToList().Where(x => x.keyno == "key1").FirstOrDefault().keycode;
                key1 = key1p;

                string txtLicenseKey = obj.Text1.Trim().ToUpper() + "-" + obj.Text2.Trim().ToUpper() + "-" + obj.Text3.Trim().ToUpper() + "-" + obj.Text4.Trim().ToUpper() + "-" + obj.Text5.Trim().ToUpper();
                if (txtLicenseKey == key1)
                {
                    if ((year1 % 4) == 0)
                    {
                        expirydate = DateTime.Now.AddDays(366);
                    }
                    else
                    {
                        expirydate = DateTime.Now.AddDays(365);
                    }

                    string RecordLoginInfo = string.Empty;

                    var txtpathLastLogin = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LastLoginV2.txt");
                    if (File.Exists(txtpathLastLogin))
                    {
                        if (new FileInfo(txtpathLastLogin).Length == 0)
                        {
                            StreamWriter writer = new StreamWriter(txtpathLastLogin);

                            writer.Write(sc.Encrypt(DateTime.Now.ToString("yyyy-MM-dd") + "," + expirydate.ToString("yyyy-MM-dd"), "KKPrajapat"));
                            writer.Close();

                            RecordLoginInfo = sc.Encrypt(DateTime.Now.ToString("yyyy-MM-dd") + "," + expirydate.ToString("yyyy-MM-dd"), "KKPrajapat");


                        }
                    }
                    else
                    {
                        StreamWriter writer = new StreamWriter(txtpathLastLogin);

                        writer.Write(sc.Encrypt(DateTime.Now.ToString("yyyy-MM-dd") + "," + expirydate.ToString("yyyy-MM-dd") , "KKPrajapat"));
                        writer.Close();


                        RecordLoginInfo = sc.Encrypt(DateTime.Now.ToString("yyyy-MM-dd") + "," + expirydate.ToString("yyyy-MM-dd"), "KKPrajapat");

                    }



                    var txtpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RecordWork49V2.txt");

                    if (File.Exists(txtpath))
                    {

                        if (new FileInfo(txtpath).Length == 0)
                        {
                            StreamWriter writer = new StreamWriter(txtpath);

                            writer.Write(sc.Encrypt(DateTime.Now.ToString("yyyy-MM-dd") + "," + expirydate.ToString("yyyy-MM-dd") + "," + txtLicenseKey + "," + vesseldetail.VesselName, "KKPrajapat"));
                            writer.Close();

                            string RecordWork49file = sc.Encrypt(DateTime.Now.ToString("yyyy-MM-dd") + "," + expirydate.ToString("yyyy-MM-dd") + "," + txtLicenseKey + "," + vesseldetail.VesselName, "KKPrajapat");

                            var date = DateTime.Now.ToString("yyyy-MM-dd");

                            var data = sc.AdminLogins.FirstOrDefault();
                            data.LastLogin = Convert.ToDateTime(date);
                            data.productinfo = RecordWork49file;
                            data.Loginfo = RecordLoginInfo;

                            sc.Entry(data).State = EntityState.Modified;
                            sc.SaveChanges();


                        }


                    }
                    else
                    {
                        StreamWriter writer = new StreamWriter(txtpath);

                        writer.Write(sc.Encrypt(DateTime.Now.ToString("yyyy-MM-dd") + "," + expirydate.ToString("yyyy-MM-dd") + "," + txtLicenseKey + "," + vesseldetail.VesselName, "KKPrajapat"));
                        writer.Close();


                        string RecordWork49file = sc.Encrypt(DateTime.Now.ToString("yyyy-MM-dd") + "," + expirydate.ToString("yyyy-MM-dd") + "," + txtLicenseKey + "," + vesseldetail.VesselName, "KKPrajapat");

                        var date = DateTime.Now.ToString("yyyy-MM-dd");

                        var data = sc.AdminLogins.FirstOrDefault();
                        data.LastLogin = Convert.ToDateTime(date);
                        data.productinfo = RecordWork49file;
                        data.Loginfo = RecordLoginInfo;

                        sc.Entry(data).State = EntityState.Modified;
                        sc.SaveChanges();
                    }

                    MessageBox.Show("Your Licence is validated successfully ", "License Key", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    CheckErrorMessage.CheckErrorMessages = false;
                    MessageBox.Show("Invalid License Key", "License Key", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void RefreshMessage(ProductInfoClass obj)
        {
            if (string.IsNullOrEmpty(obj.Text1) || string.IsNullOrEmpty(obj.Text2) || string.IsNullOrEmpty(obj.Text3) || string.IsNullOrEmpty(obj.Text4) || string.IsNullOrEmpty(obj.Text5) || obj.Text1.Length < 4 || obj.Text2.Length < 4 || obj.Text3.Length < 4 || obj.Text4.Length < 4 || obj.Text5.Length < 4)
            {
                CheckErrorMessage.CheckErrorMessages = false;
                productMessage = "Please Enter the Licence Key";
                RaisePropertyChanged("ProductMessage");
            }
            else if (!string.IsNullOrEmpty(obj.Text1) && !string.IsNullOrEmpty(obj.Text2) && !string.IsNullOrEmpty(obj.Text3) && !string.IsNullOrEmpty(obj.Text4) && !string.IsNullOrEmpty(obj.Text5))
            {
                CheckErrorMessage.CheckErrorMessages = true;
                ProductMessage = string.Empty;

            }

        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
