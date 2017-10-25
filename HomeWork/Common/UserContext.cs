using System.Data.Entity;
using HomeWork.Models;

namespace HomeWork.Common
{
    public class UserContext : DbContext
    {

        public UserContext() : base("DefaultConnection"){}

        public DbSet<UserInfo> Users { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<Cities> Cities { get; set; }

    }
}