using System.Collections.Generic;
using tba.Accounts.Entities;
using tba.accounts.Models;
using tba.accounts.Services;
using tba.Core.Utilities;
using tba.EFPersistence;

namespace tba.console
{
    class Program
    {
        private const long TenantId = 1;
        private const long UserId = 1;
        private const string ConnectionStringName = "DefaultConnection";
        private static readonly SeedDbContext DataSource = new SeedDbContext(ConnectionStringName);

        static void Main(string[] args)
        {
            var accounts = SeedAccounts();
        }

        private static IEnumerable<AccountRm> SeedAccounts()
        {
            var service = new AccountsService(new EfRepository<Account>(DataSource), DefaultTimeProvider.Instance);

            return new List<AccountRm>
            {
                service.Insert(TenantId, UserId, new AccountCm
                {
                    Name = "John C. Bla Trust Fund Current",
                    Type = Account.AccountTaxonomy.Current
                }),
                service.Insert(TenantId, UserId, new AccountCm
                {
                    Name = "John C. Bla Trust Fund ISA",
                    Type = Account.AccountTaxonomy.Isa
                }),
                service.Insert(TenantId, UserId, new AccountCm
                {
                    Name = "Mary Bla Trust Fund ISA",
                    Type = Account.AccountTaxonomy.Isa
                })
            };
        } 
    }
}
