using System.Threading.Tasks;
using System.Web.Http;
using tba.Accounts.Context;
using tba.accounts.Models;
using tba.accounts.Services;
using tba.Accounts.Entities;
using tba.Core.Persistence.Interfaces;
using tba.Core.Utilities;
using tba.EFPersistence;

namespace tba.Accounts.Controllers
{
    /// <summary>
    /// Account Types API
    /// Utilizes async tasks to relase the thread upon request, then gets another
    /// thread when the result comes back from data store.
    /// </summary>
    public class AccountTypesController : ApiController
    {
        private readonly IAccountsService _accountsService;
        // todo (tba:28/2/15): get user and client id from user token in header
        private const long UserId = 1;
        private const long TenantId = 1;

        public AccountTypesController()
        {
            // Poor man's IOC
            var dataSource = new AccountsDbContext("DefaultConnection");
            IRepository<Account> repository = new EfRepository<Account>(dataSource, TimeProvider.Current);
            _accountsService = new AccountsService(repository, DefaultTimeProvider.Instance);
        }

        public AccountTypesController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        /// <summary>
        /// Fetch the list of account types
        /// </summary>
        /// <returns>set of accounts</returns>
        [AllowAnonymous]
        [Route("accounttypes", Name = "getAccounttypes", Order = 6)]
        public IHttpActionResult GetTypes()
        {
            var vm = _accountsService.GetTypes();
            return Ok(vm);
        }
    }
}