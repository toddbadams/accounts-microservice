using System.Data.Entity;
using System.Data.Entity.SqlServer;
using tba.Accounts.Entities;

namespace tba.Accounts.Context
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
}