using System;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.Commands
{
    public class FirstPageCommand : ICommand
    {
        private NotificationsViewModel mainView;
      
        public FirstPageCommand(NotificationsViewModel mainViews)
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
