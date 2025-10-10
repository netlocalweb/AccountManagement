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
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(c => c.Code)
                .IsUnique();
            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength (100);
            builder.Property(c => c.Code)
                .HasConversion(
                v => v.ToUpper(),
                v => v);   
        }
    }
}
