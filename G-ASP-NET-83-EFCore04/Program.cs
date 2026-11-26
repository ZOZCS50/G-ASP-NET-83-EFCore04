using G_ASP_NET_83_EFCore04;
using G_ASP_NET_83_EFCore04.Enums;
using G_ASP_NET_83_EFCore04.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace G_ASP_NET_83_EFCore04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var context = new AppDbContext();
            bool running = true;
            while (running)
            {
                Console.WriteLine("National Bank Management");
                Console.WriteLine("======================================");
                Console.WriteLine("1) Add a new Customer");
                Console.WriteLine("2) Open a new Account for a Customer");
                Console.WriteLine("3) Update Account Status (Active / Closed)");
                Console.WriteLine("4) Remove an Account from a Customer");
                Console.WriteLine("5) List all Customers (with accounts)");
                Console.WriteLine("0) Exit");
                Console.WriteLine();

                Console.Write("Enter Choice : ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("\nInvalid input. Please enter a number.\n");
                    continue;
                }

                else
                {
                    switch (choice)
                    {
                        case 1:
                            AddCustomer(context);
                            Pause();
                            break;

                        case 2:
                            OpenNewAccount(context);
                            Pause();
                            break;

                        case 3:
                            UpdateAccountStatus(context);
                            Pause();
                            break;

                        case 4:
                            RemoveAccount(context);
                            Pause();
                            break;

                        case 5:
                            ListCustomers(context);
                            Pause();
                            break;

                        case 0:
                            running = false;
                            Console.WriteLine("Closing connection and exiting...");
                            break;

                        default:
                            Console.WriteLine("\nInvalid number, please try again.\n");
                            break;

                    }
                }

            }
        }

        public static void AddCustomer(AppDbContext context)
        {
            Console.WriteLine("\nAdd Customer");
            Console.WriteLine("-------------------------");

            Console.Write("Full Name          : ");
            string fullname = Console.ReadLine();

            string nationalId;
            while (true)
            {
                Console.Write("National ID        : ");
                nationalId = Console.ReadLine();
                if (context.Customers.Any(C => C.NationalId == nationalId))
                {
                    Console.WriteLine("Error: A customer with this National ID already exists. Try again.");
                    continue;
                }
                break;
            }

            DateTime DOB;
            while (true)
            {
                Console.Write("\"Date of Birth (yyyy-MM-dd): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DOB))
                {
                    Console.WriteLine("Error: Invalid date. Try again.");
                    continue;
                }
                break;
            }

            Console.Write("Email              :");
            string email = Console.ReadLine();

            Console.Write("Phone              : ");
            string phonenumber = Console.ReadLine();

            Console.WriteLine("Address            : ");
            string address = Console.ReadLine();

            int typeChoice;
            while (true)
            {
                Console.WriteLine("Customer Type:");
                Console.WriteLine("  1) Individual");
                Console.WriteLine("  2) Business");
                Console.Write("Choice");
                if (!int.TryParse(Console.ReadLine(), out typeChoice) || (typeChoice != 1 && typeChoice != 2))
                {
                    Console.WriteLine("Invalid number,please try again");
                    continue;
                }
                break;
            }
            CustomerType customertype = (CustomerType)typeChoice;

            var customer = new Customer
            {
                FullName = fullname,
                Address = address,
                DateOfBirth = DOB,
                Email = email,
                PhoneNumber = phonenumber,
                NationalId = nationalId,
                CustomerType = customertype
            };

            context.Customers.Add(customer);
            context.SaveChanges();

            Console.WriteLine($"\nCustomer created successfully. CustomerId = {customer.Id}");
        }

        public static void OpenNewAccount(AppDbContext context)
        {
            Console.WriteLine("Open New Account");
            Console.WriteLine("----------------");

            Console.Write("Account Number: ");
            int accountnumber;
            while (true)
            {
                Console.Write("Account Number: ");
                if (!int.TryParse(Console.ReadLine(), out accountnumber))
                {
                    Console.WriteLine("Error: Invalid account number. Please enter digits only.");
                    continue;
                }
                break;
            }

            int AccTypeChoice;
            while (true)
            {
                Console.WriteLine("Account Type:");
                Console.WriteLine("  1) Savings");
                Console.WriteLine("  2) Current");
                Console.WriteLine("  3) Business");
                Console.Write("Choice: ");

                if (!int.TryParse(Console.ReadLine(), out AccTypeChoice) || (AccTypeChoice != 1 && AccTypeChoice != 2 && AccTypeChoice != 3))
                {
                    Console.WriteLine("Invalid number, please try again");
                    continue;
                }
                break;
            }
            AccountType accType = (AccountType)AccTypeChoice;

            int branchCode;
            while (true)
            {
                Console.Write("Branch Code   : ");
                if (int.TryParse(Console.ReadLine(), out branchCode) && context.Branches.Any(B => B.Code == branchCode))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Error: Branch '{branchCode}' does not exist.");
                }
            }

            int customerId;
            while (true)
            {
                Console.Write("Customer Id   : ");
                if (int.TryParse(Console.ReadLine(), out customerId) && context.Customers.Any(C => C.Id == customerId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Error: Customer #{customerId} does not exist.");
                }
            }

            int ownershipchoice;
            while (true)
            {
                Console.WriteLine("Ownership Role:");
                Console.WriteLine("  1) Primary");
                Console.WriteLine("  2) CoHolder");
                Console.Write("Choice: ");
                if (!int.TryParse(Console.ReadLine(), out ownershipchoice) || (ownershipchoice != 1 && ownershipchoice != 2))
                {
                    Console.WriteLine("Invalid number, please try again");
                    continue;
                }
                break;
            }
            OwnershipType ownership = (OwnershipType)ownershipchoice;

            Console.WriteLine($"\nValidating branch '{branchCode}' and customer {customerId}...");

            Account account = new Account
            {
                AccountNumber = accountnumber,
                AccountType = accType,
                BranchCode = branchCode
            };

            CustomerAccount customeraccount = new CustomerAccount
            {
                CustomerId = customerId,
                AccountNumber = accountnumber,
                OwnershipType = ownership
            };

            context.Accounts.Add(account);
            context.SaveChanges();

            context.CustomerAccounts.Add(customeraccount);
            context.SaveChanges();

            Console.WriteLine($"Account '{accountnumber}' created and linked to customer {customerId} as {ownership} owner.");
        }

        public static void UpdateAccountStatus(AppDbContext context)
        {
            Console.WriteLine("Update Account Status");
            Console.WriteLine("---------------------");

            int accountnumber;
            int customerId;
            while (true)
            {
                Console.Write("Account Number: ");
                int.TryParse(Console.ReadLine(), out accountnumber);

                Console.Write("Customer Id   : ");
                int.TryParse(Console.ReadLine(), out customerId);

                if (context.CustomerAccounts.Any(CA => CA.AccountNumber == accountnumber && CA.CustomerId == customerId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Error: No record found for Account '{accountnumber}' and Customer #{customerId}.");
                    continue;
                }
            }

            int statustypechoice;
            var customerAccount = context.CustomerAccounts
              .FirstOrDefault(CA => CA.AccountNumber == accountnumber && CA.CustomerId == customerId);
            Console.WriteLine($"Current Status: {customerAccount.AccountStatus}");
            while (true)
            {
                Console.WriteLine("New Status:");
                Console.WriteLine("  1) Active");
                Console.WriteLine("  2) Closed");
                Console.Write("Choice: ");

                if (!int.TryParse(Console.ReadLine(), out statustypechoice) || (statustypechoice != 1 && statustypechoice != 2))
                {
                    Console.WriteLine("Error: Invalid number, please try again");
                    continue;
                }
                break;
            }
            AccountStatus newStatus = (AccountStatus)statustypechoice;


            customerAccount.AccountStatus = newStatus;
            context.SaveChanges();
            Console.WriteLine($"\nStatus updated to {newStatus}.");
        }

        public static void RemoveAccount(AppDbContext context)
        {
            Console.WriteLine("Remove Account From Customer");
            Console.WriteLine("----------------------------");

            int accountnumber;
            int customerId;
            while (true)
            {
                Console.Write("Account Number: ");
                int.TryParse(Console.ReadLine(), out accountnumber);

                Console.Write("Customer Id   : ");
                int.TryParse(Console.ReadLine(), out customerId);

                if (context.CustomerAccounts.Any(CA => CA.AccountNumber == accountnumber && CA.CustomerId == customerId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine($"Error: No ownership record found for Account '{accountnumber}' and Customer #{customerId}.");
                    continue;
                }

            }
            var customerAccount = context.CustomerAccounts
              .FirstOrDefault(ca => ca.AccountNumber == accountnumber && ca.CustomerId == customerId);

            context.CustomerAccounts.Remove(customerAccount);
            context.SaveChanges();
            Console.WriteLine("\nOwnership link deleted.");

            bool hasRemainingOwners = context.CustomerAccounts
              .Any(ca => ca.AccountNumber == accountnumber);

            if (!hasRemainingOwners)
            {
                var account = context.Accounts.FirstOrDefault(a => a.AccountNumber == accountnumber);
                if (account != null)
                {
                    context.Accounts.Remove(account);
                    context.SaveChanges();
                    Console.WriteLine($"That was the last owner — account '{accountnumber}' was also removed.");
                }
            }
        }

        public static void ListCustomers(AppDbContext context)
        {
            Console.WriteLine("All Customers");
            Console.WriteLine("-------------");

            var customers = context.Customers
              .Include(c => c.CustomerAccounts)
              .ThenInclude(ca => ca.Account)
              .ThenInclude(a => a.Branch)
              .OrderBy(c => c.Id)
              .ToList();

            if (!customers.Any())
            {
                Console.WriteLine("No customers found.");
                return;
            }

            foreach (var customer in customers)
            {
                Console.WriteLine($"#{customer.Id} {customer.FullName} ({customer.CustomerType})");

                if (!customer.CustomerAccounts.Any())
                {
                    Console.WriteLine("   (no accounts)");
                }
                else
                {
                    foreach (var ca in customer.CustomerAccounts)
                    {
                        var acc = ca.Account;
                        Console.WriteLine($"     {acc.AccountNumber}   {acc.AccountType}   Balance:   {acc.CurrentBalance:N2}   {ca.OwnershipType}   {ca.AccountStatus}   @ {acc.Branch.Name}");
                    }
                }
            }


        }

        public static void Pause()
        {
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey(true);
        }
    }
}