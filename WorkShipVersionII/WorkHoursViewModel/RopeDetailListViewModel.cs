using DataBuildingLayer;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Windows;
//using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data.Entity;
using System.Windows.Forms;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
   public class RopeDetailListViewModel:ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        public ICommand HelpCommand { get; private set; }
        public RopeDetailListViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            
        HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));

        viewCommand = new RelayCommand<MooringRopeInspectionClass>(Viewimage);
            viewCommand1 = new RelayCommand<MooringRopeInspectionClass>(Viewimage);
            

        


        }
        private ICommand viewCommand;
        public ICommand ViewCommand
        {
            get { return viewCommand; }
            set { viewCommand = value; }
        }

        private ICommand viewCommand1;
        public ICommand ViewCommand1
        {
            get { return viewCommand1; }
            set { viewCommand1 = value; }
        }


        private void Viewimage(MooringRopeInspectionClass mw)
        {
            try
            {

                StaticHelper.ViewId = mw.Id;
                StaticHelper.Photos = "Image1";
                ChildWindowManager.Instance.ShowChildWindow(new ViewPhoto());


            }
            catch (Exception ex)
            {

            }
        }

        new public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }

   
}
