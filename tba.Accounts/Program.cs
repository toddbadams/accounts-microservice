using System;
using Microsoft.Owin.Hosting;
using tba.Accounts.Configuration;

namespace tba.Accounts
{
    class Program
    {
        //private const long TenantId = 1;
        //private const long UserId = 1;
        //private const string ConnectionStringName = "DefaultConnection";
        // private static readonly AccountsDbContext DataSource = new AccountsDbContext(ConnectionStringName);

        static void Main(string[] args)
        {
            Console.WriteLine("Starting web Server...");
            var cp = new AccountsConfigurationProvider();
            var settings = cp.Read();
            WebApp.Start<Startup>(url: settings.ServiceUrl);
            Console.WriteLine("Server running at {0} - press Enter to quit. ", settings.ServiceUrl);
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
