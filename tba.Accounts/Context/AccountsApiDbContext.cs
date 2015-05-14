using System.Data.Entity;
using tba.Accounts.Entities;
using tba.accounts.Services;
using tba.Accounts.Services;
using tba.Core.Persistence.Interfaces;
using tba.Core.Utilities;
using tba.EFPersistence;

namespace tba.Accounts.Context
{
    public class AccountsDbInitializer : CreateDatabaseIfNotExists<AccountsDbContext>
    {
        protected override void Seed(AccountsDbContext context)
        {
            ITimeProvider timeProvider = TimeProvider.Current;
            IRepository<Account> repository = new EfRepository<Account>(context);
            IAccountsService accountsService = new AccountsService(repository, timeProvider);
            var accountSeedService = new AccountSeedService(accountsService);
            var seedTask = accountSeedService.SeedAccountsAsync(1, 1);
            seedTask.Wait();
        }
    }
}