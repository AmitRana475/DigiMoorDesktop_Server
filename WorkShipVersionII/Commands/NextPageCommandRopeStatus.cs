using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.Commands
{
   public class NextPageCommandRopeStatus:ICommand
    {
        private RopeStatusViewModel mainView;

        public NextPageCommandRopeStatus(RopeStatusViewModel mainViews)
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
