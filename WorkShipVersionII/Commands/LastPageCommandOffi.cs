using System;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.Commands
{
    public class LastPageCommandOffi : ICommand
    {
        private readonly NotificationViewModel mainView;
        public LastPageCommandOffi(NotificationViewModel mainViews)
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
            return mainView.CurrentOffiPage != mainView.TotalOffiPages;
        }

        public void Execute(object parameter)
        {
            mainView.ShowOffiLastPage();
        }
    }
}
