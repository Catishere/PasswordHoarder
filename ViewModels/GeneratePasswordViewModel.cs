using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PasswordHoarder.Models.UI;
using PasswordHoarder.Stores;
using PasswordHoarder.ViewModels.Commands;

namespace PasswordHoarder.ViewModels
{
    class GeneratePasswordViewModel : IViewModel
    {
        public ICommand NavigateBackCommand { get; }

        public PasswordGenerator PasswordGenerator { get; set; }

        public GeneratePasswordViewModel(NavigationStore navigationStore)
        {
            PasswordGenerator = new PasswordGenerator();
            NavigateBackCommand = new NavigationCommand<BrowserViewModel>(navigationStore);
        }
    }
}
