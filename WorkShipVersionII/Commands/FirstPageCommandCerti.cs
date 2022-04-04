using System;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.Commands
{
    public class FirstPageCommandCerti : ICommand
    {
        private NotificationViewModel mainView;
        public FirstPageCommandCerti(NotificationViewModel mainViews)
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
            return mainView.CurrentPageCertiIndex != 0;
        }

        public void Execute(object parameter)
        {
            mainView.ShowCertiFirstPage();
        }
    }
}
