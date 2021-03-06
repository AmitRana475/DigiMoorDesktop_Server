using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.Commands
{
    public class PreviousPageCommandResidualTail : ICommand
    {
        private readonly TailResidualLabTestListViewModel mainView;

        public PreviousPageCommandResidualTail(TailResidualLabTestListViewModel mainViews)
        {
            mainView = mainViews;
        }


        public bool CanExecute(object parameter)
        {
            return mainView.CurrentPageIndex != 0;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            mainView.ShowPreviousPage();
        }
    }
}
