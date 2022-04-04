using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursView;

namespace WorkShipVersionII.WorkHoursViewModel
{
      public class ChildPopUPModelWorkHours : ViewModelBase
       {
              public RelayCommand ShowChildWindowAddLogoBook { get; }
              public ChildPopUPModelWorkHours()
              {
                     ShowChildWindowAddLogoBook = new RelayCommand(ShowChildWindowLogoBookMethod);
              }

              private void ShowChildWindowLogoBookMethod()
              {
                     AddLogbookViewModel vm = new AddLogbookViewModel();
                     ChildWindowManager.Instance.ShowChildWindow(new AddLogbookView() { DataContext = vm });
              }
       }
}
