using System.Data.Entity;
using System.Security.Claims;
using tba.Accounts.Entities;
using tba.Accounts.Models;

namespace tba.Accounts.DbContext
{
    public class AccountsDbInitializer
        : DropCreateDatabaseAlways<AccountsDbContext>
    {
        protected async override void Seed(AccountsDbContext context)
        {
            context.Accounts.Add(new Account
            {
                Name = "John C. Bla Trust Fund Current",
                Type = Account.AccountTaxonomy.Current
            });
            context.Accounts.Add(new Account
            {
                Name = "John C. Bla Trust Fund ISA",
                Type = Account.AccountTaxonomy.Isa
            });
            context.Accounts.Add(new Account
            {
                Name = "Mary Bla Trust Fund ISA",
                Type = Account.AccountTaxonomy.Isa
            });
            context.SaveChanges();

            // Set up two initial users with different role claims:
            var john = new MyUser { Email = "john@example.com" };
            var jimi = new MyUser { Email = "jimi@Example.com" };

            john.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Name, UserId = john.Id, ClaimValue = john.Email });
            john.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Role, UserId = john.Id, ClaimValue = "Admin" });

            jimi.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Name, UserId = jimi.Id, ClaimValue = jimi.Email });
            jimi.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Role, UserId = john.Id, ClaimValue = "User" });

            var store = new MyUserStore(context);
            await store.AddUserAsync(john, "JohnsPassword");
            await store.AddUserAsync(jimi, "JimisPassword");
        }


    }
}