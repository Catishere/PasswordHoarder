using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PasswordHoarder.Models.UI;
using PasswordHoarder.Stores;
using PasswordHoarder.Utils;
using PasswordHoarder.ViewModels.Commands;

namespace PasswordHoarder.ViewModels
{
    class GeneratePasswordViewModel : IViewModel
    {
        public ICommand NavigateBackCommand { get; }
        public ICommand CopyCommand { get; set; }

        public PasswordGenerator PasswordGenerator { get; set; }

        public GeneratePasswordViewModel(NavigationStore navigationStore)
        {
            
            CopyCommand = new GenericCommand<object>
            {
                ExecuteDelegate = _ => Clipboard.SetText(PasswordGenerator.Password)
            };
            PasswordGenerator = new PasswordGenerator();
            NavigateBackCommand = UserMetaInfo.Username == null ?
                new NavigationCommand<LoginViewModel>(navigationStore) :
                new NavigationCommand<BrowserViewModel>(navigationStore);
        }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
    }
}
