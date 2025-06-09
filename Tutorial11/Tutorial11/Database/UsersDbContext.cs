using Microsoft.EntityFrameworkCore;
using Tutorial11.Models;

namespace Tutorial11.Database
{
    public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsersDbContext).Assembly);
        }
    }
}
