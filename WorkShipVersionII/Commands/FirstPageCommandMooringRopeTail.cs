using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.Commands
{
    public class FirstPageCommandMooringRopeTail : ICommand
    {
        private MooringWinchTailViewModel mainView;
        public FirstPageCommandMooringRopeTail(MooringWinchTailViewModel mainViews)
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
            return mainView.CurrentPageIndex != 0;
        }

        public void Execute(object parameter)
        {
            mainView.ShowFirstPage();
        }
    }
}
