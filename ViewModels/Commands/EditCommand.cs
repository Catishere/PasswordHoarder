using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordHoarder.ViewModels.Commands
{
    public class EditCommand : ICommand
    {
        public EditCommand(EditPasswordViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public EditPasswordViewModel ViewModel { get; private set; }

        public bool CanExecute(object parameter)
        {
            return ViewModel.CanEdit();
        }

        public void Execute(object parameter)
        {
            ViewModel.ExecuteEdit();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
