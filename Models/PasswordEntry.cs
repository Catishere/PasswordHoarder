using System;

namespace PasswordHoarder.Models
{
    public class PasswordEntry : IPasswordEntry
    {
#nullable enable
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; }
        public string? Url { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public string? Notes { get; set; }

        public PasswordEntry(int id, string title, string? username, string password, string? url, DateTime? expireDate, string? notes)
        {
            Id = id;
            Title = title;
            Username = username;
            Password = password;
            Url = url;
            ExpireDate = expireDate;
            Notes = notes;
        }
#nullable disable
        public int CompareTo(object other)
        {
            return string.Compare(Title, ((IPasswordEntry)other).Title, StringComparison.OrdinalIgnoreCase);
        }
    }
}
