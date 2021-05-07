using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PasswordHoarder.Models.UI;
using PasswordHoarder.Stores;
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
            NavigateBackCommand = new NavigationCommand<BrowserViewModel>(navigationStore);
        }
    }
}
