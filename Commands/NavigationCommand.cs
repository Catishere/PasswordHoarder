using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PasswordHoarder.Stores;
using PasswordHoarder.ViewModels;

namespace PasswordHoarder.Commands
{
    internal class NavigationCommand<TViewModel> : ICommand
        where TViewModel : IViewModel
    {
        private readonly NavigationStore _navigation;

        public NavigationCommand(NavigationStore navigationStore)
        {
            _navigation = navigationStore;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _navigation.CurrentViewModel = (IViewModel)Activator.CreateInstance(typeof(TViewModel), _navigation);
        }
    }
}
