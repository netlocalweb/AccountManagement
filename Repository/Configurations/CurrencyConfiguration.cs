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
    internal class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(10);
            builder.HasIndex(c => c.Code)
                .IsUnique();
            builder.Property(c => c.Description)
                .IsRequired();
            builder.Property(c => c.DateCreated)
                .IsRequired();
            builder.Property(c => c.DateModified)
                .IsRequired(false);
        }
    }
}
