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

            modelBuilder.Entity<Currency>()
                .HasIndex(c => c.Code)
                .IsUnique();

            modelBuilder.Entity<Currency>()
                .Property(c => c.ExchangeRate)
                .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<BankAccount>()
                .HasIndex(b => new { b.ClientId, b.Code })
                .IsUnique();

            modelBuilder.Entity<BankAccount>()
                .Property(b => b.Balance)
                .HasColumnType("decimal(18,2)");
        }

        public DbSet<TestEntity> Test { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }
    }
}