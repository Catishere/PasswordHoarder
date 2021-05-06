using System;
using System.Linq;
using System.Security;
using System.Text;

namespace PasswordHoarder.Utils
{
    public class PasswordUtils
    {
        public static string SecureStringToString(SecureString secureString)
        {
            return new System.Net.NetworkCredential(string.Empty, secureString).Password;
        }

        public static string GeneratePassword(Symbols.SymbolType[] types, int size)
        {
            if (size <= 0 || !types.Any()) return string.Empty;

            var password = new char[size];
            var random = new Random();

            var sbSymbols = new StringBuilder();
            foreach (var type in types)
                sbSymbols.Append(Symbols.SymbolArray[(int)type]);

            var symbols = sbSymbols.ToString();
            for (int i = 0; i < size; i++)
            {
                password[i] = symbols[random.Next(symbols.Length)];
            }

            return new string(password);
        }
    }
}
