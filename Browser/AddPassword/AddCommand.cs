using System;
using System.Windows.Input;
using PasswordHoarder.ViewModels;

namespace PasswordHoarder.Browser.AddPassword
{
    public class AddCommand : ICommand
    {
        public AddCommand(AddPasswordViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public AddPasswordViewModel ViewModel { get; private set; }

        public bool CanExecute(object parameter)
        {
            return ViewModel.CanAdd();
        }

        public void Execute(object parameter)
        {
            ViewModel.ExecuteAdd();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}