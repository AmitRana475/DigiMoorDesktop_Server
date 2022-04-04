using System;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.Commands
{
    public class NextPageCommandCerti : ICommand
    {
        private readonly NotificationViewModel mainView;
        public NextPageCommandCerti(NotificationViewModel mainViews)
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
            return mainView.TotalCertiPages - 1 > mainView.CurrentPageCertiIndex;
        }

        public void Execute(object parameter)
        {
            mainView.ShowCertiNextPage();
        }
    }
}
