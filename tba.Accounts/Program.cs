using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using tba.accounts.Models;
using tba.accounts.Services;
using tba.Accounts.DbContext;
using tba.Accounts.Entities;
using tba.Core.Utilities;
using tba.EFPersistence;

namespace tba.Accounts
{
    class Program
    {
        private const long TenantId = 1;
        private const long UserId = 1;
        private const string ConnectionStringName = "DefaultConnection";
        private const string BaseAddress = "http://localhost:9001/";
       // private static readonly AccountsDbContext DataSource = new AccountsDbContext(ConnectionStringName);

        static void Main(string[] args)
        {
            Console.WriteLine("Starting web Server...");
            WebApp.Start<Startup>(url: BaseAddress);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", BaseAddress);
            Console.ReadLine();
        }

        //private async static Task<IEnumerable<AccountRm>> SeedAccountsAsync()
        //{
        //    var service = new AccountsService(new EfRepository<Account>(DataSource), DefaultTimeProvider.Instance);

        //    var results = new List<AccountRm>{
        //        await service.InsertAsync(TenantId, UserId, new AccountCm
        //        {
        //            Name = "John C. Bla Trust Fund Current",
        //            Type = Account.AccountTaxonomy.Current
        //        }),
        //        await service.InsertAsync(TenantId, UserId, new AccountCm
        //        {
        //            Name = "John C. Bla Trust Fund ISA",
        //            Type = Account.AccountTaxonomy.Isa
        //        }),
        //        await service.InsertAsync(TenantId, UserId, new AccountCm
        //        {
        //            Name = "Mary Bla Trust Fund ISA",
        //            Type = Account.AccountTaxonomy.Isa
        //        })
        //    };

        //    return results;
        //}
    }
}
