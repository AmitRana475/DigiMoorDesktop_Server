using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Data.Entity;

namespace WorkShipVersionII.ViewModelCrewManagement
{
    public class FreezeUnfreezeViewModel : ViewModelBase
    {
        private  readonly ShipmentContaxt sc;
        public FreezeUnfreezeViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            _AddFreezDetail = sc.Freezes.FirstOrDefault();
            RaisePropertyChanged("AddFreezDetail");

            saveCommand = new RelayCommand<FreezeClass>(SaveMethod);
            freezeCommand = new RelayCommand<FreezeClass>(FreezeMethod);
            unFreezeCommand = new RelayCommand<FreezeClass>(UnFreezeMethod);
        }



        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand;
            }

        }
        private ICommand freezeCommand;
        public ICommand FreezeCommand
        {
            get
            {
                return freezeCommand;
            }

        }

        private ICommand unFreezeCommand;
        public ICommand UnFreezeCommand
        {
            get
            {
                return unFreezeCommand;
            }

        }

            
        private static FreezeClass _AddFreezDetail = new FreezeClass();
        public FreezeClass AddFreezDetail
        {
            get
            {

                return _AddFreezDetail;
            }
            set
            {
                _AddFreezDetail = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AddFreezDetail"));

            }
        }


        private void UnFreezeMethod(FreezeClass obj)
        {
            var local = sc.Set<FreezeClass>()
                     .Local
                     .FirstOrDefault(f => f.Id == obj.Id);
            if (local != null)
            {
                sc.Entry(obj).State = EntityState.Detached;
            }
            using (ShipmentContaxt sc1 = new ShipmentContaxt())
            {
                var user = new FreezeClass() { Id = obj.Id, FreezeStatus = "UnFreeze", ApplyDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()), DateFrom = Convert.ToDateTime(obj.DateFrom.ToShortDateString()), DateTo = Convert.ToDateTime(obj.DateTo.ToShortDateString()) };

                sc1.Freezes.Attach(user);
                sc1.Entry(user).Property(x => x.FreezeStatus).IsModified = true;
                sc1.Entry(user).Property(x => x.DateFrom).IsModified = true;
                sc1.Entry(user).Property(x => x.DateTo).IsModified = true;
                sc1.Entry(user).Property(x => x.ApplyDate).IsModified = true;
                sc1.SaveChanges();
            }
            MessageBox.Show("Unfreeze successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void FreezeMethod(FreezeClass obj)
        {
            var local = sc.Set<FreezeClass>()
                     .Local
                     .FirstOrDefault(f => f.Id == obj.Id);
            if (local != null)
            {
                sc.Entry(obj).State = EntityState.Detached;
            }
            using (ShipmentContaxt sc1 = new ShipmentContaxt())
            {
                var user = new FreezeClass() { Id = obj.Id, FreezeStatus = "Freeze", ApplyDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()), DateFrom = Convert.ToDateTime(obj.DateFrom.ToShortDateString()), DateTo = Convert.ToDateTime(obj.DateTo.ToShortDateString()) };

                sc1.Freezes.Attach(user);
                sc1.Entry(user).Property(x => x.FreezeStatus).IsModified = true;
                sc1.Entry(user).Property(x => x.DateFrom).IsModified = true;
                sc1.Entry(user).Property(x => x.DateTo).IsModified = true;
                sc1.Entry(user).Property(x => x.ApplyDate).IsModified = true;
                sc1.SaveChanges();
            }
            MessageBox.Show("Freeze successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveMethod(FreezeClass obj)
        {

            var local = sc.Set<FreezeClass>()
                     .Local
                     .FirstOrDefault(f => f.Id == obj.Id);
            if (local != null)
            {
                sc.Entry(obj).State = EntityState.Detached;
            }

            using (ShipmentContaxt sc1 = new ShipmentContaxt())
            {
                var user = new FreezeClass() { Id = obj.Id, FreezeDays = obj.FreezeDays, ApplyDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()) };

                sc1.Freezes.Attach(user);
                sc1.Entry(user).Property(x => x.FreezeDays).IsModified = true;
                sc1.Entry(user).Property(x => x.ApplyDate).IsModified = true;
                sc1.SaveChanges();
            }

            MessageBox.Show("Record saved successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);

        }



        new private event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

    }
}
