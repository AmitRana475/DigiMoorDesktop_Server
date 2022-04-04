using System;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.Commands
{
    public class NextPageCommandOffi : ICommand
    {
        private readonly NotificationViewModel mainView;
        public NextPageCommandOffi(NotificationViewModel mainViews)
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
            return mainView.TotalOffiPages - 1 > mainView.CurrentPageOffiIndex;
        }

        public void Execute(object parameter)
        {
            mainView.ShowOffiNextPage();
        }
    }
}
