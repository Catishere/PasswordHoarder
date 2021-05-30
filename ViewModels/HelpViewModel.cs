using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PasswordHoarder.Stores;
using PasswordHoarder.Utils;
using PasswordHoarder.ViewModels.Commands;

namespace PasswordHoarder.ViewModels
{
    public class HelpViewModel : IViewModel
    {
        public ICommand NavigateBackCommand { get; }

        public HelpViewModel(NavigationStore navigationStore)
        {
            NavigateBackCommand = UserMetaInfo.Username == null ?
                new NavigationCommand<LoginViewModel>(navigationStore) :
                new NavigationCommand<BrowserViewModel>(navigationStore);
        }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
    }
}
