using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using PasswordHoarder.ViewModels.Commands;

namespace PasswordHoarder.ViewModels
{
    public class MenuItemViewModel
    {
        public MenuItemViewModel()
        {
            Command = new GenericCommand<object>{
                ExecuteDelegate = _ => Debug.WriteLine("Default")
            };
        }

        public string Header { get; set; }

        public ObservableCollection<MenuItemViewModel> SubItems { get; set; }

        public ICommand Command { get; set; }
    }
}