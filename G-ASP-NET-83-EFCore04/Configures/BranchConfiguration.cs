using G_ASP_NET_83_EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04.Configures
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.Code);
            builder.Property(b => b.Code).HasMaxLength(20);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(100);
            builder.Property(b => b.Address).IsRequired().HasMaxLength(250);
            builder.Property(b => b.PhoneNumber).IsRequired().HasMaxLength(20);

            // Seed Data
            builder.HasData(
                new Branch
                {
                    Code = 1,
                    Name = "Cairo Main Branch",
                    Address = "15 Tahrir Square, Cairo",
                    PhoneNumber = "0227940001"
                },
                new Branch
                {
                    Code = 2,
                    Name = "Alexandria Branch",
                    Address = "42 Fouad Street, Alexandria",
                    PhoneNumber = "034870002"
                }
            );
        }
    }
}
