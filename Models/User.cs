using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PasswordHoarder
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public String Username { get; set; }
#nullable enable
        public String? DisplayName { get; set; }
        public String Password { get; set; }

        public User(string username, string? displayName, String password)
        {
            Username = username;
            DisplayName = displayName;
            Password = password;
            Id = 1;
        }
#nullable disable

        public User() {}
    }
    
}
