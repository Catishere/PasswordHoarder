using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using PasswordHoarder.Annotations;
using PasswordHoarder.Models;
using PasswordHoarder.Utils;

namespace PasswordHoarder.Browser
{
    internal class PasswordList : INotifyPropertyChanged
    {
        private string _match = "";
        private IPasswordEntry _selectedEntry = null;

        public PasswordList()
        {
            Refresh();
        }

        public List<IPasswordEntry> Passwords { get; set; }
        public IEnumerable<IPasswordEntry> DisplayedPasswords { get; set; }

        public string Match
        {
            get => _match;
            set
            {
                _match = value.ToLower();

                var query = from word in Passwords
                    where word.Title.ToLower().Contains(Match)
                    orderby word.Title
                    select word;

                DisplayedPasswords = query.ToList();
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayedPasswords));
            }
        }

        public IPasswordEntry SelectedEntry
        {
            get => _selectedEntry;
            set
            {
                _selectedEntry = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Refresh()
        {
            Passwords = new List<IPasswordEntry>();
            Passwords.AddRange(FileUtils.GetPasswords());
            Passwords.Sort();
            DisplayedPasswords = Passwords;
            OnPropertyChanged(nameof(DisplayedPasswords));
        }

        public void RemoveSelected()
        {
            Console.WriteLine($@"Deleted {SelectedEntry.Password}");
            Passwords.Remove(SelectedEntry);
            FileUtils.StorePasswords(Passwords);
            Refresh();
        }
    }
}