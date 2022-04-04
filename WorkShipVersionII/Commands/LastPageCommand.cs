using System;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.Commands
{
    public class LastPageCommand : ICommand
    {
        private readonly NotificationViewModel mainView;
        public LastPageCommand(NotificationViewModel mainViews)
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
