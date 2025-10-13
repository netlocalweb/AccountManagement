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
    public class BankTransactionConfiguration : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            builder.ToTable("BankTransaction");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Action)
                .IsRequired();

            builder.Property(t => t.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property( t => t.IsActive)
                .IsRequired();

            builder.Property(t => t.DateCreated)
                .IsRequired();

            //relationship with bankpoaccount
            builder.HasOne( t => t.BankAccount)
                .WithMany(b => b.BankTransactions)
                .HasForeignKey(t => t.BankAccountId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
