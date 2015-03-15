using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using tba.Users.Models;

namespace tba.Users.Interfaces
{
    public interface IAuthService : IDisposable
    {
        Task<IdentityResult> RegisterUser(UserLoginModel userLoginModel);
        Task<IdentityUser> FindUser(string userName, string password);
        Task<IdentityUser> FindAsync(UserLoginInfo loginInfo);
        Task<IdentityResult> CreateAsync(IdentityUser user);
        Task<IdentityResult> AddLoginAsync(long userId, UserLoginInfo login);
    }
}