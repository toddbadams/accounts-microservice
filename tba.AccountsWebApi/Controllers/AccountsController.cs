using System.Web.Http;
using tba.Accounts.Entities;
using tba.accounts.Models;
using tba.accounts.Services;
using tba.Core.Persistence.Interfaces;
using tba.Core.Utilities;
using tba.EFPersistence;

namespace tba.AccountsWebApi.Controllers
{
    /// <summary>
    /// An application is a set of features
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
        public IHttpActionResult Get(long? parentId = null)
        {
            var vm = _accountsService.Fetch(TenantId, UserId);
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
        public IHttpActionResult Post(AccountCm payload)
        {
            var vm = _accountsService.Insert(TenantId, UserId, payload);
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
        public IHttpActionResult Put(long id, AccountUm payload)
        {
            var vm = _accountsService.Update(TenantId, UserId, id, payload);
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
        public IHttpActionResult Delete(long id)
        {
            _accountsService.Delete(TenantId, UserId, id);
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