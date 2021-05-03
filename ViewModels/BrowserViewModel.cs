using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using PasswordHoarder;
using PasswordHoarder.Browser;
using PasswordHoarder.Commands;
using PasswordHoarder.Stores;
using PasswordHoarder.Utils;
using PasswordHoarder.ViewModels;

namespace PasswordHoarder.ViewModels
{
    class BrowserViewModel : IViewModel
    {
        public PasswordList PasswordList { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteEntryCommand { get; set; }
        public ICommand NavigateAddCommand { get; }
        public ObservableCollection<ContextAction> Actions { get; set; }

        public BrowserViewModel(NavigationStore navigationStore)
        {

            PasswordList = new PasswordList();
            NavigateAddCommand = new NavigationCommand<AddPasswordViewModel>(navigationStore);

            RefreshCommand = new GenericCommand<object>
            {
                ExecuteDelegate = _ => PasswordList.Refresh()
            };

            DeleteEntryCommand = new GenericCommand<object>
            {
                ExecuteDelegate = _ => PasswordList.RemoveSelected()
            };

            var copyCommand = new GenericCommand<object>
            {
                ExecuteDelegate = _ => Clipboard.SetText(PasswordList.SelectedEntry.Password)
            };

            var showCommand = new GenericCommand<object>
            {
                ExecuteDelegate = _ => MessageBox.Show(PasswordList.SelectedEntry.Password, "Password", MessageBoxButton.OK, MessageBoxImage.Information)
            };

            Actions = new ObservableCollection<ContextAction>
            {
                new ("Copy Password", copyCommand, Brushes.Tomato),
                new ("Show Password", showCommand, Brushes.Blue)
            };
        }

        public IViewModel CurrentViewModel { get; }
    }
}
