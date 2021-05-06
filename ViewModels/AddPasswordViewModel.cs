using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using PasswordHoarder.Models;
using PasswordHoarder.Models.UI;
using PasswordHoarder.Stores;
using PasswordHoarder.Utils;
using PasswordHoarder.ViewModels.Commands;

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
            var list = FileUtils.GetPasswords().ToList();
            int newId = 0;
            newId = list.Any() ? list.Max((e) => e.Id) : 1;
            PasswordEntryModel.Modified = DateTime.Now;
            PasswordEntryModel.Id = newId + 1;
            Console.WriteLine(PasswordEntryModel.Id);
            list.Add(PasswordEntryModel);
            FileUtils.StorePasswords(list);
            NavigateBackCommand.Execute(null);
        }
    }
}