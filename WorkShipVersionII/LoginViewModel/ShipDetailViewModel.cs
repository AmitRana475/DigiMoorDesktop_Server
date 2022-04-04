using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;

namespace WorkShipVersionII.LoginViewModel
{
    public class ShipDetailViewModel : ViewModelBase
    {
        private readonly AdministrationContaxt sc;
        public ShipDetailViewModel()
        {
            if (sc == null)
            {
                sc = new AdministrationContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            nextCommand = new RelayCommand<VesselClass>(NextMethod);
            try
            {
                VesselClass data = sc.Vessels.FirstOrDefault();
                loadVessel = data;
            }
            catch(Exception ex)
            {
                sc.ErrorLog(ex);
            }

        }

        private void NextMethod(VesselClass obj)
        {
            try
            {
                sc.Entry(obj).State = EntityState.Modified;
                sc.SaveChanges();
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


        private VesselClass loadVessel;
        public VesselClass LoadVessel
        {
            get { return loadVessel; }
            set
            {
                loadVessel = value;
                OnPropertyChanged(new PropertyChangedEventArgs("LoadVessel"));
            }
        }


        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
