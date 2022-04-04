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
    public class OutputsWindLoadsViewModel : ViewModelBase
    {
        private ShipmentContaxt sc;
        private IRepository<VesselP> VesselPs;
        private IRepository<WindAreas> WindAreass;
        private IRepository<WindandCurrent> WindandCurrents;
        private IRepository<GeneralP> GeneralPs;
        private IRepository<WindLoads> WindLoadss;
        public ICommand HelpCommand { get; private set; }
        public OutputsWindLoadsViewModel()
        {
            sc = new ShipmentContaxt();
            VesselPs = new Repository<VesselP>();
            WindAreass = new Repository<WindAreas>();
            WindandCurrents = new Repository<WindandCurrent>();
            GeneralPs = new Repository<GeneralP>();
            WindLoadss = new Repository<WindLoads>();

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            loadBasicParameters.Clear();
            sc.ObservableCollectionList(loadBasicParameters, GetBasicParameters());
            RaisePropertyChanged("LoadBasicParameters");

            loadWindForce.Clear();
            sc.ObservableCollectionList(loadWindForce, GetWindForce());
            RaisePropertyChanged("LoadWindForce");

            loadWindLoad.Clear();
            sc.ObservableCollectionList(loadWindLoad, GetWindLoad());
            RaisePropertyChanged("LoadWindLoad");
        }


        private List<WindLoads> GetBasicParameters()
        {
            var vessels = VesselPs.GetList().ToList();
            var windarea = WindAreass.GetList().ToList();
            var envirement = WindandCurrents.GetList().ToList();
            var generalp = GeneralPs.GetList().ToList();

            var list = WindLoadss.GetList().ToList();

            list.ForEach(x =>
            {
                if (x.Notation == "LWL") { x.MainValue = vessels.Where(p => p.Description == "LWL").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "Ae") { x.MainValue = windarea.Where(p => p.Description == "Ae").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "As") { x.MainValue = windarea.Where(p => p.Description == "As").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "Vw") { x.MainValue = envirement.Where(p => p.Description == "Vw").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "Pair") { x.MainValue = generalp.Where(p => p.Description == "Pair").Select(s => Convert.ToDecimal(s.MainValue)).FirstOrDefault(); }
                if (x.Notation == "Qw") { x.MainValue = envirement.Where(p => p.Description == "Qw").Select(s => s.MainValue).FirstOrDefault(); }
                if (string.IsNullOrEmpty(x.Notation)) { x.MainValue = windarea.Where(p => string.IsNullOrEmpty(p.Description)).Select(s => s.MainValue).FirstOrDefault(); }

            });



            return list;
        }

        public static List<WindForceCoefficients> GetWindForce()
        {
            List<WindForceCoefficients> list = new List<WindForceCoefficients>()
            {
                new WindForceCoefficients{  Name="Longitudinal Wind Force Coefficient",Description="CXW",Units="See Table 1"},
                new WindForceCoefficients{  Name="Lateral Wind Force Coefficient",Description="CYW",Units="See Table 2"},
                new WindForceCoefficients{  Name="Wind Yaw Moment Coefficient",Description="CXYW",Units="See Table 3"}
            };



            double D20 = Convert.ToDouble(loadBasicParameters.Where(x => x.Notation == "Qw").Select(s => s.MainValue).FirstOrDefault());
            double D21 = Convert.ToDouble(loadBasicParameters.Where(x => string.IsNullOrEmpty(x.Notation)).Select(s => s.MainValue).FirstOrDefault());

            var CXW = (D20 == 90 ? 0 : (D21 == 1 ? Math.Round(0.000000679 * (D20 * D20 * D20) - 0.0001833333 * (D20 * D20) + 0.0010079365 * D20 + 0.8992857143, 3) : Math.Round(-0.000000000027435 * (D20 * D20 * D20 * D20 * D20) + 0.000000012345679 * (D20 * D20 * D20 * D20) - 0.000001604938272 * (D20 * D20 * D20) + 0.000033333333332 * (D20 * D20) - 0.0022 * D20 + 0.45, 3)));


            var CYW = (D20 == 90 ? 1 : ((D20 == 0 || D20 == 180) ? 0 : Math.Round(0.0000000048 * (D20 * D20 * D20 * D20) - 0.0000017172 * (D20 * D20 * D20) + 0.000068771 * (D20 * D20) + 0.0154393939 * D20 - 0.0003679654, 3)));

            var CXYW = ((D20 == 0 || D20 == 180) ? 0 : (D20 == 90 ? -0.037 : Math.Round(-1.3115325E-13 * (D20 * D20 * D20 * D20 * D20 * D20) + 4.153539343E-11 * (D20 * D20 * D20 * D20 * D20) + 3.040873708E-11 * (D20 * D20 * D20 * D20) - 7.6143340645882E-07 * (D20 * D20 * D20) + 0.0000211970749723711 * (D20 * D20) + 0.0018519671365894 * D20 + 0.00092355496042354, 3)));


            list.ForEach(x =>
            {
                if (x.Description == "CXW") { x.Values = Convert.ToDecimal(CXW); }
                if (x.Description == "CYW") { x.Values = Convert.ToDecimal(CYW); }
                if (x.Description == "CXYW") { x.Values = Convert.ToDecimal(CXYW); }


            });

            return list;
        }

        public static  List<WindForceCoefficients> GetWindLoad()
        {
            List<WindForceCoefficients> list = new List<WindForceCoefficients>()
            {
                new WindForceCoefficients{  Name="Longitudinal Wind Force",Description="FXW=1/2*Cxw*ρair*Vw2*AE",Description1 = "CXW",Units="MT"},
                new WindForceCoefficients{  Name="Lateral Wind Force",Description="FYW=1/2*Cyw*ρair*Vw2*AS",Description1 = "CYW",Units="MT"},
                new WindForceCoefficients{  Name="Total Factored Windage Area of Hull in Lateral Direction",Description="MW=1/2*Cxyw*ρair*Vw2*AS*LWL",Description1 = "CXYW",Units="MT-m"}
            };

            double D15 = Convert.ToDouble(loadBasicParameters.Where(x => x.Notation == "LWL").Select(s => s.MainValue).FirstOrDefault());
            double D16 = Convert.ToDouble(loadBasicParameters.Where(x => x.Notation == "Ae").Select(s => s.MainValue).FirstOrDefault());
            double D17 = Convert.ToDouble(loadBasicParameters.Where(x => x.Notation == "As").Select(s => s.MainValue).FirstOrDefault());
            double D18 = Convert.ToDouble(loadBasicParameters.Where(x => x.Notation == "Vw").Select(s => s.MainValue).FirstOrDefault());
            double D19 = Convert.ToDouble(loadBasicParameters.Where(x => x.Notation == "Pair").Select(s => s.MainValue).FirstOrDefault());

            double D23 = Convert.ToDouble(loadWindForce.Where(x => x.Description == "CXW").Select(s => s.Values).FirstOrDefault());
            double D24 = Convert.ToDouble(loadWindForce.Where(x => x.Description == "CYW").Select(s => s.Values).FirstOrDefault());
            double D25 = Convert.ToDouble(loadWindForce.Where(x => x.Description == "CXYW").Select(s => s.Values).FirstOrDefault());

            var CXW = ((D19 * D23 * (D18 * D18) * D16) * 1 / 2) / 9810;
            var CYW = ((D19 * D24 * (D18 * D18) * D17) * 1 / 2) / 9810;
            var CXYW = ((D19 * D25 * (D18 * D18) * D17 * D15) * 1 / 2) / 9810;


            list.ForEach(x =>
            {
                if (x.Description1 == "CXW") { x.Values = Convert.ToDecimal(CXW); }
                if (x.Description1 == "CYW") { x.Values = Convert.ToDecimal(CYW); }
                if (x.Description1 == "CXYW") { x.Values = Convert.ToDecimal(CXYW); }


            });


            return list;
        }


        private static ObservableCollection<WindLoads> loadBasicParameters = new ObservableCollection<WindLoads>();
        public ObservableCollection<WindLoads> LoadBasicParameters
        {
            get
            {
                return loadBasicParameters;
            }
            set
            {
                loadBasicParameters = value;
                RaisePropertyChanged("LoadBasicParameters");
            }
        }


        public static ObservableCollection<WindForceCoefficients> loadWindForce = new ObservableCollection<WindForceCoefficients>();
        public ObservableCollection<WindForceCoefficients> LoadWindForce
        {
            get
            {
                return loadWindForce;
            }
            set
            {
                loadWindForce = value;
                RaisePropertyChanged("LoadWindForce");
            }
        }

        public static ObservableCollection<WindForceCoefficients> loadWindLoad = new ObservableCollection<WindForceCoefficients>();
        public ObservableCollection<WindForceCoefficients> LoadWindLoad
        {
            get
            {
                return loadWindLoad;
            }
            set
            {
                loadWindLoad = value;
                RaisePropertyChanged("LoadWindLoad");
            }
        }



    }
}
