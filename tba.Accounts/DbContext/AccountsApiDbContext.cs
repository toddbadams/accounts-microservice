using System.Data.Entity;
using System.Data.Entity.SqlServer;
using tba.Accounts.Entities;
using tba.accounts.Services;
using tba.Accounts.Services;
using tba.Core.Persistence.Interfaces;
using tba.Core.Utilities;
using tba.EFPersistence;

namespace tba.Accounts.DbContext
{
    /// <summary>
    /// </summary>
    public class AccountsDbContext : System.Data.Entity.DbContext
    {
        public AccountsDbContext(string connectionStringName)
            : base(connectionStringName)
        {
            // the terrible hack
            // http://stackoverflow.com/questions/21641435/error-no-entity-framework-provider-found-for-the-ado-net-provider-with-invarian
            var ensureDLLIsCopied =
                    SqlProviderServices.Instance;
            Database.SetInitializer(new AccountsDbInitializer());
        }

        public AccountsDbContext()
            : this("DefaultConnection")
        {
        }

        public IDbSet<Account> Accounts { get; set; }
    }

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