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
    public class CustomerAccountConfiguration : IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> builder)
        {
            builder.HasKey(ca => new { ca.CustomerId, ca.AccountNumber });

            builder.Property(ca => ca.OwnershipType).HasConversion<string>().HasMaxLength(20);
            builder.Property(ca => ca.AccountStatus).HasConversion<string>().HasMaxLength(20);

            builder.HasOne(ca => ca.Customer)
                   .WithMany(c => c.CustomerAccounts)
                   .HasForeignKey(ca => ca.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ca => ca.Account)
                   .WithMany(a => a.CustomerAccounts)
                   .HasForeignKey(ca => ca.AccountNumber)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new CustomerAccount
                {
                    CustomerId = 1,
                    AccountNumber = 1,
                    OwnershipType = OwnershipType.Primary,
                    OwnershipStartDate = new DateTime(2023, 2, 10),
                    AccountStatus = AccountStatus.Active
                },
                new CustomerAccount
                {
                    CustomerId = 2,
                    AccountNumber = 2,
                    OwnershipType = OwnershipType.Primary,
                    OwnershipStartDate = new DateTime(2022, 8, 20),
                    AccountStatus = AccountStatus.Active
                }
            );
        }
    }
}
