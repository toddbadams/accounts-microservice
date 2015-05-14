using System.Threading.Tasks;
using tba.Users.Entities;
using tba.Users.Models;

namespace tba.Users.Services
{
    public interface IUsersService
    {
        Task InsertAsync(UserCm user, string password);
        Task<TbaUser> FindByIdAsync(long userId);
        Task<bool> UserExists(TbaUser user);
        Task AddClaimAsync(long userId, TbaUserClaim claim);
        bool PasswordIsValid(TbaUser user, string password);
        Task<TbaUser> GetByEmailAsync(string email);
    }
}