using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PasswordHoarder.Models.DB;
using PasswordHoarder.Utils;

namespace PasswordHoarder.DAL
{
    class UserService
    {
        private readonly IRepository<User> _userRepository;
        public UserContext UserContext { get; set; }

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public bool Login(string username, SecureString password)
        {
            var users = _userRepository.Get(x => x.Username == username);
            var user = users.Any() ? users.First() : null;
            return user != null && user.Password.Equals(Cryptography.EncodePassword(password));
        }

        public void Register(string loginUsername, SecureString loginSecurePassword)
        {
            _userRepository.Insert(new User(loginUsername, loginUsername, Cryptography.EncodePassword(loginSecurePassword)));
            _userRepository.Save();
        }
    }
}
