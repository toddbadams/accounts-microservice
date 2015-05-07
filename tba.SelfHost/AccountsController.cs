using System.Threading.Tasks;
using System.Web.Http;
using tba.accounts.Models;
using tba.accounts.Services;
using tba.Accounts.Entities;
using tba.Core.Persistence.Interfaces;
using tba.Core.Utilities;
using tba.EFPersistence;

namespace tba.SelfHost
{
    /// <summary>
    /// Account API
    /// Utilizes async tasks to relase the thread upon request, then gets another
    /// thread when the result comes back from data store.
    /// </summary>
    [RoutePrefix("accounts")]
    public class AccountsController : ApiController
    {
        private readonly IAccountsService _accountsService;
        // todo (tba:28/2/15): get user and client id from user token in header
        private const long UserId = 1;
        private const long TenantId = 1;

        /// <summary>
        /// 
        /// </summary>
        public AccountsController()
        {
            // todo (tba:28/2/15):  move to IOC
            var dataSource = new AccountsApiDbContext("DefaultConnection");
            IRepository<Account> repository = new EfRepository<Account>(dataSource);
            _accountsService = new AccountsService(repository, DefaultTimeProvider.Instance);
        }

        /// <summary>
        /// Fetch a set of accounts for a parent
        /// </summary>
        /// <param name="parentId">the parent</param>
        /// <returns>set of accounts</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("", Name = "getAccounts")]
        public async Task<IHttpActionResult> Get(long? parentId = null)
        {
            var vm = await _accountsService.FetchAsync(TenantId, UserId);
            return Ok(vm);
        }

        /// <summary>
        /// Insert a new account
        /// </summary>
        /// <param name="payload">the account create model</param>
        /// <returns>A account read model</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("", Name = "insertAccount")]
        public async Task<IHttpActionResult> Post(AccountCm payload)
        {
            var vm = await _accountsService.InsertAsync(TenantId, UserId, payload);
            return Ok(vm);
        }

        /// <summary>
        /// Updates an existing account
        /// </summary>
        /// <param name="id">the id of the account to update</param>
        /// <param name="payload">the account update model</param>
        /// <returns>A account read model</returns>
        [AllowAnonymous]
        [HttpPut]
        [Route("{id:guid}", Name = "updateAccount")]
        public async Task<IHttpActionResult> Put(long id, AccountUm payload)
        {
            var vm = await _accountsService.UpdateAsync(TenantId, UserId, id, payload);
            return Ok(vm);
        }

        /// <summary>
        /// Delete an application
        /// </summary>
        /// <param name="id">the id of the account to delete</param>
        /// <returns>status</returns>
        [AllowAnonymous]
        [HttpDelete]
        [Route("{id:guid}", Name = "deleteAccount")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            await _accountsService.DeleteAsync(TenantId, UserId, id);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _accountsService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}