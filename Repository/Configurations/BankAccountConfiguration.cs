using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configurations
{
    internal class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Balance)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(b => b.DateCreated)
                .IsRequired();

            builder.Property(b => b.DateCreated)
                .IsRequired();

            builder.HasIndex(b => new { b.ClientId, b.Code })
                .IsUnique();

            //relationship with client
            builder.HasOne( b => b.Client)
                .WithMany( c => c.BankAccounts)
                .HasForeignKey(b => b.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            //realtioship with Currency
            builder.HasOne(b => b.Currency)
                .WithMany()
                .HasForeignKey(b => b.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
