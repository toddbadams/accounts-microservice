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
    /// Account API
    /// Utilizes async tasks to relase the thread upon request, then gets another
    /// thread when the result comes back from data store.
    /// </summary>
   // [RoutePrefix("accounts")]
    public class AccountsController : ApiController
    {
        private readonly IAccountsService _accountsService;
        // todo (tba:28/2/15): get user and client id from user token in header
        private const long UserId = 1;
        private const long TenantId = 1;

        public AccountsController()
        {
            // Poor man's IOC
            var dataSource = new AccountsDbContext("DefaultConnection");
            IRepository<Account> repository = new EfRepository<Account>(dataSource, TimeProvider.Current);
            _accountsService = new AccountsService(repository, DefaultTimeProvider.Instance);
        }

        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        /// <summary>
        /// Fetch a set of accounts for a parent
        /// </summary>
        /// <returns>set of accounts</returns>
        [AllowAnonymous]
        [Route("accounts", Name = "getAccounts", Order = 7)]
        public async Task<IHttpActionResult> GetAll()
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
        [Route("accounts", Name = "insertAccount", Order = 3)]
        public async Task<IHttpActionResult> Post(AccountCm payload)
        {
            var vm = await _accountsService.InsertAsync(TenantId, UserId, payload);
            return Ok(vm);
        }

        /// <summary>
        /// Updates an existing account
        /// </summary>
        /// <param name="id">the id of the account to update</param>
        /// <returns>A account read model</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("accounts/{id:long}/close", Name = "closeAccount", Order = 1)]
        public async Task<IHttpActionResult> Close(long id)
        {
            var vm = await _accountsService.CloseAccount(TenantId, UserId, id);
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
        [Route("accounts/{id:long}", Name = "updateAccount", Order = 2)]
        public async Task<IHttpActionResult> Update(long id, AccountUm payload)
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
        [Route("accounts/{id:long}", Name = "deleteAccount")]
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