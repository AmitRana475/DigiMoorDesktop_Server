using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursViewModel;


namespace WorkShipVersionII.Commands
{
   public class NextPageCommandWinchBrakeTest : ICommand
    {
        private WinchBrakeTestRecordViewModel mainView;

        public NextPageCommandWinchBrakeTest(WinchBrakeTestRecordViewModel mainViews)
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
            return mainView.TotalPages - 1 > mainView.CurrentPageIndex;
        }

        public void Execute(object parameter)
        {
            mainView.ShowNextPage();
        }
    }
}
