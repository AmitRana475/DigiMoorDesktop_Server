using System;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.Commands
{
    public class NextPageCommand : ICommand
    {
        private readonly NotificationsViewModel mainView;
      
        public NextPageCommand(NotificationsViewModel mainViews)
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
