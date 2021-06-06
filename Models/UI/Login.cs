using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using PasswordHoarder.Annotations;
using PasswordHoarder.Utils;

namespace PasswordHoarder.Models.UI
{
    public class Login : INotifyPropertyChanged
    {
        private string _username;
        private string _dbFileName;

        public Login(string username, SecureString password)
        {
            _username = username;
            SecurePassword = password;
        }

        public Login()
        {
            UpdateFilename();
        }

        public void UpdateFilename()
        {
            DbFileName = "Current database:\n" + FileUtils.Filename;
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string DbFileName
        {
            get => _dbFileName;
            set
            {
                _dbFileName = value;
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
