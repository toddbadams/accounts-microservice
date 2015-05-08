using System;
using System.Data.Entity;
using System.Threading.Tasks;
using tba.Accounts.DbContext;

namespace tba.Accounts.Models
{
    public class MyUserStore
    {
        readonly AccountsDbContext _db;
        public MyUserStore(AccountsDbContext context)
        {
            _db = context;
        }


        public async Task AddUserAsync(MyUser user, string password)
        {
            if (await UserExists(user))
            {
                throw new Exception(
                    "A user with that Email address already exists");
            }
            var hasher = new MyPasswordHasher();
            user.PasswordHash = hasher.CreateHash(password).ToString();
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
        }


        public async Task<MyUser> FindByEmailAsync(string email)
        {
            var user = _db.Users
                .Include(c => c.Claims)
                .FirstOrDefaultAsync(u => u.Email == email);

            return await _db.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task<MyUser> FindByIdAsync(string userId)
        {
            return await _db.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
        }


        public async Task<bool> UserExists(MyUser user)
        {
            return await _db.Users
                .AnyAsync(u => u.Id == user.Id || u.Email == user.Email);
        }


        public async Task AddClaimAsync(string UserId, MyUserClaim claim)
        {
            var user = await FindByIdAsync(UserId);
            if (user == null)
            {
                throw new Exception("User does not exist");
            }
            user.Claims.Add(claim);
            await _db.SaveChangesAsync();
        }


        public bool PasswordIsValid(MyUser user, string password)
        {
            var hasher = new MyPasswordHasher();
            var hash = hasher.CreateHash(password);
            return hash.Equals(user.PasswordHash);
        }
    }
}