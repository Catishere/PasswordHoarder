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
using PasswordHoarder.Annotations;
using PasswordHoarder.Stores;
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
            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new()
                {
                    Header = "Database",
                    SubItems = new ObservableCollection<MenuItemViewModel>
                    {
                        new() { Header = "Import", Command = new GenericCommand<object> { ExecuteDelegate = _ => Console.WriteLine("Import")}},
                        new() { Header = "Export", Command = new GenericCommand<object> { ExecuteDelegate = _ => Console.WriteLine("Export")}},
                        new() { Header = "Quit", Command = new GenericCommand<object> { ExecuteDelegate = _ => System.Windows.Application.Current.Shutdown()}}
                    }
                },
                new()
                {
                    Header = "Help",
                    SubItems = new ObservableCollection<MenuItemViewModel>
                    {
                        new() { Header = "User Guide", Command = new GenericCommand<object> { ExecuteDelegate = _ => Console.WriteLine("Guide")}},
                        new() { Header = "About", Command = new GenericCommand<object> { ExecuteDelegate = _ => Console.WriteLine("About")}},
                    }
                }
            };
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
