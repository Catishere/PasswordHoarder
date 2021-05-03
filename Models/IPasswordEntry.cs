using System;

namespace PasswordHoarder.Models
{
    public interface IPasswordEntry : IComparable
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
#nullable disable
    }
}
