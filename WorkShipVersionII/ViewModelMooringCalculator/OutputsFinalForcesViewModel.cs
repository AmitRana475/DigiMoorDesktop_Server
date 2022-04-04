using GalaSoft.MvvmLight;
using DataBuildingLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace WorkShipVersionII.ViewModelMooringCalculator
{
    public class OutputsFinalForcesViewModel : ViewModelBase
    {
        private ShipmentContaxt sc;
        public ICommand HelpCommand { get; private set; }
        public OutputsFinalForcesViewModel()
        {
            sc = new ShipmentContaxt();

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            loadWindLoad2.Clear();
            sc.ObservableCollectionList(loadWindLoad2, GetWindLoad());
            RaisePropertyChanged("LoadWindLoad2");
        }


        private List<WindForceCoefficients> GetWindLoad()
        {
            List<WindForceCoefficients> list = new List<WindForceCoefficients>()
            {
                new WindForceCoefficients{  Name="Longitudinal Current Force",Description="FXC=1/2*ρwater*VC2*B*(CXCA*S/LWL+CXCB*T)",Description1="CXW",Units="MT"},
                new WindForceCoefficients{  Name="Lateral Current Force",Description="FYC=1/2*CYC*ρwater*VC2*LWL*T)", Description1="CYW",Units="MT"},
                new WindForceCoefficients{  Name="Current Yaw Moment",Description="FXYC=1/2*CXYC*ρwater*VC2*LWL2*T)",Description1="CXYW",Units="MT-m"}
            };

            new OutputsWindLoadsViewModel();
            new OutputsCurrentLoadsViewModel();
            new InputsVesselViewModel();

            //var bms = OutputsWindLoadsViewModel.loadWindLoad.Where(x => x.Description == "CXW").Select(s => s.Values).FirstOrDefault();
            //var bms1 = OutputsCurrentLoadsViewModel.loadWindLoad1.Where(x => x.Description == "CXW").Select(s => s.Values).FirstOrDefault();
            //var bms2 = OutputsWindLoadsViewModel.loadWindLoad.Where(x => x.Description == "CYW").Select(s => s.Values).FirstOrDefault();
            //var bms21 = OutputsCurrentLoadsViewModel.loadWindLoad1.Where(x => x.Description == "CYW").Select(s => s.Values).FirstOrDefault();
            //var bms3 = OutputsWindLoadsViewModel.loadWindLoad.Where(x => x.Description == "CXYW").Select(s => s.Values).FirstOrDefault();
            //var bms31 = OutputsCurrentLoadsViewModel.loadWindLoad1.Where(x => x.Description == "CXYW").Select(s => s.Values).FirstOrDefault();
            //var bms32 = InputsVesselViewModel.loadVesselP.Where(x => x.Description == "LWL").Select(s => s.MainValue).FirstOrDefault();
            //='Outputs - Wind Loads'!F31+'Outputs - Current Loads'!F36-0.48*'Inputs - Vessel'!E14*'Outputs - Final Forces'!F9


            var CXW = Convert.ToDecimal(OutputsWindLoadsViewModel.loadWindLoad.Where(x => x.Description1 == "CXW").Select(s => s.Values).FirstOrDefault()) + Convert.ToDecimal(OutputsCurrentLoadsViewModel.loadWindLoad1.Where(x => x.Description1 == "CXW").Select(s => s.Values).FirstOrDefault());

            var CYW = Convert.ToDecimal(OutputsWindLoadsViewModel.loadWindLoad.Where(x => x.Description1 == "CYW").Select(s => s.Values).FirstOrDefault()) + Convert.ToDecimal(OutputsCurrentLoadsViewModel.loadWindLoad1.Where(x => x.Description1 == "CYW").Select(s => s.Values).FirstOrDefault());

            var CXYW = Convert.ToDecimal(OutputsWindLoadsViewModel.loadWindLoad.Where(x => x.Description1 == "CXYW").Select(s => s.Values).FirstOrDefault()) + Convert.ToDecimal(OutputsCurrentLoadsViewModel.loadWindLoad1.Where(x => x.Description1 == "CXYW").Select(s => s.Values).FirstOrDefault()) - 0.48m * Convert.ToDecimal(InputsVesselViewModel.loadVesselP.Where(x => x.Description == "LWL").Select(s => s.MainValue).FirstOrDefault()) * CXW;

            list.ForEach(x =>
            {
                if (x.Description1 == "CXW") { x.Values = Convert.ToDecimal(CXW); }
                if (x.Description1 == "CYW") { x.Values = Convert.ToDecimal(CYW); }
                if (x.Description1 == "CXYW") { x.Values = Convert.ToDecimal(CXYW); }


            });



            return list;
        }

        public static ObservableCollection<WindForceCoefficients> loadWindLoad2 = new ObservableCollection<WindForceCoefficients>();
        public ObservableCollection<WindForceCoefficients> LoadWindLoad2
        {
            get
            {
                return loadWindLoad2;
            }
            set
            {
                loadWindLoad2 = value;
                RaisePropertyChanged("LoadWindLoad2");
            }
        }

    }
}
