using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Domain;
using SimpleWebApi.Infrastructure.Database.Extensions;

namespace SimpleWebApi.Infrastructure.Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Country>  Countries { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            modelBuilder.DataSeeding();
        }
        
    }
}