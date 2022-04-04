using System;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.Commands
{
    public class LastPageCommandCerti : ICommand
    {
        private readonly NotificationViewModel mainView;
        public LastPageCommandCerti(NotificationViewModel mainViews)
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
            return mainView.CurrentCertiPage != mainView.TotalCertiPages;
        }

        public void Execute(object parameter)
        {
            mainView.ShowCertiLastPage();
        }
    }
}
