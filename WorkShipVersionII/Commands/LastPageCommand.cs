using System;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;
using WorkShipVersionII.WorkHoursViewModel;

namespace WorkShipVersionII.Commands
{
    public class LastPageCommand : ICommand
    {
        private readonly NotificationsViewModel mainView;
     
        public LastPageCommand(NotificationsViewModel mainViews)
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
