using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PasswordHoarder.Models.UI;
using PasswordHoarder.Stores;
using PasswordHoarder.Utils;
using PasswordHoarder.ViewModels.Commands;
using PasswordHoarder.Views;

namespace PasswordHoarder.ViewModels
{
    class BrowserViewModel : IViewModel
    {
        public PasswordList PasswordList { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand CopyCommand { get; set; }
        public ICommand DeleteEntryCommand { get; set; }
        public ICommand NavigateAddCommand { get; }
        public ICommand NavigateEditCommand { get; }
        public ICommand NavigateGeneratePasswordCommand { get; }
        public ObservableCollection<ContextAction> Actions { get; set; }

        public BrowserViewModel(NavigationStore navigationStore)
        {

            PasswordList = new PasswordList();
            NavigateAddCommand = new NavigationCommand<AddPasswordViewModel>(navigationStore);
            NavigateEditCommand = new NavigationCommand<EditPasswordViewModel>(navigationStore, _ => PasswordList.SelectedEntry != null);
            NavigateGeneratePasswordCommand = new NavigationCommand<GeneratePasswordViewModel>(navigationStore);

            RefreshCommand = new GenericCommand<object>
            {
                ExecuteDelegate = _ => PasswordList.Refresh()
            };

            DeleteEntryCommand = new GenericCommand<object>
            {
                ExecuteDelegate = _ => PasswordList.RemoveSelected(),
                CanExecuteDelegate = _ => PasswordList.SelectedEntry != null
            };

            CopyCommand = new GenericCommand<object>
            {
                ExecuteDelegate = _ => Clipboard.SetText(PasswordList.SelectedEntry.Password)
            };

            var showCommand = new GenericCommand<object>
            {
                ExecuteDelegate = _ => MessageBox.Show(PasswordList.SelectedEntry.Password, "Password", MessageBoxButton.OK, MessageBoxImage.Information)
            };

            Actions = new ObservableCollection<ContextAction>
            {
                new ("Copy Password", CopyCommand, @"/Resources/clipboard.png"),
                new ("Show Password", showCommand, @"/Resources/clipboard.png")
            };

            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new()
                {
                    Header = "Database",
                    SubItems = new ObservableCollection<MenuItemViewModel>
                    {
                        new() { Header = "Add New Entry", Command = NavigateAddCommand },
                        new() { Header = "Log out", Command = new GenericCommand<object> {ExecuteDelegate = _ =>
                        {
                            UserMetaInfo.Username = null;
                            UserMetaInfo.Password = null;
                            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);
                        }} },
                        new() { Header = "Quit", Command = new GenericCommand<object> { ExecuteDelegate = _ => Application.Current.Shutdown()}},
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
    }
}
