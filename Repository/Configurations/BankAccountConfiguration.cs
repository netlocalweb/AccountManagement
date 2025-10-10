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

            builder.Property(b => b.Code).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Balance).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(b => b.DateCreated).IsRequired();


        }
    }
}
