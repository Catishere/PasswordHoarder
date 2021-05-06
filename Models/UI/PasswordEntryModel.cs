using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using PasswordHoarder.Annotations;
using PasswordHoarder.Utils;

namespace PasswordHoarder.Models.UI
{
    public class PasswordEntryModel : IPasswordEntry, INotifyPropertyChanged, ICopyEntry
    {
#nullable enable
        private int _id;
        private string _title;
        private string? _username;
        private SecureString _password;
        private string? _url;
        private DateTime? _expireDate;
        private DateTime? _created;
        private DateTime? _modified;
        private string? _notes;

        public PasswordEntryModel(int id, string title, string? username, SecureString password, string? url, DateTime? expireDate, DateTime? created, DateTime? modified, string? notes)
        {
            _id = id;
            _title = title;
            _username = username;
            _password = password;
            _url = url;
            _expireDate = expireDate;
            _created = created;
            _modified = modified;
            _notes = notes;
        }

        public PasswordEntryModel()
        {
            _title = "";
            _password = new SecureString();
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string? Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => PasswordUtils.SecureStringToString(_password);
            set
            {
                _password = new SecureString();
                var chars = value.ToCharArray();

                foreach (var c in chars)
                    _password.AppendChar(c);

                _password.MakeReadOnly();
                OnPropertyChanged();
            }
        }
        public string? Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyChanged();
            }
        }
        public DateTime? ExpireDate
        {
            get => _expireDate;
            set
            {
                _expireDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime? Created
        {
            get => _created;
            set
            {
                _created = value;
                OnPropertyChanged();
            }
        }
        public DateTime? Modified
        {
            get => _modified;
            set
            {
                _modified = value;
                OnPropertyChanged();
            }
        }
        public string? Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }
#nullable disable
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CompareTo(object other)
        {
            return string.Compare(Title, ((IPasswordEntry)other).Title, StringComparison.OrdinalIgnoreCase);
        }

        public void Copy(IPasswordEntry other)
        {
            Id = other.Id;
            Title = other.Title;
            Username = other.Username;
            Password = other.Password;
            Url = other.Url;
            ExpireDate = other.ExpireDate;
            Notes = other.Notes;
        }
    }
}