using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
using PasswordHoarder.Annotations;
using PasswordHoarder.Utils;

namespace PasswordHoarder.Models.UI
{
    internal class PasswordList : INotifyPropertyChanged
    {
        private string _match = "";
        private IPasswordEntry _selectedEntry;
        private bool _showPassword;
        public bool HasError = false;

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
                _showPassword = false;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ShowPassword));
                OnPropertyChanged(nameof(EntryPasswordConceal));
            }
        }
        public bool ShowPassword
        {
            get => _showPassword;
            set
            {
                _showPassword = value && SelectedEntry != null;
                OnPropertyChanged(nameof(EntryPasswordConceal));
            }
        }

        public string EntryPasswordConceal => _showPassword ? SelectedEntry.Password : SelectedEntry == null ? "" : "*****";

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Refresh()
        {
            Passwords = new List<IPasswordEntry>();
            try
            {
                Passwords.AddRange(FileUtils.GetPasswords());
            }
            catch (JsonException)
            {
                HasError = true;
            }

            Passwords.Sort();
            DisplayedPasswords = Passwords;
            OnPropertyChanged(nameof(DisplayedPasswords));
        }

        public void RemoveSelected()
        {
            if (SelectedEntry == null) return;
            Console.WriteLine($@"Deleted {SelectedEntry.Password}");
            Passwords.Remove(SelectedEntry);
            FileUtils.StorePasswords(Passwords);
            Refresh();
        }
    }
}