using System;
using System.Collections.Generic;
using System.Windows.Input;
using PasswordHoarder.Browser.AddPassword;
using PasswordHoarder.Commands;
using PasswordHoarder.Models;
using PasswordHoarder.Stores;
using PasswordHoarder.Utils;

namespace PasswordHoarder.ViewModels
{
    public class AddPasswordViewModel : IViewModel
    {
        public PasswordEntryModel PasswordEntryModel { get; set; }
        public ICommand AddCommand { get; set; }

        public ICommand NavigateBackCommand { get; }

        public AddPasswordViewModel(NavigationStore navigationStore)
        {
            NavigateBackCommand = new NavigationCommand<BrowserViewModel>(navigationStore);
            AddCommand = new AddCommand(this);
            PasswordEntryModel = new PasswordEntryModel();
        }

        public bool CanAdd()
        {
            return !(PasswordEntryModel.Title.Equals(string.Empty) || PasswordEntryModel.Password.Equals(string.Empty));
        }

        public void ExecuteAdd()
        {
            var list = new List<IPasswordEntry>();
            list.AddRange(FileUtils.GetPasswords());
            PasswordEntryModel.Modified = DateTime.Now;
            list.Add(PasswordEntryModel);
            FileUtils.StorePasswords(list);
            NavigateBackCommand.Execute(null);
        }

        public IViewModel CurrentViewModel { get; }
    }
}