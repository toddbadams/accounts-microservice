using System.Collections.Generic;
using System.Threading.Tasks;
using tba.Accounts.Entities;
using tba.accounts.Models;
using tba.accounts.Services;

namespace tba.Accounts.Services
{
    internal class AccountSeedService
    {
        private readonly IAccountsService _accountsService;

        public AccountSeedService(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        public async Task<IEnumerable<AccountRm>> SeedAccountsAsync(long tenantId, long userId)
        {

            var results = new List<AccountRm>{
                await _accountsService.InsertAsync(tenantId, userId, new AccountCm
                {
                    Name = "John C. Bla Trust Fund Current",
                    Type = Account.AccountTaxonomy.Current
                }),
                await _accountsService.InsertAsync(tenantId, userId, new AccountCm
                {
                    Name = "John C. Bla Trust Fund ISA",
                    Type = Account.AccountTaxonomy.Isa
                }),
                await _accountsService.InsertAsync(tenantId, userId, new AccountCm
                {
                    Name = "Mary Bla Trust Fund ISA",
                    Type = Account.AccountTaxonomy.Isa
                })
            };

            return results;
        }
    }
}
