using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WorkShipVersionII.LoginViewModel
{
    public class ShipDetailViewModel : ViewModelBase
    {
        VesselClass data;
        private readonly ShipmentContaxt sc;
        public ShipDetailViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            nextCommand = new RelayCommand<VesselClass>(NextMethod);
            try
            {
                data = sc.Vessels.FirstOrDefault();
                if (data != null)
                {
                    loadVessel = data;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        //private static ShipDetailErrorMsg _ShipMSG;

        //public  ShipDetailErrorMsg ShipMsg
        //{
        //    get { return _ShipMSG; }
        //    set 
        //    {   _ShipMSG = value;
        //        RaisePropertyChanged("ShipMsg");
        //    }
        //}


        private void NextMethod(VesselClass obj4)
        {
            try
            {
                StaticHelper.IsValidateShipDetail = true;
                if (string.IsNullOrEmpty(loadVessel.VesselName))
                {
                    StaticHelper.IsValidateShipDetail = false;
                    MessageBox.Show("Please Enter Vessel Name", "DigiMoor-X7", MessageBoxButton.OK, MessageBoxImage.Warning);
                    
                }
                else if(string.IsNullOrEmpty(loadVessel.vessel_ID.ToString()))
                {
                    StaticHelper.IsValidateShipDetail = false;
                    MessageBox.Show("Please Enter IMO Number", "DigiMoor-X7", MessageBoxButton.OK, MessageBoxImage.Warning);
                    
                }

               else if (string.IsNullOrEmpty(loadVessel.Flag))
                {
                    StaticHelper.IsValidateShipDetail = false;
                    MessageBox.Show("Please Enter Flag Name", "DigiMoor-X7", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if(data == null && StaticHelper.IsValidateShipDetail == true)
                {
                    LoadVessel.imo = (int) LoadVessel.vessel_ID;
                    VesselClass ship = new VesselClass()
                    {
                        vessel_ID= LoadVessel.vessel_ID,
                        imo = LoadVessel.imo,
                        VesselName = LoadVessel.VesselName.ToUpper(),
                        Flag = LoadVessel.Flag.ToUpper(),
                    };
                    // sc.Entry(ship).State = EntityState.Modified;
                    sc.Vessels.Add(ship);
                    sc.SaveChanges();
                }
                else if (data != null && StaticHelper.IsValidateShipDetail == true)
                {
                    LoadVessel.imo = (int)LoadVessel.vessel_ID;
                    VesselClass ship = new VesselClass()
                    {
                        Vid = LoadVessel.Vid,
                        vessel_ID = LoadVessel.vessel_ID,
                        imo = LoadVessel.imo,
                        VesselName = LoadVessel.VesselName.ToUpper(),
                        Flag = LoadVessel.Flag.ToUpper(),
                    };

                    var local = sc.Set<VesselClass>()
                               .Local
                               .FirstOrDefault(f => f.Vid == data.Vid);
                    if (local != null)
                    {
                        sc.Entry(local).State = EntityState.Detached;
                    }

                    sc.Entry(ship).State = EntityState.Modified;
                    //sc.Vessels.Add(ship);
                    sc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private ICommand nextCommand;
        public ICommand NextCommand
        {
            get { return nextCommand; }

        }


        private static VesselClass loadVessel = new VesselClass();
        public VesselClass LoadVessel
        {
            get { return loadVessel; }
            set
            {
                loadVessel = value;
                RaisePropertyChanged("LoadVessel");
               // OnPropertyChanged(new PropertyChangedEventArgs("LoadVessel"));
            }
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }

    public class ShipDetailErrorMsg
    {
        public string VesselMsg { get; set; }
        public string IMOMsg { get; set; }
        public string FlagMsg { get; set; }
    }
}
