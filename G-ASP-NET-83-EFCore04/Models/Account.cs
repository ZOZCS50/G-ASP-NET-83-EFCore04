using G_ASP_NET_83_EFCore04.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_ASP_NET_83_EFCore04.Models
{
    public class Account
    {
        public int AccountNumber { get; set; } 
        public decimal CurrentBalance { get; set; }
        public AccountType AccountType { get; set; }
        public DateTime OpeningDate { get; set; } 

        public int? BranchCode { get; set; } = null!;
        public Branch? Branch { get; set; } = null!;

        public ICollection<Transaction>? Transactions { get; set; } = new HashSet<Transaction>();

        public ICollection<CustomerAccount>? CustomerAccounts { get; set; } = new HashSet<CustomerAccount>();
    }
}
