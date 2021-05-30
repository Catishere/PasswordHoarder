using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using PasswordHoarder.Annotations;
using PasswordHoarder.Models.UI;
using PasswordHoarder.Stores;
using PasswordHoarder.Utils;
using PasswordHoarder.ViewModels.Commands;

namespace PasswordHoarder.ViewModels
{
    class MainViewModel : IViewModel, INotifyPropertyChanged
    {
        private NavigationStore _navigationStore;
        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            MenuItems = CurrentViewModel.MenuItems;
        }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }

        private void OnCurrentViewModelChanged()
        {
            MenuItems = CurrentViewModel.MenuItems;
            OnPropertyChanged(nameof(MenuItems));
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public IViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
