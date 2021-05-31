using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using PasswordHoarder.ViewModels.Commands;
using PasswordHoarder.DAL;
using PasswordHoarder.Models;
using PasswordHoarder.Models.DB;
using PasswordHoarder.Models.UI;
using PasswordHoarder.Stores;
using PasswordHoarder.Utils;
using PasswordHoarder.Views;

namespace PasswordHoarder.ViewModels
{
    class LoginViewModel : IViewModel
    {
        private readonly UserService _userService;
        private NavigationStore _navigationStore;
        public ICommand LoginCommand { get; private set; }
        public ICommand RegisterCommand { get; private set; }
        public Login Login { get; set; }
        public bool CanLogin
        {
            get
            {
                if (Login == null)
                    return false;
                return !string.IsNullOrEmpty(Login.Username);
            }
        }

        public LoginViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _userService = new UserService(new UserContext());
            Login = new Login();
            LoginCommand = new LoginCommand(this);
            RegisterCommand = new RegisterCommand(this);

            MenuItems = new ObservableCollection<MenuItemViewModel>
            {
                new()
                {
                    Header = "Database",
                    SubItems = new ObservableCollection<MenuItemViewModel>
                    {
                        new() { Header = "Import", Command = new GenericCommand<object> { ExecuteDelegate = _ =>
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog
                            {
                                Filter = "Keystore files (*.kdb)|*.kdb"
                            };
                            if (openFileDialog.ShowDialog() == true)
                                FileUtils.Filename = openFileDialog.FileName;
                        }}},
                        new() { Header = "Export", Command = new GenericCommand<object> { ExecuteDelegate = _ =>
                        {
                            SaveFileDialog saveFileDialog = new SaveFileDialog
                            {
                                Filter = "Keystore files (*.kdb)|*.kdb"
                            };
                            if (saveFileDialog.ShowDialog() == true)
                                File.Copy(FileUtils.Filename, saveFileDialog.FileName);
                        }}},
                        new() { Header = "Quit", Command = new GenericCommand<object> { ExecuteDelegate = _ => Application.Current.Shutdown()}}
                    }
                },
                new()
                {
                    Header = "Tools",
                    SubItems = new ObservableCollection<MenuItemViewModel>
                    {
                        new() { Header = "Password Generator", Command = new NavigationCommand<GeneratePasswordViewModel>(navigationStore)}
                    }
                },
                new()
                {
                    Header = "Help",
                    SubItems = new ObservableCollection<MenuItemViewModel>
                    {
                        new() { Header = "User Guide", Command = new NavigationCommand<HelpViewModel>(navigationStore)},
                        new() { Header = "About", Command = new NavigationCommand<AboutViewModel>(navigationStore)}
                    }
                }
            };
        }

        public void ExecuteLogin()
        {
            bool log = _userService.Login(Login.Username, Login.SecurePassword);
            Console.WriteLine(log ? "Logged" : "Login failed");
            if (log)
            {
                UserMetaInfo.Username = Login.Username;
                _navigationStore.CurrentViewModel = new BrowserViewModel(_navigationStore);
            }
            else
            {
                MessageBox.Show("Wrong password or username!",
                    "Login unsuccessful",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public void ExecuteRegister()
        {
            _userService.Register(Login.Username, Login.SecurePassword);
        }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; set; }
    }
}
