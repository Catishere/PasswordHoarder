using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PasswordHoarder.Stores;
using PasswordHoarder.ViewModels.Commands;

namespace PasswordHoarder.ViewModels
{
    public class AboutViewModel : IViewModel
    {
        public string Version { get; set; }
        public ICommand NavigateBackCommand { get; }

        public AboutViewModel(NavigationStore navigationStore)
        {
            NavigateBackCommand = new NavigationCommand<BrowserViewModel>(navigationStore);
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
    }
}
