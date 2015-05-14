using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Threading.Tasks;
using tba.Users.Entities;
using tba.Users.Models;

namespace tba.Users.DbContext
{
    public interface IUsersDbContext
    {
        IDbSet<TbaUser> Users { get; set; }
        IDbSet<TbaUserClaim> Claims { get; set; }
        Task<int> SaveChangesAsync();
    }

    /// <summary>
    /// </summary>
    public class UsersDbContext : System.Data.Entity.DbContext, IUsersDbContext
    {
        public UsersDbContext(string connectionStringName)
            : base(connectionStringName)
        {
            // the terrible hack
            // http://stackoverflow.com/questions/21641435/error-no-entity-framework-provider-found-for-the-ado-net-provider-with-invarian
            var ensureDLLIsCopied = SqlProviderServices.Instance;
        }

        public UsersDbContext()
            : this ("DefaultConnection")
        {
        }

        public IDbSet<TbaUser> Users { get; set; }
        public IDbSet<TbaUserClaim> Claims { get; set; }
    }
}