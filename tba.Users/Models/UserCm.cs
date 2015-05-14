using System;
using tba.Users.Entities;

namespace tba.Users.Models
{
    /// <summary>
    /// A create a new User model
    /// </summary>
    public class UserCm
    {
        public string Email { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Mapper from this create user model to a TbaUser entity
        /// </summary>
        /// <returns>a TbaUser entity</returns>
        public TbaUser ToEntity(Func<string, string> hashProvider)
        {
            return TbaUser.Create(Email, hashProvider(Password));
        }
    }
}