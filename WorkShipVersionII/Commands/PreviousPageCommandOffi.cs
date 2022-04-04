using System;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.Commands
{
    public class PreviousPageCommandOffi : ICommand
    {
        private readonly NotificationViewModel mainView;
        public PreviousPageCommandOffi(NotificationViewModel mainViews)
        {
            mainView = mainViews;
        }

        public bool CanExecute(object parameter)
        {
            return mainView.CurrentPageOffiIndex != 0;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            mainView.ShowOffiPreviousPage();
        }
    }
}
