using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkShipVersionII.ViewModel;

namespace WorkShipVersionII.Commands
{
   public class LastPageCommandTrainingAttachment:ICommand
    {
        private TrainingAttachmentViewModell mainView;
        public LastPageCommandTrainingAttachment(TrainingAttachmentViewModell mainViews)
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

