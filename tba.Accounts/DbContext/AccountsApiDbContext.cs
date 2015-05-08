using System.Data.Entity;
using System.Data.Entity.SqlServer;
using tba.Accounts.Entities;
using tba.Accounts.Models;

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
          
        }

        public AccountsDbContext()
            : base ("DefaultConnection")
        {
            // the terrible hack
            // http://stackoverflow.com/questions/21641435/error-no-entity-framework-provider-found-for-the-ado-net-provider-with-invarian
            var ensureDLLIsCopied =
                    SqlProviderServices.Instance;  
        }

        static AccountsDbContext()
        {
            Database.SetInitializer(new AccountsDbInitializer());
        }

        public DbSet<Account> Accounts { get; set; }
        public IDbSet<MyUser> Users { get; set; }
        public IDbSet<MyUserClaim> Claims { get; set; }
    }
}