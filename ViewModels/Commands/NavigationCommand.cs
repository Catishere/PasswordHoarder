using System;
using System.Windows.Input;
using PasswordHoarder.Stores;

namespace PasswordHoarder.ViewModels.Commands
{
    internal class NavigationCommand<TViewModel> : ICommand
        where TViewModel : IViewModel
    {
        private readonly NavigationStore _navigation;
        private readonly Predicate<object> _canExecute;
        public NavigationCommand(NavigationStore navigationStore)
        {
            _navigation = navigationStore;
        }

        public NavigationCommand(NavigationStore navigationStore, Predicate<object> predicate)
        {
            _navigation = navigationStore;
            _canExecute = predicate;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute(parameter);
            return true;// if there is no can execute default to true
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
                _navigation.CurrentViewModel = (IViewModel)Activator.CreateInstance(typeof(TViewModel), _navigation, parameter);
            else
                _navigation.CurrentViewModel = (IViewModel)Activator.CreateInstance(typeof(TViewModel), _navigation);
        }
    }
}
