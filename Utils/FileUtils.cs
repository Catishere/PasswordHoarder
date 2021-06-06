using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using PasswordHoarder.Models;
using PasswordHoarder.Models.UI;

namespace PasswordHoarder.Utils
{
    public static class FileUtils
    {
        public static string Filename { get; set; } = "keystore.dat";
        public static void StorePasswords(IEnumerable<IPasswordEntry> passwords)
        {
            PasswordFileModel pfm = new PasswordFileModel();
            var password = PasswordUtils.SecureStringToString(UserMetaInfo.Password);
            pfm.Entries.AddRange(passwords.OfType<PasswordEntryModel>());
            File.WriteAllText(Filename, Cryptography.Encrypt(JsonSerializer.Serialize(pfm), password));
        }

        public static IEnumerable<IPasswordEntry> GetPasswords()
        {
            var pfm = GetPasswordFileModel();
            if (pfm == null) yield break;
            foreach (var entry in pfm.Entries)
                yield return entry;
        }

        public static PasswordFileModel GetPasswordFileModel()
        {
            if (!File.Exists(Filename)) return null;
            var password = PasswordUtils.SecureStringToString(UserMetaInfo.Password);
            var fileData = File.ReadAllText(Filename);
            if (fileData.Equals(string.Empty)) return null;
            var text = Cryptography.Decrypt(fileData, password);
            return JsonSerializer.Deserialize<PasswordFileModel>(text);
        }
    }
}
