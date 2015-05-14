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

       
    }
}
