using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using PasswordHoarder.Annotations;

namespace PasswordHoarder.Login
{
    public class Login : INotifyPropertyChanged
    {
        private string _username;

        public Login(string username, SecureString password)
        {
            _username = username;
            SecurePassword = password;
        }

        public Login() { }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public SecureString SecurePassword { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
