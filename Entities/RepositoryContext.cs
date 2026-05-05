using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Phone)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Username)
                .IsUnique();
        }

        public DbSet<TestEntity> Test { get; set; }

        public DbSet<Client> Clients { get; set; }
    }
}