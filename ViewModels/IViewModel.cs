using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PasswordHoarder.Stores;

namespace PasswordHoarder.ViewModels
{
    public interface IViewModel
    {
        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
    }
}
