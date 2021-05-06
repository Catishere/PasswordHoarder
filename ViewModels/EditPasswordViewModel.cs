using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PasswordHoarder.Models;
using PasswordHoarder.Models.UI;
using PasswordHoarder.Stores;
using PasswordHoarder.Utils;
using PasswordHoarder.ViewModels.Commands;

namespace PasswordHoarder.ViewModels
{
    public class EditPasswordViewModel : IViewModel
    {
        public PasswordEntryModel PasswordEntryModel { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand NavigateBackCommand { get; }

        public EditPasswordViewModel(NavigationStore navigationStore, object parameter)
        {
            NavigateBackCommand = new NavigationCommand<BrowserViewModel>(navigationStore);
            PasswordEntryModel = new PasswordEntryModel();
            PasswordEntryModel.Copy((IPasswordEntry)parameter);
            EditCommand = new EditCommand(this);
        }
        public bool CanEdit()
        {
            return !(PasswordEntryModel.Title.Equals(string.Empty) || PasswordEntryModel.Password.Equals(string.Empty));
        }

        public void ExecuteEdit()
        {
            var list = FileUtils.GetPasswords().ToList();
            PasswordEntryModel.Modified = DateTime.Now;
            PasswordEntryModel entry = (PasswordEntryModel)(from e in list
                                    where e.Id == PasswordEntryModel.Id
                                    select e).SingleOrDefault();
            entry?.Copy(PasswordEntryModel);

            FileUtils.StorePasswords(list);
            NavigateBackCommand.Execute(null);
        }

    }
}
