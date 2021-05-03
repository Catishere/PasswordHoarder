using System.Security;

namespace PasswordHoarder.Utils
{
    public static class UserMetaInfo
    {
        public static string Username { get; set; }
        public static SecureString Password { get; set; }

    }
}
