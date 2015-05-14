using System;
using System.Data.Entity;
using System.Threading.Tasks;
using tba.Core.Persistence.Extensions;
using tba.Core.Persistence.Interfaces;
using tba.Core.Services;
using tba.Core.Utilities;
using tba.Users.DbContext;
using tba.Users.Entities;
using tba.Users.Models;

namespace tba.Users.Services
{
    public class UsersService : EntityService<TbaUser>, IUsersService
    {
        readonly IUsersDbContext _db;
        private readonly IHashProvider _hashProvider;

        public UsersService(IRepository<TbaUser> repository,
            ITimeProvider timeProvider,
            IUsersDbContext context,
            IHashProvider hashProvider)
            : base(repository, typeof(UsersService).FullName, "users", timeProvider)
        {
            _db = context;
            _hashProvider = hashProvider;
        }


        public async Task InsertAsync(UserCm user, string passwordHash)
        {
            if (await UserExists(user.Email))
            {
                throw new Exception("A user with that Email address already exists");
            }

            var msg = string.Format("AddUserAsync. user={0}", Serialization.Serialize(user));
            try
            {
                Log.Debug(msg);
                // create an entity
                var e = user.ToEntity(_hashProvider.GetHash);
                // todo: need a means to capture the userId of the entering user
                await Repository.InsertAsync(0, e);
            }
            catch (Exception exception)
            {
                Log.Error(msg, exception);
                throw new ApplicationException("Failed to insert " + user.Email + " into " + FriendlyName);
            }
        }


        public async Task<TbaUser> GetByEmailAsync(string email)
        {
            var msg = string.Format("GetByEmailAsync. email={0}", email);
            try
            {
                Log.Debug(msg);
                var es = await Repository.Query()
                    .IsNotDeleted()
                    .Email(email).FirstOrDefaultAsync();

                Log.Debug(msg + " => " + Serialization.Serialize(es));
                return es;
            }
            catch (Exception exception)
            {
                Log.Error(msg, exception);
                throw;
            }
        }


        public async Task<TbaUser> FindByIdAsync(long userId)
        {
            return await _db.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
        }


        public async Task<bool> UserExists(TbaUser user)
        {
            return await _db.Users
                .AnyAsync(u => u.Id == user.Id || u.Email == user.Email);
        }

        public async Task<bool> UserExists(string email)
        {
            return await _db.Users
                .AnyAsync(u => u.Email == email);
        }


        public async Task AddClaimAsync(long userId, TbaUserClaim claim)
        {
            var user = await FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            user.Claims.Add(claim);
            await _db.SaveChangesAsync();
        }


        public bool PasswordIsValid(TbaUser user, string password)
        {
            var hash = _hashProvider.GetHash(password);
            return hash.Equals(user.PasswordHash);
        }


        //// Set up two initial users with different role claims:
        //var john = new MyUser { Email = "john@example.com" };
        //var jimi = new MyUser { Email = "jimi@Example.com" };

        //john.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Name, UserId = john.Id, ClaimValue = john.Email });
        //john.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Role, UserId = john.Id, ClaimValue = "Admin" });

        //jimi.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Name, UserId = jimi.Id, ClaimValue = jimi.Email });
        //jimi.Claims.Add(new MyUserClaim { ClaimType = ClaimTypes.Role, UserId = john.Id, ClaimValue = "User" });

        //var store = new MyUserStore(context);
        //await store.AddUserAsync(john, "JohnsPassword");
        //await store.AddUserAsync(jimi, "JimisPassword");
    }
}