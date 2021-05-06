using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using PasswordHoarder.Annotations;
using PasswordHoarder.Utils;

namespace PasswordHoarder.Models.UI
{
    internal class PasswordGenerator : INotifyPropertyChanged
    {
        private int _size = 28;
        private string _password;
        private readonly List<Symbols.SymbolType> _options;

        public PasswordGenerator()
        {
            _options = new List<Symbols.SymbolType>()
            {
                Symbols.SymbolType.Upper,
                Symbols.SymbolType.Lower,
                Symbols.SymbolType.Number,
                Symbols.SymbolType.Special
            };
            Password = PasswordUtils.GeneratePassword(_options.ToArray(), _size);
        }

        public int Size
        {
            get => _size;
            set
            {
                _size = value;
                _password = PasswordUtils.GeneratePassword(_options.ToArray(), _size);
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged();
            }
        }
        public bool Upper
        {
            get => _options.Contains(Symbols.SymbolType.Upper);
            set
            {
                if (value)
                    _options.Add(Symbols.SymbolType.Upper);
                else
                    _options.RemoveAll((type => type == Symbols.SymbolType.Upper));
                OnPropertyChanged();
            }
        }
        public bool Lower
        {
            get => _options.Contains(Symbols.SymbolType.Lower);
            set
            {
                if (value)
                    _options.Add(Symbols.SymbolType.Lower);
                else
                    _options.RemoveAll((type => type == Symbols.SymbolType.Lower));
                OnPropertyChanged();
            }
        }
        public bool Number
        {
            get => _options.Contains(Symbols.SymbolType.Number);
            set
            {
                if (value)
                    _options.Add(Symbols.SymbolType.Number);
                else
                    _options.RemoveAll((type => type == Symbols.SymbolType.Number));
                OnPropertyChanged();
            }
        }
        public bool Special
        {
            get => _options.Contains(Symbols.SymbolType.Special);
            set
            {
                if (value)
                    _options.Add(Symbols.SymbolType.Special);
                else
                    _options.RemoveAll((type => type == Symbols.SymbolType.Special));
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}