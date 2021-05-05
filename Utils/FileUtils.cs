using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using PasswordHoarder.Models;
using PasswordHoarder.Models.UI;

namespace PasswordHoarder.Utils
{
    public static class FileUtils
    {
        private const string Filename = "keystore.dat";
        public static void StorePasswords(IEnumerable<IPasswordEntry> passwords)
        {
            PasswordFileModel pfm = new PasswordFileModel();
            var password = PasswordUtils.SecureStringToString(UserMetaInfo.Password);
            pfm.Entries.AddRange(passwords.OfType<PasswordEntryModel>());
            File.WriteAllText(Filename, Cryptography.Encrypt(JsonSerializer.Serialize(pfm), password));
        }

        public static IEnumerable<IPasswordEntry> GetPasswords()
        {
            if (!File.Exists(Filename)) yield break;
            var password = PasswordUtils.SecureStringToString(UserMetaInfo.Password);
            var fileData = File.ReadAllText(Filename);
            var text = Cryptography.Decrypt(fileData, password);
            var pfm = JsonSerializer.Deserialize<PasswordFileModel>(text);

            if (pfm == null) yield break;
            foreach (var entry in pfm.Entries)
                yield return entry;
        }
    }
}
