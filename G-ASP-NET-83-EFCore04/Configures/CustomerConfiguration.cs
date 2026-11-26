using G_ASP_NET_83_EFCore04.Enums;
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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FullName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.NationalId).IsRequired().HasMaxLength(20);
            builder.HasIndex(c => c.NationalId).IsUnique();
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(c => c.Address).IsRequired().HasMaxLength(250);
            builder.Property(c => c.CustomerType).HasConversion<string>().HasMaxLength(20);

            builder.HasData(
                new Customer
                {
                    Id = 1,
                    FullName = "Ahmed Ali",
                    NationalId = "29501010101234",
                    DateOfBirth = new DateTime(1995, 1, 1),
                    Email = "ahmed.ali@gmail.com",
                    PhoneNumber = "01012345678",
                    Address = "10 Nasr City, Cairo",
                    CustomerType = CustomerType.Individual
                },
                new Customer
                {
                    Id = 2,
                    FullName = "Nile Trading LLC",
                    NationalId = "90011223344",
                    DateOfBirth = new DateTime(2015, 5, 10),
                    Email = "info@niletrading.com",
                    PhoneNumber = "035556677",
                    Address = "88 Smouha, Alexandria",
                    CustomerType = CustomerType.Business
                }
            );
        }
    }
}
