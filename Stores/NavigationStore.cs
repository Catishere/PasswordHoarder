using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordHoarder.ViewModels;

namespace PasswordHoarder.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;

        public NavigationStore()
        {
            CurrentViewModel = new LoginViewModel(this);
        }

        private IViewModel _currentViewModel;
        public IViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }

        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
