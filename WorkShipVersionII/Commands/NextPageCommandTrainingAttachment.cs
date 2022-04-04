using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.Commands
{
    public class NextPageCommandTrainingAttachment:ICommand
    {
        private TrainingAttachmentViewModell mainView;

        public NextPageCommandTrainingAttachment(TrainingAttachmentViewModell mainViews)
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
