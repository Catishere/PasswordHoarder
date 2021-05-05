using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
using PasswordHoarder.Annotations;

namespace PasswordHoarder.Models.UI
{
    class ContextAction : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public ICommand Action { get; set; }
        public Brush Icon { get; set; }

        public ContextAction()
        {
        }

        public ContextAction(string name, ICommand action, Brush icon)
        {
            Name = name;
            Action = action;
            Icon = icon;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
