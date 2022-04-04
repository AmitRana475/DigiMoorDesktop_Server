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
    public class OutputsCurrentLoadsViewModel : ViewModelBase
    {
        private ShipmentContaxt sc;
        private IRepository<VesselP> VesselPs;
        private IRepository<WindandCurrent> WindandCurrents;
        private IRepository<GeneralP> GeneralPs;
        private IRepository<CurrentLoad> CurrentLoads;
        public ICommand HelpCommand { get; private set; }
        public OutputsCurrentLoadsViewModel()
        {
            sc = new ShipmentContaxt();
            VesselPs = new Repository<VesselP>();
            WindandCurrents = new Repository<WindandCurrent>();
            GeneralPs = new Repository<GeneralP>();
            CurrentLoads = new Repository<CurrentLoad>();

            HelpCommand = new RelayCommand(() => StaticHelper.HelpMethod(StaticHelper.HelpFor));
            loadBasicParameters1.Clear();
            sc.ObservableCollectionList(loadBasicParameters1, GetBasicParameters());
            RaisePropertyChanged("LoadBasicParameters1");

            loadCurrentForce.Clear();
            sc.ObservableCollectionList(loadCurrentForce, GetCurrentForce());
            RaisePropertyChanged("LoadCurrentForce");

            loadWindLoad1.Clear();
            sc.ObservableCollectionList(loadWindLoad1, GetWindLoad());
            RaisePropertyChanged("LoadWindLoad1");
        }


        private List<CurrentLoad> GetBasicParameters()
        {

            var vessels = VesselPs.GetList().ToList();
            var envirement = WindandCurrents.GetList().ToList();
            var generalp = GeneralPs.GetList().ToList();

            var list = CurrentLoads.GetList().ToList();

            list.ForEach(x =>
            {
                if (x.Notation == "LWL") { x.MainValue = vessels.Where(p => p.Description == "LWL").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "B") { x.MainValue = vessels.Where(p => p.Description == "B").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "T") { x.MainValue = vessels.Where(p => p.Description == "T").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "WD") { x.MainValue = envirement.Where(p => p.Description == "WD").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "S") { x.MainValue = vessels.Where(p => p.Description == "S").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "Vc") { x.MainValue = envirement.Where(p => p.Description == "Vc").Select(s => s.MainValue).FirstOrDefault(); }
                if (x.Notation == "Pwater") { x.MainValue = generalp.Where(p => p.Description == "Pwater").Select(s => Convert.ToDecimal(s.MainValue)).FirstOrDefault(); }
                if (x.Notation == "Qc") { x.MainValue = envirement.Where(p => p.Description == "Qc").Select(s => s.MainValue).FirstOrDefault(); }

                if (x.Notation == "WD/T")
                {
                    var main = envirement.Where(p => p.Description == "WD").Select(s => s.MainValue).FirstOrDefault();
                    var val2 = vessels.Where(p => p.Description == "T").Select(s => s.MainValue).FirstOrDefault();
                    x.MainValue = val2==0?0: main / val2;
                    // x.MainValue = envirement.Where(p => p.Description == "WD").Select(s => s.MainValue).FirstOrDefault() / vessels.Where(p => p.Description == "T").Select(s => s.MainValue).FirstOrDefault(); }
                }
            });



            return list;
        }

        private List<WindForceCoefficients> GetCurrentForce()
        {
            List<WindForceCoefficients> list = new List<WindForceCoefficients>()
            {
                new WindForceCoefficients{  Name="Longitudinal Current Skin Friction Coefficient",Description="CXCA",Units="See Table 1"},
                new WindForceCoefficients{  Name="Lateral Current Force Coefficient",Description="CYC",Units="See Table 2"},
                new WindForceCoefficients{  Name="Longitudinal Current Drag Coefficient",Description="CXCB",Units="CYC * cos2θC"},
                new WindForceCoefficients{  Name="Current Yaw Moment Coefficient",Description="CXYC",Units="See Table 3"}
            };

            double D24 = Convert.ToDouble(loadBasicParameters1.Where(x => x.Notation == "Qc").Select(s => s.MainValue).FirstOrDefault());
            decimal D25 = Convert.ToDecimal(loadBasicParameters1.Where(x => x.Notation == "WD/T").Select(s => s.MainValue).FirstOrDefault());
            decimal D28 = 1.19m;

            object CXCB = null;

            if (D25 > Convert.ToDecimal(1.05))
                CXCB = Convert.ToDecimal(D28) * Convert.ToDecimal(Math.Cos(D24 * (Math.PI / 180.0)) * Math.Cos(D24 * (Math.PI / 180.0)));
            else
                CXCB = "1.05";// "Low Water Depth";

            list.ForEach(x =>
            {
                if (x.Description == "CXCA") { x.Values = Convert.ToDecimal(1.21430643318376E-17); }
                if (x.Description == "CYC") { x.Values = Convert.ToDecimal(1.19967479226758); }
                //if (x.Description == "CXCB") { x.Values = Convert.ToDecimal((D25 < 1.05 ? "Low Water Depth" : D28 * COS(RADIANS(D24)) * COS(RADIANS(D24)))); }
                if (x.Description == "CXCB") { x.Values = CXCB; }
                if (x.Description == "CXYC") { x.Values = Convert.ToDecimal(-0.0299922569589333); } else { }


            });



            return list;
        }

        private List<WindForceCoefficients> GetWindLoad()
        {

            List<WindForceCoefficients> list = new List<WindForceCoefficients>()
            {
                new WindForceCoefficients{  Name="Longitudinal Current Force",Description="FXC=1/2*ρwater*VC2*B*(CXCA*S/LWL+CXCB*T)",Description1="CXW",Units="MT"},
                new WindForceCoefficients{  Name="Lateral Current Force",Description="FYC=1/2*CYC*ρwater*VC2*LWL*T)",Description1="CYW",Units="MT"},
                new WindForceCoefficients{  Name="Current Yaw Moment",Description="FXYC=1/2*CXYC*ρwater*VC2*LWL2*T)",Description1="CXYW",Units="MT-m"}
            };


            double D17 = Convert.ToDouble(loadBasicParameters1.Where(x => x.Notation == "LWL").Select(s => s.MainValue).FirstOrDefault());
            double D18 = Convert.ToDouble(loadBasicParameters1.Where(x => x.Notation == "B").Select(s => s.MainValue).FirstOrDefault());
            double D19 = Convert.ToDouble(loadBasicParameters1.Where(x => x.Notation == "T").Select(s => s.MainValue).FirstOrDefault());
            double D21 = Convert.ToDouble(loadBasicParameters1.Where(x => x.Notation == "S").Select(s => s.MainValue).FirstOrDefault());
            double D22 = Convert.ToDouble(loadBasicParameters1.Where(x => x.Notation == "Vc").Select(s => s.MainValue).FirstOrDefault());
            double D23 = Convert.ToDouble(loadBasicParameters1.Where(x => x.Notation == "Pwater").Select(s => s.MainValue).FirstOrDefault());

            double D27 = Convert.ToDouble(loadCurrentForce.Where(x => x.Description == "CXCA").Select(s => s.Values).FirstOrDefault());
            double D28 = Convert.ToDouble(loadCurrentForce.Where(x => x.Description == "CYC").Select(s => s.Values).FirstOrDefault());
            double D29 = Convert.ToDouble(loadCurrentForce.Where(x => x.Description == "CXCB").Select(s => s.Values).FirstOrDefault());
            double D30 = Convert.ToDouble(loadCurrentForce.Where(x => x.Description == "CXYC").Select(s => s.Values).FirstOrDefault());

            var CXW = ((D23 * (D22 * D22) * D18 * (D27 * D21 / D17 + D29 * D19)) * 1 / 2) / 9810;
            var CYW = ((D23 * (D22 * D22) * D28 * D17 * D19) * 1 / 2) / 9810;
            var CXYW = ((D23 * D30 * (D22 * D22) * (D17 * D17) * D19) * 1 / 2) / 9810;
            CXW = double.IsInfinity(CXW) == true ? 0 : CXW;
            CYW = double.IsInfinity(CYW) == true ? 0 : CYW;
            CXYW = double.IsInfinity(CXYW) == true ? 0 : CXYW;

            list.ForEach(x =>
            {
                if (x.Description1 == "CXW") { x.Values = Convert.ToDecimal(CXW); }
                if (x.Description1 == "CYW") { x.Values = Convert.ToDecimal(CYW); }
                if (x.Description1 == "CXYW") { x.Values = Convert.ToDecimal(CXYW); }


            });

            return list;
        }


        private ObservableCollection<CurrentLoad> loadBasicParameters1 = new ObservableCollection<CurrentLoad>();
        public ObservableCollection<CurrentLoad> LoadBasicParameters1
        {
            get
            {
                return loadBasicParameters1;
            }
            set
            {
                loadBasicParameters1 = value;
                RaisePropertyChanged("LoadBasicParameters1");
            }
        }


        private ObservableCollection<WindForceCoefficients> loadCurrentForce = new ObservableCollection<WindForceCoefficients>();
        public ObservableCollection<WindForceCoefficients> LoadCurrentForce
        {
            get
            {
                return loadCurrentForce;
            }
            set
            {
                loadCurrentForce = value;
                RaisePropertyChanged("LoadCurrentForce");
            }
        }

        public static ObservableCollection<WindForceCoefficients> loadWindLoad1 = new ObservableCollection<WindForceCoefficients>();
        public ObservableCollection<WindForceCoefficients> LoadWindLoad1
        {
            get
            {
                return loadWindLoad1;
            }
            set
            {
                loadWindLoad1 = value;
                RaisePropertyChanged("LoadWindLoad1");
            }
        }


    }
}
