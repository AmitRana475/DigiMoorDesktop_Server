using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.ViewsMooringCalulator;

namespace WorkShipVersionII.ViewModelMooringCalculator
{
  public class ChildPopUPModelMooringCalculator : ViewModelBase
    {
              public RelayCommand ShowChildWindowAddMooringLine { get; }
              public ChildPopUPModelMooringCalculator()
              {
                    // ShowChildWindowAddMooringLine = new RelayCommand(ShowChildWindowAddMooringLineMethod);
              }
              private void ShowChildWindowAddMooringLineMethod()
              {
                     //AddMooringViewModel vm = new AddMooringViewModel();
                     //ChildWindowManager.Instance.ShowChildWindow(new AddMooringView() { DataContext = vm });
              }
       }
}
