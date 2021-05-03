using System;
using System.Windows.Input;
using PasswordHoarder.ViewModels;

namespace PasswordHoarder.Login
{
    internal class RegisterCommand : ICommand
    {
        private readonly LoginViewModel _viewModel;

        public RegisterCommand(LoginViewModel loginViewModel)
        {
            _viewModel = loginViewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanLogin;
        }

        public void Execute(object parameter)
        {
            _viewModel.ExecuteRegister();
        }
    }
}
