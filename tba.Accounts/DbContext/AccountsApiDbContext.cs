using System.Data.Entity;
using tba.Accounts.Entities;

namespace tba.Accounts.DbContext
{
    /// <summary>
    /// </summary>
    public class AccountsApiDbContext : System.Data.Entity.DbContext
    {
        public AccountsApiDbContext(string connectionStringName)
            : base(connectionStringName)
        {
            // the terrible hack
            // http://stackoverflow.com/questions/21641435/error-no-entity-framework-provider-found-for-the-ado-net-provider-with-invarian
            var ensureDLLIsCopied =
                    System.Data.Entity.SqlServer.SqlProviderServices.Instance;  

            
        }

        public DbSet<Account> Accounts { get; set; }
    }
}