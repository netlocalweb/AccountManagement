using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }//table of client
        public DbSet<Currency> Currencies { get; set; }//table of currency
        public DbSet<Category> Categories { get; set; }//table of category
        public DbSet<Product> Products { get; set; }//table of products
        public DbSet<BankAccount> BankAccounts { get; set; }//table of bank accounts
        public DbSet<BankTransaction> BankTransactions { get; set; }//table of bank transactions




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Client entity constraints
            //Unique Email, Phone ,Username
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Phone)
                .IsUnique();

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Username)
                .IsUnique();


            //Currency entity constraints
            //Unique code and uppercase 
            modelBuilder.Entity<Currency>()
                .HasIndex(c => c.Code)
                .IsUnique();

            modelBuilder.Entity<Currency>()
                .Property(c => c.Code)
                .HasConversion(code => code.ToUpper(), code => code);


            //Category entity constraints
            // Unik and uppercase
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Code)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .Property(c => c.Code)
                .HasConversion(code => code.ToUpper(), code => code);


            // Unique Code and ClientId
            modelBuilder.Entity<BankAccount>()
                .HasIndex(b => new { b.ClientId, b.Code })
                .IsUnique();
        }

    }

}
