using System.Data.Entity;
using PasswordHoarder;
using PasswordHoarder.Migrations;

namespace PasswordHoarder.DAL
{
    class UserContext : DbContext
    {
        public UserContext() : base("UserDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<UserContext, Configuration>());
        }

        public DbSet<User> Users { get; set; }
    }
}
