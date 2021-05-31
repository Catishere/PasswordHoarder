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
        private readonly UserContext _userContext;

        public UserService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public bool Login(string username, SecureString password)
        {
            var users = _userContext.Users.Where(x => x.Username == username).ToList();
            var user = users.Any() ? users.First() : null;
            return user != null && user.Password.Equals(Cryptography.EncodePassword(password));
        }

        public void Register(string loginUsername, SecureString loginSecurePassword)
        {
            _userContext.Users.Add(new User(loginUsername, loginUsername, Cryptography.EncodePassword(loginSecurePassword)));
            _userContext.SaveChanges();
        }
    }
}
