using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.Commands
{
   public class LastPageCommandWinchBrakeTest :ICommand
    {
        private WinchBrakeTestRecordViewModel mainView;
        public LastPageCommandWinchBrakeTest(WinchBrakeTestRecordViewModel mainViews)
        {
            mainView = mainViews;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            return mainView.CurrentPage != mainView.TotalPages;
        }

        public void Execute(object parameter)
        {
            mainView.ShowLastPage();
        }
    }
}
