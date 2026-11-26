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
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.AccountNumber);
            builder.Property(a => a.AccountNumber).HasMaxLength(30);
            builder.Property(a => a.CurrentBalance).HasColumnType("decimal(18,2)");
            builder.Property(a => a.AccountType).HasConversion<string>().HasMaxLength(20);

            builder.HasOne(a => a.Branch)
                   .WithMany(b => b.Accounts)
                   .HasForeignKey(a => a.BranchCode)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new Account
                {
                    AccountNumber = 1,
                    CurrentBalance = 15400.00m,
                    AccountType = AccountType.Current,
                    OpeningDate = new DateTime(2023, 2, 10),
                    BranchCode = 1
                },
                new Account
                {
                    AccountNumber = 2,
                    CurrentBalance = 120000.00m,
                    AccountType = AccountType.Business,
                    OpeningDate = new DateTime(2022, 8, 20),
                    BranchCode = 2
                }
            );
        }
    }
}
