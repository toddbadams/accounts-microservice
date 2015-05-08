using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using tba.accounts.Models;
using tba.accounts.Services;
using tba.Accounts.Entities;
using tba.Accounts.Models;
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
                    System.Data.Entity.SqlServer.SqlProviderServices.Instance;  
          
        }

        public AccountsDbContext()
            : base ("DefaultConnection")
        {
            // the terrible hack
            // http://stackoverflow.com/questions/21641435/error-no-entity-framework-provider-found-for-the-ado-net-provider-with-invarian
            var ensureDLLIsCopied =
                    System.Data.Entity.SqlServer.SqlProviderServices.Instance;  
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