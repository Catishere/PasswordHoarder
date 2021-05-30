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
            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new()
                {
                    Header = "Database",
                    SubItems = new ObservableCollection<MenuItemViewModel>
                    {
                        new() { Header = "Import", Command = new GenericCommand<object> { ExecuteDelegate = _ =>
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog
                            {
                                Filter = "Keystore files (*.kdb)|*.kdb"
                            };
                            if (openFileDialog.ShowDialog() == true)
                                FileUtils.Filename = openFileDialog.FileName;
                        }}},
                        new() { Header = "Export", Command = new GenericCommand<object> { ExecuteDelegate = _ =>
                        {
                            SaveFileDialog saveFileDialog = new SaveFileDialog
                            {
                                Filter = "Keystore files (*.kdb)|*.kdb"
                            };
                        }}},
                        new() { Header = "Quit", Command = new GenericCommand<object> { ExecuteDelegate = _ => System.Windows.Application.Current.Shutdown()}}
                    }
                },
                new()
                {
                    Header = "Tools",
                    SubItems = new ObservableCollection<MenuItemViewModel>
                    {
                        new() { Header = "Password Generator", Command = new NavigationCommand<GeneratePasswordViewModel>(navigationStore)}
                    }
                },
                new()
                {
                    Header = "Help",
                    SubItems = new ObservableCollection<MenuItemViewModel>
                    {
                        new() { Header = "User Guide", Command = new NavigationCommand<HelpViewModel>(navigationStore)},
                        new() { Header = "About", Command = new NavigationCommand<AboutViewModel>(navigationStore)}
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
