using System.Data.Entity;
using tba.Accounts.Entities;

namespace tba.SelfHost
{
    /// <summary>
    /// </summary>
    public class AccountsApiDbContext : DbContext
    {
        public AccountsApiDbContext(string connectionStringName)
            : base(connectionStringName)
        {
            // the terrible hack
            // http://stackoverflow.com/questions/21641435/error-no-entity-framework-provider-found-for-the-ado-net-provider-with-invarian
            var ensureDLLIsCopied =
                    System.Data.Entity.SqlServer.SqlProviderServices.Instance;  

            
        }

        public DbSet<Account> Locales { get; set; }
    }
}