using System;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.Commands
{
    public class PreviousPageCommand : ICommand
    {
        private readonly NotificationViewModel mainView;
        public PreviousPageCommand(NotificationViewModel mainViews)
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
