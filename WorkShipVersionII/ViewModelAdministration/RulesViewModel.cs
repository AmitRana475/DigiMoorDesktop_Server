using DataBuildingLayer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace WorkShipVersionII.ViewModelAdministration
{
       public class RulesViewModel : ViewModelBase
    {
        private readonly ShipmentContaxt sc;
        private BackgroundWorker _Rules;
        private BackgroundWorker _Opa90;
        private Grid DynamicGrid = new Grid();
        public RulesViewModel()
        {
            if (sc == null)
            {
                sc = new ShipmentContaxt();
                sc.Configuration.ProxyCreationEnabled = false;
            }

            _RulesCommand = new RelayCommand(() => _Rules.RunWorkerAsync(), () => !_Rules.IsBusy);
            _Rules = new BackgroundWorker();
            _Rules.WorkerReportsProgress = true;
            _Rules.DoWork += RulesMethod;
            _Rules.ProgressChanged += new ProgressChangedEventHandler(RulesProgress);
            _Rules.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RulesCompleted);




            _Opa90Command = new RelayCommand(() => _Opa90.RunWorkerAsync(), () => !_Opa90.IsBusy);
            _Opa90 = new BackgroundWorker();
            _Opa90.WorkerReportsProgress = true;
            _Opa90.DoWork += OPa90Method;
            _Opa90.ProgressChanged += new ProgressChangedEventHandler(Opa90ProgressChanged);
            _Opa90.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Opa90Completed);

            RadioBTNCommand = new RelayCommand<object>(RadioBTNmethod);


           
            RaisePropertyChanged("OpaRuleName");
        }

        private void RadioBTNmethod(object parameter)
        {

         
            RaisePropertyChanged("OpaRuleNames");

        }

        public ICommand RadioBTNCommand { get; private set; }
        private ICommand _RulesCommand;
        public ICommand RulesCommand
        {
            get
            {
                return _RulesCommand;
            }

        }

        private ICommand _Opa90Command;
        public ICommand Opa90Command
        {
            get
            {
                return _Opa90Command;
            }

        }


        private double _RuleProgress;
        public double RuleProgress
        {
            get
            {

                return _RuleProgress;
            }
            set
            {
                _RuleProgress = value;
                RaisePropertyChanged("RuleProgress");
            }
        }

        private double _Opa90Progress;
        public double Opa90Progress
        {
            get
            {

                return _Opa90Progress;
            }
            set
            {
                _Opa90Progress = value;
                RaisePropertyChanged("Opa90Progress");
            }
        }



        private string ruleVisibles;
        public string RuleVisibles
        {
            get
            {
                if (ruleVisibles == null)
                {
                    ruleVisibles = "Collapsed";
                }
                return ruleVisibles;
            }
            set
            {
                ruleVisibles = value;
                RaisePropertyChanged("RuleVisibles");
            }
        }

        private string opaVisibles;
        public string OpaVisibles
        {
            get
            {
                if (opaVisibles == null)
                {
                    opaVisibles = "Collapsed";
                }
                return opaVisibles;
            }
            set
            {
                opaVisibles = value;
                RaisePropertyChanged("OpaVisibles");
            }
        }

        private OpaRuleTypes _OpaRuleName;
        public OpaRuleTypes OpaRuleName
        {
            get
            {
                if (_OpaRuleName == null)
                    _OpaRuleName = new OpaRuleTypes();
                return _OpaRuleName;

            }
            set
            {
                _OpaRuleName = value;
                RaisePropertyChanged("OpaRuleName");
            }
        }

       
      

       

      
        


      
       
       

        private void Opa90Completed(object sender, RunWorkerCompletedEventArgs e)
        {
           
            OpaVisibles = "Collapsed";

           
        }

        private void Opa90ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _Opa90Progress = e.ProgressPercentage;
            RaisePropertyChanged("Opa90Progress");
        }

        private void RulesProgress(object sender, ProgressChangedEventArgs e)
        {
            _RuleProgress = e.ProgressPercentage;
            RaisePropertyChanged("RuleProgress");
        }

        private void RulesCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
        }


              private void RulesMethod(object sender, DoWorkEventArgs e)
              {
              }

               

        private bool CheckDateYoung(DateTime dt1, DateTime dt2)
        {
            var ssa = dt2.AddYears(-18) < dt1;
            return ssa;
        }


              private void OPa90Method(object sender, DoWorkEventArgs e)
              {
              }

                   


    }

    public class OpaRuleTypes
    {
        public bool OPA90option1 { get; set; }
        public bool OPA90option2 { get; set; }
        public bool OPA90option3 { get; set; }


    }


}
